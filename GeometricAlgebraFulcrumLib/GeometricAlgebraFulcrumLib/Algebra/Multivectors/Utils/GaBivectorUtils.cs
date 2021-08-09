using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils
{
    public static class GaBivectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Sp<T>(this GaBivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaScalar<T>(
                processor,
                processor.Sp(v1.BivectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Sp<T>(this GaBivector<T> v1)
        {
            var processor = v1.Processor;

            return new GaScalar<T>(
                processor,
                processor.Sp(v1.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> Inverse<T>(this GaBivector<T> v1)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.Sp(v1.BivectorStorage))
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ENorm<T>(this GaBivector<T> v1)
        {
            var processor = v1.Processor;

            return new GaScalar<T>(
                processor,
                processor.ENorm(v1.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ENormSquared<T>(this GaBivector<T> v1)
        {
            var processor = v1.Processor;

            return new GaScalar<T>(
                processor,
                processor.ENormSquared(v1.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ESp<T>(this GaBivector<T> v1)
        {
            var processor = v1.Processor;

            return new GaScalar<T>(
                processor,
                processor.ESp(v1.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ESp<T>(this GaBivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaScalar<T>(
                processor,
                processor.ESp(v1.BivectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> EInverse<T>(this GaBivector<T> v1)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.ESp(v1.BivectorStorage))
            );
        }
    }
}