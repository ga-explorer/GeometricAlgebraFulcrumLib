using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Converters.Float64;

public static class GaFloat64MultivectorConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Convert(this RGaFloat64Processor metric, XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => metric.Convert(mv1),
            XGaFloat64Vector mv1 => metric.Convert(mv1),
            XGaFloat64Bivector mv1 => metric.Convert(mv1),
            XGaFloat64HigherKVector mv1 => metric.Convert(mv1),
            XGaFloat64GradedMultivector mv1 => metric.Convert(mv1),
            _ => metric.Convert((XGaFloat64UniformMultivector)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Convert(this XGaFloat64Processor metric, RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => metric.Convert(mv1),
            RGaFloat64Vector mv1 => metric.Convert(mv1),
            RGaFloat64Bivector mv1 => metric.Convert(mv1),
            RGaFloat64HigherKVector mv1 => metric.Convert(mv1),
            RGaFloat64GradedMultivector mv1 => metric.Convert(mv1),
            _ => metric.Convert((RGaFloat64UniformMultivector)mv)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector Convert(this RGaFloat64Processor metric, Func<double, double> scalarMapping, XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64HigherKVector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64GradedMultivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaFloat64UniformMultivector)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector Convert(this XGaFloat64Processor metric, Func<double, double> scalarMapping, RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64HigherKVector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64GradedMultivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (RGaFloat64UniformMultivector)mv)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> Convert<T>(this RGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64HigherKVector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64GradedMultivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaFloat64UniformMultivector)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64HigherKVector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64GradedMultivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (RGaFloat64UniformMultivector)mv)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> Convert<T>(this RGaProcessor<T> metric, Func<double, T> scalarMapping, RGaFloat64Multivector mv)
    {
        return mv switch
        {
            RGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64HigherKVector mv1 => metric.Convert(scalarMapping, mv1),
            RGaFloat64GradedMultivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (RGaFloat64UniformMultivector)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Convert<T>(this XGaProcessor<T> metric, Func<double, T> scalarMapping, XGaFloat64Multivector mv)
    {
        return mv switch
        {
            XGaFloat64Scalar mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Vector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64Bivector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64HigherKVector mv1 => metric.Convert(scalarMapping, mv1),
            XGaFloat64GradedMultivector mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaFloat64UniformMultivector)mv)
        };
    }

}