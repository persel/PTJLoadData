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
                        From KSS_ANSTALLNING
                        WHERE AKTIV =:AKTIV 
                        and ROWNUM <=:Limit ", new { AKTIV = "J", Limit = 4 }).ToList();

            }
        }

        public static IEnumerable<dynamic> FindHomeAdressPerson(List<long> persNrList)
        {
            //var t = string.Join(",", persNrList);
            using (IDbConnection db2 = new OracleConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                return db2.Query(@"Select * 
                        From kss_adress, kss_telefon , kss_notes_delar
                        WHERE kss_adress.ID = kss_telefon.ID 
                        and kss_telefon.ID = kss_notes_delar.ID
                        and kss_adress.ID IN :PersNrList 
                        and kss_adress.adrtyp = :Typ
                        and kss_telefon.teltyp = :TelTyp
                        and kss_adress.GADR is not null
                        and kss_adress.GADR != ' '", new { PersNrList = persNrList , Typ = "H", TelTyp = 40}).ToList();

            }
        }

        public static IEnumerable<dynamic> FindWorkAdressPerson(List<long> persNrList)
        {
            //var t = string.Join(",", persNrList);
            using (IDbConnection db2 = new OracleConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                return db2.Query(@"Select * 
                        From kss_adress a,kss_anstallning b
                        WHERE a.ID = b.kstnr
                        and a.GADR is not null
                        and a.GADR != ' '
                        and b.persnr IN :PersNrList", new { PersNrList = persNrList, AdrTypWork = "U", AdrTypVisit = "U" }).ToList();

            }
        }

        public static IEnumerable<dynamic> FindResultatEnhetAdress(long resNr)
        {
            //var t = string.Join(",", persNrList);
            using (IDbConnection db2 = new OracleConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                return db2.Query(@"Select * 
                        From kss_adress a,kss_kostnadsstalle b
                        WHERE a.ID = b.kstnr
                        and a.adrtyp = 'U' 
                        and b.Kstnr =:ResNR", new { ResNr = resNr}).ToList();

            }
        }

        public static IEnumerable<dynamic> FindWorkPhone(long resNr)
        {
            //var t = string.Join(",", persNrList);
            using (IDbConnection db2 = new OracleConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                return db2.Query(@"Select * 
                        From kss_telefon
                        WHERE ID =:ResNr 
                        and TELTYP IN(10,30)", new { ResNr = resNr }).ToList();

            }
        }

        public static IEnumerable<dynamic> FindResultatenheter(List<long> persNrList)
        {
          
            using (IDbConnection db2 = new OracleConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                return db2.Query(@"Select DISTINCT(KSTNR), PERSNR, KSTTYP, ENAMN, FNAMN, UPPL_DATUM
                                From KSS_KOSTNADSSTALLE
                                WHERE FNAMN is not null 
                                and FNAMN != ' ' ", new { PersNrList = persNrList }).ToList();

            }
        }

        public static IEnumerable<dynamic> FindResultatenheterById(List<long> kstNrList)
        {

            using (IDbConnection db2 = new OracleConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {

                return db2.Query(@"Select DISTINCT(KSTNR), PERSNR, KSTTYP, ENAMN, FNAMN, UPPL_DATUM
                                From KSS_KOSTNADSSTALLE
                                WHERE KSTNR IN :KstNrList", new { KstNrList = kstNrList }).ToList();

            }
        }

    }
}