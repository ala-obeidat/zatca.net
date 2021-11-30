using System.Text;
using System.Text.RegularExpressions;

namespace ZatcaQrcode
{
    public static class QR
    {
        public static string GetText(ZATCAModel model)
        {
            var data1 = GetValue("1", model.SellerName);
            var data2 = GetValue("2", model.VatNumber);
            var data3 = GetValue("3", model.Date.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            var data4 = GetValue("4", model.Total.ToString());
            var data5 = GetValue("5", model.Vat.ToString());
            var conact = $"{data1}{data2}{data3}{data4}{data5}";
            var bytes = HexStringToHex(conact);
            return System.Convert.ToBase64String(bytes);
        }

        #region Helper methods
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
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion
    }
}
