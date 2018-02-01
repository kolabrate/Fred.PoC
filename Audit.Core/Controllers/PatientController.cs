using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Audit.Core.Filter;
using Audit.Core.Models;
using Microsoft.Azure.CosmosDB.Table;
using Microsoft.Azure.Storage;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Audit.Core.Channels;
using Audit.Core.Channels.MessageBus;
using Newtonsoft.Json.Linq;


namespace Audit.Core.Controllers
{
    [Auth]
    [RoutePrefix("api/patients")]
    public class PatientController : ApiController
    {
        private readonly Channel _channel;
        public PatientController()
        {
            _channel = new Channel(new EventHub(ConfigurationManager.AppSettings["EventHub"])); // DI - once finalised the messaging framework , this can be removed.
        }

        [Route("log")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]JObject data)
        {
            try
            {
                
                //PopulatePatientEvent(data.ToString());
               await _channel.WriteAsync(PopulatePatientEvent(data.ToString()));           
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(new Exception(e.Message));
            }
        }

        #region private methods

        private  Patient PopulatePatientEvent(string data)
        {
            var jwtEncodedString = HttpContext.Current.Request.Headers["Authorization"].Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            return new Patient
            {
                rowkey = Guid.NewGuid().ToString(),
               patientid = RetrievePatientId(data),
                patientName = "Anand Maran",
               //patientid = "a02ea8bf-7704-4f2b-893e-9d0b37fa936d",
                AadId = token.Claims.First(c => c.Type == "oid").Value,
                UserDisplayName = token.Claims.First(c => c.Type == "displayname").Value,
                UserPrincipalName = token.Claims.First(c => c.Type == "principalname").Value,
                UserEmailName = token.Claims.First(c => c.Type == "useremail").Value,
                SiteId = token.Claims.First(c => c.Type == "masterSiteId").Value,
                Data = data,
                ModifiedDateTime = DateTime.UtcNow,
                CreatedDateTime = DateTime.UtcNow

            };

        }

        private string RetrievePatientId(string data)
        {
            return Guid.NewGuid().ToString();
        }

        #endregion

    }
}
