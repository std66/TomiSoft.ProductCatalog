using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.Server.Configuration;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Controllers;
using TomiSoft.ProductCatalog.Server.OpenApiGenerated.Models;

namespace TomiSoft.ProductCatalog.Server.Controllers {
    public class SystemController : SystemApiController {
        private readonly LanguageConfiguration languageConfiguration;

        public SystemController(IOptionsSnapshot<LanguageConfiguration> languageOptions) {
            languageConfiguration = languageOptions.Value;
        }

        public override Task<IActionResult> GetSupportedLanguages() {
            return Task.FromResult<IActionResult>(
                new ApiResult(
                    new SystemSupportedLanguagesDto() {
                        DefaultLanguage = languageConfiguration.DefaultLanguage,
                        SupportedLanguages = new List<string>(languageConfiguration.SupportedLanguages)
                    }
                )
            );
        }
    }
}
