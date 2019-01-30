using Harman.Healthcare.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;
using static Harman.Healthcare.Common.Constants;
namespace Harman.Healthcare.Entity
{
    [Serializable]
    public class Patient
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
        public Gender Gender { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "TelephoneNumber")]
        public List<TelephoneNumber> TelephoneNumbers { get; set; }

        [XmlIgnore]
        public Dictionary<string, string> TelephoneNumberDictionary
        {
            get { return TelephoneNumbers.ToDictionary(x => x.Type.ToString(), x => x.Number); }
            set
            {
                TelephoneNumbers = value.Select(x => new TelephoneNumber()
                { Type = (TelephoneNumberType)System.Enum.Parse(typeof(TelephoneNumberType), x.Key), Number = x.Value }).ToList();
            }
        }
    }


    public class TelephoneNumber
    {
        public TelephoneNumberType Type { get; set; }
        public string Number { get; set; }
    }
}
