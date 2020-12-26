using CertificateManager;
using DarrenCloudDemos.Lib.DesignPatterns;
using DarrenCloudDemos.Lib.DesignPatterns.TemplateMethod;
using DarrenCloudDemos.Lib.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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


            #region Template Method Pattern
            #region 只针对int
            //Random _rand = new Random(1024);
            //var unsorted = Enumerable.Range(0, 10)
            //    .Select(t => _rand.Next(100))
            //    .ToList();
            //List<int> sorted;
            //Console.WriteLine("original array:");
            //foreach (var item in unsorted)
            //{
            //    Console.Write(item + " ");
            //}
            //Console.WriteLine();

            //var algorithm = new MergeSort();
            //sorted = algorithm.Sort(unsorted);
            //Console.WriteLine("sorted array:");
            //foreach (var item in sorted)
            //{
            //    Console.Write(item + " ");
            //}
            //Console.WriteLine(); 
            #endregion

            #region 针对泛型

            #endregion
            var names = new List<string>()
            {
                "Tamra Grist"       ,
                "Bennie Sweatt"     ,
                "Misha Mattei"      ,
                "Mable Lampkins"    ,
                "Kaley Gervasi"     ,
                "Nettie Horace"     ,
                "Cassidy Broxton"   ,
                "January Berk"      ,
                "Michele Barga"     ,
                "Arden Emig"        ,
            };
            Random _rand = new Random(1024);
            var unsorted = Enumerable.Range(0, 10)
                .Select(r => new TemplateMethodPerson(names[r], _rand.Next(100)))
                .ToList();

            List<TemplateMethodPerson> sorted;
            Console.WriteLine("Original array elements:");
            foreach (var item in unsorted)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var algorithm = new MergeSort1<TemplateMethodPerson>();
            sorted = algorithm.Sort(unsorted);

            Console.WriteLine("Sorted array elements: ");
            foreach (var item in sorted)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            #endregion

            Console.WriteLine();
        }
    }
}
