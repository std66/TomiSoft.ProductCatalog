using System.Net;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;

namespace TomiSoft.ProductCatalog.Server.Result {
    public class ApiGenericErrorResult : ApiErrorResult {

        public ApiGenericErrorResult() : this("Unknown error occurred") {

        }

        public ApiGenericErrorResult(string message) : base(ErrorResultDto.ErrorCodeEnum.GenericErrorEnum, message, HttpStatusCode.InternalServerError) {
        }
    }
}
