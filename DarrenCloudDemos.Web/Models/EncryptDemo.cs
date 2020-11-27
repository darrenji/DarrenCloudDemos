using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarrenCloudDemos.Web.Models
{
    public class EncryptDemo
    {
        /// <summary>
        /// 非对称公钥
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// 非对称私钥
        /// </summary>
        public string PrivateKey { get; set; }


        /// <summary>
        /// 对称加密Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 对称加密IV
        /// </summary>
        public string IV { get; set; }

        /// <summary>
        /// 加密后的字符串
        /// </summary>
        public string EncryptedText { get; set; }
    }
}
