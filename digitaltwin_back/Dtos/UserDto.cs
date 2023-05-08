namespace auth.Dtos
{
    public class UserDto
    {
        public int userId { get; set; }

        public string phone { get; set; }

        public string? wechatId { get; set; }

        public string? userName { get; set; }

        public string? avatar { get; set; }

        public string? sex { get; set; }

        public string? birthdate { get; set; }

        public string roles { get; set; }

        public string? setting { get; set; }

        public string? residence { get; set; }

        public string? created { get; set; }
    }
}
