using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.DataManagement;

namespace TomiSoft.ProductCatalog.WebUI.Controllers {
    public class CategoryController : Controller {
        private readonly ICategoryDataManager categoryDataManager;

        public CategoryController(ICategoryDataManager categoryDataManager) {
            this.categoryDataManager = categoryDataManager;
        }

        public async Task<IActionResult> Index([FromQuery, Required] string languageCode) {
            ViewBag.Categories = await categoryDataManager.GetAllAsync(languageCode);
            return View("Index");
        }
    }
}
