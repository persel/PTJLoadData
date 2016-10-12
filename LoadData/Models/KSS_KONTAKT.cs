namespace LoadData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KSS_JAG.KSS_KONTAKT")]
    public partial class KSS_KONTAKT
    {
        public byte ID { get; set; }

        [Required]
        [StringLength(30)]
        public string TEXT { get; set; }
    }
}
