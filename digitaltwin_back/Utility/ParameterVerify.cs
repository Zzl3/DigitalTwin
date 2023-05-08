namespace auth.Utility
{
    public class ParameterVerify
    {
        /// <summary>
        /// 验证用户手机号是否合法
        /// </summary>
        /// <param name="phone">用户手机号</param>
        /// <returns>符合规则返回true否则为false</returns>
        public static bool verifyPhone(string phone)
        {
            if (phone == null) { return false; }
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^(1)[3-9]\d{9}$");
        }

        /// <summary>
        /// 验证用户密码是否合法，由8-16位字符组成，至少包含一个大写字母，一个小写字母和一个数字
        /// </summary>
        /// <param name="password"></param>
        /// <returns>符合规则返回true否则为false</returns>
        public static bool verifyPassword(string password)
        {
            if (password == null) { return false; }
            return System.Text.RegularExpressions.Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[\s\S]{8,16}$");
        }
    }
}
