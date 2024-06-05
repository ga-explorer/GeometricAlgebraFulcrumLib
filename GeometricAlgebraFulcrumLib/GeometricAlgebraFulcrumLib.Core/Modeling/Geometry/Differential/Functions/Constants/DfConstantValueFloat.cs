using System.Runtime.CompilerServices;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Differential.Functions.Constants;

public sealed class DfConstantValueFloat :
    DfConstantValue
{
    public override bool IsZero 
        => FloatValue.IsZero;

    public override bool IsOne 
        => (FloatValue - EFloat.One).IsZero;

    public EFloat FloatValue { get; }

    public override double Float64Value
        => FloatValue.ToDouble();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal DfConstantValueFloat(EFloat floatValue)
    {
        if (floatValue.IsNaN() || floatValue.IsQuietNaN() || floatValue.IsNaN() || floatValue.IsInfinity())
            throw new NotFiniteNumberException(nameof(floatValue));

        FloatValue = floatValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DfConstantValue Simplify()
    {
        if (FloatValue.IsInteger())
            return new DfConstantValueInteger(
                FloatValue.ToEIntegerIfExact()
            );
        
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return FloatValue.ToString();
    }
}