using System.Globalization;
using AngleSharp.Html.Dom;
using AngleSharp;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using AngleSharp.Dom;
using DataStructuresLib.Files;
using TextComposerLib;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;
using WebComposerLib.ImageSharp.Processing.AutoCrop.Extensions;
using System.Net;

namespace WebComposerLib.LaTeX.KaTeX
{
    /// <summary>
    /// https://github.com/KaTeX/KaTeX
    /// https://katex.org/docs/supported.html
    /// </summary>
    public class WclKaTeXComposer
    {
        //For Unicode Direct Input use these sites:
        //https://unicodelookup.com/
        //https://r12a.github.io/app-conversion/
        //https://r12a.github.io/uniview/?charlist=<put_unicode_characters_here>#title


        public enum OutputKind
        {
            Html = 1,
            MathMl = 2,
            HtmlAndMathMl = 3
        }

        public static string HtmlTemplateText { get; } = @"
<!DOCTYPE html>
<html>
    <head>
        <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />

        <title>KaTeX HTML Template</title>

        <!-- KaTeX Core -->
        <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/katex@0.16.0/dist/katex.min.css"" integrity=""sha384-Xi8rHCmBmhbuyyhbI88391ZKP2dmfnOl4rT9ZfRI7mLTdk1wblIUnrIq35nqwEvC"" crossorigin=""anonymous"">
        <script src=""https://cdn.jsdelivr.net/npm/katex@0.16.0/dist/katex.min.js"" integrity=""sha384-X/XCfMm41VSsqRNQgDerQczD69XqmjOOOwYQvr/uuC+j4OPoNhVgjdGFwhvN02Ja"" crossorigin=""anonymous""></script>
        
        <!-- KaTeX Auto-render extension -->
        <!-- To automatically render math in text elements, include the auto-render extension: -->
        <script src=""https://cdn.jsdelivr.net/npm/katex@0.16.0/dist/contrib/auto-render.min.js"" integrity=""sha384-+XBljXPPiv+OzfbB3cVmLHf4hdUFHlWNZN5spNQ7rmHTXpd7WvJum6fIACpNNfIR"" crossorigin=""anonymous"" ></script>
        
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

        <script>
          window.WebFontConfig = {
            custom: {
              families: ['KaTeX_AMS', 'KaTeX_Caligraphic:n4,n7', 'KaTeX_Fraktur:n4,n7',
                'KaTeX_Main:n4,n7,i4,i7', 'KaTeX_Math:i4,i7', 'KaTeX_Script',
                'KaTeX_SansSerif:n4,n7,i4', 'KaTeX_Size1', 'KaTeX_Size2', 'KaTeX_Size3',
                'KaTeX_Size4', 'KaTeX_Typewriter'],
            },
          };
        </script>

        <script defer src=""https://cdn.jsdelivr.net/npm/webfontloader@1.6.28/webfontloader.js"" integrity=""sha256-4O4pS1SH31ZqrSO2A/2QJTVjTPqVe+jnYgOWUVr7EEc="" crossorigin=""anonymous""></script>
        
        <style>
            html, body {
                overflow: auto;
                width: 100%;
                height: 100%;
                margin: 0;
                padding: 0;
            }

            .katex { font-size: #font-size#; }
        </style>
    </head>
    <body>
        <script>
            function svgToDataURI(svg) {
                svg = svg.trim();

                // remove xml, doctype, generator...
                svg = svg.slice(svg.indexOf('<svg'));

                // soft validate
                if (!svg.startsWith('<svg') || !svg.endsWith('svg>')) return;

                // add namespace if necessary
                if (!svg.includes('http://www.w3.org/2000/svg')) svg = svg.replace(/<svg/g, `<svg xmlns='http://www.w3.org/2000/svg'`);

                // remove comments
                svg = svg.replace(/<!--.{1,}-->/g, '');

                // remove unnecessary attributes
                svg = svg.replace(/version=[\""\'](.{0,}?)[\""\'](?=[\s>])/g, '');

                // svg = svg.replace(/id=[\""\'](.{0,}?)[\""\'](?=[\s>])/g, '');
                // svg = svg.replace(/class=[\""\'](.{0,}?)[\""\'](?=[\s>])/g, '');

                // replace nested quotes
                svg = svg.replace(/""'(.{1,})'""/g, '\'$1\'');

                // replace double quotes
                svg = svg.replace(/""/g, '\'');

                // remove empty spaces between tags
                svg = svg.replace(/>\s{1,}</g, '><');

                // remove duplicate spaces
                svg = svg.replace(/\s{2,}/g, ' ');

                // trim again
                svg = svg.trim();

                // soft validate again
                if (!(svg.startsWith('<svg')) || !(svg.endsWith('svg>'))) return;

                // replace ampersand
                svg = svg.replace(/&/g, '&amp;');

                // encode only unsafe symbols
                svg = svg.replace(/[%#<>?\[\\\]^`{|}]/g, encodeURIComponent);

                // build data uri
                svg = `data:image/svg+xml,${svg}`;

                // ok, ship it!
                return svg;
            }
        </script>
    </body>
</html>
".Trim();


        public string WorkingFolder { get; }

        public List<string> KaTeXCodeList { get; }
            = new List<string>();

        public List<Image<Rgba32>> KaTeXPngImageList { get; }
            = new List<Image<Rgba32>>();
        
        public List<string> KaTeXSvgCodeList { get; }
            = new List<string>();

        public List<string> KaTeXSvgDataUriList { get; }
            = new List<string>();

        public List<string> KaTeXImageFileName { get; }
            = new List<string>();

        public bool SaveImages { get; set; } 
            = false;

        public bool DisplayMode { get; set; }
            = false;

        public OutputKind Output { get; set; }
            = OutputKind.HtmlAndMathMl;

        public bool ThrowOnError { get; set; }
            = true;

        public Color ErrorColor { get; set; }
            = Color.DarkRed;

        public float FontSizeEm { get; set; }
            = 1.21f;


        public WclKaTeXComposer()
            : this(string.Empty)
        {

        }

        public WclKaTeXComposer(string workingFolder)
        {
            if (workingFolder.IsNullOrEmpty())
                workingFolder = Path.GetTempPath();

            if (!Path.Exists(workingFolder))
                throw new DirectoryNotFoundException(workingFolder);

            WorkingFolder = workingFolder;
        }


        /// <summary>
        /// Converts the value of this instance to a hexadecimal string.
        /// </summary>
        /// <returns>A hexadecimal string representation of the value.</returns>
        public static string RgbToHex(Color color)
        {
            var c = color.ToPixel<Rgb24>();

            var hexOrder = (uint)(c.B << 0 | c.G << 8 | c.R << 16);

            return hexOrder.ToString("X6", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the value of this instance to a hexadecimal string.
        /// </summary>
        /// <returns>A hexadecimal string representation of the value.</returns>
        public static string RgbaToHex(Color color)
        {
            var c = color.ToPixel<Rgba32>();

            var hexOrder = (uint)(c.A << 0 | c.B << 8 | c.G << 16 | c.R << 24);

            return hexOrder.ToString("X8", CultureInfo.InvariantCulture);
        }

        private string GetOptionsCode()
        {
            var displayMode =
                DisplayMode ? "displayMode: true" : string.Empty;

            var output = Output switch
            {
                OutputKind.Html => "output: 'html'",
                OutputKind.MathMl => "output: 'mathml'",
                _ => string.Empty
            };

            var throwOnError =
                ThrowOnError ? string.Empty : "throwOnError: false";

            var errorColor =
                $"errorColor: '#{RgbToHex(ErrorColor)}'";

            return new[]
            {
                displayMode,
                output,
                throwOnError,
                errorColor
            }
                .Where(s => !s.IsNullOrEmpty())
                .Concatenate(", ", "{", "}");
        }


//        private async Task<string> CreateBaseHtmlDocument()
//        {
//            var htmlCode =
//                HtmlTemplateText.Replace(
//                    "#font-size#",
//                    FontSizeEm.ToString("N2") + "em"
//                );

//            //Create initial HTML document
//            var config =
//                AngleSharp.Configuration.Default
//                    .WithCss();

//            // Create empty document
//            var document = await BrowsingContext.New(config).OpenAsync(
//                m => m.Content(htmlCode)
//            );

//            return document.ToHtml();
//        }

//        private void RenderKaTeXSvgDataUrlStrings(string htmlCode)
//        {
//            KaTeXSvgDataUrlList.Clear();

//            //Save html document to local file
//            var filePath =
//                WorkingFolder.GetFilePath(
//                    "katex" + Path.GetFileNameWithoutExtension(Path.GetTempFileName()),
//                    "html"
//                );

//            File.WriteAllText(
//                filePath,
//                htmlCode
//            );

//            // https://www.automatetheplanet.com/selenium-webdriver-csharp-cheat-sheet/
//            // Read document and execute javascript
//            var chromeOptions = new ChromeOptions
//            {
//                PageLoadStrategy = PageLoadStrategy.Normal,
//                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
//            };

//            chromeOptions.AddUserProfilePreference("download.default_directory", WorkingFolder);
//            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
//            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

//            //chromeOptions.AddAdditionalChromeOption("window-size", "1920,1080");
//            chromeOptions.AddArgument("headless");

//            var driver = new ChromeDriver(chromeOptions);

//            driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
//            driver.Manage().Window.Maximize();
//            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);

//            try
//            {
//                // Open a new tab
//                var tabHandleCollection = driver.WindowHandles;

//                if (tabHandleCollection.Count < 1)
//                    driver.SwitchTo().NewWindow(WindowType.Tab);

//                driver.SwitchTo().Window(
//                    tabHandleCollection.First()
//                );

//                var fileUri =
//                    new UriBuilder()
//                    {
//                        Scheme = Uri.UriSchemeFile,
//                        Host = "",
//                        Path = filePath
//                    }.Uri.AbsoluteUri;

//                driver.Navigate().GoToUrl(fileUri);

//                // Wait until a page is fully loaded via JavaScript
//                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
//                wait.Until((x) =>
//                {
//                    return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
//                });

//                var optionsCode = GetOptionsCode();

//                var index = 0;
//                foreach (var latexCode in KaTeXCodeList)
//                {
//                    var latexHtmlString =
//                        WebUtility.HtmlEncode(
//                            latexCode.Replace(@"\", @"\\")
//                        );

//                    var (width, height) = KaTeXPngImageList[index].Size;
                    
//                    var katexScript = @$"
//const katexHtml = katex.renderToString('{latexHtmlString}', {optionsCode});

//const svg = `
//<?xml version=""1.0"" encoding=""utf-8""?>
//<!-- generator: software -->
//<svg version=""1.1"" id=""html5"" xmlns=""http://www.w3.org/2000/svg"" xmlns:xlink=""http://www.w3.org/1999/xlink"" x=""0px"" y=""0px"" width=""{width}"" height=""{height}"" viewBox=""0 0 50 50"" enable-background=""new 0 0 50 50"" xml:space=""preserve"">
//<title>KaTeX Equation</title>
//<foreignObject width=""100%"" height=""100%"">
//	<div xmlns=""http://www.w3.org/1999/xhtml"">${{katexHtml}}</div>
//</foreignObject>
//</svg>
//`;

//return svgToDataURI(svg);

////const svgBlob = new Blob( [svg], {{ type: 'image/svg+xml;charset=utf-8' }} );
////const svgObjectUrl = URL.createObjectURL( svgBlob );

////return svg;
//".Trim();

//                    var svgDataUrl =
//                        ((IJavaScriptExecutor)driver)
//                        .ExecuteScript(katexScript)
//                        .ToString() ?? string.Empty;

//                    //File.WriteAllText(
//                    //    WorkingFolder.GetFilePath($"KaTeX-{index:D6}", "svg"),
//                    //    svgCode
//                    //);

//                    KaTeXSvgDataUrlList.Add(svgDataUrl.DoubleQuote());

//                    //index++;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error: {ex.Message}");
//            }
//            finally
//            {
//                Thread.Sleep(200);

//                driver.Quit();
//            }

//            if (File.Exists(filePath))
//                File.Delete(filePath);
//        }

//        public async void RenderKaTeXSvgDataUrlStrings()
//        {
//            RenderKaTeX();

//            var htmlCode = await CreateBaseHtmlDocument();

//            RenderKaTeXSvgDataUrlStrings(htmlCode);
//        }


        private async Task<string> CreateFullHtmlDocument()
        {
            var htmlCode =
                HtmlTemplateText.Replace(
                    "#font-size#",
                    FontSizeEm.ToString("N2") + "em"
                );

            //Create initial HTML document
            var config =
                AngleSharp.Configuration.Default
                    .WithCss();

            // Create empty document
            var document = await BrowsingContext.New(config).OpenAsync(
                m => m.Content(htmlCode)
            );

            var documentBody =
                document.Body ?? throw new InvalidOperationException();

            var codeComposer = new LinearTextComposer();

            var i = 0;
            foreach (var latexCode in KaTeXCodeList)
            {
                var divId = $"katex-div-{i}";

                var divElement = document.CreateElement<IHtmlDivElement>();

                divElement.SetAttribute("class", "katexDiv");
                divElement.SetAttribute("id", divId);

                documentBody.AppendChild(divElement);

                var optionsCode = GetOptionsCode();

                var latexHtmlString =
                    WebUtility.HtmlEncode(
                        latexCode.Replace(@"\", @"\\")
                    );

                codeComposer.AppendLineAtNewLine(
                    @$"katex.render('{latexHtmlString}', document.getElementById('{divId}'), {optionsCode});"
                );

                i++;
            }

            var scriptElement = document.CreateElement<IHtmlScriptElement>();
            scriptElement.TextContent = codeComposer.ToString();
            documentBody.AppendChild(scriptElement);

            return document.ToHtml();
        }

        private void RenderKaTeX(string htmlCode)
        {
            KaTeXPngImageList.Clear();
            KaTeXSvgCodeList.Clear();
            KaTeXSvgDataUriList.Clear();
            KaTeXImageFileName.Clear();

            //Save html document to local file
            var filePath =
                WorkingFolder.GetFilePath(
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

            chromeOptions.AddUserProfilePreference("download.default_directory", WorkingFolder);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

            //chromeOptions.AddAdditionalChromeOption("window-size", "1920,1080");
            chromeOptions.AddArgument("headless");

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
                    var screenShot = ((ITakesScreenshot)divElement).GetScreenshot();

                    var image = Image.Load<Rgba32>(screenShot.AsByteArray);

                    // Crop white space around equation
                    image.Mutate(x => x.AutoCrop());

                    // Replace white background with transparent background
                    // https://docs.sixlabors.com/articles/imagesharp/pixelbuffers.html
                    image.ProcessPixelRows(accessor =>
                    {
                        // Color is pixel-agnostic, but it's implicitly convertible to the Rgba32 pixel type
                        //Rgba32 transparent = Color.Transparent;

                        for (var y = 0; y < accessor.Height; y++)
                        {
                            var pixelRow = accessor.GetRowSpan(y);

                            // pixelRow.Length has the same value as accessor.Width,
                            // but using pixelRow.Length allows the JIT to optimize away bounds checks:
                            for (var x = 0; x < pixelRow.Length; x++)
                            {
                                // Get a reference to the pixel at position x
                                ref var pixel = ref pixelRow[x];

                                pixel.A = (byte)(255 - pixel.ToGrayscale());

                                //if (pixel.R == 255 && pixel.G == 255 && pixel.B == 255)
                                //{
                                //    // Overwrite the pixel referenced by 'ref Rgba32 pixel':
                                //    pixel = transparent;
                                //}
                            }
                        }
                    });
                    
                    KaTeXPngImageList.Add(image);

                    var (width, height) = image.Size;
                    //var heightPercentage = 100;
                    //var widthPercentage = (int) Math.Ceiling(100 * height / (double)width);

                    var katexHtml =
                        ((IJavaScriptExecutor)driver).ExecuteScript(
                            @$"return document.getElementById('{divElement.GetAttribute("id")}').innerHTML;"
                        ).ToString() ?? string.Empty;

                    var svgCode = @$"
<?xml version='1.0' encoding='utf-8'?>
<svg version='1.1' id='html5' xmlns='http://www.w3.org/2000/svg' xmlns:xlink='http://www.w3.org/1999/xlink' x='0px' y='0px' width='{width}' height='{height}' viewBox='0 0 {width} {height}' enable-background='new 0 0 {width} {height}' xml:space='preserve'>
<title>KaTeX Equation</title>
<foreignObject width='100%' height='100%'>
	<div xmlns='http://www.w3.org/1999/xhtml'>{katexHtml}</div>
</foreignObject>
</svg>
".Trim();

                    KaTeXSvgCodeList.Add(svgCode);

                    var svgDataUrl =
                        ((IJavaScriptExecutor)driver).ExecuteScript(
                            $"return svgToDataURI(`{svgCode}`);"
                        ).ToString() ?? string.Empty;

                    KaTeXSvgDataUriList.Add(svgDataUrl);

                    if (SaveImages)
                    {
                        var imageFileName = $"KaTeX-{index:D6}";

                        KaTeXImageFileName.Add(imageFileName);

                        image.SaveAsPng(
                            WorkingFolder.GetPngFilePath(imageFileName)
                        );

                        File.WriteAllText(
                            WorkingFolder.GetSvgFilePath(imageFileName),
                            svgCode
                        );
                        
                        //screenShot.SaveAsFile(
                        //    WorkingFolder.GetFilePath($"KaTeX-{index:D6}", "png"),
                        //    ScreenshotImageFormat.Png
                        //);
                    }

                    //Console.WriteLine(svgCode);
                    //Console.WriteLine();

                    index++;
                }

                //htmlCode = driver.PageSource;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Thread.Sleep(200);

                driver.Quit();
            }

            //if (File.Exists(filePath))
            //    File.Delete(filePath);
        }

        public async void RenderKaTeX()
        {
            var htmlCode = await CreateFullHtmlDocument();

            RenderKaTeX(htmlCode);
        }
        
        public async void RenderKaTeX(IEnumerable<string> kaTeXCodeList)
        {
            KaTeXCodeList.Clear();
            KaTeXCodeList.AddRange(kaTeXCodeList);

            var htmlCode = await CreateFullHtmlDocument();

            RenderKaTeX(htmlCode);
        }
    }
}
