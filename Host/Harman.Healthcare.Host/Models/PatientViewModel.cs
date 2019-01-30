using Harman.Healthcare.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Harman.Healthcare.Common.Constants;
namespace Harman.Healthcare.Host.Models
{
    public class PatientViewModel
    {
        public int PatientId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ForenameValidationMessage)]
        public string Forename { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = SurnameValidationMessage)]
        public string Surname { get; set; }


        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        public List<TelephoneNumberViewModel> TelephoneNumbers { get; set; }
    }


    public class TelephoneNumberViewModel
    {
        public TelephoneNumberType Type { get; set; }
        public string Number { get; set; }
    }
}