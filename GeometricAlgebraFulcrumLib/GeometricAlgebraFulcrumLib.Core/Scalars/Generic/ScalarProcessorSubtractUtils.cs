using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

public static class ScalarProcessorSubtractUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Subtract(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfSubtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Subtract(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractOne<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1)
    {
        return scalarProcessor.Subtract(
            scalar1,
            scalarProcessor.OneValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractOne<T>(this IScalarProcessor<T> scalarProcessor, int scalar1)
    {
        return scalarProcessor.Subtract(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractOne<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1)
    {
        return scalarProcessor.Subtract(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractOne<T>(this IScalarProcessor<T> scalarProcessor, long scalar1)
    {
        return scalarProcessor.Subtract(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractOne<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1)
    {
        return scalarProcessor.Subtract(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractOne<T>(this IScalarProcessor<T> scalarProcessor, float scalar1)
    {
        return scalarProcessor.Subtract(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractOne<T>(this IScalarProcessor<T> scalarProcessor, double scalar1)
    {
        return scalarProcessor.Subtract(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractOne<T>(this IScalarProcessor<T> scalarProcessor, string scalar1)
    {
        return scalarProcessor.Subtract(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar1)
    {
        return scalarProcessor.Subtract(
            scalar1,
            scalarProcessor.OneValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractFromOne<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar2)
    {
        return scalarProcessor.Subtract(
            scalarProcessor.OneValue,
            scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractFromOne<T>(this IScalarProcessor<T> scalarProcessor, int scalar2)
    {
        return scalarProcessor.Subtract(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractFromOne<T>(this IScalarProcessor<T> scalarProcessor, uint scalar2)
    {
        return scalarProcessor.Subtract(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractFromOne<T>(this IScalarProcessor<T> scalarProcessor, long scalar2)
    {
        return scalarProcessor.Subtract(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractFromOne<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar2)
    {
        return scalarProcessor.Subtract(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractFromOne<T>(this IScalarProcessor<T> scalarProcessor, float scalar2)
    {
        return scalarProcessor.Subtract(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractFromOne<T>(this IScalarProcessor<T> scalarProcessor, double scalar2)
    {
        return scalarProcessor.Subtract(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractFromOne<T>(this IScalarProcessor<T> scalarProcessor, string scalar2)
    {
        return scalarProcessor.Subtract(
            scalarProcessor.OneValue,
            scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SubtractFromOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar2)
    {
        return scalarProcessor.Subtract(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
}