namespace WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        [StringLength(255)]
        public string ac01 { get; set; }

        [Required]
        [StringLength(255)]
        public string ac02 { get; set; }

        public DateTime ac03 { get; set; }

        [Required]
        [StringLength(255)]
        public string ac04 { get; set; }

        public DateTime ac05 { get; set; }

        [StringLength(255)]
        public string ac06 { get; set; }

        [StringLength(255)]
        public string ac07 { get; set; }

        public bool ac08 { get; set; }

        [Required]
        [StringLength(255)]
        public string ac09 { get; set; }

        [Required]
        [StringLength(255)]
        public string ac10 { get; set; }

        public int ac11 { get; set; }
    }
}
