using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Constants;

public sealed class DfConstantValueFloat64 :
    DfConstantValue
{
    public override bool IsZero 
        => Float64Value.IsZero();

    public override bool IsOne 
        => (Float64Value - 1d).IsZero();

    public override double Float64Value { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal DfConstantValueFloat64(double float64Value)
    {
        Float64Value = float64Value;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DfConstantValue Simplify()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return Float64Value.ToString("G");
    }
}