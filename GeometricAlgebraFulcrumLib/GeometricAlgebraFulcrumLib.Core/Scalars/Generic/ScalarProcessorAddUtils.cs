using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

public static class ScalarProcessorAddUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromNumber(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, T scalar2)
    {
        return scalarProcessor.ScalarFromText(scalar1).Add(
            scalarProcessor.ScalarFromValue(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, IntegerSign scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2.Value)
        ).Sign();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, int scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, uint scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, long scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, ulong scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, float scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, double scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromNumber(scalar2)
        ).Sign();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> SignOfAdd<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, string scalar2)
    {
        return scalarProcessor.ScalarFromValue(scalar1).Add(
            scalarProcessor.ScalarFromText(scalar2)
        ).Sign();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddOne<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1)
    {
        return scalarProcessor.Add(
            scalar1,
            scalarProcessor.OneValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddOne<T>(this IScalarProcessor<T> scalarProcessor, int scalar1)
    {
        return scalarProcessor.Add(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddOne<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1)
    {
        return scalarProcessor.Add(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddOne<T>(this IScalarProcessor<T> scalarProcessor, long scalar1)
    {
        return scalarProcessor.Add(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddOne<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1)
    {
        return scalarProcessor.Add(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddOne<T>(this IScalarProcessor<T> scalarProcessor, float scalar1)
    {
        return scalarProcessor.Add(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddOne<T>(this IScalarProcessor<T> scalarProcessor, double scalar1)
    {
        return scalarProcessor.Add(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddOne<T>(this IScalarProcessor<T> scalarProcessor, string scalar1)
    {
        return scalarProcessor.Add(
            scalar1,
            scalarProcessor.OneValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar1)
    {
        return scalarProcessor.Add(
            scalar1,
            scalarProcessor.OneValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddToOne<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar2)
    {
        return scalarProcessor.Add(
            scalarProcessor.OneValue,
            scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddToOne<T>(this IScalarProcessor<T> scalarProcessor, int scalar2)
    {
        return scalarProcessor.Add(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddToOne<T>(this IScalarProcessor<T> scalarProcessor, uint scalar2)
    {
        return scalarProcessor.Add(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddToOne<T>(this IScalarProcessor<T> scalarProcessor, long scalar2)
    {
        return scalarProcessor.Add(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddToOne<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar2)
    {
        return scalarProcessor.Add(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddToOne<T>(this IScalarProcessor<T> scalarProcessor, float scalar2)
    {
        return scalarProcessor.Add(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddToOne<T>(this IScalarProcessor<T> scalarProcessor, double scalar2)
    {
        return scalarProcessor.Add(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddToOne<T>(this IScalarProcessor<T> scalarProcessor, string scalar2)
    {
        return scalarProcessor.Add(
            scalarProcessor.OneValue,
            scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddToOne<T>(this IScalarProcessor<T> scalarProcessor, T scalar2)
    {
        return scalarProcessor.Add(
            scalarProcessor.OneValue,
            scalar2
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddSquares<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
    {
        return scalarList
            .Select(s => scalarProcessor.Times(s, s))
            .Aggregate(
                scalarProcessor.Zero,
                (scalar1, scalar2) => scalar1.Add(scalar2)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddSquares<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
    {
        return scalarList
            .Select(s => scalarProcessor.Times(s, s))
            .Aggregate(
                scalarProcessor.Zero,
                (scalar1, scalar2) => scalar1.Add(scalar2)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IntegerSign scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Add(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, int scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, long scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ulong scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, float scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, double scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromNumber(scalar1),
            (a, b) => a.Add(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, string scalar1, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.ScalarFromText(scalar1),
            (a, b) => a.Add(b)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<int> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<uint> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<long> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<ulong> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<float> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<double> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<string> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<Scalar<T>> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IScalar<T>> scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params int[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params uint[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params long[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params ulong[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params float[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params double[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params string[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params T[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params Scalar<T>[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params IScalar<T>[] scalarList)
    {
        return scalarList.Aggregate(
            scalarProcessor.Zero,
            (a, b) => a.Add(b)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, T scalar2, T scalar3)
    {
        return scalarProcessor.Add(scalar1, scalar2).Add(scalar3);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IEnumerable<int> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Add(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IEnumerable<uint> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Add(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IEnumerable<long> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Add(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IEnumerable<ulong> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Add(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IEnumerable<float> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Add(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IEnumerable<double> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Add(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IEnumerable<string> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Add(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IEnumerable<T> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Add(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IEnumerable<Scalar<T>> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Add(scalarList);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IEnumerable<IScalar<T>> scalarList, IScalarProcessor<T> scalarProcessor)
    {
        return scalarProcessor.Add(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> Add<T>(this IReadOnlyList<IScalar<T>> scalarList)
    {
        var scalarProcessor = scalarList[0].ScalarProcessor;

        return scalarProcessor.Add(scalarList);
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddMapped<T, T2>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T2> scalarList, Func<T2, T> mappingFunc)
    {
        return scalarList
            .MapItems(mappingFunc)
            .Aggregate(
                scalarProcessor.Zero, 
                (a, b) => a.Add(b)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Scalar<T> AddMapped<T, T2>(this IScalarProcessor<T> scalarProcessor, IEnumerable<T2> scalarList, Func<int, T2, T> mappingFunc)
    {
        return scalarList
            .MapItems(mappingFunc)
            .Aggregate(
                scalarProcessor.Zero, 
                (a, b) => a.Add(b)
            );
    }


}