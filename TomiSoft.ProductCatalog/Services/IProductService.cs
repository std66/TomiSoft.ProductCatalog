using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;
using TomiSoft.ProductCatalog.BusinessModels.Request;

namespace TomiSoft.ProductCatalog.Services {
    public interface IProductService {
        Task<LocalizedProductBM> GetProductAsync(string barcode, string languageCode);
        Task<GetProductsByCategoryResultBM> GetProductsByCategoryAsync(int categoryId, string languageCode);
        Task<ResultBM<EmptyBM, CreateProductExplanation>> CreateProduct(CreateProductRequestBM createProductRequest);
    }
}