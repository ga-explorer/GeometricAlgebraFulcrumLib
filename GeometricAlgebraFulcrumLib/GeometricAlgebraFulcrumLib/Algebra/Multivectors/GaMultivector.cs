using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.TextComposers;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors
{
    public sealed record GaMultivector<T> : 
        IGaAlgebraElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Negative(v1.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(int v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.IntegerToScalar(v1), v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(double v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.Float64ToScalar(v1), v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(T v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaScalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.Scalar, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(int v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.IntegerToScalar(v1), v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(double v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.Float64ToScalar(v1), v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(T v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaScalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.Scalar, v2.MultivectorStorage)
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(int v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.IntegerToScalar(v1), v2.MultivectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(double v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.Float64ToScalar(v1), v2.MultivectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(T v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1, v2.MultivectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaScalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.Scalar, v2.MultivectorStorage)
            );

        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, v2.Scalar)
            );
        }


        public IGaSpace Space 
            => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGaStorageMultivector<T> MultivectorStorage { get; }

        public GaScalar<T> this[ulong id] =>
            MultivectorStorage.TryGetTermScalar(id, out var scalar)
                ? scalar.CreateScalar(Processor)
                : Processor.GetZeroScalar().CreateScalar(Processor);

        public GaScalar<T> this[uint grade, ulong index] =>
            MultivectorStorage.TryGetTermScalar(grade, index, out var scalar)
                ? scalar.CreateScalar(Processor)
                : Processor.GetZeroScalar().CreateScalar(Processor);


        internal GaMultivector([NotNull] IGaProcessor<T> processor, [NotNull] IGaStorageMultivector<T> storage)
        {
            Processor = processor;
            MultivectorStorage = storage;
        }
        

        public override string ToString()
        {
            return MultivectorStorage.GetMultivectorText();
        }
    }
}