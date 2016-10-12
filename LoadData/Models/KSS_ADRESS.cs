namespace LoadData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KSS_JAG.KSS_ADRESS")]
    public partial class KSS_ADRESS
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1)]
        public string ADRTYP { get; set; }

        [StringLength(50)]
        public string NAMN { get; set; }

        [StringLength(35)]
        public string GADR { get; set; }

        [StringLength(30)]
        public string COADR { get; set; }

        public short? POSTNR { get; set; }

        [StringLength(20)]
        public string ORT { get; set; }

        public byte? LAN { get; set; }

        public short? KOMMUN { get; set; }

        [StringLength(50)]
        public string UTLAND { get; set; }

        public int? CFARNR { get; set; }

        public bool? HEMADR_SOM_UTSADR { get; set; }
    }
}
