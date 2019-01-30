using Harman.Healthcare.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harman.Healthcare.Contract.DAL
{
    public interface IPatientInformationDAL
    {
        Dictionary<int, string> GetPatientInformation();
         bool SavePatientInformation(string patientInformationXML);
    }
}
