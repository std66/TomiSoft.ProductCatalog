using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.DataManagement;
using TomiSoft.ProductCatalog.Services;

namespace TomiSoft.ProductCatalog.WebUI.Controllers {
    public class CategoryController : Controller {
        private readonly ICategoryDataManager categoryDataManager;
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryDataManager categoryDataManager, ICategoryService categoryService) {
            this.categoryDataManager = categoryDataManager;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index([FromQuery, Required] string languageCode) {
            ViewBag.Categories = await categoryDataManager.GetAllAsync(languageCode);
            return View("Index");
        }
    }
}
