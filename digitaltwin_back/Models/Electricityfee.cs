using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models
{
    [Table("t_electricityfee")]
    public class Electricityfee
    {
        [Key]
        [Column("Feetoday")]
        public string Feetoday { get; set; }
        [Column("Comp1")]
        public string? air1 { get; set; }
        [Column("Comp2")]
        public string? air2 { get; set; }
        [Column("Comp3")]
        public string? air3 { get; set; }
    }
}
