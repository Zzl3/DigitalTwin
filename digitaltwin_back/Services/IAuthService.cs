using auth.Models;

namespace auth.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// 用户登录，可等待
        /// </summary>
        /// <param name="phone">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>{状态码code，信息msg，数据data(包括两个jwt的token)}</returns>
        public Task<Tuple<int, string, Tuple<string, string>, User>> UserLoginAsync(string phone, string password);

        /// <summary>
        /// 更新用户的token
        /// </summary>
        /// <param name="refreshToken">用户的refresh_token</param>
        /// <returns>新的access_token</returns>
        public Task<Tuple<string, string>> TORefreshTokenAsync(string refreshToken);

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>登出成功为true，其它为false</returns>
        public bool UserLoginOut(string userId);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>用户信息</returns>
        public Task<User> GetUserInfo(uint userId);
    }
}
