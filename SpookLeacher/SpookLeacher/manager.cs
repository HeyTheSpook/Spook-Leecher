using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpookLeacher
{
    class manager
    {
        public static int proxycount = 0;
        public static int results = 0;
        public static int linkct = 0;
        public static bool run = false;
        public static string time = "&tbs=qdr:w";
        public static string[] keywords = { "@gmail.com", "@yahoo.com", "@aol.com", "@hotmail.com", "@yandex.com", "@yahoo.co.uk", "@walla.co.il" };
        public static string[] proxies = { "" };
       

        public static string getProxy() {
            Random _random = new Random();
            int r = _random.Next(proxycount);
            return proxies[r];
        }
    }
}
