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
    [RoutePrefix("api/audit")]
    public class AuditController : ApiController
    {
        private readonly Channel _channel;
        public AuditController()
        {
            _channel = new Channel(new EventHub(ConfigurationManager.AppSettings["EventHub"])); // DI - once finalised the messaging framework , this can be removed.
        }

        [Route("log")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]JObject data)
        {
            try
            {
               await _channel.WriteAsync(PopulateAuditEvent(data.ToString()));           
               return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(new Exception(e.Message));
            }
        }

        #region private methods

        private  Models.Audit PopulateAuditEvent(string data)
        {
            var jwtEncodedString = HttpContext.Current.Request.Headers["Authorization"].Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            return new Models.Audit
            {
                id = Guid.NewGuid().ToString(),
                storeid = token.Claims.First(c => c.Type == "masterSiteId").Value,
                userAAID = token.Claims.First(c => c.Type == "oid").Value,
                UserDisplayName = token.Claims.First(c => c.Type == "displayname").Value,
                UserLogin = token.Claims.First(c => c.Type == "useremail").Value,

                #region - hardcoded - refactor required
                Source = "Fred Identity", //this needs to be validated
                CompanyId = "SIGM_001001",//to be modified
                CompanyName = "SIGMA", //to be modified
                Environment = "Dev",//to be modified
                SessionId = "Sess_00110011",//to be modified
                SequenceId = "1",//to be modified
                #endregion

                Data = data,
                ModifiedDateTime = DateTime.UtcNow,
                CreatedDateTime = DateTime.UtcNow

            };

        }
        #endregion

    }
}
