using System.Collections.Generic;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;

namespace TomiSoft.ProductCatalog.BusinessModels.Request {
    public class UpdateProductRequestBM {
        public UpdateProductRequestBM(BarcodeBM barcode, int? newManufacturerId, int? newCategoryId, IReadOnlyDictionary<string, string> productName) {
            Barcode = barcode;
            NewManufacturerId = newManufacturerId;
            NewCategoryId = newCategoryId;
            ProductName = productName;
        }

        public BarcodeBM Barcode { get; }
        public int? NewManufacturerId { get; }
        public int? NewCategoryId { get; }
        public IReadOnlyDictionary<string, string> ProductName { get; }
    }
}
