﻿using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpookLeacher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show("Welcome to Spook Leecher! A simple virus free open source leecher to learn from or critiqe! (I am in no way an expert in the field, just sombody who is interested)");
        }

        public void console(string message)//simply displays to the console whatever string is inserted
        {
            if (richTextBox3.InvokeRequired == true)
                richTextBox3.Invoke((MethodInvoker)delegate { richTextBox3.AppendText(message + "\n"); });//runs this if calling process was in another thread
            else
                richTextBox3.AppendText(message + "\n");//runs this if it is in th esame thread
            try
            {
                using (StreamWriter writetext = new StreamWriter("log.txt", true))
                {
                    writetext.WriteLine(message + " - " + DateTime.Now.ToString());
                }
            }
            catch
            {
                Thread.Sleep(1);
                console(message);
            }
        }
        public void linkout(string message)
        {
            if (richTextBox4.InvokeRequired == true)
                richTextBox4.Invoke((MethodInvoker)delegate { richTextBox4.AppendText(message + "\n"); });//runs this if calling process was in another thread
            else
                richTextBox4.AppendText(message + "\n");//runs this if it is in th esame thread
        }
        public void newCombo(string message)
        {
            if (resultsBox.InvokeRequired == true)
                resultsBox.Invoke((MethodInvoker)delegate { resultsBox.AppendText(message + "\n"); });//runs this if calling process was in another thread
            else
                resultsBox.AppendText(message + "\n");//runs this if it is in th esame thread
            try
            {
                using (StreamWriter writetext = new StreamWriter("combos.txt", true))
                {
                    writetext.WriteLine(message + " - " + DateTime.Now.ToString());
                }
            }
            catch
            {
                Thread.Sleep(1);
                newCombo(message);
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            proxylabel.Text = richTextBox2.Lines.Count().ToString();//updates the proxy counter on the form
            manager.proxycount = richTextBox2.Lines.Count();//updates the proxy counter in the manager class
            manager.proxies = richTextBox2.Lines;//sets the manager proxy list to the array in the textbox

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                manager.keywords = new string[] { "NordVPN @gmail.com", "NordVPN @yahoo.com", "NordVPN @aol.com", "NordVPN @hotmail.com", "NordVPN @yandex.com", "VPN @gmail.com", "VPN @yahoo.com", "VPN @aol.com", "VPN @hotmail.com", "VPN @yandex.com" };
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                manager.keywords = new string[] { "Uplay @gmail.com", "Uplay @yahoo.com", "Uplay @aol.com", "Uplay @hotmail.com", "Uplay @yandex.com", "Ubisoft @gmail.com", "Ubisoft @yahoo.com", "Ubisoft @aol.com", "Ubisoft @hotmail.com", "Ubisoft @yandex.com" };
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                manager.keywords = new string[] { "Spotify @gmail.com", "Spotify @yahoo.com", "Spotify @aol.com", "Spotify @hotmail.com", "Spotify @yandex.com", "music @gmail.com", "music @yahoo.com", "music @aol.com", "music @hotmail.com", "music @yandex.com" };
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
                manager.keywords = richTextBox1.Lines;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                manager.keywords = new string[] { "@gmail.com", "@yahoo.com", "@aol.com", "@hotmail.com", "@yandex.com" };
        }

        private void leechbtn_Click(object sender, EventArgs e)
        {
            int psize = manager.keywords.Length;
            progressBar1.Maximum = psize;
            manager.run = true;
            new Thread(new ThreadStart(linkscrape)).Start();

        }

        private void linkscrape()
        {
            ThreadPool.SetMaxThreads(manager.threadct, manager.threadct);
            foreach (string key in manager.keywords)
            {
                if (manager.run)
                {
                    bool more = true;
                    if (more)
                    {
                        using (var req = new HttpRequest())
                        {
                            console("scraping key " + key + "...");
                            int it = 0;

                         //   req.AddHeader("accept-language", "en-US,en;q=0.9");
                          //  req.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                          //  req.AddHeader("upgrade-insecure-requests", "1");
                          //  req.AddHeader("sec-fetch-site", "none");
                          //  req.AddHeader("sec-fetch-mode", "navigate");
                          //  req.AddHeader("sec-fetch-dest", "document");
                           // req.AddHeader("authority", "www.google.com");
                           // req.AddHeader("accept-encoding", "gzip, deflate, br");
                           // req.AddHeader("scheme", "https");
                           // req.AddHeader(HttpHeader.Referer, "https://google.com");
                            
                            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.183 Safari/537.36";
                            req.UseCookies = false;

                            if (manager.proxycount > 0)
                                req.Proxy = HttpProxyClient.Parse(manager.getProxy());

                            string html = "";
                            try
                            {
                                html = req.Get("https://www.google.com/search?q=" + key + manager.time + "&num=100&as_sitesearch=https://pastebin.com/").ToString();
                                it++;
                            }
                            catch(HttpException ex)
                            {
                                console(ex.ToString());
                                console("Request failed on: " + key);
                                manager.badreq++;
                                label13.Invoke((MethodInvoker)delegate { label13.Text = manager.badreq.ToString(); });
                                label13.Text = manager.badreq.ToString();
                                
                            }
                            string[] splits = html.Split('<');
                            foreach (string s in splits)
                            {
                                string url = s.Substring("href=\"", "\"");
                                if (String.IsNullOrEmpty(url)) { }
                                else if (url.Contains("pastebin.com/") && !url.Contains("/u/") && !url.Contains("gmail") && !url.Contains("=") && !url.Contains("search") && !url.Contains("chrome") && !url.Contains("roblox") && !url.Contains("google") && !url.Contains("how") && !url.Contains("free") && !url.Contains("accounts"))
                                {
                                    var filterthread = new Thread(() => linkfilter(url));
                                    filterthread.Start();
                                }

                            }
                            req.Dispose();
                        }
                    }
                }
                try
                {
                    progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value++; });
                }
                catch { progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value = 0; }); }
                Random r = new Random(1000);
                Thread.Sleep(r.Next()+1);
            }
            console("Link scrape complete!");

            MessageBox.Show("Link Scrape Complete!\n" + manager.linkct + " Links Found");
            progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value = 0; });
            manager.run = false;
            if ((manager.keywords.Length/2) <= manager.badreq) {
                MessageBox.Show("Potentially high amounts of errors detected. This means you lost on alot of potential links and in turn potentially lost many many accounts. Please switch your vpn area, or use a new list of proxies.");
            }
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {
            label6.Text = richTextBox4.Lines.Length.ToString();
            manager.linkct = richTextBox4.Lines.Length;
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton14.Checked) manager.time = "&tbs=qdr:h";
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton13.Checked) manager.time = "&tbs=qdr:d";
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked) manager.time = "&tbs=qdr:w";
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton11.Checked) manager.time = "&tbs=qdr:m";
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked) manager.time = "&tbs=qdr:y";
        }

        private void stopleechbtn_Click(object sender, EventArgs e)
        {
            manager.run = false;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            try
            {
                using (var req = new HttpRequest())
                {
                    string resp = req.Get("https://api.proxyscrape.com/?request=getproxies&proxytype=http&timeout=1000&country=US&ssl=all&anonymity=all").ToString();
                    richTextBox2.Text = resp;
                }
            }
            catch
            {
                MessageBox.Show("Proxyscraper api failed, check your internet connection to ensure the page isnt being blocked by somthing");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
                manager.keywords = richTextBox1.Lines;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            manager.keywords = new string[] { "Netflix @gmail.com", "Netflix @yahoo.com", "Netflix @aol.com", "Netflix @hotmail.com", "Netflix @yandex.com", "movies @gmail.com", "movies @yahoo.com", "movies @aol.com", "movies @hotmail.com", "movies @yandex.com" };
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            manager.keywords = new string[] { "Fortnite @gmail.com", "Fortnite @yahoo.com", "Fortnite @aol.com", "Fortnite @hotmail.com", "Fortnite @yandex.com", "Fortnite accounts @gmail.com", "Fortnite accounts @yahoo.com", "Fortnite accounts @aol.com", "Fortnite accounts @hotmail.com", "Fortnite accounts @yandex.com" };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int psize = richTextBox4.Lines.Length;
            progressBar1.Maximum = psize;
            manager.run = true;
            new Thread(new ThreadStart(getCombos)).Start();
        }

        private void getCombos()
        {
            string[] urllist= { };
            if (richTextBox3.InvokeRequired == true)
                richTextBox4.Invoke((MethodInvoker)delegate { urllist = richTextBox4.Lines; });
            else
                urllist = richTextBox4.Lines;
            foreach (string url in urllist)
            {
                if (manager.run)
                {
                    string newurl = url.Substring(0, 20) + "/raw/" + url.Substring(21);
                    using (var req = new HttpRequest())
                    {
                        console("checking page " + newurl + "...");
                        req.ConnectTimeout = 5;
                        req.AddHeader("accept-language" , "en-US,en;q=0.9");
                        req.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                        req.AddHeader("dnt", "1");
                        req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.183 Safari/537.36";
                        req.UseCookies = false;

                        if (manager.proxycount > 0)
                            req.Proxy = HttpProxyClient.Parse(manager.getProxy());

                        console("Requesting...");
                        string html = "";
                        try
                        {
                            html = req.Get(newurl).ToString();
                            console("Request Success!");
                        }
                        catch(HttpException ex)
                        {
                            console("Request failed!");
                            console(ex.ToString());
                        }
                        html.Replace("\r", "\n");
                        string[] words = html.Replace(" ", "\n").Split('\n');
                        foreach (string s in words)
                        {
                            if (manager.isCombo(s))
                            {
                                newCombo(s);
                                manager.results++;
                            }
                        }
                    }
                }
                progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value++; });
            }
            console("Combo scrape complete!");

            MessageBox.Show("Combo Scrape Complete!\n" + manager.results + " Combo's Found");
            progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value = 0; });
            manager.run = false;
        }
        private void linkfilter(string url) {
            if (manager.run)
            {
                using (var req2 = new HttpRequest())
                {
                    if (manager.proxycount > 0)
                        HttpProxyClient.Parse(manager.getProxy());
                    req2.UserAgentRandomize();
                    try
                    {
                        string test = req2.Get(url).ToString();
                        if (!test.Contains("This paste has been deemed potentially harmful"))
                        {
                            linkout(url);
                        }
                    }
                    catch
                    {
                        linkout(url);
                    }
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label11.Text = trackBar1.Value.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            HashSet<string> set = new HashSet<string>(richTextBox4.Lines);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            richTextBox4.Lines = result;
            console("Duplicate links removed");

            using (StreamWriter writetext = new StreamWriter("good links.txt", true))
            {
                writetext.WriteLine(Environment.NewLine +richTextBox4.Text + " - " + DateTime.Now.ToString());
            }

        }
    }
}
