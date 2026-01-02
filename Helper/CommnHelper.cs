using Microsoft.AspNetCore.Components.Forms;
using System.Security.Cryptography;
using System.Text;

namespace CICDPipeLine.Helper
{
    public class CommnHelper
    {
        private string DecryptValue(string value)
        {
            using (var des = new TripleDESCryptoServiceProvider { Mode=CipherMode.ECB, Key=GetKey("test"),Padding=PaddingMode.PKCS7})
            using (var desEncrypt = des.CreateDecryptor())
            {
                var buffer = Convert.FromBase64String(value.Replace("", "+"));
                return Encoding.UTF8.GetString(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
        }

        private byte[] GetKey(string value)
        {
            string val = null;
            if (Encoding.UTF8.GetByteCount(value) < 24)
            {
                val = value.PadRight(24, ' ');
            } else
            {
                val = value.Substring(0, 24);
            }
            return Encoding.UTF8.GetBytes(val);
        }
    }
}
