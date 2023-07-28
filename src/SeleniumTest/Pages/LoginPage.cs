using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest.Pages;

public class LoginPage
{
    private readonly IWebDriver driver;
    public const string URI = "http://localhost:4200/login";
    private IWebElement EmailElement => driver.FindElement(By.Id("email"));
    private IWebElement PasswordElement => driver.FindElement(By.Id("password"));
    private IWebElement LoginButtonElement => driver.FindElement(By.Id("loginButton"));
    
    
    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
    }
    
    public bool EmailElementDisplayed()
    {
        return EmailElement.Displayed;
    }
    
    public bool PasswordElementDisplayed()
    {
        return PasswordElement.Displayed;
    }
    
    public bool LoginButtonElementDisplayed()
    {
        return LoginButtonElement.Displayed;
    }
    
    public void InsertEmail(string email)
    {
        EmailElement.SendKeys(email);
    }
    
    public void InsertPassword(string password)
    {
        PasswordElement.SendKeys(password);
    }
    
    public void ClickLoginButton()
    {
        LoginButtonElement.Click();
    }
    
    public bool EnsurePageIsDisplayed()
    {
        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
        if (wait.Until(driver => driver.Url==URI && EmailElementDisplayed() && PasswordElementDisplayed() && LoginButtonElementDisplayed()))
        {
            return true;
        }
        return false;
    }
}