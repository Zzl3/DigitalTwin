using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models
{
    [Table("t_data")]
    public class Data
    {
        [Key]
        [Column("id")]
        public int id { get; set; }
        [Column("proper1")]
        public string? proper1 { get; set; }
        [Column("proper2")]
        public string? proper2 { get; set; }
        [Column("proper3")]
        public string? proper3 { get; set; }
    }
}
