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
            
            for (int i = 0; i < 10; i++)
            { 
                await LogAuditAsync();
            }

          

        }

        #region - Audit related private functions

        static async Task LogAuditAsync()
        {

            HttpResponseMessage response = await _client.PostAsJsonAsync(
                "api/audit/log", new Request() { Data = ReturnAuditJson() });
            response.EnsureSuccessStatusCode();
        }

        private static string ReturnAuditJson()
        {

            return (File.ReadAllText(@"..\..\Files\Audit.json"));
        }

        #endregion




        private static HttpClient GethttpClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(ConfigurationManager.AppSettings["AuditApi"].ToString());
            _client.DefaultRequestHeaders.Add("Authorization","Bearer " + ConfigurationManager.AppSettings["FredAccessToken"].ToString());
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return _client;

        }


    }
}
