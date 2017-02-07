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
            
       
            var persons = LoadFromOracle.FindAllActivePersons();

            //Start Person
            SavePerson.SendDataToPersonService(persons);
           
           
           



            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
         
        }

       


     








    }
}
