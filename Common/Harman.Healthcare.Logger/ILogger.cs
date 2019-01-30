using System;
using System.Collections;

namespace Harman.Healthcare.Logger
{
    public interface ILogger
    {
        void LogError(Exception ex);

        void LogInformation(string message, IDictionary additionalInformation);
    }
}
