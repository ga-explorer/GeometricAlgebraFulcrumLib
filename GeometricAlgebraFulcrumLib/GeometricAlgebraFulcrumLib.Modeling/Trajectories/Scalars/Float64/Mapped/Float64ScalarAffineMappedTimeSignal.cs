using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

public sealed class Float64ScalarAffineMappedTimeSignal :
    Float64ScalarSignal
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarAffineMappedTimeSignal Create(Float64ScalarSignal baseSignal, Float64AffineMap1D affineMap)
    {
        return new Float64ScalarAffineMappedTimeSignal(baseSignal, affineMap);
    }


    public Float64ScalarSignal BaseSignal { get; }

    public Float64AffineMap1D AffineMap { get; }

    public Float64AffineMap1D AffineMapInverse { get; }


    private Float64ScalarAffineMappedTimeSignal(Float64ScalarSignal baseSignal, Float64AffineMap1D affineMap)
        : base(
            affineMap.Scaling > 0
                ? Float64ScalarRange.Create(
                    affineMap[baseSignal.MinTime],
                    affineMap[baseSignal.MaxTime]
                )
                : Float64ScalarRange.Create(
                    affineMap[baseSignal.MaxTime],
                    affineMap[baseSignal.MinTime]
                ),
            baseSignal.IsPeriodic
        )
    {
        BaseSignal = baseSignal;

        AffineMap = affineMap;
        AffineMapInverse = (Float64AffineMap1D)affineMap.GetInverseAffineMap();

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
            : new Float64ScalarAffineMappedTimeSignal(
                BaseSignal.ToFiniteSignal(),
                AffineMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarAffineMappedTimeSignal(
                BaseSignal.ToPeriodicSignal(),
                AffineMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarRange FindValueRange()
    {
        return BaseSignal.GetValueRange();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarRange FindValueRange(double minTime, double maxTime)
    {
        return BaseSignal.FindValueRange(minTime, maxTime);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        return BaseSignal.GetValue(
            AffineMapInverse.MapPoint(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        return AffineMapInverse.Scaling.ScalarValue *
               BaseSignal.GetDerivative1Value(AffineMapInverse.MapPoint(t));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        return AffineMapInverse.Scaling.ScalarValue.Square() *
               BaseSignal.GetDerivative2Value(AffineMapInverse.MapPoint(t));
    }
}