using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using LoadData.DTOModel;
using LoadData.Models;

namespace LoadData
{
    static class LoadAvtal
    {
        private const string DateFormat = "yyyy-MM-dd";
        private const string DateTimeFormat = "yyyy-MM-dd HH:mm";

        public static async void SendDataToAvtalsService(List<string> persNrList)
        {
            var db = new ApplicationDbContext();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            int antalAvtal = 0;
            using (var client = new HttpClient())
            {
                Console.WriteLine("Start -- SendDataToAvtalsService");
                foreach (var persNr in persNrList)
                {
                    var myPersNr = long.Parse(persNr);
                    var minaAvtal = (from fa in db.KSS_ANSTALLNING
                               where fa.PERSNR == myPersNr
                                     orderby fa.KSTNR ascending
                               select fa).ToList();

                    var org = (from k in db.KSS_KOSTNADSSTALLE
                               where k.PERSNR.Value == myPersNr
                               orderby k.KSTNR ascending
                               select new {k.KSTNR , k.KSTTYP} ).ToList();

                    if (org.Count > 0)
                    {
                        List<OrganisationAvtalInputDTO> orgAvtal = new List<OrganisationAvtalInputDTO>();
                        foreach (var o in org)
                        {
                            var _org = new OrganisationAvtalInputDTO
                            {
                                KostnadsstalleNr = o.KSTNR,
                                Huvudkostnadsstalle = (o.KSTTYP == "H") ? true : false,
                                ProcentuellFordelning = 100
                            };
                            orgAvtal.Add(_org);
                        }
                        antalAvtal += minaAvtal.Count;
                        foreach (var avtal in minaAvtal)
                        {
                            var avtalToSave = new AvtalInputDTO
                            {
                                AnstalldPersonnummer = avtal.PERSNR.ToString(),
                                KonsultPersonnummer = "",
                                Avtalskod = avtal.AVTALSKOD,
                                Avtalstext = avtal.AVTALSTEXT,
                                ArbetstidVecka = (int?) avtal.ARBTIDV,
                                Befkod = avtal.BEFKOD,
                                BefText = avtal.BEFTEXT,
                                Aktiv = (avtal.AKTIV == "J") ? true : false,
                                Ansvarig = (avtal.ANSVARIG == "J") ? true : false,
                                Chef = (avtal.CHEF == "J") ? true : false,
                                TjledigFran = avtal.TJLEDIGFROM?.ToString(DateFormat),
                                TjledigTom = avtal.TJLEDIGTOM?.ToString(DateFormat),
                                Fproc = avtal.FPROC,
                                DeltidFranvaro = avtal.DELTID_FRANV,
                                FranvaroProcent = avtal.FRANVPROC,
                                SjukP = avtal.SJUKP,
                                GrundArbtidVecka = avtal.GRUND_ARBTIDV,
                                AvtalsTyp = 0,
                                Lon = avtal.LON,
                                LonDatum = avtal.LONDAT?.ToString(DateFormat),
                                LoneTyp = avtal.LONETYP,
                                LoneTillagg = avtal.LONETILLAGG,
                                TimLon = (int?) avtal.TIMLON,
                                Anstallningsdatum = avtal.ANSTDAT?.ToString(DateFormat),
                                Avgangsdatum = avtal.AVGDAT?.ToString(DateFormat),
                                Personnummer = avtal.PERSNR.ToString(),
                                systemId = $"DB{avtal.PERSNR.ToString()}-{avtal.KSTNR}-{avtal.BEFKOD}",
                                uppdateradDatum = DateTime.Now.ToString(DateTimeFormat),
                                uppdateradAv = "PSE",
                                skapadDatum = avtal.ANSTDAT?.ToString(DateTimeFormat),
                                skapadAv = "PSE",
                                Kostnadsstallen = orgAvtal
                            };


                            var jsonInStringAvtal = Newtonsoft.Json.JsonConvert.SerializeObject(avtalToSave);

                            var response = await client.PostAsync("http://localhost:57107/api/Person/avtal",
                                new StringContent(jsonInStringAvtal, Encoding.UTF8, "application/json"));

                        }
                    }
                }
            }

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine("SendDataToAvtalsService");
            Console.WriteLine("Time to save " + antalAvtal + " Avtal " + elapsedTime);
            Console.WriteLine("End -- SendDataToAvtalsService");

            

        }

    }
}