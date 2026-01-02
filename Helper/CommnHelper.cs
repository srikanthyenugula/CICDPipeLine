using Microsoft.AspNetCore.Components.Forms;
using System.Security.Cryptography;
using System.Text;

namespace CICDPipeLine.Helper
{
    public class CommnHelper
    {
        private string DecryptValue(string value)
        {
            using (var aes = new AesCryptoServiceProvider
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                Key = GetKey("Test"),
                IV = GetIV("Test")
            })
            using (var decryptor = aes.CreateDecryptor())
            {
                var buffer = Convert.FromBase64String(value.Replace("", "+"));
                var plainBytes = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(plainBytes);
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
