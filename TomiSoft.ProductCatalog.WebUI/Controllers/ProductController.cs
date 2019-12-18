using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.Services;

namespace TomiSoft.ProductCatalog.WebUI.Controllers {
    public class ProductController : Controller {
        private readonly IProductService productService;

        public ProductController(IProductService productService) {
            this.productService = productService;
        }

        public async Task<IActionResult> GetProductsInCategory([FromQuery, Required] int categoryId, [FromQuery, Required] string languageCode) {
            ViewBag.Products = await productService.GetProductsByCategoryAsync(categoryId, languageCode);
            return View("GetProductsInCategory");
        }

        public async Task<IActionResult> Details([FromQuery, Required] string barcode, [FromQuery, Required] string languageCode) {
            ViewBag.Product = await productService.GetProductAsync(barcode, languageCode);
            return View("Details");
        }
    }
}
