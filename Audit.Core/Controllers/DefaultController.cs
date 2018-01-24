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
using Newtonsoft.Json.Linq;


namespace Audit.Core.Controllers
{
    [Auth]
    [RoutePrefix("api/audit")]
    public class DefaultController : ApiController
    {
        private readonly Channel _channel;
        public DefaultController()
        {
           // _channel = new Channel(new ServiceBus()); // DI
        }

        [Route("insert")]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]JObject data)
        {
            try
            {
                
                await _channel.WriteAsync(PopulateAuditData(data.ToString()));           
                return Ok();

            }
            catch (Exception e)
            {
                return InternalServerError(new Exception(e.Message));
            }
        }


        [Route("Query")]
        [HttpGet]
        public IHttpActionResult GetAuditData()
        {

            throw new NotImplementedException();
        }


        #region private methods

        private  Models.Audit PopulateAuditData(string data)
        {
            var jwtEncodedString = HttpContext.Current.Request.Headers["Authorization"].Substring(7);
            var token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            return new Models.Audit()
            {
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



        #endregion

    }
}
