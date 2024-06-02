using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using _34_HoangPhi_B1;

namespace TestCase_34
{
    [TestClass]
    public class UnitTest1
    {
        private Form1 form;

        [TestInitialize]
        public void Setup()
        {
            form = new Form1();
            form.Show();
            form.RichTextBox1.Clear();
            form.RichTextBox2.Clear();
        }

        [TestCleanup]
        public void Cleanup()
        {
            form.Close();
        }

        [TestMethod]
        public void TestButton1_Click()
        {
            form.RichTextBox1.Text = "6";
            form.Button1.PerformClick();
            string expected = "Số 6 là số nguyên.\nSố 6 là số dương.\nSố 6 không phải là số nguyên tố.\nSố 6 là số chẵn.\nSố 6 là số hoàn hảo.\n";
            Assert.AreEqual(expected, form.RichTextBox2.Text);
        }

        [TestMethod]
        public void TestButton1_Click_NonInteger()
        {
            form.RichTextBox1.Text = "abc";
            form.Button1.PerformClick();
            string expected = "'abc' không phải là số nguyên.\n";
            Assert.AreEqual(expected, form.RichTextBox2.Text);
        }

        [TestMethod]
        public void TestButton2_PTB1()
        {
            form.RichTextBox1.Text = "2x + 4 = 0";
            form.Button2.PerformClick();
            string expected = "Nghiệm của phương trình 2x + 4 = 0 là x = -20\n";
            Assert.AreEqual(expected, form.RichTextBox2.Text);
        }

        [TestMethod]
        public void TestButton2_Click_InvalidEquation()
        {
            form.RichTextBox1.Text = "2x + 4";
            form.Button2.PerformClick();
            string expected = "Phương trình không hợp lệ.\n";
            Assert.AreEqual(expected, form.RichTextBox2.Text);
        }

        [TestMethod]
        public void TestButton3_Click_ValidEquation()
        {
            form.RichTextBox1.Text = "1x^2 - 3x + 2 = 0";
            form.Button3.PerformClick();
            string expected = "Phương trình 1x^2 - 3x + 2 = 0 có 2 nghiệm x1 = 2 và x2 = 1\n";
            Assert.AreEqual(expected, form.RichTextBox2.Text);
        }

        [TestMethod]
        public void TestButton3_Click_InvalidEquation()
        {
            form.RichTextBox1.Text = "1x^2 + 3 = 0";
            form.Button3.PerformClick();
            string expected = "Phương trình không hợp lệ.\n";
            Assert.AreEqual(expected, form.RichTextBox2.Text);
        }

        [TestMethod]
        public void TestButton4_Click_ValidExpression()
        {
            form.RichTextBox1.Text = "2 + 3 * 4";
            form.Button4.PerformClick();
            string expected = "Kết quả của phép toán 2 + 3 * 4 là 14\n";
            Assert.AreEqual(expected, form.RichTextBox2.Text);
        }

        [TestMethod]
        public void TestButton4_Click_InvalidExpression()
        {
            form.RichTextBox1.Text = "2 + 3 *";
            form.Button4.PerformClick();
            string expected = "Lỗi: Biểu thức không hợp lệ.\n";
            Assert.AreEqual(expected, form.RichTextBox2.Text);
        }
    }
}
