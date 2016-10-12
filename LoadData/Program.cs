using LoadData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadData
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Start");

            ApplicationDbContext db = new ApplicationDbContext();

            
            List<int> kstnrList = new List<int> { 165985, 400770, 338111 }; 

            var t  = (from fa in db.KSS_ANSTALLNING
                      where kstnrList.Contains( fa.KSTNR ) 
                      orderby fa.FNAMN ascending
                      select fa).ToList();


            SqlServer sqlDB = new SqlServer();

            var _sql = sqlDB.Person.FirstOrDefault();

        }
    }
}
