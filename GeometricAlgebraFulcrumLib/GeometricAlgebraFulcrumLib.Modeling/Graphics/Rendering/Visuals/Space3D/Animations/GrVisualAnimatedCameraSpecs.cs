using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Signals;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
// ReSharper disable InconsistentNaming

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Visuals.Space3D.Animations;

public sealed class GrVisualAnimatedCameraSpecs
{
    public Float64SamplingSpecs SamplingSpecs { get; }

    public Float64ScalarRange TimeRange 
        => SamplingSpecs.TimeRange;

    private int _canvasWidth = 1280;
    public int CanvasWidth
    {
        get => _canvasWidth;
        set
        {
            if (value < 1) throw new ArgumentOutOfRangeException(nameof(value));

            _canvasWidth = value;
        }
    }

    private int _canvasHeight = 720;
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

    public Pair<int> CanvasSize 
        => new Pair<int>(_canvasWidth, _canvasHeight);

    private Float64ScalarSignal _cameraDistance;
    public Float64ScalarSignal CameraDistance
    {
        get => _cameraDistance;
        set => _cameraDistance = value.MapTimeRangeTo(0, SamplingSpecs.MaxTime);
    }

    private Float64ScalarSignal _cameraAlpha;
    public Float64ScalarSignal CameraAlpha
    {
        get => _cameraAlpha;
        set => _cameraAlpha = value.MapTimeRangeTo(0, SamplingSpecs.MaxTime);
    }

    private Float64ScalarSignal _cameraBeta;
    public Float64ScalarSignal CameraBeta
    {
        get => _cameraBeta;
        set => _cameraBeta = value.MapTimeRangeTo(0, SamplingSpecs.MaxTime);
    }

    public bool IsStatic
        => SamplingSpecs.IsStatic ||
           (_cameraAlpha is Float64ScalarConstantOneSignal &&
           _cameraBeta is Float64ScalarConstantOneSignal &&
           _cameraDistance is Float64ScalarConstantOneSignal);

    public bool IsAnimated
        => !IsStatic;


    public GrVisualAnimatedCameraSpecs(Float64SamplingSpecs samplingSpecs)
    {
        SamplingSpecs = samplingSpecs;

        _cameraDistance = Float64ScalarSignal.FiniteConstant(samplingSpecs.TimeRange, 15);
        _cameraAlpha = Float64ScalarSignal.FiniteConstant(samplingSpecs.TimeRange, 135.DegreesToRadians());
        _cameraBeta = Float64ScalarSignal.FiniteConstant(samplingSpecs.TimeRange, 45.DegreesToRadians());
    }


    public GrVisualAnimatedCameraSpecs SetCanvas480p()
    {
        return SetCanvas(720, 480);
    }
    
    public GrVisualAnimatedCameraSpecs SetCanvas720p()
    {
        return SetCanvas(1280, 720);
    }
    
    public GrVisualAnimatedCameraSpecs SetCanvas1080p()
    {
        return SetCanvas(1920, 1080);
    }
    
    public GrVisualAnimatedCameraSpecs SetCanvas1440p()
    {
        return SetCanvas(2560, 1440);
    }
    
    public GrVisualAnimatedCameraSpecs SetCanvas2160p()
    {
        return SetCanvas(3840, 2160);
    }
    
    public GrVisualAnimatedCameraSpecs SetCanvas4320p()
    {
        return SetCanvas(7680, 4320);
    }

    public GrVisualAnimatedCameraSpecs SetCanvas(int width, int height)
    {
        CanvasWidth = width;
        CanvasHeight = height;

        return this;
    }
    

    public GrVisualAnimatedCameraSpecs SetCamera(double alpha, double beta, double distance)
    {
        CameraAlpha = alpha.ToTimeSignal(TimeRange);
        CameraBeta = beta.ToTimeSignal(TimeRange);
        CameraDistance = distance.ToTimeSignal(TimeRange);

        return this;
    }
    
    public GrVisualAnimatedCameraSpecs SetCamera(LinFloat64PolarAngle alpha, LinFloat64PolarAngle beta, double distance)
    {
        CameraAlpha = alpha.RadiansValue.ToTimeSignal(TimeRange);
        CameraBeta = beta.RadiansValue.ToTimeSignal(TimeRange);
        CameraDistance = distance.ToTimeSignal(TimeRange);

        return this;
    }

    public GrVisualAnimatedCameraSpecs SetCamera(Float64ScalarSignal alpha, double beta, double distance)
    {
        CameraAlpha = alpha.MapTimeRangeTo(TimeRange);
        CameraBeta = beta.ToTimeSignal(TimeRange);
        CameraDistance = distance.ToTimeSignal(TimeRange);

        return this;
    }
    
    public GrVisualAnimatedCameraSpecs SetCamera(Float64ScalarSignal alpha, LinFloat64PolarAngle beta, double distance)
    {
        CameraAlpha = alpha.MapTimeRangeTo(TimeRange);
        CameraBeta = beta.RadiansValue.ToTimeSignal(TimeRange);
        CameraDistance = distance.ToTimeSignal(TimeRange);

        return this;
    }

    public GrVisualAnimatedCameraSpecs SetCamera(Float64ScalarSignal alpha, Float64ScalarSignal beta, double distance)
    {
        CameraAlpha = alpha.MapTimeRangeTo(TimeRange);
        CameraBeta = beta.MapTimeRangeTo(TimeRange);
        CameraDistance = distance.ToTimeSignal(TimeRange);

        return this;
    }

    public GrVisualAnimatedCameraSpecs SetCamera(Float64ScalarSignal alpha, Float64ScalarSignal beta, Float64ScalarSignal distance)
    {
        CameraAlpha = alpha.MapTimeRangeTo(TimeRange);
        CameraBeta = beta.MapTimeRangeTo(TimeRange);
        CameraDistance = distance.MapTimeRangeTo(TimeRange);

        return this;
    }


    public Pair<int> GetCanvasWidthHeight()
    {
        return new Pair<int>(CanvasWidth, CanvasHeight);
    }

    public Tuple<LinFloat64PolarAngle, LinFloat64PolarAngle, double> GetCameraAlphaBetaDistanceAtFrame(int frameIndex)
    {
        var time = frameIndex * SamplingSpecs.TimeResolution;

        return new Tuple<LinFloat64PolarAngle, LinFloat64PolarAngle, double>(
            CameraAlpha.GetValue(time).RadiansToPolarAngle(),
            CameraBeta.GetValue(time).RadiansToPolarAngle(),
            CameraDistance.GetValue(time)
        );
    }
}