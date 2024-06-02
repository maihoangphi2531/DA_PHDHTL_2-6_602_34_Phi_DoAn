using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace _34_HoangPhi_B1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public RichTextBox RichTextBox1 => richTextBox1;
        public RichTextBox RichTextBox2 => richTextBox2;
        public Button Button1 => button1;
        public Button Button2 => button2;
        public Button Button3 => button3;
        public Button Button4 => button4;

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // You can add event handling code here if needed.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear previous results
            richTextBox2.Clear();

            // Kiểm tra số nguyên, số nguyên tố, số chẵn/lẻ, số âm/dương, và số hoàn hảo
            if (int.TryParse(richTextBox1.Text, out int number))
            {
                richTextBox2.AppendText($"Số {number} là số nguyên.\n");

                if (number > 0)
                {
                    richTextBox2.AppendText($"Số {number} là số dương.\n");
                }
                else if (number < 0)
                {
                    richTextBox2.AppendText($"Số {number} là số âm.\n");
                }
                else
                {
                    richTextBox2.AppendText($"Số {number} là số không.\n");
                }

                if (IsPrime(number))
                {
                    richTextBox2.AppendText($"Số {number} là số nguyên tố.\n");
                }
                else
                {
                    richTextBox2.AppendText($"Số {number} không phải là số nguyên tố.\n");
                }

                if (number % 2 == 0)
                {
                    richTextBox2.AppendText($"Số {number} là số chẵn.\n");
                }
                else
                {
                    richTextBox2.AppendText($"Số {number} là số lẻ.\n");
                }

                if (IsPerfectNumber(number))
                {
                    richTextBox2.AppendText($"Số {number} là số hoàn hảo.\n");
                }
                else
                {
                    richTextBox2.AppendText($"Số {number} không phải là số hoàn hảo.\n");
                }
            }
            else
            {
                richTextBox2.AppendText($"'{richTextBox1.Text}' không phải là số nguyên.\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Clear previous results
            richTextBox2.Clear();

            // Giải phương trình bậc nhất ax + b = 0
            var equation = richTextBox1.Text;
            var coefficients = ParseLinearEquation(equation);
            if (coefficients != null)
            {
                double a = coefficients[0];
                double b = coefficients[1];
                if (a != 0)
                {
                    double x = -b / a;
                    richTextBox2.AppendText($"Nghiệm của phương trình {equation} là x = {x}\n");
                }
                else
                {
                    richTextBox2.AppendText($"Phương trình {equation} vô nghiệm.\n");
                }
            }
            else
            {
                richTextBox2.AppendText("Phương trình không hợp lệ.\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Clear previous results
            richTextBox2.Clear();

            // Giải phương trình bậc hai ax^2 + bx + c = 0
            var equation = richTextBox1.Text;
            var coefficients = ParseQuadraticEquation(equation);
            if (coefficients != null)
            {
                double a = coefficients[0];
                double b = coefficients[1];
                double c = coefficients[2];
                double delta = b * b - 4 * a * c;
                if (delta > 0)
                {
                    double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                    double x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                    richTextBox2.AppendText($"Phương trình {equation} có 2 nghiệm x1 = {x1} và x2 = {x2}\n");
                }
                else if (delta == 0)
                {
                    double x = -b / (2 * a);
                    richTextBox2.AppendText($"Phương trình {equation} có nghiệm kép x = {x}\n");
                }
                else
                {
                    richTextBox2.AppendText($"Phương trình {equation} vô nghiệm.\n");
                }
            }
            else
            {
                richTextBox2.AppendText("Phương trình không hợp lệ.\n");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Clear previous results
            richTextBox2.Clear();

            // Thực hiện phép toán cơ bản + - * /
            var expression = richTextBox1.Text;
            try
            {
                var result = EvaluateExpression(expression);
                richTextBox2.AppendText($"Kết quả của phép toán {expression} là {result}\n");
            }
            catch (Exception ex)
            {
                richTextBox2.AppendText($"Lỗi: {ex.Message}\n");
            }
        }

        private bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        private bool IsPerfectNumber(int number)
        {
            if (number < 1) return false;
            int sum = 0;
            for (int i = 1; i <= number / 2; i++)
            {
                if (number % i == 0)
                {
                    sum += i;
                }
            }
            return sum == number;
        }

        private double[] ParseLinearEquation(string equation)
        {
            try
            {
                equation = equation.Replace(" ", "");
                var parts = equation.Split(new[] { 'x', '=' });
                if (parts.Length == 3)
                {
                    double a = double.Parse(parts[0]);
                    double b = double.Parse(parts[1] + parts[2]);
                    return new[] { a, b };
                }
            }
            catch { }
            return null;
        }

        private double[] ParseQuadraticEquation(string equation)
        {
            try
            {
                equation = equation.Replace(" ", "");

               
                equation = equation.Replace("x^2", "a");
                equation = equation.Replace("x", "b");
                equation = equation.Replace("=", "c");

             
                var match = Regex.Match(equation, @"(?<a>-?\d*\.?\d*)a(?<b>[+-]?\d*\.?\d*)b(?<c>[+-]?\d*\.?\d*)c");
                if (match.Success)
                {
                    double a = double.Parse(string.IsNullOrWhiteSpace(match.Groups["a"].Value) ? "1" : match.Groups["a"].Value);
                    double b = double.Parse(string.IsNullOrWhiteSpace(match.Groups["b"].Value) ? "0" : match.Groups["b"].Value);
                    double c = double.Parse(string.IsNullOrWhiteSpace(match.Groups["c"].Value) ? "0" : match.Groups["c"].Value);

                    return new[] { a, b, c };
                }
            }
            catch { }
            return null;
        }

        private double EvaluateExpression(string expression)
        {
            try
            {
                // Sử dụng DataTable để tính toán biểu thức
                var dataTable = new DataTable();
                var value = dataTable.Compute(expression, "");
                return Convert.ToDouble(value);
            }
            catch
            {
                throw new ArgumentException("Biểu thức không hợp lệ.");
            }
        }
    }
}
