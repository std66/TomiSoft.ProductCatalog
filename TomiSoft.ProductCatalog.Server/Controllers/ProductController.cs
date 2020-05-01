﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Controllers;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;
using TomiSoft.ProductCatalog.Services;

namespace TomiSoft.ProductCatalog.Server.Controllers {
    public class ProductController : ProductApiController {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper) {
            this.productService = productService;
            this.mapper = mapper;
        }

        public override async Task<IActionResult> GetByBarcode([FromRoute, Required] string barcode) {
            LocalizedProductBM result = await productService.GetProductAsync(barcode, "hu-HU");

            if (result == null) {
                return new JsonResult(
                    new ErrorResultDto() {
                        ErrorCode = ErrorResultDto.ErrorCodeEnum.ProductNotFoundEnum,
                        Message = "Product was not found with the given barcode"
                    }
                ) {
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            return new JsonResult(
                mapper.Map<LocalizedProductBM, ProductInformationDto>(result)
            );
        }
    }
}