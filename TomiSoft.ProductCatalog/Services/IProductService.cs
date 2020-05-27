using System.Threading.Tasks;
using TomiSoft.ProductCatalog.BusinessModels;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;
using TomiSoft.ProductCatalog.BusinessModels.Explanations;
using TomiSoft.ProductCatalog.BusinessModels.OperationResult;
using TomiSoft.ProductCatalog.BusinessModels.Request;

namespace TomiSoft.ProductCatalog.Services {
    public interface IProductService {
        Task<LocalizedProductBM> GetProductAsync(BarcodeBM barcode, string languageCode);
        Task<GetProductsByCategoryResultBM> GetProductsByCategoryAsync(int categoryId, string languageCode);
        Task<EmptyResultBM<CreateProductExplanation>> CreateProductAsync(CreateProductRequestBM createProductRequest);
        Task<EmptyResultBM<DeleteProductExplanation>> DeleteProductAsync(BarcodeBM barcode);
        Task<EmptyResultBM<UpdateProductExplanation>> UpdateProductAsync(UpdateProductRequestBM updateRequest);
    }
}