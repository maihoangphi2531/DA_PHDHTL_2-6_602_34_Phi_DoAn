using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace e
{
    public partial class Form1 : Form
    {
        private ChromeDriverService chromeDriverService;
        private IWebDriver driver;
        private bool isDriverInitialized = false;

        // Đường dẫn tới thư mục của tiện ích 2Captcha đã giải nén
        private const string extensionPath = @"C:\Users\maiho\AppData\Local\Google\Chrome\User Data\Default\Extensions\ifibfemgeogfhoebkmokieepdoobkbpo\3.6.3_0";
        private const string predefinedUrl = "https://thedragonslayer2.github.io/Redirect/ReachabilityCheck.html?Identification=QQJCqsJMeLpuo9woAlO&EncodedMsg=xakUUWJGAyHBh-Bgrjp6b&ExpireAt=1716827861";
        private const string apiKey = "6079433a2ed67998f87ad5f8a68bdeaa"; // API key cho 2Captcha

        public Form1()
        {
            InitializeComponent();

            chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
        }

        private void InitializeDriver()
        {
            if (!isDriverInitialized)
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--load-extension=" + extensionPath); // Thêm tiện ích mở rộng từ thư mục
                driver = new ChromeDriver(chromeDriverService, options);
                isDriverInitialized = true;
            }
        }

        private void EnterApiKey()
        {
            try
            {
                // Mở trang cài đặt của tiện ích 2Captcha để nhập API key
                driver.Navigate().GoToUrl("chrome-extension://ifibfemgeogfhoebkmokieepdoobkbpo/options.html");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                var apiKeyInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Name("apiKey")));
                apiKeyInput.Clear();
                apiKeyInput.SendKeys(apiKey);

                var saveButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("connect"))); // ID của nút lưu là "connect"
                saveButton.Click();

                // Đợi một khoảng thời gian để đảm bảo API key được lưu và tiện ích sẵn sàng
                Thread.Sleep(5000);
            }
            catch (NoSuchElementException ex)
            {
                MessageBox.Show("Không thể tìm thấy phần tử: " + ex.Message);
                Debug.WriteLine("Không thể tìm thấy phần tử: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi nhập API key: " + ex.Message);
                Debug.WriteLine("Đã xảy ra lỗi khi nhập API key: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeDriver();
            EnterApiKey(); // Nhập API key trước khi thực hiện các chức năng khác

            string url = txt1.Text;
            if (string.IsNullOrEmpty(url))
            {
                url = predefinedUrl;
                txt1.Text = url;
            }

            try
            {
                driver.Navigate().GoToUrl(url);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                var generateKeyButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[text()='Generate Key']")));
                generateKeyButton.Click();

                Thread.Sleep(5000);

                driver.Navigate().GoToUrl("https://linkvertise.com/591410/730.46889609213/dynamic?r=aHR0cHM6Ly9tcmphY2sueDEwLm14L1doaXRlbGlzdC5waHA%2FRW5jb2RlZE1zZz1veHdEWHJzczAx&o=sharing");

                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body")));

                var menuButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//mat-icon[text()='menu']")));
                Debug.WriteLine("Menu button found.");
                menuButton.Click();

                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                var profileLink = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Profiles']")));
                Debug.WriteLine("Profile link found.");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", profileLink);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", profileLink);

                string currentUrl = driver.Url;
                Debug.WriteLine("Current URL: " + currentUrl);
                Debug.WriteLine("Page Title: " + driver.Title);

                var memberLoginLink = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[@class='login-link' and text()='Member Login']")));
                Debug.WriteLine("Member Login link found.");
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", memberLoginLink);
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", memberLoginLink);

                currentUrl = driver.Url;
                Debug.WriteLine("Current URL after Member Login: " + currentUrl);
                Debug.WriteLine("Page Title after Member Login: " + driver.Title);

                var emailInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@type='email']")));
                emailInput.SendKeys("maihoangphi2003@gmail.com");

                var passwordInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@type='password']")));
                passwordInput.SendKeys("Maihoangphi2003");

                var loginButton = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@type='submit']")));
                loginButton.Click();

                currentUrl = driver.Url;
                Debug.WriteLine("Current URL after login: " + currentUrl);
                Debug.WriteLine("Page Title after login: " + driver.Title);

                // Kiểm tra lỗi đăng nhập
                var errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='error-message']")));
                if (errorMessage.Displayed)
                {
                    Debug.WriteLine("Login Error: " + errorMessage.Text);
                    MessageBox.Show("Please check your credentials.");
                }
            }
            catch (NoSuchElementException ex)
            {
                MessageBox.Show("Không thể tìm thấy phần tử: " + ex.Message);
                Debug.WriteLine("Không thể tìm thấy phần tử: " + ex.Message);
            }
            catch (ElementNotInteractableException ex)
            {
                MessageBox.Show("Phần tử không thể tương tác: " + ex.Message);
                Debug.WriteLine("Phần tử không thể tương tác: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                Debug.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (isDriverInitialized)
            {
                driver.Quit();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeDriver();
                EnterApiKey(); // Nhập API key trước khi thực hiện các chức năng khác

                var h2Tags = driver.FindElements(By.TagName("h2"));
                foreach (var h2 in h2Tags)
                {
                    Debug.WriteLine("h2 Tag Text: " + h2.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                Debug.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        // Định nghĩa sự kiện cho btn3_Click
        private void btn3_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeDriver();
                EnterApiKey(); // Nhập API key trước khi thực hiện các chức năng khác
                driver.Navigate().GoToUrl("https://www.example.com/upload"); // Thay thế URL bằng URL thực tế của bạn

                var fileInput = driver.FindElement(By.XPath("//input[@type='file']"));
                fileInput.SendKeys(@"D:\path\to\your\file.txt"); // Thay thế đường dẫn file của bạn

                var uploadButton = driver.FindElement(By.XPath("//button[@id='upload']"));
                uploadButton.Click();

                MessageBox.Show("Upload thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                Debug.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        // Định nghĩa sự kiện cho button2_Click_1
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                InitializeDriver();
                EnterApiKey(); // Nhập API key trước khi thực hiện các chức năng khác
                driver.Navigate().GoToUrl("https://www.example.com/delete"); // Thay thế URL bằng URL thực tế của bạn

                var deleteButton = driver.FindElement(By.XPath("//button[@id='delete']"));
                deleteButton.Click();

                MessageBox.Show("Đã xóa thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                Debug.WriteLine("Đã xảy ra lỗi: " + ex.Message);
            }
        }
    }
}
