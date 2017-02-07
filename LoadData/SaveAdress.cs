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

        public static async void PersonAdressService(IList<Anstallning> persons)
        {
            var resList = new List<AdressInputDTO>();
            var host = ConfigurationManager.AppSettings["ServiceHost"];
            Console.WriteLine("Start -- SaveAdress");
            var slitList = Split<Anstallning>(persons);

            //To Start only get the org that the persons is using
            foreach (var sublist in slitList)
            {
                var subAdresssList = LoadFromOracle.FindAdress(sublist);

                foreach (var adress in subAdresssList)
                {
                    int postnr;
                    int.TryParse(adress.POSTNR, out postnr);
                    var ad = new AdressInputDTO()
                    {
                        Personnummer = adress.PERSNR.ToString(),
                        AdressVariant = "1", //Hem adress
                        GatuadressInput =
                            new GatuadressInputDTO()
                            {
                                AdressRad1 = adress.BOSTADR,
                                Postnummer = postnr,
                                Stad = adress.PADR
                            },
                        TelefonInput = new TelefonInputDTO() {Telefonnummer = ""},
                        MailInput = new MailInputDTO() {MailAdress = ""},
                        systemId = $"DB{adress.KSTNR}{adress.KSTTYP}",
                        uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                        uppdateradAv = "PSE",
                        skapadDatum = DateTime.Now.ToString(DateTimeFormat),
                        skapadAv = "PSE"
                    };
                }
            }

            resList = resList.GroupBy(o => o.Personnummer).Select(grp => grp.First()).ToList();


            //Save resultatenhet
            //http://localhost:57107/api/Adress/personadress

            using (var client = new HttpClient())
            {
                //var personInputDtos = personDTOList as IList<PersonInputDTO> ?? personDTOList.ToList();
                foreach (var p in resList)
                {
                    var jsonInStringPerson = Newtonsoft.Json.JsonConvert.SerializeObject(p);
                    var response = await client.PostAsync(host + "/api/Adress/personadress",
                        new StringContent(jsonInStringPerson, Encoding.UTF8, "application/json"));
                }
            }

            Console.WriteLine("END -- SaveAdress");
        }


        private static List<List<long>> Split<T>(IEnumerable<Anstallning> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x.PERSNR })
                .GroupBy(x => x.Index / 900)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
