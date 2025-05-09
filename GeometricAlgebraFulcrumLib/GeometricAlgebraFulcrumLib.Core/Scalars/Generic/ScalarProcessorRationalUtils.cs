using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

public static class ScalarProcessorRationalUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Rational<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }


    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> RationalSqrt<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sqrt();
    }

}