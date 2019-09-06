using System.Globalization;
using Common;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters
{
    public class LanguageFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var hasLanguage = context.HttpContext.Request.Headers.TryGetValue("Accept-Language", out var acceptLanguage);

            CultureInfo.CurrentUICulture = new CultureInfo(CultureHelper.GetCulture(acceptLanguage));
        }
    }
}
