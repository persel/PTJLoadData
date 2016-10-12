namespace LoadData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KSS_JAG.KSS_AFFOMR")]
    public partial class KSS_AFFOMR
    {
        [Key]
        public byte AFFOMR { get; set; }

        public byte AFFOMR_HAFFOMR { get; set; }

        [Required]
        [StringLength(40)]
        public string TEXT { get; set; }

        [StringLength(10)]
        public string FKT { get; set; }
    }
}
