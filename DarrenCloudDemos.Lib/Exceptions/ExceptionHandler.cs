using DarrenCloudDemos.Lib.Trace;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DarrenCloudDemos.Lib.Exceptions
{
    /// <summary>
    /// 全局API异常处理
    /// </summary>
    public static class ExceptionHandler
    {
        public static void UseApiExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError => {
                appError.Run(async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature!=null)
                    {
                        //记录日志
                        TraceHelper.SendCustomLog("GlobalException", contextFeature.Error.StackTrace);

                        //优雅处理异常
                        await context.Response.WriteAsync(new ErrorDetails { StatusCode=context.Response.StatusCode, Message="Something went wrongs. Please try again later"}.ToString());
                    }
                });
            });
        }
    }
}
