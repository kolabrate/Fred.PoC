using System;

namespace Audit.Core.Models
{
    public class Audit
    {


        //This info is retrieved from Identity Token

        public string SiteId; // this is a partition key - need to investigate - look at the guiding principles.
        public string AadId { get; set; } // Azure Active Directory Id
        public string UserDisplayName { get; set; } // Display name of the token
        public string UserPrincipalName { get; set; } // Principal name of the token
        public string UserEmailName { get; set; } // User Email


        //Data dump from the applications
        public dynamic Data { get; set; }


        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }

    }
}