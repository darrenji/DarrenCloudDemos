using CertificateManager;
using DarrenCloudDemos.Lib.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography;

namespace DarrenCloudDemos.Con
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 非对称加密1
            //var serviceProvider = new ServiceCollection()
            //    .AddCertificateManager()
            //    .BuildServiceProvider();

            //var cc = serviceProvider.GetService<CreateCertificates>();

            //var cert3072 = CreateRsaCertificates.CreateRsaCertificate(cc, 3072);

            //var text = "I have a big dog. You've got a cat. We all love animals!";

            //Console.WriteLine("-- Encrypt Decrypt asymmetric --");
            //Console.WriteLine("");

            //var asymmetricEncryptDecrypt = new AsymmetricEncryptDecrypt();

            //var encryptedText = asymmetricEncryptDecrypt.Encrypt(text,
            //    Utils.CreateRsaPublicKey(cert3072));

            //Console.WriteLine("");
            //Console.WriteLine("-- Encrypted Text --");
            //Console.WriteLine(encryptedText);

            //var decryptedText = asymmetricEncryptDecrypt.Decrypt(encryptedText,
            //   Utils.CreateRsaPrivateKey(cert3072));

            //Console.WriteLine("-- Decrypted Text --");
            //Console.WriteLine(decryptedText);
            #endregion

            #region 非对称加密2
            //RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            //string publicKey = rsa.ToXmlString(false); // false to get the public key   
            //string privateKey = rsa.ToXmlString(true); // true to get the private key

            //var text = "I have a big dog. You've got a cat. We all love animals!";
            //string encrypted = RSAHelper.Encrypt(publicKey, text);
            //Console.WriteLine(encrypted);

            //string decrypted = RSAHelper.Decrypt(privateKey, encrypted);
            //Console.WriteLine(decrypted);
            #endregion


            Console.WriteLine();
        }
    }
}
