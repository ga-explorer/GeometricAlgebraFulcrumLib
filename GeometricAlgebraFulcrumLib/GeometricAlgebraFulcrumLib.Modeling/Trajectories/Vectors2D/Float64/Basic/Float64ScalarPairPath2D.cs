using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

public sealed class Float64ScalarPairPath2D :
    Float64Path2D,
    IPair<Float64ScalarSignal>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarPairPath2D Finite(Float64ScalarSignal item1, Float64ScalarSignal item2)
    {
        return new Float64ScalarPairPath2D(
            item1.TimeRange.Intersect(item2.TimeRange), 
            false, 
            item1, 
            item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarPairPath2D Finite(Float64ScalarRange timeRange, Float64ScalarSignal item1, Float64ScalarSignal item2)
    {
        return new Float64ScalarPairPath2D(
            timeRange, 
            false, 
            item1, 
            item2
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarPairPath2D Periodic(Float64ScalarSignal item1, Float64ScalarSignal item2)
    {
        return new Float64ScalarPairPath2D(
            item1.TimeRange.Intersect(item2.TimeRange), 
            true, 
            item1, 
            item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarPairPath2D Periodic(Float64ScalarRange timeRange, Float64ScalarSignal item1, Float64ScalarSignal item2)
    {
        return new Float64ScalarPairPath2D(
            timeRange, 
            true, 
            item1, 
            item2
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarPairPath2D Create(bool isPeriodic, Float64ScalarSignal item1, Float64ScalarSignal item2)
    {
        return new Float64ScalarPairPath2D(
            item1.TimeRange.Intersect(item2.TimeRange), 
            isPeriodic, 
            item1, 
            item2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarPairPath2D Create(Float64ScalarRange timeRange, bool isPeriodic, Float64ScalarSignal item1, Float64ScalarSignal item2)
    {
        return new Float64ScalarPairPath2D(
            timeRange, 
            isPeriodic, 
            item1, 
            item2
        );
    }


    public Float64ScalarSignal Item1 { get; }

    public Float64ScalarSignal Item2 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarPairPath2D(Float64ScalarRange timeRange, bool isPeriodic, Float64ScalarSignal item1, Float64ScalarSignal item2) 
        : base(timeRange, isPeriodic)
    {
        Item1 = item1;
        Item2 = item2;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Item1.IsValid() &&
               Item2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return LinFloat64Vector2D.Create(
            Item1.GetValue(t), 
            Item2.GetValue(t)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return LinFloat64Vector2D.Create(
            Item1.GetDerivative1Value(t), 
            Item2.GetDerivative1Value(t)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        return LinFloat64Vector2D.Create(
            Item1.GetDerivative2Value(t), 
            Item2.GetDerivative2Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToFinitePath()
    {
        return IsFinite
            ? this
            : new Float64ScalarPairPath2D(
                TimeRange,
                false,
                Item1,
                Item2
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new Float64ScalarPairPath2D(
                TimeRange,
                true,
                Item1,
                Item2
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Pair<Float64ScalarSignal> GetScalarComponents()
    {
        return new Pair<Float64ScalarSignal>(Item1, Item2);
    }
}