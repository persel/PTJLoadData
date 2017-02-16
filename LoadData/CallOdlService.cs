using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoadData.DTOModel;
using LoadData.Models;

namespace LoadData
{
    public static class CallOdlService
    {
       
        public static async Task Send<T>(IList<T> listOfObjects, string url)
        {
            var host = ConfigurationManager.AppSettings["ServiceHost"];
            using (var client = new HttpClient())
            {
               
                    //var personInputDtos = list as IList<dynamic> ?? list.ToList();
                    foreach (var p in listOfObjects)
                    {
                        var jsonInStringPerson = Newtonsoft.Json.JsonConvert.SerializeObject(p);

                        var response = await client.PostAsync(host + url, new StringContent(jsonInStringPerson, Encoding.UTF8, "application/json"));

                        if (!response.IsSuccessStatusCode )
                        {
                            Console.WriteLine("Fel " + response.StatusCode + " " + response.ReasonPhrase);
                            //Console.WriteLine("Fel " + jsonInStringPerson);      
                        }
                    }
            }
        }

    }

}