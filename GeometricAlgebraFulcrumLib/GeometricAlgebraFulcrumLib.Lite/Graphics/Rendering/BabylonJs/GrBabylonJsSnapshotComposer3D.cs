using DataStructuresLib.Files;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Cameras;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SixLabors.ImageSharp.Formats.Gif;
using WebComposerLib.Html.Media;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.BabylonJs
{
    public abstract class GrBabylonJsSnapshotComposer3D
    {
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
        

        public string Title { get; init; }

        public int FrameCount { get; }

        public string WorkingFolder { get; init; }

        public string HostUrl { get; init; }

        public bool GenerateHtml { get; set; } = true;

        public bool GeneratePng { get; set; } = false;

        public bool GenerateAnimatedGif { get; set; } = false;

        public int AnimatedGifFrameDelay { get; set; } = 20;

        public bool GenerateMp4 { get; set; } = false;

        public double Mp4FrameRate { get; set; } = 50d;

        public int FrameIndex { get; private set; } = -1;

        public double LaTeXScalingFactor
        {
            get => HtmlComposer.LaTeXScalingFactor; 
            set => HtmlComposer.LaTeXScalingFactor = value;
        }

        public WclHtmlImageDataUrlCache ImageCache
            => HtmlComposer.ImageCache;

        public int CanvasWidth { get; set; } = 1280;

        public int CanvasHeight { get; set; } = 720;

        public int GridUnitCount { get; set; } = 24;

        public double CameraDistance { get; set; } = 15;

        public int CameraRotationCount { get; set; } = 1;

        public Float64Vector3D AxesOrigin { get; set; } = Float64Vector3D.Zero;

        public bool ShowGrid { get; set; } = true;

        public bool ShowAxes { get; set; } = true;

        public bool ShowGuiLayer { get; set; } = true;

        public bool ShowCopyright { get; set; } = true;

        public IReadOnlyList<double> CameraAlphaValues { get; }

        public IReadOnlyList<double> CameraBetaValues { get; }

        public GrBabylonJsHtmlComposer3D HtmlComposer { get; protected set; }

        public GrBabylonJsSceneComposer3D MainSceneComposer
            => HtmlComposer.FirstSceneComposer;
    
        public GrBabylonJsScene MainScene
            => HtmlComposer.FirstScene;


        public GrBabylonJsSnapshotComposer3D(IReadOnlyList<double> cameraAlphaValues, IReadOnlyList<double> cameraBetaValues)
        {
            FrameCount = cameraAlphaValues.Count;
            CameraAlphaValues = cameraAlphaValues;
            CameraBetaValues = cameraBetaValues;
        }

    
        protected abstract void InitializeImageCache();

        protected abstract GrBabylonJsHtmlComposer3D InitializeSceneComposers(int index);

        protected virtual void AddCamera(int index)
        {
            // Add main scene camera
            MainSceneComposer.SceneObject.AddArcRotateCamera(
                "camera1",
                CameraAlphaValues[index], //"2 * Math.PI / 20",
                CameraBetaValues[index], //"2 * Math.PI / 5",
                CameraDistance,
                "BABYLON.Vector3.Zero()",
                new GrBabylonJsArcRotateCamera.ArcRotateCameraProperties
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

                new GrBabylonJsEnvironmentHelper.EnvironmentHelperOptions
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
                new GrVisualSquareGrid3D("grid", GrVisualSquareGridPlane3D.ZxPlane)
                {
                    UnitCount1 = GridUnitCount,
                    UnitCount2 = GridUnitCount,
                    UnitSize = 1,
                    DistanceToOrigin = 0,
                    Opacity = 0.25,
                    BaseSquareColor = Color.LightYellow,
                    BaseLineColor = Color.BurlyWood,
                    MidLineColor = Color.SandyBrown,
                    BorderLineColor = Color.SaddleBrown,
                    BaseSquareCount = 4,
                    BaseSquareSize = 64,
                    BaseLineWidth = 2,
                    MidLineWidth = 4,
                    BorderLineWidth = 3
                }
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

        protected abstract void AddGuiLayer(int index);

        protected virtual GrBabylonJsHtmlComposer3D GenerateSnapshotCode(int index)
        {
            HtmlComposer = InitializeSceneComposers(index);

            AddCamera(index);
            AddEnvironment();
            if (ShowGrid) AddGrid();
            if (ShowAxes) AddAxes();
            if (ShowGuiLayer) AddGuiLayer(index);

            return HtmlComposer;
        }

        private void GenerateHtmlFiles()
        {
            InitializeImageCache();

            Console.Write("Generating HTML files .. ");

            for (var index = 0; index < FrameCount; index++)
            {
                FrameIndex = index;

                var htmlCode = GenerateSnapshotCode(index).GetHtmlCode();

                var htmlFilePath = WorkingFolder.GetFilePath(
                    @$"Frame-{index:D6}", 
                    "html"
                );

                File.WriteAllText(htmlFilePath, htmlCode);
            }

            FrameIndex = -1;

            Console.WriteLine("Done.");
        }

        private void GeneratePngFiles()
        {
            const int delay1 = 2000;
            const int delay2 = 1000;
            const int tabCount = 10;

            var startTime = DateTime.Now;

            Console.Write("Generating Png files .. ");
        
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
            
                Console.WriteLine("Press any key to start png file generation ..");
                Console.WriteLine();
                Console.ReadKey();

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

                    var flag = false;
                    while (!flag)
                    {
                        flag = frameIndexList.All(i =>
                            File.Exists(WorkingFolder.GetFilePath(@$"Frame-{i:D6}", "png"))
                        );
                    }

                    //Thread.Sleep(delay1);
                    //Thread.Sleep(FrameIndex == 0 ? delay1 : delay2);

                    Console.WriteLine($"Snapshot {FrameIndex} done.");
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
            Console.WriteLine($"Done in {timeSpan.Minutes} minutes.");
        }

        private void GenerateAnimatedGifFile()
        {
            Console.Write("Generating Animated Gif file .. ");

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
            metadata.FrameDelay = AnimatedGifFrameDelay;

            for (var i = 0; i < FrameCount; i++)
            {
                using var imageStream = File.OpenRead(
                    WorkingFolder.GetFilePath(@$"Frame-{i:D6}", "png")
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
                WorkingFolder.GetFilePath(Title, "gif")
            );

            Console.WriteLine("Done.");
        }

        public virtual void GenerateSnapshots()
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

            GenerateHtmlFiles();

            if (!GeneratePng && !GenerateAnimatedGif && !GenerateMp4)
                return;

            GeneratePngFiles();

            if (GenerateAnimatedGif)
                GenerateAnimatedGifFile();
        }
    }
}