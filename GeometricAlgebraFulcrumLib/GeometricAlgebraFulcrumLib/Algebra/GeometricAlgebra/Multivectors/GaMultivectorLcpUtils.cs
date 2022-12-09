using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors
{
    public static class GaMultivectorLcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this int v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this uint v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this long v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this ulong v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this float v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this double v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this T v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this Scalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Lcp(
                    v1.MultivectorStorage,
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this LinVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Lcp(
                    v1.VectorStorage.CreateVectorStorage(),
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Lcp(
                    v1.MultivectorStorage,
                    v2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Lcp(
                    v1.VectorStorage,
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Lcp(
                    v1.MultivectorStorage,
                    v2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Lcp(
                    v1.BivectorStorage,
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Lcp(
                    v1.MultivectorStorage,
                    v2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaKVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Lcp(
                    v1.KVectorStorage,
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Lcp(
                    v1.MultivectorStorage,
                    v2.MultivectorStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this int v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this uint v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this long v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this ulong v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this float v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this double v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this T v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this Scalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.ELcp(
                    v1.MultivectorStorage,
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this LinVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.ELcp(
                    v1.VectorStorage.CreateVectorStorage(),
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.ELcp(
                    v1.MultivectorStorage,
                    v2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.ELcp(
                    v1.VectorStorage,
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.ELcp(
                    v1.MultivectorStorage,
                    v2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.ELcp(
                    v1.BivectorStorage,
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.ELcp(
                    v1.MultivectorStorage,
                    v2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaKVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.ELcp(
                    v1.KVectorStorage,
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.ELcp(
                    v1.MultivectorStorage,
                    v2.MultivectorStorage
                )
            );
        }
    }
}