using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models
{
    [Table("t_systemelectricity")]
    public class Systemelectricity
    {
        [Key]
        [Column("date")]
        public int date { get; set; }
        [Column("fee")]
        public string? fee{ get; set; }
        [Column("pressure")]
        public string? pressure { get; set; }
    }
}
