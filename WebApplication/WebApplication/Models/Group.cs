namespace WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Group")]
    public partial class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int gp01 { get; set; }

        [Required]
        [StringLength(255)]
        public string gp02 { get; set; }

        public DateTime gp03 { get; set; }

        [Required]
        [StringLength(255)]
        public string gp04 { get; set; }

        public DateTime gp05 { get; set; }

        [StringLength(255)]
        public string gp06 { get; set; }

        [StringLength(255)]
        public string gp07 { get; set; }

        public bool gp08 { get; set; }

        public bool gp09 { get; set; }
    }
}
