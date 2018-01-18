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
            await InsertAuditAsync();
        }

        static async Task InsertAuditAsync()
        {
            
            HttpResponseMessage response = await _client.PostAsJsonAsync(
                "api/audit/insert", new Request() {Data= ReturnJson() });
            response.EnsureSuccessStatusCode();
        }


        private static string ReturnJson()
        {
            //modify the code here to plain json string.
            return (File.ReadAllText(@"..\..\Files\Patient.json"));
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
