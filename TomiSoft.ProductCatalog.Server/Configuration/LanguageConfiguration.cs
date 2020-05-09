using System.Collections.Generic;

namespace TomiSoft.ProductCatalog.Server.Configuration {
    public class LanguageConfiguration {
        public IReadOnlyList<string> SupportedLanguages { get; set; }
        public string DefaultLanguage { get; set; }
    }
}
