using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LOGINPUA.Util.Seguridad
{
    public class EncriptadorHacom
    {
        public enum Mode
        {
            ENCRYPT,
            DECRYPT
        };

        static void Main(string[] args)
        {
            string mensaje = "xxxxxxxxxxxxxx";
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.GenerateIV();
                aes.GenerateKey();

                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] encrypted = AESCrypto(Mode.ENCRYPT, aes, Encoding.UTF8.GetBytes(mensaje));
                Console.WriteLine("Encrypted text: " + BitConverter.ToString(encrypted).Replace("-", ""));
                byte[] decrypted = AESCrypto(Mode.DECRYPT, aes, encrypted);
                Console.WriteLine("Decrypted text: " + Encoding.UTF8.GetString(decrypted));
            }
            Console.ReadLine();
        }

        static byte[] AESCrypto(Mode mode, AesCryptoServiceProvider aes, byte[] mensaje)
        {
            using (var memString = new MemoryStream())
            {
                CryptoStream cryptoStream = null;

                if(mode == Mode.ENCRYPT)
                {
                    cryptoStream = new CryptoStream(memString, aes.CreateEncryptor(), CryptoStreamMode.Write);
                }
                else if (mode == Mode.DECRYPT)
                {
                    cryptoStream = new CryptoStream(memString, aes.CreateDecryptor(), CryptoStreamMode.Write);
                }

                if (cryptoStream == null)
                {
                    return null;
                }

                cryptoStream.Write(mensaje, 0, mensaje.Length);
                cryptoStream.FlushFinalBlock();
                return memString.ToArray();

            }
            return null;
        }
    }
}