using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

public static class XGaScalarComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Scalar<T>(this XGaProcessor<T> processor, int scalarValue)
    {
        return new XGaScalar<T>(
            processor, 
            processor.ScalarProcessor.ValueFromNumber(scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Scalar<T>(this XGaProcessor<T> processor, uint scalarValue)
    {
        return new XGaScalar<T>(
            processor, 
            processor.ScalarProcessor.ValueFromNumber(scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Scalar<T>(this XGaProcessor<T> processor, long scalarValue)
    {
        return new XGaScalar<T>(
            processor, 
            processor.ScalarProcessor.ValueFromNumber(scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Scalar<T>(this XGaProcessor<T> processor, ulong scalarValue)
    {
        return new XGaScalar<T>(
            processor, 
            processor.ScalarProcessor.ValueFromNumber(scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Scalar<T>(this XGaProcessor<T> processor, float scalarValue)
    {
        return new XGaScalar<T>(
            processor, 
            processor.ScalarProcessor.ValueFromNumber(scalarValue)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Scalar<T>(this XGaProcessor<T> processor, double scalarValue)
    {
        return new XGaScalar<T>(
            processor, 
            processor.ScalarProcessor.ValueFromNumber(scalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Scalar<T>(this XGaProcessor<T> processor, T scalarValue)
    {
        return new XGaScalar<T>(processor, scalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Scalar<T>(this XGaProcessor<T> processor, Scalar<T> scalar)
    {
        return new XGaScalar<T>(processor, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Scalar<T>(this XGaProcessor<T> processor, IScalar<T> scalar)
    {
        return new XGaScalar<T>(processor, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ScalarFromSum<T>(this XGaProcessor<T> processor, T scalar1, T scalar2)
    {
        return new XGaScalar<T>(
            processor,
            processor.ScalarProcessor.Add(scalar1, scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ScalarFromSum<T>(this XGaProcessor<T> processor, params T[] scalarValueList)
    {
        var scalar = processor.ScalarProcessor.ZeroValue;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

            if (processor.ScalarProcessor.IsZero(scalarValue))
                continue;

            scalar = processor.ScalarProcessor.Add(scalar, scalarValue).ScalarValue;
        }

        return new XGaScalar<T>(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ScalarFromSum<T>(this XGaProcessor<T> processor, IEnumerable<T> scalarValueList)
    {
        var scalar = processor.ScalarProcessor.ZeroValue;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

            if (processor.ScalarProcessor.IsZero(scalarValue))
                continue;

            scalar = processor.ScalarProcessor.Add(scalar, scalarValue).ScalarValue;
        }

        return new XGaScalar<T>(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ScalarFromProduct<T>(this XGaProcessor<T> processor, T scalar1, T scalar2)
    {
        return new XGaScalar<T>(
            processor,
            processor.ScalarProcessor.Times(scalar1, scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ScalarFromProduct<T>(this XGaProcessor<T> processor, int sign, T scalar1, T scalar2)
    {
        return new XGaScalar<T>(
            processor,
            processor.ScalarProcessor.Times(sign, scalar1, scalar2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ScalarFromProduct<T>(this XGaProcessor<T> processor, params T[] scalarValueList)
    {
        var scalar = processor.ScalarProcessor.OneValue;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

            if (processor.ScalarProcessor.IsZero(scalarValue))
                return new XGaScalar<T>(processor);

            scalar = processor.ScalarProcessor.Times(scalar, scalarValue).ScalarValue;
        }

        return new XGaScalar<T>(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> ScalarFromProduct<T>(this XGaProcessor<T> processor, IEnumerable<T> scalarValueList)
    {
        var scalar = processor.ScalarProcessor.OneValue;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(processor.ScalarProcessor.IsValid(scalarValue));

            if (processor.ScalarProcessor.IsZero(scalarValue))
                return new XGaScalar<T>(processor);

            scalar = processor.ScalarProcessor.Times(scalar, scalarValue).ScalarValue;
        }

        return new XGaScalar<T>(processor, scalar);
    }
}