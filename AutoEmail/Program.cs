using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize Chromedriver
            var options = new ChromeOptions();
            options.AddArgument("--no-sandbox --disable-infobars --incognito");
            ChromeDriver driver = new ChromeDriver(options);

            // Benutzerdaten generieren
            var userData = GenerateRandomUserData();
            var username = userData.Item1;
            var firstname = userData.Item1;
            var lastname = userData.Item1;
            var password = userData.Item2;
            var email = $"C{username}";

            // Website für das Erstellen eines E-Mail-Accounts bei Outlook aufrufen
            driver.Navigate().GoToUrl("https://signup.live.com/signup?lcid=1033&wa=wsignin1.0&rpsnv=13&ct=1672112140&rver=7.0.6737.0&wp=MBI_SSL&wreply=https%3a%2f%2foutlook.live.com%2fowa%2f%3fnlp%3d1%26signup%3d1%26RpsCsrfState%3dec907f44-5c7c-95bc-fb36-fbb9207b2321&id=292841&CBCXT=out&lw=1&fl=dob%2cflname%2cwld&cobrandid=90015&lic=1&uaid=13dee67bbb024f75b33a579b5e53cfb5");

            // PasswordInput
            // Felder für Vorname, Nachname, Benutzername und Passwort ausfüllen
            var usernameField = driver.FindElement(By.Id("MemberName"));
            usernameField.SendKeys(email);
            var singupbutton = driver.FindElement(By.Id("iSignupAction"));
            singupbutton.Click();
            System.Threading.Thread.Sleep(2000);
            var passwordField = driver.FindElement(By.Id("PasswordInput"));
            passwordField.SendKeys(password);
            var singupbutton1 = driver.FindElement(By.Id("iSignupAction"));
            singupbutton1.Click();
            System.Threading.Thread.Sleep(2000);
            var firstNameField = driver.FindElement(By.Id("FirstName"));
            firstNameField.SendKeys($"{firstname}");
            var lastNameField = driver.FindElement(By.Id("LastName"));
            lastNameField.SendKeys($"{lastname}");
            


            // Formular absenden
            var submitButton = driver.FindElement(By.Id("iSignupAction"));
            submitButton.Click();
            System.Threading.Thread.Sleep(2000);
            var birthdayDay = driver.FindElement(By.Id("BirthDay"));
            birthdayDay.SendKeys(Keys.Enter);
            birthdayDay.SendKeys(Keys.ArrowDown);
            birthdayDay.SendKeys(Keys.Enter);
           // System.Threading.Thread.Sleep(2000);
            var birthdayMonth = driver.FindElement(By.Id("BirthMonth"));
            birthdayMonth.SendKeys(Keys.Enter);
            birthdayMonth.SendKeys(Keys.ArrowDown);
            birthdayMonth.SendKeys(Keys.Enter);
            var birthdayYear = driver.FindElement(By.Id("BirthYear"));
            birthdayYear.SendKeys("1999");

            var submitButton2 = driver.FindElement(By.Id("iSignupAction"));
            submitButton2.Click();
            System.Threading.Thread.Sleep(-1);
        }

        static Tuple<string, string> GenerateRandomUserData()
        {
            // Zufälligen Benutzernamen erstellen
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var username = new string(
                Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            // Zufälliges Passwort erstellen
            var password = new string(
                Enumerable.Repeat(chars, 12)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            // Benutzerdaten als Tuple zurückgeben
            return Tuple.Create(username, password);

            
        }
    }
}
