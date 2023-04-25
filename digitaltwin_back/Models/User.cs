using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models
{
    [Table("t_user")]
    public class User
    {
        [Column("user_id")]
        public int userId { get; set; }

        [Column("phone")]
        public string phone { get; set; }

        [Column("wechat_id")]
        public string? wechatId { get; set; }

        [Column("login_pwd")]
        public string password { get; set; }

        [Column("user_name")]
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
