using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Web.Images;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;
using Humanizer;
using SixLabors.ImageSharp;
// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;

public abstract class GrBabylonJsAnimationComposer
{
    public Float64SamplingSpecs SceneSamplingSpecs { get; }

    public int SceneSamplingRate
        => (int)SceneSamplingSpecs.SamplingRate;

    public double SceneMaxTime
        => SceneSamplingSpecs.MaxTime;
    
    public string WorkingFolder { get; }

    public string SceneTitle { get; protected set; }

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
    
    public GrBabylonJsCodeFilesComposer CodeFilesComposer { get; protected set; }

    public GrBabylonJsSceneComposer SceneComposer
        => CodeFilesComposer.FirstSceneComposer;

    public GrBabylonJsScene Scene
        => CodeFilesComposer.FirstScene;
    
    public double LaTeXScalingFactor { get; init; } 
        = 1 / 96d;

    public bool ShowCopyright { get; init; } 
        = true;

    public bool ShowGuiLayer { get; init; } 
        = true;
    

    protected GrBabylonJsAnimationComposer(string workingFolder, int samplingRate, double maxTime)
        : this(workingFolder, Float64SamplingSpecs.Create(samplingRate, maxTime))
    {

    }

    protected GrBabylonJsAnimationComposer(string workingFolder, Float64SamplingSpecs samplingSpecs)
    {
        WorkingFolder = Directory.Exists(workingFolder)
            ? workingFolder
            : throw new DirectoryNotFoundException(workingFolder);
        
        if (!samplingSpecs.IsValidForBabylonJs())
            throw new ArgumentException(nameof(samplingSpecs));

        SceneSamplingSpecs = samplingSpecs;

        KaTeXComposer = new WclKaTeXComposer(WorkingFolder)
        {
            FontSizeEm = 2,
            Output = WclKaTeXComposer.OutputKind.Html,
            ThrowOnError = false,
            SaveImages = false
        };

        SceneTitle = "Scene";
        TemporalScalars = new Float64ScalarSignalSet(SceneSamplingSpecs);
        ImageSet = new GrVisualImageSet(WorkingFolder);
        CameraSpecs = new GrVisualAnimatedCameraSpecs(SceneSamplingSpecs);

        var mainSceneComposer = new GrBabylonJsSceneComposer("mainScene")
        {
            BackgroundColor = Color.AliceBlue,
            ShowDebugLayer = false,
        };

        CodeFilesComposer = new GrBabylonJsCodeFilesComposer(mainSceneComposer)
        {
            CanvasWidth = CameraSpecs.CanvasWidth,
            CanvasHeight = CameraSpecs.CanvasHeight,
            CanvasFullScreen = false,
            HtmlPageTitle = SceneTitle
        };
    }

    
    public GrBabylonJsAnimationComposer SetTitle(string sceneTitle)
    {
        SceneTitle = sceneTitle;

        return this;
    }


    public GrBabylonJsAnimationComposer SetCanvas480p()
    {
        return SetCanvas(720, 480);
    }
    
    public GrBabylonJsAnimationComposer SetCanvas720p()
    {
        return SetCanvas(1280, 720);
    }
    
    public GrBabylonJsAnimationComposer SetCanvas1080p()
    {
        return SetCanvas(1920, 1080);
    }
    
    public GrBabylonJsAnimationComposer SetCanvas1440p()
    {
        return SetCanvas(2560, 1440);
    }
    
    public GrBabylonJsAnimationComposer SetCanvas2160p()
    {
        return SetCanvas(3840, 2160);
    }
    
    public GrBabylonJsAnimationComposer SetCanvas4320p()
    {
        return SetCanvas(7680, 4320);
    }

    public GrBabylonJsAnimationComposer SetCanvas(int width, int height)
    {
        CameraSpecs.SetCanvas(width, height);

        return this;
    }

    public virtual GrBabylonJsAnimationComposer SetCamera(Float64ScalarSignal alpha, Float64ScalarSignal beta, Float64ScalarSignal distance)
    {
        CameraSpecs.SetCamera(alpha, beta, distance);

        return this;
    }
    
    public GrBabylonJsAnimationComposer SetCamera(LinFloat64PolarAngle alpha, LinFloat64PolarAngle beta, double distance)
    {
        return SetCamera(
            alpha.RadiansValue.ToTimeSignal(CameraSpecs.TimeRange), 
            beta.RadiansValue.ToTimeSignal(CameraSpecs.TimeRange), 
            distance.ToTimeSignal(CameraSpecs.TimeRange)
        );
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

    protected virtual void InitializeSceneComposers()
    {
        var mainSceneComposer = new GrBabylonJsSceneComposer("mainScene")
        {
            BackgroundColor = Color.AliceBlue,
            ShowDebugLayer = false,
        };

        CodeFilesComposer = new GrBabylonJsCodeFilesComposer(mainSceneComposer)
        {
            CanvasWidth = CameraSpecs.CanvasWidth,
            CanvasHeight = CameraSpecs.CanvasHeight,
            CanvasFullScreen = false,
            HtmlPageTitle = SceneTitle
        };
    }
    
    protected virtual void SetCameraAndLights()
    {
        var (alpha, beta, distance) =
            GetCameraAlphaBetaDistanceAtFrame(0);

        // Add main scene camera
        SceneComposer.SceneObject.AddArcRotateCamera(
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
        SceneComposer.SceneObject.AddEnvironmentHelper(
            "environmentHelper",

            new GrBabylonJsEnvironmentHelperOptions
            {
                GroundYBias = 0.01,
                SkyboxColor = Color.LightSkyBlue,
                GroundColor = Color.White,
                CreateGround = true,
                GroundSize = 10,
                SkyboxSize = 50
            }
        );
    }

    protected abstract void AddGuiLayer();

    protected abstract void ComposeScene();

    public GrBabylonJsAnimationComposer BeginDrawing()
    {
        Console.WriteLine("Adding temporal values ..");
        
        TemporalScalars.Clear();
        AddTemporalValues();
        
        Console.WriteLine("Adding temporal values done.");
        Console.WriteLine();

        
        Console.WriteLine("Adding image textures ..");
        
        ImageSet.Clear();
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


        Console.WriteLine("Initializing scene composers ..");

        InitializeSceneComposers();

        Console.WriteLine("Initializing scene composers done.");
        Console.WriteLine();

        SetCameraAndLights();
        
        AddEnvironment();

        return this;
    } 

    public GrBabylonJsAnimationComposer EndDrawing()
    {
        Console.WriteLine("Composing scene code ..");
        
        ComposeScene();

        if (ShowGuiLayer) AddGuiLayer();

        Console.WriteLine("Composing scene code done.");
        Console.WriteLine();

        return this;
    }

    
    public string GetHtmlCode()
    {
        return CodeFilesComposer.GetHtmlCode();
    }

    public GrBabylonJsAnimationComposer SaveHtmlFile()
    {
        return SaveHtmlFile(SceneTitle.Pascalize());
    }

    public GrBabylonJsAnimationComposer SaveHtmlFile(string htmlFileName)
    {
        var htmlCode = CodeFilesComposer.GetHtmlCode();

        var htmlFilePath = WorkingFolder.GetFilePath(
            htmlFileName,
            "html"
        );

        File.WriteAllText(htmlFilePath, htmlCode);

        return this;
    }
}