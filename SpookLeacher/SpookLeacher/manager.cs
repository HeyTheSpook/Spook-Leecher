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
            Random random = new Random();
            try
            {
                int r = random.Next(proxycount);

                return proxies[r];
            }
            catch { return proxies[0]; }
        }
        public static bool isCombo(string combo) {

            string[] s = combo.Split(':');
            string email = s[0];
            bool isemail = true;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                isemail = (addr.Address == email);
            }
            catch { isemail = false; }

            if (isemail) {
                return true;
            }
            else{
                return false;
            }
        }
    }
}
