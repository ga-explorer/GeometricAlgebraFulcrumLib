using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors
{
    public static class GaVectorLcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this GaVector<T> mv1, int mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Lcp<T>(this int mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this GaVector<T> mv1, uint mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Lcp<T>(this uint mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this GaVector<T> mv1, long mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Lcp<T>(this long mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this GaVector<T> mv1, ulong mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Lcp<T>(this ulong mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this GaVector<T> mv1, float mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Lcp<T>(this float mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this GaVector<T> mv1, double mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Lcp<T>(this double mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this GaVector<T> mv1, T mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Lcp<T>(this T mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    mv1,
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this GaVector<T> mv1, Scalar<T> mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Lcp<T>(this Scalar<T> mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    mv1.ScalarValue,
                    mv2.VectorStorage
                )
            );
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Vector<T> Lcp<T>(this Vector<T> mv1, Bivector<T> mv2)
        //{
        //    var processor = mv1.GeometricProcessor;

        //    return new Vector<T>(
        //        processor, 
        //        processor.Lcp(
        //            mv1.VectorStorage, 
        //            mv2.BivectorStorage
        //        ).GetVectorPart()
        //    );
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this GaBivector<T> mv1, GaVector<T> mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this GaVector<T> mv1, GaVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor.CreateScalar(
                processor.Sp(mv1.VectorStorage, mv2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this GaVector<T> mv1, int mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ELcp<T>(this int mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this GaVector<T> mv1, uint mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ELcp<T>(this uint mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this GaVector<T> mv1, long mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ELcp<T>(this long mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this GaVector<T> mv1, ulong mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ELcp<T>(this ulong mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this GaVector<T> mv1, float mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ELcp<T>(this float mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this GaVector<T> mv1, double mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ELcp<T>(this double mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this GaVector<T> mv1, T mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ELcp<T>(this T mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    mv1,
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this GaVector<T> mv1, Scalar<T> mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ELcp<T>(this Scalar<T> mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    mv1.ScalarValue,
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> ELcp<T>(this GaVector<T> mv1, GaBivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.ELcp(
                    mv1.VectorStorage,
                    mv2.BivectorStorage
                ).GetVectorPart()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this GaBivector<T> mv1, GaVector<T> mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this GaVector<T> mv1, GaVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor.CreateScalar(
                processor.ESp(mv1.VectorStorage, mv2.VectorStorage)
            );
        }
    }
}