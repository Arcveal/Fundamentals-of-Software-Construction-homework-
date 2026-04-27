using System;
using System.IO;
using System.Windows.Forms;

namespace FileMergeDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 选择第一个文件（对应button1）
        

        

        // 合并文件（对应button3）
        

        private void btnSelect1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                ofd.Title = "请选择第一个文本文件";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFile1.Text = ofd.FileName;
                }
            }
        }

        // 选择第二个文件（对应button2）
        private void btnSelect2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
                ofd.Title = "请选择第二个文本文件";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtFile2.Text = ofd.FileName;
                }
            }
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. 校验路径
                if (string.IsNullOrWhiteSpace(txtFile1.Text) || string.IsNullOrWhiteSpace(txtFile2.Text))
                {
                    lblResult.Text = "请先选择两个文件！";
                    return;
                }
                if (!File.Exists(txtFile1.Text) || !File.Exists(txtFile2.Text))
                {
                    lblResult.Text = "所选文件不存在！";
                    return;
                }

                // 2. 读取内容并合并
                string content1 = File.ReadAllText(txtFile1.Text);
                string content2 = File.ReadAllText(txtFile2.Text);
                string mergedContent = content1 + Environment.NewLine + "--- 分割线 ---" + Environment.NewLine + content2;

                // 3. 准备Data目录
                string exeDir = Application.StartupPath;
                string dataDir = Path.Combine(exeDir, "Data");
                if (!Directory.Exists(dataDir))
                {
                    Directory.CreateDirectory(dataDir);
                }

                // 4. 生成新文件
                string newFileName = $"merged_{DateTime.Now:yyyyMMddHHmmss}.txt";
                string newFilePath = Path.Combine(dataDir, newFileName);
                File.WriteAllText(newFilePath, mergedContent);

                lblResult.Text = $"合并成功！\n文件保存在：{newFilePath}";
            }
            catch (Exception ex)
            {
                lblResult.Text = $"出错了：{ex.Message}";
            }
        }
    }

}