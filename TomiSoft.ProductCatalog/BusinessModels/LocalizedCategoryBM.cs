using TomiSoft.ProductCatalog.BusinessModels.Concepts;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class LocalizedCategoryBM {
        public LocalizedCategoryBM(CategoryIdBM categoryId, string languageCode, string localizedName, CategoryIdBM parentCategoryId) {
            CategoryId = categoryId;
            LanguageCode = languageCode;
            LocalizedName = localizedName;
            ParentCategoryId = parentCategoryId;
        }

        public CategoryIdBM CategoryId { get; }
        public string LanguageCode { get; }
        public string LocalizedName { get; }
        public CategoryIdBM ParentCategoryId { get; }
    }
}
