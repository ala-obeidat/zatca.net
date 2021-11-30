# ZATCA QR code library (.Net)

An unofficial package to help developers implement ZATCA (Fatoora) QR code easily which is required for e-invoicing in Saudi Arabia.

✨ You could use it in both frontend and backend .Net projects.

✅ Validated to have the same output as ZATCA's SDK as of 30 November 2021.


## Use it:
Call the function `ZatcaQrcode.QR.GetText` and pass `ZATCAModel`, to get qr-code text
```c#
    class ZATCAModel
    {
        public string SellerName { get; set; }
        public string VatNumber { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public double Vat { get; set; }
    }
```
