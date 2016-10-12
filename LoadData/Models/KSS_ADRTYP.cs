namespace LoadData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KSS_JAG.KSS_ADRTYP")]
    public partial class KSS_ADRTYP
    {
        [Key]
        [StringLength(1)]
        public string ADRTYP { get; set; }

        [Required]
        [StringLength(25)]
        public string TEXT { get; set; }

        [Required]
        [StringLength(6)]
        public string FKT { get; set; }
    }
}
