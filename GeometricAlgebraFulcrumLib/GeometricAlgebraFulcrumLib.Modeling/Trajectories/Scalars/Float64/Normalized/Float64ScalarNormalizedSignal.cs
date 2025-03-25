using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Normalized;

/// <summary>
/// A normalized time signal has time in the range [-1, 1] and values in the range [-1, 1]
/// </summary>
public abstract class Float64ScalarNormalizedSignal :
    Float64ScalarSignal
{
    /// <summary>
    /// A normalized time signal has time in the range [-1, 1] and values in the range [-1, 1]
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Float64ScalarNormalizedSignal(bool isPeriodic) 
        : base(Float64ScalarRange.SymmetricOne, isPeriodic)
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarRange FindValueRange()
    {
        return Float64ScalarRange.SymmetricOne;
    }
}