using System.Collections.Generic;
using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;
using TomiSoft.ProductCatalog.BusinessModels.Request;

namespace TomiSoft.ProductCatalog.DataManagement {
    public interface IProductDataManager {
        Task<LocalizedProductBM> GetLocalizedProductAsync(string barcode, string languageCode);
        Task<IReadOnlyList<LocalizedProductByCategoryBM>> GetLocalizedProductByCategoryAsync(int categoryId, string languageCode);
        Task<IReadOnlyDictionary<int, int>> GetNumberOfProductsInCategoriesAsync(params int[] categoryIds);
        Task<bool> ProductExistsWithBarcodeAsync(string barcode);
        Task<bool> CreateProductAsync(CreateProductRequestBM createProductRequest);
        Task<EmptyResultBM<DeleteProductExplanation>> DeleteProductAsync(string barcode);
    }
}
