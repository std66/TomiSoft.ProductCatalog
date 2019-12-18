using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.DataManagement;
using TomiSoft.ProductCatalog.WebUI.Models;

namespace TomiSoft.ProductCatalog.WebUI.Controllers {
    public class ManufacturerController : Controller {
        private readonly ILogger<ManufacturerController> _logger;
        private readonly IManufacturerDataManager dataManager;
        private readonly IProductDataManager productDataManager;

        public ManufacturerController(ILogger<ManufacturerController> logger, IManufacturerDataManager dataManager, IProductDataManager productDataManager) {
            _logger = logger;
            this.dataManager = dataManager;
            this.productDataManager = productDataManager;
        }

        public async Task<IActionResult> Index() {
            ViewBag.Manufacturers = await dataManager.GetAllAsync();
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddManufacturer([FromForm] string name, [FromForm] string countrycode, [FromForm] string address, [FromForm] Uri website_url, IFormFile logo) {
            byte[] logoData = new byte[logo.Length];
            using (Stream s = logo.OpenReadStream()) {
                await s.ReadAsync(logoData, 0, logoData.Length);
            }

            await dataManager.InsertAsync(
                new ManufacturerBM(
                    default(int),
                    name,
                    new ManufacturerLocationBM(countrycode, address),
                    website_url,
                    new ManufacturerLogoBM(logoData, logo.ContentType)
                )
            );

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetCompanyLogo([FromQuery] int id) {
            ManufacturerLogoBM logo = await dataManager.GetLogoAsync(id);

            return this.File(logo.Data, logo.MimeType);
        }

        public async Task<IActionResult> AddWebsiteUrl(int id, string url) {
            ManufacturerBM model = await dataManager.GetAsync(id);

            await dataManager.UpdateAsync(
                new ManufacturerBM(model.ManufacturerId, model.Name, model.Location, new Uri(url), model.Logo)
            );

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddCompanyLogo([FromForm]int id, IFormFile logo) {
            ManufacturerBM model = await dataManager.GetAsync(id);

            byte[] logoData = new byte[logo.Length];
            using (Stream s = logo.OpenReadStream()) {
                await s.ReadAsync(logoData, 0, logoData.Length);
            }

            await dataManager.UpdateAsync(
                new ManufacturerBM(model.ManufacturerId, model.Name, model.Location, model.WebsiteUri, new ManufacturerLogoBM(logoData, logo.ContentType))
            );

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id) {
            await dataManager.DeleteAsync(
                await dataManager.GetAsync(id)
            );

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
