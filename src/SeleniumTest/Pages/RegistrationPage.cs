using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest.Pages;

public class RegistrationPage
{
    private readonly IWebDriver driver;
    public const string URI = "http://localhost:4200/register";
    private IWebElement NameElement => driver.FindElement(By.Id("name"));
    private IWebElement SurnameElement => driver.FindElement(By.Id("surname"));
    private IWebElement EmailElement => driver.FindElement(By.Id("email"));
    private IWebElement UcidElement => driver.FindElement(By.Id("ucid"));
    private IWebElement PasswordElement => driver.FindElement(By.Id("password"));
    private IWebElement ConfirmPasswordElement => driver.FindElement(By.Id("confirmPassword"));
    private IWebElement BirthDateElement => driver.FindElement(By.Id("birthDate"));
    private IWebElement CountryElement => driver.FindElement(By.Id("country"));
    private IWebElement CityElement => driver.FindElement(By.Id("city"));
    private IWebElement StreetElement => driver.FindElement(By.Id("street"));
    private IWebElement NumberElement => driver.FindElement(By.Id("number"));
    private SelectElement GenderElement => new SelectElement(driver.FindElement(By.Id("gender")));
    private SelectElement BloodTypeElement => new SelectElement(driver.FindElement(By.Id("bloodType")));
    private SelectElement ChosenDoctorElement => new SelectElement(driver.FindElement(By.Id("chosenDoctor")));
    private IWebElement RegisterButtonElement => driver.FindElement(By.Id("registerButton"));
    private IWebElement ErrorMessageElement => driver.FindElement(By.Id("errorMessage"));
    public const string ExistingEmailMessage = "User with this email already exists";
    public const string ExistingUcidMessage = "User with this UCID already exists";
    
    public RegistrationPage(IWebDriver driver)
    {
        this.driver = driver;
    }
    
    public bool NameElementDisplayed()
    {
        return NameElement.Displayed;
    }
    
    public bool SurnameElementDisplayed()
    {
        return SurnameElement.Displayed;
    }
    
    public bool EmailElementDisplayed()
    {
        return EmailElement.Displayed;
    }
    
    public bool UcidElementDisplayed()
    {
        return UcidElement.Displayed;
    }
    
    public bool PasswordElementDisplayed()
    {
        return PasswordElement.Displayed;
    }
    
    public bool ConfirmPasswordElementDisplayed()
    {
        return ConfirmPasswordElement.Displayed;
    }
    
    public bool BirthDateElementDisplayed()
    {
        return BirthDateElement.Displayed;
    }
    
    public bool CountryElementDisplayed()
    {
        return CountryElement.Displayed;
    }
    
    public bool CityElementDisplayed()
    {
        return CityElement.Displayed;
    }
    
    public bool StreetElementDisplayed()
    {
        return StreetElement.Displayed;
    }
    
    public bool NumberElementDisplayed()
    {
        return NumberElement.Displayed;
    }
    
    public bool GenderElementDisplayed()
    {
        return GenderElement.WrappedElement.Displayed;
    }
    
    public bool BloodTypeElementDisplayed()
    {
        return BloodTypeElement.WrappedElement.Displayed;
    }
    
    public bool ChosenDoctorElementDisplayed()
    {
        return ChosenDoctorElement.WrappedElement.Displayed;
    }
    
    public bool RegisterButtonElementDisplayed()
    {
        return RegisterButtonElement.Displayed;
    }

    public bool EnsureWholePageIsDisplayed()
    {
        return NameElementDisplayed() && SurnameElementDisplayed() && EmailElementDisplayed() && UcidElementDisplayed() &&
               PasswordElementDisplayed() && ConfirmPasswordElementDisplayed() && BirthDateElementDisplayed() &&
               CountryElementDisplayed() && CityElementDisplayed() && StreetElementDisplayed() &&
               NumberElementDisplayed() && GenderElementDisplayed() && BloodTypeElementDisplayed() &&
               ChosenDoctorElementDisplayed() && RegisterButtonElementDisplayed();
    }
    
    
    
    public bool ErrorMessageElementDisplayed()
    {
        return ErrorMessageElement.Displayed;
    }
    
    public void WaitForErrorMessage()
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(driver => ErrorMessageElementDisplayed());
    }
    
    public void InsertName(string name)
    {
        NameElement.SendKeys(name);
    }
    
    public void InsertSurname(string surname)
    {
        SurnameElement.SendKeys(surname);
    }
    
    public void InsertEmail(string email)
    {
        EmailElement.SendKeys(email);
    }
    
    public void InsertUcid(string ucid)
    {
        UcidElement.SendKeys(ucid);
    }
    
    public void InsertPassword(string password)
    {
        PasswordElement.SendKeys(password);
    }
    
    public void InsertConfirmPassword(string confirmPassword)
    {
        ConfirmPasswordElement.SendKeys(confirmPassword);
    }
    
    public void InsertBirthDate(string birthDate)
    {
        BirthDateElement.SendKeys(birthDate);
    }
    
    public void InsertCountry(string country)
    {
        CountryElement.SendKeys(country);
    }
    
    public void InsertCity(string city)
    {
        CityElement.SendKeys(city);
    }
    
    public void InsertStreet(string street)
    {
        StreetElement.SendKeys(street);
    }
    
    public void InsertNumber(string number)
    {
        NumberElement.SendKeys(number);
    }
    
    public void SelectGender(string gender)
    {
        GenderElement.SelectByText(gender);
    }
    
    public void SelectBloodType(string bloodType)
    {
        BloodTypeElement.SelectByText(bloodType);
    }
    
    public void SelectChosenDoctor(string chosenDoctor)
    {
        ChosenDoctorElement.SelectByValue(chosenDoctor);
    }
    
    public void ClickRegisterButton()
    {
        RegisterButtonElement.Click();
    }
    
    public string GetErrorMessage()
    {
        return ErrorMessageElement.Text;
    }
    
    public void Navigate() => driver.Navigate().GoToUrl(URI);

}