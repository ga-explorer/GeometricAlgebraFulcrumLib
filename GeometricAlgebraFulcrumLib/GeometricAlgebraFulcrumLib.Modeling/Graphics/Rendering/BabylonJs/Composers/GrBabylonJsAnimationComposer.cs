using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Animations;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Constants;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Basic;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Textures;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Files;
using GeometricAlgebraFulcrumLib.Utilities.Web.LaTeX.KaTeX;
using Humanizer;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;

public abstract class GrBabylonJsAnimationComposer
{
    public Float64SamplingSpecs SamplingSpecs { get; }
    
    public int SamplingRate
        => (int)SamplingSpecs.SamplingRate;

    public double MaxTime
        => SamplingSpecs.MaxTime;

    public string WorkingFolder { get; }
    
    public string SceneTitle { get; protected set; } = "Scene";

    public string SceneFileName { get; protected set; } = "Scene";

    public TemporalFloat64ScalarSet TemporalScalars { get; }

    public WclKaTeXComposer KaTeXComposer { get; }

    public GrBabylonJsAnimationCameraSpecs CameraSpecs { get; }

    public GrVisualTextureSet TextureSet { get; }
    
    public GrBabylonJsCodeFilesComposer CodeFilesComposer { get; protected set; }

    public GrBabylonJsSceneComposer SceneComposer
        => CodeFilesComposer.FirstSceneComposer;

    public GrBabylonJsScene Scene
        => CodeFilesComposer.FirstScene;
    
    public double LaTeXScalingFactor { get; init; } = 1 / 72d;

    public bool ShowCopyright { get; init; } = true;

    public bool ShowGuiLayer { get; init; } = false;
    

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

        SamplingSpecs = samplingSpecs;

        KaTeXComposer = new WclKaTeXComposer(WorkingFolder)
        {
            FontSizeEm = 2,
            Output = WclKaTeXComposer.OutputKind.Html,
            ThrowOnError = false,
            SaveImages = false
        };
        
        TemporalScalars = new TemporalFloat64ScalarSet(SamplingSpecs);
        TextureSet = new GrVisualTextureSet(WorkingFolder);
        CameraSpecs = new GrBabylonJsAnimationCameraSpecs(SamplingSpecs);

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

    
    public GrBabylonJsAnimationComposer SetFileNameAndTitle(string sceneFileName)
    {
        SceneFileName = sceneFileName;
        SceneTitle = sceneFileName;

        return this;
    }

    public GrBabylonJsAnimationComposer SetFileNameAndTitle(string sceneFileName, string sceneTitle)
    {
        SceneFileName = sceneFileName;
        SceneTitle = sceneTitle;

        return this;
    }
    
    public GrBabylonJsAnimationComposer SetCanvas(int width, int height)
    {
        CameraSpecs.SetCanvas(width, height);

        return this;
    }

    public virtual GrBabylonJsAnimationComposer SetCamera(TemporalFloat64Scalar alpha, TemporalFloat64Scalar beta, TemporalFloat64Scalar distance)
    {
        CameraSpecs.SetCamera(alpha, beta, distance);

        return this;
    }
    
    public virtual GrBabylonJsAnimationComposer SetCamera(LinFloat64PolarAngle alpha, LinFloat64PolarAngle beta, double distance)
    {
        CameraSpecs.SetCamera(
            alpha.RadiansValue, 
            beta.RadiansValue, 
            distance
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

    protected abstract void AddImageTextures();

    protected abstract void AddLaTeXTextures();

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

    protected virtual void AddGrid(GrVisualSquareGrid3D grid)
    {
        // Add ground coordinates grid
        SceneComposer.GridMaterialKind =
            GrBabylonJsGridMaterialKind.TexturedMaterial;

        SceneComposer.AddSquareGrid(grid);
    }

    protected virtual void AddAxes(ITriplet<Float64Scalar> origin)
    {
        var scene = SceneComposer.SceneObject;

        // Add reference unit axis frame
        SceneComposer.AddElement(
            GrVisualFrame3D.CreateStatic(
                "axisFrame",
                new GrVisualFrameStyle3D
                {
                    OriginStyle = scene.AddSimpleMaterial("axisFrameOriginMaterial", Color.DarkGray).CreateThickSurfaceStyle(0.075),
                    Direction1Style = scene.AddSimpleMaterial("axisFrameXMaterial", Color.DarkRed).CreateTubeCurveStyle(0.035),
                    Direction2Style = scene.AddSimpleMaterial("axisFrameYMaterial", Color.DarkGreen).CreateTubeCurveStyle(0.035),
                    Direction3Style = scene.AddSimpleMaterial("axisFrameZMaterial", Color.DarkBlue).CreateTubeCurveStyle(0.035)
                },
                origin.ToLinVector3D()
            )
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
        
        TextureSet.Clear();
        AddImageTextures();

        KaTeXComposer.Clear();
        AddLaTeXTextures();
        KaTeXComposer.RenderKaTeX();

        TextureSet.AddTextures(
            "latex", 
            KaTeXComposer
        );
        
        TextureSet.FinalizeTextures();

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