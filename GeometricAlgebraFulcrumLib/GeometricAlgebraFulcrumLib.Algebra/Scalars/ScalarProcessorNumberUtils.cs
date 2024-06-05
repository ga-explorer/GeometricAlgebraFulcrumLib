using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars;

public static class ScalarProcessorNumberUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNumber<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsFiniteNumber<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return double.IsFinite(number);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               number.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, T scalar, bool nearZeroFlag)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               nearZeroFlag 
            ? number.IsNearZero(scalarProcessor.ZeroEpsilon) 
            : number.IsZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Subtract(scalar, scalarProcessor.OneValue).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsMinusOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Subtract(scalar, scalarProcessor.MinusOneValue).IsZero();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZero<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               !number.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZero<T>(this IScalarProcessor<T> scalarProcessor, T scalar, bool nearZeroFlag)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               nearZeroFlag 
            ? !number.IsNearZero(scalarProcessor.ZeroEpsilon) 
            : !number.IsZero();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Subtract(scalar, scalarProcessor.OneValue).IsNotZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotMinusOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Subtract(scalar, scalarProcessor.MinusOneValue).IsNotZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               number.IsNearZero(scalarProcessor.ZeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Subtract(scalar, scalarProcessor.OneValue).IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearMinusOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Subtract(scalar, scalarProcessor.MinusOneValue).IsNearZero();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNearZero<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               !number.IsNearZero(scalarProcessor.ZeroEpsilon);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNearOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Subtract(scalar, scalarProcessor.OneValue).IsNotNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNearMinusOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Subtract(scalar, scalarProcessor.MinusOneValue).IsNotNearZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               number > 0;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               number < 0;
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               !(number > 0);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               !(number < 0);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               number >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               number <= 0;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               !(number >= 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               !(number <= 0);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               number >= -scalarProcessor.ZeroEpsilon;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               number <= scalarProcessor.ZeroEpsilon;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNearZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               !(number >= -scalarProcessor.ZeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNotNearZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        var number = scalarProcessor.ToFloat64(scalar);

        return !double.IsNaN(number) && 
               !(number <= scalarProcessor.ZeroEpsilon);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double CompareTo<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        var s1 = scalarProcessor.ToFloat64(scalar1);
        var s2 = scalarProcessor.ToFloat64(scalar2);

        if (s1.IsNaN() || s2.IsNaN()) return double.NaN;

        return s1 - s2;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HaveOppositeSign<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        return scalarProcessor.IsPositive(scalar1) && 
               scalarProcessor.IsNegative(scalar2) ||
               scalarProcessor.IsNegative(scalar1) && 
               scalarProcessor.IsPositive(scalar2);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllSameSign<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        return scalarProcessor.AllPositive(scalar1, scalar2) ||
               scalarProcessor.AllNegative(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllSameSign<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        return scalarProcessor.AllPositive(scalar1, scalar2, scalar3) ||
               scalarProcessor.AllNegative(scalar1, scalar2, scalar3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllSameSign<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
    {
        return scalarList.All(scalarProcessor.IsPositive) ||
               scalarList.All(scalarProcessor.IsNegative);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllZeroOrSameSign<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        return scalarProcessor.AllZeroOrPositive(scalar1, scalar2) ||
               scalarProcessor.AllZeroOrNegative(scalar1, scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllZeroOrSameSign<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        return scalarProcessor.AllZeroOrPositive(scalar1, scalar2, scalar3) ||
               scalarProcessor.AllZeroOrNegative(scalar1, scalar2, scalar3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllZeroOrSameSign<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
    {
        return scalarList.All(scalarProcessor.IsZeroOrPositive) ||
               scalarList.All(scalarProcessor.IsZeroOrNegative);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        return scalarProcessor.IsPositive(scalar1) &&
               scalarProcessor.IsPositive(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        return scalarProcessor.IsPositive(scalar1) &&
               scalarProcessor.IsPositive(scalar2) &&
               scalarProcessor.IsPositive(scalar3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllPositive<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
    {
        return scalarList.All(scalarProcessor.IsPositive);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        return scalarProcessor.IsNegative(scalar1) &&
               scalarProcessor.IsNegative(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        return scalarProcessor.IsNegative(scalar1) &&
               scalarProcessor.IsNegative(scalar2) &&
               scalarProcessor.IsNegative(scalar3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllNegative<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
    {
        return scalarList.All(scalarProcessor.IsNegative);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        return scalarProcessor.IsZeroOrPositive(scalar1) &&
               scalarProcessor.IsZeroOrPositive(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        return scalarProcessor.IsZeroOrPositive(scalar1) &&
               scalarProcessor.IsZeroOrPositive(scalar2) &&
               scalarProcessor.IsZeroOrPositive(scalar3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllZeroOrPositive<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
    {
        return scalarList.All(scalarProcessor.IsZeroOrPositive);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        return scalarProcessor.IsZeroOrNegative(scalar1) &&
               scalarProcessor.IsZeroOrNegative(scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        return scalarProcessor.IsZeroOrNegative(scalar1) &&
               scalarProcessor.IsZeroOrNegative(scalar2) &&
               scalarProcessor.IsZeroOrNegative(scalar3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AllZeroOrNegative<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
    {
        return scalarList.All(scalarProcessor.IsZeroOrNegative);
    }

}