using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models
{
    [Table("t_systemair")]
    public class Systemair
    {
        [Key]
        [Column("air")]
        public string air { get; set; }
        [Column("ratio")]
        public string? ratio { get; set; }
        [Column("fee")]
        public string? fee { get; set; }
        [Column("pressure")]
        public string? pressure { get; set; }
        [Column("usernum")]
        public string? usernum { get; set; }
    }
}
