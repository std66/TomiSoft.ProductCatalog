using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;
using TomiSoft.ProductCatalog.BusinessModels.Request;
using TomiSoft.ProductCatalog.Server.Helper;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Controllers;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;
using TomiSoft.ProductCatalog.Server.Result;
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

        public override async Task<IActionResult> DeleteByBarcode(
            [FromRoute, Required] string barcode
        ) {
            EmptyResultBM<DeleteProductExplanation> result = await productService.DeleteProductAsync(barcode);

            if (!result.Successful) {
                switch (result.Explanation) {
                    case DeleteProductExplanation.DatabaseError:
                        return new ApiGenericErrorResult();

                    case DeleteProductExplanation.ProductNotExists:
                        return new ApiErrorResult(
                            ErrorResultDto.ErrorCodeEnum.ProductNotFoundEnum,
                            "Product was not found with the given barcode",
                            HttpStatusCode.NotFound
                        );
                }
            }

            return NoContent();
        }

        public override async Task<IActionResult> GetByBarcode(
            [FromRoute, Required] string barcode,
            [FromHeader(Name = "Accept-Language")] string acceptLanguage
        ) {
            string languageCode = localizationHelper.GetLanguageCode(acceptLanguage);
            LocalizedProductBM result = await productService.GetProductAsync(barcode, languageCode);
            
            if (result == null) {
                return new ApiErrorResult(
                    ErrorResultDto.ErrorCodeEnum.ProductNotFoundEnum,
                    "Product was not found with the given barcode",
                    HttpStatusCode.NotFound
                );
            }

            return new ApiResult(
                mapper.Map<LocalizedProductBM, ProductInformationDto>(result)
            );
        }

        public override async Task<IActionResult> PatchProduct(
            [FromRoute, Required] string barcode, 
            [FromBody] PatchProductRequestDto patchProductRequestDto
        ) {
            EmptyResultBM<UpdateProductExplanation> result = await productService.UpdateProductAsync(
                new UpdateProductRequestBM(
                    barcode: barcode,
                    newManufacturerId: patchProductRequestDto.ManufacturerId,
                    newCategoryId: patchProductRequestDto.CategoryId,
                    productName: patchProductRequestDto.ProductName
                )
            );

            if (result.Successful) {
                return NoContent();
            }
            else {
                switch (result.Explanation) {
                    case UpdateProductExplanation.ProductNotFound:
                        return new ApiErrorResult(
                            ErrorResultDto.ErrorCodeEnum.ProductNotFoundEnum,
                            "Product was not found with the given barcode",
                            HttpStatusCode.NotFound
                        );

                    default:
                        return new ApiGenericErrorResult();
                }
            }
        }

        public override async Task<IActionResult> PostNewProduct(
            [FromRoute, Required] string barcode,
            [FromBody] PostProductRequestDto postProductRequestDto
        ) {
            CreateProductRequestBM request = new CreateProductRequestBM(barcode, postProductRequestDto.ManufacturerId, postProductRequestDto.CategoryId, postProductRequestDto.ProductName);

            EmptyResultBM<CreateProductExplanation> result = await productService.CreateProductAsync(request);

            if (result.Successful) {
                return new NoContentResult();
            }
            else {
                switch (result.Explanation) {
                    case CreateProductExplanation.ProductWithBarcodeAlreadyExists:
                        return new ApiErrorResult(
                            ErrorResultDto.ErrorCodeEnum.ProductAlreadyExistsEnum,
                            "A product with the specified barcode already exists.",
                            HttpStatusCode.Conflict
                        );

                    default:
                        return new ApiGenericErrorResult();
                }
            }
        }
    }
}
