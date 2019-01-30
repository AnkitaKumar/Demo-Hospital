using Harman.Healthcare.Contract.BL;
using Harman.Healthcare.Contract.DAL;
using Harman.Healthcare.Contract.Translator;
using Harman.Healthcare.Entity;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Harman.Healthcare.BL
{
    public class PatientInformationBL : IPatientInformationBL
    {
        IPatientInformationDAL _patientInformationDAL;

        #region Constructor
        public PatientInformationBL(IPatientInformationDAL patientInformationDAL)
        {
            _patientInformationDAL = patientInformationDAL;
        }
        #endregion

        #region Public Methods
        public List<Patient> GetPatientInformation()
        {
            var patientXMLList = _patientInformationDAL.GetPatientInformation();

            var patientList = new List<Patient>();
            if(patientXMLList != null)
            {
                foreach (var patientXMLItem in patientXMLList.Keys)
                {
                    var patient = PatientXMLTranslator.FromXMLString(patientXMLList[patientXMLItem]);
                    if (patient != null)
                    {
                        patientList.Add(patient);
                    }
                }
            }            
            
            return patientList;
        }

        public bool SavePatientInformation(Patient patient)
        {
            var patientXML = PatientXMLTranslator.ToXML(patient);
            return _patientInformationDAL.SavePatientInformation(patientXML);            
        }
        #endregion

       


    }
}
