using CertificateManager;
using DarrenCloudDemos.Lib.DesignPatterns;
using DarrenCloudDemos.Lib.DesignPatterns.ChainOfResponsibility;
using DarrenCloudDemos.Lib.DesignPatterns.PublishSubscribe;
using DarrenCloudDemos.Lib.DesignPatterns.Strategy;
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
            //var names = new List<string>()
            //{
            //    "Tamra Grist"       ,
            //    "Bennie Sweatt"     ,
            //    "Misha Mattei"      ,
            //    "Mable Lampkins"    ,
            //    "Kaley Gervasi"     ,
            //    "Nettie Horace"     ,
            //    "Cassidy Broxton"   ,
            //    "January Berk"      ,
            //    "Michele Barga"     ,
            //    "Arden Emig"        ,
            //};
            //Random _rand = new Random(1024);
            //var unsorted = Enumerable.Range(0, 10)
            //    .Select(r => new TemplateMethodPerson(names[r], _rand.Next(100)))
            //    .ToList();

            //List<TemplateMethodPerson> sorted;
            //Console.WriteLine("Original array elements:");
            //foreach (var item in unsorted)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();

            //var algorithm = new MergeSort1<TemplateMethodPerson>();
            //sorted = algorithm.Sort(unsorted);

            //Console.WriteLine("Sorted array elements: ");
            //foreach (var item in sorted)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();
            #endregion

            #endregion

            #region Strategy Pattern
            //把策略包裹在内部Context
            //StrategyContext context = new StrategyContext(100);
            //Console.WriteLine("Enter month number between 1 and 12");
            //var input = Console.ReadLine();
            //int month = Convert.ToInt32(input);
            //Console.WriteLine("Month =" + month);
            ////根据某个条件获取策略
            //IOfferStrategy strategy = context.GetStrategy(month);
            ////运行策略
            //context.ApplyStrategy(strategy);

            //把策略放在方法参数里
            // translate a phrase using the AustralianTranslationStrategy class
            //string aussieHello = EnglishTranslation.Translate("Hello",
            //        new AustralianTranslationStrategy());
            //Console.WriteLine(aussieHello);
            //// Hello, mate
            //// translate a phrase using the AmericanTranslationStrategy class
            //string usaHello = EnglishTranslation.Translate("Hello",
            //        new AmericanTranslationStrategy());
            //Console.WriteLine(usaHello);

            //把策略交给一个服务类
            //CommunicateViaPhone communicateViaPhone = new CommunicateViaPhone();
            //CommunicateViaEmail communicateViaEmail = new CommunicateViaEmail();
            //CommunicateViaVideo communicateViaVideo = new CommunicateViaVideo();

            //CommunicationService communicationService = new CommunicationService();
            //// via phone
            //communicationService.SetCommuncationMeans(communicateViaPhone);
            //communicationService.Communicate("1234567");
            //// via email
            //communicationService.SetCommuncationMeans(communicateViaEmail);
            //communicationService.Communicate("hi@me.com");

            //把委托交给一个服务类
            //string communicateViaPhone(string destination) => "communicating " + destination + " via Phone..";
            //CommunicationService1 communicationService = new CommunicationService1();
            //communicationService.SetCommuncationMeans(communicateViaPhone);
            //communicationService.Communicate("1234567");//内部交给了委托
            #endregion

            #region Pub/Sub
            //Pub p = new Pub();
            ////register for OnChange event - Subscriber 1
            //p.OnChange += () => Console.WriteLine("Subscriber 1!");
            ////register for OnChange event - Subscriber 2
            //p.OnChange += () => Console.WriteLine("Subscriber 2!");

            ////raise the event
            //p.Raise();

            ////After this Raise() method is called
            ////all subscribers callback methods will get invoked

            //Console.WriteLine("Press enter to terminate!");


            //Pub1 p = new Pub1();
            ////register for OnChange event - Subscriber 1
            //p.OnChange += () => Console.WriteLine("Subscriber 1!");
            ////register for OnChange event - Subscriber 2
            //p.OnChange += () => Console.WriteLine("Subscriber 2!");

            ////raise the event
            //p.Raise();

            ////After this Raise() method is called
            ////all subscribers callback methods will get invoked

            //Console.WriteLine("Press enter to terminate!");


            //Pub3 p = new Pub3();
            ////register for OnChange event - Subscriber 1
            //p.OnChange += (sender, e) => Console.WriteLine("Subscriber 1! Value:" + e.Value);
            ////register for OnChange event - Subscriber 2
            //p.OnChange += (sender, e) => Console.WriteLine("Subscriber 2! Value:" + e.Value);

            ////raise the event
            //p.Raise();

            ////After this Raise() method is called
            ////all subscribers callback methods will get invoked

            //Console.WriteLine("Press enter to terminate!");
            #endregion

            #region ChainOfResponsibility
            ////create handlers
            //var bills50s = new CurrencyBill(50, 1);
            //var bills20s = new CurrencyBill(20, 2);
            //var bills10s = new CurrencyBill(10, 5);

            ////set handlers pipeline 链条在这里
            //bills50s.RegisterNext(bills20s)
            //        .RegisterNext(bills10s);

            ////client code that uses the handler
            //while (true)
            //{
            //    Console.WriteLine("Please enter amount to dispense:");
            //    var isParsed = int.TryParse(Console.ReadLine(), out var amount);

            //    if (isParsed)
            //    {

            //        //sender pass the request to first handler in the pipeline
            //        var isDepensible = bills50s.DispenseRequest(amount);
            //        if (isDepensible)
            //        {
            //            Console.WriteLine($"Your amount ${amount} is dispensable!");
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Failed to dispense ${amount}!");
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Please enter a valid amount to dispense");
            //    }
            //}




            #endregion
            
        }
    }
}
