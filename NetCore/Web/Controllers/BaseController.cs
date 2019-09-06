using Core.Common.Model;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class BaseController : Controller
    {
        protected DatabaseContext ctx;

        public BaseController(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public Response<T> OkResponse<T>(T data)
        {
            return new Response<T>(data);
        }
    }
}