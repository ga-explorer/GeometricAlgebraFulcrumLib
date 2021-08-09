using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Storage;
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
        public static GaVector<T> operator +(GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator +(GaVector<T> v1, IGaStorageVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator +(IGaStorageVector<T> v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Add(v1, v2.VectorStorage)
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
        public static GaVector<T> operator -(GaVector<T> v1, IGaStorageVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator -(IGaStorageVector<T> v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Subtract(v1, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, int s2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.IntegerToScalar(s2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(int s1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(processor.IntegerToScalar(s1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, double s2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.Float64ToScalar(s2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(double s1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(processor.Float64ToScalar(s1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, T s2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, s2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(T s1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(s1, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Gp(v1.VectorStorage, v2.VectorStorage).GetBivectorPart()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaVector<T> v1, IGaStorageVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Gp(v1.VectorStorage, v2).GetBivectorPart()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(IGaStorageVector<T> v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaBivector<T>(
                processor,
                processor.Gp(v1, v2.VectorStorage).GetBivectorPart()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, int s2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.IntegerToScalar(s2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, double s2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.Float64ToScalar(s2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, T s2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, s2)
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