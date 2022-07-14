using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Origin.YMC.Web.Client.Filter
{
    public class JsonCustomResult : JsonResult
    {
        private readonly HttpStatusCode _statusCode;

        public JsonCustomResult(HttpStatusCode statusCode)
        {
            _statusCode = statusCode;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = (int)_statusCode;
            context.HttpContext.Response.TrySkipIisCustomErrors = true;
            base.ExecuteResult(context);
        }
    }
}