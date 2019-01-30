using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Harman.Healthcare.Common
{
    public class Constants
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public static readonly string InternalServerErrorMessage = "Some internal error has occured. Please contact System Admin";

        #region Validation Messages
        public const string ForenameValidationMessage = "Forename is not in correct format";
        public const string SurnameValidationMessage = "Surname is not in correct format";

        #endregion

        
    }

    public class SpNames
    {
        #region SpNames

        public static readonly string GetPatientInformation = "dbo.GetPatientInformation";
        public static readonly string SavePatientInformation = "dbo.SavePatientInformation";

        #endregion
    }
}
