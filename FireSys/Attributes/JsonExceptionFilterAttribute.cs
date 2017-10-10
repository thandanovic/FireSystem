using FireSys.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSys.Attributes
{
    public class JsonExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;

                var errorMessage = string.Empty;
                if (filterContext.Exception is ClientException)
                {
                    errorMessage = filterContext.Exception.Message;
                }
                // Handle of A potentially dangerous Request.Form value that was detected from the client (xss, ajax)
                else if (filterContext.Exception is HttpRequestValidationException)
                {
                    errorMessage = "Failed to process request.";
                }
                else
                {
                    errorMessage = "There was an error processing your request. Please contact your system admin for additional info.";
                }

                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        errorMessage = errorMessage
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}