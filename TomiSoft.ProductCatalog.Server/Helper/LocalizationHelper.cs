using Microsoft.Extensions.Options;
using System;
using System.Globalization;
using System.Linq;
using TomiSoft.ProductCatalog.Server.Configuration;

namespace TomiSoft.ProductCatalog.Server.Helper {
    public class LocalizationHelper : ILocalizationHelper {
        private readonly LanguageConfiguration languageOptions;

        private class AcceptLanguageEntry {
            public AcceptLanguageEntry(string languageCode, double qualityValue) {
                LanguageCode = languageCode;
                QualityValue = qualityValue;
            }

            public string LanguageCode { get; }
            public double QualityValue { get; }
        }

        public LocalizationHelper(IOptionsSnapshot<LanguageConfiguration> languageOptions) {
            this.languageOptions = languageOptions.Value;
        }

        public string GetLanguageCode(string acceptLanguage) {
            IOrderedEnumerable<AcceptLanguageEntry> acceptLanguageEntries = GetEntries(acceptLanguage ?? languageOptions.DefaultLanguage);
            return GetClosestMatchingLanguage(acceptLanguageEntries);
        }

        private string GetClosestMatchingLanguage(IOrderedEnumerable<AcceptLanguageEntry> acceptLanguageEntries) {
            foreach (AcceptLanguageEntry entry in acceptLanguageEntries) {
                if (languageOptions.SupportedLanguages.Contains(entry.LanguageCode)) {
                    return entry.LanguageCode;
                }
                else if (languageOptions.SupportedLanguages.Where(x => x.StartsWith(entry.LanguageCode)).Any()) {
                    return languageOptions.SupportedLanguages.Where(x => x.StartsWith(entry.LanguageCode)).First();
                }
            }

            return languageOptions.DefaultLanguage;
        }

        private IOrderedEnumerable<AcceptLanguageEntry> GetEntries(string acceptLanguage) {
            return acceptLanguage
                .Replace(" ", "")
                .Split(',')
                .Select(x => {
                    string[] parts = x.Split(';');

                    if (parts.Length == 1) {
                        return new AcceptLanguageEntry(parts[0], 1.0);
                    }
                    else {
                        string quality = parts[1].Substring(2);
                        return new AcceptLanguageEntry(parts[0], double.Parse(quality, CultureInfo.InvariantCulture));
                    }
                })
                .OrderByDescending(x => x.QualityValue);
        }
    }
}
