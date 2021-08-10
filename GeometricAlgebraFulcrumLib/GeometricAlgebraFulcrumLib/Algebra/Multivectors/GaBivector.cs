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
    public sealed record GaBivector<T>
        : IGaAlgebraElement<T>
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
                processor.Add(v1.BivectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(int v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.IntegerToScalar(v1), v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(double v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.Float64ToScalar(v1), v2.BivectorStorage)
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
        public static GaMultivector<T> operator +(GaBivector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaScalar<T> v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.Scalar, v2.BivectorStorage)
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
                processor.Subtract(v1.BivectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(int v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.IntegerToScalar(v1), v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(double v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.Float64ToScalar(v1), v2.BivectorStorage)
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
        public static GaMultivector<T> operator -(GaBivector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaScalar<T> v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.Scalar, v2.BivectorStorage)
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
                processor.Times(v1.BivectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(int v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(processor.IntegerToScalar(v1), v2.BivectorStorage)
            );

        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.Float64ToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(double v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(processor.Float64ToScalar(v1), v2.BivectorStorage)
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
        public static GaBivector<T> operator *(GaBivector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, v2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaScalar<T> v1, GaBivector<T> v2)
        {
            var processor = v2.Processor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.Scalar, v2.BivectorStorage)
            );

        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, int v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.IntegerToScalar(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, double v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.Float64ToScalar(v2))
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
        public static GaBivector<T> operator /(GaBivector<T> v1, GaScalar<T> v2)
        {
            var processor = v1.Processor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, v2.Scalar)
            );
        }


        public IGaSpace Space 
            => Processor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => Processor.GaSpaceDimension;

        public IGaProcessor<T> Processor { get; }

        public IGaStorageBivector<T> BivectorStorage { get; }

        public GaScalar<T> this[int index]
            => Processor.CreateScalar(
                Processor.GetTermScalarByIndex(BivectorStorage, (ulong) index)
            );
        
        public GaScalar<T> this[ulong index]
            => Processor.CreateScalar(
                Processor.GetTermScalarByIndex(BivectorStorage, index)
            );
        
        public GaScalar<T> this[int index1, int index2]
        {
            get
            {
                if (index1 == index2)
                    return Processor.ZeroScalar.CreateScalar(Processor);

                var id = 
                    (1UL << index1) | (1UL << index2);

                return index1 < index2
                    ? Processor.GetTermScalar(BivectorStorage, id).CreateScalar(Processor)
                    : Processor.GetTermScalar(Processor.Negative(BivectorStorage), id).CreateScalar(Processor);
            }
        }
        
        public GaScalar<T> this[ulong index1, ulong index2]
        {
            get
            {
                if (index1 == index2)
                    return Processor.ZeroScalar.CreateScalar(Processor);

                var id = 
                    (1UL << (int) index1) | (1UL << (int) index2);

                return index1 < index2
                    ? Processor.GetTermScalar(BivectorStorage, id).CreateScalar(Processor)
                    : Processor.GetTermScalar(Processor.Negative(BivectorStorage), id).CreateScalar(Processor);
            }
        }


        internal GaBivector([NotNull] IGaProcessor<T> processor, [NotNull] IGaStorageBivector<T> bivector)
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