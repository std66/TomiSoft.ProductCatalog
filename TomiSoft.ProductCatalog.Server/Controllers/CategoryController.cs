using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.Server.Helper;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Controllers;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;
using TomiSoft.ProductCatalog.Server.Result;
using TomiSoft.ProductCatalog.Services;

namespace TomiSoft.ProductCatalog.Server.Controllers {
    public class CategoryController : CategoryApiController {
        private readonly ICategoryService categoryService;
        private readonly ILocalizationHelper localizationHelper;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService categoryService, ILocalizationHelper localizationHelper, IMapper mapper) {
            this.categoryService = categoryService;
            this.localizationHelper = localizationHelper;
            this.mapper = mapper;
        }

        public override async Task<IActionResult> GetAllCategories(
            [FromHeader(Name = "Accept-Language")] string acceptLanguage
        ) {
            string languageCode = localizationHelper.GetLanguageCode(acceptLanguage);

            var result = await categoryService.GetAllCategoriesAsync(languageCode);

            if (result.Count == 0)
                return NoContent();

            return new ApiResult(
                result.Select(x => mapper.Map<LocalizedCategoryWithProductCountBM, CategoryInfoWithProductCountDto>(x)),
                HttpStatusCode.OK
            );
        }
    }
}
