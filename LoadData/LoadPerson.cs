using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using LoadData.DTOModel;
using LoadData.Models;

namespace LoadData
{
    static class LoadPerson
    {

        public const string DateFormat = "yyyy-MM-dd";
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm";

        public static async void SendDataToPersonService(List<KSS_ANSTALLNING> persons)
        {
            var stopWatch = new Stopwatch();

            var personDTOList = persons.Select(p => new PersonInputDTO()
            {
                Personnummer = p.PERSNR.ToString(),
                Fornamn = p.FNAMN,
                Mellannamn = "",
                Efternamn = p.ENAMN,
                Namn = p.FNAMN + " " + p.ENAMN,
                systemId = $"DB20-{p.PERSNR.ToString()}",
                uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                uppdateradAv = "PSE",
                skapadDatum = DateTime.Now.ToString(DateTimeFormat),
                skapadAv = "PSE"
            }).ToList();

            stopWatch.Start();
            Console.WriteLine("Start -- SendDataToPersonService");
            //Save person
            //http://localhost:57107//api/Person/person

            using (var client = new HttpClient())
            {
                var personInputDtos = personDTOList as IList<PersonInputDTO> ?? personDTOList.ToList();
                foreach (var p in personInputDtos)
                {
                    var jsonInStringPerson = Newtonsoft.Json.JsonConvert.SerializeObject(p);

                    var response = await client.PostAsync("http://localhost:57107/api/Person/person",
                                                new StringContent(jsonInStringPerson, Encoding.UTF8, "application/json"));

                    if (response.StatusCode.ToString() != "500")
                    {
                        //Console.WriteLine("Sparat " + p.Personnummer + " " + p.Fornamn + " " + p.Efternamn);
                        //Console.WriteLine(response.StatusCode.ToString());
                        //Console.WriteLine("-----------------------");
                    }
                }
            }

            stopWatch.Stop();



            // Format and display the TimeSpan value.
            var ts = stopWatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine("SendDataToPersonService");
            Console.WriteLine("Time to save " + personDTOList.ToList().Count() + " persons " + elapsedTime);
            Console.WriteLine("End -- SendDataToPersonService");


            LoadResultatenhet.SendDataToResultatenhetService(persons);

        }

    }
}