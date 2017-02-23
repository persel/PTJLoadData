using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoadData.DTOModel;
using LoadData.Models;

namespace LoadData
{
    internal static class SaveAdress
    {

        private const string DateFormat = "yyyy-MM-dd";
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm";

        public static async void SendToOdlOrg(IList<dynamic> adressInputWorkDTOList)
        {
            await CallOdlService.Send(adressInputWorkDTOList, "/api/Adress/organisationadress");
        }

        public static async void SendToOdlPerson(IList<dynamic> adressInputWorkDTOList)
        {
            await CallOdlService.Send(adressInputWorkDTOList, "/api/Adress/personadress");
        }

        public static List<dynamic> CreatListOHomeAdressPersonInputDTO(List<dynamic> adresssListHome)
        {
            var resListAdress = new List<dynamic>();
            foreach (var adress in adresssListHome)
            {

                var ad = new AdressInputDTO()
                {
                    Personnummer = adress.ID.ToString(),
                    AdressVariant = "Folkbokföringsadress", //Hem adress
                    GatuadressInput =
                        new GatuadressInputDTO()
                        {
                            AdressRad1 = adress.GADR,
                            Postnummer = adress.POSTNR,
                            Stad = adress.ORT
                        },
                    systemId = $"DB{adress.KSTNR}{adress.KSTTYP}",
                    uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                    uppdateradAv = "PSE",
                    skapadDatum = DateTime.Now.ToString(DateTimeFormat),
                    skapadAv = "PSE"
                };
                resListAdress.Add(ad);

                var adTel = new AdressInputDTO()
                {
                    Personnummer = adress.ID.ToString(),
                    AdressVariant = "Telefon Privat",
                    TelefonInput = new TelefonInputDTO() {Telefonnummer = adress.TELNR},
                    systemId = $"DB{adress.KSTNR}{adress.KSTTYP}",
                    uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                    uppdateradAv = "PSE",
                    skapadDatum = DateTime.Now.ToString(DateTimeFormat),
                    skapadAv = "PSE"
                };
                resListAdress.Add(adTel);

                var adMail = new AdressInputDTO()
                {
                    Personnummer = adress.ID.ToString(),
                    AdressVariant = "Mailadress Privat",
                    MailInput = new MailInputDTO() {MailAdress = adress.EMAIL},
                    systemId = $"DB{adress.KSTNR}{adress.KSTTYP}",
                    uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                    uppdateradAv = "PSE",
                    skapadDatum = DateTime.Now.ToString(DateTimeFormat),
                    skapadAv = "PSE"
                };
                resListAdress.Add(adMail);
            }


            return resListAdress;
        }

        public static List<dynamic> CreatListOfWorkAdressPersonInputDTO(List<dynamic> adresssListWork)
        {
            var resListAdress = new List<dynamic>();
           
            foreach (var adressWork in adresssListWork)
            {
                
                var workPhone = LoadFromOracle.FindWorkPhone(adressWork.ID);

                var adW = new AdressInputDTO()
                {
                    Personnummer = adressWork.PERSNR.ToString(),
                    GatuadressInput =
                        new GatuadressInputDTO()
                        {
                            AdressRad1 = adressWork.GADR,
                            Postnummer = adressWork.POSTNR,
                            Stad = adressWork.ORT
                        },
                    systemId = $"DB{adressWork.KSTNR}{adressWork.KSTTYP}",
                    uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                    uppdateradAv = "PSE",
                    skapadDatum = DateTime.Now.ToString(DateTimeFormat),
                    skapadAv = "PSE",
                    AdressVariant = adressWork.ADRTYP == "U" ? "Adress Arbete" : "LeveransAdress"
                };
                resListAdress.Add(adW);

                foreach (var phone in workPhone)
                {
                    var adTel = new AdressInputDTO
                    {
                        Personnummer = adressWork.PERSNR.ToString(),
                        TelefonInput = new TelefonInputDTO() {Telefonnummer = phone.TELNR},
                        systemId = $"DB{phone.KSTNR}{phone.KSTNRKSTTYP}",
                        uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                        uppdateradAv = "PSE",
                        skapadDatum = DateTime.Now.ToString(DateTimeFormat),
                        skapadAv = "PSE",
                        AdressVariant = phone.TELTYP == 10 ? "Telefon Arbete" : "Mobil Arbete"
                    };
                    resListAdress.Add(adTel);
                }
            }


            return resListAdress;

        }

        public static List<dynamic> CreatListOfAdressResultatEnheterInputDTO(IEnumerable<dynamic> adresssListResultatEnheter)
        {
            var resListAdress = new List<dynamic>();
            foreach (var adressWork in adresssListResultatEnheter)
            {
                var w = new AdressOrgInputDTO
                {
                    KostnadsstalleNr = adressWork.KSTNR.ToString(),
                    GatuadressInput =
                        new GatuadressInputDTO()
                        {
                            AdressRad1 = adressWork.GADR,
                            Postnummer = adressWork.POSTNR,
                            Stad = adressWork.ORT
                        },
                    systemId = $"DB{adressWork.KSTNR}{adressWork.KSTTYP}",
                    uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                    uppdateradAv = "MAH",
                    skapadDatum = DateTime.Now.ToString(DateTimeFormat),
                    skapadAv = "MAH"
                };

                if (adressWork.ADRTYP == "L")
                {
                    w.AdressVariant = "LeveransAdress";
                }
                else if (adressWork.ADRTYP == "U")
                {
                    w.AdressVariant = "FaktureringsAdress";
                }
                else if (adressWork.ADRTYP == "H" )
                {
                    w.AdressVariant = "Folkbokföringsadress";
                }
                else if (adressWork.ADRTYP == "B")
                {
                    w.AdressVariant = "Folkbokföringsadress";
                }

                resListAdress.Add(w);
            }

            return resListAdress;

        }
    }
}
