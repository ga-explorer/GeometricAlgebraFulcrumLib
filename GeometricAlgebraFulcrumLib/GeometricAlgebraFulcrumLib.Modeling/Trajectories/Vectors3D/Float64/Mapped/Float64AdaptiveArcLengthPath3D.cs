using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;

public sealed class Float64AdaptiveArcLengthPath3D :
    Float64ArcLengthPath3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptiveArcLengthPath3D Create(Float64Path3D basePath)
    {
        return new Float64AdaptiveArcLengthPath3D(
            basePath, 
            new Float64AdaptivePath3DSamplingOptions(
                5.DegreesToDirectedAngle(),
                3,
                16
            )
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AdaptiveArcLengthPath3D Create(Float64Path3D basePath, Float64AdaptivePath3DSamplingOptions samplingOptions)
    {
        return new Float64AdaptiveArcLengthPath3D(
            basePath, 
            samplingOptions
        );
    }


    private readonly Float64AdaptivePath3DSamplingOptions _adaptiveSamplingOptions;
    private readonly Float64AdaptivePath3D _adaptiveCurve;
    private readonly double _adaptiveCurveLength;

    public Float64Path3D BasePath { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AdaptiveArcLengthPath3D(Float64Path3D basePath, Float64AdaptivePath3DSamplingOptions samplingOptions)
        : base(basePath.TimeRange, basePath.IsPeriodic)
    {
        BasePath = basePath;

        _adaptiveSamplingOptions = samplingOptions;
        _adaptiveCurve = basePath.CreateAdaptiveCurve3D(samplingOptions);
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
    public override Float64ArcLengthPath3D ToFiniteArcLengthPath()
    {
        return IsFinite
            ? this
            : new Float64AdaptiveArcLengthPath3D(
                BasePath.ToFinitePath(), 
                _adaptiveSamplingOptions
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath3D ToPeriodicArcLengthPath()
    {
        return IsPeriodic
            ? this
            : new Float64AdaptiveArcLengthPath3D(
                BasePath.ToPeriodicPath(), 
                _adaptiveSamplingOptions
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        return BasePath.GetValue(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        return BasePath.GetDerivative1Value(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double t)
    {
        return BasePath.GetDerivative2Value(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3DLocalFrame GetFrame(double t)
    {
        return BasePath.GetFrame(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Triplet<Float64ScalarSignal> GetScalarComponents()
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