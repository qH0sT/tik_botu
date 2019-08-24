using Gecko;
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
        List<string> URL_Listesi = new List<string>();
        List<string> kullanilanlar = new List<string>();

        string Rastgele_url = "";
        string Rastgele_prxy = "";
        string Rastgele_user_agents = "";
        string Rastgele_referer = "";

        string[] ayrac = new string[] { "X" };
        string tur = "";
        string id = "";
        int geri_sayim = 0;
        int geri_sayim2 = 0;
        bool ppup = false;

        public Tarayici(List<string> url, List<string> proxy_listesi, string prxy_turu, bool 
        popup_show, List<string> referer, List<string> user_agents, int time_out, string ID)
        {
            InitializeComponent();
            Xpcom.Initialize("Firefox");
            URL_Listesi.AddRange(url);
            prxy_lstesi.AddRange(proxy_listesi);
            referer_listesi.AddRange(referer);
            user_Agent_listesi.AddRange(user_agents);
            tur = prxy_turu;          
            ppup = popup_show;
            geri_sayim = time_out;
            geri_sayim2 = geri_sayim;
            id = ID;

            GeckoPreferences.User["browser.xul.error_pages.enabled"] = true;
            GeckoPreferences.Default["network.proxy.type"] = 1;

            Rastgele_url = URL_Listesi[new Random().Next(0, URL_Listesi.Count - 1)]; kullanilanlar.Add(Rastgele_url);
            Rastgele_prxy = prxy_lstesi[new Random().Next(0, prxy_lstesi.Count - 1)]; kullanilanlar.Add(Rastgele_prxy);
            Rastgele_referer = referer_listesi[new Random().Next(0, referer_listesi.Count - 1)]; kullanilanlar.Add(Rastgele_referer);
            Rastgele_user_agents = user_Agent_listesi[new Random().Next(0, user_Agent_listesi.Count - 1)]; kullanilanlar.Add(Rastgele_user_agents);

            Ana_Islem();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
       

           if (Rastgele_url.Contains("bc.vc")) {

                foreach (GeckoHtmlElement geckoHtmlElement2 in geckoWebBrowser1.Document.GetElementsByTagName("div"))
                {

                if (geckoHtmlElement2.GetAttribute("class") == "skip_btt")
                    {

                        geckoHtmlElement2.Click();
                     
                    }

                }
            }
            
           if (Rastgele_url.Contains("http://link.tl"))
            {
                foreach(GeckoHtmlElement elmnt in geckoWebBrowser1.Document.GetElementsByTagName("button"))
              
                if (elmnt.GetAttribute("id") == "get_link_btn")
                    {

                        elmnt.Click();
  
                    }
                   
                }
            
           
        }
        public void Ana_Islem()
        {
            if (prxy_lstesi.Count != 0)
            {
                geri_sayim2 = geri_sayim;
                timer2.Stop();
                timer2.Enabled = false;
                timer3.Stop();
                timer3.Enabled = false;

                Xpcom.QueryInterface<nsICookieManager>(Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1")).RemoveAll();
                CookieManager.RemoveAll();
                ayrac = Rastgele_prxy.Split(':');

                GeckoPreferences.User["general.useragent.override"] = Rastgele_user_agents;
                
                switch (tur)
                {
                    case "4":
                        GeckoPreferences.Default["network.proxy.socks"] = ayrac[0];
                        GeckoPreferences.Default["network.proxy.socks_port"] = Convert.ToInt32(ayrac[1]);
                        GeckoPreferences.User["network.proxy.socks_version"] = 4;
                        break;

                    case "5":
                        GeckoPreferences.Default["network.proxy.socks"] = ayrac[0];
                        GeckoPreferences.Default["network.proxy.socks_port"] = Convert.ToInt32(ayrac[1]);
                        GeckoPreferences.User["network.proxy.socks_version"] = 5;
                        break;

                    case "h":
                        GeckoPreferences.Default["network.proxy.type"] = 1;
                        GeckoPreferences.Default["network.proxy.http"] = ayrac[0];
                        GeckoPreferences.Default["network.proxy.http_port"] = Convert.ToInt32(ayrac[1]);
                        break;

                    case "s":
                        GeckoPreferences.Default["network.proxy.http"] = ayrac[0];
                        GeckoPreferences.Default["network.proxy.http_port"] = Convert.ToInt32(ayrac[1]);
                        GeckoPreferences.Default["network.proxy.ssl"] = ayrac[0];
                        GeckoPreferences.Default["network.proxy.ssl_port"] = Convert.ToInt32(ayrac[1]);
                        break;
                }
                               
                geckoWebBrowser1.Navigate(Rastgele_url, GeckoLoadFlags.BypassHistory, Rastgele_referer, null, null);
                ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add(Rastgele_url + " Adresine yönlendirildi. "+ Rastgele_prxy);
                Rastgele_url = URL_Listesi[new Random().Next(0, URL_Listesi.Count - 1)];
                Rastgele_prxy = prxy_lstesi[new Random().Next(0, prxy_lstesi.Count - 1)];
                prxy_lstesi.Remove(Rastgele_prxy);
                Rastgele_referer = referer_listesi[new Random().Next(0, referer_listesi.Count - 1)];
                Rastgele_user_agents = user_Agent_listesi[new Random().Next(0, user_Agent_listesi.Count - 1)];

                if (kullanilanlar.Contains(Rastgele_url))
                {
                    Rastgele_url = URL_Listesi[new Random().Next(0, URL_Listesi.Count - 1)];
                    kullanilanlar.Add(Rastgele_url);
                }
                else { kullanilanlar.Add(Rastgele_url); }

                if (kullanilanlar.Contains(Rastgele_referer))
                {
                    Rastgele_referer = referer_listesi[new Random().Next(0, referer_listesi.Count - 1)];
                    kullanilanlar.Add(Rastgele_url);
                }
                else { kullanilanlar.Add(Rastgele_referer); }

                if (kullanilanlar.Contains(Rastgele_user_agents))
                {
                    Rastgele_user_agents = user_Agent_listesi[new Random().Next(0, user_Agent_listesi.Count - 1)];
                    kullanilanlar.Add(Rastgele_url);
                }
                else { kullanilanlar.Add(Rastgele_user_agents); }

                timer2.Enabled = true;
                timer2.Start();
                timer3.Enabled = true;
                timer3.Start();
            }
            else {

                ((Form1)(Application.OpenForms["Form1"])).listBox3.Items.Add(id + " Numaralı tarayıcının proxy listesi bitti.");
                Close();

                 }
        }
        
        public delegate void Main_Process();
        private void timer3_Tick(object sender, EventArgs e)
        {
            //if (!geckoWebBrowser1.Url.ToString().Contains("http://lnk.news")) { geckoWebBrowser1.Navigate(Rastgele_url); }
           
            geri_sayim2 = geri_sayim2 - 1;
            label1.Text = geri_sayim2.ToString();
            Text = geckoWebBrowser1.Url.ToString();
            if(geri_sayim2 == 0)
            {
                Invoke(new Main_Process(Ana_Islem));
            }
        }

        private void geckoWebBrowser1_CreateWindow(object sender, GeckoCreateWindowEventArgs e)
        {
            if (ppup == true)
            {
                e.Cancel = true;
            }
        }
    }
}

