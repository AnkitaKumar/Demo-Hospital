using Harman.Healthcare.Common;
using Harman.Healthcare.Contract.BL;
using Harman.Healthcare.Entity;
using Harman.Healthcare.Entity.Enum;
using Harman.Healthcare.Host.Filters;
using Harman.Healthcare.Host.Models;
using Harman.Healthcare.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace Harman.Healthcare.Host.Controllers
{

    [RoutePrefix("uk-healthcare/api/v1")]   
    public class PatientController : ApiController
    {
        private IPatientInformationBL _patientInformationBL;
        private ILogger _logger;
        #region Constructor
        public PatientController(IPatientInformationBL patientInformationBL,ILogger logger)
        {
            _patientInformationBL = patientInformationBL;
            _logger = logger;
        }
        #endregion
        [Route("patients")]        
        public IHttpActionResult GetPatientInformation()
        {
            try
            {
                var response = _patientInformationBL.GetPatientInformation();

                if (response is null)
                {
                    return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.NoContent));
                }

                var patientList = response.Select(x => new PatientViewModel
                {
                    Forename = x.Forename,
                    DateOfBirth = x.DateOfBirth,
                    Gender = x.Gender.ToString(),
                    PatientId = x.PatientId,
                    Surname = x.Surname,
                    TelephoneNumbers = x.TelephoneNumbers?.Select(y => new TelephoneNumberViewModel
                    {
                        Number = y.Number,
                        Type = y.Type
                    }).ToList()
                }).ToList();
                return Ok(patientList);
            }
            catch(Exception ex)
            {
                //Log exception
                _logger.LogError(ex);
                return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = Constants.InternalServerErrorMessage });
            }
            
        }

        //[Route("patients/{id}")]
        //public IHttpActionResult GetPatientInformation(int id)
        //{
        //    return NotImplemented();
        //}

        [Route("patient")]        
        public IHttpActionResult Post(PatientViewModel patientSaveRequest)
        {
            try
            {
                ReadFormDataFromRequest(patientSaveRequest);

                if (!ModelState.IsValid)
                {
                        return BadRequest();
                }
                var patient = new Patient
                {
                    Gender = (Gender)Convert.ToInt16(patientSaveRequest.Gender),
                    Forename = patientSaveRequest.Forename,
                    Surname = patientSaveRequest.Surname,
                    DateOfBirth = patientSaveRequest.DateOfBirth,
                    TelephoneNumbers = patientSaveRequest.TelephoneNumbers?.Select(x => new TelephoneNumber { Type = x.Type, Number = x.Number }).ToList()
                };
                if (_patientInformationBL.SavePatientInformation(patient))
                {
                    return Ok();
                }
                return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = Constants.InternalServerErrorMessage });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = Constants.InternalServerErrorMessage });
            }
        }

        private void  ReadFormDataFromRequest(PatientViewModel patientSaveRequest)
        {
            patientSaveRequest.DateOfBirth = Convert.ToDateTime(HttpContext.Current?.Request?.Form["DateOfBirth"]);
            patientSaveRequest.TelephoneNumbers = new List<TelephoneNumberViewModel>
            {
                { new TelephoneNumberViewModel {Number = HttpContext.Current?.Request?.Form["HomeNumber"], Type = TelephoneNumberType.HomeNumber} },
                { new TelephoneNumberViewModel {Number = HttpContext.Current?.Request?.Form["WorkNumber"], Type = TelephoneNumberType.WorkNumber} },
                { new TelephoneNumberViewModel {Number = HttpContext.Current?.Request?.Form["MobileNumber"], Type = TelephoneNumberType.MobileNumber} }
            };
        }
    }
}
