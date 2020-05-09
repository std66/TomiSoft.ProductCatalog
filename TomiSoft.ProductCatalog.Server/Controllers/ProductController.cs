using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Server.Helper;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Controllers;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;
using TomiSoft.ProductCatalog.Services;

namespace TomiSoft.ProductCatalog.Server.Controllers {
    public class ProductController : ProductApiController {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ILocalizationHelper localizationHelper;

        public ProductController(IProductService productService, IMapper mapper, ILocalizationHelper localizationHelper) {
            this.productService = productService;
            this.mapper = mapper;
            this.localizationHelper = localizationHelper;
        }

        public override async Task<IActionResult> GetByBarcode(
            [FromRoute, Required] string barcode,
            [FromHeader(Name = "Accept-Language")] string acceptLanguage
        ) {
            string languageCode = localizationHelper.GetLanguageCode(acceptLanguage);
            LocalizedProductBM result = await productService.GetProductAsync(barcode, languageCode);
            
            if (result == null) {
                return new ApiResult(
                    new ErrorResultDto() {
                        ErrorCode = ErrorResultDto.ErrorCodeEnum.ProductNotFoundEnum,
                        Message = "Product was not found with the given barcode"
                    },
                    
                    HttpStatusCode.NotFound
                );
            }

            return new ApiResult(
                mapper.Map<LocalizedProductBM, ProductInformationDto>(result)
            );
        }
    }
}
