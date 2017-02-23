using LoadData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using LoadData.DTOModel;

namespace LoadData
{
    class Program
    {
        public const string DateFormat = "yyyy-MM-dd";
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm";
        static void Main(string[] args)
        {


            Console.WriteLine("Start");
            var kstnrList = new List<long>();
            //Start Load is based on all active persons/user or how many you chose to load.
            var persons = LoadFromOracle.FindAllActivePersons();
            Console.WriteLine("Start -- Personer " + persons.Count);

            var numbers = Enumerable.Range(1, 9999).Select(i => (long)i).ToList();

            kstnrList.AddRange(persons.Select(org => org.KSTNR).Select(i => (long)i));
            Console.WriteLine("Start -- kstnrList " + kstnrList.Count);
            kstnrList = kstnrList.GroupBy(x => x).Select(g => g .First()).ToList();
            Console.WriteLine("After distinct -- kstnrList " + kstnrList.Count);

            persons = persons.GroupBy(x => x.PERSNR).Select(g => g.First()).ToList();
            Console.WriteLine("After distinct -- Personer " + persons.Count);
            //Tmp solution to avoid oracle limitaions max 999 items IN conditions..
            var persNrSubLists = Split<long>(persons);

            //1.Start save all persons
            SavePerson.SendDataToPersonService(persons).ContinueWith(task =>
            {
                task.Wait();
                foreach (var sublist in persNrSubLists)
                {

                    var subAdresssListHome = LoadFromOracle.FindHomeAdressPerson(sublist) as List<dynamic>;
                    var subAdresssListWork = LoadFromOracle.FindWorkAdressPerson(sublist) as List<dynamic>;

                    //if (subAdresssListWork != null)
                    //    kstnrList.AddRange(subAdresssListWork.Select(org => org.ID).Cast<long>());

                    var adressInputHomeDTOList = SaveAdress.CreatListOHomeAdressPersonInputDTO(subAdresssListHome);
                    var adressInputWorkDTOList = SaveAdress.CreatListOfWorkAdressPersonInputDTO(subAdresssListWork);
                    SaveAdress.SendToOdlPerson(adressInputHomeDTOList);
                    SaveAdress.SendToOdlPerson(adressInputWorkDTOList);

                    //2.Save all org related to the persons
                    SaveResultatenhet.SendDataToResultatenhetServiceByKstnr(kstnrList).ContinueWith(task2 =>
                    {
                        task2.Wait();
                        foreach (var kstnr in kstnrList)
                        {
                            var kstNrAdressList = LoadFromOracle.FindResultatEnhetAdress(kstnr);
                            var adressInputOrgDTOList = SaveAdress.CreatListOfAdressResultatEnheterInputDTO(kstNrAdressList);

                            SaveAdress.SendToOdlOrg(adressInputOrgDTOList);
                        }


                    });

                    //TODO - mah
                    //SaveAvtal.AvtalsService(sublist, kstnrList);



                }

                //var kstnrNrSubLists = Split<long>(kstnrList);
                //foreach (var subkstnrList in kstnrNrSubLists)
                //{


                //}

            });










            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        private static List<List<long>> Split<T>(IEnumerable<Anstallning> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x.PERSNR })
                .GroupBy(x => x.Index / 998)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        private static List<List<long>> Split<T>(IEnumerable<long> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 998)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }













    }
}
