using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WindowsAuthServices.Models;
using System.Security.Cryptography;
using System.Text;


namespace WindowsAuthServices.Controllers
{
    public class AuthController : Controller
    {
       

        //test git pull
        public IActionResult Index(string returnUrl)
        {
            if(returnUrl == null)
            {
                return View();
            }
            
            var Gid = User.Identity.Name;
            //取后八位GID
            Gid = Gid.Substring(Gid.Length - 8);
            //32位key
            var enctpytGid = MD5Hash(Gid, "SSMECT" + System.DateTime.Now.ToString("yyyyMMdd"));
            
            return Redirect(returnUrl +  (returnUrl.Contains("?")?"&":"?")  + "gid=" + Gid + "&encrypt=" + enctpytGid);
        }


        private string MD5Hash(string text, string key)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(text + key));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
