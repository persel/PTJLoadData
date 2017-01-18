using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using LoadData.DTOModel;
using LoadData.Models;

namespace LoadData
{
    static class LoadResultatenhet
    {

        private const string DateFormat = "yyyy-MM-dd";
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm";
        public static async void SendDataToResultatenhetService(List<KSS_ANSTALLNING> persons)
        {
            
            var resList = new List<ResultatenhetInputDTO>();
            var db = new ApplicationDbContext();

            //var orgList = new List();
            var slitList = Split<KSS_ANSTALLNING>(persons);

            //To Start only get the org that the persons is using
            foreach (var sublist in slitList)
            {

                var subOrganisationsList = (from fa in db.KSS_KOSTNADSSTALLE
                                            where sublist.Contains(fa.PERSNR.Value)
                                            orderby fa.KSTNR ascending
                                            select new
                                            {
                                                fa.KSTNR,
                                                fa.KSTTYP,
                                                fa.ENAMN,
                                                fa.FNAMN,
                                                fa.UPPL_DATUM
                                            }).ToList();

                  resList.AddRange(from org in subOrganisationsList
                                 where org.UPPL_DATUM != null
                                 select new ResultatenhetInputDTO()
                                 {
                                     OrganisationsId = "0",
                                     KostnadsstalleNr = org.KSTNR,
                                     Namn = org.FNAMN + " " + org.ENAMN,
                                     Typ = org.KSTTYP,
                                     systemId = $"DB{org.KSTNR}{org.KSTTYP}",
                                     uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                                     uppdateradAv = "PSE",
                                     skapadDatum = org.UPPL_DATUM.Value.ToString(DateTimeFormat),
                                     skapadAv = "PSE"
                                 });
            }

            resList = resList.GroupBy(o => o.KostnadsstalleNr).Select(grp => grp.First()).ToList();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
        
            Console.WriteLine("Start -- SendDataToResultatenhetService");



            //Save resultatenhet
            //http://localhost:57107/api/Organisation/resultatenhet


            using (var client = new HttpClient())
            {
                //var personInputDtos = personDTOList as IList<PersonInputDTO> ?? personDTOList.ToList();
                foreach (var p in resList)
                {
                    var jsonInStringPerson = Newtonsoft.Json.JsonConvert.SerializeObject(p);
                    var response = await client.PostAsync("http://localhost:57107/api/Organisation/resultatenhet",
                                                new StringContent(jsonInStringPerson, Encoding.UTF8, "application/json"));
                }
            }

            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine("SendDataToResultatenhetService");
            Console.WriteLine("Time to save " + resList.ToList().Count() + " Organisation " + elapsedTime);
            Console.WriteLine("End -- SendDataToResultatenhetService");

            var persNrList = persons.Select(p => p.PERSNR.ToString()).ToList();
            LoadAvtal.SendDataToAvtalsService(persNrList);

        }




        public static List<List<long>> Split<T>(List<KSS_ANSTALLNING> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x.PERSNR })
                .GroupBy(x => x.Index / 900)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


    }
}