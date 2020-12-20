using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AspNetCoreIdentityLab.Application.Tools
{
    public class AesEncryptor
    {
        private readonly ICryptoTransform _encryptor;
        private readonly ICryptoTransform _decryptor;

        public AesEncryptor(string encryptionKey)
        {
            var secretKey = Encoding.UTF8.GetBytes(encryptionKey);
            var iv = new byte[16];

            using (var aes = Aes.Create())
            {
                _encryptor = aes.CreateEncryptor(secretKey, iv);
                _decryptor = aes.CreateDecryptor(secretKey, iv);
            }
        }

        public string Encrypt(string text)
        {
            using var msEncryptor = new MemoryStream();
            using var csEncryptor = new CryptoStream(msEncryptor, _encryptor, CryptoStreamMode.Write);
            using (var sw = new StreamWriter(csEncryptor))
            {
                sw.Write(text);
            }

            return Convert.ToBase64String(msEncryptor.ToArray());
        }

        public string Decrypt(string text)
        {
            using var msDecryptor = new MemoryStream(Convert.FromBase64String(text));
            using var csDecryptor = new CryptoStream(msDecryptor, _decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(csDecryptor);

            return sr.ReadToEnd();
        }
    }
}
