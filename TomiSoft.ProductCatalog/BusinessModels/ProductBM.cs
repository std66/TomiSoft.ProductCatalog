using System.Collections.Generic;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;

namespace TomiSoft.ProductCatalog.BusinessModels {
    public class ProductBM {
        public ProductBM(BarcodeBM barcode, long? manufacturerId, long? categoryId, IReadOnlyDictionary<string, string> productName) {
            Barcode = barcode;
            ManufacturerId = manufacturerId;
            CategoryId = categoryId;
            ProductName = productName;
        }

        public BarcodeBM Barcode { get; }
        public long? ManufacturerId { get; }
        public long? CategoryId { get; }
        public IReadOnlyDictionary<string, string> ProductName { get; }
    }
}
