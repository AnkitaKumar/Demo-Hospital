using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Harman.Healthcare.Host.Models
{
    public class PatientSaveRequest
    {
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender     { get; set; }
        public string HomeNumber { get; set; }
        public string WorkNumber { get; set; }
        public string MobileNumber { get; set; }
    }
}