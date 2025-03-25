using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64;

/// <summary>
/// A parametric 3D curve with continuous first derivative
/// </summary>
public abstract class Float64Path3D(Float64ScalarRange timeRange, bool isPeriodic) :
    Float64Trajectory<LinFloat64Vector3D>(timeRange, isPeriodic)
{
    public abstract Float64Path3D ToFinitePath();

    public abstract Float64Path3D ToPeriodicPath();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IFloat64Trajectory ToFinite()
    {
        return ToFinitePath();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IFloat64Trajectory ToPeriodic()
    {
        return ToPeriodicPath();
    }
    

    public virtual Triplet<Float64ScalarSignal> GetScalarComponents()
    {
        if (IsFinite)
            return new Triplet<Float64ScalarSignal>(
                Float64ScalarSignal.FiniteComputed(
                    TimeRange,
                    t => GetValue(t).Item1.ScalarValue,
                    t => GetDerivative1Value(t).Item1.ScalarValue,
                    t => GetDerivative2Value(t).Item1.ScalarValue
                ),

                Float64ScalarSignal.FiniteComputed(
                    TimeRange,
                    t => GetValue(t).Item2.ScalarValue,
                    t => GetDerivative1Value(t).Item2.ScalarValue,
                    t => GetDerivative2Value(t).Item2.ScalarValue
                ),

                Float64ScalarSignal.FiniteComputed(
                    TimeRange,
                    t => GetValue(t).Item3.ScalarValue,
                    t => GetDerivative1Value(t).Item3.ScalarValue,
                    t => GetDerivative2Value(t).Item3.ScalarValue
                )
            );

        return new Triplet<Float64ScalarSignal>(
            Float64ScalarSignal.PeriodicComputed(
                TimeRange,
                t => GetValue(t).Item1.ScalarValue,
                t => GetDerivative1Value(t).Item1.ScalarValue,
                t => GetDerivative2Value(t).Item1.ScalarValue
            ),

            Float64ScalarSignal.PeriodicComputed(
                TimeRange,
                t => GetValue(t).Item2.ScalarValue,
                t => GetDerivative1Value(t).Item2.ScalarValue,
                t => GetDerivative2Value(t).Item2.ScalarValue
            ),

            Float64ScalarSignal.PeriodicComputed(
                TimeRange,
                t => GetValue(t).Item3.ScalarValue,
                t => GetDerivative1Value(t).Item3.ScalarValue,
                t => GetDerivative2Value(t).Item3.ScalarValue
            )
        );
    }


    private Triplet<Float64ScalarRange>? _valueRange;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Triplet<Float64ScalarRange> FindValueRange()
    {
        var (s1, s2, s3) = 
            GetScalarComponents();

        return new Triplet<Float64ScalarRange>(
            s1.GetValueRange(),
            s2.GetValueRange(),
            s3.GetValueRange()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<Float64ScalarRange> FindValueRange(double minTime, double maxTime)
    {
        var (s1, s2, s3) = 
            GetScalarComponents();

        return new Triplet<Float64ScalarRange>(
            s1.FindValueRange(minTime, maxTime),
            s2.FindValueRange(minTime, maxTime),
            s3.FindValueRange(minTime, maxTime)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<Float64ScalarRange> GetValueRange()
    {
        _valueRange ??= FindValueRange();

        return _valueRange;
    }


    public LinFloat64Vector3D GetDerivative1ValueNumerical(double t)
    {
        if (IsFinite && !TimeRange.Contains(t))
            return LinFloat64Vector3D.Zero;

        var epsilon = Math.Pow(2, -39); // Near 1.82e-12

        t = this.ClampTime(t);

        if (t < MinTime + 16 * epsilon)
            return (GetValue(t + epsilon) - GetValue(t)) / epsilon;

        if (t > MaxTime - 16 * epsilon)
            return (GetValue(t) - GetValue(t - epsilon)) / epsilon;

        var x = MathNet.Numerics.Differentiate.Derivative(
            t1 => GetValue(t1).Item1.ScalarValue, t, 1
        );

        var y = MathNet.Numerics.Differentiate.Derivative(
            t1 => GetValue(t1).Item2.ScalarValue, t, 1
        );

        var z = MathNet.Numerics.Differentiate.Derivative(
            t1 => GetValue(t1).Item3.ScalarValue, t, 1
        );

        return LinFloat64Vector3D.Create(x, y, z);
    }

    public LinFloat64Vector3D GetDerivative2ValueNumerical(double t)
    {
        if (IsFinite && !TimeRange.Contains(t))
            return LinFloat64Vector3D.Zero;

        var epsilon = Math.Pow(2, -39); // Near 1.82e-12

        t = this.ClampTime(t);

        if (t < MinTime + 16 * epsilon)
            return (GetDerivative1Value(t + epsilon) - GetDerivative1Value(t)) / epsilon;

        if (t > MaxTime - 16 * epsilon)
            return (GetDerivative1Value(t) - GetDerivative1Value(t - epsilon)) / epsilon;

        var x = MathNet.Numerics.Differentiate.Derivative(
            t1 => GetDerivative1Value(t1).Item1.ScalarValue, t, 1
        );

        var y = MathNet.Numerics.Differentiate.Derivative(
            t1 => GetDerivative1Value(t1).Item2.ScalarValue, t, 1
        );

        var z = MathNet.Numerics.Differentiate.Derivative(
            t1 => GetDerivative1Value(t1).Item3.ScalarValue, t, 1
        );

        return LinFloat64Vector3D.Create(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual LinFloat64Vector3D GetDerivative1Value(double t)
    {
        return GetDerivative1ValueNumerical(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual LinFloat64Vector3D GetDerivative2Value(double t)
    {
        return GetDerivative2ValueNumerical(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path3D GetDerivative1ErrorSignal()
    {
        return Float64ComputedPath3D.Finite(
            TimeRange,
            t => GetDerivative1Value(t) - GetDerivative1ValueNumerical(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Path3D GetDerivative2ErrorSignal()
    {
        return Float64ComputedPath3D.Finite(
            TimeRange,
            t => GetDerivative2Value(t) - GetDerivative2ValueNumerical(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Float64Path3DLocalFrame GetFrame(double t)
    {
        return Float64Path3DLocalFrame.Create(
            t,
            GetValue(t),
            GetDerivative1Value(t)
        );
    }

}