namespace auth.Utility
{
    /// <summary>
    /// JWT token的配置信息
    /// </summary>
    public class JWTTokenOptions
    {
        // 密钥
        public string SecretKey
        {
            get; set;
        }

        // 发布者
        public string Issuser
        {
            get; set;
        }

        // 订阅者
        public string Audience
        {
            get; set;
        }
    }
}
