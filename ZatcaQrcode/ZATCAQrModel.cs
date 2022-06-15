using System;

namespace ZatcaQrcode
{
    public class ZATCAQrModel
    {
        public string SellerName { get; set; }
        public string VatNumber { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public double Vat { get; set; }
        public string XmlInvoiceHash { get; set; }
        public string Signature { get; set; }
        public string PublicKey { get; set; }
        public string CryptographicStamp { get; set; }
    }
}
