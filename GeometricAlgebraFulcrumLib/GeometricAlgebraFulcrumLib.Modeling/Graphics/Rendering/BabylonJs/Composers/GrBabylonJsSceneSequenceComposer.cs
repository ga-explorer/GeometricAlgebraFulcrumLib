using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using Xabe.FFmpeg;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;

public abstract class GrBabylonJsSceneSequenceComposer :
    GrVisualSceneSequenceComposer
{
    public string HostUrl { get; init; }

    //public WclHtmlImageDataUrlCache ImageCache
    //    => CodeFilesComposer.ImageCache;

    public int CanvasWidth { get; set; } = 1280;

    public int CanvasHeight { get; set; } = 720;

    public GrBabylonJsCodeFilesComposer CodeFilesComposer { get; protected set; }

    public GrBabylonJsSceneComposer MainSceneComposer
        => CodeFilesComposer.FirstSceneComposer;

    public GrBabylonJsScene MainScene
        => CodeFilesComposer.FirstScene;


    public GrBabylonJsSceneSequenceComposer(string workingFolder, Float64SamplingSpecs samplingSpecs)
        : base(workingFolder, samplingSpecs)
    {
    }

    
    protected override void InitializeTemporalValues()
    {
    }

    protected override void InitializeTextureSet()
    {
    }

    protected override void SetCameraAndLights(int frameIndex)
    {
        var (alpha, beta, distance) =
            GetCameraAlphaBetaDistanceAtFrame(frameIndex);

        // Add main scene camera
        MainSceneComposer.SceneObject.AddArcRotateCamera(
            "camera1",
            alpha.RadiansValue,
            beta.RadiansValue,
            distance,
            "BABYLON.Vector3.Zero()",
            new GrBabylonJsArcRotateCameraProperties
            {
                Mode = GrBabylonJsCameraMode.PerspectiveCamera,
                //OrthoLeft = -8,
                //OrthoRight = 8,
                //OrthoBottom = -8,
                //OrthoTop = 8
            }
        );
    }

    protected virtual void AddEnvironment()
    {
        //var scene = MainSceneComposer.SceneObject;
        //scene.SceneProperties.AmbientColor = Color.AliceBlue;

        // Add scene environment
        MainSceneComposer.SceneObject.AddEnvironmentHelper(
            "environmentHelper",

            new GrBabylonJsEnvironmentHelperOptions
            {
                GroundYBias = 0.01,
                SkyboxColor = Color.LightSkyBlue,
                GroundColor = Color.White,
                CreateGround = true,
                GroundSize = 8,
                SkyboxSize = GridUnitCount + 10
            }
        );
    }

    protected virtual void AddGrid()
    {
        // Add ground coordinates grid
        MainSceneComposer.GridMaterialKind =
            GrBabylonJsGridMaterialKind.TexturedMaterial;

        MainSceneComposer.AddSquareGrid(
            GrVisualSquareGrid3D.DefaultZx(
                LinFloat64Vector3D.Zero,
                GridUnitCount,
                1,
                0.25
            )
        );
    }

    protected void AddAxes()
    {
        var scene = MainSceneComposer.SceneObject;

        // Add reference unit axis frame
        MainSceneComposer.AddElement(
            GrVisualFrame3D.CreateStatic(
                "axisFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = scene.AddSimpleMaterial("axisFrameOriginMaterial", Color.DarkGray).CreateThickSurfaceStyle(0.075),
                    Direction1Style = scene.AddSimpleMaterial("axisFrameXMaterial", Color.DarkRed).CreateTubeCurveStyle(0.035),
                    Direction2Style = scene.AddSimpleMaterial("axisFrameYMaterial", Color.DarkGreen).CreateTubeCurveStyle(0.035),
                    Direction3Style = scene.AddSimpleMaterial("axisFrameZMaterial", Color.DarkBlue).CreateTubeCurveStyle(0.035)
                },
                AxesOrigin
            )
        );
    }

    protected abstract void AddGuiLayer(int frameIndex);

    protected override void ComposeFrame(int frameIndex)
    {
        InitializeSceneComposers(frameIndex);

        SetCameraAndLights(frameIndex);
        AddEnvironment();
        if (ShowGrid) AddGrid();
        if (ShowAxes) AddAxes();
        if (ShowGuiLayer) AddGuiLayer(frameIndex);
    }

    protected override void ComposeSceneFiles()
    {
        InitializeTextureSet();

        Console.Write("Composing scene files .. ");

        for (var index = 0; index < FrameCount; index++)
        {
            FrameIndex = index;
            
            ComposeFrame(index);

            var htmlCode = CodeFilesComposer.GetHtmlCode();

            var htmlFilePath = WorkingFolder.GetFilePath(
                @$"Frame-{index:D6}",
                "html"
            );

            File.WriteAllText(htmlFilePath, htmlCode);
        }

        FrameIndex = -1;

        Console.WriteLine("done.");
    }

    protected override void RenderImageFiles()
    {
        const int delay1 = 2000;
        const int delay2 = 1000;
        const int tabCount = 10;

        var startTime = DateTime.Now;

        Console.Write("Rendering image files .. ");

        var chromeOptions = new ChromeOptions
        {
            PageLoadStrategy = PageLoadStrategy.Normal,
            UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
        };

        chromeOptions.AddUserProfilePreference("download.default_directory", WorkingFolder);
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

            // Open several tabs at once
            for (var i = 0; i < tabCount - 1; i++)
                driver.SwitchTo().NewWindow(WindowType.Tab);

            while (driver.WindowHandles.Count < tabCount) ;

            var tabHandles = driver.WindowHandles;

            //Console.WriteLine("Press any key to start image file generation ..");
            //Console.WriteLine();
            //Console.ReadKey();

            startTime = DateTime.Now;

            FrameIndex = 0;
            while (FrameIndex < FrameCount)
            {
                var frameIndexList = new List<int>(tabHandles.Count);

                foreach (var tabHandle in tabHandles)
                {
                    if (FrameIndex >= FrameCount) continue;

                    frameIndexList.Add(FrameIndex);

                    //htmlFilePath = WorkingPath.GetFilePath(@$"Frame-{index:D6}", "html");
                    //htmlFileUri = new Uri(htmlFilePath).AbsoluteUri;

                    driver.SwitchTo().Window(tabHandle);
                    driver.Navigate().GoToUrl(@$"{HostUrl}Frame-{FrameIndex:D6}.html");

                    //var canvas = driver.FindElement(By.Id("renderCanvas"));
                    //var screenShot = ((ITakesScreenshot) canvas).GetScreenshot();

                    //screenShot.SaveAsFile(
                    //    WorkingPath.GetFilePath($"Frame-{index:D6}", "png"),
                    //    ScreenshotImageFormat.Png
                    //);

                    FrameIndex++;
                }

                foreach (var tabHandle in tabHandles)
                {
                    driver.SwitchTo().Window(tabHandle);
                    Thread.Sleep(200);
                }

                //var flag = false;
                //while (!flag)
                //{
                //    flag = frameIndexList.All(i =>
                //        File.Exists(WorkingFolder.GetFilePath(@$"Frame-{i:D6}", "png"))
                //    );
                //}

                //Thread.Sleep(delay1);
                //Thread.Sleep(FrameIndex == 0 ? delay1 : delay2);

                Console.WriteLine($"    Image {FrameIndex} done.");
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

        var timeSpan = DateTime.Now - startTime;
        Console.WriteLine($"Rendering image files done in {timeSpan.Minutes} minutes.");
    }

    protected override void RenderGifFile()
    {
        Console.Write("Rendering animated GIF file .. ");

        // Image dimensions of the gif.
        using var firstImageStream = File.OpenRead(
            WorkingFolder.GetFilePath(@$"Frame-{0:D6}", "png")
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
        metadata.FrameDelay = GifFrameDelay;

        for (var i = 0; i < FrameCount; i++)
        {
            using var imageStream = File.OpenRead(
                WorkingFolder.GetFilePath(@$"Frame-{i:D6}", "png")
            );

            // Create a color image, which will be added to the gif.
            using var image = Image.Load(imageStream);

            // Set the delay until the next image is displayed.
            metadata = image.Frames.RootFrame.Metadata.GetGifMetadata();
            metadata.FrameDelay = GifFrameDelay;

            // Add the color image to the gif.
            animatedGif.Frames.AddFrame(image.Frames.RootFrame);
        }

        // Save the final result.
        animatedGif.SaveAsGif(
            WorkingFolder.GetFilePath(Title, "gif")
        );

        Console.WriteLine("done.");
        Console.WriteLine();
    }

    protected override void RenderVideoFile()
    {
        Console.Write("Rendering video file .. ");

        var imageFileList = new List<string>(
            FrameCount.MapRange(i => 
                WorkingFolder.GetFilePath(
                    @$"Frame-{i:D6}",
                    "png"
                )
            )
        );

        FFmpeg.SetExecutablesPath(@"D:\ffmpeg\bin");

        var videoFilePath = WorkingFolder.GetFilePath("Scene.mp4");

        if (File.Exists(videoFilePath))
            File.Delete(videoFilePath);

        new Conversion()
            .SetInputFrameRate(VideoFrameRate)
            .BuildVideoFromImages(imageFileList)
            //.SetFrameRate(1)
            .SetPixelFormat(PixelFormat.yuv420p)
            .SetOutput(videoFilePath)
            .Start();

        Console.WriteLine("done.");
        Console.WriteLine();
    }

    public override void RenderFiles()
    {
        // Delete any old html\png files
        Array.ForEach(
            Directory.GetFiles(
                WorkingFolder,
                @"Frame-*.html",
                SearchOption.TopDirectoryOnly
            ),
            File.Delete
        );

        Array.ForEach(
            Directory.GetFiles(
                WorkingFolder,
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
                    WorkingFolder,
                    @"Frame-*.html",
                    SearchOption.TopDirectoryOnly
                ).Length > 0;

            pngFilesExist =
                Directory.GetFiles(
                    WorkingFolder,
                    @"Frame-*.png",
                    SearchOption.TopDirectoryOnly
                ).Length > 0;
        } while (htmlFilesExist || pngFilesExist);

        ComposeSceneFiles();

        if (!RenderImageFilesEnabled && !RenderGifFileEnabled && !RenderVideoFileEnabled)
            return;

        RenderImageFiles();

        if (RenderGifFileEnabled)
            RenderGifFile();

        if (RenderVideoFileEnabled)
            RenderVideoFile();
    }
}