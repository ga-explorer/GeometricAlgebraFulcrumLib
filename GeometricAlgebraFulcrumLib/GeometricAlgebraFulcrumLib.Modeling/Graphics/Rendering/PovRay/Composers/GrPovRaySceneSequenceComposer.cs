using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using Instances;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using Xabe.FFmpeg;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;

public abstract class GrPovRaySceneSequenceComposer :
    GrVisualSceneSequenceComposer
{
    public GrPovRaySceneComposer ActiveSceneComposer { get; protected set; }

    public GrPovRayScene ActiveSceneObject
        => ActiveSceneComposer.SceneObject;
    
    public GrPovRayStatementList ActiveSceneStatements
        => ActiveSceneComposer.SceneObject.Statements;

    public GrPovRayRenderingOptions ActiveRenderingOptions
        => ActiveSceneComposer.SceneObject.RenderingOptions;

    public GrPovRayRenderingOptions DefaultRenderingOptions { get; } 
        = new GrPovRayRenderingOptions();


    protected GrPovRaySceneSequenceComposer(string workingFolder, Float64SamplingSpecs samplingSpecs)
        : base(workingFolder, samplingSpecs)
    {
    }

    
    protected override void InitializeTextureSet()
    {

    }

    protected override void InitializeSceneComposers(int frameIndex)
    {

    }

    protected override void SetCameraAndLights(int frameIndex)
    {
        var (alpha, beta, distance) = 
            GetCameraAlphaBetaDistanceAtFrame(frameIndex);

        var camera = 
            GrPovRayCamera.ArcRotatePerspective(
                alpha,
                beta,
                distance,
                LinFloat64Vector3D.Zero, //LinFloat64Vector3D.E2 * 1.001,
                67.DegreesToPolarAngle(),
                DefaultRenderingOptions.AspectRatio
            );

        camera.FlashLightColor = 
            GrPovRayColorValue.Rgb(0.9, 0.9, 1);

        ActiveSceneObject.Camera = camera;

        // Sunlight
        ActiveSceneObject.AddStatement(
            GrPovRayLightSource.PointLight(
                LinFloat64Vector3D.Create(-1500, 2500, -2500),
                GrPovRayColorValue.Rgb(0.9)
            )
        );

        // Camera flashlight
        if (camera.FlashLightColor is not null)
            ActiveSceneObject.AddStatement(
                GrPovRayLightSource.PointLight(
                    camera.Position,
                    camera.FlashLightColor //GrPovRayColorValue.Rgb(0.9, 0.9, 1)
                )
            );
    }

    protected virtual void AddEnvironment()
    {
        ////var scene = MainSceneComposer.SceneObject;
        ////scene.SceneProperties.AmbientColor = Color.AliceBlue;

        //// Add scene environment
        //ActiveSceneComposer.SceneObject.AddEnvironmentHelper(
        //    "environmentHelper",

        //    new GrPovRayEnvironmentHelperOptions
        //    {
        //        GroundYBias = 0.01,
        //        SkyboxColor = Color.LightSkyBlue,
        //        GroundColor = Color.White,
        //        CreateGround = true,
        //        GroundSize = 8,
        //        SkyboxSize = GridUnitCount + 10
        //    }
        //);
    }

    protected virtual void AddGrid()
    {
        // Add ground coordinates grid
        ActiveSceneComposer.GridMaterialKind =
            GrPovRayGridMaterialKind.TexturedMaterial;

        ActiveSceneComposer.AddSquareGrid(
            GrVisualSquareGrid3D.DefaultZx(
                LinFloat64Vector3D.Zero, 
                GridUnitCount,
                1,
                0.25
            )
        );
    }

    protected virtual void AddAxes()
    {
        var scene = ActiveSceneComposer.SceneObject;

        // Add reference unit axis frame
        ActiveSceneComposer.AddElement(
            GrVisualFrame3D.CreateStatic(
                "axisFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = scene.Statements.DeclareMaterial("axisFrameOriginMaterial", Color.DarkGray).CreateThickSurfaceStyle(0.075),
                    Direction1Style = scene.Statements.DeclareMaterial("axisFrameXMaterial", Color.DarkRed).CreateTubeCurveStyle(0.035),
                    Direction2Style = scene.Statements.DeclareMaterial("axisFrameYMaterial", Color.DarkGreen).CreateTubeCurveStyle(0.035),
                    Direction3Style = scene.Statements.DeclareMaterial("axisFrameZMaterial", Color.DarkBlue).CreateTubeCurveStyle(0.035)
                },
                AxesOrigin
            )
        );
    }

    protected virtual void AddGuiLayer(int frameIndex)
    {

    }

    protected override void ComposeFrame(int frameIndex)
    {
        ActiveSceneComposer = new GrPovRaySceneComposer(
            WorkingFolder,
            $"Frame-{frameIndex:D6}", 
            DefaultRenderingOptions
        );

        InitializeSceneComposers(frameIndex);

        SetCameraAndLights(frameIndex);
        AddEnvironment();

        if (ShowGrid) AddGrid();
        if (ShowAxes) AddAxes();
        if (ShowGuiLayer) AddGuiLayer(frameIndex);
    }


    protected override void ComposeSceneFiles()
    {
        InitializeTemporalValues();

        InitializeTextureSet();

        Console.Write("Generating scene files .. ");

        for (var frameIndex = 0; frameIndex < FrameCount; frameIndex++)
        {
            FrameIndex = frameIndex;

            ComposeFrame(frameIndex);

            var sceneFilePath = WorkingFolder.GetFilePath(
                @$"Frame-{frameIndex:D6}",
                "pov"
            );
            
            var optionsFilePath = WorkingFolder.GetFilePath(
                @$"Frame-{frameIndex:D6}",
                "ini"
            );

            File.WriteAllText(
                sceneFilePath, 
                ActiveSceneComposer.SceneObject.GetPovRayCode()
            );
            
            File.WriteAllText(
                optionsFilePath, 
                ActiveSceneComposer.RenderingOptions.GetPovRayCode()
            );
        }

        FrameIndex = -1;

        Console.WriteLine("Done.");
    }

    protected override void RenderImageFiles()
    {
        Console.WriteLine("Rendering image files ..");
        Console.WriteLine();

        var startTime = DateTime.Now;

        try
        {
            for (var frameIndex = 0; frameIndex < FrameCount; frameIndex++)
            {
                var fileName = @$"Frame-{frameIndex:D6}";

                var sceneFilePath = WorkingFolder.GetFilePath(fileName, "pov");
                var optionsFilePath = WorkingFolder.GetFilePath(fileName, "ini");
                var imageFilePath = WorkingFolder.GetFilePath(fileName, "png");

                if (File.Exists(imageFilePath))
                    File.Delete(imageFilePath);

                Console.Write($"    Rendering frame {frameIndex + 1} of {FrameCount} .. ");

                using var instance = Instance.Start(
                    @"C:\Program Files\POV-Ray\v3.7\bin\pvengine.exe",
                    @$"/NORESTORE ""{optionsFilePath}"" /EXIT /RENDER ""{sceneFilePath}"""
                );
                
                var result = instance.WaitForExit();

                Console.WriteLine("done");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
        }

        FrameIndex = -1;

        var timeSpan = DateTime.Now - startTime;

        Console.WriteLine($"Rendering image files done in {timeSpan.Minutes} minutes.");
        Console.WriteLine();
    }

    protected override void RenderGifFile()
    {
        Console.Write("Rendering animated GIF file .. ");

        // Image dimensions of the gif.
        using var firstImageStream = File.OpenRead(
            WorkingFolder.GetFilePath(@$"Frame-{(0):D6}", "png")
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
            .SetPixelFormat(PixelFormat.yuv440p)
            .SetOutput(videoFilePath)
            .Start();

        Console.WriteLine("done.");
        Console.WriteLine();
    }

    public override void RenderFiles()
    {
        // Delete any old pov\ini\png files
        Array.ForEach(
            Directory.GetFiles(
                WorkingFolder,
                @"Frame-*.pov",
                SearchOption.TopDirectoryOnly
            ),
            File.Delete
        );

        Array.ForEach(
            Directory.GetFiles(
                WorkingFolder,
                @"Frame-*.ini",
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

        bool povFilesExist;
        bool iniFilesExist;
        bool pngFilesExist;
        do
        {
            povFilesExist =
                Directory.GetFiles(
                    WorkingFolder,
                    @"Frame-*.pov",
                    SearchOption.TopDirectoryOnly
                ).Length > 0;

            iniFilesExist =
                Directory.GetFiles(
                    WorkingFolder,
                    @"Frame-*.ini",
                    SearchOption.TopDirectoryOnly
                ).Length > 0;

            pngFilesExist =
                Directory.GetFiles(
                    WorkingFolder,
                    @"Frame-*.png",
                    SearchOption.TopDirectoryOnly
                ).Length > 0;
        } while (povFilesExist || iniFilesExist || pngFilesExist);

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