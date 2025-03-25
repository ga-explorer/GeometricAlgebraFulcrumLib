using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;

public abstract class GrBabylonJsSceneSequenceComposer :
    GrVisualSceneSequenceComposer
{
    public string HostUrl { get; init; }

    public GrBabylonJsCodeFilesComposer CodeFilesComposer { get; protected set; }

    public GrBabylonJsSceneComposer MainSceneComposer
        => CodeFilesComposer.FirstSceneComposer;

    public GrBabylonJsScene MainScene
        => CodeFilesComposer.FirstScene;


    public GrBabylonJsSceneSequenceComposer(string workingFolder, Float64SamplingSpecs samplingSpecs)
        : base(workingFolder, samplingSpecs)
    {
    }

    
    protected override void CleanSceneFiles()
    {
        CleanOutputFiles("*.html");
        CleanOutputFiles("*.png");
        CleanOutputFiles("*.gif");
        CleanOutputFiles("*.mp4");
    }

    protected override void SetCameraAndLights(int frameIndex)
    {
        var (alpha, beta, distance) =
            CameraSpecs.GetCameraAlphaBetaDistanceAtFrame(frameIndex);

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

    protected override void AddEnvironment(int frameIndex)
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
    
    protected override void SaveSceneFiles(int frameIndex)
    {
        var htmlCode = 
            CodeFilesComposer.GetHtmlCode();

        var htmlFilePath = 
            GetOutputFilePath(GetFrameName(frameIndex), "html");

        File.WriteAllText(
            htmlFilePath, 
            htmlCode
        );
    }
    
    protected string GetOutputHtmlFileUrl(int frameIndex)
    {
        return @$"{HostUrl}Output/{GetFrameName(frameIndex)}.html";
    }

    protected override void RenderImageFile(int frameIndex)
    {
        const int delay1 = 2000;
        const int delay2 = 1000;
        
        var chromeOptions = new ChromeOptions
        {
            PageLoadStrategy = PageLoadStrategy.Normal,
            UnhandledPromptBehavior = UnhandledPromptBehavior.Accept
        };

        chromeOptions.AddUserProfilePreference("download.default_directory", GetOutputPath());
        chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
        chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

        //chromeOptions.AddAdditionalChromeOption("window-size", "1920,1080");
        //chromeOptions.AddArgument("headless");

        var driver = new ChromeDriver(chromeOptions);

        try
        {
            driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(delay1);

            driver.Navigate().GoToUrl(GetOutputHtmlFileUrl(frameIndex));
            Thread.Sleep(1000);

            while (!File.Exists(GetImageFilePath(frameIndex)))
            {
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
    }

    protected override void BatchRenderImageFiles()
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

        chromeOptions.AddUserProfilePreference("download.default_directory", GetOutputPath());
        chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
        chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

        //chromeOptions.AddAdditionalChromeOption("window-size", "1920,1080");
        //chromeOptions.AddArgument("headless");

        var driver = new ChromeDriver(chromeOptions);

        try
        {
            //var htmlFilePath = GetOutputFilePath(GetFrameName(frameIndex), "html");
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
            while (FrameIndex < ImageCount)
            {
                var frameIndexList = new List<int>(tabHandles.Count);

                foreach (var tabHandle in tabHandles)
                {
                    if (FrameIndex >= ImageCount) continue;

                    frameIndexList.Add(FrameIndex);

                    //htmlFilePath = GetOutputFilePath(GetFrameName(FrameIndex), "html");
                    //htmlFileUri = new Uri(htmlFilePath).AbsoluteUri;

                    driver.SwitchTo().Window(tabHandle);
                    driver.Navigate().GoToUrl(GetOutputHtmlFileUrl(FrameIndex));

                    //var canvas = driver.FindElement(By.Id("renderCanvas"));
                    //var screenShot = ((ITakesScreenshot) canvas).GetScreenshot();

                    //screenShot.SaveAsFile(
                    //    GetOutputImagePath(FrameIndex),
                    //    ScreenshotImageFormat.Png
                    //);

                    FrameIndex++;
                }

                foreach (var tabHandle in tabHandles)
                {
                    driver.SwitchTo().Window(tabHandle);
                    Thread.Sleep(200);
                }

                var flag = false;
                while (!flag)
                {
                    flag = frameIndexList.All(i =>
                        File.Exists(GetImageFilePath(i))
                    );
                }

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


}