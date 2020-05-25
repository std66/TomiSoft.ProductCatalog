using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Controllers;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;
using TomiSoft.ProductCatalog.Server.Result;
using TomiSoft.ProductCatalog.Services;

namespace TomiSoft.ProductCatalog.Server.Controllers {
    public class ManufacturerController : ManufacturerApiController {
        private readonly IManufacturerService manufacturerService;
        private readonly IMapper mapper;

        public ManufacturerController(IManufacturerService manufacturerService, IMapper mapper) {
            this.manufacturerService = manufacturerService;
            this.mapper = mapper;
        }

        public override async Task<IActionResult> GetAllManufacturers() {
            var manufacturers = await manufacturerService.GetAllManufacturersBriefAsync();

            return new ApiResult(
                manufacturers.Select(x => mapper.Map<BriefManufacturerBM, ManufacturerInfoDto>(x))
            );
        }

        public override async Task<IActionResult> PostNewManufacturer([FromBody] PostManufacturerRequestDto postManufacturerRequestDto) {
            var result = await manufacturerService.AddManufacturerAsync(
                name: postManufacturerRequestDto.Name,
                location: new ManufacturerLocationBM(
                    countryCode: postManufacturerRequestDto.Country,
                    address: postManufacturerRequestDto.Address
                ),
                website: new Uri(postManufacturerRequestDto.Website)
            );

            if (result.Successful) {
                return new ApiResult(
                    new PostManufacturerResponseDto() {
                        ManufacturerID = result.Object.ManufacturerId
                    },
                    HttpStatusCode.Created
                );
            }
            else {
                return new ApiErrorResult(ErrorResultDto.ErrorCodeEnum.GenericErrorEnum, "Unknown error occurred", HttpStatusCode.InternalServerError);
            }
        }
    }
}
