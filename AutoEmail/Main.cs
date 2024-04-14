using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

            Console.WriteLine("Möchten Sie eine neue E-Mail erstellen oder eine vorhandene verwenden?\nNew Email [1]\nExisting Email [2]");
            string antwort = Console.ReadLine().ToLower();
            Console.Clear();

            
            

            if (antwort == "1")
            {
                // Initalisieren des ChromeDrivers
                Console.WriteLine("Initialisiere ChromeDriver...");
                var options = new ChromeOptions();
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-infobars");
                options.AddArgument("--incognito");
                var service = ChromeDriverService.CreateDefaultService();
                service.SuppressInitialDiagnosticInformation = true;
                service.HideCommandPromptWindow = true;
                ChromeDriver driver = new ChromeDriver(service, options);
                Console.WriteLine("ChromeDriver wurde erfolgreich initialisiert.");

                // Generieren und Speichern von neuen Benutzerdaten.
                Console.WriteLine("Generiere neue Benutzerdaten...");
                var userData = GenerateRandomUserData();
                var username = userData.Item1;
                var firstname = userData.Item1;
                var lastname = userData.Item1;
                var password = userData.Item2;
                var email = $"C{username}";
                Console.WriteLine("Benutzerdaten wurden erfolgreich generiert.");

                // Benutzerdaten in einer ordnerstruktur, die sich im selben Verzeichnis wie das Programm befindet, speichern Account/Outlook/username.txt
                System.IO.Directory.CreateDirectory("Accounts/Outlook");
                System.IO.File.WriteAllText($"Accounts/Outlook/C{username}@outlook.de.txt", $"Email: C{username}@outlook.de\nPassword: {password}\n Firstname: {firstname}\n Lastname: {lastname}");
                Console.WriteLine("Benutzerdaten wurden erfolgreich gespeichert.");

                // Website für das Erstellen eines E-Mail-Accounts bei Outlook aufrufen
                Console.WriteLine("Öffne Website für die Erstellung eines E-Mail-Accounts...");
                driver.Navigate().GoToUrl("https://signup.live.com/signup?lcid=1033&wa=wsignin1.0&rpsnv=13&ct=1672112140&rver=7.0.6737.0&wp=MBI_SSL&wreply=https%3a%2f%2foutlook.live.com%2fowa%2f%3fnlp%3d1%26signup%3d1%26RpsCsrfState%3dec907f44-5c7c-95bc-fb36-fbb9207b2321&id=292841&CBCXT=out&lw=1&fl=dob%2cflname%2cwld&cobrandid=90015&lic=1&uaid=13dee67bbb024f75b33a579b5e53cfb5");

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
                var birthdayMonth = driver.FindElement(By.Id("BirthMonth"));
                birthdayMonth.SendKeys(Keys.Enter);
                birthdayMonth.SendKeys(Keys.ArrowDown);
                birthdayMonth.SendKeys(Keys.Enter);
                var birthdayYear = driver.FindElement(By.Id("BirthYear"));
                birthdayYear.SendKeys("1999");

                var submitButton2 = driver.FindElement(By.Id("iSignupAction"));
                submitButton2.Click();
                Console.WriteLine("Email was created successfully!\nThe Capcha is not implemented yet, so you have to solve it manually.\nPress any key to close the browser.");


                Console.WriteLine("If you want to close the browser press any key!");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Don't close this console via the X button! Otherwise the ChromeDriver.exe will not be closed!");


                if (Console.ReadKey() != null)
                {
                    Console.Clear();
                    //resette die Farben with Console.ResetColor();
                    Console.ResetColor();
                    Console.WriteLine("if you sure to close the browser press any key again!");
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Disclaimer: If you close the console, the Browser will close too!");
                    Console.ReadKey();
                    driver.Quit();
                }


                //close chromedriver.exe 
                Console.WriteLine("If you want to close the browser press any key!");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Don't close this console via the X button! Otherwise the ChromeDriver.exe will not be closed!");


                if (Console.ReadKey() != null)
                {
                    Console.Clear();
                    //resette die Farben with Console.ResetColor();
                    Console.ResetColor();
                    Console.WriteLine("if you sure to close the browser press any key again!");
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("Disclaimer: If you close the console, the Browser will close too!");
                    Console.ReadKey();
                    driver.Quit();
                }
            }
            else if (antwort == "2")
            {
                // Vorhandene E-Mail verwenden
                Console.WriteLine("Geben Sie den Benutzernamen der vorhandenen E-Mail ein:");
                string existierenderBenutzername = Console.ReadLine();
                string pfadZurDatei = $"Accounts/Outlook/{existierenderBenutzername}.txt";



                if (System.IO.File.Exists(pfadZurDatei))
                {
                    Console.Clear();
                    // Initalisieren des ChromeDrivers
                    var options = new ChromeOptions();
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--disable-infobars");
                    options.AddArgument("--incognito");
                    var service = ChromeDriverService.CreateDefaultService();
                    service.SuppressInitialDiagnosticInformation = true;
                    service.HideCommandPromptWindow = true;
                    ChromeDriver driver = new ChromeDriver(service, options);

                    // Lese die Datei und verwende die Daten für den Login
                    string[] daten = System.IO.File.ReadAllLines(pfadZurDatei);
                    string email = daten[0].Split(new string[] { ": " }, StringSplitOptions.None)[1].Trim();
                    string password = daten[1].Split(new string[] { ": " }, StringSplitOptions.None)[1].Trim();

                    //Login-Seite
                    driver.Navigate().GoToUrl("https://login.live.com/");
                    // Andmelden
                    Thread.Sleep(1000);
                    var usernameField = driver.FindElement(By.Id("i0116"));
                    usernameField.SendKeys(email);
                    var nextButton = driver.FindElement(By.Id("idSIButton9"));
                    nextButton.Click();
                    System.Threading.Thread.Sleep(2000);
                    var passwordField = driver.FindElement(By.Id("i0118"));
                    passwordField.SendKeys(password);
                    var submitButton = driver.FindElement(By.Id("idSIButton9"));
                    submitButton.Click();

                    //close chromedriver.exe 
                    Console.WriteLine("If you want to close the browser press any key!");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Don't close this console via the X button! Otherwise the ChromeDriver.exe will not be closed!");


                    if (Console.ReadKey() != null)
                    {
                        Console.Clear();
                        //resette die Farben with Console.ResetColor();
                        Console.ResetColor();
                        Console.WriteLine("if you sure to close the browser press any key again!");
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Disclaimer: If you close the console, the Browser will close too!");
                        Console.ReadKey();
                        driver.Quit();
                    }

                }
                else
                {
                    Console.WriteLine("Keine Daten für den angegebenen Benutzernamen gefunden. Programm wird beendet.");
                    System.Threading.Thread.Sleep(2000);
                    return; 
                }
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe.");
                System.Threading.Thread.Sleep(2000);
                return;
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
}
