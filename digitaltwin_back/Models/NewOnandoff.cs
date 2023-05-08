using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//对应开关
namespace auth.Models
{
    [Table("t_newonandoff")]
    public class NewOnandoff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("min")]
        public int min { get; set; }
        [Column("air1")]
        public string? air1 { get; set; }
        [Column("air2")]
        public string? air2 { get; set; }
        [Column("air3")]
        public string? air3 { get; set; }
    }
}
