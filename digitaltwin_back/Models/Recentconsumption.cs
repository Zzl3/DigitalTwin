using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//对应开关
namespace auth.Models
{
    [Table("t_recentconsumption")]
    public class Recentconsumption
    {
        [Key]
        [Column("Date")]
        public int date { get; set; }
        [Column("Comp1")]
        public string? air1 { get; set; }
        [Column("Comp2")]
        public string? air2 { get; set; }
        [Column("Comp3")]
        public string? air3 { get; set; }
    }
}
