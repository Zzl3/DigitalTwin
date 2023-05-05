using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models
{
    [Table("t_airelectricity")]
    public class Airelectricity
    {
        [Key]
        [Column("Total")]
        public string total { get; set; }
        [Column("Comp1")]
        public string? air1 { get; set; }
        [Column("Comp2")]
        public string? air2 { get; set; }
        [Column("Comp3")]
        public string? air3 { get; set; }
    }
}
