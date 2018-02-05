using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.OData.UriParser;

namespace Audit.Core.Filter
{

    //Validate Token against Fred Identity.
    public class Auth : AuthorizationFilterAttribute
    {
      
        public override Task OnAuthorizationAsync(HttpActionContext actionContext,
            System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization.ToString().Substring(7) != ConfigurationManager.AppSettings["FredAccessToken"]) // validate the token against fred identity - as part of implementation.
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
               
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError);

            }
            return Task.FromResult<object>(null);
        }

    }
}