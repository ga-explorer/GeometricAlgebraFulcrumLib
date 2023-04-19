using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Converters.Generic
{
    public static class GaKVectorConverterUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> Convert<T>(this RGaProcessor<T> metric, XGaKVector<T> mv)
        {
            return mv switch
            {
                XGaScalar<T> mv1 => metric.Convert(mv1),
                XGaVector<T> mv1 => metric.Convert(mv1),
                XGaBivector<T> mv1 => metric.Convert(mv1),
                _ => metric.Convert((XGaHigherKVector<T>)mv)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> Convert<T>(this XGaProcessor<T> metric, RGaKVector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => metric.Convert(mv1),
                RGaVector<T> mv1 => metric.Convert(mv1),
                RGaBivector<T> mv1 => metric.Convert(mv1),
                _ => metric.Convert((RGaHigherKVector<T>)mv)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T> Convert<T>(this RGaProcessor<T> metric, Func<T, T> scalarMapping, XGaKVector<T> mv)
        {
            return mv switch
            {
                XGaScalar<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
                XGaVector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
                XGaBivector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
                _ => metric.Convert<T>(scalarMapping, (XGaHigherKVector<T>)mv)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T> Convert<T>(this XGaProcessor<T> metric, Func<T, T> scalarMapping, RGaKVector<T> mv)
        {
            return mv switch
            {
                RGaScalar<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
                RGaVector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
                RGaBivector<T> mv1 => metric.Convert<T>(scalarMapping, mv1),
                _ => metric.Convert<T>(scalarMapping, (RGaHigherKVector<T>)mv)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaKVector<T1> mv)
        {
            return mv switch
            {
                XGaScalar<T1> mv1 => metric.Convert(scalarMapping, mv1),
                XGaVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
                XGaBivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
                _ => metric.Convert(scalarMapping, (XGaHigherKVector<T1>)mv)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaKVector<T1> mv)
        {
            return mv switch
            {
                RGaScalar<T1> mv1 => metric.Convert(scalarMapping, mv1),
                RGaVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
                RGaBivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
                _ => metric.Convert(scalarMapping, (RGaHigherKVector<T1>)mv)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaKVector<T2> Convert<T1, T2>(this RGaProcessor<T2> metric, Func<T1, T2> scalarMapping, RGaKVector<T1> mv)
        {
            return mv switch
            {
                RGaScalar<T1> mv1 => metric.Convert(scalarMapping, mv1),
                RGaVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
                RGaBivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
                _ => metric.Convert(scalarMapping, (RGaHigherKVector<T1>)mv)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaKVector<T2> Convert<T1, T2>(this XGaProcessor<T2> metric, Func<T1, T2> scalarMapping, XGaKVector<T1> mv)
        {
            return mv switch
            {
                XGaScalar<T1> mv1 => metric.Convert(scalarMapping, mv1),
                XGaVector<T1> mv1 => metric.Convert(scalarMapping, mv1),
                XGaBivector<T1> mv1 => metric.Convert(scalarMapping, mv1),
                _ => metric.Convert(scalarMapping, (XGaHigherKVector<T1>)mv)
            };
        }
    }
}