using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ops = "+-*/=";
        bool newEquation = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, EventArgs e)
        {            
            Button btn = (Button)sender;
            char c = btn.Content.ToString()[0];

            if (ops.Contains(c))
            {
                switch (c)
                {
                    case '=':
                        newEquation = true;
                        equationBox.Text = solveEquation(equationBox.Text);
                        break;

                    default:
                        bool isContains = false;
                        foreach (char op in ops)
                        {
                            if (equationBox.Text.Contains(op))
                            {
                                isContains = true;
                                break;
                            }
                        }
                        if (isContains)
                        {
                            equationBox.Text = solveEquation(equationBox.Text);
                        }
                        equationBox.AppendText(c.ToString());
                        break;
                }
            }
            else if (c == ',')
            {
                if (!char.IsDigit(equationBox.Text.Last()))
                {
                    equationBox.AppendText("0");
                }
                equationBox.AppendText(c.ToString());
            }
            else
            {
                if (newEquation)
                {
                    newEquation = false;
                    equationBox.Text = c.ToString();
                }
                else
                {
                    equationBox.AppendText(c.ToString());
                }

            }
            scrollEnd(equationBox);
        }

        string solveEquation(string equation)
        {
            if (equation.Length > 0)
            {
                string t = equation;
                if (ops.Contains(t.Last()))
                {
                    t = t.Substring(0, t.Length - 1);
                }

                float num1 = 0, num2 = 1;

                string t2 = t;
                foreach (char op in ops)
                {
                    if (t2.Contains(op))
                    {
                        t2 = t2.Split(op)[0];
                    }
                }
                num1 = float.Parse(t2);

                t2 = t;
                foreach (char op in ops)
                {
                    if (t2.Contains(op))
                    {
                        t2 = t2.Split(op)[1];
                    }
                }
                num2 = float.Parse(t2);

                t2 = t;
                char current = '=';
                foreach (char op in ops)
                {
                    if (t2.Contains(op))
                    {
                        current = op;
                        break;
                    }
                }

                float result = 0;
                switch (current)
                {
                    case '+':
                        result = (num1 + num2);
                        break;

                    case '-':
                        result = (num1 - num2);
                        break;

                    case '*':
                        result = (num1 * num2);
                        break;

                    case '/':
                        if (num1 == 0 && num2 == 0)
                        {
                            return "Undefined";
                        }
                        else if (num2 == 0)
                        {
                            return "Indeterminate";
                        }
                        result = (num1 / num2);
                        break;

                    default:
                        return num1.ToString();
                }

                return result.ToString();
            }
            return "";
        }
    
        void scrollEnd(TextBox t)
        {
            t.CaretIndex = t.Text.Length;
            var rect = t.GetRectFromCharacterIndex(t.CaretIndex);
            t.ScrollToHorizontalOffset(rect.Right);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            newEquation = true;
            equationBox.Text = "";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Dictionary<Key, string> keyDict = new Dictionary<Key, string>();
            keyDict.Add(Key.D0, "0");
            keyDict.Add(Key.D1, "1");
            keyDict.Add(Key.D2, "2");
            keyDict.Add(Key.D3, "3");
            keyDict.Add(Key.D4, "4");
            keyDict.Add(Key.D5, "5");
            keyDict.Add(Key.D6, "6");
            keyDict.Add(Key.D7, "7");
            keyDict.Add(Key.D8, "8");
            keyDict.Add(Key.D9, "9");

            keyDict.Add(Key.NumPad0, "0");
            keyDict.Add(Key.NumPad1, "1");
            keyDict.Add(Key.NumPad2, "2");
            keyDict.Add(Key.NumPad3, "3");
            keyDict.Add(Key.NumPad4, "4");
            keyDict.Add(Key.NumPad5, "5");
            keyDict.Add(Key.NumPad6, "6");
            keyDict.Add(Key.NumPad7, "7");
            keyDict.Add(Key.NumPad8, "8");
            keyDict.Add(Key.NumPad9, "9");

            keyDict.Add(Key.OemComma, ",");
            keyDict.Add(Key.OemPlus, "+");
            keyDict.Add(Key.OemMinus, "-");
            keyDict.Add(Key.Multiply, "*");
            keyDict.Add(Key.Divide, "/");
            keyDict.Add(Key.Enter, "=");
            keyDict.Add(Key.Subtract, "-");
            keyDict.Add(Key.Add, "+");
            keyDict.Add(Key.Decimal, ",");

            if (keyDict.ContainsKey(e.Key))
            {
                Button_Click(new Button() { Content = keyDict[e.Key] }, null);
            }
            else if (e.Key == Key.Delete || e.Key == Key.C)
            {
                Clear_Click(null, null);
            }
            else if (e.Key == Key.Back)
            {
                if (equationBox.Text.Length > 0)
                {
                    equationBox.Text = equationBox.Text.Substring(0, equationBox.Text.Length - 1);
                }
            }
        }

        private void equationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            quickResult.Text = solveEquation(equationBox.Text);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (equationBox.Text.Length > 0)
            {
                equationBox.Text = equationBox.Text.Substring(0, equationBox.Text.Length - 1);
            }
        }
    }
}
