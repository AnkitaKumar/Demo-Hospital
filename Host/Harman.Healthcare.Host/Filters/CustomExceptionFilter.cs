using Harman.Healthcare.Logger;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Unity;

namespace Harman.Healthcare.Host.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            //Log exceptions using Logger
            var logger = IocConfig.Container.Resolve(typeof(ILogger)) as ILogger;
            logger?.LogError(context.Exception);
            context.Response = context.Request.CreateResponse(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = "Internal Error occured. Please contact System Admin." });

        }
    }
}


