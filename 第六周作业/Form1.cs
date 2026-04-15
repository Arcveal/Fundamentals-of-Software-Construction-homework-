using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Http;

namespace 第六周作业
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // 清空
            textBox2.Clear();
            textBox3.Clear();
            richTextBox1.Clear();

            string url = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("请输入URL");
                return;
            }

            if (!url.StartsWith("http"))
            {
                url = "https://" + url;
            }

            try
            {
                string html = await GetHtmlAsync(url);
                richTextBox1.Text = html;

                // 匹配手机号
                var phones = Regex.Matches(html, @"1[3-9]\d{9}");
                if (phones.Count > 0)
                {
                    var list = phones.Cast<Match>().Select(m => m.Value).Distinct();
                    textBox2.Text = string.Join(Environment.NewLine, list);
                }
                else
                {
                    textBox2.Text = "未找到手机号";
                }

                // 匹配邮箱
                var emails = Regex.Matches(html, @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}");
                if (emails.Count > 0)
                {
                    var list = emails.Cast<Match>().Select(m => m.Value).Distinct();
                    textBox3.Text = string.Join(Environment.NewLine, list);
                }
                else
                {
                    textBox3.Text = "未找到邮箱";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("出错：" + ex.Message);
            }
        }

        private async Task<string> GetHtmlAsync(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
                var resp = await client.GetAsync(url);
                resp.EnsureSuccessStatusCode();
                byte[] bytes = await resp.Content.ReadAsByteArrayAsync();
                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
}