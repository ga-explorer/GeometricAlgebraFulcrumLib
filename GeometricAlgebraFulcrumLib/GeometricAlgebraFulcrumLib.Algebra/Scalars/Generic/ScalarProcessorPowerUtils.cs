using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public static class ScalarProcessorPowerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Power<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Power(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativePower<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Power(
            scalarProcessor.ScalarFromText(scalar2)
        ).Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, int scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, uint scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, long scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, float scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).Sqrt();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Sqrt<T>(this IScalarProcessor<T> scalarProcessor, string scalar)
    {
        return scalarProcessor.ScalarFromText(scalar).Sqrt();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfNegative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, int scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfNegative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, uint scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfNegative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, long scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfNegative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, float scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfNegative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfNegative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfNegative<T>(this IScalarProcessor<T> scalarProcessor, string scalar)
    {
        return scalarProcessor.ScalarFromText(scalar).SqrtOfNegative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, int scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfAbs();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, uint scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfAbs();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, long scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfAbs();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, float scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfAbs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
    {
        return scalarProcessor.ScalarFromNumber(scalar).SqrtOfAbs();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SqrtOfAbs<T>(this IScalarProcessor<T> scalarProcessor, string scalar)
    {
        return scalarProcessor.ScalarFromText(scalar).SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Square<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar)
    {
        return scalarProcessor.Times(scalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Square<T>(this IScalarProcessor<T> scalarProcessor, int scalar)
    {
        return scalarProcessor.Times(scalar, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Square<T>(this IScalarProcessor<T> scalarProcessor, uint scalar)
    {
        return scalarProcessor.Times(scalar, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Square<T>(this IScalarProcessor<T> scalarProcessor, long scalar)
    {
        return scalarProcessor.Times(scalar, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Square<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar)
    {
        return scalarProcessor.Times(scalar, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Square<T>(this IScalarProcessor<T> scalarProcessor, float scalar)
    {
        return scalarProcessor.Times(scalar, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Square<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
    {
        return scalarProcessor.Times(scalar, scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Square<T>(this IScalarProcessor<T> scalarProcessor, string scalar)
    {
        return scalarProcessor.Times(scalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Square<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Times(scalar, scalar);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeSquare<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeSquare<T>(this IScalarProcessor<T> scalarProcessor, int scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeSquare<T>(this IScalarProcessor<T> scalarProcessor, uint scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeSquare<T>(this IScalarProcessor<T> scalarProcessor, long scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeSquare<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeSquare<T>(this IScalarProcessor<T> scalarProcessor, float scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeSquare<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeSquare<T>(this IScalarProcessor<T> scalarProcessor, string scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeSquare<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cube<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cube<T>(this IScalarProcessor<T> scalarProcessor, int scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cube<T>(this IScalarProcessor<T> scalarProcessor, uint scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cube<T>(this IScalarProcessor<T> scalarProcessor, long scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cube<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cube<T>(this IScalarProcessor<T> scalarProcessor, float scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cube<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cube<T>(this IScalarProcessor<T> scalarProcessor, string scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Cube<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeCube<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeCube<T>(this IScalarProcessor<T> scalarProcessor, int scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeCube<T>(this IScalarProcessor<T> scalarProcessor, uint scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeCube<T>(this IScalarProcessor<T> scalarProcessor, long scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeCube<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeCube<T>(this IScalarProcessor<T> scalarProcessor, float scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeCube<T>(this IScalarProcessor<T> scalarProcessor, double scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeCube<T>(this IScalarProcessor<T> scalarProcessor, string scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeCube<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Times(scalar, scalar).Times(scalar).Negative();
    }

    
    
}