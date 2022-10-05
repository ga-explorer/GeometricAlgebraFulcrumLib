using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TextComposerLib.Text.Parametric;

namespace GeometricAlgebraFulcrumLib.Samples.Browser
{
    public static class Sample1
    {
        /// <summary>
        /// Code examples for starting and stopping a session with each browser.
        /// https://www.selenium.dev/documentation/webdriver/getting_started/open_browser/
        /// </summary>
        public static void Example1()
        {
            // Once you have a Selenium library installed, and your desired browser driver,
            // you can start and stop a session with a browser.
            // 
            // Typically, browsers are started with specific options that describe which
            // capabilities the browser must support, and how the browser should behave
            // during the session. Some capabilities are shared by all browsers, and some
            // will be specific to the browser being used. This page will show examples of
            // starting a browser with the default capabilities.

            // Chrome:
            // By default, Selenium 4 is compatible with Chrome v75 and greater. Note that
            // the version of the Chrome browser and the version of chromedriver must match
            // the major version.
            // 
            // In addition to the shared capabilities, there are specific Chrome capabilities
            // that can be used.

            var options = new ChromeOptions();
            var driver = new ChromeDriver(options);

            driver.Quit();


            // Edge:
            // Microsoft Edge is implemented with Chromium, with the earliest supported
            // version of v79. Similar to Chrome, the major version number of edgedriver
            // must match the major version of the Edge browser.

            //var options = new EdgeOptions();
            //var driver = new EdgeDriver(options);
  
            //driver.Quit();


            // Firefox:
            // Selenium 4 requires Firefox 78 or greater. It is recommended to always use
            // the latest version of geckodriver.

            //var options = new FirefoxOptions();
            //var driver = new FirefoxDriver(options);
  
            //driver.Quit();


            // Internet Explorer:
            // The IE Driver is the only driver maintained by the Selenium Project directly.
            // While binaries for both the 32-bit and 64-bit versions of Internet Explorer are
            // available, there are some limitations with the 64-bit driver. As such it is
            // recommended to use the 32-bit driver.
            // 
            // Legacy
            // The Selenium project aims to support the same releases that Microsoft considers
            // current. Older releases may work, but will not be supported. Note that Internet
            // Explorer 11 will end support for certain operating systems, including Windows 10
            // on June 15, 2022.
            // 
            // It should be noted that as Internet Explorer preferences are saved against the
            // logged-in user’s account, some additional setup is required.
            // 
            // Additional information about using Internet Explorer can be found on the Selenium wiki

            //var options = new InternetExplorerOptions();
            //var driver = new InternetExplorerDriver(options);
  
            //driver.Quit();


            // Edge Compatibility Mode:
            // Microsoft Edge can be used in IE compatibility mode using the IE Driver.

            //var options = new InternetExplorerOptions
            //{
            //    AttachToEdgeChrome = true,
            //    EdgeExecutablePath = "/path/to/edge/browser"
            //};
            //var driver = new InternetExplorerDriver(options);
  
            //driver.Quit();


            // Opera:
            // Since the opera driver does not set w3c as default value, but is based on Chromium,
            // it is recommended to drive Opera browser with the chromedriver. Like all Chromium
            // implementations, make sure that the browser version matches the driver version.

            //var options = new ChromeOptions
            //{
            //    BinaryLocation = "/path/to/opera/browser"
            //};
            //var driver = new ChromeDriver(options);
  
            //driver.Quit();
        }

        /// <summary>
        /// Step-by-step instructions for constructing a Selenium script
        /// https://www.selenium.dev/documentation/webdriver/getting_started/first_script/
        /// </summary>
        public static void Example2()
        {
            // Everything Selenium does is send the browser commands to do something
            // or send requests for information. Most of what you’ll do with Selenium
            // is a combination of these basic commands:

            // 1. Start the session
            var driver = new ChromeDriver();

            // 2. Take action on browser
            driver.Navigate().GoToUrl("https://google.com");

            // 3. Request browser information
            // There are a bunch of types of information about the browser you can request,
            // including window handles, browser size / position, cookies, alerts, etc.
            var title = driver.Title;

            // 4. Establish Waiting Strategy
            // Synchronizing the code with the current state of the browser is one of the
            // biggest challenges with Selenium, and doing it well is an advanced topic.
            // 
            // Essentially you want to make sure that the element is on the page before you
            // attempt to locate it and the element is in an interactable state before you
            // attempt to interact with it.
            // 
            // An implicit wait is rarely the best solution, but it’s the easiest to
            // demonstrate here, so we’ll use it as a placeholder.
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            // 5. Find an element
            // The majority of commands in most Selenium sessions are element related, and
            // you can’t interact with one without first finding an element
            var searchBox = driver.FindElement(By.Name("q"));
            var searchButton = driver.FindElement(By.Name("btnK"));

            // 6. Take action on element
            // There are only a handful of actions to take on an element, but you will use
            // them frequently.
            searchBox.SendKeys("Selenium");
            searchButton.Click();

            // 7. Request element information
            // Elements store a lot of information that can be requested. Notice that we need
            // to relocate the search box because the DOM has changed since we first located it.
            searchBox = driver.FindElement(By.Name("q"));
            var value = searchBox.GetAttribute("value");

            // 8. End the session
            // This ends the driver process, which by default closes the browser as well. No
            // more commands can be sent to this driver instance.
            driver.Quit();

            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Value: {value}");
            Console.WriteLine();
        }

        /// <summary>
        /// Creating browser options and specifying PageLoadStrategy
        /// https://www.selenium.dev/documentation/webdriver/capabilities/shared/
        /// </summary>
        public static void Example3()
        {
            var chromeOptions = new ChromeOptions
            {
                // This will make Selenium WebDriver to wait for the entire page is loaded.
                // When set to normal, Selenium WebDriver waits until the load event fire
                // is returned. By default normal is set to browser if none is provided.
                PageLoadStrategy = PageLoadStrategy.Normal

                // This will make Selenium WebDriver to wait until the initial HTML document
                // has been completely loaded and parsed, and discards loading of stylesheets,
                // images and subframes. When set to eager, Selenium WebDriver waits until
                // DOMContentLoaded event fire is returned.
                //PageLoadStrategy = PageLoadStrategy.Eager

                // When set to none Selenium WebDriver only waits until the initial page is
                // downloaded.
                //PageLoadStrategy = PageLoadStrategy.None
            };

            var driver = new ChromeDriver(chromeOptions);

            try
            {
                driver.Navigate().GoToUrl("https://google.com");

                var url = driver.Url;
                var title = driver.Title;
                
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

                var searchBox = driver.FindElement(By.Name("q"));
                var searchButton = driver.FindElement(By.Name("btnK"));

                searchBox.SendKeys("Selenium");
                searchButton.Click();

                searchBox = driver.FindElement(By.Name("q"));
                var value = searchBox.GetAttribute("value");

                Console.WriteLine($"URL: {url}");
                Console.WriteLine($"Title: {title}");
                Console.WriteLine($"Value: {value}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally 
            {
                driver.Quit();
            }
        }

        
        /// <summary>
        /// Operations on browser windows and tabs
        /// https://www.selenium.dev/documentation/webdriver/capabilities/shared/
        /// </summary>
        public static void Example4()
        {
            var chromeOptions = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };

            var driver = new ChromeDriver(chromeOptions);

            try
            {
                driver.Navigate().GoToUrl("https://google.com");

                var url = driver.Url;
                var title = driver.Title;
                
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

                var searchBox = driver.FindElement(By.Name("q"));
                var searchButton = driver.FindElement(By.Name("btnK"));

                searchBox.SendKeys("Selenium");
                searchButton.Click();

                searchBox = driver.FindElement(By.Name("q"));
                var value = searchBox.GetAttribute("value");

                Console.WriteLine($"URL: {url}");
                Console.WriteLine($"Title: {title}");
                Console.WriteLine($"Value: {value}");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally 
            {
                driver.Quit();
            }
        }

        /// <summary>
        /// Generate and open HTML containing KaTeX math
        /// </summary>
        public static void Example5()
        {
            var htmlTemplateText = @"
<!DOCTYPE html>
<!-- KaTeX requires the use of the HTML5 doctype. Without it, KaTeX may not render properly -->
<html>
  <head>
    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/katex@0.16.0/dist/katex.min.css"" integrity=""sha384-Xi8rHCmBmhbuyyhbI88391ZKP2dmfnOl4rT9ZfRI7mLTdk1wblIUnrIq35nqwEvC"" crossorigin=""anonymous"">

    <!-- The loading of KaTeX is deferred to speed up page rendering -->
    <!-- script src=""https://cdn.jsdelivr.net/npm/katex@0.16.0/dist/katex.min.js"" integrity=""sha384-X/XCfMm41VSsqRNQgDerQczD69XqmjOOOwYQvr/uuC+j4OPoNhVgjdGFwhvN02Ja"" crossorigin=""anonymous""></script -->
	
	<script src=""katex/katex.min.js""></script>

    <!-- To automatically render math in text elements, include the auto-render extension: -->
    <!-- script src=""https://cdn.jsdelivr.net/npm/katex@0.16.0/dist/contrib/auto-render.min.js"" integrity=""sha384-+XBljXPPiv+OzfbB3cVmLHf4hdUFHlWNZN5spNQ7rmHTXpd7WvJum6fIACpNNfIR"" crossorigin=""anonymous"" ></script -->
	
	<script src=""katex/contrib/auto-render.min.js""></script>
	
    <script>
      document.addEventListener(""DOMContentLoaded"", function() {
        renderMathInElement(document.body, {
          delimiters: 
		  [
            {left: ""$$"", right: ""$$"", display: true},
			{left: '$', right: '$', display: false},
            {left: ""\\("", right: ""\\)"", display: false},
            {left: ""\\begin{equation}"", right: ""\\end{equation}"", display: true},
            {left: ""\\begin{align}"", right: ""\\end{align}"", display: true},
            {left: ""\\begin{alignat}"", right: ""\\end{alignat}"", display: true},
            {left: ""\\begin{gather}"", right: ""\\end{gather}"", display: true},
            {left: ""\\begin{CD}"", right: ""\\end{CD}"", display: true},
            {left: ""\\["", right: ""\\]"", display: true}
          ],
          throwOnError : false
        });
      });
    </script>
  </head>
  <body>
    <div id=""text-div"">
	  This equation: $#autorender-equation#$ is inline.
	</div>
    <div id=""katex-div"">
      <script>
	    const katexDivElement = document.getElementById(""katex-div"");
		
        katex.render(""#callrender-equation#"", katexDivElement, {throwOnError: true});
      </script>
    </div>
  </body>
</html>
".Trim();

            var composer = new ParametricTextComposer(
                "#", 
                "#", 
                htmlTemplateText
            );

            var htmlCode = composer.GenerateUsing(
                @"c = a^2 + b^2", 
                @"c = \\pm\\sqrt{a^2 + b^2}"
            );

            const string workingPath = @"D:\Projects\Study\KaTeX\";

            var filePath = Path.Combine(workingPath, @"katex-example-1.html");

            File.WriteAllText(filePath, htmlCode);

            var chromeOptions = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };

            var uri = new Uri(filePath).AbsoluteUri;

            var driver = new ChromeDriver(chromeOptions);

            try
            {
                driver.Navigate().GoToUrl(uri);

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

                var webElement = driver.FindElement(By.Id("text-div"));

                var elementScreenShot = 
                    ((ITakesScreenshot) webElement).GetScreenshot();

                elementScreenShot.SaveAsFile(
                    Path.Combine(workingPath, "text-div Screen Shot.png"), 
                    ScreenshotImageFormat.Png
                );

                webElement = driver.FindElement(By.Id("katex-div"));

                elementScreenShot = 
                    ((ITakesScreenshot) webElement).GetScreenshot();

                elementScreenShot.SaveAsFile(
                    Path.Combine(workingPath, "katex-div Screen Shot.png"), 
                    ScreenshotImageFormat.Png
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally 
            {
                driver.Quit();
            }
        }
    }
}
