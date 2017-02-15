using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LoadData.DTOModel;
using LoadData.Models;

namespace LoadData
{
    public static class SavePerson
    {
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm";

        public static async Task SendDataToPersonService(IList<Anstallning> persons)
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
            Console.WriteLine("Start -- SendDataToPersonService");

            stopWatch.Start();

            await CallOdlService.Send(personDTOList, "/api/Person/person");
            



            // Format and display the TimeSpan value.
            var ts = stopWatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine("SendDataToPersonService");
            Console.WriteLine("Time to save " + personDTOList.ToList().Count() + " persons " + elapsedTime);
            Console.WriteLine("End -- SendDataToPersonService");

            stopWatch.Stop();
        }

     

    }
}