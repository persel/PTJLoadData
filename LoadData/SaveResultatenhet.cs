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
using System.Threading.Tasks;

namespace LoadData
{
    static class SaveResultatenhet
    {

        private const string DateFormat = "yyyy-MM-dd";
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm";
  

        private static List<List<long>> Split<T>(IEnumerable<long> source)
        {
            return source
                .Select((x, i) => new {Index = i, Value = x})
                .GroupBy(x => x.Index / 990)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


        public static async Task SendDataToResultatenhetServiceByKstnr(IEnumerable<long> kstnrList)
        {
            var resList = new List<ResultatenhetInputDTO>();

            var slitList = Split<long>(kstnrList);

            foreach (var sublist in slitList)
            {
                var subOrganisationsList = LoadFromOracle.FindResultatenheterById(sublist);

                resList.AddRange(from org in subOrganisationsList
                    where org.UPPL_DATUM != null
                    select new ResultatenhetInputDTO()
                    {
                        OrganisationsId = "0",
                        KostnadsstalleNr = org.KSTNR,
                        Namn = org.FNAMN + "  " + org.ENAMN,
                        Typ = org.KSTTYP,
                        systemId = $"DB{org.KSTNR}{org.KSTTYP}",
                        uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                        uppdateradAv = "PSE",
                        skapadDatum = org.UPPL_DATUM.ToString(DateTimeFormat),
                        skapadAv = "PSE"
                    });

            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Console.WriteLine("Start -- SendDataToResultatenhetService " + resList.Count);

            await CallOdlService.Send(resList, "/api/Organisation/resultatenhet");

         
            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine("SendDataToResultatenhetService");
            Console.WriteLine("Time to save " + resList.ToList().Count() + " Organisation " + elapsedTime);
            Console.WriteLine("End -- SendDataToResultatenhetService");


        }
    }
}