using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models
{
    [Table("t_userloading")]
    public class Userloading
    {
        [Key]
        [Column("time")]
        public string? time { get; set; }
        [Column("user1")]
        public string? user1 { get; set; }
        [Column("user2")]
        public string? user2 { get; set; }
        [Column("user3")]
        public string? user3 { get; set; }
        [Column("user4")]
        public string? user4 { get; set; }
        [Column("user5")]
        public string? user5 { get; set; }
        [Column("user6")]
        public string? user6 { get; set; }
        [Column("user7")]
        public string? user7 { get; set; }
        [Column("user8")]
        public string? user8 { get; set; }
        [Column("user9")]
        public string? user9 { get; set; }
        [Column("user10")]
        public string? user10 { get; set; }
        [Column("user11")]
        public string? user11 { get; set; }
        [Column("user12")]
        public string? user12 { get; set; }
        [Column("user13")]
        public string? user13 { get; set; }
        [Column("user14")]
        public string? user14 { get; set; }
        [Column("user15")]
        public string? user15 { get; set; }

    }
}
