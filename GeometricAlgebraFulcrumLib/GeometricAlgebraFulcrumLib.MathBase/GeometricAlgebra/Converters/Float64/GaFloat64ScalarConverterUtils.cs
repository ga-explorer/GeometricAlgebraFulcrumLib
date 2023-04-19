using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Converters.Float64;

public static class GaFloat64ScalarConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar Convert(this RGaFloat64Processor metric, XGaFloat64Scalar mv)
    {
        return new RGaFloat64Scalar(
            metric,
            mv.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar Convert(this XGaFloat64Processor metric, RGaFloat64Scalar mv)
    {
        return new XGaFloat64Scalar(
            metric,
            mv.ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Scalar Convert(this RGaFloat64Processor metric, Func<double, double> scalarMapping, XGaFloat64Scalar mv)
    {
        return new RGaFloat64Scalar(
            metric,
            scalarMapping(mv.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar Convert(this XGaFloat64Processor metric, Func<double, double> scalarMapping, RGaFloat64Scalar mv)
    {
        return new XGaFloat64Scalar(
            metric,
            scalarMapping(mv.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Convert<T>(this RGaProcessor<T> metric, Func<double, T> scalarMapping, RGaFloat64Scalar mv)
    {
        return new RGaScalar<T>(
            metric,
            scalarMapping(mv.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64Scalar mv)
    {
        return new XGaScalar<T>(
            metric,
            scalarMapping(mv.ScalarValue)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Convert<T>(this RGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64Scalar mv)
    {
        return new RGaScalar<T>(
            metric,
            scalarMapping(mv.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, RGaFloat64Scalar mv)
    {
        return new XGaScalar<T>(
            metric,
            scalarMapping(mv.ScalarValue)
        );
    }
}