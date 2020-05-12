using System;
using System.Collections.Generic;

namespace TomiSoft.ProductCatalog.BusinessModels.Request {
    public class CreateProductRequestBM {
        public CreateProductRequestBM(string barcode, int manufacturerId, int categoryId, IReadOnlyDictionary<string, string> name) {
            Barcode = barcode ?? throw new ArgumentNullException(nameof(barcode));
            ManufacturerId = manufacturerId;
            CategoryId = categoryId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Barcode { get; }
        public int ManufacturerId { get; }
        public int CategoryId { get; }
        public IReadOnlyDictionary<string, string> Name { get; }
    }
}
