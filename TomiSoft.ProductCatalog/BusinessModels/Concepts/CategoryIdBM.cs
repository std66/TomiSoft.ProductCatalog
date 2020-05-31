﻿namespace TomiSoft.ProductCatalog.BusinessModels.Concepts {
    public sealed class CategoryIdBM : NumericIdentifierBM {
        public CategoryIdBM(long value) : base(value) {
        }

        public static implicit operator CategoryIdBM(long value) => new CategoryIdBM(value);
        public static implicit operator long(CategoryIdBM identifierBM) => identifierBM.Value;
    }
}
