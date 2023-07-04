using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using DataStructuresLib.Files;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TextComposerLib.Text.Linear;
using Configuration = AngleSharp.Configuration;
using WebComposerLib.ImageSharp.Processing.AutoCrop.Extensions;

namespace WebComposerLib.Samples.BabylonJs
{
    public static class BabylonJsSamples
    {
        public static string HtmlTemplateText { get; } = @"
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />

        <title>Babylon.js HTML Template</title>
        
        <!--  -->
        <script src=""https://cdnjs.cloudflare.com/ajax/libs/dat-gui/0.6.2/dat.gui.min.js""></script>
        
        <!--  -->
        <script src=""https://assets.babylonjs.com/generated/Assets.js""></script>
        
        <!--  -->
        <script src=""https://preview.babylonjs.com/ammo.js""></script>
        
        <!--  -->
        <script src=""https://preview.babylonjs.com/cannon.js""></script>
        
        <!--  -->
        <script src=""https://preview.babylonjs.com/Oimo.js""></script>
        
        <!--  -->
        <script src=""https://preview.babylonjs.com/earcut.min.js""></script>
        
        <!-- Babylon.js Core -->
        <script src=""https://cdn.babylonjs.com/babylon.js""></script>
        
        <!-- Babylon.js Supported Advanced Materials -->
        <script src=""https://cdn.babylonjs.com/materialsLibrary/babylonjs.materials.min.js""></script>
        
        <!-- Babylon.js Procedural Textures -->
        <script src=""https://cdn.babylonjs.com/proceduralTexturesLibrary/babylonjs.proceduralTextures.min.js""></script>
        
        <!-- Babylon.js Post Processes -->
        <script src=""https://cdn.babylonjs.com/postProcessesLibrary/babylonjs.postProcess.min.js""></script>
        
        <!-- Babylon.js All Official Loaders (OBJ, STL, glTF) -->
        <script src=""https://cdn.babylonjs.com/loaders/babylonjs.loaders.min.js""></script>
        
        <!-- Babylon.js Scene/Mesh Serializers -->
        <script src=""https://cdn.babylonjs.com/serializers/babylonjs.serializers.min.js""></script>
        
        <!-- Babylon.js GUI -->
        <script src=""https://cdn.babylonjs.com/gui/babylon.gui.min.js""></script>
        
        <!-- Babylon.js Inspector -->
        <script src=""https://cdn.babylonjs.com/inspector/babylon.inspector.bundle.js""></script>
        
        <!-- Babylon.js Viewer -->
        <script src=""https://cdn.babylonjs.com/viewer/babylon.viewer.js""></script>
        
        <!-- KaTeX Core -->
        <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/katex.min.css"" integrity=""sha384-Xi8rHCmBmhbuyyhbI88391ZKP2dmfnOl4rT9ZfRI7mLTdk1wblIUnrIq35nqwEvC"" crossorigin=""anonymous"">
        <script src=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/katex.min.js"" integrity=""sha384-X/XCfMm41VSsqRNQgDerQczD69XqmjOOOwYQvr/uuC+j4OPoNhVgjdGFwhvN02Ja"" crossorigin=""anonymous""></script>
        
        <!-- KaTeX Auto-render extension -->
        <!-- To automatically render math in text elements, include the auto-render extension: -->
        <script src=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/contrib/auto-render.min.js"" integrity=""sha384-+XBljXPPiv+OzfbB3cVmLHf4hdUFHlWNZN5spNQ7rmHTXpd7WvJum6fIACpNNfIR"" crossorigin=""anonymous"" ></script>
        
        <!-- html2canvas.js -->
        <script src=""https://cdnjs.cloudflare.com/ajax/libs/html2canvas/1.4.1/html2canvas.min.js"" integrity=""sha512-BNaRQnYJYiPSqHHDb58B0yaPfCu+Wgds8Gp/gU33kqBtgNS4tSPHuGibyoeqMV/TJlSKda6FXzoEyYGjTe+vXA=="" crossorigin=""anonymous"" referrerpolicy=""no-referrer""></script>
	    
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

        <style>
            html, body {
                overflow: auto;
                width: 100%;
                height: 100%;
                margin: 0;
                padding: 0;
            }

            #renderCanvas {
                position: absolute;
                top: 0px;
                left: 0px;
            	width: 1024px;
            	height: 728px;
            	touch-action: none;
            }

            .katex { font-size: 4em; }
        </style>
    </head>
    <body>
        <canvas id=""renderCanvas""> </canvas>
    </body>
</html>
".Trim();


        public static async void Example1()
        {
            var workingFolder = @"D:\Projects\Active\WebComposerLib\Study";

            var latexCodeArray = new[]
            {
                "x_2-y_1",
                @"f \\left( x \\right) = e^{\\pi/2kt}"
            };

            var htmlCode = await CreateDocument(workingFolder, latexCodeArray);

            SaveKaTeXImages(htmlCode, workingFolder);

            //await File.WriteAllTextAsync(
            //    Path.Combine(workingFolder, "Example1.html"),
            //    htmlCode
            //);
        }

        public static async Task<string> CreateDocument(string workingPath, IEnumerable<string> latexCodeArray)
        {
            //Create initial HTML document
            var config =
                Configuration.Default
                    .WithJs()
                    .WithCss();

            // Create empty document
            var document = await BrowsingContext.New(config).OpenAsync(
                m => m.Content(HtmlTemplateText)
            );

            var documentBody =
                document.Body ?? throw new InvalidOperationException();

            var codeComposer = new LinearTextComposer();

            var i = 0;
            foreach (var latexCode in latexCodeArray)
            {
                var divId = $"katex-div-{i}";

                var divElement = document.CreateElement<IHtmlDivElement>();

                divElement.SetAttribute("id", divId);

                documentBody.AppendChild(divElement);

                codeComposer.AppendLineAtNewLine(
                    @$"katex.render('{latexCode}', document.getElementById('{divId}'), {{throwOnError: true}});"
                );

                i++;
            }
            
            var scriptElement = document.CreateElement<IHtmlScriptElement>();
            scriptElement.TextContent = codeComposer.ToString();
            documentBody.AppendChild(scriptElement);

            //document.ExecuteScript(codeComposer.ToString());

            return document.ToHtml();
        }

        public static string SaveKaTeXImages(string htmlCode, string workingPath)
        {
            //Save html document to local file
            var filePath =
                workingPath.GetFilePath(
                    "katex" + Path.GetFileNameWithoutExtension(Path.GetTempFileName()), 
                    "html"
                );

            File.WriteAllText(
                filePath, 
                htmlCode
            );
            
            // https://www.automatetheplanet.com/selenium-webdriver-csharp-cheat-sheet/
            // Read document and execute javascript
            var chromeOptions = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
            };

            chromeOptions.AddUserProfilePreference("download.default_directory", workingPath);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

            //chromeOptions.AddAdditionalChromeOption("window-size", "1920,1080");
            //chromeOptions.AddArgument("headless");

            var driver = new ChromeDriver(chromeOptions);
            
            driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

            try
            {
                // Open a new tab
                var tabHandleCollection = driver.WindowHandles;

                if (tabHandleCollection.Count < 1)
                    driver.SwitchTo().NewWindow(WindowType.Tab);
                
                driver.SwitchTo().Window(
                    tabHandleCollection.First()
                );

                var fileUri =
                    new UriBuilder()
                    {
                        Scheme = Uri.UriSchemeFile,
                        Host = "",
                        Path = filePath
                    }.Uri.AbsoluteUri;

                driver.Navigate().GoToUrl(fileUri);

                // Wait until a page is fully loaded via JavaScript
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until((x) =>
                {
                    return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
                });

                var divElements = 
                    driver.FindElements(By.TagName("div")).ToArray();

                var index = 0;
                foreach (var divElement in divElements)
                {
                    var screenShot = ((ITakesScreenshot) divElement).GetScreenshot();
                    
                    var image = Image.Load<Rgba32>(screenShot.AsByteArray);

                    image.Mutate(x => x.AutoCrop());

                    image.SaveAsPng(
                        workingPath.GetFilePath($"KaTeX-{index:D6}", "png")
                    );

                    //screenShot.SaveAsFile(
                    //    workingPath.GetFilePath($"KaTeX-{index:D6}", "png"),
                    //    ScreenshotImageFormat.Png
                    //);

                    index++;
                }

                htmlCode = driver.PageSource;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Thread.Sleep(2000);

                driver.Quit();
            }
            
            if (File.Exists(filePath))
                File.Delete(filePath);

            return htmlCode;
        }
    }
}
