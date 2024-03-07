# Outlook Email Account Creator

This program automatically creates email accounts on Outlook, using randomly generated usernames, passwords, first names, and last names. The login details for the created email accounts are saved in a folder called "Accounts" in the same directory as the program.

## DISCLAIMER
Please note that creating accounts through third party software is against the [Microsoft ToS](https://www.microsoft.com/en-us/servicesagreement). Use at your own risk


## Usage

1. Open a command prompt and navigate to the directory where the program is located.
2. Run the program with the command `dotnet run`.
3. The program will open a new Chrome browser and go through the process of creating an Outlook email account.

## Requirements
- [Chrome](https://www.google.com/chrome/) or [Chromium](https://www.chromium.org/getting-involved/download-chromium/)
- [.NET Core](https://dotnet.microsoft.com/download) must be installed.
- [ChromeDriver](https://chromedriver.chromium.org/) (in version 1.1.0+ included) 


## Dependencies

- [Selenium.WebDriver](https://www.nuget.org/packages/Selenium.WebDriver/)
- [Selenium.WebDriver.ChromeDriver](https://www.nuget.org/packages/Selenium.WebDriver.ChromeDriver/)

I hope this helps!
