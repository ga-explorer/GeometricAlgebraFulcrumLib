using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Mapped;

public sealed class Float64AdaptiveArcLengthPath2D :
    Float64ArcLengthPath2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptiveArcLengthPath2D Create(Float64Path2D basePath)
    {
        return new Float64AdaptiveArcLengthPath2D(
            basePath, 
            new Float64AdaptivePath2DSamplingOptions(
                5.DegreesToDirectedAngle(),
                3,
                16
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptiveArcLengthPath2D Create(Float64Path2D basePath, Float64AdaptivePath2DSamplingOptions samplingOptions)
    {
        return new Float64AdaptiveArcLengthPath2D(
            basePath, 
            samplingOptions
        );
    }


    private readonly Float64AdaptivePath2DSamplingOptions _adaptiveSamplingOptions;
    private readonly Float64AdaptivePath2D _adaptiveCurve;
    private readonly double _adaptiveCurveLength;

    public Float64Path2D BasePath { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AdaptiveArcLengthPath2D(Float64Path2D basePath, Float64AdaptivePath2DSamplingOptions samplingOptions)
        : base(basePath.TimeRange, basePath.IsPeriodic)
    {
        BasePath = basePath;

        _adaptiveSamplingOptions = samplingOptions;
        _adaptiveCurve = basePath.CreateAdaptiveCurve2D(samplingOptions);
        _adaptiveCurveLength = _adaptiveCurve.GetLength();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BasePath.IsValid() &&
               _adaptiveCurve.IsValid() && 
               _adaptiveCurveLength.IsFinite();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath2D ToFiniteArcLengthPath()
    {
        return IsFinite
            ? this
            : new Float64AdaptiveArcLengthPath2D(
                BasePath.ToFinitePath(), 
                _adaptiveSamplingOptions
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath2D ToPeriodicArcLengthPath()
    {
        return IsPeriodic
            ? this
            : new Float64AdaptiveArcLengthPath2D(
                BasePath.ToPeriodicPath(), 
                _adaptiveSamplingOptions
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return BasePath.GetValue(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return BasePath.GetDerivative1Value(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        return BasePath.GetDerivative2Value(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2DLocalFrame GetFrame(double t)
    {
        return BasePath.GetFrame(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<Float64ScalarSignal> GetScalarComponents()
    {
        return BasePath.GetScalarComponents();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar GetLength()
    {
        return _adaptiveCurveLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar TimeToLength(double t)
    {
        return _adaptiveCurve.TimeToLength(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar LengthToTime(double length)
    {
        length = length.ClampPeriodic(_adaptiveCurveLength);

        return _adaptiveCurve.LengthToTime(length);
    }

}