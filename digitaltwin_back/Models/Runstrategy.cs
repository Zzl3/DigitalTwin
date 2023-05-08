using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace auth.Models
{
    [Table("t_runstrategy")]
    public class Runstrategy
    {
        [Key]
        [Column("air")]
        public string air { get; set; }
        [Column("time1")]
        public string? time1 { get; set; }
        [Column("pressure1")]
        public string? pressure1 { get; set; }
        [Column("time2")]
        public string? time2 { get; set; }
        [Column("pressure2")]
        public string? pressure2 { get; set; }
    }
}
