using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Converters.Float64;

public static class GaFloat64KVectorConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Convert(this RGaFloat64Processor metric, XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => metric.Convert(mv1),
            XGaFloat64Vector mv1 => metric.Convert(mv1),
            XGaFloat64Bivector mv1 => metric.Convert(mv1),
            _ => metric.Convert((XGaFloat64HigherKVector)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Convert(this XGaFloat64Processor metric, RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => metric.Convert(mv1),
            RGaFloat64Vector mv1 => metric.Convert(mv1),
            RGaFloat64Bivector mv1 => metric.Convert(mv1),
            _ => metric.Convert((RGaFloat64HigherKVector)mv)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64KVector Convert(this RGaFloat64Processor metric, Func<double, double> scalarMapping, XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaFloat64HigherKVector)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector Convert(this XGaFloat64Processor metric, Func<double, double> scalarMapping, RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (RGaFloat64HigherKVector)mv)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Convert<T>(this RGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaFloat64HigherKVector)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (RGaFloat64HigherKVector)mv)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKVector<T> Convert<T>(this RGaProcessor<T> metric, Func<double, T> scalarMapping, RGaFloat64KVector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (RGaFloat64HigherKVector)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64KVector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaFloat64HigherKVector)mv)
        };
    }
}