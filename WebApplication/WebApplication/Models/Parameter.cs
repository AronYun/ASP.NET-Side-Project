namespace WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Parameter")]
    public partial class Parameter
    {
        [Key]
        [StringLength(255)]
        public string prm01 { get; set; }

        [Required]
        [StringLength(255)]
        public string prm02 { get; set; }

        public DateTime prm03 { get; set; }

        [Required]
        [StringLength(255)]
        public string prm04 { get; set; }

        public DateTime prm05 { get; set; }

        [StringLength(255)]
        public string prm06 { get; set; }

        [StringLength(255)]
        public string prm07 { get; set; }
    }
}
