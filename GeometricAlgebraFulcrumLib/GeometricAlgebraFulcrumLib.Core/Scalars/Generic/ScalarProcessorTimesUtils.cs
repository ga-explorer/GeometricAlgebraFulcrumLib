using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

public static class ScalarProcessorTimesUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Times(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Times(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Times(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Times(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Times(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        return scalarProcessor.Times(scalar1, scalar2).Times(scalar3);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).NegativeTimes(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2)
    {
        return scalarProcessor.Times(scalar1, scalar2).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        return scalarProcessor.Times(scalar1, scalar2).Times(scalar3).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, IntegerSign sign)
    {
        if (sign.IsZero) 
            return scalarProcessor.Times(scalar, scalarProcessor.ZeroValue);

        return sign.IsPositive 
            ? scalarProcessor.Positive(scalar) 
            : scalarProcessor.Negative(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign sign, T scalar)
    {
        if (sign.IsZero) 
            return scalarProcessor.Times(scalarProcessor.ZeroValue, scalar);

        return sign.IsPositive 
            ? scalarProcessor.Positive(scalar) 
            : scalarProcessor.Negative(scalar);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Times(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromText(scalar1),
            (a, b) => a.Times(b)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<int> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<uint> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<long> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<ulong> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<float> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<double> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<string> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<Scalar<T>> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IScalar<T>> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, params int[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, params uint[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, params long[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, params ulong[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, params float[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, params double[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, params string[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, params Scalar<T>[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IScalarProcessor<T> scalarProcessor, params IScalar<T>[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.One,
            (a, b) => a.Times(b)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IEnumerable<int> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Times(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IEnumerable<uint> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Times(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IEnumerable<long> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Times(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IEnumerable<ulong> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Times(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IEnumerable<float> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Times(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IEnumerable<double> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Times(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IEnumerable<string> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Times(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IEnumerable<T> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Times(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IEnumerable<Scalar<T>> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Times(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IEnumerable<IScalar<T>> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Times(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Times<T>(this IReadOnlyList<IScalar<T>> scalarList)
    {
        var scalarProcessor = scalarList[0].ScalarProcessor;

        return scalarProcessor.Times(scalarList);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params int[] scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params uint[] scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params long[] scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params ulong[] scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params float[] scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params double[] scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params string[] scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params Scalar<T>[] scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, params IScalar<T>[] scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<int> scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<uint> scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<long> scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<ulong> scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<float> scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<double> scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<string> scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<Scalar<T>> scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> NegativeTimes<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IScalar<T>> scalarList)
    {
        return scalarProcessor.Times(scalarList).Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TimesMapped<T, T2>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T2> scalarList, Func<T2, T> mappingFunc)
    {
        return scalarList
            .MapItems(mappingFunc)
            .Aggregate(
                scalarProcessor.One, 
                (a, b) => a.Times(b)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> TimesMapped<T, T2>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T2> scalarList, Func<int, T2, T> mappingFunc)
    {
        return scalarList
            .MapItems(mappingFunc)
            .Aggregate(
                scalarProcessor.One, 
                (a, b) => a.Times(b)
            );
    }


}