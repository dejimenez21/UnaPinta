using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnaPinta.Core.Contracts;
using UnaPinta.Dto.Models.Errors;

namespace UnaPinta.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger, IWebHostEnvironment env) 
        { 
            app.UseExceptionHandler(appError => 
            { 
                appError.Run(async context => 
                { 
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                    context.Response.ContentType = "application/json"; 
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>(); 
                    if (contextFeature != null) 
                    { 
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        if(env.IsDevelopment())
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message
                            }.ToString()); 
                        else
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Something went wrong. Please contact system admisitrator.",
                                spMessage = "Algo ha salido mal. Porfavor contacte al administrador del sistema."
                            }.ToString());
                    } 
                }); 
            }); 
        }
    }
}
