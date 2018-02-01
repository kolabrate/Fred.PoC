using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Audit.Core.Models
{
    public class Patient : Audit
    {
        public string patientid { get; set; }
        public string patientName { get; set; }
    }
}