using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;

namespace TomiSoft.ProductCatalog.Server.Middleware {
    public class ExceptionHandlerMiddleware {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await next(context);
            }
            catch (Exception e) {
                await HandleException(context, e);
            }
        }

        private static async Task HandleException(HttpContext context, Exception e) {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(
                    new ErrorResultDto() {
                        ErrorCode = ErrorResultDto.ErrorCodeEnum.GenericErrorEnum,
                        Message = e.ToString()
                    }
                )
            );
        }
    }
}
