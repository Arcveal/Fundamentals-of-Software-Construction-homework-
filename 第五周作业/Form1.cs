using System;
using System.Windows.Forms;

namespace 第五周作业
{
    public partial class Form1 : Form
    {
        // 计算器变量
        private double firstNum = 0;
        private double secondNum = 0;
        private string operation = "";
        private bool isOperatorClicked = false;
        private bool isCalculated = false;

        public Form1()
        {
            InitializeComponent();

            // 初始化文本框样式
            textBox1.ReadOnly = true;
            textBox1.TextAlign = HorizontalAlignment.Right;
            textBox1.Font = new System.Drawing.Font("微软雅黑", 18F);
        }

        // ------------------- 数字按钮 0-9 -------------------
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    InputNumber("0");
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    InputNumber("1");
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    InputNumber("2");
        //}

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    InputNumber("3");
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    InputNumber("4");
        //}

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    InputNumber("5");
        //}

        //private void button7_Click(object sender, EventArgs e)
        //{
        //    InputNumber("6");
        //}

        //private void button8_Click(object sender, EventArgs e)
        //{
        //    InputNumber("7");
        //}

        //private void button9_Click(object sender, EventArgs e)
        //{
        //    InputNumber("8");
        //}

        //private void button10_Click(object sender, EventArgs e)
        //{
        //    InputNumber("9");
        //}

        // 输入数字的公共方法
        private void InputNumber(string num)
        {
            if (isCalculated)
            {
                textBox1.Clear();
                isCalculated = false;
                isOperatorClicked = false;
            }

            if (textBox1.Text == "0")
            {
                textBox1.Text = num;
            }
            else
            {
                textBox1.Text += num;
            }
        }

        // ------------------- 运算符按钮 + - * / -------------------
        //private void button11_Click(object sender, EventArgs e)
        //{
        //    SetOperation("+");
        //}

        //private void button12_Click(object sender, EventArgs e)
        //{
        //    SetOperation("-");
        //}

        //private void button13_Click(object sender, EventArgs e)
        //{
        //    SetOperation("*");
        //}

        //private void button14_Click(object sender, EventArgs e)
        //{
        //    SetOperation("/");
        //}

        // 设置运算符号
        private void SetOperation(string op)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                firstNum = double.Parse(textBox1.Text);
                operation = op;
                isOperatorClicked = true;
                isCalculated = false;
                textBox1.Text += " " + op + " ";
            }
        }

        // ------------------- 等于号 = -------------------
        private void button15_Click(object sender, EventArgs e)
        {
            if (isOperatorClicked == false || string.IsNullOrEmpty(operation))
                return;

            try
            {
                string[] values = textBox1.Text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length < 3) return;

                secondNum = double.Parse(values[2]);
                double result = 0;

                switch (operation)
                {
                    case "+":
                        result = firstNum + secondNum;
                        break;
                    case "-":
                        result = firstNum - secondNum;
                        break;
                    case "*":
                        result = firstNum * secondNum;
                        break;
                    case "/":
                        if (secondNum == 0)
                        {
                            textBox1.Text = "不能除以0";
                            isCalculated = true;
                            return;
                        }
                        result = firstNum / secondNum;
                        break;
                }

                // 显示 18+5=23 格式
                textBox1.Text = $"{firstNum}{operation}{secondNum}={result}";
                isCalculated = true;
                isOperatorClicked = false;
            }
            catch
            {
                textBox1.Text = "输入错误";
                isCalculated = true;
            }
        }

        // ------------------- 清除按钮 C -------------------
        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            firstNum = 0;
            secondNum = 0;
            operation = "";
            isOperatorClicked = false;
            isCalculated = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            InputNumber("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InputNumber("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InputNumber("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InputNumber("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InputNumber("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InputNumber("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InputNumber("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            InputNumber("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            InputNumber("9");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            InputNumber("0");
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            SetOperation("+");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SetOperation("-");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SetOperation("*");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SetOperation("/");
        }

    }
}