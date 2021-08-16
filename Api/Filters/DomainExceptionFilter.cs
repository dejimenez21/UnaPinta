using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnaPinta.Core.Errors;

namespace UnaPinta.Api.Filters
{
    public class DomainExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is BaseDomainException)
            {
                var DomainException = (BaseDomainException)context.Exception;
                context.HttpContext.Response.StatusCode = DomainException.StatusCode;
                context.Result = new JsonResult(DomainException.ToResponseObject());
            }
        }
    }
}
