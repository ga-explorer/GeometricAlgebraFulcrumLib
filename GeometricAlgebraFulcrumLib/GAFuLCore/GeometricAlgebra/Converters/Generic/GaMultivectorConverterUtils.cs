using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Converters.Generic;

public static class GaMultivectorConverterUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> Convert<T>(this RGaProcessor<T> metric, XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => metric.Convert(mv1),
            XGaVector<T> mv1 => metric.Convert(mv1),
            XGaBivector<T> mv1 => metric.Convert(mv1),
            XGaHigherKVector<T> mv1 => metric.Convert(mv1),
            XGaGradedMultivector<T> mv1 => metric.Convert(mv1),
            _ => metric.Convert((XGaUniformMultivector<T>)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Convert<T>(this XGaProcessor<T> metric, RGaMultivector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> mv1 => metric.Convert(mv1),
            RGaVector<T> mv1 => metric.Convert(mv1),
            RGaBivector<T> mv1 => metric.Convert(mv1),
            RGaHigherKVector<T> mv1 => metric.Convert(mv1),
            RGaGradedMultivector<T> mv1 => metric.Convert(mv1),
            _ => metric.Convert((RGaUniformMultivector<T>)mv)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> Convert<T>(this RGaProcessor<T> metric, Func<T, T> scalarMapping, XGaMultivector<T> mv)
    {
        return mv switch
        {
            XGaScalar<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
            XGaVector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
            XGaBivector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
            XGaHigherKVector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
            XGaGradedMultivector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
            _ => metric.Convert<T>(scalarMapping, (XGaUniformMultivector<T>)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> Convert<T>(this XGaProcessor<T> metric, Func<T, T> scalarMapping, RGaMultivector<T> mv)
    {
        return mv switch
        {
            RGaScalar<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
            RGaVector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
            RGaBivector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
            RGaHigherKVector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
            RGaGradedMultivector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
            _ => metric.Convert<T>(scalarMapping, (RGaUniformMultivector<T>)mv)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaMultivector<T1> mv)
    {
        return mv switch
        {
            XGaScalar<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaBivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaHigherKVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaGradedMultivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaUniformMultivector<T1>)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaMultivector<T1> mv)
    {
        return mv switch
        {
            RGaScalar<T1> mv1 => metric.Convert(scalarMapping, mv1),
            RGaVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            RGaBivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            RGaHigherKVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            RGaGradedMultivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (RGaUniformMultivector<T1>)mv)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaMultivector<T1> mv)
    {
        return mv switch
        {
            RGaScalar<T1> mv1 => metric.Convert(scalarMapping, mv1),
            RGaVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            RGaBivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            RGaHigherKVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            RGaGradedMultivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (RGaUniformMultivector<T1>)mv)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaMultivector<T1> mv)
    {
        return mv switch
        {
            XGaScalar<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaBivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaHigherKVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            XGaGradedMultivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
            _ => metric.Convert(scalarMapping, (XGaUniformMultivector<T1>)mv)
        };
    }
}