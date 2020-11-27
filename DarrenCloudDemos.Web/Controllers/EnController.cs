using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper.Wrappers;
using CertificateManager;
using DarrenCloudDemos.Lib.Helpers;
using DarrenCloudDemos.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DarrenCloudDemos.Web.Controllers
{
    /// <summary>
    /// 加密解密
    /// </summary>
    [Route("api/en")]
    [ApiController]
    public class EnController : Controller
    {
        private readonly CreateCertificates _createCertificates;
        private readonly ImportExportCertificate _importExportCertificate;
        private readonly SymmetricEncryptDecrypt _symmetricEncryptDecrypt;
        private readonly AsymmetricEncryptDecrypt _asymmetricEncryptDecrypt;
        private string _origin = "";

        public EnController(CreateCertificates createCertificates,ImportExportCertificate importExportCertificate, SymmetricEncryptDecrypt symmetricEncryptDecrypt,
        AsymmetricEncryptDecrypt asymmetricEncryptDecrypt)
        {
            _createCertificates = createCertificates;
            _importExportCertificate = importExportCertificate;
            _symmetricEncryptDecrypt = symmetricEncryptDecrypt;
            _asymmetricEncryptDecrypt = asymmetricEncryptDecrypt;
        }

        public IActionResult Index()
        {
            var identityRsaCert3072 = CreateRsaCertificates.CreateRsaCertificate(_createCertificates, 3072);
            var publicKeyPem = _importExportCertificate.PemExportPublicKeyCertificate(identityRsaCert3072);
            var privateKeyPem = _importExportCertificate.PemExportRsaPrivateKey(identityRsaCert3072);

            var dicEntity = new EncryptDemo();
            dicEntity.PublicKey = publicKeyPem;
            dicEntity.PrivateKey = privateKeyPem;

            Mock.dics.Add("demo", dicEntity);

            //string temp = $"public key:{publicKeyPem}, private key:{privateKeyPem}";

            #region 加密
            var (Key, IVBase64) = _symmetricEncryptDecrypt.InitSymmetricEncryptionKeyIV();

            var encryptedText = _symmetricEncryptDecrypt.Encrypt(_origin, IVBase64, Key);

            var targetUserPublicCertificate = _importExportCertificate.PemImportCertificate(publicKeyPem);

            var encryptedKey = _asymmetricEncryptDecrypt.Encrypt(Key,
        Utils.CreateRsaPublicKey(targetUserPublicCertificate));

            var encryptedIV = _asymmetricEncryptDecrypt.Encrypt(IVBase64,
                Utils.CreateRsaPublicKey(targetUserPublicCertificate));

            var encryptedDto = new EncryptedDto
            {
                EncryptedText = encryptedText,
                Key = encryptedKey,
                IV = encryptedIV
            };
            #endregion

            #region 解密
            var certWithPublicKey = _importExportCertificate.PemImportCertificate(publicKeyPem);
            var privateKey = _importExportCertificate.PemImportPrivateKey(privateKeyPem);

            var cert = _importExportCertificate.CreateCertificateWithPrivateKey(
                certWithPublicKey, privateKey);

            var key = _asymmetricEncryptDecrypt.Decrypt(encryptedDto.Key,
        Utils.CreateRsaPrivateKey(cert));

            var IV = _asymmetricEncryptDecrypt.Decrypt(encryptedDto.IV,
                Utils.CreateRsaPrivateKey(cert));

            var text = _symmetricEncryptDecrypt.Decrypt(encryptedDto.EncryptedText, IV, key);
            #endregion

            return Content(text);
        }

        [HttpGet]
        [Route("encrypt")]
        public ApiResponse Encrypt()
        {
            var dto = new EncryptDemo();

            var identityRsaCert3072 = CreateRsaCertificates.CreateRsaCertificate(_createCertificates, 3072);
            var publicKeyPem = _importExportCertificate.PemExportPublicKeyCertificate(identityRsaCert3072);
            var privateKeyPem = _importExportCertificate.PemExportRsaPrivateKey(identityRsaCert3072);

            var (Key, IVBase64) = _symmetricEncryptDecrypt.InitSymmetricEncryptionKeyIV();

            var encryptedText = _symmetricEncryptDecrypt.Encrypt(_origin, IVBase64, Key);

            var targetUserPublicCertificate = _importExportCertificate.PemImportCertificate(publicKeyPem);

            var encryptedKey = _asymmetricEncryptDecrypt.Encrypt(Key,
        Utils.CreateRsaPublicKey(targetUserPublicCertificate));

            var encryptedIV = _asymmetricEncryptDecrypt.Encrypt(IVBase64,
                Utils.CreateRsaPublicKey(targetUserPublicCertificate));

            dto.PublicKey = publicKeyPem;
            dto.PrivateKey = privateKeyPem;
            dto.Key = encryptedKey;
            dto.IV = encryptedIV;
            dto.EncryptedText = encryptedText;


            return new ApiResponse(dto, StatusCodes.Status200OK);
        }

        [HttpPost]
        [Route("decrypt")]
        public ApiResponse Decrypt(EncryptDemo dto)
        {
            var certWithPublicKey = _importExportCertificate.PemImportCertificate(dto.PublicKey);
            var privateKey = _importExportCertificate.PemImportPrivateKey(dto.PrivateKey);

            var cert = _importExportCertificate.CreateCertificateWithPrivateKey(
                certWithPublicKey, privateKey);

            var key = _asymmetricEncryptDecrypt.Decrypt(dto.Key,
        Utils.CreateRsaPrivateKey(cert));

            var IV = _asymmetricEncryptDecrypt.Decrypt(dto.IV,
                Utils.CreateRsaPrivateKey(cert));

            var text = _symmetricEncryptDecrypt.Decrypt(dto.EncryptedText, IV, key);

            return new ApiResponse("New record has been created in the database", text, StatusCodes.Status201Created);
        }
    }
}
