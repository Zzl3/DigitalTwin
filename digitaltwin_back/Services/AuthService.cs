using auth.Database;
using auth.Models;
using auth.Utility;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJWTService _jwtService;
        private readonly AppDbContext _database;
        private readonly IDatabase _redis;
        public AuthService(IJWTService jWTService, AppDbContext appDbContext, IConnectionMultiplexer connection)
        {
            _jwtService = jWTService;
            _database = appDbContext;
            _redis = connection.GetDatabase();
        }

        public async Task<User> GetUserInfo(uint userId)
        {
            User userInfo = null;
            try
            {
                userInfo = await _database.Users.Where(u => u.userId == userId).FirstAsync();
            }
            catch (Exception)
            {
                userInfo = null;
            }
            return userInfo;
        }

        public async Task<Tuple<string, string>> TORefreshTokenAsync(string refreshToken)
        {
            return await _jwtService.GetAccessByRefresh(refreshToken);
        }

        public async Task<Tuple<int, string, Tuple<string, string>, User>> UserLoginAsync(string phone, string password)
        {
            int code = 200;
            string message = "success";
            User user = null;
            try
            {
                user = await _database.Users.Where(u => u.phone.Equals(phone)).FirstAsync();
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("连接数据库错误 - OperationCanceledException : ");
                Console.WriteLine(e.GetType().ToString());
                Console.WriteLine(e.ToString(), e.StackTrace);
                throw;
            }
            catch (InvalidOperationException)
            {
                code = 4011;
                message = "no such user";
            }

            if (user != null)
            {
                if (user.password.Equals(password))
                {
                    var v = _jwtService.GetAccessRefreshTokens(user);
                    return new Tuple<int, string, Tuple<string, string>, User>(code, message, v, user);
                }
                else
                {
                    // 4041表示用户输入密码错误
                    code = 4041;
                    message = "password error";
                }
            }
            else
            {
                // 4011表示未查询到该用户，即账号错误
                code = 4011;
                message = "no such user";
            }
            return new Tuple<int, string, Tuple<string, string>, User>(code, message, null, null);
        }

        public bool UserLoginOut(string refreshToken)
        {
            return _jwtService.DeleteRefreshToken(refreshToken);
        }
    }
}
