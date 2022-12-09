using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors
{
    public static class LinVectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetNorm<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return processor.Sqrt(
                processor.TimesInner(v1.VectorStorage)
            ).CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetNormSquared<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return processor.TimesInner(v1.VectorStorage).CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> InnerProduct<T>(this LinVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.LinearProcessor;

            return processor.TimesInner(
                v1.VectorStorage,
                v2.VectorStorage
            ).CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> GetUnitVector<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                processor.Divide(
                    v1.VectorStorage,
                    processor.Sqrt(processor.TimesInner(v1.VectorStorage))
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetCosAngle<T>(this LinVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.LinearProcessor;

            return processor.Divide(
                processor.TimesInner(v1.VectorStorage, v2.VectorStorage),
                processor.Sqrt(processor.Times(
                    processor.TimesInner(v1.VectorStorage),
                    processor.TimesInner(v2.VectorStorage)
                ))
            ).CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> GetAngle<T>(this LinVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.LinearProcessor;

            return processor.ArcCos(processor.Divide(
                processor.TimesInner(v1.VectorStorage, v2.VectorStorage),
                processor.Sqrt(processor.Times(
                    processor.TimesInner(v1.VectorStorage),
                    processor.TimesInner(v2.VectorStorage)
                ))
            )).CreateScalar(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> MapScalars<T>(this LinVector<T> v1, Func<T, T> mappingFunc)
        {
            return new LinVector<T>(
                v1.LinearProcessor,
                v1.VectorStorage.MapScalars(mappingFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> MapScalars<T>(this LinVector<T> v1, Func<ulong, T, T> mappingFunc)
        {
            return new LinVector<T>(
                v1.LinearProcessor,
                v1.VectorStorage.MapScalars(mappingFunc)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Abs<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Abs)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Sqrt<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Sqrt)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> SqrtOfAbs<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.SqrtOfAbs)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Exp<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Exp)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> LogE<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.LogE)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Log2<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Log2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Log10<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Log10)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Log<T>(this LinVector<T> v1, T scalar)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(s => processor.Log(s, scalar))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Log<T>(this T scalar, LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(s => processor.Log(scalar, s))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Power<T>(this LinVector<T> v1, T scalar)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(s => processor.Power(s, scalar))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Power<T>(this T scalar, LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(s => processor.Power(scalar, s))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Cos<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Cos)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Sin<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Sin)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Tan<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Tan)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> ArcCos<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.ArcCos)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> ArcSin<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.ArcSin)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> ArcTan<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.ArcTan)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Cosh<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Cosh)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Sinh<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Sinh)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinVector<T> Tanh<T>(this LinVector<T> v1)
        {
            var processor = v1.LinearProcessor;

            return new LinVector<T>(
                processor,
                v1.VectorStorage.MapScalars(processor.Tanh)
            );
        }
    }
}