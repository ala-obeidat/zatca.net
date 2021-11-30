using System;

namespace ZatcaQrcode
{
    public class ZATCAModel
    {
        public string SellerName { get; set; }
        public string VatNumber { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public double Vat { get; set; }
    }
}
