namespace LoadData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KSS_JAG.KSS_ANSTALLNING")]
    public partial class KSS_ANSTALLNING
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PERSNR { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short FTGNR { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string ENHET { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string BEFNR { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KSTNR { get; set; }

        [StringLength(20)]
        public string FNAMN { get; set; }

        [StringLength(40)]
        public string ENAMN { get; set; }

        [StringLength(1)]
        public string ANSVARIG { get; set; }

        public int? BEFKOD { get; set; }

        [StringLength(24)]
        public string BEFTEXT { get; set; }

        public decimal? ARBTIDV { get; set; }

        public DateTime? ANSTDAT { get; set; }

        public DateTime? ANSTDAT_KST { get; set; }

        public DateTime? AVGDAT { get; set; }

        public DateTime? AVGDAT_KST { get; set; }

        public int? LON { get; set; }

        public DateTime? LONDAT { get; set; }

        public DateTime? TJLEDIGFROM { get; set; }

        public DateTime? TJLEDIGTOM { get; set; }

        public decimal? FPROC { get; set; }

        [StringLength(2)]
        public string DELTID_FRANV { get; set; }

        [StringLength(6)]
        public string ALIAS { get; set; }

        public byte? FRANVPROC { get; set; }

        [StringLength(1)]
        public string FLEDIG { get; set; }

        [StringLength(1)]
        public string AKTIV { get; set; }

        [StringLength(1)]
        public string SJUKP { get; set; }

        public decimal? GRUND_ARBTIDV { get; set; }

        [StringLength(1)]
        public string LONETYP { get; set; }

        [StringLength(1)]
        public string CHEF { get; set; }

        [StringLength(4)]
        public string AVTALSKOD { get; set; }

        [StringLength(40)]
        public string AVTALSTEXT { get; set; }

        public int? LONETILLAGG { get; set; }

        public decimal? TIMLON { get; set; }
    }
}
