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
                    t = t.Substring(0, t.Length - 2);
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
                        break;
                }

                return result.ToString();
            }
            return "Error";
        }
    
        void scrollEnd(TextBox t)
        {
            t.CaretIndex = t.Text.Length;
            var rect = t.GetRectFromCharacterIndex(t.CaretIndex);
            t.ScrollToHorizontalOffset(rect.Right);
        }
    }
}
