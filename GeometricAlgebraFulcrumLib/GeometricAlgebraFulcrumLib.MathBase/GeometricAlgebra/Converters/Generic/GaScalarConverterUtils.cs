using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Converters.Generic;

public static class GaScalarConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Convert<T>(this RGaProcessor<T> metric, XGaScalar<T> mv)
    {
        return new RGaScalar<T>(
            metric,
            mv.ScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Convert<T>(this XGaProcessor<T> metric, RGaScalar<T> mv)
    {
        return new XGaScalar<T>(
            metric,
            mv.ScalarValue()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T> Convert<T>(this RGaProcessor<T> metric, Func<T, T> scalarMapping, XGaScalar<T> mv)
    {
        return new RGaScalar<T>(
            metric,
            scalarMapping(mv.ScalarValue())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T> Convert<T>(this XGaProcessor<T> metric, Func<T, T> scalarMapping, RGaScalar<T> mv)
    {
        return new XGaScalar<T>(
            metric,
            scalarMapping(mv.ScalarValue())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaScalar<T1> mv)
    {
        return new RGaScalar<T2>(
            metric,
            scalarMapping(mv.ScalarValue())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaScalar<T1> mv)
    {
        return new XGaScalar<T2>(
            metric,
            scalarMapping(mv.ScalarValue())
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaScalar<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaScalar<T1> mv)
    {
        return new RGaScalar<T2>(
            metric,
            scalarMapping(mv.ScalarValue())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaScalar<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaScalar<T1> mv)
    {
        return new XGaScalar<T2>(
            metric,
            scalarMapping(mv.ScalarValue())
        );
    }
}