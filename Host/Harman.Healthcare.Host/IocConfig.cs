using Harman.Healthcare.BL;
using Harman.Healthcare.Contract.BL;
using Harman.Healthcare.Contract.DAL;
using Harman.Healthcare.DAL;
using Harman.Healthcare.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace Harman.Healthcare.Host
{
    public class IocConfig
    {  
        public static IUnityContainer Container { get; set; }
        public static volatile object _lockObject = new object();

        static IocConfig()
        {
            if(Container is null)
            {
                lock (_lockObject)
                {
                    Container = new UnityContainer();
                }
            }
            
        }

          public static void InitializeIOCContainer()
        {
            //Register Types here
            Container.RegisterType<IPatientInformationBL, PatientInformationBL>();
            Container.RegisterType<IPatientInformationDAL, PatientInformationDAL>();
            Container.RegisterType<ILogger, TextLogger>();
        }

    }
}