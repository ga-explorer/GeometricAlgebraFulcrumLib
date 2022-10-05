using DataStructuresLib.Files;
using FFMpegCore;
using FFMpegCore.Enums;
using GraphicsComposerLib.Rendering.Images;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;

namespace GraphicsComposerLib.Rendering.BabylonJs;

public class GrBabylonJsSnapshotComposer3D
{
    public string Title { get; init; }

    public int FrameCount { get; init; }

    public string WorkingPath { get; init; }

    public string HostUrl { get; init; }

    public bool GenerateHtml { get; set; }

    public bool GeneratePng { get; set; }

    public bool GenerateAnimatedGif { get; set; }

    public int AnimatedGifFrameDelay { get; set; }

    public bool GenerateMp4 { get; set; }

    public double Mp4FrameRate { get; set; }

    public Func<int, string> CodeGenerationFunc { get; protected set; }

    public int FrameIndex { get; private set; } = -1;

    public static double LaTeXScalingFactor { get; set; }
        = 1 / 75d;

    public static GrImageBase64StringCache ImageCache { get; }
        = new GrImageBase64StringCache();


    public GrBabylonJsSnapshotComposer3D()
    {
    }

    public GrBabylonJsSnapshotComposer3D(Func<int, string> codeGenerationFunc)
    {
        CodeGenerationFunc = codeGenerationFunc;
    }


    private static void WaitFor(int timeInMilliseconds)
    {
        Thread.Sleep(timeInMilliseconds);
    }

    private static void Cleanup(IEnumerable<string> pathList)
    {
        foreach (var path in pathList)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }

    private static void ConversionSizeExceptionCheck(int width, int height)
    {
        if (height % 2 != 0 || width % 2 != 0)
            throw new ArgumentException("FFMpeg yuv420p encoding requires the width and height to be a multiple of 2!");
    }

    /// <summary>
    /// Converts an image sequence to a video.
    /// </summary>
    /// <param name="output">Output video file.</param>
    /// <param name="frameRate">FPS</param>
    /// <param name="images">Image sequence collection</param>
    /// <returns>Output video information.</returns>
    public static bool JoinImageSequence(string output, double frameRate, IEnumerable<ImageInfo> images)
    {
        var tempFolderName = Path.Combine(GlobalFFOptions.Current.TemporaryFilesFolder, Guid.NewGuid().ToString());
        var temporaryImageFiles = images.Select((imageInfo, index) =>
        {
            using var image = Image.Load(imageInfo.FullName);
            ConversionSizeExceptionCheck(image.Width, image.Height);
            var destinationPath = Path.Combine(tempFolderName, $"{index.ToString().PadLeft(9, '0')}{imageInfo.Extension}");
            Directory.CreateDirectory(tempFolderName);
            File.Copy(imageInfo.FullName, destinationPath);
            return destinationPath;
        }).ToArray();

        //var firstImage = images.First();
        try
        {
            return FFMpegArguments
                .FromFileInput(Path.Combine(tempFolderName, "%09d.png"), false)
                .OutputToFile(
                    output,
                    true,
                    options => options
                        //.ForcePixelFormat("yuv420p")
                        .WithVideoCodec(VideoCodec.Png)
                        //.Resize(firstImage.Width, firstImage.Height)
                        .WithVideoFilters(filterOptions =>
                            filterOptions.Scale(VideoSize.Hd)
                        )
                        .WithFastStart()
                        .WithFramerate(frameRate)
                    )
                .ProcessSynchronously();
        }
        finally
        {
            Cleanup(temporaryImageFiles);
            Directory.Delete(tempFolderName);
        }
    }

    public void GenerateHtmlFiles()
    {
        Console.Write("Generating HTML files .. ");

        for (var index = 0; index < FrameCount; index++)
        {
            FrameIndex = index;

            var htmlCode = CodeGenerationFunc(index);

            var htmlFilePath = WorkingPath.GetFilePath(
                @$"Frame-{index:D6}", 
                "html"
            );

            File.WriteAllText(htmlFilePath, htmlCode);
        }

        FrameIndex = -1;

        Console.WriteLine("Done.");
    }

    public void GeneratePngFiles()
    {
        const int delay1 = 2000;
        const int delay2 = 1000;


        Console.Write("Generating Png files .. ");
        
        var chromeOptions = new ChromeOptions
        {
            PageLoadStrategy = PageLoadStrategy.Normal,
            UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
        };
        
        chromeOptions.AddUserProfilePreference("download.default_directory", WorkingPath);
        chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
        chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

        //chromeOptions.AddAdditionalChromeOption("window-size", "1920,1080");
        //chromeOptions.AddArgument("headless");
        
        var driver = new ChromeDriver(chromeOptions);

        try
        {
            //var htmlFilePath = WorkingPath.GetFilePath(@$"Frame-{0:D6}", "html");
            //var htmlFileUri = new Uri(htmlFilePath).AbsoluteUri;
            
            driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(delay1);
            
            Console.WriteLine("Press any key to start png file generation ..");
            Console.WriteLine();
            Console.ReadKey();
            
            for (var index = 0; index < FrameCount; index++)
            {
                FrameIndex = index;

                //htmlFilePath = WorkingPath.GetFilePath(@$"Frame-{index:D6}", "html");
                //htmlFileUri = new Uri(htmlFilePath).AbsoluteUri;
                
                driver.Navigate().GoToUrl(@$"{HostUrl}Frame-{index:D6}.html");
                Thread.Sleep(index == 0 ? delay1 : delay2);
                
                //var canvas = driver.FindElement(By.Id("renderCanvas"));
                //var screenShot = ((ITakesScreenshot) canvas).GetScreenshot();

                //screenShot.SaveAsFile(
                //    WorkingPath.GetFilePath($"Frame-{index:D6}", "png"),
                //    ScreenshotImageFormat.Png
                //);

                if (index % 10 == 0)
                    Console.WriteLine($"Snapshot {index} done.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            Thread.Sleep(delay1);

            driver.Quit();
        }

        FrameIndex = -1;

        Console.WriteLine("Done.");
    }

    public void GenerateAnimatedGifFile()
    {
        Console.Write("Generating Animated Gif file .. ");

        // Image dimensions of the gif.
        using var firstImageStream = File.OpenRead(
            WorkingPath.GetFilePath(@$"Frame-{0:D6}", "png")
        );

        var firstImageInfo = Image.Identify(firstImageStream);
        
        var width = firstImageInfo.Width;
        var height = firstImageInfo.Height;

        firstImageStream.Close();

        //// For demonstration: use images with different colors.
        //Color[] colors = {
        //    Color.Green,
        //    Color.Red
        //};

        // Create empty image.
        using var animatedGif = new Image<Rgba32>(width, height, Color.Blue);

        // Set animation loop repeat count to 10.
        var gifMetaData = animatedGif.Metadata.GetGifMetadata();
        gifMetaData.RepeatCount = 10;
        gifMetaData.ColorTableMode = GifColorTableMode.Global;

        // Set the delay until the next image is displayed.
        var metadata = animatedGif.Frames.RootFrame.Metadata.GetGifMetadata();
        metadata.FrameDelay = AnimatedGifFrameDelay;

        for (var i = 0; i < FrameCount; i++)
        {
            using var imageStream = File.OpenRead(
                WorkingPath.GetFilePath(@$"Frame-{i:D6}", "png")
            );

            // Create a color image, which will be added to the gif.
            using var image = Image.Load(imageStream);

            // Set the delay until the next image is displayed.
            metadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
            metadata.FrameDelay = AnimatedGifFrameDelay;

            // Add the color image to the gif.
            animatedGif.Frames.AddFrame(image.Frames.RootFrame);
        }

        // Save the final result.
        animatedGif.SaveAsGif(
            WorkingPath.GetFilePath(Title, "gif")
        );

        Console.WriteLine("Done.");
    }

    public void GenerateMp4File()
    {
        Console.Write("Generating Mp4 file .. ");

        var imageFilePathList =
            Enumerable
                .Range(0, FrameCount)
                .Select(i => WorkingPath.GetFilePath($"Frame-{i:D6}", "png"));

        GlobalFFOptions.Configure(
            new FFOptions
            {
                BinaryFolder = @"D:\Projects\Tools\ffmpeg\bin\",
                TemporaryFilesFolder = Path.GetTempPath()
            }
        );

        var images =
            imageFilePathList
                .Select(ImageInfo.FromPath);

        JoinImageSequence(
            WorkingPath.GetFilePath(Title, "mp4"),
            frameRate: Mp4FrameRate,
            images
        );

        Console.WriteLine("Done.");
    }

    public virtual void GenerateSnapshots()
    {
        // Delete any old html\png files
        Array.ForEach(
            Directory.GetFiles(
                WorkingPath, 
                @"Frame-*.html", 
                SearchOption.TopDirectoryOnly
            ),
            File.Delete
        );

        Array.ForEach(
            Directory.GetFiles(
                WorkingPath, 
                @"Frame-*.png", 
                SearchOption.TopDirectoryOnly
            ),
            File.Delete
        );

        bool htmlFilesExist;
        bool pngFilesExist;
        do
        {
            htmlFilesExist =
                Directory.GetFiles(
                    WorkingPath,
                    @"Frame-*.html",
                    SearchOption.TopDirectoryOnly
                ).Length > 0;

            pngFilesExist =
                Directory.GetFiles(
                    WorkingPath,
                    @"Frame-*.png",
                    SearchOption.TopDirectoryOnly
                ).Length > 0;
        } while (htmlFilesExist || pngFilesExist);

        GenerateHtmlFiles();

        if (!GeneratePng && !GenerateAnimatedGif && !GenerateMp4)
            return;

        GeneratePngFiles();

        if (GenerateAnimatedGif)
            GenerateAnimatedGifFile();

        if (GenerateMp4)
            GenerateMp4File();
    }
}