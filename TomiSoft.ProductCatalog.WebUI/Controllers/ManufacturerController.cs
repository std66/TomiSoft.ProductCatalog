using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.DataManagement;
using TomiSoft.ProductCatalog.WebUI.Models;

namespace TomiSoft.ProductCatalog.WebUI.Controllers {
    public class ManufacturerController : Controller {
        private readonly ILogger<ManufacturerController> _logger;
        private readonly IManufacturerDataManager dataManager;

        public ManufacturerController(ILogger<ManufacturerController> logger, IManufacturerDataManager dataManager) {
            _logger = logger;
            this.dataManager = dataManager;
        }

        public async Task<IActionResult> Index() {
            ViewBag.Manufacturers = await dataManager.GetAllAsync();
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddManufacturer([FromForm] string name, [FromForm] string countrycode, [FromForm] int zipcode, [FromForm] string address, [FromForm] Uri website_url) {
            await dataManager.InsertAsync(
                new BusinessModels.ManufacturerBM(
                    default(int),
                    name,
                    new BusinessModels.ManufacturerLocationBM(countrycode, zipcode, address),
                    website_url
                )
            );

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddWebsiteUrl(int id, string url) {
            ManufacturerBM model = await dataManager.GetAsync(id);

            await dataManager.UpdateAsync(
                new ManufacturerBM(model.ManufacturerId, model.Name, model.Location, new Uri(url))
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
