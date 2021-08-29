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
    public sealed record GaBivector<T>
        : IGeometricAlgebraElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator -(GaBivector<T> v1)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Negative(v1.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(int v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromInteger(v1), v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(double v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromFloat64(v1), v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(T v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(Scalar<T> v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator +(GaBivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(int v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromInteger(v1), v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(double v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromFloat64(v1), v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(T v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(Scalar<T> v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.BivectorStorage)
            );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator -(GaBivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(int v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(processor.GetScalarFromInteger(v1), v2.BivectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(double v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(processor.GetScalarFromFloat64(v1), v2.BivectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(T v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1, v2.BivectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(Scalar<T> v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.BivectorStorage)
            );

        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromInteger(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromFloat64(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, T v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, v2.ScalarValue)
            );
        }


        public IGaSpace Space 
            => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGaBivectorStorage<T> BivectorStorage { get; }

        public Scalar<T> this[int index]
            => Processor.CreateGaScalarStorage(
                Processor.GetTermScalarByIndex(BivectorStorage, (ulong) index)
            );
        
        public Scalar<T> this[ulong index]
            => Processor.CreateGaScalarStorage(
                Processor.GetTermScalarByIndex(BivectorStorage, index)
            );
        
        public Scalar<T> this[int index1, int index2]
        {
            get
            {
                if (index1 == index2)
                    return Processor.ScalarZero.CreateGaScalarStorage(Processor);

                var id = 
                    GaBasisBivectorUtils.BasisBivectorId(index1, index2);

                return index1 < index2
                    ? Processor.GetTermScalar(BivectorStorage, id).CreateGaScalarStorage(Processor)
                    : Processor.GetTermScalar(Processor.Negative(BivectorStorage), id).CreateGaScalarStorage(Processor);
            }
        }
        
        public Scalar<T> this[ulong index1, ulong index2]
        {
            get
            {
                if (index1 == index2)
                    return Processor.ScalarZero.CreateGaScalarStorage(Processor);

                var id = 
                    GaBasisBivectorUtils.BasisBivectorId(index1, index2);

                return index1 < index2
                    ? Processor.GetTermScalar(BivectorStorage, id).CreateGaScalarStorage(Processor)
                    : Processor.GetTermScalar(Processor.Negative(BivectorStorage), id).CreateGaScalarStorage(Processor);
            }
        }


        internal GaBivector([NotNull] IScalarProcessor<T> processor, [NotNull] IGaBivectorStorage<T> bivector)
        {
            Processor = (IGaProcessor<T>) processor;
            BivectorStorage = bivector;
        }
        
        internal GaBivector([NotNull] IGaProcessor<T> processor, [NotNull] IGaBivectorStorage<T> bivector)
        {
            Processor = processor;
            BivectorStorage = bivector;
        }

        
        public GaBivector<T> Reverse()
        {
            return new GaBivector<T>(
                Processor, 
                Processor.Negative(BivectorStorage)
            );
        }

        public GaBivector<T> GradeInvolution()
        {
            return this;
        }

        public GaBivector<T> CliffordConjugate()
        {
            return new GaBivector<T>(
                Processor, 
                Processor.Negative(BivectorStorage)
            );
        }

        public override string ToString()
        {
            return BivectorStorage.GetMultivectorText();
        }
    }
}