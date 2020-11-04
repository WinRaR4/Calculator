using System;
using System.Windows;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        bool dotted, signed, memoryMode, beenUndotted, undefinedValue = false;
        enum operation {plus, minus, multiply, div, power};
        public MainWindow()
        {
            InitializeComponent();
        }

        public Boolean IsNumeric()
        {
            bool isNumeric = Char.IsDigit(lol.Text[lol.Text.Length - 1]);
            return isNumeric;
        }

        public Boolean IsOperand (int t)
        {
            bool a;
            if (lol.Text[t] == '＋' || lol.Text[t] == '－' || lol.Text[t] == '／' || lol.Text[t] == 'Ｘ' || lol.Text[t] == '^')
                a = true;
            else
                a = false;
            return a;
        }

        private void setBracket()
        {
            if (lol.Text[lol.Text.Length - 1] == '.') lol.Text = lol.Text.Remove(lol.Text.Length - 1);
            lol.Text = lol.Text.Insert(lol.Text.Length, ")");
        }
        private void Sign(object sender, RoutedEventArgs e)
        {
            if (signed) signed = false; else signed = true;
            for (int i = lol.Text.Length - 1; i >= 0; i--)
            {
                if (IsOperand(i))
                {
                    if (i < lol.Text.Length - 1) {
                        if (lol.Text[i + 1] != '(')
                        {
                            lol.Text = lol.Text.Insert(i + 1, "-");
                            lol.Text = lol.Text.Insert(i + 1, "(");
                        }
                        else
                        {
                            if (lol.Text[i + 1] == '(') lol.Text = lol.Text.Remove(i + 1, 2);
                        }
                    } else
                    {
                        lol.Text = lol.Text.Insert(i + 1, "-");
                        lol.Text = lol.Text.Insert(i + 1, "(");
                    }
                    break;
                } else
                    if (i == 0)
                {
                    signed = false;
                    if (lol.Text[i] != '-') lol.Text = lol.Text.Insert(i, "-");
                    else lol.Text = lol.Text.Remove(i, 1);
                    break;
                }
            }
        }

        public void AddOperand(char operand)
        {
            if (undefinedValue)
            {
                lol.Text = "0";
                dotted = signed = beenUndotted = undefinedValue = false;
            }
            if (lol.Text[lol.Text.Length - 1] == '-') lol.Text += "0";
            if (dotted && lol.Text[lol.Text.Length - 1] != '.') beenUndotted = true; else beenUndotted = false;
            if (signed)
            {
                setBracket();
                signed = false;
            }
            if (IsNumeric()) lol.Text += "＋";
            if (IsOperand(lol.Text.Length - 1) || lol.Text[lol.Text.Length - 1] == '.')
            {
                removeLast();
                lol.Text += operand;
            }
            else lol.Text += operand;
            dotted = false;
        }

        private void Plus(object sender, RoutedEventArgs e)
        {
            AddOperand('＋');
        }
        private void Minus(object sender, RoutedEventArgs e)
        {
            AddOperand('－');
        }
        private void Div(object sender, RoutedEventArgs e)
        {
            AddOperand('／');
        }
        private void Multiply(object sender, RoutedEventArgs e)
        {
            AddOperand('Ｘ');
        }
        private void Power(object sender, RoutedEventArgs e)
        {
            AddOperand('^');
        }
        private void Dot(object sender, RoutedEventArgs e)
        {
            if (lol.Text[lol.Text.Length - 1] == '.')
            {
                lol.Text = lol.Text.Remove(lol.Text.Length - 1);
                dotted = false;
            }
            else if (IsNumeric() && !dotted)
            {
                lol.Text += ".";
                dotted = true;
            }
        }

        public void AddThis(String digit)
        {
            if (lol.Text.Length == 1 && lol.Text[0] == '0') lol.Text = digit;
            else if (lol.Text[lol.Text.Length - 1] == '0' && (IsOperand(lol.Text.Length - 2)))
            {
                lol.Text = lol.Text.Remove(lol.Text.Length - 1);
                lol.Text += digit;
            }
            else lol.Text += digit;
        }
        public void AddNumber(String digit)
        {
            if (undefinedValue)
            {
                lol.Text = digit;
                dotted = signed = beenUndotted = undefinedValue = false;
            }
            if (lol.Text.Length > 1)
            {
                if (lol.Text[lol.Text.Length - 1] != '0' || lol.Text[lol.Text.Length - 2] != '-')
                    AddThis(digit);
                else
                {
                    lol.Text = lol.Text.Remove(lol.Text.Length - 1);
                    lol.Text += digit;
                }
            }
            else
                lol.Text = digit;
        }

        private void Zero(object sender, RoutedEventArgs e)
        {
            AddNumber("0");
        }
        private void One(object sender, RoutedEventArgs e)
        {
            AddNumber("1");
        }
        private void Two(object sender, RoutedEventArgs e)
        {
            AddNumber("2");
        }
        private void Three(object sender, RoutedEventArgs e)
        {
            AddNumber("3");
        }
        private void Four(object sender, RoutedEventArgs e)
        {
            AddNumber("4");
        }
        private void Five(object sender, RoutedEventArgs e)
        {
            AddNumber("5");
        }
        private void Six(object sender, RoutedEventArgs e)
        {
            AddNumber("6");
        }
        private void Seven(object sender, RoutedEventArgs e)
        {
            AddNumber("7");
        }
        private void Eight(object sender, RoutedEventArgs e)
        {
            AddNumber("8");
        }
        private void Nine(object sender, RoutedEventArgs e)
        {
            AddNumber("9");
        }

        private void Ans(object sender, RoutedEventArgs e)
        {
            memoryMode = false;
            Answer(sender, e);
        }

        private void Answer(object sender, RoutedEventArgs e)
        {
            SByte a = 1;
            dotted = false;
            double kof = 10, number = 0, buf = 0, answer = 0;
            int itk = 1;
            operation c = operation.plus;
            for (int i = 0; i < lol.Text.Length; i++)
            {
                if (lol.Text[i] == '-') a = -1;
                bool isNumeric = Char.IsDigit(lol.Text[i]);
                if (isNumeric)
                {
                    if (Math.Abs(kof) == 10) number *= (double)kof;
                    if (kof < 1) number += (double)kof * (lol.Text[i] - '0'); else number += (double)lol.Text[i] - (double)'0';
                }
                else if (lol.Text[i] == '.') kof = 1;
                if (Math.Abs(kof) < 10) kof /= 10;
                isNumeric = false;
                if (IsOperand(i) || i == lol.Text.Length - 1)
                {
                    number *= a;
                    a = 1;
                    switch (c)
                    {
                        case operation.plus:
                            answer = (double)buf + (double)number;
                            break;
                        case operation.minus:
                            answer = (double)buf - (double)number;
                            break;
                        case operation.multiply:
                            answer = (double)buf * (double)number;
                            break;
                        case operation.div:
                            answer = (double)buf / (double)number;
                            break;
                        case operation.power:
                            answer = Math.Pow((double)buf, (double)number);
                            break;
                    }

                    buf = answer;
                    
                    char b = lol.Text[i];

                    switch (b)
                    {
                        case '＋':
                            c = operation.plus;
                            break;
                        case '－':
                            c = operation.minus;
                            break;
                        case 'Ｘ':
                            c = operation.multiply;
                            break;
                        case '／':
                            c = operation.div;
                            break;
                        case '^':
                            c = operation.power;
                            break;
                    }

                    kof = 10;
                    number = 0;
                    itk++;
                }
            }
            signed = false;
            if (memoryMode) MemoryBox.Text = answer.ToString(System.Globalization.CultureInfo.InvariantCulture); else
            lol.Text = answer.ToString(System.Globalization.CultureInfo.InvariantCulture);
            if (Math.Round(answer) != answer) dotted = true;
            if (Double.IsNaN(answer) || Double.IsInfinity(answer) 
                || Double.IsNegativeInfinity(answer) || Double.IsPositiveInfinity(answer))
                    undefinedValue = true; else undefinedValue = false;
        }

        private void removeLast()
        {
            lol.Text = lol.Text.Remove(lol.Text.Length - 1);
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            if (lol.Text[lol.Text.Length - 1] == ')')
            {
                removeLast();
                signed = true;
            }
            else
            if (IsOperand(lol.Text.Length - 1) && beenUndotted)
            {
                removeLast();
                dotted = true;
            }
            else
                if (lol.Text[lol.Text.Length - 1] == '-')
            {
                removeLast();
                signed = false;
                if (lol.Text[lol.Text.Length - 1] == '(') removeLast();
            }
            else
            if (lol.Text[lol.Text.Length - 1] == '.')
            {
                dotted = false;
                removeLast();
            }
            else
            if (lol.Text.Length == 2 && lol.Text[0] == '-') lol.Text = "0";
            else
                removeLast();
            if (lol.Text.Length == 0) lol.Text = "0";
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            lol.Text = "0";
            dotted = signed = beenUndotted = false;
        }

        private void MemorySave(object sender, RoutedEventArgs e)
        {
            Define();
            memoryMode = true;
            Answer(sender, e);
        }

        private void MemoryPlus(object sender, RoutedEventArgs e)
        {
            Define();
            Plus(sender, e);
            IsMinus();
            lol.Text += MemoryBox.Text;
        }
        private void MemoryMinus(object sender, RoutedEventArgs e)
        {
            Define();
            Minus(sender, e);
            IsMinus();
            lol.Text += MemoryBox.Text;
        }

        private void MemoryPower(object sender, RoutedEventArgs e)
        {
            Define();
            Power(sender, e);
            IsMinus();
            lol.Text += MemoryBox.Text;
        }

        private void MemoryMultiply(object sender, RoutedEventArgs e)
        {
            Define();
            Multiply(sender, e);
            IsMinus();
            lol.Text += MemoryBox.Text;
        }

        private void MemoryDiv(object sender, RoutedEventArgs e)
        {
            Define();
            Div(sender, e);
            IsMinus();
            lol.Text += MemoryBox.Text;
        }

        private void clearMemory(object sender, RoutedEventArgs e)
        {
            MemoryBox.Text = "0";
        }

        private void IsMinus()
        {
            if (MemoryBox.Text[0] == '-')
            {
                lol.Text += "(";
                signed = true;
            }
        }
        private void Define()
        {
            if (undefinedValue) lol.Text = "0";
            undefinedValue = false;
        }

        private void Info(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Калькулятор 'Сипшар' ©\n\n Автор - Кравченко Илья Павлович ");
        }

        private void Menu(object sender, RoutedEventArgs e)
        {
          this.Close();
        }
    }
}
