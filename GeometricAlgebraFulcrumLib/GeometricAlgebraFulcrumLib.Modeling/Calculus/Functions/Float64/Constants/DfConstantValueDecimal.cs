using System.Runtime.CompilerServices;
using PeterO.Numbers;

namespace GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64.Constants;

public sealed class DfConstantValueDecimal :
    DfConstantValue
{

    public override bool IsZero
        => DecimalValue.IsZero;

    public override bool IsOne
        => (DecimalValue - EDecimal.One).IsZero;

    public EDecimal DecimalValue { get; }

    public override double Float64Value
        => DecimalValue.ToDouble();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal DfConstantValueDecimal(EDecimal decimalValue)
    {
        if (decimalValue.IsNaN() || decimalValue.IsQuietNaN() || decimalValue.IsNaN() || decimalValue.IsInfinity())
            throw new NotFiniteNumberException(nameof(decimalValue));

        DecimalValue = decimalValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override DfConstantValue Simplify()
    {
        if (DecimalValue.IsInteger())
            return new DfConstantValueInteger(
                DecimalValue.ToEIntegerIfExact()
            );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return DecimalValue.ToString();
    }
}