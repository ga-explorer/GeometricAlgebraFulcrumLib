using System.Collections;
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
using System.Net;
using AngleSharp.Html;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.CodeComposer;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Extensions;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;

/// <summary>
/// https://github.com/KaTeX/KaTeX
/// https://katex.org/docs/supported.html
/// </summary>
public class WclKaTeXComposer : 
    IReadOnlyDictionary<string, WclKaTeXComposerItem>
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
                {left: ""\\begin{align*}"", right: ""\\end{align*}"", display: true},
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

            html {
                width:100%;
                overflow-x:hidden;
                overflow-y:hidden;
            }

            .katex { font-size: #font-size#; }
        </style>
    </head>
    <body>
        
    </body>
</html>
".Trim();


    private readonly Dictionary<string, WclKaTeXComposerItem> _keyItemDictionary
        = new Dictionary<string, WclKaTeXComposerItem>();
    
    
    //public List<string> KaTeXCodeList { get; }
    //    = new List<string>();

    //public List<Image<Rgba32>> KaTeXPngImageList { get; }
    //    = new List<Image<Rgba32>>();
        
    //public List<string> KaTeXImageFileName { get; }
    //    = new List<string>();


    public int Count 
        => _keyItemDictionary.Count;

    public IEnumerable<string> Keys 
        => _keyItemDictionary.Keys;

    public IEnumerable<WclKaTeXComposerItem> Values 
        => _keyItemDictionary.Values;

    public WclKaTeXComposerItem this[string key]
        => _keyItemDictionary[key];

    public IEnumerable<Image> Images 
        => _keyItemDictionary.Values.Select(v => v.PngImage);

    public string WorkingFolder { get; }

    public bool SaveImages { get; init; } 

    public bool DisplayMode { get; set; }
        = false;

    public OutputKind Output { get; init; }
        = OutputKind.HtmlAndMathMl;

    public bool ThrowOnError { get; init; }
        = true;

    public Color ErrorColor { get; set; }
        = Color.Black; //Color.DarkRed;

    public float FontSizeEm { get; init; }
        = 1.21f;

        
    public WclKaTeXComposer(string workingFolder)
    {
        if (workingFolder.IsNullOrEmpty())
            workingFolder = Path.GetTempPath();

        if (!Path.Exists(workingFolder))
            throw new DirectoryNotFoundException(workingFolder);

        WorkingFolder = workingFolder;
    }

    
    public WclKaTeXComposer Clear()
    {
        _keyItemDictionary.Clear();

        return this;
    }

    public bool Remove(string key)
    {
        return _keyItemDictionary.Remove(key);
    }

    public bool ContainsKey(string key)
    {
        return _keyItemDictionary.ContainsKey(key);
    }

    public bool TryGetValue(string key, out WclKaTeXComposerItem value)
    {
        return _keyItemDictionary.TryGetValue(key, out value);
    }

    public WclKaTeXComposer AddLaTeXCode(string key, string latexCode)
    {
        var item = new WclKaTeXComposerItem(key, latexCode);

        if (_keyItemDictionary.ContainsKey(key))
            _keyItemDictionary[key] = item;
        else
            _keyItemDictionary.Add(key, item);

        return this;
    }

    public WclKaTeXComposer AddLaTeXCode(Dictionary<string, string> keyCodeDictionary)
    {
        foreach (var (key, latexCode) in keyCodeDictionary)
            AddLaTeXCode(key, latexCode);

        return this;
    }

    public WclKaTeXComposer AddLaTeXEquation(string key, string latexCode)
    {
        return AddLaTeXCode(
            key, 
            latexCode.GetLaTeXDisplayEquation()
        );
    }
    
    public WclKaTeXComposer AddLaTeXAlignedEquations(string key, string latexCode, params Pair<string>[] latexCodeArray)
    {
        return AddLaTeXCode(
            key, 
            latexCode.Trim() +
            Environment.NewLine +
            latexCodeArray.GetLaTeXAlignedEquations(false)
        );
    }

    public WclKaTeXComposer AddLaTeXAlignedEquations(string key, params Pair<string>[] latexCodeArray)
    {
        return AddLaTeXCode(
            key, 
            latexCodeArray.GetLaTeXAlignedEquations(false)
        );
    }
    
    public WclKaTeXComposer AddLaTeXAlignedEquations(string key, IEnumerable<Pair<string>> latexCodeArray)
    {
        return AddLaTeXCode(
            key, 
            latexCodeArray.GetLaTeXAlignedEquations(false)
        );
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
            $"errorColor: '#{ErrorColor.RgbToHexString()}'";

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
        
    private async Task<string> CreateFullHtmlDocument(string key, string latexCode)
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

        var divId = $"katex-div-{key}";

        var divElement = document.CreateElement<IHtmlDivElement>();

        divElement.SetAttribute("class", "katexDiv");
        divElement.SetAttribute("id", divId);

        documentBody.AppendChild(divElement);
            
        var optionsCode = GetOptionsCode();

        var latexHtmlString =
            WebUtility.HtmlEncode(
                latexCode
                    .Replace(@"\", @"\\")
                    .Replace(@"`", @"\`")
            ).Replace(@"&amp;", @"&");

        codeComposer.AppendLineAtNewLine(
            @$"katex.render(`{latexHtmlString}`, document.getElementById('{divId}'), {optionsCode});"
        ).AppendLine();

        var scriptElement = document.CreateElement<IHtmlScriptElement>();
        scriptElement.TextContent = codeComposer.ToString();
        documentBody.AppendChild(scriptElement);
        
        return document.ToHtml(new PrettyMarkupFormatter());
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
        
//        documentBody.AppendChild(
//            document.CreateTextNode(@"
//<style>
//    html{
//        width:100%;
//        overflow-x:hidden;
//        overflow-y:hidden;
//    }
//</style>
//".Trim()
//            )
//        );

        var codeComposer = new LinearTextComposer();

        //var i = 0;
        foreach (var (key, item) in _keyItemDictionary)
        {
            var latexCode = item.LaTeXCode;
            var divId = $"katex-div-{key}";

            var divElement = document.CreateElement<IHtmlDivElement>();

            divElement.SetAttribute("class", "katexDiv");
            divElement.SetAttribute("id", divId);

            documentBody.AppendChild(divElement);
            
            var optionsCode = GetOptionsCode();

            var latexHtmlString =
                WebUtility.HtmlEncode(
                    latexCode
                        .Replace(@"\", @"\\")
                        .Replace(@"`", @"\`")
                ).Replace(@"&amp;", @"&");

            codeComposer.AppendLineAtNewLine(
                @$"katex.render(`{latexHtmlString}`, document.getElementById('{divId}'), {optionsCode});"
            ).AppendLine();

            //i++;
        }

        var scriptElement = document.CreateElement<IHtmlScriptElement>();
        scriptElement.TextContent = codeComposer.ToString();
        documentBody.AppendChild(scriptElement);
        
        return document.ToHtml(new PrettyMarkupFormatter());
    }

    private void RenderKaTeXFromHtml(string htmlCode)
    {
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
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

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

            //Thread.Sleep(10000);

            var divElements =
                driver.FindElements(By.TagName("div")).ToArray();

            var index = 0;
            foreach (var divElement in divElements)
            {
                //driver.SwitchTo().Frame(divElement);

                //((IJavaScriptExecutor)driver).ExecuteScript(
                //    $"window.scroll(0, {divElement.Location.Y});"
                //);

                //var div = wait.Until(
                //    ExpectedConditions.ElementIsVisible(By.Id(divElement.))
                //);
                
                var screenShot = ((ITakesScreenshot)divElement).GetScreenshot();
                var key = divElement.GetDomAttribute("id")["katex-div-".Length..];
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

                var item = _keyItemDictionary[key];
                item.PngImage = image;
                
                if (SaveImages)
                {
                    image.SaveAsPng(
                        WorkingFolder.GetPngFilePath(item.ImageFileName)
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

        //if (File.Exists(filePath))
        //    File.Delete(filePath);
    }
    
    private void RenderKaTeXFromHtml()
    {
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
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        
        try
        {
            // Open a new tab
            var tabHandleCollection = driver.WindowHandles;

            if (tabHandleCollection.Count < 1)
                driver.SwitchTo().NewWindow(WindowType.Tab);

            driver.SwitchTo().Window(
                tabHandleCollection.First()
            );
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            //var htmlCodeId = 0;
            foreach (var (key, item) in _keyItemDictionary)
            {
                var htmlCode = item.HtmlCode;

                //Save html document to local file
                var filePath =
                    WorkingFolder.GetFilePath(
                        "KaTeX-" + key, //Path.GetFileNameWithoutExtension(Path.GetTempFileName()),
                        "html"
                    );

                File.WriteAllText(
                    filePath,
                    htmlCode
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
                wait.Until((x) =>
                {
                    return ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
                });

                var divElement =
                    driver.FindElement(By.TagName("div"));

                //driver.SwitchTo().Frame(divElement);

                //((IJavaScriptExecutor)driver).ExecuteScript(
                //    $"window.scroll(0, {divElement.Location.Y});"
                //);

                //var div = wait.Until(
                //    ExpectedConditions.ElementIsVisible(By.Id(divElement.))
                //);

                var screenShot = ((ITakesScreenshot)divElement).GetScreenshot();

                var image = Image.Load<Rgba32>(screenShot.AsByteArray);

                // Crop white space around equation
                image.Mutate(x => x.AutoCrop());

                // Replace white background with transparent background
                // https://github.com/SixLabors/ImageSharp.Drawing/issues/26
                //image.Mutate(i => i.Clear(Color.Transparent));

                // https://docs.sixlabors.com/articles/imagesharp/pixelbuffers.html
                image.ProcessPixelRows(accessor =>
                {
                    // Color is pixel-agnostic, but it's implicitly convertible to the Rgba32 pixel type
                    var transparent = Color.Transparent.ToPixel<Rgba32>();
                    var black = Color.Black.FullAlpha().ToPixel<Rgba32>();

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

                            //// Overwrite the pixel referenced by 'ref Rgba32 pixel':
                            //if (pixel.R > 128 && pixel.G > 128 && pixel.B > 128)
                            //    pixel = transparent;
                            //else
                            //    pixel = black;
                        }
                    }
                });

                item.PngImage = image;

                if (SaveImages)
                {
                    image.SaveAsPng(
                        WorkingFolder.GetPngFilePath(item.ImageFileName),
                        new PngEncoder()
                        {
                            ColorType = PngColorType.RgbWithAlpha
                        }
                    );
                }

                if (File.Exists(filePath))
                    File.Delete(filePath);
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

    }

    //public async void RenderKaTeX()
    //{
    //    var htmlCode = await CreateFullHtmlDocument();

    //    RenderKaTeXFromHtml(htmlCode);
    //}
    
    public async void RenderKaTeX()
    {
        foreach (var (key, item) in _keyItemDictionary)
        {
            var latexCode = item.LaTeXCode;
            var htmlCode = await CreateFullHtmlDocument(key, latexCode);
            
            item.HtmlCode = htmlCode;
        }
        
        RenderKaTeXFromHtml();
    }

    public IEnumerator<KeyValuePair<string, WclKaTeXComposerItem>> GetEnumerator()
    {
        return _keyItemDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}