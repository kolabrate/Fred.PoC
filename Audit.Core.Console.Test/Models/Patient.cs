using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audit.Core.Console.Test.Models
{
    public class PatientNote : Patient
    {
        public Patient Patient { get; set; }
        public string Message { get; set; }
        public bool IsImportant { get; set; }
        public string LastModifierName { get; set; }
        public string LastModifierEmail { get; set; }
        public DateTime? LastModificationDateTime { get; set; }
        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; }
        public DateTime? CreationDateTime { get; set; }

    }

    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
