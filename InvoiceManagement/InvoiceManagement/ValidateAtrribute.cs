using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace InvoiceManagement
{
    public class ValidateAtrribute : ActionFilterAttribute
    {
       
        public ValidateAtrribute()
        {
            
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
           
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           
            var contentType = context.HttpContext.Request.ContentType;

            if (context.HttpContext.Request.Method == "POST" && (contentType == null || !contentType.Contains("application/json")))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 415,
                    Content = "Unsupported Media Type. Please use application/json content type."
                };


            }
            if (context.HttpContext.Request.Method == "POST" )
            {
                var requestBody = context.HttpContext.Request.Body;
                if (requestBody == null)
                {
                    context.Result = new ContentResult
                    {
                        StatusCode = 400,
                        Content = "Bad Request : Request provided is not supported."
                    };
                }
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }

    }
}
