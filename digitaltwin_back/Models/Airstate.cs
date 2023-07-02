using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models
{
    [Table("t_airstate")]
    public class Airstate
    {
        [Key]
        [Column("air")]
        public string air { get; set; }
        [Column("state")]
        public string? state { get; set; }
        [Column("stress")]
        public string? stress { get; set; }
        [Column("timepre")]
        public string? timepre { get; set; }
    }
}
