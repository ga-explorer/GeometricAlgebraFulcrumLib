using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

public sealed class Float64ScalarTripletPath3D :
    Float64Path3D,
    ITriplet<Float64ScalarSignal>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarTripletPath3D Finite(Float64ScalarSignal item1, Float64ScalarSignal item2, Float64ScalarSignal item3)
    {
        return new Float64ScalarTripletPath3D(
            item1.TimeRange.Intersect(item2.TimeRange), 
            false, 
            item1, 
            item2, 
            item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarTripletPath3D Finite(Float64ScalarRange timeRange, Float64ScalarSignal item1, Float64ScalarSignal item2, Float64ScalarSignal item3)
    {
        return new Float64ScalarTripletPath3D(
            timeRange, 
            false, 
            item1, 
            item2, 
            item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarTripletPath3D Periodic(Float64ScalarSignal item1, Float64ScalarSignal item2, Float64ScalarSignal item3)
    {
        return new Float64ScalarTripletPath3D(
            item1.TimeRange.Intersect(item2.TimeRange), 
            true, 
            item1, 
            item2, 
            item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarTripletPath3D Periodic(Float64ScalarRange timeRange, Float64ScalarSignal item1, Float64ScalarSignal item2, Float64ScalarSignal item3)
    {
        return new Float64ScalarTripletPath3D(
            timeRange, 
            true, 
            item1, 
            item2, 
            item3
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarTripletPath3D Create(bool isPeriodic, Float64ScalarSignal item1, Float64ScalarSignal item2, Float64ScalarSignal item3)
    {
        return new Float64ScalarTripletPath3D(
            item1.TimeRange.Intersect(item2.TimeRange), 
            isPeriodic, 
            item1, 
            item2, 
            item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarTripletPath3D Create(Float64ScalarRange timeRange, bool isPeriodic, Float64ScalarSignal item1, Float64ScalarSignal item2, Float64ScalarSignal item3)
    {
        return new Float64ScalarTripletPath3D(
            timeRange, 
            isPeriodic, 
            item1, 
            item2, 
            item3
        );
    }


    public Float64ScalarSignal Item1 { get; }

    public Float64ScalarSignal Item2 { get; }
    
    public Float64ScalarSignal Item3 { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarTripletPath3D(Float64ScalarRange timeRange, bool isPeriodic, Float64ScalarSignal item1, Float64ScalarSignal item2, Float64ScalarSignal item3) 
        : base(timeRange, isPeriodic)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Item1.IsValid() &&
               Item2.IsValid() &&
               Item3.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        return LinFloat64Vector3D.Create(
            Item1.GetValue(t), 
            Item2.GetValue(t), 
            Item3.GetValue(t)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        return LinFloat64Vector3D.Create(
            Item1.GetDerivative1Value(t), 
            Item2.GetDerivative1Value(t), 
            Item3.GetDerivative1Value(t)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double t)
    {
        return LinFloat64Vector3D.Create(
            Item1.GetDerivative2Value(t), 
            Item2.GetDerivative2Value(t), 
            Item3.GetDerivative2Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToFinitePath()
    {
        return IsFinite
            ? this
            : new Float64ScalarTripletPath3D(
                TimeRange,
                false,
                Item1,
                Item2,
                Item3
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new Float64ScalarTripletPath3D(
                TimeRange,
                true,
                Item1,
                Item2,
                Item3
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Triplet<Float64ScalarSignal> GetScalarComponents()
    {
        return new Triplet<Float64ScalarSignal>(Item1, Item2, Item3);
    }
}