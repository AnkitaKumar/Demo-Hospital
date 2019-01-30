using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harman.Healthcare.Logger
{
    public class TextLogger : ILogger
    {        
        private  log4net.ILog _log { get; set; }

        private object lockObject = new object();

        public TextLogger()
        {
            if (_log is null)
            {
                lock (lockObject)
                {
                    log4net.Config.XmlConfigurator.Configure();
                    _log = log4net.LogManager.GetLogger(typeof(TextLogger));
                    
                }
            }

        }
        public void LogError(Exception ex)
        {
            _log.Error($"{ex.Message} {System.Environment.NewLine} {ex.StackTrace}");
        }

        public void LogInformation(string message, IDictionary additionalInformation)
        {
            var additionInfoText = new StringBuilder(); ;
            foreach(var item in additionalInformation.Keys)
            {
                additionInfoText.AppendLine(additionalInformation[item]?.ToString());
            }
            _log.Info($"{message} {System.Environment.NewLine} {additionalInformation.ToString()}"); 
        }
    }
}
