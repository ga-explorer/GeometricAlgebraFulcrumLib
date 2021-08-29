using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Space;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors
{
    public sealed record GaKVector<T>
        : IGeometricAlgebraElement<T>
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
        public static GaMultivector<T> operator +(GaKVector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(int v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromInteger(v1), v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(double v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromFloat64(v1), v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(T v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1, v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(Scalar<T> v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.KVectorStorage)
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
        public static GaMultivector<T> operator -(GaKVector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(int v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromInteger(v1), v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(double v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromFloat64(v1), v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(T v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1, v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(Scalar<T> v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.KVectorStorage)
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.KVectorStorage)
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
        public static GaKVector<T> operator *(GaKVector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(int v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(processor.GetScalarFromInteger(v1), v2.KVectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(double v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(processor.GetScalarFromFloat64(v1), v2.KVectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(T v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1, v2.KVectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(Scalar<T> v1, GaKVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.KVectorStorage)
            );

        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, v2.ScalarValue)
            );
        }


        public IGaSpace Space 
            => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGaKVectorStorage<T> KVectorStorage { get; }

        public Scalar<T> this[int index]
            => Processor.CreateGaScalarStorage(
                Processor.GetTermScalarByIndex(KVectorStorage, (ulong) index)
            );
        
        public T this[ulong index]
            => Processor.CreateGaScalarStorage(
                Processor.GetTermScalarByIndex(KVectorStorage, index)
            );


        internal GaKVector([NotNull] IScalarProcessor<T> processor, [NotNull] IGaKVectorStorage<T> storage)
        {
            Processor = (IGaProcessor<T>) processor;
            KVectorStorage = storage;
        }
        
        internal GaKVector([NotNull] IGaProcessor<T> processor, [NotNull] IGaKVectorStorage<T> storage)
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