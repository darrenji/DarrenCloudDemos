using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DarrenCloudDemos.Lib.Helpers
{
    public class RSAHelper
    {
        public static string Encrypt(string publicKey, string text)
        {
            //把字符串转换成字节数组
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = byteConverter.GetBytes(text);

            byte[] encryptedData;//加密后的字节数组

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                //设置公钥
                rsa.FromXmlString(publicKey);

                //加密字节数组
                encryptedData = rsa.Encrypt(dataToEncrypt, false);
            }

            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string privateKey, string encryptedStr)
        {
            //被加密字符串转换成字节数组
            byte[] dataToDecrypt = Convert.FromBase64String(encryptedStr);

            //解密后的字节数组
            byte[] decryptedData;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                decryptedData = rsa.Decrypt(dataToDecrypt, false);
            }

            UnicodeEncoding byteConverter = new UnicodeEncoding();
            return byteConverter.GetString(decryptedData);
        }
    }
}
