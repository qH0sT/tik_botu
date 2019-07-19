using Gecko;
using Gecko.DOM;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace tik_botu
{
    public partial class Tarayici : Form
    {
        List<string> prxy_lstesi = new List<string>();
        List<string> referer_listesi = new List<string>();
        List<string> user_Agent_listesi = new List<string>();
        string url = ""; 
        string[] ayrac = new string[] { "X" };
        string tur = ""; 
        int liste_say = 0;
        int basla = 0;
        int tik_sayisi = 0;
        bool ppup = false;
        public Tarayici(string url, IEnumerable<string> proxy_listesi, string prxy_turu, bool 
        popup_show, IEnumerable<string> referer, IEnumerable<string> user_agents)
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");
            this.url = url;
            prxy_lstesi.AddRange(proxy_listesi);
            referer_listesi.AddRange(referer);
            user_Agent_listesi.AddRange(user_agents);
            liste_say = prxy_lstesi.Count;
            tur = prxy_turu;
            timer1.Enabled = true;
            ppup = popup_show;
            GeckoPreferences.User["browser.xul.error_pages.enabled"] = true;
            GeckoPreferences.Default["network.proxy.type"] = 1;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (url.Contains("bc.vc")) {

                foreach (GeckoHtmlElement geckoHtmlElement2 in geckoWebBrowser1.Document.GetElementsByTagName("div"))
                {

                if (geckoHtmlElement2.GetAttribute("class") == "skip_btt")
                    {

                        geckoHtmlElement2.Click();
                        ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add("Tıklama yapıldı. " + url);
                        tik_sayisi += 1;
                        timer2.Enabled = false;
                    }

                }
            }
            
           if (url.Contains("link.tl"))
            {
                foreach (GeckoHtmlElement geckoHtmlElement2 in geckoWebBrowser1.Document.GetElementsByTagName("div"))
                {

                    if (geckoHtmlElement2.GetAttribute("class") == "btn")
                    {
                      
                        geckoHtmlElement2.Click();
                        ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add("Tıklama yapıldı. " + url);
                        tik_sayisi += 1;
                        timer2.Enabled = false;
                    }
                   
                }
            }
           
        }
        private void geckoWebBrowser1_DocumentCompleted(object sender, Gecko.Events.GeckoDocumentCompletedEventArgs e)
        {
            Text = "URL: " + geckoWebBrowser1.Url.ToString();
            ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add("Yönlendirme Tamamlandı. " + geckoWebBrowser1.Url.ToString());
            if (geckoWebBrowser1.Document.Head.InnerHtml.Contains("Wrap Long Lines"))
            {
                
                ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add("HTTP Bad Request. Tıklama yapılamadı. " + geckoWebBrowser1.Url.ToString() + " PRXY: " + prxy_lstesi[basla - 1]);
                timer1.Enabled = true;
            }

            if (geckoWebBrowser1.Url.ToString().Replace("https","http") != url.Replace("https","http"))
            {
                timer1.Enabled = true;

            }
           
            if (geckoWebBrowser1.Document.Head.InnerHtml.Contains("| Cloudflare"))
            {
                
                ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add("Cloudflare Koruma Sistemi ile karşılaşıldı. URL: " + geckoWebBrowser1.Url.ToString() + " PROXY: " + prxy_lstesi[basla - 1]);
                timer1.Enabled = true;
            }

        }
        string baglanti_turu = "";
        bool t = true; //bi test için kullanmıştım, isterseniz kaldırabilirsiniz.
       
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (basla < prxy_lstesi.Count)
            {
                Xpcom.QueryInterface<nsICookieManager>(Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1")).RemoveAll();
                CookieManager.RemoveAll();
                ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add(url+" için timer1 çalışıyor.");
                ayrac = prxy_lstesi[basla].Split(':');
                GeckoPreferences.User["general.useragent.override"] = user_Agent_listesi[new Random().Next(0, user_Agent_listesi.Count - 1)];

                if (t == true) // test için eklemiştim, isterseniz kaldırabilirsiniz bu kontrolü.
                {
                    switch (tur)
                    {
                        case "4":
                            GeckoPreferences.Default["network.proxy.socks"] = ayrac[0];
                            GeckoPreferences.Default["network.proxy.socks_port"] = Convert.ToInt32(ayrac[1]);
                            GeckoPreferences.User["network.proxy.socks_version"] = 4;
                            baglanti_turu = "SOCKS4";
                            break;

                        case "5":
                            GeckoPreferences.Default["network.proxy.socks"] = ayrac[0];
                            GeckoPreferences.Default["network.proxy.socks_port"] = Convert.ToInt32(ayrac[1]);
                            GeckoPreferences.User["network.proxy.socks_version"] = 5;
                            baglanti_turu = "SOCKS5";
                            break;

                        case "h":
                            GeckoPreferences.Default["network.proxy.type"] = 1;
                            GeckoPreferences.Default["network.proxy.http"] = ayrac[0];
                            GeckoPreferences.Default["network.proxy.http_port"] = Convert.ToInt32(ayrac[1]);
                            baglanti_turu = "HTTP";
                            break;

                        case "s":
                            GeckoPreferences.Default["network.proxy.http"] = ayrac[0];
                            GeckoPreferences.Default["network.proxy.http_port"] = Convert.ToInt32(ayrac[1]);
                            GeckoPreferences.Default["network.proxy.ssl"] = ayrac[0];
                            GeckoPreferences.Default["network.proxy.ssl_port"] = Convert.ToInt32(ayrac[1]);
                            baglanti_turu = "HTTPS";
                            break;
                    }
                }
               
                geckoWebBrowser1.Navigate(url, GeckoLoadFlags.BypassHistory, referer_listesi[new Random().Next(0,referer_listesi.Count -1)], null, null);
                
                Text = "Tarayıcı yönlendirildi: " + prxy_lstesi[basla];
                ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add(url + " için tarayıcı "+ prxy_lstesi[basla] + " adresinden yönlendirildi. Bağlantı Türü: " + baglanti_turu);
                basla += 1;
                timer2.Enabled = true;
                timer1.Enabled = false;
            }
            else {
            ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add(url + " için tıklama işlemleri bitti. "+"Toplam Tık: " + tik_sayisi.ToString());
              timer1.Enabled = false; }
        }

        private void geckoWebBrowser1_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
        {
            if(ppup == true)
            {
                e.Cancel = true;
            }
           
        }
        private void geckoWebBrowser1_NavigationError(object sender, Gecko.Events.GeckoNavigationErrorEventArgs e)
        {
            
            if (string.IsNullOrEmpty(geckoWebBrowser1.Document.Head.InnerHtml) || geckoWebBrowser1.Document.Head.InnerHtml.ToLower().Contains("page load error") || 
               e.ErrorCode == GeckoError.NS_ERROR_PROXY_CONNECTION_REFUSED || e.ErrorCode == GeckoError.NS_ERROR_UNKNOWN_PROXY_HOST
               || e.ErrorCode == GeckoError.NS_ERROR_CONNECTION_REFUSED)

            {
                ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add("URL'ye Erişilemedi: " + prxy_lstesi[basla - 1]);
                timer1.Enabled = true;
            }
           
        }
    }
}

