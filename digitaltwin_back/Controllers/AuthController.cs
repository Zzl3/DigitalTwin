using auth.Dtos;
using auth.Services;
using auth.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace auth.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public class LoginDto
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDto login)
        {
            int code = 200;
            string msg = string.Empty;
            string phone = login.username;
            string password = login.password;
            //Console.WriteLine("phone =" + phone);
            if (ParameterVerify.verifyPhone(phone) && ParameterVerify.verifyPassword(password))
            {
                try
                {
                    var data = await _authService.UserLoginAsync(phone, password);
                    var user = data.Item4;
                    UserDto? userInfo = null;
                    if (user != null)
                    {
                        userInfo = new UserDto()
                        {
                            userId = user.userId,
                            phone = user.phone,
                            wechatId = user.wechatId,
                            userName = user.userName,
                            avatar = user.avatar,
                            sex = user.sex,
                            birthdate = user.birthdate,
                            roles = user.roles,
                            setting = user.setting,
                            residence = user.residence,
                            created = user.created,
                        };
                    }
                    var result1 = new
                    {
                        code = data.Item1,
                        msg = data.Item2,
                        data = new
                        {
                            accessToken = data.Item3?.Item1,
                            refreshToken = data.Item3?.Item2,
                            userInfo
                        }
                    };
                    return Ok((result1));
                }
                catch (Exception)
                {
                    var result2 = new
                    {
                        code = 500,
                        msg = "Internal error."
                    };
                    //Console.WriteLine(e.StackTrace);
                    return BadRequest((result2));
                }
            }
            if (phone.IsNullOrEmpty() || ParameterVerify.verifyPhone(phone) == false)
            {
                code = 4011;
                msg = "Phone is not valid.";
            }
            else if (password.IsNullOrEmpty() || ParameterVerify.verifyPassword(password) == false)
            {
                code = 4012;
                msg = "Password is not valid.";
            }
            var result3 = new { code = code, msg = msg };
            return Ok((result3));
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult UserLogout([FromForm] string refreshToken)
        {
            var code = 200;
            var msg = "success";
            Console.WriteLine(refreshToken);
            var success = _authService.UserLoginOut(refreshToken);
            if (success)
            {
                return Ok((new
                {
                    code,
                    msg,
                    data = ""
                }));
            }
            else
            {
                return Ok(new
                {
                    code = 401,
                    msg = "token已失效",
                    data = ""
                });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> ToRefreshTokenAsync([FromForm] string refreshToken)
        {
            var code = 200;
            var msg = "success";
            string newAccess = null;
            string newRefresh = null;
            try
            {
                (newAccess, newRefresh) = (await _authService.TORefreshTokenAsync(refreshToken)).ToValueTuple();
            }
            catch (Exception)
            {
                code = 400;
                msg = "error";
            }
            if (!newAccess.IsNullOrEmpty() && !newRefresh.IsNullOrEmpty())
            {
                return Ok(new
                {
                    code,
                    msg,
                    data = new
                    {
                        newAccess,
                        newRefresh
                    }
                });
            }
            else
            {
                return Ok(new
                {
                    code = 401,
                    msg = "Login timeout.",
                    data = ""
                });
            }
        }


        [Authorize]
        [HttpGet("userinfo")]
        public async Task<IActionResult> getRequestUserInfo()
        {
            uint userId;
            int code = 200;
            String msg = "success";
            UserDto? userInfo;
            try
            {
                userId = uint.Parse(User.FindFirst("userId")?.Value.ToString());
                var user = await _authService.GetUserInfo(userId);
                userInfo = new UserDto()
                {
                    userId = user.userId,
                    phone = user.phone,
                    wechatId = user.wechatId,
                    userName = user.userName,
                    avatar = user.avatar,
                    sex = user.sex,
                    birthdate = user.birthdate,
                    roles = user.roles,
                    setting = user.setting,
                    residence = user.residence,
                    created = user.created,
                };
            }
            catch (Exception)
            {
                code = 400;
                msg = "数据解析出错";
                userInfo = null;
            }
            return Ok(new { code, msg, userInfo });
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult Test([FromHeader] string Token)
        {
            return Ok(new
            {
                Token,
                context = this.HttpContext.ToString(),
                User = this.HttpContext.User,
                Header = this.HttpContext.Request.Headers,
                // 获取用户ID
                userId = this.User.FindFirst("userId")?.Value.ToString(),
                // 获取用户角色
                roles = this.User.FindFirst(ClaimTypes.Role)?.Value.ToString(),
            });
        }
    }
}
