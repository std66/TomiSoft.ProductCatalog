namespace TomiSoft.ProductCatalog.BusinessModels {
    public class ManufacturerLogoBM {
        public ManufacturerLogoBM(byte[] data, string mimeType) {
            Data = data;
            MimeType = mimeType;
        }

        public byte[] Data { get; }
        public string MimeType { get; }
    }
}
