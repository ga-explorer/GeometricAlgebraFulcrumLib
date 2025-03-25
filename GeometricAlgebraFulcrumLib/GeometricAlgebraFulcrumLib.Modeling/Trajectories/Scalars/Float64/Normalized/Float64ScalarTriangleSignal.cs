using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Normalized;

public sealed class Float64ScalarTriangleSignal :
    Float64ScalarNormalizedSignal
{
    internal static Float64ScalarTriangleSignal FiniteSymmetric { get; }
        = new Float64ScalarTriangleSignal(false, 0);

    internal static Float64ScalarTriangleSignal PeriodicSymmetric { get; }
        = new Float64ScalarTriangleSignal(true, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarTriangleSignal Finite(double vertexRelativeTime)
    {
        return new Float64ScalarTriangleSignal(false, 2 * vertexRelativeTime - 1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarTriangleSignal Periodic(double vertexRelativeTime)
    {
        return new Float64ScalarTriangleSignal(true, 2 * vertexRelativeTime - 1);
    }


    public double VertexTime { get; }

    public double VertexRelativeTime 
        => (VertexTime + 1) / 2;

    public bool IsSymmetric 
        => VertexTime.IsZero();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarTriangleSignal(bool isPeriodic, double vertexTime)
        : base(isPeriodic)
    {
        if (vertexTime < TimeRange.MinValue || vertexTime > TimeRange.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(vertexTime));

        VertexTime = vertexTime;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        if (IsFinite)
            return this;

        if (IsSymmetric) 
            return FiniteSymmetric;
        
        return new Float64ScalarTriangleSignal(false, VertexTime);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        if (IsPeriodic) 
            return this;
        
        if (IsSymmetric) 
            return PeriodicSymmetric;

        return new Float64ScalarTriangleSignal(true, VertexTime);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.ClampTime(t);

        return t <= VertexTime
            ? 2 * (t + 1) / (VertexTime + 1) - 1
            : 2 * (t - 1) / (VertexTime - 1) - 1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative1Value(double t)
    {
        t = this.ClampTime(t);

        return t <= VertexTime
            ? 2 / (VertexTime + 1)
            : 2 / (VertexTime - 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetDerivative2Value(double t)
    {
        return 0;
    }
}