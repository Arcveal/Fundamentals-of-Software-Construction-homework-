using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace 第八周作业
{
    public partial class Form1 : Form
    {
        private List<Word> wordList;
        private int currentIndex = 0;
        private WordManager wordManager;

        public Form1()
        {
            InitializeComponent();
            wordManager = new WordManager();
            wordList = wordManager.GetAllWords();

            if (wordList.Count == 0)
            {
                MessageBox.Show("单词列表为空，请检查！");
                return;
            }

            // 显示第一个单词的中文
            ShowNextWord();

            // 给 TextBox 绑定回车事件
            textBox1.KeyDown += TxtInput_KeyDown;
        }

        private void ShowNextWord()
        {
            if (currentIndex >= wordList.Count)
            {
                MessageBox.Show("所有单词已完成！");
                currentIndex = 0; // 循环从头开始
            }

            label1.Text = wordList[currentIndex].Chinese;
            textBox1.Clear();
            label2.Text = "";
            textBox1.Focus();
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckAnswer();
            }
        }

        private void CheckAnswer()
        {
            if (wordList.Count == 0) return;

            string userInput = textBox1.Text.Trim().ToLower();
            string correctAnswer = wordList[currentIndex].English.ToLower();

            if (userInput == correctAnswer)
            {
                label2.Text = "✅ 正确";
                label2.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                label2.Text = $"❌ 错误，正确答案：{correctAnswer}";
                label2.ForeColor = System.Drawing.Color.Red;
            }

            // 移到下一题
            currentIndex++;

            // 延迟1秒再显示下一题
            var timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                ShowNextWord();
            };
            timer.Start();
        }
    }
}