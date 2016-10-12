namespace LoadData.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KSS_JAG.KSS_KOSTNADSSTALLE")]
    public partial class KSS_KOSTNADSSTALLE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KSTNR { get; set; }

        public byte BOLAG { get; set; }

        public byte? HAFFOMR { get; set; }

        public byte? AFFOMR { get; set; }

        public byte? AFFOMR_GRUPP { get; set; }

        public byte? FINANSFORM { get; set; }

        public byte? YRKESOMR { get; set; }

        [Required]
        [StringLength(1)]
        public string KSTTYP { get; set; }

        public long? PERSNR { get; set; }

        public short? ARBNR { get; set; }

        [StringLength(20)]
        public string FNAMN { get; set; }

        [StringLength(20)]
        public string ENAMN { get; set; }

        [StringLength(5)]
        public string TEAM { get; set; }




        public DateTime? OPENDAT { get; set; }

        public DateTime? STAENGDAT { get; set; }

        public DateTime? SLUTAVRDAT { get; set; }

        [StringLength(1)]
        public string VILANDE { get; set; }

        [StringLength(2)]
        public string AVGORSAK { get; set; }

        [StringLength(2)]
        public string AVDGRUPP { get; set; }

        public byte? ENHET { get; set; }

        [StringLength(1)]
        public string RANTA { get; set; }

        [StringLength(1)]
        public string VARULAGER_FLG { get; set; }

        [StringLength(1)]
        public string PATFOR_FLG { get; set; }

        [StringLength(1)]
        public string FK_FLG { get; set; }

        public decimal? MOMSPLIKT { get; set; }

        [StringLength(4)]
        public string HANDL_1480 { get; set; }

        [StringLength(2000)]
        public string NOTERING { get; set; }

        [StringLength(1)]
        public string VIKARIE { get; set; }

        public int? BUFFERT { get; set; }

        public int? ORGANISERAT_UNDER_KSTNR { get; set; }

        public byte? KONTAKT_ID { get; set; }

        [StringLength(30)]
        public string KONTAKT_KOMMENTAR { get; set; }

        public byte? KONTAKT_FREKVENS_ID { get; set; }

        [StringLength(30)]
        public string KONTAKT_FREKVENS_KOM { get; set; }

        public bool? VARNINGSMARKERING { get; set; }

        public bool? HEMADR_SOM_UTSADR { get; set; }

        [StringLength(31)]
        public string HSAIDENTITY { get; set; }

        public bool? SYNKAS_MOT_HSA { get; set; }

        [StringLength(1024)]
        public string PTJ_BOKA_URL { get; set; }

        [StringLength(64)]
        public string GEOGRAPHICALCOORDINATES { get; set; }

        public bool VISA_HSA_POST { get; set; }

        public DateTime? VILANDE_FROMDAT { get; set; }

        public DateTime? VILANDE_TOMDAT { get; set; }

        [StringLength(1)]
        public string MOTTAGNING_EXIT { get; set; }

        public byte? AVGORSAK_EGEN_BEGARAN { get; set; }

        public DateTime? UPPL_DATUM { get; set; }

        [StringLength(1)]
        public string OPPEN_SLUTENV { get; set; }
    }
}
