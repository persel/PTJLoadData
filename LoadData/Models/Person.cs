using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PTJ.DataLayer.Models
{
    [Table("Person", Schema = "Person")]
    public partial class Person
    {
       
        [Key]
        public long Id { get; set; }
        public string ForNamn { get; set; }
        public string MellanNamn { get; set; }
        public string EfterNamn { get; set; }
        public string PersonNummer { get; set; }
        public DateTime SkapadDatum { get; set; }
        public DateTime? UppdateradDatum { get; set; }
        public string UppdateradAv { get; set; }

    }
}
