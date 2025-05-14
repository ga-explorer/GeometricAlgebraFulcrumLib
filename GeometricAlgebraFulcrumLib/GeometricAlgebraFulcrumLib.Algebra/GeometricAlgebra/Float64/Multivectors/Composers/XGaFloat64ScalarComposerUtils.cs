using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors.Composers;

public static class XGaFloat64ScalarComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar Scalar(this XGaFloat64Processor processor, double scalarValue)
    {
        return new XGaFloat64Scalar(processor, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ScalarFromSum(this XGaFloat64Processor processor, double scalar1, double scalar2)
    {
        return new XGaFloat64Scalar(
            processor,

            scalar1 + scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ScalarFromSum(this XGaFloat64Processor processor, params double[] scalarValueList)
    {
        var scalar = 0d;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(scalarValue.IsValid());

            if (scalarValue.IsZero())
                continue;

            scalar += scalarValue;
        }

        return new XGaFloat64Scalar(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ScalarFromSum(this XGaFloat64Processor processor, IEnumerable<double> scalarValueList)
    {
        var scalar = 0d;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(scalarValue.IsValid());

            if (scalarValue.IsZero())
                continue;

            scalar += scalarValue;
        }

        return new XGaFloat64Scalar(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ScalarFromProduct(this XGaFloat64Processor processor, double scalar1, double scalar2)
    {
        return new XGaFloat64Scalar(
            processor,
            scalar1 * scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ScalarFromProduct(this XGaFloat64Processor processor, int sign, double scalar1, double scalar2)
    {
        return new XGaFloat64Scalar(
            processor,
            sign * scalar1 * scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ScalarFromProduct(this XGaFloat64Processor processor, params double[] scalarValueList)
    {
        var scalar = 1d;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(scalarValue.IsValid());

            if (scalarValue.IsZero())
                return new XGaFloat64Scalar(processor);

            scalar *= scalarValue;
        }

        return new XGaFloat64Scalar(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar ScalarFromProduct(this XGaFloat64Processor processor, IEnumerable<double> scalarValueList)
    {
        var scalar = 1d;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(scalarValue.IsValid());

            if (scalarValue.IsZero())
                return new XGaFloat64Scalar(processor);

            scalar *= scalarValue;
        }

        return new XGaFloat64Scalar(processor, scalar);
    }
}