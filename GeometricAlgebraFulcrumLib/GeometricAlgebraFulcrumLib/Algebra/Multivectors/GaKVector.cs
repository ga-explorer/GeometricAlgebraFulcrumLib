using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Utils;
using GeometricAlgebraFulcrumLib.TextComposers;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors
{
    public sealed record GaKVector<T>
        : IGaAlgebraElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator -(GaKVector<T> v1)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Negative(v1.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, IGaStorageKVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(IGaStorageKVector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1, v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, IGaStorageKVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(IGaStorageKVector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1, v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, int s2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, processor.IntegerToScalar(s2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(int s1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(processor.IntegerToScalar(s1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, double s2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, processor.Float64ToScalar(s2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(double s1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(processor.Float64ToScalar(s1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, T s2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(T s1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(s1, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, GaScalar<T> s2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, s2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaScalar<T> s1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(s1.Scalar, v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, int s2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, processor.IntegerToScalar(s2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, double s2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, processor.Float64ToScalar(s2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, T s2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, GaScalar<T> s2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, s2.Scalar)
            );
        }


        public IGaSpace Space 
            => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGaStorageKVector<T> KVectorStorage { get; }

        public GaScalar<T> this[int index]
            => Processor.CreateScalar(
                Processor.GetTermScalarByIndex(KVectorStorage, (ulong) index)
            );
        
        public T this[ulong index]
            => Processor.CreateScalar(
                Processor.GetTermScalarByIndex(KVectorStorage, index)
            );


        internal GaKVector([NotNull] IGaProcessor<T> processor, [NotNull] IGaStorageKVector<T> storage)
        {
            Processor = processor;
            KVectorStorage = storage;
        }


        public GaMultivector<T> Reverse()
        {
            return new GaMultivector<T>(
                Processor, 
                Processor.Reverse(KVectorStorage)
            );
        }

        public GaMultivector<T> GradeInvolution()
        {
            return new GaMultivector<T>(
                Processor, 
                Processor.GradeInvolution(KVectorStorage)
            );
        }

        public GaMultivector<T> CliffordConjugate()
        {
            return new GaMultivector<T>(
                Processor, 
                Processor.CliffordConjugate(KVectorStorage)
            );
        }

        public override string ToString()
        {
            return KVectorStorage.GetMultivectorText();
        }
    }
}