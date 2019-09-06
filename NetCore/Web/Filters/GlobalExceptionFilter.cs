using Core.Common.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        ILogger<GlobalExceptionFilter> logger = null;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> exceptionLogger)
        {
            logger = exceptionLogger;
        }

        public void OnException(ExceptionContext context)
        {
            string content = "";

            var result = new ContentResult();

            if (context.Exception is ValidationError)
            {
                var exception = context.Exception as ValidationError;

                content = JsonConvert
                    .SerializeObject(new Response<object>(null, exception.Errors), new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                logger.LogError(0, context.Exception.GetBaseException(), "Exception occurred.");

                string errorMessage = Startup.HostingEnvironment.IsDevelopment() ? context.Exception.Message : string.Empty;

                if (context.Exception is DbUpdateConcurrencyException)
                {
                    errorMessage = "Someone already changed this record.";
                }

                content = JsonConvert
                    .SerializeObject(new Response<object>(null, new ApiError((int)HttpStatusCode.InternalServerError, "Something went wrong. " + errorMessage)), new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            result.Content = content;
            result.ContentType = "application/json";

            context.Result = result;
        }
    }
}
