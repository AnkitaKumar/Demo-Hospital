using Harman.Healthcare.BL;
using Harman.Healthcare.Contract.BL;
using Harman.Healthcare.Contract.DAL;
using Harman.Healthcare.Entity;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Harman.Heathcare.UnitTest.BL
{
    [TestFixture]
    public class PatientInformationBLTest
    {
        private IPatientInformationDAL _fakePatientInformationDAL;
        private IPatientInformationBL _patientInformationBL;

        [SetUp]
        public void SetUp()
        {
            _fakePatientInformationDAL = Substitute.For<IPatientInformationDAL>();
            _patientInformationBL = new PatientInformationBL(_fakePatientInformationDAL);
        }

       [Test]        
        public void ShouldNotReturnPatientDataWhenCalled()
        {
            //Arrange
            _fakePatientInformationDAL.GetPatientInformation().Returns(x => null);
            //Act
            var patientData = _patientInformationBL.GetPatientInformation();
            //Assert           
            Assert.IsTrue(patientData.Count == 0);
        }

     [Test]
        public void ShouldReturnPatientDataWhenCalled()
        {
           //Arrange            
            string patientXml = GetSamplePatientXml();

            var fakePatientXMLList = new Dictionary<int, string> { { 1, patientXml } };
            _fakePatientInformationDAL.GetPatientInformation().Returns(x => fakePatientXMLList);
            //Act
            var patientData = _patientInformationBL.GetPatientInformation();
            //Assert           
            Assert.IsTrue(patientData.Count == 1);
        }


        [Test]
        public void ShouldNotReturnPatientDataWhenCalledWithEmptyPatientXml()
        {
            //Arrange
            string patientXml = GetSamplePatientXml();

            var fakePatientXMLList = new Dictionary<int, string> { { 1, null } };
            _fakePatientInformationDAL.GetPatientInformation().Returns(x => fakePatientXMLList);
            //Act
            var patientData = _patientInformationBL.GetPatientInformation();
            //Assert           
            Assert.IsTrue(patientData.Count == 0);
        }

        [Test]
        public void ShouldSavePatientDataWhenCalled()
        {
            //Arrange            
            var fakePatientData = GetPatientData();
            
            _fakePatientInformationDAL.SavePatientInformation(Arg.Any<string>()).ReturnsForAnyArgs(x => true);
            //Act
            var isSaved = _patientInformationBL.SavePatientInformation(fakePatientData);
            //Assert           
            Assert.IsTrue(isSaved);
        }

        private string GetSamplePatientXml()
        {
            XmlDocument doc = new XmlDocument();
            var path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug","") + "TestFiles\\SamplePatientXml.txt";
            doc.Load(path);
           return doc.InnerXml;
        }

        private Patient GetPatientData()
        {
            return new Patient
            {
                Forename = "Ankita",
                Surname = "Tiwary",
                Gender = Healthcare.Entity.Enum.Gender.Female
            };
        }
    }
}
