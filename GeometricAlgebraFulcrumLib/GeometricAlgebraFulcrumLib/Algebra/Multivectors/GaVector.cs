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
    public sealed record GaVector<T>
        : IGeometricAlgebraElement<T>
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
                processor.Add(v1.VectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(int v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromInteger(v1), v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(double v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromFloat64(v1), v2.VectorStorage)
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
        public static GaMultivector<T> operator +(GaVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(Scalar<T> v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.VectorStorage)
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
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(int v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromInteger(v1), v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(double v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromFloat64(v1), v2.VectorStorage)
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
        public static GaMultivector<T> operator -(GaVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(Scalar<T> v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.VectorStorage)
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
                processor.Times(v1.VectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(int v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(processor.GetScalarFromInteger(v1), v2.VectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(double v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(processor.GetScalarFromFloat64(v1), v2.VectorStorage)
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
        public static GaVector<T> operator *(GaVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(Scalar<T> v1, GaVector<T> v2)
        {
            var processor = v2.Processor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.VectorStorage)
            );

        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromFloat64(v2))
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
        public static GaVector<T> operator /(GaVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, v2.ScalarValue)
            );
        }


        public IGaSpace Space 
            => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGaVectorStorage<T> VectorStorage { get; }

        public Scalar<T> this[ulong index]
            => Processor
                .GetTermScalarByIndex(VectorStorage, index)
                .CreateGaScalarStorage(Processor);


        internal GaVector([NotNull] IScalarProcessor<T> processor, [NotNull] IGaVectorStorage<T> vector)
        {
            Processor = (IGaProcessor<T>) processor;
            VectorStorage = vector;
        }

        internal GaVector([NotNull] IGaProcessor<T> processor, [NotNull] IGaVectorStorage<T> vector)
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