using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using SSLZertifikatCheck.i18n;

namespace SSLZertifikatCheck
{
    internal class UrlHelper
    {
        public static string ChangedInput(string input)
        {
           
            bool startsWithhttps = input.ToLower().StartsWith("https://");
            if (startsWithhttps)
            {
                input = input.Remove(0, 8);
            }
             
            bool startsWithhttp = input.ToLower().StartsWith("http://");
            if (startsWithhttp)
            {
                return input; 
            }
           
            bool startsWithwww = input.ToLower().StartsWith("www.");
            if (startsWithwww)
            {
                input = input.Remove(0, 4);
            }
            string[] breakApart = input.Split('/');
            bool isempty = string.IsNullOrWhiteSpace(breakApart[0]);
            if (!isempty)
            {
                input = breakApart[0];
            }
            return input;
        }
    }
}
