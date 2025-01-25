using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Temporal.Float64.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.BabylonJs.Composers;

public sealed class GrBabylonJsAnimationCameraSpecs
{
    public Float64SamplingSpecs SamplingSpecs { get; }

    private int _canvasWidth = 1024;
    public int CanvasWidth 
    { 
        get => _canvasWidth;
        set
        {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(value));

            _canvasWidth = value;
        }
    }

    private int _canvasHeight = 728;
    public int CanvasHeight
    {
        get => _canvasHeight;
        set
        {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(value));

            _canvasHeight = value;
        }
    }

    public double CanvasWidthToHeight 
        => _canvasWidth / (double)_canvasHeight;
    
    public double CanvasHeightToWidth 
        => _canvasHeight / (double)_canvasWidth;

    private TemporalFloat64Scalar _cameraDistance = 15;
    public TemporalFloat64Scalar CameraDistance
    {
        get => _cameraDistance;
        set => _cameraDistance = value.MapTimeRangeTo(0, SamplingSpecs.MaxTime);
    }
    
    private TemporalFloat64Scalar _cameraAlpha = 60.DegreesToRadians();
    public TemporalFloat64Scalar CameraAlpha
    {
        get => _cameraAlpha;
        set => _cameraAlpha = value.MapTimeRangeTo(0, SamplingSpecs.MaxTime);
    }

    private TemporalFloat64Scalar _cameraBeta = 60.DegreesToRadians();
    public TemporalFloat64Scalar CameraBeta
    {
        get => _cameraBeta;
        set => _cameraBeta = value.MapTimeRangeTo(0, SamplingSpecs.MaxTime);
    }
    
    public bool IsStatic
        => SamplingSpecs.IsStatic ||
           (_cameraAlpha is TsConstant &&
           _cameraBeta is TsConstant &&
           _cameraDistance is TsConstant);

    public bool IsAnimated
        => SamplingSpecs.IsAnimated &&
           _cameraAlpha is not TsConstant &&
           _cameraBeta is not TsConstant &&
           _cameraDistance is not TsConstant;


    public GrBabylonJsAnimationCameraSpecs(Float64SamplingSpecs samplingSpecs)
    {
        SamplingSpecs = samplingSpecs;
    }


    public GrBabylonJsAnimationCameraSpecs SetCanvas(int width, int height)
    {
        CanvasWidth = width;
        CanvasHeight = height;

        return this;
    }

    public GrBabylonJsAnimationCameraSpecs SetCamera(TemporalFloat64Scalar alpha, TemporalFloat64Scalar beta, TemporalFloat64Scalar distance)
    {
        CameraAlpha = alpha;
        CameraBeta = beta;
        CameraDistance = distance;

        return this;
    }


    public Pair<int> GetCanvasWidthHeight()
    {
        return new Pair<int>(CanvasWidth, CanvasHeight);
    }

    public Tuple<LinFloat64PolarAngle, LinFloat64PolarAngle, double> GetCameraAlphaBetaDistanceAtFrame(int frameIndex)
    {
        var time = frameIndex * SamplingSpecs.SamplingRate;

        return new Tuple<LinFloat64PolarAngle, LinFloat64PolarAngle, double>(
            CameraAlpha.GetValue(time).RadiansToPolarAngle(), 
            CameraBeta.GetValue(time).RadiansToPolarAngle(), 
            CameraDistance.GetValue(time)
        );
    } 
}