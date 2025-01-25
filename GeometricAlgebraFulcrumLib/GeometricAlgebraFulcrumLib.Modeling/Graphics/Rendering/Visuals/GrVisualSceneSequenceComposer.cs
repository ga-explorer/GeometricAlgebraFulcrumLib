using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Textures;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals;

public abstract class GrVisualSceneSequenceComposer
{
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


    public string Title { get; init; }

    public Float64SamplingSpecs FrameSpecs
        => TemporalScalarSet.SamplingSpecs;

    public int FrameCount
        => TemporalScalarSet.FrameCount;

    public int FrameIndex { get; protected set; } = -1;

    public string WorkingFolder { get; }

    public bool ComposeSceneFilesEnabled { get; set; } = true;

    public bool RenderImageFilesEnabled { get; init; }

    public bool RenderGifFileEnabled { get; init; }

    public int GifFrameDelay
        => TemporalScalarSet.FrameTime.RoundToInt32();

    public bool RenderVideoFileEnabled { get; init; }

    public double VideoFrameRate
        => TemporalScalarSet.FrameRate;

    public double LaTeXScalingFactor { get; init; }
        = 1 / 72d;

    public GrVisualTextureSet TextureSet { get; }

    public TemporalFloat64ScalarSet TemporalScalarSet { get; }

    public int GridUnitCount { get; init; } 
        = 24;

    public LinFloat64Vector3D AxesOrigin { get; init; } 
        = LinFloat64Vector3D.Zero;

    public bool ShowGrid { get; init; } = true;

    public bool ShowAxes { get; init; } = true;

    public bool ShowGuiLayer { get; init; } = true;

    public bool ShowCopyright { get; init; } = true;


    protected GrVisualSceneSequenceComposer(string workingFolder, Float64SamplingSpecs samplingSpecs)
    {
        WorkingFolder = workingFolder;

        TextureSet = new GrVisualTextureSet(WorkingFolder);
        TemporalScalarSet = new TemporalFloat64ScalarSet(samplingSpecs);
    }


    protected abstract void InitializeTemporalValues();

    protected abstract void InitializeTextureSet();

    protected abstract void InitializeSceneComposers(int frameIndex);


    public Tuple<LinFloat64PolarAngle, LinFloat64PolarAngle, double> GetCameraAlphaBetaDistanceAtFrame(int frameIndex)
    {
        var alpha =
            TemporalScalarSet.GetScalarAtFrame(
                "camera.alpha",
                frameIndex
            ).RadiansToPolarAngle();

        var beta =
            TemporalScalarSet.GetScalarAtFrame(
                "camera.beta",
                frameIndex
            ).RadiansToPolarAngle();

        var distance =
            TemporalScalarSet.GetScalarAtFrame(
                "camera.distance",
                frameIndex
            );

        return new Tuple<LinFloat64PolarAngle, LinFloat64PolarAngle, double>(
            alpha, beta, distance
        );
    }

    public void SetCameraAlphaBetaDistance(TemporalFloat64Scalar alpha, TemporalFloat64Scalar beta, TemporalFloat64Scalar distance)
    {
        TemporalScalarSet.SetScalar("camera.alpha", alpha);
        TemporalScalarSet.SetScalar("camera.beta", beta);
        TemporalScalarSet.SetScalar("camera.distance", distance);
    }


    protected abstract void SetCameraAndLights(int frameIndex);

    protected abstract void ComposeFrame(int frameIndex);

    protected abstract void ComposeSceneFiles();

    protected abstract void RenderImageFiles();

    protected abstract void RenderGifFile();

    protected abstract void RenderVideoFile();

    public abstract void RenderFiles();
}