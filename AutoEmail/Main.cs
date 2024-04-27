using System;
using System.IO;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // URL der Webseite über die Konsole abfragen
            Console.Write("Bitte geben Sie die URL der Webseite ein: ");
            string url = Console.ReadLine();

            // Pfad zum Ordner, in dem die GIFs gespeichert werden sollen
            string downloadFolderPath = @"C:\Downloads";

            // Pfad zum Ordner, in dem der Chromedriver gespeichert wird
            string chromeDriverPath = @"C:\chromedriver";

            // Pfad zum Chrome-Browser
            string chromeBrowserPath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

            // Bestimme die installierte Chrome-Version
            string chromeVersion = GetChromeVersion();
            Console.WriteLine($"Installierte Chrome-Version: {chromeVersion}");

            // Konstruiere den Download-URL für den Chromedriver
            string chromeDriverDownloadUrl = $"https://chromedriver.storage.googleapis.com/{chromeVersion}/chromedriver_win32.zip";

            // Pfad zum heruntergeladenen Chromedriver
            string chromedriverZipPath = Path.Combine(chromeDriverPath, "chromedriver_win32.zip");
            string chromedriverExePath = Path.Combine(chromeDriverPath, "chromedriver.exe");

            // Chromedriver herunterladen, wenn er noch nicht vorhanden ist
            if (!File.Exists(chromedriverExePath))
            {
                Console.WriteLine("ChromeDriver wird heruntergeladen...");
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(chromeDriverDownloadUrl, chromedriverZipPath);
                }
                Console.WriteLine("ChromeDriver wurde erfolgreich heruntergeladen.");

                // Chromedriver entpacken
                Console.WriteLine("Entpacke ChromeDriver...");
                System.IO.Compression.ZipFile.ExtractToDirectory(chromedriverZipPath, chromeDriverPath);
                Console.WriteLine("ChromeDriver wurde erfolgreich entpackt.");

                // Chromedriver-Zip-Datei löschen
                File.Delete(chromedriverZipPath);
            }

            // ChromeDriver initialisieren
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless"); // Browser im Hintergrund ausführen (ohne GUI)
            options.BinaryLocation = chromeBrowserPath;
            IWebDriver driver = new ChromeDriver(chromeDriverPath, options);

            // Webseite laden
            driver.Navigate().GoToUrl(url);

            // Alle img-Tags auf der Seite abrufen
            var imgElements = driver.FindElements(By.TagName("img"));

            foreach (var imgElement in imgElements)
            {
                string imgUrl = imgElement.GetAttribute("src");

                // Wenn die Quelle des Bildes mit ".gif" endet, wird es heruntergeladen
                if (!string.IsNullOrEmpty(imgUrl) && imgUrl.EndsWith(".gif"))
                {
                    // GIF-Bild herunterladen
                    DownloadGif(imgUrl, downloadFolderPath);
                }
            }

            // ChromeDriver beenden
            driver.Quit();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ein Fehler ist aufgetreten:");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }

    static string GetChromeVersion()
    {
        // Der Pfad zur Registrierungsschlüssel
        string keyPath = @"SOFTWARE\Google\Chrome";

        // Der Name des Werts, der die Version enthält
        string valueName = "Version";

        // Versuche, die Chrome-Version aus der Registrierung zu lesen
        using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyPath))
        {
            if (key != null)
            {
                object version = key.GetValue(valueName);
                if (version != null)
                {
                    return version.ToString().Split('.')[0];
                }
            }
        }

        // Wenn die Version nicht gefunden wurde, gib eine Fehlermeldung zurück
        throw new Exception("Chrome-Version nicht gefunden.");
    }

    static void DownloadGif(string imgUrl, string downloadFolderPath)
    {
        // GIF-Bild herunterladen
        using (WebClient webClient = new WebClient())
        {
            // Dateiname aus der URL extrahieren
            string fileName = Path.GetFileName(imgUrl);
            // Den vollständigen Pfad zum Speichern des Bildes erstellen
            string filePath = Path.Combine(downloadFolderPath, fileName);
            // Bild herunterladen und im angegebenen Ordner speichern
            webClient.DownloadFile(imgUrl, filePath);
            Console.WriteLine($"GIF heruntergeladen: {filePath}");
        }
    }
}
