using auth.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace auth.Utility
{
    public class JWTService : IJWTService
    {
        private static readonly int accessTokenLife = 60 * 60; // access_token的有效期(s)
        private static readonly int refreshTokenLife = 60 * 60 * 24; // refresh_token的有效期(s)
        private readonly JWTTokenOptions _jwtOptions;
        private readonly IDatabase _redis;
        public JWTService(IOptions<JWTTokenOptions> jwtOptions, IConnectionMultiplexer connectionMultiplexer)
        {
            _jwtOptions = jwtOptions.Value;
            _redis = connectionMultiplexer.GetDatabase();
        }
        public async Task<Tuple<string, string>> GetAccessByRefresh(string refreshToken)
        {
            var userInfo = await _redis.HashGetAllAsync($@"Auth:{refreshToken}");
            if (!userInfo.IsNullOrEmpty())
            {
                string userId = "", roles = "";
                foreach (var item in userInfo)
                {
                    if (item.Name.Equals("userId"))
                    {
                        userId = item.Value.ToString();
                    }
                    else if (item.Name.Equals("roles"))
                    {
                        roles = item.Value.ToString();
                    }
                }
                var newRefreshToken = IssueToken(userId, roles, refreshTokenLife);
                await _redis.KeyRenameAsync($@"Auth:{refreshToken}", $@"Auth:{newRefreshToken}");
                string accessToken = IssueToken(userId, roles, accessTokenLife);
                return Tuple.Create(accessToken, newRefreshToken);
                //return accessToken;
            }
            else
            {
                return Tuple.Create(string.Empty, string.Empty);
            }
        }

        public Tuple<string, string> GetAccessRefreshTokens(User userInfo)
        {
            string accessToken = IssueToken(userInfo.userId.ToString(), userInfo.roles.ToString(), accessTokenLife);
            string refreshToken = IssueToken(userInfo.userId.ToString(), userInfo.roles.ToString(), refreshTokenLife);

            // 将用户的refreshToken保存在redis中,并设置有效期，过期将删除
            var value = new[]
            {
                new HashEntry("userId", userInfo.userId.ToString()),
                new HashEntry("roles", userInfo.roles.ToString()),
            };
            _redis.HashSet($@"Auth:{refreshToken}", value);
            _redis.KeyExpire($@"Auth:{refreshToken}", new TimeSpan(0, 0, refreshTokenLife));
            return Tuple.Create(accessToken, refreshToken);
        }

        public string GetJWTToken(User userInfo)
        {
            return IssueToken(userInfo.userId.ToString(), userInfo.roles.ToString(), accessTokenLife);
        }

        public bool RefreshTokenExisted(string refreshToken)
        {
            return _redis.KeyExists($@"Auth:{refreshToken}");
        }

        public bool DeleteRefreshToken(string refreshToken)
        {
            return _redis.KeyDelete($@"Auth:{refreshToken}");
        }

        /// <summary>
        /// 根据用户信息生成JWT的token
        /// </summary>
        /// <param name="userId">用户的唯一标识</param>
        /// <param name="roles">用户的角色，多个角色以,分隔</param>
        /// <param name="lifeSecond">token的生存期，默认为600秒</param>
        /// <returns>jwt的token字符串</returns>
        private string IssueToken(string userId, string roles, int lifeSecond = 600)
        {
            var claims = new[]
            {
                new Claim("userId",userId.ToString()),
            };
            var roleClaims = (from r in roles.Trim().Split(",")
                              select new Claim(ClaimTypes.Role, r.Trim().ToString())).ToArray();
            claims = claims.Concat(roleClaims).ToArray();

            // 生成数字签名
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);

            // jwt Token
            var securityToken = new JwtSecurityToken(
                signingCredentials: signingCredentials, //密钥
                issuer: _jwtOptions.Issuser,// 接收者
                audience: _jwtOptions.Audience,// 发行方
                claims: claims, // 负载
                notBefore: DateTime.Now, // 生效时间
                expires: DateTime.Now.Add(new TimeSpan(0, 0, lifeSecond)) // 到期时间
                );
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenStr;
        }
    }
}
