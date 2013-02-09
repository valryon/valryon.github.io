using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Portfolio.Utils.Security
{
   

    public static class EncryptionHelper
    {
        /// <summary>
        /// Clé partagée avec l'application mobile
        /// </summary>
        private static string cryptoKey = "j7gdft5'(eqA84Mo"; // 16 octets MAX

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipheredtext">Texte en base 64</param>
        /// <returns></returns>
        public static String Decrypt(String cipheredText)
        {
            string finalKey = cryptoKey.PadRight(16, '\0');

            RijndaelManaged crypto = null;
            MemoryStream mStream = null;
            ICryptoTransform decryptor = null;
            CryptoStream cryptoStream = null;
 
            try
            {
                byte[] cipheredData = Convert.FromBase64String(cipheredText);

                crypto = new RijndaelManaged();
                crypto.KeySize = 128;
                crypto.Padding = PaddingMode.PKCS7;

                decryptor = crypto.CreateDecryptor(Encoding.UTF8.GetBytes(finalKey), Encoding.UTF8.GetBytes(finalKey));

                mStream = new System.IO.MemoryStream(cipheredData);
                cryptoStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read);
                StreamReader creader = new StreamReader(cryptoStream, Encoding.UTF8);

                String data = creader.ReadToEnd();
                return data;
                //return Encoding.UTF8.GetString(Encoding.Default.GetBytes(data));
            }
            finally
            {
                if (crypto != null)
                {
                    crypto.Clear();
                }

                if (cryptoStream != null)
                {
                    cryptoStream.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String Encrypt(String plainText)
        {
            string finalKey = cryptoKey.PadRight(16, '\0');

            RijndaelManaged crypto = null;
            MemoryStream mStream = null;
            ICryptoTransform encryptor = null;
            CryptoStream cryptoStream = null;

            byte[] plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

            try
            {
                crypto = new RijndaelManaged();
                crypto.KeySize = 128;
                crypto.Padding = PaddingMode.PKCS7;

                encryptor = crypto.CreateEncryptor(Encoding.UTF8.GetBytes(finalKey), Encoding.UTF8.GetBytes(finalKey));

                mStream = new MemoryStream();
                cryptoStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                if (crypto != null)
                    crypto.Clear();

                cryptoStream.Close();
            }

            return Convert.ToBase64String(mStream.ToArray());
        }

    }

}
