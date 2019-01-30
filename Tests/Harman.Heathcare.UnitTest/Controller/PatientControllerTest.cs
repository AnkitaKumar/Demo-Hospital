using Harman.Healthcare.Contract.BL;
using Harman.Healthcare.Entity;
using Harman.Healthcare.Host.Controllers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using NSubstitute.ExceptionExtensions;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Harman.Healthcare.Host.Filters;
using Harman.Healthcare.Logger;
using System.Net;
using Harman.Healthcare.Host.Models;
using Harman.Healthcare.Entity.Enum;

namespace Harman.Heathcare.UnitTest.Controller
{
    [TestFixture]
    public class PatientControllerTest
    {
        private PatientController _patientController;
        private IPatientInformationBL _fakepatientInformationBL;
        private ILogger _fakeLogger;
        [SetUp]
        public void Setup()
        {
            _fakepatientInformationBL = Substitute.For<IPatientInformationBL>();
            _fakeLogger = Substitute.For<ILogger>();
            _patientController = new PatientController(_fakepatientInformationBL, _fakeLogger);


            _patientController.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/uk-healthcare/api/v1/patients")
            };
            _patientController.Configuration = new HttpConfiguration();
            _patientController.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            _patientController.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "patient" } });
        }

        [Test]
        public void ShouldReturnDataWhenGetPatientInformationIsCalled()
        {
            //Arrange
            var fakePatient = GetMockPatientData();
            var fakePatientList = new List<Patient> { fakePatient };
            _fakepatientInformationBL.GetPatientInformation().Returns(x => fakePatientList);

            //Act
            var response = _patientController.GetPatientInformation();

            //Assert            
            Assert.IsTrue(response.GetType().Name == "OkNegotiatedContentResult`1");
        }

        [Test]
        public void ShouldNotReturnDataWhenGetPatientInformationIsCalled()
        {
            //Arrange
            _fakepatientInformationBL.GetPatientInformation().Returns(x => null);
            //Act
            var response = _patientController.GetPatientInformation();
            //Assert
            Assert.IsTrue(((ResponseMessageResult)response).Response.StatusCode == HttpStatusCode.NoContent);
        }

        [Test]
        public void ShouldNotSaveDataWhenSavePatientInformationIsCalled()
        {
            //Arrange
            var fakePatientViewModel = new PatientViewModel
            {
                Forename = "forename",
                Surname = "sur",
                DateOfBirth = DateTime.Now,
                Gender = ((int)Gender.Male).ToString()
            };
            _fakepatientInformationBL.SavePatientInformation(Arg.Any<Patient>()).Returns(false);
            //Act
            var response = _patientController.Post(fakePatientViewModel);

            //Assert
            Assert.IsTrue(((ResponseMessageResult)response).Response.StatusCode == HttpStatusCode.InternalServerError);
        }

        [Test]
        public void ShouldSaveDataWhenSavePatientInformationIsCalled()
        {
            //Arrange
            var fakePatientViewModel = new PatientViewModel
            {
                Forename = "forename",
                Surname = "sur",
                DateOfBirth = DateTime.Now,
                Gender = ((int)Gender.Male).ToString()
            };
            _fakepatientInformationBL.SavePatientInformation(Arg.Any<Patient>()).Returns(true);
            //Act
            var response = _patientController.Post(fakePatientViewModel);

            //Assert
            Assert.IsTrue(response.GetType().Name == "OkResult");
        }


        private Patient GetMockPatientData()
        {
            return new Patient
            {
                Forename = "Test",
                Surname = "Surname",
                Gender = Healthcare.Entity.Enum.Gender.Female,
                DateOfBirth = DateTime.Now
            };
        }

    }
}
