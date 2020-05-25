using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TomiSoft.ProductCatalog.Server.Result {
    public class ApiResult : JsonResult {
        public ApiResult(object result) : base(result) {
            StatusCode = (int)HttpStatusCode.OK;
        }

        public ApiResult(object result, HttpStatusCode statusCode) : base(result) {
            StatusCode = (int)statusCode;
        }
    }
}
