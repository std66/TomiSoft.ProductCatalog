namespace TomiSoft.ProductCatalog.BusinessModels.Concepts {
    public sealed class ManufacturerIdBM : NumericIdentifierBM {
        public ManufacturerIdBM(long value) : base(value) {
        }

        public static implicit operator ManufacturerIdBM(long value) => new ManufacturerIdBM(value);
        public static implicit operator ManufacturerIdBM(long? value) => value.HasValue ? new ManufacturerIdBM(value.Value) : null;
        public static implicit operator long(ManufacturerIdBM identifierBM) => identifierBM.Value;
        public static implicit operator long?(ManufacturerIdBM identifierBM) => identifierBM == null ? null : new long?(identifierBM.Value);
    }
}
