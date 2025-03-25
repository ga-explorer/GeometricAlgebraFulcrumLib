using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

public sealed class Float64ScalarAffineMappedSignal :
    Float64ScalarSignal
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarAffineMappedSignal Create(Float64ScalarSignal baseSignal, Float64AffineMap1D affineMap)
    {
        return new Float64ScalarAffineMappedSignal(baseSignal, affineMap);
    }


    public Float64ScalarSignal BaseSignal { get; }

    public Float64AffineMap1D AffineMap { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarAffineMappedSignal(Float64ScalarSignal baseSignal, Float64AffineMap1D affineMap)
        : base(baseSignal.TimeRange, baseSignal.IsPeriodic)
    {
        BaseSignal = baseSignal;
        AffineMap = affineMap;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseSignal.IsValid() &&
               AffineMap.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarAffineMappedSignal(
                BaseSignal.ToFiniteSignal(),
                AffineMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarAffineMappedSignal(
                BaseSignal.ToPeriodicSignal(),
                AffineMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarRange FindValueRange()
    {
        var (v1, v2) = BaseSignal.GetValueRange();

        return Float64ScalarRange.Create(
            AffineMap[v1],
            AffineMap[v2]
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarRange FindValueRange(double minTime, double maxTime)
    {
        var (v1, v2) = BaseSignal.FindValueRange(minTime, maxTime);

        return Float64ScalarRange.Create(
            AffineMap[v1],
            AffineMap[v2]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return AffineMap.MapPoint(
            BaseSignal.GetValue(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        return AffineMap.Scaling.ScalarValue *
               BaseSignal.GetDerivative1Value(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        return AffineMap.Scaling.ScalarValue *
               BaseSignal.GetDerivative2Value(t);
    }
}