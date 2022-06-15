using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ZatcaQrcode
{
    public static class QR
    {
        /// <summary>
        /// Get QR text for zatca invoice TLV encoded.
        /// </summary>
        /// <param name="model">QR code data parameter</param>
        /// <returns>QR code base64 TLV encoded string</returns>
        public static string GetText(ZATCAQrModel model)
        {
            var tags = new List<string>
            {
                model.SellerName,
                model.VatNumber,
                model.Date.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                model.Total.ToString(),
                model.Vat.ToString(),
                model.XmlInvoiceHash,
                model.Signature,
                model.PublicKey,
                model.CryptographicStamp
            };
            return GetText(tags);
        }


        /// <summary>
        /// Get QR text for zatca invoice TLV encoded.
        /// </summary>
        /// <param name="tags">Ordered list of QR tags</param>
        /// <returns>QR code base64 TLV encoded string</returns>
        public static string GetText(List<string> tags)
        {
            var conact = GetContactData(tags);
            var bytes = HexStringToHex(conact);
            return System.Convert.ToBase64String(bytes);
        }

        #region Helper methods
        private static string GetContactData(List<string> tags)
        {
            var contact = new StringBuilder();
            for (int i = 0; i < tags.Count; i++)
            {
                if (!string.IsNullOrEmpty(tags[i]))
                {
                    _ = contact.Append(GetValue((i + 1).ToString(), tags[i]));
                }
            }
            return contact.ToString();
        }
        private static string GetValue(string tagNum, string tagValue)
        {

            int length = tagValue.Length;
            if (Regex.IsMatch(tagValue, @"\p{IsArabic}"))
            {
                length = 0;
                for (int i = 0; i < tagValue.Length; i++)
                {
                    length++;
                    if (Regex.IsMatch(tagValue[i].ToString(), @"\p{IsArabic}"))
                    {
                        length++;
                    }
                }
            }

            var lengthBytes = length.ToString("X");

            var valueBytes = GetHexa(tagValue);
            if (lengthBytes.Length == 1)
            {
                lengthBytes = "0" + lengthBytes;
            }
            if (tagNum.Length == 1)
            {
                tagNum = "0" + tagNum;
            }
            return $"{tagNum}{lengthBytes}{valueBytes}";
        }
        private static byte[] HexStringToHex(string inputHex)
        {
            var resultantArray = new byte[inputHex.Length / 2];
            for (var i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = System.Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
            }
            return resultantArray;
        }
        private static string GetHexa(string value)
        {
            var sb = new StringBuilder();
            var bytes = Encoding.UTF8.GetBytes(value);
            foreach (var t in bytes)
            {
                _ = sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion
    }
}
