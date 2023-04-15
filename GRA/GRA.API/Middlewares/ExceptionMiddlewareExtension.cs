using GRA.Application.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace GRA.API.Middlewares
{
    /// <summary>
    /// Exception Handling.
    /// </summary>
    public static class ExceptionMiddlewareExtension
    {
        /// <summary>
        /// Configure the Exception Handler.
        /// </summary>
        /// <param name="app"></param>
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature == null)
                        return;

                    // TODO: Use can logger the error using the "contextFeature.Error".

                    // The object that contains information about the response.
                    var errorDetails = new Response400ClientErrorViewModel<object>(contextFeature.Error?.Message, HttpStatusCode.BadRequest);

                    var jsonObject = JsonConvert.SerializeObject(errorDetails);

                    // Microsoft.AspNetCore.Http.Abstractions
                    await context.Response.WriteAsync(jsonObject);
                });
            });
        }
    }
}
