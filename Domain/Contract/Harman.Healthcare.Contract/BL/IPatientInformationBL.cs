using Harman.Healthcare.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harman.Healthcare.Contract.BL
{
    public interface IPatientInformationBL
    {
        List<Patient> GetPatientInformation();        
        bool SavePatientInformation(Patient patient);
    }
}
