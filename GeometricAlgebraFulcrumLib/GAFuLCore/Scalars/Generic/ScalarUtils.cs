using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public static class ScalarUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNumeric<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNumeric;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSymbolic<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsSymbolic;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsValid(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNumber<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNumber(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFiniteNumber<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsFiniteNumber(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsZero(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOne<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsOne(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMinusOne<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsMinusOne(scalar.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZero<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNotZero(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotOne<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNotOne(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotMinusOne<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNotMinusOne(scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositive<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsPositive(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegative<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNegative(scalar.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotPositive<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNotPositive(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNegative<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNotNegative(scalar.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroOrPositive<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsZeroOrPositive(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroOrNegative<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsZeroOrNegative(scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZeroOrPositive<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNotZeroOrPositive(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZeroOrNegative<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNotZeroOrNegative(scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNearZero(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOne<T>(this IScalar<T> scalar)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar.ScalarValue,
            scalarProcessor.OneValue
        ).IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearMinusOne<T>(this IScalar<T> scalar)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar.ScalarValue,
            scalarProcessor.MinusOneValue
        ).IsNearZero();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNearZero<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNotNearZero(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNearOne<T>(this IScalar<T> scalar)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar.ScalarValue,
            scalarProcessor.OneValue
        ).IsNotNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNearMinusOne<T>(this IScalar<T> scalar)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar.ScalarValue,
            scalarProcessor.MinusOneValue
        ).IsNotNearZero();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZeroOrPositive<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNearZeroOrPositive(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZeroOrNegative<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNearZeroOrNegative(scalar.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNearZeroOrPositive<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNotNearZeroOrPositive(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNearZeroOrNegative<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.IsNotNearZeroOrNegative(scalar.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this IScalar<T> scalar1, int scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this IScalar<T> scalar1, uint scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this IScalar<T> scalar1, long scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this IScalar<T> scalar1, ulong scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this IScalar<T> scalar1, float scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this IScalar<T> scalar1, double scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this IScalar<T> scalar1, string scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsEqualTo(scalarProcessor.ScalarFromText(scalar2).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this IScalar<T> scalar, T scalar2)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar.ScalarValue,
            scalar2
        ).IsZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this IScalar<T> scalar1, Scalar<T> scalar2)
    {
        return scalar1.IsEqualTo(scalar2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this IScalar<T> scalar1, IScalar<T> scalar2)
    {
        return scalar1.IsEqualTo(scalar2.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this int scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this uint scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this long scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this ulong scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this float scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this double scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsEqualTo(scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this string scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromText(scalar1).IsEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsEqualTo<T>(this T scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar1,
            scalar2.ScalarValue
        ).IsZero();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this IScalar<T> scalar1, int scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsNearEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this IScalar<T> scalar1, uint scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsNearEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this IScalar<T> scalar1, long scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsNearEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this IScalar<T> scalar1, ulong scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsNearEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this IScalar<T> scalar1, float scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsNearEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this IScalar<T> scalar1, double scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsNearEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this IScalar<T> scalar1, string scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsNearEqualTo(scalarProcessor.ScalarFromText(scalar2).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this IScalar<T> scalar, T scalar2)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar.ScalarValue,
            scalar2
        ).IsNearZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this IScalar<T> scalar1, Scalar<T> scalar2)
    {
        return scalar1.IsNearEqualTo(scalar2.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this IScalar<T> scalar1, IScalar<T> scalar2)
    {
        return scalar1.IsNearEqualTo(scalar2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this int scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsNearEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this uint scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsNearEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this long scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsNearEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this ulong scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsNearEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this float scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsNearEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this double scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsNearEqualTo(scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this string scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromText(scalar1).IsNearEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearEqualTo<T>(this T scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar1,
            scalar2.ScalarValue
        ).IsNearZero();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this IScalar<T> scalar1, int scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this IScalar<T> scalar1, uint scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this IScalar<T> scalar1, long scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this IScalar<T> scalar1, ulong scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this IScalar<T> scalar1, float scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this IScalar<T> scalar1, double scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this IScalar<T> scalar1, string scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThan(scalarProcessor.ScalarFromText(scalar2).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this IScalar<T> scalar, T scalar2)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar.ScalarValue,
            scalar2
        ).IsNegative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this IScalar<T> scalar1, IScalar<T> scalar2)
    {
        return scalar1.IsLessThan(scalar2.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this int scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this uint scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this long scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this ulong scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this float scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this double scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThan(scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this string scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromText(scalar1).IsLessThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThan<T>(this T scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar1,
            scalar2.ScalarValue
        ).IsNegative();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this IScalar<T> scalar1, int scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this IScalar<T> scalar1, uint scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this IScalar<T> scalar1, long scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this IScalar<T> scalar1, ulong scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this IScalar<T> scalar1, float scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this IScalar<T> scalar1, double scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this IScalar<T> scalar1, string scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsLessThanOrEqualTo(scalarProcessor.ScalarFromText(scalar2).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this IScalar<T> scalar, T scalar2)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar.ScalarValue,
            scalar2
        ).IsZeroOrNegative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this IScalar<T> scalar1, IScalar<T> scalar2)
    {
        return scalar1.IsLessThanOrEqualTo(scalar2.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this int scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this uint scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this long scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this ulong scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this float scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this double scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsLessThanOrEqualTo(scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this string scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromText(scalar1).IsLessThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsLessThanOrEqualTo<T>(this T scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar1,
            scalar2.ScalarValue
        ).IsZeroOrNegative();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this IScalar<T> scalar1, int scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this IScalar<T> scalar1, uint scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this IScalar<T> scalar1, long scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this IScalar<T> scalar1, ulong scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this IScalar<T> scalar1, float scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this IScalar<T> scalar1, double scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThan(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this IScalar<T> scalar1, string scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThan(scalarProcessor.ScalarFromText(scalar2).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this IScalar<T> scalar, T scalar2)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar.ScalarValue,
            scalar2
        ).IsPositive();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this IScalar<T> scalar1, IScalar<T> scalar2)
    {
        return scalar1.IsMoreThan(scalar2.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this int scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this uint scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this long scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this ulong scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this float scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this double scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThan(scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this string scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromText(scalar1).IsMoreThan(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThan<T>(this T scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar1,
            scalar2.ScalarValue
        ).IsPositive();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this IScalar<T> scalar1, int scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this IScalar<T> scalar1, uint scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this IScalar<T> scalar1, long scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this IScalar<T> scalar1, ulong scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this IScalar<T> scalar1, float scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this IScalar<T> scalar1, double scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThanOrEqualTo(scalarProcessor.ScalarFromNumber(scalar2).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this IScalar<T> scalar1, string scalar2)
    {
        var scalarProcessor = scalar1.ScalarProcessor;

        return scalar1.IsMoreThanOrEqualTo(scalarProcessor.ScalarFromText(scalar2).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this IScalar<T> scalar, T scalar2)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar.ScalarValue,
            scalar2
        ).IsZeroOrPositive();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this IScalar<T> scalar1, IScalar<T> scalar2)
    {
        return scalar1.IsMoreThanOrEqualTo(scalar2.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this int scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this uint scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this long scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this ulong scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this float scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this double scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromNumber(scalar1).IsMoreThanOrEqualTo(scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this string scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.ScalarFromText(scalar1).IsMoreThanOrEqualTo(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMoreThanOrEqualTo<T>(this T scalar1, IScalar<T> scalar2)
    {
        var scalarProcessor = scalar2.ScalarProcessor;

        return scalarProcessor.Subtract(
            scalar1,
            scalar2.ScalarValue
        ).IsZeroOrPositive();
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Float64Scalar3D GetScalar3D(this Random random)
    //{
    //    return Float64Scalar3D.Create(
    //        random.NextDouble()
    //    );
    //}

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> MapScalar<T>(this IScalar<T> scalar, Func<T, T> scalarMapping)
    {
        return Scalar<T>.Create(
            scalar.ScalarProcessor,
            scalarMapping(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T2> MapScalar<T, T2>(this IScalar<T> scalar, Func<T, T2> scalarMapping, IScalarProcessor<T2> scalarProcessor2)
    {
        return Scalar<T2>.Create(
            scalarProcessor2,
            scalarMapping(scalar.ScalarValue)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Positive<T>(this IScalar<T> scalar)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Positive(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Negative<T>(this IScalar<T> scalar)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Negative(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Inverse<T>(this IScalar<T> scalar)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.Inverse(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Abs<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Abs(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Square<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Square(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeSquare<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Square(scalar.ScalarValue).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cube<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Cube(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sign<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Sign(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> UnitStep<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.UnitStep(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DegreesToRadians<T>(this IScalar<T> scalar)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.DegreesToRadians(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RadiansToDegrees<T>(this IScalar<T> scalar)
    {
        var scalarProcessor = scalar.ScalarProcessor;

        return scalarProcessor.RadiansToDegrees(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sqrt<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Sqrt(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfNegative<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Sqrt(scalar.Negative().ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfAbs<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.SqrtOfAbs(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Exp<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Exp(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> LogE<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.LogE(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log2<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Log2(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log10<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Log10(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cos<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Cos(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sin<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Sin(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Tan<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Tan(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sec<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.One.Divide(
            scalar.ScalarProcessor.Cos(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Csc<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.One.Divide(
            scalar.ScalarProcessor.Sin(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cot<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.One.Divide(
            scalar.ScalarProcessor.Tan(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cosh<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Cosh(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sinh<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Sinh(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Tanh<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Tanh(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sech<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.One.Divide(
            scalar.ScalarProcessor.Cosh(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Csch<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.One.Divide(
            scalar.ScalarProcessor.Sinh(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Coth<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.One.Divide(
            scalar.ScalarProcessor.Tanh(scalar.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcCos<T>(this IScalar<T> scalar)
    {
        return LinPolarAngle<T>.CreateFromCos(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcSin<T>(this IScalar<T> scalar)
    {
        return LinPolarAngle<T>.CreateFromSin(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan<T>(this IScalar<T> scalar)
    {
        return LinPolarAngle<T>.CreateFromTan(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcSec<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Divide(
            scalar.ScalarProcessor.OneValue,
            scalar.ScalarValue
        ).ArcCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcCsc<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Divide(
            scalar.ScalarProcessor.OneValue,
            scalar.ScalarValue
        ).ArcSin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcCot<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.Divide(
            scalar.ScalarProcessor.OneValue,
            scalar.ScalarValue
        ).ArcTan();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalar<T> scalarX, int scalarY)
    {
        return scalarX.ArcTan2(
            scalarX.ScalarProcessor.ScalarFromNumber(scalarY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalar<T> scalarX, uint scalarY)
    {
        return scalarX.ArcTan2(
            scalarX.ScalarProcessor.ScalarFromNumber(scalarY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalar<T> scalarX, long scalarY)
    {
        return scalarX.ArcTan2(
            scalarX.ScalarProcessor.ScalarFromNumber(scalarY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalar<T> scalarX, ulong scalarY)
    {
        return scalarX.ArcTan2(
            scalarX.ScalarProcessor.ScalarFromNumber(scalarY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalar<T> scalarX, float scalarY)
    {
        return scalarX.ArcTan2(
            scalarX.ScalarProcessor.ScalarFromNumber(scalarY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalar<T> scalarX, double scalarY)
    {
        return scalarX.ArcTan2(
            scalarX.ScalarProcessor.ScalarFromNumber(scalarY)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalar<T> scalarX, string scalarY)
    {
        return scalarX.ArcTan2(
            scalarX.ScalarProcessor.ScalarFromText(scalarY)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalar<T> scalarX, T scalarY)
    {
        return scalarX.ArcTan2(
            scalarX.ScalarProcessor.ScalarFromValue(scalarY)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalar<T> scalarX, Scalar<T> scalarY)
    {
        return LinPolarAngle<T>.CreateFromVector(scalarX, scalarY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this IScalar<T> scalarX, IScalar<T> scalarY)
    {
        return LinPolarAngle<T>.CreateFromVector(scalarX, scalarY);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this int scalarX, IScalar<T> scalarY)
    {
        return scalarY.ScalarProcessor.ScalarFromNumber(scalarX).ArcTan2(scalarY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this uint scalarX, IScalar<T> scalarY)
    {
        return scalarY.ScalarProcessor.ScalarFromNumber(scalarX).ArcTan2(scalarY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this long scalarX, IScalar<T> scalarY)
    {
        return scalarY.ScalarProcessor.ScalarFromNumber(scalarX).ArcTan2(scalarY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this ulong scalarX, IScalar<T> scalarY)
    {
        return scalarY.ScalarProcessor.ScalarFromNumber(scalarX).ArcTan2(scalarY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this float scalarX, IScalar<T> scalarY)
    {
        return scalarY.ScalarProcessor.ScalarFromNumber(scalarX).ArcTan2(scalarY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this double scalarX, IScalar<T> scalarY)
    {
        return scalarY.ScalarProcessor.ScalarFromNumber(scalarX).ArcTan2(scalarY);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this string scalarX, IScalar<T> scalarY)
    {
        return scalarY.ScalarProcessor.ScalarFromText(scalarX).ArcTan2(scalarY);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinPolarAngle<T> ArcTan2<T>(this T scalarX, IScalar<T> scalarY)
    {
        return scalarY.ScalarProcessor.ScalarFromValue(scalarX).ArcTan2(scalarY);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, IntegerSign exponentScalar)
    {
        return scalar.Power(
            scalar.ScalarProcessor.ScalarFromNumber(exponentScalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, int exponentScalar)
    {
        return scalar.Power(
            scalar.ScalarProcessor.ScalarFromNumber(exponentScalar).ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, uint exponentScalar)
    {
        return scalar.Power(
            scalar.ScalarProcessor.ScalarFromNumber(exponentScalar).ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, long exponentScalar)
    {
        return scalar.Power(
            scalar.ScalarProcessor.ScalarFromNumber(exponentScalar).ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, ulong exponentScalar)
    {
        return scalar.Power(
            scalar.ScalarProcessor.ScalarFromNumber(exponentScalar).ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, float exponentScalar)
    {
        return scalar.Power(
            scalar.ScalarProcessor.ScalarFromNumber(exponentScalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, double exponentScalar)
    {
        return scalar.Power(
            scalar.ScalarProcessor.ScalarFromNumber(exponentScalar).ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, string exponentScalar)
    {
        return scalar.Power(
            scalar.ScalarProcessor.ScalarFromText(exponentScalar).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, T exponentScalar)
    {
        return scalar.ScalarProcessor.Power(
            scalar.ScalarValue, 
            exponentScalar
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, Scalar<T> exponentScalar)
    {
        return scalar.Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalar<T> scalar, IScalar<T> exponentScalar)
    {
        return scalar.Power(exponentScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IntegerSign scalar, IScalar<T> exponentScalar)
    {
        return exponentScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this int scalar, IScalar<T> exponentScalar)
    {
        return exponentScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this uint scalar, IScalar<T> exponentScalar)
    {
        return exponentScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this long scalar, IScalar<T> exponentScalar)
    {
        return exponentScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this ulong scalar, IScalar<T> exponentScalar)
    {
        return exponentScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this float scalar, IScalar<T> exponentScalar)
    {
        return exponentScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this double scalar, IScalar<T> exponentScalar)
    {
        return exponentScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Power(exponentScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this string scalar, IScalar<T> exponentScalar)
    {
        return exponentScalar
            .ScalarProcessor
            .ScalarFromText(scalar)
            .Power(exponentScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this T scalar, IScalar<T> exponentScalar)
    {
        return exponentScalar
            .ScalarProcessor
            .ScalarFromValue(scalar)
            .Power(exponentScalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this IScalar<T> scalar, int baseScalar)
    {
        return scalar.Log(
            scalar.ScalarProcessor.ScalarFromNumber(baseScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this IScalar<T> scalar, uint baseScalar)
    {
        return scalar.Log(
            scalar.ScalarProcessor.ScalarFromNumber(baseScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this IScalar<T> scalar, long baseScalar)
    {
        return scalar.Log(
            scalar.ScalarProcessor.ScalarFromNumber(baseScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this IScalar<T> scalar, ulong baseScalar)
    {
        return scalar.Log(
            scalar.ScalarProcessor.ScalarFromNumber(baseScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this IScalar<T> scalar, float baseScalar)
    {
        return scalar.Log(
            scalar.ScalarProcessor.ScalarFromNumber(baseScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this IScalar<T> scalar, double baseScalar)
    {
        return scalar.Log(
            scalar.ScalarProcessor.ScalarFromNumber(baseScalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this IScalar<T> scalar, string baseScalar)
    {
        return scalar.Log(
            scalar.ScalarProcessor.ScalarFromText(baseScalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this IScalar<T> scalar, T baseScalar)
    {
        return scalar.ScalarProcessor.Log(
            baseScalar, 
            scalar.ScalarValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this IScalar<T> scalar, Scalar<T> baseScalar)
    {
        return scalar.Log(baseScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this IScalar<T> scalar, IScalar<T> baseScalar)
    {
        return scalar.Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this int scalar, IScalar<T> baseScalar)
    {
        return baseScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this uint scalar, IScalar<T> baseScalar)
    {
        return baseScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this long scalar, IScalar<T> baseScalar)
    {
        return baseScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this ulong scalar, IScalar<T> baseScalar)
    {
        return baseScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this float scalar, IScalar<T> baseScalar)
    {
        return baseScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this double scalar, IScalar<T> baseScalar)
    {
        return baseScalar
            .ScalarProcessor
            .ScalarFromNumber(scalar)
            .Log(baseScalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this string scalar, IScalar<T> baseScalar)
    {
        return baseScalar
            .ScalarProcessor
            .ScalarFromText(scalar)
            .Log(baseScalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Log<T>(this T scalar, IScalar<T> baseScalar)
    {
        return baseScalar
            .ScalarProcessor
            .ScalarFromValue(scalar)
            .Log(baseScalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, IntegerSign scalar)
    {
        return scalar1.Add(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, int scalar)
    {
        return scalar1.Add(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, uint scalar)
    {
        return scalar1.Add(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, long scalar)
    {
        return scalar1.Add(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, ulong scalar)
    {
        return scalar1.Add(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, float scalar)
    {
        return scalar1.Add(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, double scalar)
    {
        return scalar1.Add(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, string scalar)
    {
        return scalar1.Add(
            scalar1.ScalarProcessor.ScalarFromText(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, T scalar)
    {
        return scalar1.ScalarProcessor.Add(scalar1.ScalarValue, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, Scalar<T> scalar)
    {
        return scalar1.Add(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalar<T> scalar1, IScalar<T> scalar)
    {
        return scalar1.Add(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IntegerSign scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Add(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this int scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Add(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this uint scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Add(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this long scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Add(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this ulong scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Add(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this float scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Add(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this double scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Add(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this string scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromText(scalar1).Add(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this T scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromValue(scalar1).Add(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, IntegerSign scalar)
    {
        return scalar1.Subtract(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, int scalar)
    {
        return scalar1.Subtract(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, uint scalar)
    {
        return scalar1.Subtract(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, long scalar)
    {
        return scalar1.Subtract(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, ulong scalar)
    {
        return scalar1.Subtract(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, float scalar)
    {
        return scalar1.Subtract(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, double scalar)
    {
        return scalar1.Subtract(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, string scalar)
    {
        return scalar1.Subtract(
            scalar1.ScalarProcessor.ScalarFromText(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, T scalar)
    {
        return scalar1.ScalarProcessor.Subtract(scalar1.ScalarValue, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, Scalar<T> scalar)
    {
        return scalar1.Subtract(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalar<T> scalar1, IScalar<T> scalar)
    {
        return scalar1.Subtract(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IntegerSign scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Subtract(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this int scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Subtract(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this uint scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Subtract(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this long scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Subtract(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this ulong scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Subtract(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this float scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Subtract(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this double scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Subtract(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this string scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromText(scalar1).Subtract(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this T scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromValue(scalar1).Subtract(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, IntegerSign scalar)
    {
        return scalar1.Times(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, int scalar)
    {
        return scalar1.Times(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, uint scalar)
    {
        return scalar1.Times(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, long scalar)
    {
        return scalar1.Times(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, ulong scalar)
    {
        return scalar1.Times(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, float scalar)
    {
        return scalar1.Times(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, double scalar)
    {
        return scalar1.Times(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, string scalar)
    {
        return scalar1.Times(
            scalar1.ScalarProcessor.ScalarFromText(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, T scalar)
    {
        return scalar1.ScalarProcessor.Times(scalar1.ScalarValue, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, Scalar<T> scalar)
    {
        return scalar1.Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalar<T> scalar1, IScalar<T> scalar)
    {
        return scalar1.Times(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IntegerSign scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this int scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this uint scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this long scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this ulong scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this float scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this double scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this string scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromText(scalar1).Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this T scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromValue(scalar1).Times(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, IntegerSign scalar)
    {
        return scalar1.Divide(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, int scalar)
    {
        return scalar1.Divide(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, uint scalar)
    {
        return scalar1.Divide(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, long scalar)
    {
        return scalar1.Divide(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, ulong scalar)
    {
        return scalar1.Divide(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, float scalar)
    {
        return scalar1.Divide(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, double scalar)
    {
        return scalar1.Divide(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, string scalar)
    {
        return scalar1.Divide(
            scalar1.ScalarProcessor.ScalarFromText(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, T scalar)
    {
        return scalar1.ScalarProcessor.Divide(scalar1.ScalarValue, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, Scalar<T> scalar)
    {
        return scalar1.Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalar<T> scalar1, IScalar<T> scalar)
    {
        return scalar1.Divide(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IntegerSign scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this int scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this uint scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this long scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this ulong scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this float scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this double scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this string scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromText(scalar1).Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this T scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromValue(scalar1).Divide(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, IntegerSign scalarFactor)
    {
        return scalar.NegativeTimes(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, int scalarFactor)
    {
        return scalar.NegativeTimes(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, uint scalarFactor)
    {
        return scalar.NegativeTimes(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, long scalarFactor)
    {
        return scalar.NegativeTimes(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, ulong scalarFactor)
    {
        return scalar.NegativeTimes(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, float scalarFactor)
    {
        return scalar.NegativeTimes(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, double scalarFactor)
    {
        return scalar.NegativeTimes(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, string scalarFactor)
    {
        return scalar.NegativeTimes(
            scalar.ScalarProcessor.ScalarFromText(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, T scalarFactor)
    {
        return scalar.ScalarProcessor.Times(
            scalar.ScalarValue, 
            scalarFactor
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, Scalar<T> scalarFactor)
    {
        return scalar.NegativeTimes(scalarFactor.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalar<T> scalar, IScalar<T> scalarFactor)
    {
        return scalar.NegativeTimes(scalarFactor.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IntegerSign scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this int scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this uint scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this long scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this ulong scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this float scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this double scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this string scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromText(scalar1).NegativeTimes(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this T scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromValue(scalar1).NegativeTimes(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, IntegerSign scalarFactor)
    {
        return scalar.NegativeDivide(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, int scalarFactor)
    {
        return scalar.NegativeDivide(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, uint scalarFactor)
    {
        return scalar.NegativeDivide(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, long scalarFactor)
    {
        return scalar.NegativeDivide(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, ulong scalarFactor)
    {
        return scalar.NegativeDivide(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, float scalarFactor)
    {
        return scalar.NegativeDivide(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, double scalarFactor)
    {
        return scalar.NegativeDivide(
            scalar.ScalarProcessor.ScalarFromNumber(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, string scalarFactor)
    {
        return scalar.NegativeDivide(
            scalar.ScalarProcessor.ScalarFromText(scalarFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, T scalarFactor)
    {
        return scalar.ScalarProcessor.Divide(
            scalar.ScalarValue, 
            scalarFactor
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, Scalar<T> scalarFactor)
    {
        return scalar.NegativeDivide(scalarFactor.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalar<T> scalar, IScalar<T> scalarFactor)
    {
        return scalar.NegativeDivide(scalarFactor.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IntegerSign scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this int scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this uint scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this long scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this ulong scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this float scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this double scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this string scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromText(scalar1).NegativeDivide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this T scalar1, IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ScalarFromValue(scalar1).NegativeDivide(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, IntegerSign scalar2)
    {
        return scalar1.Min(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, int scalar2)
    {
        return scalar1.Min(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, uint scalar2)
    {
        return scalar1.Min(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, long scalar2)
    {
        return scalar1.Min(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, ulong scalar2)
    {
        return scalar1.Min(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, float scalar2)
    {
        return scalar1.Min(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, double scalar2)
    {
        return scalar1.Min(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, string scalar2)
    {
        return scalar1.Min(
            scalar1.ScalarProcessor.ScalarFromText(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, T scalar2)
    {
        return scalar1.Min(
            scalar1.ScalarProcessor.ScalarFromValue(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, Scalar<T> scalar2)
    {
        return scalar1.Subtract(scalar2).IsNegative() 
            ? scalar1.ToScalar() 
            : scalar2.ToScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IScalar<T> scalar1, IScalar<T> scalar2)
    {
        return scalar1.Subtract(scalar2).IsNegative() 
            ? scalar1.ToScalar() 
            : scalar2.ToScalar();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this IntegerSign scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Min(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this int scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Min(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this uint scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Min(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this long scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Min(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this ulong scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Min(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this float scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Min(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this double scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Min(scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this string scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromText(scalar1).Min(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Min<T>(this T scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromValue(scalar1).Min(scalar2);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, IntegerSign scalar2)
    {
        return scalar1.Max(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, int scalar2)
    {
        return scalar1.Max(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, uint scalar2)
    {
        return scalar1.Max(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, long scalar2)
    {
        return scalar1.Max(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, ulong scalar2)
    {
        return scalar1.Max(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, float scalar2)
    {
        return scalar1.Max(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, double scalar2)
    {
        return scalar1.Max(
            scalar1.ScalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, string scalar2)
    {
        return scalar1.Max(
            scalar1.ScalarProcessor.ScalarFromText(scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, T scalar2)
    {
        return scalar1.Max(
            scalar1.ScalarProcessor.ScalarFromValue(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, Scalar<T> scalar2)
    {
        return scalar1.Subtract(scalar2).IsPositive()
            ? scalar1.ToScalar() 
            : scalar2.ToScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IScalar<T> scalar1, IScalar<T> scalar2)
    {
        return scalar1.Subtract(scalar2).IsPositive()
            ? scalar1.ToScalar() 
            : scalar2.ToScalar();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this IntegerSign scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Max(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this int scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Max(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this uint scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Max(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this long scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Max(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this ulong scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Max(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this float scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Max(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this double scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromNumber(scalar1).Max(scalar2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this string scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromText(scalar1).Max(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Max<T>(this T scalar1, IScalar<T> scalar2)
    {
        return scalar2.ScalarProcessor.ScalarFromValue(scalar1).Max(scalar2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ToFloat64<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ToFloat64(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ToFloat64Scalar<T>(this IScalar<T> scalar)
    {
        return Float64Scalar.Create(
            scalar.ScalarProcessor.ToFloat64(scalar.ScalarValue)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T1> ToScalar<T, T1>(this IScalar<T> scalar, IScalarProcessor<T1> scalarProcessor)
    {
        return scalarProcessor.ScalarFromNumber(
            scalar.ScalarProcessor.ToFloat64(scalar.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToText<T>(this IScalar<T> scalar)
    {
        return scalar.ScalarProcessor.ToText(scalar.ScalarValue);
    }
}