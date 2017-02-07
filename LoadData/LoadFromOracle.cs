using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Dapper;
using LoadData.Models;
using Oracle.ManagedDataAccess.Client;

namespace LoadData
{
    static class LoadFromOracle
    {
        public static IList<Anstallning> FindAllActivePersons()
        {
            
            using (IDbConnection db2 = new OracleConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                return db2.Query<Anstallning>(@"Select * 
                        From KSS_ANSTALLNING, KSS_KOSTNADSSTALLE
                        WHERE AKTIV =:AKTIV
                        and KSS_ANSTALLNING.PERSNR = KSS_KOSTNADSSTALLE.PERSNR
                        and ROWNUM <=:Limit ", new { AKTIV = "J", Limit = 4000 }).ToList();

            }
        }


        public static IEnumerable<dynamic> FindAdress(List<long> persNrList)
        {
            //var t = string.Join(",", persNrList);
            using (IDbConnection db2 = new OracleConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                return db2.Query(@"Select * 
                        From MAVY
                        WHERE PERSNR IN :PersNrList ", new { PersNrList = persNrList , Limit = 10 }).ToList();

            }
        }

        public static IEnumerable<dynamic> FindResultatenheter(List<long> persNrList)
        {
          
            using (IDbConnection db2 = new OracleConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                return db2.Query(@"Select KSTNR, KSTTYP, ENAMN, FNAMN, UPPL_DATUM
                                From KSS_KOSTNADSSTALLE 
                                WHERE PERSNR IN :PersNrList ", new { PersNrList = persNrList }).ToList();

            }
        }

    }
}