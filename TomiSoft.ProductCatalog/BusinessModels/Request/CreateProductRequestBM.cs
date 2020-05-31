using System;
using System.Collections.Generic;
using TomiSoft.ProductCatalog.BusinessModels.Concepts;

namespace TomiSoft.ProductCatalog.BusinessModels.Request {
    public class CreateProductRequestBM {
        public CreateProductRequestBM(BarcodeBM barcode, ManufacturerIdBM manufacturerId, CategoryIdBM categoryId, IReadOnlyDictionary<string, string> name) {
            Barcode = barcode ?? throw new ArgumentNullException(nameof(barcode));
            ManufacturerId = manufacturerId;
            CategoryId = categoryId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public BarcodeBM Barcode { get; }
        public ManufacturerIdBM ManufacturerId { get; }
        public CategoryIdBM CategoryId { get; }
        public IReadOnlyDictionary<string, string> Name { get; }
    }
}
