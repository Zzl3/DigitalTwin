using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//对应优化前后的表
namespace auth.Models
{
    [Table("t_newupdateconsumption")]
    public class NewConsumption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("min")]
        public int min { get; set; }
        [Column("before")]
        public string? before { get; set; }
        [Column("after")]
        public string? after { get; set; }
    }
}
