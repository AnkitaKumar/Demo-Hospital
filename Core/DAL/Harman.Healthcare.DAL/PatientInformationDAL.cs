using Harman.Healthcare.Contract.DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Harman.Healthcare.Common;
using System;

namespace Harman.Healthcare.DAL
{
    public class PatientInformationDAL:IPatientInformationDAL
    {
        public Dictionary<int, string> GetPatientInformation()
        {
            Dictionary<int, string> patientXmlList = null;

            try
            {
                using (var connection = new SqlConnection(Constants.ConnectionString))
                {
                    connection.Open();
                    var sqlCommand = connection.CreateCommand();
                    sqlCommand.CommandText = SpNames.GetPatientInformation;
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        var patientIdIndex = reader.GetOrdinal("PATIENTID");
                        var patientXmlIndex = reader.GetOrdinal("PATIENTDATA");

                        if (reader is null)
                        {
                            return null;
                        }
                        if (reader.HasRows)
                        {
                            patientXmlList = new Dictionary<int, string>();
                            while (reader.Read())
                            {
                                patientXmlList.Add(reader.GetInt32(patientIdIndex), reader.GetString(patientXmlIndex));
                            }
                        }

                    }

                    return patientXmlList;
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }                  
        }

        public bool SavePatientInformation(string patientInformationXML)
        {            
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                connection.Open();
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = SpNames.SavePatientInformation;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                var sqlParameter = sqlCommand.CreateParameter();
                sqlParameter.ParameterName = "@PATIENTXML";
                sqlParameter.Value = patientInformationXML;

                var resultRecordCount = sqlCommand.ExecuteNonQuery();

                return resultRecordCount > 0;
            }
        }
    }
}
