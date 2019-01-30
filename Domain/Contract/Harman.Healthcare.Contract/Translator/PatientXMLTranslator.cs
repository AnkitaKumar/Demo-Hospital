using Harman.Healthcare.Entity;
using System.IO;
using System.Xml.Serialization;

namespace Harman.Healthcare.Contract.Translator
{
    public class PatientXMLTranslator
    {
        public static string ToXML(Patient patient)
        {
            var stringwriter = new StringWriter();
            var serializer = new XmlSerializer(patient.GetType());
            serializer.Serialize(stringwriter, patient);
            return stringwriter.ToString();
        }

        public static Patient FromXMLString(string xmlText)
        {
            if(string.IsNullOrEmpty(xmlText))
            {
                return null;
            }
            var stringReader = new StringReader(xmlText);
            var serializer = new XmlSerializer(typeof(Patient));
            return serializer.Deserialize(stringReader) as Patient;
        }

    }
}
