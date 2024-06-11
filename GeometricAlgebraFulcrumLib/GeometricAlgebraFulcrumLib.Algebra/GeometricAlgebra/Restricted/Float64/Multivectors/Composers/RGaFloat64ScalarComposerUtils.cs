using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;

public static class RGaFloat64ScalarComposerUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar Scalar(this RGaFloat64Processor processor, double scalarValue)
    {
        return new RGaFloat64Scalar(processor, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ScalarFromSum(this RGaFloat64Processor processor, double scalar1, double scalar2)
    {
        return new RGaFloat64Scalar(
            processor,
            scalar1 + scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ScalarFromSum(this RGaFloat64Processor processor, params double[] scalarValueList)
    {
        var scalar = 0d;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(scalarValue.IsValid());

            if (scalarValue.IsZero())
                continue;

            scalar += scalarValue;
        }

        return new RGaFloat64Scalar(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ScalarFromSum(this RGaFloat64Processor processor, IEnumerable<double> scalarValueList)
    {
        var scalar = 0d;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(scalarValue.IsValid());

            if (scalarValue.IsZero())
                continue;

            scalar += scalarValue;
        }

        return new RGaFloat64Scalar(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ScalarFromProduct(this RGaFloat64Processor processor, double scalar1, double scalar2)
    {
        return new RGaFloat64Scalar(
            processor,
            scalar1 * scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ScalarFromProduct(this RGaFloat64Processor processor, int sign, double scalar1, double scalar2)
    {
        return new RGaFloat64Scalar(
            processor,
            sign * scalar1 * scalar2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ScalarFromProduct(this RGaFloat64Processor processor, params double[] scalarValueList)
    {
        var scalar = 1d;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(scalarValue.IsValid());

            if (scalarValue.IsZero())
                return new RGaFloat64Scalar(processor);

            scalar *= scalarValue;
        }

        return new RGaFloat64Scalar(processor, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar ScalarFromProduct(this RGaFloat64Processor processor, IEnumerable<double> scalarValueList)
    {
        var scalar = 1d;

        foreach (var scalarValue in scalarValueList)
        {
            Debug.Assert(scalarValue.IsValid());

            if (scalarValue.IsZero())
                return new RGaFloat64Scalar(processor);

            scalar *= scalarValue;
        }

        return new RGaFloat64Scalar(processor, scalar);
    }
}