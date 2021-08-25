using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Utils;
using GeometricAlgebraFulcrumLib.TextComposers;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors
{
    public sealed record GaVector<T>
        : IGaAlgebraElement<T>
    {
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static implicit operator IGasVector<T>(GaGenericVector<T> v)
        //{
        //    return v.Vector;
        //}


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator -(GaVector<T> v1)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Negative(v1.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(int v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.IntegerToScalar(v1), v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(double v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.Float64ToScalar(v1), v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(T v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaScalar<T> v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.Scalar, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator +(GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(int v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.IntegerToScalar(v1), v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(double v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.Float64ToScalar(v1), v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(T v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaScalar<T> v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.Scalar, v2.VectorStorage)
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator -(GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(int v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(processor.IntegerToScalar(v1), v2.VectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(double v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(processor.Float64ToScalar(v1), v2.VectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(T v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1, v2.VectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, v2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaScalar<T> v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.Scalar, v2.VectorStorage)
            );

        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, v2.Scalar)
            );
        }


        public IGaSpace Space 
            => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGaStorageVector<T> VectorStorage { get; }

        public GaScalar<T> this[ulong index]
            => Processor
                .GetTermScalarByIndex(VectorStorage, index)
                .CreateScalar(Processor);


        internal GaVector([NotNull] IGaProcessor<T> processor, [NotNull] IGaStorageVector<T> vector)
        {
            Processor = processor;
            VectorStorage = vector;
        }


        public override string ToString()
        {
            return VectorStorage.GetMultivectorText();
        }
    }
}