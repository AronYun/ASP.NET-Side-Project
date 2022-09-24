namespace WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string lg01 { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime lg02 { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(255)]
        public string lg03 { get; set; }

        [StringLength(255)]
        public string lg04 { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string lg05 { get; set; }

        [StringLength(255)]
        public string lg06 { get; set; }
    }
}
