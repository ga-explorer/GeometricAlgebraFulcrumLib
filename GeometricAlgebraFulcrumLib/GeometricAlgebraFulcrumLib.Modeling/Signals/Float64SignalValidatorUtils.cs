using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals;

public static class Float64SignalValidatorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqualZero(this Float64SignalValidator validator, IScalar<Float64Signal> scalarSignal1)
    {
        return validator.ValidateEqualZero(scalarSignal1.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, Scalar<Float64Signal> scalarSignal1, Scalar<Float64Signal> scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, IScalar<Float64Signal> scalarSignal1, Scalar<Float64Signal> scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, Scalar<Float64Signal> scalarSignal1, IScalar<Float64Signal> scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, IScalar<Float64Signal> scalarSignal1, IScalar<Float64Signal> scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2.ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, Float64Signal scalarSignal1, IScalar<Float64Signal> scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1,
            scalarSignal2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, IScalar<Float64Signal> scalarSignal1, Float64Signal scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, RGaVector<Float64Signal> scalarSignal1, RGaVector<Float64Signal> scalarSignal2)
    {
        return validator.ValidateZeroNorm(
            scalarSignal1 - scalarSignal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, XGaVector<Float64Signal> scalarSignal1, XGaVector<Float64Signal> scalarSignal2)
    {
        return validator.ValidateZeroNorm(
            scalarSignal1 - scalarSignal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, XGaBivector<Float64Signal> scalarSignal1, XGaBivector<Float64Signal> scalarSignal2)
    {
        return validator.ValidateZeroNormSquared(
            scalarSignal1 - scalarSignal2
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateZeroNorm(this Float64SignalValidator validator, RGaVector<Float64Signal> scalarSignal1)
    {
        return validator.ValidateEqualZero(
            scalarSignal1.Norm().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateZeroNorm(this Float64SignalValidator validator, XGaVector<Float64Signal> scalarSignal1)
    {
        return validator.ValidateEqualZero(
            scalarSignal1.Norm().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateZeroNormSquared(this Float64SignalValidator validator, XGaVector<Float64Signal> scalarSignal1)
    {
        return validator.ValidateEqualZero(
            scalarSignal1.NormSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateZeroNormSquared(this Float64SignalValidator validator, XGaBivector<Float64Signal> scalarSignal1)
    {
        return validator.ValidateEqualZero(
            scalarSignal1.NormSquared().ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateUnitNormSquared(this Float64SignalValidator validator, RGaVector<Float64Signal> scalarSignal1)
    {
        return validator.ValidateEqual(
            scalarSignal1.NormSquared().ScalarValue,
            scalarSignal1.ScalarProcessor.ScalarFromNumber(1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateUnitNormSquared(this Float64SignalValidator validator, XGaVector<Float64Signal> scalarSignal1)
    {
        return validator.ValidateEqual(
            scalarSignal1.NormSquared().ScalarValue,
            scalarSignal1.ScalarProcessor.ScalarFromNumber(1)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateOrthogonal(this Float64SignalValidator validator, RGaVector<Float64Signal> vectorSignal1, RGaVector<Float64Signal> vectorSignal2)
    {
        return validator.ValidateEqualZero(vectorSignal1.Sp(vectorSignal2).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateOrthogonal(this Float64SignalValidator validator, XGaVector<Float64Signal> vectorSignal1, XGaVector<Float64Signal> vectorSignal2)
    {
        return validator.ValidateEqualZero(vectorSignal1.Sp(vectorSignal2).ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateOrthogonal(this Float64SignalValidator validator, IReadOnlyList<RGaVector<Float64Signal>> vectorSignalList)
    {
        var validatedFlag = true;
        for (var i = 0; i < vectorSignalList.Count; i++)
        {
            var vectorSignal1 = vectorSignalList[i];

            for (var j = 0; j < vectorSignalList.Count; j++)
            {
                if (i == j) continue;

                var vectorSignal2 = vectorSignalList[j];

                validatedFlag &= validator.ValidateEqualZero(vectorSignal1.Sp(vectorSignal2).ScalarValue);
            }
        }

        return validatedFlag;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateOrthogonal(this Float64SignalValidator validator, IReadOnlyList<XGaVector<Float64Signal>> vectorSignalList)
    {
        var validatedFlag = true;
        for (var i = 0; i < vectorSignalList.Count; i++)
        {
            var vectorSignal1 = vectorSignalList[i];

            for (var j = 0; j < vectorSignalList.Count; j++)
            {
                if (i == j) continue;

                var vectorSignal2 = vectorSignalList[j];

                validatedFlag &= validator.ValidateEqualZero(vectorSignal1.Sp(vectorSignal2).ScalarValue);
            }
        }

        return validatedFlag;
    }

}