using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using Dapper;
using LoadData.DTOModel;
using LoadData.Models;
using Oracle.ManagedDataAccess.Client;

namespace LoadData
{
    static class SaveResultatenhet
    {

        private const string DateFormat = "yyyy-MM-dd";
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm";
        public static async void SendDataToResultatenhetService(IList<Anstallning> persons)
        {
            
            var resList = new List<ResultatenhetInputDTO>();
            var db = new ApplicationDbContext();

            //var orgList = new List();
            var slitList = Split<Anstallning>(persons);

            //To Start only get the org that the persons is using
            foreach (var sublist in slitList)
            {

                var subOrganisationsList = LoadFromOracle.FindResultatenheter(sublist);
                    
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
                                     skapadDatum = org.UPPL_DATUM.ToString(DateTimeFormat),
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
                var host = ConfigurationManager.AppSettings["ServiceHost"];
                foreach (var p in resList)
                {
                    var jsonInStringPerson = Newtonsoft.Json.JsonConvert.SerializeObject(p);
                    var response = await client.PostAsync( host + "/api/Organisation/resultatenhet",
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
            SaveAvtal.AvtalsService(persNrList);

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