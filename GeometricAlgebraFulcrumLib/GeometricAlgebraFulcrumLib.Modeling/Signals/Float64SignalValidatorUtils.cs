﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Modeling.Signals;

public static class Float64SignalValidatorUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqualZero(this Float64SignalValidator validator, IScalar<Float64SampledTimeSignal> scalarSignal1)
    {
        return validator.ValidateEqualZero(scalarSignal1.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, Scalar<Float64SampledTimeSignal> scalarSignal1, Scalar<Float64SampledTimeSignal> scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, IScalar<Float64SampledTimeSignal> scalarSignal1, Scalar<Float64SampledTimeSignal> scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, Scalar<Float64SampledTimeSignal> scalarSignal1, IScalar<Float64SampledTimeSignal> scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, IScalar<Float64SampledTimeSignal> scalarSignal1, IScalar<Float64SampledTimeSignal> scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2.ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, Float64SampledTimeSignal scalarSignal1, IScalar<Float64SampledTimeSignal> scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1,
            scalarSignal2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, IScalar<Float64SampledTimeSignal> scalarSignal1, Float64SampledTimeSignal scalarSignal2)
    {
        return validator.ValidateEqual(
            scalarSignal1.ScalarValue,
            scalarSignal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, XGaVector<Float64SampledTimeSignal> scalarSignal1, XGaVector<Float64SampledTimeSignal> scalarSignal2)
    {
        return validator.ValidateZeroNorm(
            scalarSignal1 - scalarSignal2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateEqual(this Float64SignalValidator validator, XGaBivector<Float64SampledTimeSignal> scalarSignal1, XGaBivector<Float64SampledTimeSignal> scalarSignal2)
    {
        return validator.ValidateZeroNormSquared(
            scalarSignal1 - scalarSignal2
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateZeroNorm(this Float64SignalValidator validator, XGaVector<Float64SampledTimeSignal> scalarSignal1)
    {
        return validator.ValidateEqualZero(
            scalarSignal1.Norm().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateZeroNormSquared(this Float64SignalValidator validator, XGaVector<Float64SampledTimeSignal> scalarSignal1)
    {
        return validator.ValidateEqualZero(
            scalarSignal1.NormSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateZeroNormSquared(this Float64SignalValidator validator, XGaBivector<Float64SampledTimeSignal> scalarSignal1)
    {
        return validator.ValidateEqualZero(
            scalarSignal1.NormSquared().ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateUnitNormSquared(this Float64SignalValidator validator, XGaVector<Float64SampledTimeSignal> scalarSignal1)
    {
        return validator.ValidateEqual(
            scalarSignal1.NormSquared().ScalarValue,
            scalarSignal1.ScalarProcessor.ScalarFromNumber(1)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateOrthogonal(this Float64SignalValidator validator, XGaVector<Float64SampledTimeSignal> vectorSignal1, XGaVector<Float64SampledTimeSignal> vectorSignal2)
    {
        return validator.ValidateEqualZero(vectorSignal1.Sp(vectorSignal2).ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ValidateOrthogonal(this Float64SignalValidator validator, IReadOnlyList<XGaVector<Float64SampledTimeSignal>> vectorSignalList)
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