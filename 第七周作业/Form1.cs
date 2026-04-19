using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 第七周作业
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient _http = new HttpClient();

        public Form1()
        {
            InitializeComponent();

            // 关键：必须加这个请求头，不然必出安全验证
            _http.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

            textBox2.Multiline = true;
            textBox3.Multiline = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string kw = textBox1.Text?.Trim();
            if (string.IsNullOrEmpty(kw))
            {
                MessageBox.Show("请输入关键词");
                return;
            }

            button1.Enabled = false;

            try
            {
                var t1 = GetSearchText($"https://www.baidu.com/s?wd={Uri.EscapeDataString(kw)}");
                var t2 = GetSearchText($"https://cn.bing.com/search?q={Uri.EscapeDataString(kw)}");

                await Task.WhenAll(t1, t2);

                // 真正前200字
                textBox2.Text = Substring200(await t1);
                textBox3.Text = Substring200(await t2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("失败：" + ex.Message);
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        // 统一获取 + 清理 HTML
        private async Task<string> GetSearchText(string url)
        {
            string html = await _http.GetStringAsync(url);

            // 清理所有标签
            string res = Regex.Replace(html, @"<(script|style|head|header|footer)[\s\S]*?</\1>", "", RegexOptions.IgnoreCase);
            res = Regex.Replace(res, "<[^>]+>", " ");       // 删标签
            res = Regex.Replace(res, @"[\s\r\n\t]+", " ");  // 空格换行压缩

            return res.Trim();
        }

        // 严格 200 字
        private string Substring200(string s)
        {
            return s.Length > 200 ? s.Substring(0, 200) + "…" : s;
        }
    }
}