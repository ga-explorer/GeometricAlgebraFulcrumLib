using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space1D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;
using Humanizer;
using Instances;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;

public abstract class GrVisualSceneSequenceComposer :
    IGrVisualImageSequenceSource
{
    public enum RenderImageFilesMethod
    {
        Disabled,
        PerScene,
        Batch
    }

    protected static void WaitFor(int timeInMilliseconds)
    {
        Thread.Sleep(timeInMilliseconds);
    }

    protected static void Cleanup(IEnumerable<string> pathList)
    {
        foreach (var path in pathList)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }

    protected static void ConversionSizeExceptionCheck(int width, int height)
    {
        if (height % 2 != 0 || width % 2 != 0)
            throw new ArgumentException("FFMpeg yuv420p encoding requires the width and height to be a multiple of 2!");
    }


    public Float64SamplingSpecs SceneSamplingSpecs { get; }

    public Float64ScalarRange SceneTimeRange 
        => SceneSamplingSpecs.TimeRange;

    public int SceneSamplingRate
        => (int)SceneSamplingSpecs.SamplingRate;

    public double SceneMaxTime
        => SceneSamplingSpecs.MaxTime;
    
    public string WorkingFolder { get; }

    public string SceneTitle { get; init; } = "Scene";

    public string SceneFileName 
        => SceneTitle.Pascalize();

    public Float64ScalarSignalSet TemporalScalars { get; }
    
    public WclKaTeXComposer KaTeXComposer { get; }

    public GrVisualAnimatedCameraSpecs CameraSpecs { get; }
    
    public int ImageCount
        => SceneSamplingSpecs.SampleCount;

    public int ImageWidth 
        => CameraSpecs.CanvasWidth;

    public int ImageHeight 
        => CameraSpecs.CanvasHeight;

    public double ImageWidthToHeight
        => CameraSpecs.CanvasWidthToHeight;
    
    public double ImageHeightToWidth
        => CameraSpecs.CanvasWidthToHeight;
    
    public Pair<int> ImageSize
        => CameraSpecs.CanvasSize;

    public GrVisualImageSet ImageSet { get; }

    public int FrameIndex { get; protected set; } = -1;

    public Int32Range1D FrameRange { get; private set; }

    public bool ClearOutputFilesEnabled { get; set; }

    public bool ComposeSceneFilesEnabled { get; set; } = true;
    
    public RenderImageFilesMethod SceneRenderMethod { get; set; } 
        = RenderImageFilesMethod.PerScene;

    public bool RenderGifFileEnabled { get; init; }

    public int GifFrameDelay
        => SceneSamplingSpecs.TimeResolution.RoundToInt32();

    public bool RenderVideoFileEnabled { get; init; }

    public double VideoFrameRate
        => SceneSamplingSpecs.SamplingRate;

    public double LaTeXScalingFactor { get; init; }
        = 1 / 96d;

    public int GridUnitCount { get; init; } 
        = 24;

    public LinFloat64Vector3D AxesOrigin { get; init; } 
        = LinFloat64Vector3D.Zero;

    public bool ShowGrid { get; init; } = true;

    public bool ShowAxes { get; init; } = true;

    public bool ShowGuiLayer { get; init; } = true;

    public bool ShowCopyright { get; init; } = true;

    public string FfmpegFolder { get; set; }
        = @"D:\ffmpeg\bin";



    protected GrVisualSceneSequenceComposer(string workingFolder, Float64SamplingSpecs samplingSpecs)
    {
        WorkingFolder = Directory.Exists(workingFolder)
            ? workingFolder
            : throw new DirectoryNotFoundException(workingFolder);
        
        if (!samplingSpecs.IsValidForBabylonJs())
            throw new ArgumentException(nameof(samplingSpecs));

        SceneSamplingSpecs = samplingSpecs;
        FrameRange = Int32Range1D.Create(0, ImageCount);

        KaTeXComposer = new WclKaTeXComposer(WorkingFolder)
        {
            FontSizeEm = 2,
            Output = WclKaTeXComposer.OutputKind.Html,
            ThrowOnError = false,
            SaveImages = false
        };
        
        TemporalScalars = new Float64ScalarSignalSet(SceneSamplingSpecs);
        ImageSet = new GrVisualImageSet(WorkingFolder);
        CameraSpecs = new GrVisualAnimatedCameraSpecs(SceneSamplingSpecs);
    }

    
    public void SetFrameRange(int index1)
    {
        FrameRange = 
            Int32Range1D.Create(
                index1.ClampInt(ImageCount - 1),
                ImageCount - 1
            );
    }
    
    public void SetFrameRange(int index1, int index2)
    {
        FrameRange = 
            index1 <= index2
                ? Int32Range1D.Create(
                    index1.ClampInt(ImageCount - 1),
                    index2.ClampInt(ImageCount - 1)
                )
                : Int32Range1D.Create(
                    index2.ClampInt(ImageCount - 1),
                    index1.ClampInt(ImageCount - 1)
                );
    }

    public string GetFrameName(int imageIndex)
    {
        return $"Frame-{imageIndex:D6}";
    }

    public Image GetImage(int imageIndex)
    {
        return ImageSet.GetOrAddGroup("Output")[GetFrameName(imageIndex)].GetImage();
    }

    public string GetImageDataUrlBase64(int imageIndex)
    {
        return ImageSet.GetOrAddGroup("Output")[GetFrameName(imageIndex)].GetImageDataUrlBase64();
    }

    public string GetImageFilePath(int imageIndex)
    {
        return Path.Combine(
            ImageSet.GetOrAddGroup("Output").ImageSetGroupFolder, 
            GetFrameName(imageIndex) + ".png"
        );
    }
    
    public string GetOutputPath()
    {
        return Path.Combine(
            ImageSet.GetOrAddGroup("Output").ImageSetGroupFolder
        );
    }
    
    public string GetOutputFilePath(string fileName)
    {
        return Path.Combine(
            ImageSet.GetOrAddGroup("Output").ImageSetGroupFolder,
            fileName
        );
    }
    
    public string GetOutputFilePath(string fileName, string ext)
    {
        return Path.Combine(
            ImageSet.GetOrAddGroup("Output").ImageSetGroupFolder,
            fileName + "." + ext
        );
    }

    
    public GrVisualSceneSequenceComposer SetCanvas480p()
    {
        return SetCanvas(720, 480);
    }
    
    public GrVisualSceneSequenceComposer SetCanvas720p()
    {
        return SetCanvas(1280, 720);
    }
    
    public GrVisualSceneSequenceComposer SetCanvas1080p()
    {
        return SetCanvas(1920, 1080);
    }
    
    public GrVisualSceneSequenceComposer SetCanvas1440p()
    {
        return SetCanvas(2560, 1440);
    }
    
    public GrVisualSceneSequenceComposer SetCanvas2160p()
    {
        return SetCanvas(3840, 2160);
    }
    
    public GrVisualSceneSequenceComposer SetCanvas4320p()
    {
        return SetCanvas(7680, 4320);
    }

    public virtual GrVisualSceneSequenceComposer SetCanvas(int width, int height)
    {
        CameraSpecs.SetCanvas(width, height);

        return this;
    }
    
    public GrVisualSceneSequenceComposer SetCamera(double alpha, double beta, double distance)
    {
        return SetCamera(
            alpha.ToTimeSignal(CameraSpecs.TimeRange), 
            beta.ToTimeSignal(CameraSpecs.TimeRange), 
            distance.ToTimeSignal(CameraSpecs.TimeRange)
        );
    }

    public GrVisualSceneSequenceComposer SetCamera(Float64ScalarSignal alpha, double beta, double distance)
    {
        return SetCamera(
            alpha, 
            beta.ToTimeSignal(CameraSpecs.TimeRange), 
            distance.ToTimeSignal(CameraSpecs.TimeRange)
        );
    }

    public GrVisualSceneSequenceComposer SetCamera(Float64ScalarSignal alpha, Float64ScalarSignal beta, double distance)
    {
        return SetCamera(
            alpha, 
            beta, 
            distance.ToTimeSignal(CameraSpecs.TimeRange)
        );
    }

    public virtual GrVisualSceneSequenceComposer SetCamera(Float64ScalarSignal alpha, Float64ScalarSignal beta, Float64ScalarSignal distance)
    {
        CameraSpecs.SetCamera(alpha, beta, distance);

        return this;
    }
    
    public virtual GrVisualSceneSequenceComposer SetCamera(LinFloat64PolarAngle alpha, LinFloat64PolarAngle beta, double distance)
    {
        CameraSpecs.SetCamera(
            alpha.RadiansValue.ToTimeSignal(SceneTimeRange), 
            beta.RadiansValue.ToTimeSignal(SceneTimeRange), 
            distance.ToTimeSignal(SceneTimeRange)
        );

        return this;
    }

    public Tuple<LinFloat64PolarAngle, LinFloat64PolarAngle, double> GetCameraAlphaBetaDistanceAtFrame(int frameIndex)
    {
        return CameraSpecs.GetCameraAlphaBetaDistanceAtFrame(frameIndex);
    }


    protected virtual void AddTemporalValues()
    {
    }

    protected virtual void AddImageTextures()
    {
    }

    protected virtual void AddLaTeXTextures()
    {
    }
    
    protected void CleanOutputFiles(string fileNamePattern)
    {
        Array.ForEach(
            Directory.GetFiles(
                GetOutputPath(),
                fileNamePattern,
                SearchOption.TopDirectoryOnly
            ),
            File.Delete
        );

        bool filesExist;
        do
        {
            filesExist =
                Directory.GetFiles(
                    GetOutputPath(),
                    fileNamePattern,
                    SearchOption.TopDirectoryOnly
                ).Length > 0;
        } while (filesExist);
    }

    protected abstract void CleanSceneFiles();


    protected abstract void InitializeSceneComposers(int frameIndex);
    
    protected abstract void SetCameraAndLights(int frameIndex);

    protected abstract void ComposeScene(int frameIndex);

    protected virtual void AddEnvironment(int frameIndex)
    {

    }
    
    protected virtual void AddGuiLayer(int frameIndex)
    {

    }

    protected abstract void SaveSceneFiles(int frameIndex);
    
    protected abstract void RenderImageFile(int frameIndex);

    protected virtual void PostProcessImageFile(int frameIndex)
    {

    }

    protected virtual void BatchRenderImageFiles()
    {
        for (var frameIndex = 0; frameIndex < ImageCount; frameIndex++)
        {
            FrameIndex = frameIndex;

            RenderImageFile(frameIndex);
        }

        FrameIndex = -1;
    }
    
    public virtual void RenderGifFile()
    {
        Console.Write("Rendering animated GIF file .. ");

        // Image dimensions of the gif.
        using var firstImageStream = File.OpenRead(
            GetImageFilePath(0)
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

        for (var i = 0; i < ImageCount; i++)
        {
            using var imageStream = File.OpenRead(
                GetImageFilePath(i)
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
            GetOutputFilePath(SceneTitle, "gif")
        );

        Console.WriteLine("done.");
        Console.WriteLine();
    }

    public virtual void RenderVideoFile()
    {
        Console.Write("Rendering video file .. ");

        //var imageFileList = ImageCount.MapRange(GetImageFilePath).ToList();

        var videoFilePath = GetOutputFilePath(SceneTitle, "mp4");

        if (File.Exists(videoFilePath))
            File.Delete(videoFilePath);

        var inputFiles = GetOutputFilePath("Frame-%06d", "png");
        var outputFile = GetOutputFilePath(SceneFileName, "mp4");
        var exePath = Path.Combine(FfmpegFolder, "ffmpeg.exe");
        var exeArgs = @$"-framerate {VideoFrameRate} -r {VideoFrameRate} -i ""{inputFiles}"" -preset veryslow -crf 17 -pix_fmt yuv420p ""{outputFile}""";

        Console.WriteLine();
        Console.WriteLine(exePath + " " + exeArgs);
        Console.WriteLine();

        using var instance = Instance.Start(exePath, exeArgs);
        
        var result = instance.WaitForExit();
        
        //FFmpeg.SetExecutablesPath(FfmpegFolder);

        //var composer = new Conversion()
        //    .SetPreset(ConversionPreset.VerySlow)
        //    .SetInputFrameRate(VideoFrameRate)
        //    .BuildVideoFromImages(imageFileList)
        //    //.SetFrameRate(1)
        //    .SetPixelFormat(PixelFormat.yuv420p)
        //    .SetOutput(videoFilePath);
        
        //Console.WriteLine();
        //Console.WriteLine(composer.Build());
        //Console.WriteLine();

        //composer.Start();
        
        Console.WriteLine("done.");
        Console.WriteLine();
    }

    public GrVisualSceneSequenceComposer ComposeSceneSequence()
    {
        Console.WriteLine("Adding temporal values ..");
        
        TemporalScalars.Clear();
        AddTemporalValues();
        
        Console.WriteLine("Adding temporal values done.");
        Console.WriteLine();

        
        Console.WriteLine("Adding image textures ..");
        
        ImageSet.Clear();
        ImageSet.GetOrAddGroup("Output");

        if (ShowCopyright)
        {
            ImageSet.AddImageFromPngFile(
                "gui",
                "Copyright"
            );
        }

        AddImageTextures();

        KaTeXComposer.Clear();
        AddLaTeXTextures();
        KaTeXComposer.RenderKaTeX();

        ImageSet.AddImages(
            "latex", 
            KaTeXComposer
        );
        
        ImageSet.FinalizeGroups();

        Console.WriteLine("Adding image textures done.");
        Console.WriteLine();


        Console.WriteLine("Composing scene sequence ..");
        
        if (ClearOutputFilesEnabled) CleanSceneFiles();

        foreach (var frameIndex in FrameRange)
        {
            FrameIndex = frameIndex;

            Console.WriteLine($"Composing scene {frameIndex} ..");

            InitializeSceneComposers(frameIndex);

            SetCameraAndLights(frameIndex);

            AddEnvironment(frameIndex);

            ComposeScene(frameIndex);

            if (ShowGuiLayer) 
                AddGuiLayer(frameIndex);

            SaveSceneFiles(frameIndex);

            if (SceneRenderMethod == RenderImageFilesMethod.PerScene)
            {
                RenderImageFile(frameIndex);

                ImageSet.GetOrAddGroup("Output").AddOrSetImageFromPngFile(
                    GetFrameName(frameIndex)
                );

                PostProcessImageFile(frameIndex);

                // Sleep every 20 frames to cool down CPU
                if (frameIndex > 0 && frameIndex % 20 == 0)
                {
                    Console.Write("Sleeping for 10 seconds .. ");
                    Thread.Sleep(10 * 1000);
                    Console.WriteLine("done.");
                }
            }
        }

        FrameIndex = -1;

        Console.WriteLine("Composing scene sequence done.");
        Console.WriteLine();

        if (SceneRenderMethod == RenderImageFilesMethod.Batch)
        {
            Console.WriteLine("Rendering image files ..");
            Console.WriteLine();

            var startTime = DateTime.Now;

            BatchRenderImageFiles();

            for (var frameIndex = 0; frameIndex < ImageCount; frameIndex++)
            {
                ImageSet.GetOrAddGroup("Output").AddOrSetImageFromPngFile(
                    GetFrameName(frameIndex)
                );

                PostProcessImageFile(frameIndex);
            }

            var timeSpan = DateTime.Now - startTime;

            Console.WriteLine($"Rendering image files done in {timeSpan.Minutes} minutes.");
            Console.WriteLine();
        }
        
        if (RenderGifFileEnabled)
            RenderGifFile();

        if (RenderVideoFileEnabled)
            RenderVideoFile();

        return this;
    } 
    
    //public static double GetCpuTemperature()
    //{
    //    var mos = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");
        
    //    foreach (ManagementObject mo in mos.Get())
    //    {
    //        var temp = Convert.ToDouble(mo["CurrentTemperature"].ToString());
    //        var celsiusTemp = (temp - 2732) / 10.0;
    //        Console.WriteLine($"CPU Temperature: {celsiusTemp}°C");
    //    }

    //    return 0;
    //}
}