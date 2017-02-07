using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadData.DTOModel
{
    class AdressInputDTO
    {
        public string Personnummer { get; set; }
        public string AdressVariant { get; set; }
        public GatuadressInputDTO GatuadressInput { get; set; }
        public MailInputDTO MailInput { get; set; }
        public TelefonInputDTO TelefonInput { get; set; }

        public string systemId { get; set; }

        public string uppdateradDatum { get; set; }

        public string uppdateradAv { get; set; }

        public string skapadDatum { get; set; }

        public string skapadAv { get; set; }

    }

    public class TelefonInputDTO
    {
        public string Telefonnummer { get; set; }
    }

    public class MailInputDTO
    {
        public string MailAdress { get; set; }
    }

    public class GatuadressInputDTO
    {
        public string AdressRad1 { get; set; }
        public string AdressRad2 { get; set; }
        public string AdressRad3 { get; set; }
        public string AdressRad4 { get; set; }
        public string AdressRad5 { get; set; }
        public int Postnummer { get; set; }
        public string Stad { get; set; }
        public string Land { get; set; }
    }
}
