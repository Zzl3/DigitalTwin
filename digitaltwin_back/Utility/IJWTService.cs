using auth.Models;

namespace auth.Utility
{
    public interface IJWTService
    {
        /// <summary>
        /// 根据用户信息生成jwt的token，默认token的有效期为10min
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>jwt的token字符串</returns>
        string GetJWTToken(User userInfo);

        /// <summary>
        /// 根据用户信息获取access_token和refresh_token
        /// access_token有效期默认为30分钟
        /// refresh_token有效期默认为一天
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns>两个jwt的token字符串</returns>
        Tuple<string,string> GetAccessRefreshTokens(User userInfo);

        /// <summary>
        /// 基于refresh_token获取access_token，即token的刷新
        /// </summary>
        /// <param name="refereToken">refresh_token</param>
        /// <returns>access_token</returns>
        Task<Tuple<string,string>> GetAccessByRefresh(string refereToken);

        /// <summary>
        /// 判断refreshToken是否在redis中存在
        /// </summary>
        /// <param name="refreshToken">用户的refresh_token</param>
        /// <returns>refreshToken在redis中存在则返回true，否则返回false</returns>
        bool RefreshTokenExisted(string refreshToken);

        /// <summary>
        /// 用户登出，删除refresh_token
        /// </summary>
        /// <param name="refreshToken">用户的refresh_token</param>
        /// <returns>成功删除返回true，否则返回false</returns>
        public bool DeleteRefreshToken(string refreshToken);
    }
}
