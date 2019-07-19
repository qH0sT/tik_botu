using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;                                     //GÜNCELLEME SİSTEMİ

namespace tik_botu
{
    public partial class Intro : Form
    {

        WebClient wc; 
        Stopwatch sw = new Stopwatch();
        public Intro()
        {
            InitializeComponent();
           
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                using (WebClient wclient = new WebClient())
                {
                    string s = wclient.DownloadString("https://kajmeran41.blogspot.com/p/botguncelleme.html");
                    if (s.Substring(s.IndexOf("V"), s.IndexOf("'")).Replace("' property='og:descrip", "") != label1.Text.Replace("Sürüm ", ""))
                    {
                        label3.Visible = true;
                        using (wc = new WebClient())
                        {
                            wc.DownloadDataCompleted += new DownloadDataCompletedEventHandler(indirme_bitti);
                            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(surec);
                            sw.Start();
                            wc.DownloadDataAsync(new Uri("https://habersitesi.000webhostapp.com/Link_Bot_Yeni.rar"));
                        }

                    }

                    else { Opacity = 0; new Form1().Show(); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Bağlanılamadı");
                Environment.Exit(0);
            }
        }
        
        private void surec(object sender, DownloadProgressChangedEventArgs e)
        {
            label5.Text = string.Format("{0} KB/S", (e.BytesReceived / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));
            progressBar1.Value = e.ProgressPercentage;
            label2.Text = "%"+e.ProgressPercentage.ToString();
            label6.Text = string.Format("{0} MB / {1} MB",
            (e.BytesReceived / 1024d / 1024d).ToString("0.00"),
            (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
        }

        private void indirme_bitti(object sender, DownloadDataCompletedEventArgs e)
        {
            sw.Reset();
            label3.Text = "İndirme işlemi bitti. Yeni Program Masaüstünde.";
            File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Link_Bot_Yeni.rar", e.Result);
            button1.Visible = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try { Process.Start("winrar.exe", Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Link_Bot_Yeni.rar"); } catch (Exception) { }
           
        }
    }
}
