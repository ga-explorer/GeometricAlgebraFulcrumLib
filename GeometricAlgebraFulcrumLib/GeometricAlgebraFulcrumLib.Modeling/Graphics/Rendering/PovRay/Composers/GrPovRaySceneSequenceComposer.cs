using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Cameras;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lights;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Lists;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Values;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using Instances;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.PovRay.Composers;

public abstract class GrPovRaySceneSequenceComposer :
    GrVisualSceneSequenceComposer
{
    public string PovRayFolder { get; set; }
        = @"C:\Program Files\POV-Ray\v3.7\bin\";

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

    public override GrVisualSceneSequenceComposer SetCanvas(int width, int height)
    {
        base.SetCanvas(width, height);

        DefaultRenderingOptions.Width = width;
        DefaultRenderingOptions.Height = height;

        return this;
    }

    protected override void AddImageTextures()
    {

    }

    protected override void CleanSceneFiles()
    {
        CleanOutputFiles("*.pov");
        CleanOutputFiles("*.ini");
        CleanOutputFiles("*.png");
        CleanOutputFiles("*.gif");
        CleanOutputFiles("*.mp4");
    }

    protected override void InitializeSceneComposers(int frameIndex)
    {
        ActiveSceneComposer = new GrPovRaySceneComposer(
            GetOutputPath(),
            GetFrameName(frameIndex), 
            DefaultRenderingOptions
        );

        ActiveSceneComposer.AddDefaultSkySphere();
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

    protected override void SaveSceneFiles(int frameIndex)
    {
        var sceneFilePath = GetOutputFilePath(GetFrameName(frameIndex), "pov");
        var optionsFilePath = GetOutputFilePath(GetFrameName(frameIndex), "ini");

        File.WriteAllText(
            sceneFilePath, 
            ActiveSceneComposer.SceneObject.GetPovRayCode()
        );
        
        File.WriteAllText(
            optionsFilePath, 
            ActiveSceneComposer.RenderingOptions.GetPovRayCode()
        );
    }

    protected override void RenderImageFile(int frameIndex)
    {
        try
        {
            var fileName = @GetFrameName(frameIndex);

            var sceneFilePath = GetOutputFilePath(fileName, "pov");
            var optionsFilePath = GetOutputFilePath(fileName, "ini");
            var imageFilePath = GetOutputFilePath(fileName, "png");

            if (File.Exists(imageFilePath))
                File.Delete(imageFilePath);

            Console.WriteLine($"    Rendering frame {frameIndex + 1} of {ImageCount} .. ");

            var exePath = Path.Combine(PovRayFolder, "pvengine.exe");
            var exeArgs = @$"/NORESTORE ""{optionsFilePath}"" /EXIT /RENDER ""{sceneFilePath}""";

            Console.WriteLine();
            Console.WriteLine(exePath + " " + exeArgs);
            Console.WriteLine();

            using var instance = Instance.Start(exePath, exeArgs);
            
            var result = instance.WaitForExit();

            Console.WriteLine("done");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
        }
    }

}