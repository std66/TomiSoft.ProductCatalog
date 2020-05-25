using System.Net;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;

namespace TomiSoft.ProductCatalog.Server.Result {
    public class ApiErrorResult : ApiResult {
        public ApiErrorResult(ErrorResultDto.ErrorCodeEnum errorCode, string message, HttpStatusCode statusCode) 
            : base(new ErrorResultDto() { ErrorCode = errorCode, Message = message}, statusCode) {
        }
    }
}
