using System.Globalization;
using AngleSharp.Html.Dom;
using AngleSharp;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using AngleSharp.Dom;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Linear;
using GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Extensions;
using System.Net;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;

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
        <link rel=""stylesheet"" href=""./katex/katex.min.css"">
        <!-- link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/katex.min.css"" integrity=""sha384-3UiQGuEI4TTMaFmGIZumfRPtfKQ3trwQE2JgosJxCnGmQpL/lJdjpcHkaaFwHlcI"" crossorigin=""anonymous"" -->

        <script src=""./katex/katex.min.js""></script>
        <!-- script defer src=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/katex.min.js"" integrity=""sha384-G0zcxDFp5LWZtDuRMnBkk3EphCK1lhEf4UEyEM693ka574TZGwo4IWwS6QLzM/2t"" crossorigin=""anonymous""></script -->

        <!-- KaTeX Auto-render extension -->
        <!-- To automatically render math in text elements, include the auto-render extension: -->
        <script defer src=""./katex/contrib/auto-render.min.js"" onload=""renderMathInElement(document.body);""></script>
        <!-- script defer src=""https://cdn.jsdelivr.net/npm/katex@0.16.7/dist/contrib/auto-render.min.js"" integrity=""sha384-+VBxd3r6XgURycqtZ117nYw44OOcIax56Z4dCRWbxyPt0Koah1uHoK0o4+/RRE05"" crossorigin=""anonymous"" onload=""renderMathInElement(document.body);""></script -->
        
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

        <!-- script defer src=""https://cdn.jsdelivr.net/npm/webfontloader@1.6.28/webfontloader.js"" integrity=""sha256-4O4pS1SH31ZqrSO2A/2QJTVjTPqVe+jnYgOWUVr7EEc="" crossorigin=""anonymous""></script -->
        
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
        
    </body>
</html>
".Trim();


    public string WorkingFolder { get; }

    public List<string> KaTeXCodeList { get; }
        = new List<string>();

    public List<Image<Rgba32>> KaTeXPngImageList { get; }
        = new List<Image<Rgba32>>();
        
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
                    
                if (SaveImages)
                {
                    var imageFileName = $"KaTeX-{index:D6}";

                    KaTeXImageFileName.Add(imageFileName);

                    image.SaveAsPng(
                        WorkingFolder.GetPngFilePath(imageFileName)
                    );
                }

                index++;
            }
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

        if (File.Exists(filePath))
            File.Delete(filePath);
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