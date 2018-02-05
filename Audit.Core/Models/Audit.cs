using System;

namespace Audit.Core.Models
{
    public class Audit
    {
        public string id { get; set; }

        #region - these properties are retrieved from FI token 
        public string storeid; // this is a partition key - need to investigate - look at the guiding principles.
        public string userAAID { get; set; } // Azure Active Directory Id
        public string UserDisplayName { get; set; } // Display name of the token
        public string UserLogin { get; set; } // User Email
        public string Source { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName  { get; set; }
        #endregion

        public string Environment { get; set; }
        public string SessionId { get; set; }
        public string SequenceId { get; set; }

        #region - data dump from application
        public dynamic Data { get; set; }
        #endregion

        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }

    }
}