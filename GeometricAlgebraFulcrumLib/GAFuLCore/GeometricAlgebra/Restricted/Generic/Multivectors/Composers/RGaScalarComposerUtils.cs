using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;

public static class RGaScalarComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Scalar<T>(this RGaProcessor<T> processor, int mv)
    {
        return new RGaScalar<T>(
            processor,
            processor.ScalarProcessor.ScalarFromNumber(mv)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Scalar<T>(this RGaProcessor<T> processor, uint mv)
    {
        return new RGaScalar<T>(
            processor,
            processor.ScalarProcessor.ScalarFromNumber(mv)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Scalar<T>(this RGaProcessor<T> processor, long mv)
    {
        return new RGaScalar<T>(
            processor,
            processor.ScalarProcessor.ScalarFromNumber(mv)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Scalar<T>(this RGaProcessor<T> processor, ulong mv)
    {
        return new RGaScalar<T>(
            processor,
            processor.ScalarProcessor.ScalarFromNumber(mv)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Scalar<T>(this RGaProcessor<T> processor, float mv)
    {
        return new RGaScalar<T>(
            processor,
            processor.ScalarProcessor.ScalarFromNumber(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Scalar<T>(this RGaProcessor<T> processor, double mv)
    {
        return new RGaScalar<T>(
            processor,
            processor.ScalarProcessor.ScalarFromNumber(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Scalar<T>(this RGaProcessor<T> processor, string mv)
    {
        return new RGaScalar<T>(
            processor,
            processor.ScalarProcessor.ScalarFromText(mv)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Scalar<T>(this RGaProcessor<T> processor, T scalarValue)
    {
        return new RGaScalar<T>(processor, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Scalar<T>(this RGaProcessor<T> processor, IScalar<T> scalar)
    {
        return new RGaScalar<T>(processor, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ScalarFromSum<T>(this RGaProcessor<T> processor, T scalar1, T scalar2)
    {
        return new RGaScalar<T>(
            processor,

            processor.ScalarProcessor.Add(scalar1, scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ScalarFromSum<T>(this RGaProcessor<T> processor, params T[] scalarValueList)
    {
        var scalar = processor.ScalarProcessor.ZeroValue;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

            if (processor.ScalarProcessor.IsZero(scalarValue))
                continue;

            scalar = processor.ScalarProcessor.Add(scalar, scalarValue).ScalarValue;
        }

        return new RGaScalar<T>(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ScalarFromSum<T>(this RGaProcessor<T> processor, IEnumerable<T> scalarValueList)
    {
        var scalar = processor.ScalarProcessor.ZeroValue;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

            if (processor.ScalarProcessor.IsZero(scalarValue))
                continue;

            scalar = processor.ScalarProcessor.Add(scalar, scalarValue).ScalarValue;
        }

        return new RGaScalar<T>(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ScalarFromProduct<T>(this RGaProcessor<T> processor, T scalar1, T scalar2)
    {
        return new RGaScalar<T>(
            processor,

            processor.ScalarProcessor.Times(scalar1, scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ScalarFromProduct<T>(this RGaProcessor<T> processor, int sign, T scalar1, T scalar2)
    {
        return new RGaScalar<T>(
            processor,

            processor.ScalarProcessor.Times(sign, scalar1, scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ScalarFromProduct<T>(this RGaProcessor<T> processor, params T[] scalarValueList)
    {
        var scalar = processor.ScalarProcessor.OneValue;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

            if (processor.ScalarProcessor.IsZero(scalarValue))
                return new RGaScalar<T>(processor);

            scalar = processor.ScalarProcessor.Times(scalar, scalarValue).ScalarValue;
        }

        return new RGaScalar<T>(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> ScalarFromProduct<T>(this RGaProcessor<T> processor, IEnumerable<T> scalarValueList)
    {
        var scalar = processor.ScalarProcessor.OneValue;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

            if (processor.ScalarProcessor.IsZero(scalarValue))
                return new RGaScalar<T>(processor);

            scalar = processor.ScalarProcessor.Times(scalar, scalarValue).ScalarValue;
        }

        return new RGaScalar<T>(processor, scalar);
    }
}