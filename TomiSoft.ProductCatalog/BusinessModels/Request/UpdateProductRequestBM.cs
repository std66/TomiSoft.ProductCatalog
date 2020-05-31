using System.Collections.Generic;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;

namespace TomiSoft.ProductCatalog.BusinessModels.Request {
    public class UpdateProductRequestBM {
        public UpdateProductRequestBM(BarcodeBM barcode, ManufacturerIdBM newManufacturerId, CategoryIdBM newCategoryId, IReadOnlyDictionary<string, string> productName) {
            Barcode = barcode;
            NewManufacturerId = newManufacturerId;
            NewCategoryId = newCategoryId;
            ProductName = productName;
        }

        public BarcodeBM Barcode { get; }
        public ManufacturerIdBM NewManufacturerId { get; }
        public CategoryIdBM NewCategoryId { get; }
        public IReadOnlyDictionary<string, string> ProductName { get; }
    }
}
