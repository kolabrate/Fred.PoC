using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Audit.Core.Console.Test.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//partition Key - /storeid

namespace Audit.Core.Console.Test
{
    class Program
    {
        static HttpClient _client;
        static void Main(string[] args)
        {

            try
            {

                _client = GethttpClient();
                RunAsync().GetAwaiter().GetResult();

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }

        }

        
        private static async Task RunAsync()
        {
            //record three patient records
            for (int i = 0; i < 5; i++)
            {
                await LogPatientAsync(i);
            }

          

        }


        static async Task LogPatientAsync(int patientId)
        {
            
            HttpResponseMessage response = await _client.PostAsJsonAsync(
                "api/patients/log", new Request() {Data= ReturnJson(patientId) });
            response.EnsureSuccessStatusCode();
        }


        private static string ReturnJson(int patientId)
        {
            switch (patientId)
            {
                case 1:
                    return (File.ReadAllText(@"..\..\Files\Patient1.json"));
                case 2:
                    return (File.ReadAllText(@"..\..\Files\Patient2.json"));
                case 3:
                    return (File.ReadAllText(@"..\..\Files\Patient3.json"));
                default:
                        return (File.ReadAllText(@"..\..\Files\Patient1.json"));
            }

            //modify the code here to plain json string.
            
        }

        private static HttpClient GethttpClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ConfigurationManager.AppSettings["AuditApi"].ToString());
            _client.DefaultRequestHeaders.Add("Authorization","Bearer " + ConfigurationManager.AppSettings["FredToken"].ToString());
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return _client;

        }


    }
}
