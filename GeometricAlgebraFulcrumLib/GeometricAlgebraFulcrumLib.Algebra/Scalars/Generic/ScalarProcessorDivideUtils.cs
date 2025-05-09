using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public static class ScalarProcessorDivideUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Divide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Divide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Divide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeDivide(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        return scalarProcessor.Divide(scalar1, scalar2).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeDivide<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        return scalarProcessor.Divide(scalar1, scalar2).Divide(scalar3).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IntegerSign sign)
    {
        if (sign.IsZero) 
            return scalarProcessor.Divide(scalar, scalarProcessor.ZeroValue);

        return sign.IsPositive 
            ? scalarProcessor.Positive(scalar) 
            : scalarProcessor.Negative(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Divide<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign sign, T scalar)
    {
        if (sign.IsZero) 
            return scalarProcessor.Divide(scalarProcessor.ZeroValue, scalar);

        return sign.IsPositive 
            ? scalarProcessor.Positive(scalar) 
            : scalarProcessor.Negative(scalar);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DivideTwo<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Divide(
            scalar,
            scalarProcessor.TwoValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> DivideMinusTwo<T>(this IScalarProcessor<T> scalarProcessor, T scalar)
    {
        return scalarProcessor.Divide(
            scalar,
            scalarProcessor.MinusTwoValue
        );
    }
}