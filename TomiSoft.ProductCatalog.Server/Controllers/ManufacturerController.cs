using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Controllers;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;
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
    }
}
