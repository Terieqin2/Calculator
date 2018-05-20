using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using LinearAlgebra;


namespace WindowsFormsApp1
{
    public partial class MyCalcultor : Form
    {
        public MyCalcultor()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }
        enum A
        {
            e_Null,
            e_Number,
            e_Plus,
            e_Minus,
            e_Divide,
            e_Multiply,
            e_Sqrt,
            e_Power,
            e_LeftParen,
            e_RightParen,
            e_Sin,
            e_Cos,
            e_Tan,
            e_Cot,
            e_Log,
            e_Lg,
            e_Ln,
            e_PI,
            e_E
        };
        const double PI = 3.14159265358979323;
        const double MathE = 2.718281828459045;
        int curIndex = 0;
        double num;
        char charTemp;
        A sym = A.e_Null;
        char getChar(string inputText)
        {
            if (curIndex == inputText.Length) return '\0';
            else
            {
                char temp = inputText[curIndex++];
                while(temp == ' ' || temp == '\n')
                {
                    temp = inputText[curIndex++];
                }
                return temp;
            }
        }
        void getSym(string inputText)
        {
            num = 0;
            if (charTemp == '\0')
            {
                sym = A.e_Null;
                return;
            }
            else if (charTemp >= 48 && charTemp <= 57)
            {
                num = num * 10 + (int)charTemp - 48;
                charTemp = getChar(inputText);
                while (charTemp >= 48 && charTemp <= 57)
                {
                    num = num * 10 + (int)charTemp - 48;
                    charTemp = getChar(inputText);
                }
                if (charTemp == '.')
                {
                    charTemp = getChar(inputText);
                    int dotIndex = 10;
                    while (charTemp >= 48 && charTemp <= 57)
                    {
                        num = num + (double)((int)charTemp - 48) / dotIndex;
                        dotIndex *= 10;
                        charTemp = getChar(inputText);
                    }
                }
                sym = A.e_Number;
            }
            else if (charTemp == '+')
            {
                charTemp = getChar(inputText);
                sym = A.e_Plus;
            }
            else if (charTemp == '-')
            {
                charTemp = getChar(inputText);
                sym = A.e_Minus;
            }
            else if (charTemp == '*')
            {
                charTemp = getChar(inputText);
                sym = A.e_Multiply;
            }
            else if (charTemp == '/')
            {
                charTemp = getChar(inputText);
                sym = A.e_Divide;
            }
            else if (charTemp == 's')
            {
                charTemp = getChar(inputText);
                if (charTemp == 'i')
                {
                    charTemp = getChar(inputText);
                    if (charTemp == 'n')
                    {
                        charTemp = getChar(inputText);
                        sym = A.e_Sin;
                    }
                    else
                    {
                        errorOutput(1);
                        sym = A.e_Null;
                    }
                }
                else if (charTemp == 'q')
                {
                    charTemp = getChar(inputText);
                    if (charTemp == 'r') charTemp = getChar(inputText);
                    else
                    {
                        errorOutput(3);
                        sym = A.e_Null;
                    }
                    if (charTemp == 't')
                    {
                        charTemp = getChar(inputText);
                        sym = A.e_Sqrt;
                    }
                    else
                    {
                        errorOutput(3);
                    }
                }
                else
                {
                    errorOutput(2);
                }

            }
            else if (charTemp == 'c')
            {
                charTemp = getChar(inputText);
                if (charTemp == 'o') charTemp = getChar(inputText);
                else
                {
                    errorOutput(4);
                    sym = A.e_Null;
                }
                if (charTemp == 's')
                {
                    charTemp = getChar(inputText);
                    sym = A.e_Cos;
                }
                else if (charTemp == 't')
                {
                    charTemp = getChar(inputText);
                    sym = A.e_Cot;
                }
                else
                {
                    errorOutput(5);
                }
            }
            else if (charTemp == 't')
            {
                charTemp = getChar(inputText);
                if (charTemp == 'a') charTemp = getChar(inputText);
                else
                {
                    errorOutput(6);
                    sym = A.e_Null;
                }
                if (charTemp == 'n')
                {
                    charTemp = getChar(inputText);
                    sym = A.e_Tan;
                }
                else
                {
                    errorOutput(3);
                    sym = A.e_Null;
                }
            }
            else if (charTemp == 'l')
            {
                charTemp = getChar(inputText);
                if (charTemp == 'o')
                {
                    charTemp = getChar(inputText);
                    if (charTemp == 'g')
                    {
                        charTemp = getChar(inputText);
                        sym = A.e_Log;
                    }
                    else
                    {
                        errorOutput(7);
                        sym = A.e_Null;
                    }
                }
                else if(charTemp =='g')
                {
                    charTemp = getChar(inputText);
                    sym = A.e_Lg;
                }
                else if(charTemp =='n')
                {
                    charTemp = getChar(inputText);
                    sym = A.e_Ln;
                }
                else
                {
                    errorOutput(16);
                    sym = A.e_Null;
                }
               
            }
            else if (charTemp == '(')
            {
                charTemp = getChar(inputText);
                sym = A.e_LeftParen;
            }
            else if (charTemp == ')')
            {
                charTemp = getChar(inputText);
                sym = A.e_RightParen;
            }
            else if (charTemp == '^')
            {
                charTemp = getChar(inputText);
                sym = A.e_Power;
            }
            else if(charTemp == 'e')
            {
                charTemp = getChar(inputText);
                sym = A.e_E;
            }
            else if(charTemp == 'p'||charTemp == 'P')
            {
                charTemp = getChar(inputText);
                if (charTemp == 'i' || charTemp == 'I')
                {
                    charTemp = getChar(inputText);
                    sym = A.e_PI;
                }
                else errorOutput(18);
            }
            else errorOutput(3);
        }
        String[] errorMessage =
        {
            "",
            "你可能想要输入sin",//1
            "不能识别的标识符",
            "你可能想要输入sqrt",
            "你可能想要输入cos",
            "你可能想要输入cot",//5
            "你可能想要输入tan",
            "你可能想要输入log",
            "^后需要跟()",
            "括号对不匹配",
            "sin,cos,sqrt,tan,cot后面需要跟()",//10
            "log后面需要跟()()",
            "非法二进制",
            "非法八进制",
            "非法十进制",
            "非法十六进制",//15
            "你可能想要输log,ln或者lg",
            "缺少括号",
            "你可能想输入Pi,PI,pI或PI"

        };
        void errorOutput(int n)
        {
            textBox16.Text = errorMessage[n];
            //MessageBox.Show(errorMessage[n]);
            
        }
        double expr1(string inputText)
        {
            double sum = 0;
            sum += expr2(inputText);
            while (sym == A.e_Plus || sym == A.e_Minus)
            {
                A symTemp = sym;
                getSym(inputText);
                double temp = expr2(inputText);
                if (symTemp == A.e_Plus) sum += temp;
                else sum -= temp;
            }
            return sum;
        }
        double expr2(string inputText)
        {
            double sum = 0;
            sum += expr3(inputText);
            while (sym == A.e_Multiply || sym == A.e_Divide)
            {
                A symTemp = sym;
                getSym(inputText);
                double temp = expr3(inputText);
                if (symTemp == A.e_Multiply) sum *= temp;
                else sum /= temp;
            }
            return sum;
        }
        double expr3(string inputText)
        {
            double sum = 0;
            if (sym == A.e_Number || sym == A.e_E || sym == A.e_PI)
            {
                if (sym == A.e_Number) sum += num;
                else if (sym == A.e_E) sum += MathE;
                else sum += PI;
                //MessageBox.Show(sum.ToString());
                getSym(inputText);
                if (sym == A.e_Power)
                {
                    getSym(inputText);
                    if (sym == A.e_LeftParen)
                    {
                        getSym(inputText);
                        double temp = expr1(inputText);
                        sum = Math.Pow(sum, temp);
                        if (sym != A.e_RightParen) errorOutput(9);
                        else getSym(inputText);
                    }
                    else errorOutput(8);
                }
            }
            else if (sym == A.e_Sin || sym == A.e_Cos || sym == A.e_Tan || sym == A.e_Cot || sym == A.e_Sqrt)
            {
                A symTemp = sym;
                getSym(inputText);
                if (sym == A.e_LeftParen)
                {
                    getSym(inputText);
                    double temp = expr1(inputText);
                    if (symTemp == A.e_Sin)
                    {
                        sum = Math.Sin(temp);
                    }
                    else if (symTemp == A.e_Cos)
                    {
                        sum = Math.Cos(temp);
                    }
                    else if (symTemp == A.e_Tan)
                    {
                        sum = Math.Tan(temp);
                    }
                    else if (symTemp == A.e_Cot)
                    {
                        sum = 1 / Math.Tan(temp);
                    }
                    else if (symTemp == A.e_Sqrt)
                    {
                        sum = Math.Sqrt(temp);
                    }
                    if (sym == A.e_RightParen) getSym(inputText);
                    else errorOutput(9);
                }
                else errorOutput(10);
            }
            else if (sym == A.e_Log)
            {
                
                getSym(inputText);
                double temp1, temp2;
                temp1 = 1;
                temp2 = 1;
                if (sym == A.e_LeftParen)
                {
                    getSym(inputText);
                    temp1 = expr1(inputText);
                    //MessageBox.Show(temp1.ToString());
                    if (sym == A.e_RightParen) getSym(inputText);
                    else errorOutput(9);
                }
                else errorOutput(11);
                if (sym == A.e_LeftParen)
                {
                    getSym(inputText);
                   // MessageBox.Show("************");
                    temp2 = expr1(inputText);
                    if (sym == A.e_RightParen) getSym(inputText);
                    else errorOutput(9);
                }
                else errorOutput(11);
                sum = Math.Log(temp2) / Math.Log(temp1);
            }
            else if(sym == A.e_Lg)
            {
                getSym(inputText);
                if (sym == A.e_LeftParen)
                {
                    getSym(inputText);
                    double temp = expr1(inputText);
                    sum += Math.Log10(temp);
                    if (sym == A.e_RightParen) getSym(inputText);
                    else errorOutput(9);
                }
                else errorOutput(17);
            }
            else if (sym == A.e_Ln)
            {
                getSym(inputText);
                if (sym == A.e_LeftParen)
                {
                    getSym(inputText);
                    double temp = expr1(inputText);
                    sum += Math.Log(temp);
                    if (sym == A.e_RightParen) getSym(inputText);
                    else errorOutput(9);
                }
                else errorOutput(17);
            }
            //Console.WriteLine(ToString(sum));
            else if (sym == A.e_LeftParen)
            {
                getSym(inputText);
                sum += expr1(inputText);
                if (sym == A.e_RightParen) getSym(inputText);
                else errorOutput(9);
                if (sym == A.e_Power)
                {
                    getSym(inputText);
                    if (sym == A.e_LeftParen)
                    {
                        getSym(inputText);
                        double temp = expr1(inputText);
                        sum = Math.Pow(sum, temp);
                        if (sym != A.e_RightParen) errorOutput(9);
                        else getSym(inputText);
                    }
                    else errorOutput(8);
                }
            }
            return sum;
        }

        private void button0_Click(object sender, EventArgs e)
        {
            var insertText = "0";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var insertText = "1";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var insertText = "2";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var insertText = "3";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var insertText = "4";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var insertText = "5";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var insertText = "6";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var insertText = "7";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var insertText = "8";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var insertText = "9";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            var insertText = ".";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            textBox16.Text = "";
            String inputText = textBox1.Text;
            curIndex = 0;
            charTemp = getChar(inputText);
            getSym(inputText);
            double sum = expr1(inputText);
            textBox2.Text = "Answer = ";
            textBox2.Text = textBox2.Text + sum.ToString();
            MatrixTest();
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            var insertText = "+";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            var insertText = "-";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            var insertText = "*";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            var insertText = "/";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + insertText.Length;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var selectionIndex = textBox1.SelectionStart;
            if (selectionIndex == 0) return;
            //			MessageBox.Show("The length of Text is: " + textBox1.Text.Length.ToString());
            //			MessageBox.Show(selectionIndex.ToString());
            textBox1.Text = textBox1.Text.Remove(selectionIndex - 1, 1);
            textBox1.SelectionStart = selectionIndex - 1;
            //			MessageBox.Show("此按键不能正常使用，请使用鼠标删除，谢谢合作！");
        }

        private void buttonPower_Click(object sender, EventArgs e)
        {
            var insertText = "^()";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + 2;
        }

        private void buttonSqrt_Click(object sender, EventArgs e)
        {
            var insertText = "sqrt()";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + 5;
        }

        private void buttonSin_Click(object sender, EventArgs e)
        {
            var insertText = "sin()";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + 4;
        }

        private void buttonCos_Click(object sender, EventArgs e)
        {
            var insertText = "cos()";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + 4;
        }

        private void buttonTan_Click(object sender, EventArgs e)
        {
            var insertText = "tan()";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + 4;
        }

        private void buttonCot_Click(object sender, EventArgs e)
        {
            var insertText = "cot()";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + 4;
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            var insertText = "log()";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + 4;
        }

        private void buttonParen_Click(object sender, EventArgs e)
        {
            var insertText = "()";
            var selectionIndex = textBox1.SelectionStart;
            textBox1.Text = textBox1.Text.Insert(selectionIndex, insertText);
            textBox1.SelectionStart = selectionIndex + 1;
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            string strTemp = DePreZero(textBox3.Text);
            if (comboBox1.Text == "2Sys")
            {
                
                textBox4.Text = strTemp;
                textBox5.Text = Convert2To8(strTemp);
                textBox6.Text = Convert2To10(strTemp);
                textBox7.Text = Convert2To16(strTemp);
            }
            else if (comboBox1.Text == "8Sys")
            {
                textBox4.Text = Convert8To2(strTemp);
                textBox5.Text = strTemp;
                textBox6.Text = Convert8To10(strTemp);
                textBox7.Text = Convert8To16(strTemp);
            }
            else if (comboBox1.Text == "10Sys")
            {
                textBox4.Text = Convert10To2(strTemp);
                textBox5.Text = Convert10To8(strTemp); ;
                textBox6.Text = strTemp;
                textBox7.Text = Convert10To16(strTemp); ;
            }
            else
            {
                textBox4.Text = Convert16To2(strTemp);
                textBox5.Text = Convert16To8(strTemp);
                textBox6.Text = Convert16To10(strTemp);
                textBox7.Text = strTemp;
            }
        }

        string Convert2To8(string inputText)
        {
            int strIndex = inputText.Length - 1;
            int dotIndex = 0;
            for (int i = 0; i < inputText.Length; i++)
            {
                if (inputText[i] != '0' && inputText[i] != '1' && inputText[i] != '.')
                {
                    errorOutput(12);
                    return null;
                }
                if (inputText[i] == '.')
                {
                    strIndex = i - 1;
                    dotIndex = i;
                }
            }
            string outputString = "";
            while (true)
            {
                if (strIndex == 0)
                {
                    if (inputText[strIndex] == '1') outputString = outputString.Insert(0, "1");
                    break;
                }
                else if (strIndex == 1)
                {
                    string strTemp = inputText.Substring(0, 2);
                    if (strTemp == "01") outputString = outputString.Insert(0, "1");
                    else if (strTemp == "10") outputString = outputString.Insert(0, "2");
                    else if (strTemp == "11") outputString = outputString.Insert(0, "3");
                    break;
                }
                else
                {
                    string strTemp = inputText.Substring(strIndex - 2, 3);
                    if (strTemp == "000")
                    {
                        if (strIndex != 2) outputString = outputString.Insert(0, "0");
                    }
                    else if (strTemp == "001") outputString = outputString.Insert(0, "1");
                    else if (strTemp == "010") outputString = outputString.Insert(0, "2");
                    else if (strTemp == "011") outputString = outputString.Insert(0, "3");
                    else if (strTemp == "100") outputString = outputString.Insert(0, "4");
                    else if (strTemp == "101") outputString = outputString.Insert(0, "5");
                    else if (strTemp == "110") outputString = outputString.Insert(0, "6");
                    else if (strTemp == "111") outputString = outputString.Insert(0, "7");
                    strIndex -= 3;
                    if (strIndex < 0) break;
                }
            }
            if (outputString == "") outputString = "0";
            if (dotIndex == 0) return outputString;
            strIndex = dotIndex + 1;
            outputString = outputString.Insert(outputString.Length, ".");
            while (true)
            {
                if (strIndex == inputText.Length - 1)
                {
                    if (inputText[strIndex] == '0') outputString = outputString.Insert(outputString.Length, "0");
                    else outputString = outputString.Insert(outputString.Length, "4");
                    break;
                }
                else if (strIndex == inputText.Length - 2)
                {
                    string strTemp = inputText.Substring(strIndex, 2);
                    if (strTemp == "00") outputString = outputString.Insert(outputString.Length, "0");
                    else if (strTemp == "01") outputString = outputString.Insert(outputString.Length, "2");
                    else if (strTemp == "10") outputString = outputString.Insert(outputString.Length, "4");
                    else outputString = outputString.Insert(outputString.Length, "6");
                    break;
                }
                else
                {
                    string strTemp = inputText.Substring(strIndex, 3);
                    strIndex += 3;
                    if (strTemp == "000") outputString = outputString.Insert(outputString.Length, "0");
                    else if (strTemp == "001") outputString = outputString.Insert(outputString.Length, "1");
                    else if (strTemp == "010") outputString = outputString.Insert(outputString.Length, "2");
                    else if (strTemp == "011") outputString = outputString.Insert(outputString.Length, "3");
                    else if (strTemp == "100") outputString = outputString.Insert(outputString.Length, "4");
                    else if (strTemp == "101") outputString = outputString.Insert(outputString.Length, "5");
                    else if (strTemp == "110") outputString = outputString.Insert(outputString.Length, "6");
                    else outputString = outputString.Insert(outputString.Length, "7");
                    if (strIndex >= inputText.Length) break;
                }
            }
            if (outputString == "") outputString = "0";
            return DePreZero(outputString);
            
            //return DePreZero(outputString);
        }
        string Convert2To10(string inputText)
        {
            int strIndex = inputText.Length - 1;
            int dotIndex = 0;
            for (int i = 0; i < inputText.Length; i++)
            {
                if (inputText[i] != '0' && inputText[i] != '1' && inputText[i] != '.')
                {
                    errorOutput(12);
                    return null;
                }
                if (inputText[i] == '.')
                {
                    strIndex = i - 1;
                    dotIndex = i;
                }
            }
            double j = 0;
            double sum = 0;
            while (strIndex >= 0)
            {
                if (inputText[strIndex] == '1') sum += Math.Pow(2, j);
                j++;
                strIndex--;
            }
            if (dotIndex == 0) return sum.ToString();
            strIndex = dotIndex + 1;
            j = 1;
            while (strIndex < inputText.Length)
            {
                if (inputText[strIndex] == '1') sum += Math.Pow(0.5, j);
                j++;
                strIndex++;
            }
            return sum.ToString();
        }
        string Convert2To16(string inputText)
        {
            int strIndex = inputText.Length - 1;
            int dotIndex = 0;
            for (int i = 0; i < inputText.Length; i++)
            {
                if (inputText[i] != '0' && inputText[i] != '1' && inputText[i] != '.')
                {
                    errorOutput(12);
                    return null;
                }
                if (inputText[i] == '.')
                {
                    strIndex = i - 1;
                    dotIndex = i;
                }
            }
            string outputString = "";
            while (true)
            {
                if (strIndex == 0)
                {
                    if (inputText[strIndex] == '1') outputString = outputString.Insert(0, "1");
                    break;
                }
                else if (strIndex == 1)
                {
                    string strTemp = inputText.Substring(0, 2);
                    if (strTemp == "01") outputString = outputString.Insert(0, "1");
                    else if (strTemp == "10") outputString = outputString.Insert(0, "2");
                    else if (strTemp == "11") outputString = outputString.Insert(0, "3");
                    break;
                }
                else if (strIndex == 2)
                {
                    string strTemp = inputText.Substring(0, 3);
                    if (strTemp == "000")
                    {
                        if (strIndex != 2) outputString = outputString.Insert(0, "0");
                    }
                    else if (strTemp == "001") outputString = outputString.Insert(0, "1");
                    else if (strTemp == "010") outputString = outputString.Insert(0, "1");
                    else if (strTemp == "011") outputString = outputString.Insert(0, "3");
                    else if (strTemp == "100") outputString = outputString.Insert(0, "4");
                    else if (strTemp == "101") outputString = outputString.Insert(0, "5");
                    else if (strTemp == "110") outputString = outputString.Insert(0, "6");
                    else if (strTemp == "111") outputString = outputString.Insert(0, "7");
                    break;
                }
                else
                {
                    string strTemp = inputText.Substring(strIndex - 3, 4);
                    if (strTemp == "0000")
                    {
                        if (strIndex != 3) outputString = outputString.Insert(0, "0");
                    }
                    else if (strTemp == "0001") outputString = outputString.Insert(0, "1");
                    else if (strTemp == "0010") outputString = outputString.Insert(0, "2");
                    else if (strTemp == "0011") outputString = outputString.Insert(0, "3");
                    else if (strTemp == "0100") outputString = outputString.Insert(0, "4");
                    else if (strTemp == "0101") outputString = outputString.Insert(0, "5");
                    else if (strTemp == "0110") outputString = outputString.Insert(0, "6");
                    else if (strTemp == "0111") outputString = outputString.Insert(0, "7");
                    else if (strTemp == "1000") outputString = outputString.Insert(0, "8");
                    else if (strTemp == "1001") outputString = outputString.Insert(0, "9");
                    else if (strTemp == "1010") outputString = outputString.Insert(0, "A");
                    else if (strTemp == "1011") outputString = outputString.Insert(0, "B");
                    else if (strTemp == "1100") outputString = outputString.Insert(0, "C");
                    else if (strTemp == "1101") outputString = outputString.Insert(0, "D");
                    else if (strTemp == "1110") outputString = outputString.Insert(0, "E");
                    else outputString = outputString.Insert(0, "F");
                    strIndex -= 4;
                    if (strIndex < 0) break;
                }
            }
            if (outputString == "") outputString = "0";
            if (dotIndex == 0) return outputString;
            strIndex = dotIndex + 1;
            outputString = outputString.Insert(outputString.Length, ".");
            while (true)
            {
                if (strIndex == inputText.Length - 1)
                {
                    if (inputText[strIndex] == '0') outputString = outputString.Insert(outputString.Length, "0");
                    else outputString = outputString.Insert(outputString.Length, "8");
                    break;
                }
                else if (strIndex == inputText.Length - 2)
                {
                    string strTemp = inputText.Substring(strIndex, 2);
                    if (strTemp == "00") outputString = outputString.Insert(outputString.Length, "0");
                    else if (strTemp == "01") outputString = outputString.Insert(outputString.Length, "4");
                    else if (strTemp == "10") outputString = outputString.Insert(outputString.Length, "8");
                    else outputString = outputString.Insert(outputString.Length, "12");
                    break;
                }
                else if (strIndex == inputText.Length - 3)
                {
                    string strTemp = inputText.Substring(strIndex, 3);
                    strIndex += 3;
                    if (strTemp == "000") outputString = outputString.Insert(outputString.Length, "0");
                    else if (strTemp == "001") outputString = outputString.Insert(outputString.Length, "2");
                    else if (strTemp == "010") outputString = outputString.Insert(outputString.Length, "4");
                    else if (strTemp == "011") outputString = outputString.Insert(outputString.Length, "6");
                    else if (strTemp == "100") outputString = outputString.Insert(outputString.Length, "8");
                    else if (strTemp == "101") outputString = outputString.Insert(outputString.Length, "A");
                    else if (strTemp == "110") outputString = outputString.Insert(outputString.Length, "C");
                    else outputString = outputString.Insert(outputString.Length, "E");
                    break;
                }
                else
                {
                    string strTemp = inputText.Substring(strIndex, 4);
                    if (strTemp == "0000") outputString = outputString.Insert(outputString.Length, "0");
                    else if (strTemp == "0001") outputString = outputString.Insert(outputString.Length, "1");
                    else if (strTemp == "0010") outputString = outputString.Insert(outputString.Length, "2");
                    else if (strTemp == "0011") outputString = outputString.Insert(outputString.Length, "3");
                    else if (strTemp == "0100") outputString = outputString.Insert(outputString.Length, "4");
                    else if (strTemp == "0101") outputString = outputString.Insert(outputString.Length, "5");
                    else if (strTemp == "0110") outputString = outputString.Insert(outputString.Length, "6");
                    else if (strTemp == "0111") outputString = outputString.Insert(outputString.Length, "7");
                    else if (strTemp == "1000") outputString = outputString.Insert(outputString.Length, "8");
                    else if (strTemp == "1001") outputString = outputString.Insert(outputString.Length, "9");
                    else if (strTemp == "1010") outputString = outputString.Insert(outputString.Length, "A");
                    else if (strTemp == "1011") outputString = outputString.Insert(outputString.Length, "B");
                    else if (strTemp == "1100") outputString = outputString.Insert(outputString.Length, "C");
                    else if (strTemp == "1101") outputString = outputString.Insert(outputString.Length, "D");
                    else if (strTemp == "1110") outputString = outputString.Insert(outputString.Length, "E");
                    else outputString = outputString.Insert(outputString.Length, "F");
                    strIndex += 4;
                    if (strIndex >= inputText.Length) break;
                }
            }
            if (outputString == "") outputString = "0";
            return DePreZero(outputString);
            
        }
        string Convert8To2(string inputText)
        {
            int strIndex = inputText.Length - 1;
            int dotIndex = 0;
            for (int i = 0; i < inputText.Length - 1; i++)
            {
                if ((inputText[i] < 48 || inputText[i] > 55) && inputText[i] != '.')
                {
                    errorOutput(13);
                    return null;
                }
                if (inputText[i] == '.')
                {
                    dotIndex = i;
                    strIndex = i - 1;
                }
            }
            string outputText = "";
            while (true)
            {
                if (inputText[strIndex] == '0') outputText = outputText.Insert(0, "000");
                else if (inputText[strIndex] == '1') outputText = outputText.Insert(0, "001");
                else if (inputText[strIndex] == '2') outputText = outputText.Insert(0, "010");
                else if (inputText[strIndex] == '3') outputText = outputText.Insert(0, "011");
                else if (inputText[strIndex] == '4') outputText = outputText.Insert(0, "100");
                else if (inputText[strIndex] == '5') outputText = outputText.Insert(0, "101");
                else if (inputText[strIndex] == '6') outputText = outputText.Insert(0, "110");
                else outputText = outputText.Insert(0, "111");
                strIndex--;
                if (strIndex < 0) break;
            }
            int realIndex = 0;
            for (; realIndex < outputText.Length; realIndex++)
            {
                if (outputText[realIndex] != '0') break;
            }
            outputText = outputText.Substring(realIndex, outputText.Length - realIndex - 1);
            if (dotIndex == 0) return outputText;
            strIndex = dotIndex + 1;
            outputText = outputText.Insert(outputText.Length, ".");
            while (true)
            {
                if (inputText[strIndex] == '0')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "000");
                }
                else if (inputText[strIndex] == '1') outputText = outputText.Insert(outputText.Length, "001");
                else if (inputText[strIndex] == '2')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "010");
                    else outputText = outputText.Insert(outputText.Length, "01");
                }
                else if (inputText[strIndex] == '3') outputText = outputText.Insert(outputText.Length, "011");
                else if (inputText[strIndex] == '4')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "100");
                    else outputText = outputText.Insert(outputText.Length, "1");
                }
                else if (inputText[strIndex] == '5') outputText = outputText.Insert(outputText.Length, "101");
                else if (inputText[strIndex] == '6')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "110");
                    else outputText = outputText.Insert(outputText.Length, "11");
                }
                else outputText = outputText.Insert(outputText.Length, "111");
                strIndex++;
                if (strIndex >= inputText.Length) break;
            }
            return DePreZero(outputText);
        }
        string Convert8To10(string inputText)
        {
            int strIndex = inputText.Length - 1;
            int dotIndex = 0;
            for (int i = 0; i < inputText.Length; i++)
            {
                if ((inputText[i] < 48 || inputText[i] > 55) && inputText[i] != '.')
                {
                    errorOutput(13);
                    return null;
                }
                if (inputText[i] == '.')
                {
                    strIndex = i - 1;
                    dotIndex = i;
                }
            }
            double j = 0;
            double sum = 0;
            while (strIndex >= 0)
            {
                double temp1 = (double)(inputText[strIndex]-48);
                if (temp1 !=0) sum +=  temp1*Math.Pow(8, j);
                j++;
                strIndex--;
            }
            if (dotIndex == 0) return sum.ToString();
            strIndex = dotIndex + 1;
            j = 1;
            double temp = 1.0 / 8.0;
            while (strIndex < inputText.Length)
            {
                double temp1 = (double)(inputText[strIndex]-48);
                if (temp1 != '0') sum += temp1*Math.Pow(temp, j);
                j++;
                strIndex++;
            }
            return sum.ToString();
        }
        string Convert8To16(string inputText)
        {
            string outputTemp = Convert8To2(inputText);
            return Convert2To16(outputTemp);
        }
        string Convert10To2(string inputText)
        {
            int strIndex = inputText.Length - 1;
            int dotIndex = 0;
            int i;
            for (i = 0; i < inputText.Length; i++)
            {
                if ((inputText[i] < 48 || inputText[i] > 57) && inputText[i] != '.')
                {
                    errorOutput(14);
                    return null;
                }
                if (inputText[i] == '.')
                {
                    dotIndex = i;
                    strIndex = i - 1;
                }
            }
            int inputNumInt = 0;
            double inputNumDec = 0;
            for (i = strIndex; i >= 0; i--)
            {
                inputNumInt += (int)(inputText[i] - 48) * (int)Math.Pow(10, strIndex - i);
            }
            if (dotIndex != 0)
            {
                strIndex = dotIndex + 1;
                double j = 1;
                for (i = strIndex; i < inputText.Length; i++)
                {
                    inputNumDec += (double)(inputText[i] - 48) * Math.Pow(0.1, j);
                    j++;
                }
            }
            //			MessageBox.Show("int = "+inputNumInt+" dec = "+inputNumDec);
            string outputText = "";
            while (true)
            {
                if (inputNumInt % 2 == 0) outputText = outputText.Insert(0, "0");
                else outputText = outputText.Insert(0, "1");
                inputNumInt /= 2;
                if (inputNumInt == 0) break;
            }
            if (dotIndex == 0) return outputText;
            outputText = outputText.Insert(outputText.Length, ".");
            int count = 0;
            while (true)
            {
                inputNumDec *= 2;
                if (inputNumDec >= 1.0)
                {
                    outputText = outputText.Insert(outputText.Length, "1");
                    inputNumDec -= 1.0;
                }
                else outputText = outputText.Insert(outputText.Length, "0");
                count++;
                if (inputNumDec == 0.0 || count > 13) break;
            }
            i = outputText.Length - 1;
            while (true)
            {
                if (outputText[i] == '.' || outputText[i] == '1') break;
                i--;
            }
            outputText = outputText.Substring(0, i + 1);
            //			MessageBox.Show("i = "+ i);
            return DePreZero(outputText);
        }
        string Convert10To8(string inputText)
        {
            string strTemp = Convert10To2(inputText);
            return Convert2To8(strTemp);
        }
        string Convert10To16(string inputText)
        {
            string strTemp = Convert10To2(inputText);
            return Convert2To16(strTemp);
        }
        string Convert16To2(string inputText)
        {
            inputText = inputText.ToUpper();
            int strIndex = inputText.Length - 1;
            int dotIndex = 0;
            for (int i = 0; i < inputText.Length - 1; i++)
            {
                if ((inputText[i] < 48 || (inputText[i] > 57 && inputText[i] < 65) || inputText[i] > 90) && inputText[i] != '.')
                {
                    errorOutput(15);
                    return null;
                }
                if (inputText[i] == '.')
                {
                    dotIndex = i;
                    strIndex = i - 1;
                }
            }
            string outputText = "";
            while (true)
            {
                if (inputText[strIndex] == '0') outputText = outputText.Insert(0, "0000");
                else if (inputText[strIndex] == '1') outputText = outputText.Insert(0, "0001");
                else if (inputText[strIndex] == '2') outputText = outputText.Insert(0, "0010");
                else if (inputText[strIndex] == '3') outputText = outputText.Insert(0, "0011");
                else if (inputText[strIndex] == '4') outputText = outputText.Insert(0, "0100");
                else if (inputText[strIndex] == '5') outputText = outputText.Insert(0, "0101");
                else if (inputText[strIndex] == '6') outputText = outputText.Insert(0, "0110");
                else if (inputText[strIndex] == '7') outputText = outputText.Insert(0, "0111");
                else if (inputText[strIndex] == '8') outputText = outputText.Insert(0, "1000");
                else if (inputText[strIndex] == '9') outputText = outputText.Insert(0, "1001");
                else if (inputText[strIndex] == 'A') outputText = outputText.Insert(0, "1010");
                else if (inputText[strIndex] == 'B') outputText = outputText.Insert(0, "1011");
                else if (inputText[strIndex] == 'C') outputText = outputText.Insert(0, "1100");
                else if (inputText[strIndex] == 'D') outputText = outputText.Insert(0, "1101");
                else if (inputText[strIndex] == 'E') outputText = outputText.Insert(0, "1110");
                else outputText = outputText.Insert(0, "1111");
                strIndex--;
                if (strIndex < 0) break;
            }
            int realIndex = 0;
            for (; realIndex < outputText.Length; realIndex++)
            {
                if (outputText[realIndex] != '0') break;
            }
            outputText = outputText.Substring(realIndex, outputText.Length - realIndex - 1);
            if (dotIndex == 0) return outputText;
            strIndex = dotIndex + 1;
            outputText = outputText.Insert(outputText.Length, ".");
            while (true)
            {
                if (inputText[strIndex] == '0')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "0000");
                }
                else if (inputText[strIndex] == '1') outputText = outputText.Insert(outputText.Length, "0001");
                else if (inputText[strIndex] == '2')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "0010");
                    else outputText = outputText.Insert(outputText.Length, "001");
                }
                else if (inputText[strIndex] == '3') outputText = outputText.Insert(outputText.Length, "0110");
                else if (inputText[strIndex] == '4')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "0100");
                    else outputText = outputText.Insert(outputText.Length, "01");
                }
                else if (inputText[strIndex] == '5') outputText = outputText.Insert(outputText.Length, "0101");
                else if (inputText[strIndex] == '6')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "0110");
                    else outputText = outputText.Insert(outputText.Length, "011");
                }
                else if (inputText[strIndex] == '7') outputText = outputText.Insert(outputText.Length, "0111");
                else if (inputText[strIndex] == '8')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "1000");
                    else outputText = outputText.Insert(outputText.Length, "1");
                }
                else if (inputText[strIndex] == '9') outputText = outputText.Insert(outputText.Length, "1001");
                else if (inputText[strIndex] == 'A')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "1010");
                    else outputText = outputText.Insert(outputText.Length, "101");
                }
                else if (inputText[strIndex] == 'B') outputText = outputText.Insert(outputText.Length, "1011");
                else if (inputText[strIndex] == 'C')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "1100");
                    else outputText = outputText.Insert(outputText.Length, "11");
                }
                else if (inputText[strIndex] == 'D') outputText = outputText.Insert(outputText.Length, "1101");
                else if (inputText[strIndex] == 'E')
                {
                    if (strIndex != inputText.Length - 1) outputText = outputText.Insert(outputText.Length, "1110");
                    else outputText = outputText.Insert(outputText.Length, "111");
                }
                else outputText = outputText.Insert(outputText.Length, "1111");
                strIndex++;
                if (strIndex >= inputText.Length) break;
            }
            return DePreZero(outputText);
        }
        string Convert16To8(string inputText)
        {
            string strTemp = Convert16To2(inputText);
            return Convert2To8(strTemp);
        }
        string Convert16To10(string inputText)
        {
            string strTemp = Convert16To2(inputText);
            return Convert2To10(strTemp);
        }

        string DePreZero(string inputText)
        {
            string strTemp = inputText;
            int i = 0;
            for(;i<strTemp.Length;i++)
            {
                if (strTemp[i] != '0') break;
            }
            if (i == strTemp.Length)
            {
                return "0";
            }
            strTemp = strTemp.Substring(i);
            //MessageBox.Show("strTemp = " + strTemp);
            if (strTemp[0] == '.') strTemp = strTemp.Insert(0, "0");
            return strTemp;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonPlot_Click(object sender, EventArgs e)
        {
            Form2 formTemp = new Form2();
            formTemp.Show();
            formTemp.textBox3.Text = "y = " + textBox12.Text;
            Series s1 = new Series();
            double a = Convert.ToDouble(textBox13.Text);
            double b = Convert.ToDouble(textBox14.Text);
            //Console.WriteLine("a=" + a.ToString());
            //Console.WriteLine("b=" + b.ToString());
            double b_a = b - a;
            for(int i=0;i<1000;i++)
            {
                double x = a+(double)i * (b_a) / 1000.0;
                //Console.WriteLine(x.ToString());
                double y = AnalyseFunction(textBox12.Text, x.ToString());
                Console.WriteLine(y.ToString());
                s1.Points.AddXY(x, y);
            }
            double xMin = Convert.ToDouble(textBox13.Text);
            double xMax = Convert.ToDouble(textBox14.Text);
            formTemp.chartAddSeries(s1,xMin,xMax);
        }
        public double AnalyseFunction(string inputText,string NumX)
        {
            string strTemp = inputText;
            NumX = NumX.Insert(0, "(");
            NumX = NumX.Insert(NumX.Length, ")");
            strTemp = strTemp.Replace("x", NumX);
            curIndex = 0;
            charTemp = getChar(strTemp);
            getSym(strTemp);
            return expr1(strTemp);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox16.Text = "";
            String inputText = textBox1.Text;
            curIndex = 0;
            charTemp = getChar(inputText);
            getSym(inputText);
            double sum = expr1(inputText);
            textBox2.Text = "Answer = ";
            textBox2.Text = textBox2.Text + sum.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if(dr == System.Windows.Forms.DialogResult.OK)
            {
                textBox17.Text = openFileDialog1.FileName;
            }
        }

        // Plot button of interpolation
        private void button13_Click(object sender, EventArgs e)
        {
            double[] x = new double[1000];
            double[] y = new double[1000];
            int dataNum = 0;
            int i = 0;
            string allText;
            if (textBox17.Text != "")
            {
                string fileName = textBox17.Text;
                if(File.Exists(fileName) == true)allText = File.ReadAllText(fileName);
                else
                {
                    MessageBox.Show("File not exist or the format of filePath is wrong");
                    return;
                }
                //MessageBox.Show(allText);
                
                //for(i=0;i<dataNum;i++)
                //{
                //    MessageBox.Show(x[i].ToString() + " " + y[i].ToString());
                //}
                
            }
            else
            {
                allText = textBox15.Text;
                //MessageBox.Show(allText);
            }
            allText = allText.Replace("\r\n", " ");
            string[] datas = allText.Split();
            int flag = 0;
            foreach (string data in datas)
            {
                if (flag == 0)
                {
                    x[i] = Convert.ToDouble(data);
                    flag = 1;
                }
                else
                {
                    y[i] = Convert.ToDouble(data);
                    flag = 0;
                    i++;
                    dataNum++;
                }
            }
            double[] coefficient = interpolation(x, y, dataNum);
            //foreach (double temp in coefficient) MessageBox.Show(temp.ToString());
            string func = "";
            for (i = 0; i < coefficient.Length; i++)
            {
                if (coefficient[i] == 0) continue;
                if (i == 0) func = func + coefficient[i].ToString();
                else
                {
                    if (coefficient[i] > 0) func = func + "+";
                    func = func + coefficient[i].ToString();
                    func = func + "*x^(";
                    func = func + i.ToString();
                    func = func + ")";
                }
            }
            //MessageBox.Show(func);

            Form2 formTemp = new Form2();
            formTemp.Show();
            formTemp.textBox3.Text = "y = " + func;
            Series s1 = new Series();
            double a = Convert.ToDouble(textBox18.Text);
            double b = Convert.ToDouble(textBox19.Text);
            //Console.WriteLine("a=" + a.ToString());
            //Console.WriteLine("b=" + b.ToString());
            double b_a = b - a;
            for (i = 0; i < 1000; i++)
            {
                double x1 = a + (double)i * (b_a) / 1000.0;
                //Console.WriteLine(x.ToString());
                double y1 = AnalyseFunction(func, x1.ToString());
                Console.WriteLine(y1.ToString());
                s1.Points.AddXY(x1, y1);
            }
            double xMin = Convert.ToDouble(textBox13.Text);
            double xMax = Convert.ToDouble(textBox14.Text);
            formTemp.chartAddSeries(s1, xMin, xMax);
        }
        public double[] interpolation(double[] x, double[] y, int dataLength)
        {
            if (dataLength == 0)
            {
                return x;
            }
            int n = dataLength - 1;
            //   Console.WriteLine(n);
            double[] g = new double[n + 1];//就是课本里那个g
            double[] f = new double[n + 1];//求的多项式
            double[] t = new double[n + 1];//课本里那个t
            int i, j, k;
            for (i = 0; i <= n; i++)
                g[i] = y[i];
            for (k = 1; k <= n; k++)
                for (j = n; j >= k; j--)
                    if (x[j] == x[j - k])
                    {
                        return x;
                    }
                    else
                        g[j] = (g[j] - g[j - 1]) / (x[j] - x[j - k]);
            t[0] = 1;
            f[0] = g[0];

            for (i = 1; i <= n; i++)
            {
                t[i] = 1;
                for (j = i - 1; j > 0; j--)
                    t[j] = -t[j] * x[i - 1] + t[j - 1];
                t[0] = 0 - t[0] * x[i - 1];
                for (k = i; k >= 0; k--)
                    f[k] = f[k] + t[k] * g[i];
            }
            /*          Console.Read();
                        foreach (double z in f)
                            Console.WriteLine(z);*/
            return f;

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        public void Integral(double upperLimit, double floor, string func)
        {
            double sum = 0;
            if (upperLimit == floor)
            {
                textBox23.Text = "";
                textBox23.Text = "Answer = 0";
                return;
            }
            double _floor, _upperLimit;
            if (floor < upperLimit)
            {
                _floor = floor;
                _upperLimit = upperLimit;
            }
            else
            {
                _floor = upperLimit;
                _upperLimit = floor;
            }
            double interval = _upperLimit - _floor;
            double x = _floor;
            double temp = (interval / 10000);
            for (x = _floor;x<=_upperLimit;x+=temp)
            {
                sum += temp*AnalyseFunction(func, x.ToString());
                
            }
            textBox23.Text = "";
            textBox23.Text = "Answer = " + sum.ToString();
            return;
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
        }

        private void button14_Click(object sender, EventArgs e)
        {
            double upperLimit = Convert.ToDouble(textBox20.Text);
            double floor = Convert.ToDouble(textBox21.Text);
            Integral(upperLimit, floor, textBox22.Text);
        }

        private void MatrixTest()
        {
            Matrix B = new double[,] { { 5, 6 }, { 7, 8 } };
            Matrix A = new double[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            //Console.WriteLine(A);
            //Console.WriteLine(B);
            textBox28.Text = A.ToString();
            
        }


        string[] MatrixName = new string[50];
        Matrix[] matrixs = new Matrix[1000];
        int MatrixNum = 0;
        private void buttonBound_Click(object sender, EventArgs e)
        {
            
            if (textBox24.Text == "" || textBox29.Text == "" || textBox30.Text == "")
            {
                MessageBox.Show("请输入一个矩阵和它的规模，列之间用空格隔开，行之间用回车隔开");
                return;
            }
            int row = Convert.ToInt32(textBox30.Text);
            int col = Convert.ToInt32(textBox30.Text);
            
            string matrixTemp = textBox24.Text;
            matrixTemp = matrixTemp.Replace("\r\n", " ");
            string[] matrixTemps = matrixTemp.Split();
            if(matrixTemps.Length != row*col)
            {
                MessageBox.Show("矩阵大小和设定的不一致");
                return;
            }
            Matrix A = new double[row, col];
            for(int i =0;i<row;i++)
            {
                for(int j=0;j<col;j++)
                {
                    A[i,j] = Convert.ToDouble(matrixTemps[i*row+j]);
                }
            }
            if(textBox25.Text == "")
            {
                MessageBox.Show("需要绑定的矩阵名称不能为空");
                return;
            }
            for(int i = 0;i<MatrixNum-1;i++)
            {
                if(textBox25.Text == MatrixName[i])
                {
                    MessageBox.Show("矩阵名称不能重复定义");
                    return;
                }
            }
            //记录绑定的矩阵名称
            MatrixNum++;
            MatrixName[MatrixNum] = textBox25.Text;
            // 打印已经记录的矩阵
            if (textBox26.Text == "") textBox26.Text += textBox25.Text + "=\r\n"+A.ToString();
            else textBox26.Text += "\r\n" + textBox25.Text + "=\r\n" + A.ToString();
            //Matrix C;
            //C = A;
            //记录矩阵
            matrixs[MatrixNum] = new Matrix(A);
            
            //textBox28.Text = matrixs[MatrixNum].ToString();            
            
            return;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            MatrixName[0] = "";
            textBox29.Text = "2";
            textBox30.Text = "2";
            textBox24.Text = "1 2 3 4";
            textBox25.Text = "A";
            textBox26.Text = "";
            textBox27.Text = "";
            textBox28.Text = "";
            MatrixNum = 0;
            //textBox31.Text = "";
        }

        enum B
        {
            e_M_NULL,
            e_M_Plus,
            e_M_Minus,
            e_M_Mul,
            e_M_Inverse,
            e_M_LParen,
            e_M_RParen,
            e_M_Identifier
        };

        B symB = B.e_M_NULL;
        string nameTemp = "";

        void getSymB(string inputText)
        {
            if (charTemp == '+')
            {
                symB = B.e_M_Plus;
                charTemp = getChar(inputText);
                nameTemp = "";
            }
            else if(charTemp == '-')
            {
                symB = B.e_M_Minus;
                charTemp = getChar(inputText);
                nameTemp = "";
            }
            else if(charTemp == '*')
            {
                symB = B.e_M_Mul;
                charTemp = getChar(inputText);
                nameTemp = "";
            }
            else if(charTemp == 'T')
            {
                symB = B.e_M_Inverse;
                charTemp = getChar(inputText);
                nameTemp = "";
            }
            else if(charTemp =='(')
            {
                symB = B.e_M_LParen;
                charTemp = getChar(inputText);
                nameTemp = "";
            }
            else if(charTemp == ')')
            {
                symB = B.e_M_RParen;
                charTemp = getChar(inputText);
                nameTemp = "";
            }
            else
            {
                nameTemp = nameTemp.Insert(nameTemp.Length, charTemp.ToString());
                if (curIndex == inputText.Length)
                {
                    symB = B.e_M_Identifier;
                    return;
                }
                charTemp = getChar(inputText);
                while(charTemp != '+' && charTemp != '-' && charTemp != '*' && charTemp != 'T' && charTemp != '(' && charTemp != ')' )
                {
                    nameTemp = nameTemp.Insert(nameTemp.Length, charTemp.ToString());
                    if (curIndex == inputText.Length)
                    {
                        symB = B.e_M_Identifier;
                        return;
                    }
                    charTemp = getChar(inputText);                   
                }
                symB = B.e_M_Identifier;
            }
            //textBox31.Text = textBox31.Text + "\r\n" + symB.ToString();
        }
        

        public Matrix MatrixExpression_1(string inputText)
        {
            Matrix sum = MatrixExpression_2(inputText);
            while(symB == B.e_M_Plus || symB == B.e_M_Minus)
            {
                B symBTemp = symB;
                getSymB(inputText);
                
                Matrix sum2 = MatrixExpression_2(inputText);
                if(symBTemp == B.e_M_Plus)
                {
                    sum = sum + sum2;
                }
                else
                {
                    sum = sum - sum2;
                }
            }
            return sum;
        }
        public Matrix MatrixExpression_2(string inputText)
        {
            Matrix sum = MatrixExpression_3(inputText);
            while (symB == B.e_M_Mul)
            {
                B symBTemp = symB;
                getSymB(inputText);
                
                Matrix sum2 = MatrixExpression_3(inputText);
                sum = sum * sum2;
            }
            return sum;
        }
        public Matrix MatrixExpression_3(string inputText)
        {
            Matrix sum;
            if(symB == B.e_M_Identifier)
            {
                
                int p = position();
                if(p==0)
                {
                    MessageBox.Show("未定义的矩阵标识符");
                    return null;
                }
                getSymB(inputText);
                sum = matrixs[p];
            }
            else if(symB == B.e_M_LParen)
            {
                getSymB(inputText);
                sum = MatrixExpression_1(inputText);
                if(symB != B.e_M_RParen)
                {
                    MessageBox.Show("括号对不匹配");
                }
                getSymB(inputText);
            }
            else
            //if(symB == B.e_M_Inverse)
            {
                getSymB(inputText);
                if(symB!=B.e_M_LParen)
                {
                    MessageBox.Show("转置运算符后面需要跟一对括号");
                    return null;
                }
                getSymB(inputText);
                sum = MatrixExpression_1(inputText);
                if (symB != B.e_M_RParen)
                {
                    MessageBox.Show("括号对不匹配");
                    return null;
                }
                getSymB(inputText);
                sum = sum.Inverse();
            }
            return sum;
        }

        int position()
        {
            for(int i =1;i<=MatrixNum;i++)
            {
                if (nameTemp == MatrixName[i]) return i;
            }
            return 0;
        }


        private Matrix textm()
        {
            Matrix A = new double[2, 2] { { 1, 2 }, { 3, 4 } };
            return A;
        }
        private void buttonMatrixEqual_Click(object sender, EventArgs e)
        {
            curIndex = 0;
            nameTemp = "";
            charTemp = getChar(textBox27.Text);
            getSymB(textBox27.Text);
            //MessageBox.Show(symB.ToString());
            //getSymB(textBox27.Text);
            //MessageBox.Show(symB.ToString());
            //getSymB(textBox27.Text);
            //MessageBox.Show(symB.ToString());
            Matrix sum;
            sum = MatrixExpression_1(textBox27.Text);
            //Matrix sum;
            //sum= textm();
            textBox28.Text = sum.ToString();
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyCode == Keys.Enter)
            //{
            //    textBox3.Text = textBox3.Text.Substring(0, textBox3.Text.Length - 1);
            //    string strTemp = DePreZero(textBox3.Text);
            //    if (comboBox1.Text == "2Sys")
            //    {

            //        textBox4.Text = strTemp;
            //        textBox5.Text = Convert2To8(strTemp);
            //        textBox6.Text = Convert2To10(strTemp);
            //        textBox7.Text = Convert2To16(strTemp);
            //    }
            //    else if (comboBox1.Text == "8Sys")
            //    {
            //        textBox4.Text = Convert8To2(strTemp);
            //        textBox5.Text = strTemp;
            //        textBox6.Text = Convert8To10(strTemp);
            //        textBox7.Text = Convert8To16(strTemp);
            //    }
            //    else if (comboBox1.Text == "10Sys")
            //    {
            //        textBox4.Text = Convert10To2(strTemp);
            //        textBox5.Text = Convert10To8(strTemp); ;
            //        textBox6.Text = strTemp;
            //        textBox7.Text = Convert10To16(strTemp); ;
            //    }
            //    else
            //    {
            //        textBox4.Text = Convert16To2(strTemp);
            //        textBox5.Text = Convert16To8(strTemp);
            //        textBox6.Text = Convert16To10(strTemp);
            //        textBox7.Text = strTemp;
            //    }
            //}
        }
    }
}
