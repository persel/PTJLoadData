
using PTJ.DataLayer.Models;
using System.Collections.Generic;


namespace PersonSvc.ViewModels
{
    public class PersonViewModelSave
    {

        public long Id { get; set; }
        public string ForNamn { get; set; }
        public string MellanNamn { get; set; }
        public string EfterNamn { get; set; }
        public string PersonNummer { get; set; }
        public string UppdateradAvAlias { get; set; }


        public bool PersonAnstalld { get; set; }
        public long AnstalldFkid { get; set; }


        public bool PersonKonsult { get; set; }
        public long KonsultFkid { get; set; }

       
        public bool PersonSjukHalsovardsPersonal { get; set; }
        public long SjukHalsovardsPersonalFKID { get; set; }


        public bool PersonPatient { get; set; }
        public long PatientFkid { get; set; }

        public bool PersonAnnanPerson { get; set; }
        public long AnnanPersonFkid { get; set; }

    }
}
