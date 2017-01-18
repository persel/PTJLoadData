using LoadData.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            
            var db = new ApplicationDbContext();

            var kstnrList = new List<int> { 165985, 400770, 338111 };
            //Only take person with an org.. to start
            var persons = (from fa in db.KSS_ANSTALLNING
                           where fa.AKTIV == "J" //&& fa.ANSVARIG == "J"//kstnrList.Contains(fa.KSTNR) &&
                           orderby fa.KSTNR descending 
                           select fa).Take(5000).ToList();
          
            persons = persons.GroupBy(p => p.PERSNR).Select(grp => grp.First()).ToList();


            //Start Person
            LoadPerson.SendDataToPersonService(persons);

           // Thread.Sleep(3000);
            //start org the when that is finish load all the Avtal..
            


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
         
        }


     








    }
}
