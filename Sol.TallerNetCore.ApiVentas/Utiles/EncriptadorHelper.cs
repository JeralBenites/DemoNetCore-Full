using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sol.TallerNetCore.ApiVentas.Utiles
{

    public class EncriptadorHelper
    {

        private static Rfc2898DeriveBytes GetSecretKey()
        {
            const string encryptionKey = "s3ñ0R-d3-10$-@n¡110$";
            byte[] salt = Encoding.UTF8.GetBytes(encryptionKey);

            var secretKey = new Rfc2898DeriveBytes(encryptionKey, salt);
            return secretKey;
        }

        public static byte[] EncryptToByte(string rawText)
        {
            var rijndaelCipher = new RijndaelManaged();
            byte[] rawTextData = Encoding.UTF8.GetBytes(rawText);

            Rfc2898DeriveBytes secretKey = GetSecretKey();


            using (var encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(rawTextData, 0, rawTextData.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        public static string Decrypt(byte[] encryptByte)
        {
            try
            {
                var rijndaelCipher = new RijndaelManaged();

                Rfc2898DeriveBytes secretKey = GetSecretKey();

                using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
                {
                    using (var memoryStream = new MemoryStream(encryptByte))
                    {
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            var plainText = new byte[encryptByte.Length];
                            int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                            return Encoding.UTF8.GetString(plainText, 0, decryptedCount);
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

    }


}
