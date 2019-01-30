using Harman.Healthcare.Logger;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using Unity;

namespace Harman.Healthcare.Host
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            //Log exception details from the context
            var logger = IocConfig.Container.Resolve(typeof(ILogger)) as ILogger;
            logger?.LogError(context.Exception);
            context.ExceptionContext.Response = context.Request.CreateResponse(new HttpResponseMessage(HttpStatusCode.InternalServerError) { ReasonPhrase = "Internal Error occured. Please contact System Admin." });
        }       
    }
}