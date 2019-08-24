using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Net;

namespace tik_botu
{
    public partial class Form1 : Form
    {
        string Surum = "V3.0.0";
        public Form1()
        {
            InitializeComponent();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Guncelleme_Kontrol();           
            prxy_tr = "4";
        }
        public async void Guncelleme_Kontrol()
        {
            try
            {
                using (WebClient wclient = new WebClient())
                {
                    string s = await wclient.DownloadStringTaskAsync("https://kajmeran41.blogspot.com/p/botguncelleme.html");
                    if (s.Substring(s.IndexOf("V"), s.IndexOf("'")).Replace("' property='og:descrip", "") != Surum)
                    {

                        textBox5.Text = "Yeni Sürüm Mevcut" + Environment.NewLine + s.Substring(s.IndexOf("V"), s.IndexOf("'")).Replace("' property='og:descrip", "");

                    }

                }
            }
            catch (Exception)
            {
                textBox5.Text = "Güncelleme kontrol edilemedi.";
            }
        }
        Tarayici t = null;
        bool pop = true;
        string prxy_tr = "";
        List<Tarayici> tarayici_listesi = new List<Tarayici>();
        private void button1_Click(object sender, EventArgs e)
        {
         
                if (listBox1.Items.Count > 0 && listBox2.Items.Count > 0)
                {
                    for (int i = 0; i < Convert.ToInt32(numericUpDown1.Value); i++)
                    {

                        t = new Tarayici(listBox1.Items.Cast<string>().ToList(), listBox2.Items.Cast<string>().ToList(),
                        prxy_tr, pop, listBox4.Items.Cast<string>().ToList(), listBox5.Items.Cast<string>().ToList(),
                        Convert.ToInt32(numericUpDown2.Value), i.ToString());

                        tarayici_listesi.Add(t);

                        if (!checkBox2.Checked)
                        {
                            t.Opacity = 100;
                            t.Show();
                        }
                        else { t.Opacity = 0; t.ShowInTaskbar = false; }

                        System.Threading.Thread.Sleep(300);
                    }

                    button1.Enabled = false;
                    button2.Enabled = true;
                    başlatToolStripMenuItem.Enabled = false;
                    durdurToolStripMenuItem.Enabled = true;
                }
            
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            foreach(Tarayici tr in tarayici_listesi)
            {
                tr.Dispose();
                tr.Close();
            }
            GC.Collect();
            button1.Enabled = true;
            button2.Enabled = false;
            başlatToolStripMenuItem.Enabled = true;
            durdurToolStripMenuItem.Enabled = false;
        }
      
        private void radioButton1_Click(object sender, EventArgs e)
        {
            prxy_tr = "h";
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            prxy_tr = "4";
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            prxy_tr = "5";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked) prxy_tr = "h";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) prxy_tr = "4";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) prxy_tr = "5";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Metin Belgesi (.txt)|*.txt";
            op.Title = "Proxy listesi seçin";
            if(op.ShowDialog() == DialogResult.OK)
            listBox2.Items.AddRange(File.ReadAllLines(op.FileName));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Opacity = 0;
            ShowInTaskbar = false;
            notifyIcon1.BalloonTipTitle = "Link Tıklama Otomasyonu";
            notifyIcon1.BalloonTipText = "Ben buradayım! üstümde sağ tık yaparak menüyü açabilirsiniz.";
            notifyIcon1.BalloonTipIcon = (ToolTipIcon)1;
            notifyIcon1.ShowBalloonTip(2000);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Metin Belgesi (.txt)|*.txt";
            op.Title = "Link listesi seçin";
            if (op.ShowDialog() == DialogResult.OK)
                listBox1.Items.AddRange(File.ReadAllLines(op.FileName));
        }
        bool goster_gizle = false;
        private void gösterGizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (goster_gizle == false) { Opacity = 0; goster_gizle = true; ShowInTaskbar = false;
            } else Opacity = 100; goster_gizle = true; ShowInTaskbar = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { pop = true; } else { pop = false; }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
        }
       
        private void kaldırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
                listBox1.Items.Remove(listBox1.SelectedItem);
        }

      
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems.Count > 0)
                listBox2.Items.Remove(listBox2.SelectedItem);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listBox4.Items.Add(textBox2.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Metin Belgesi (.txt)|*.txt";
            op.Title = "Referer Link listesi seçin";
            if (op.ShowDialog() == DialogResult.OK)
                listBox4.Items.AddRange(File.ReadAllLines(op.FileName));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            listBox4.Items.Remove(listBox4.SelectedItem);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            listBox5.Items.Remove(listBox5.SelectedItem);
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void başlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void durdurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            listBox5.Items.Add(textBox3.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Metin Belgesi (.txt)|*.txt";
            op.Title = "User Agent listesi seçin";
            if (op.ShowDialog() == DialogResult.OK)
                listBox5.Items.AddRange(File.ReadAllLines(op.FileName));
        }

        private void button12_Click(object sender, EventArgs e)
        {
            listBox5.Items.Clear();
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked) { prxy_tr = "s"; }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            
            listBox2.Items.Add(textBox4.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
      
    }

}

