using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTest.Pages;
using Xunit;

namespace SeleniumTest;

public class RegisterPatientTests : IDisposable
{
    private readonly IWebDriver driver;
    private readonly RegistrationPage registrationPage;

    public RegisterPatientTests()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArguments("start-maximized");
        options.AddArguments("disable-infobars");
        options.AddArguments("--disable-extensions");
        options.AddArguments("--disable-gpu");
        options.AddArguments("--disable-dev-shm-usage");
        options.AddArguments("--no-sandbox");
        options.AddArguments("--disable-notifications");
        driver = new ChromeDriver();
        registrationPage = new RegistrationPage(driver);
        registrationPage.Navigate();
        Assert.Equal(driver.Url, RegistrationPage.URI);
        Assert.True(registrationPage.EnsureWholePageIsDisplayed());
    }
    
    public void Dispose()
    {
        driver.Quit();
        driver.Dispose();
    }

    [Fact]
    public void TestEmailAlreadyTaken()
    {
        registrationPage.InsertName("Jovan");
        registrationPage.InsertSurname("Petrovic");
        registrationPage.InsertEmail("jovan@gmail.com");
        registrationPage.InsertPassword("asd");
        registrationPage.InsertConfirmPassword("asd");
        registrationPage.InsertUcid("1234567");
        registrationPage.InsertBirthDate("07/21/1996");
        registrationPage.InsertCountry("Srbija");
        registrationPage.InsertCity("Novi Sad");
        registrationPage.InsertStreet("Bulevar Oslobodjenja");
        registrationPage.InsertNumber("5");
        registrationPage.SelectGender("Male");
        registrationPage.SelectBloodType("AB+");
        registrationPage.SelectChosenDoctor("2");
        registrationPage.ClickRegisterButton();
        registrationPage.WaitForErrorMessage();
        Assert.Equal(RegistrationPage.ExistingEmailMessage,registrationPage.GetErrorMessage());
        Assert.Equal(RegistrationPage.URI,driver.Url);
    }
    
    [Fact]
    public void TestExistingUserUcid()
    {
        registrationPage.InsertName("Jovan");
        registrationPage.InsertSurname("Petrovic");
        registrationPage.InsertEmail("asdf@gmail.com");
        registrationPage.InsertPassword("asd");
        registrationPage.InsertConfirmPassword("asd");
        registrationPage.InsertUcid("111");
        registrationPage.InsertBirthDate("07/21/1996");
        registrationPage.InsertCountry("Srbija");
        registrationPage.InsertCity("Novi Sad");
        registrationPage.InsertStreet("Bulevar Oslobodjenja");
        registrationPage.InsertNumber("5");
        registrationPage.SelectGender("Male");
        registrationPage.SelectBloodType("AB+");
        registrationPage.SelectChosenDoctor("2");
        registrationPage.ClickRegisterButton();
        registrationPage.WaitForErrorMessage();
        Assert.Equal(RegistrationPage.ExistingUcidMessage,registrationPage.GetErrorMessage());
        Assert.Equal(RegistrationPage.URI,driver.Url);
    }

    [Fact]
    public void TestSuccessfulRegistration()
    {
        registrationPage.InsertName("Jovan");
        registrationPage.InsertSurname("Petrovic");
        registrationPage.InsertEmail("qwe@gmail.com");
        registrationPage.InsertPassword("asd");
        registrationPage.InsertConfirmPassword("asd");
        registrationPage.InsertUcid("532143");
        registrationPage.InsertBirthDate("07/21/1996");
        registrationPage.InsertCountry("Srbija");
        registrationPage.InsertCity("Novi Sad");
        registrationPage.InsertStreet("Bulevar Oslobodjenja");
        registrationPage.InsertNumber("5");
        registrationPage.SelectGender("Male");
        registrationPage.SelectBloodType("AB+");
        registrationPage.SelectChosenDoctor("2");
        registrationPage.ClickRegisterButton();
        LoginPage loginPage = new LoginPage(driver);
        loginPage.EnsurePageIsDisplayed();
        Assert.True(loginPage.EnsurePageIsDisplayed());
    }
}