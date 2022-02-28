using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors
{
    public sealed record Vector<T> : 
        IVectorStorageContainer<T>,
        IGeometricAlgebraElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator VectorStorage<T>(Vector<T> v)
        {
            return v.VectorStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator -(Vector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Negative(v1.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Vector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(int v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Vector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(uint v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Vector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(long v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Vector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(ulong v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(float v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Vector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Vector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(double v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Vector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(T v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Vector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Scalar<T> v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator +(Vector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Add(
                    v1.VectorStorage.GetLinVectorIndexScalarStorage(), 
                    v2.VectorStorage
                ).CreateVectorStorage()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator +(LinVector<T> v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Add(
                    v1.VectorStorage, 
                    v2.VectorStorage.GetLinVectorIndexScalarStorage()
                ).CreateVectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator +(Vector<T> v1, Vector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Vector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(int v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Vector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(uint v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Vector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(long v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Vector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(ulong v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(float v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Vector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Vector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(double v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Vector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(T v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Vector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Scalar<T> v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator -(Vector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Subtract(
                    v1.VectorStorage.GetLinVectorIndexScalarStorage(), 
                    v2.VectorStorage
                ).CreateVectorStorage()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator -(LinVector<T> v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Subtract(
                    v1.VectorStorage, 
                    v2.VectorStorage.GetLinVectorIndexScalarStorage()
                ).CreateVectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator -(Vector<T> v1, Vector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator *(Vector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator *(int v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator *(Vector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator *(double v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator *(Vector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator *(T v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(v1, v2.VectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator *(Vector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(v1.VectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator *(Scalar<T> v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.VectorStorage)
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator *(Vector<T> v1, Vector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(Vector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(int v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(Vector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(uint v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(Vector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(long v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(Vector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(ulong v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(Vector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(float v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(Vector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(double v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(Vector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Divide(v1.VectorStorage, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(T v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(
                    v1, 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(Vector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Divide(v1.VectorStorage, v2.ScalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> operator /(Scalar<T> v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Vector<T>(
                processor,
                processor.Times(
                    v1.ScalarValue, 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator /(Vector<T> v1, Vector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage, 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public VectorStorage<T> VectorStorage { get; }
        
        public Scalar<T> this[int index]
            => GeometricProcessor
                .GetTermScalarByIndex(VectorStorage, index)
                .CreateScalar(GeometricProcessor);

        public Scalar<T> this[ulong index]
            => GeometricProcessor
                .GetTermScalarByIndex(VectorStorage, index)
                .CreateScalar(GeometricProcessor);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Vector([NotNull] IScalarAlgebraProcessor<T> processor, [NotNull] VectorStorage<T> vector)
        {
            GeometricProcessor = (IGeometricAlgebraProcessor<T>) processor;
            VectorStorage = vector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Vector([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] VectorStorage<T> vector)
        {
            GeometricProcessor = processor;
            VectorStorage = vector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return GeometricProcessor.IsZero(VectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero()
        {
            return GeometricProcessor.IsNearZero(VectorStorage);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> AsKVector()
        {
            return new KVector<T>(GeometricProcessor, VectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> AsMultivector()
        {
            return new Multivector<T>(GeometricProcessor, VectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> Conjugate()
        {
            return new Vector<T>(
                GeometricProcessor, 
                GeometricProcessor.Conjugate(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> Reverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> GradeInvolution()
        {
            return new Vector<T>(
                GeometricProcessor, 
                GeometricProcessor.Negative(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> CliffordConjugate()
        {
            return new Vector<T>(
                GeometricProcessor, 
                GeometricProcessor.Negative(VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> Norm()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.Norm(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ENorm()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.ENorm(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> NormSquared()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.NormSquared(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ENormSquared()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.ENormSquared(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> DivideByNorm()
        {
            return new Vector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByNorm(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> DivideByENorm()
        {
            return new Vector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByENorm(VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> DivideByNormSquared()
        {
            return new Vector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByNormSquared(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> DivideByENormSquared()
        {
            return new Vector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByENormSquared(VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> PseudoInverse()
        {
            return new Vector<T>(
                GeometricProcessor,
                GeometricProcessor.BladePseudoInverse(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> Inverse()
        {
            return new Vector<T>(
                GeometricProcessor,
                GeometricProcessor.Divide(VectorStorage, GeometricProcessor.SpSquared(VectorStorage))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> EInverse()
        {
            return new Vector<T>(
                GeometricProcessor,
                GeometricProcessor.Divide(VectorStorage, GeometricProcessor.ESp(VectorStorage))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> Dual()
        {
            return new KVector<T>(
                GeometricProcessor, 
                GeometricProcessor.Dual(VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> EDual()
        {
            return new KVector<T>(
                GeometricProcessor, 
                GeometricProcessor.EDual(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> UnDual()
        {
            return new KVector<T>(
                GeometricProcessor, 
                GeometricProcessor.UnDual(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> EUnDual()
        {
            return new KVector<T>(
                GeometricProcessor, 
                GeometricProcessor.EUnDual(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return VectorStorage.GetScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new Vector<T>(
                GeometricProcessor, 
                VectorStorage.MapVectorScalars(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> MapScalarsById(Func<ulong, T, T> scalarMapping)
        {
            return new Vector<T>(
                GeometricProcessor, 
                VectorStorage.MapVectorScalarsById(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector<T> MapScalarsByGradeIndex(Func<uint, ulong, T, T> scalarMapping)
        {
            return new Vector<T>(
                GeometricProcessor, 
                VectorStorage.MapVectorScalarsByGradeIndex(scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> GetMultivectorStorage()
        {
            return VectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorStorage()
        {
            return VectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> GetVectorStorage()
        {
            return VectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Subspace<T> GetSubspace()
        {
            return Subspace<T>.Create(
                GeometricProcessor,
                VectorStorage.CreateKVector(GeometricProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Subspace<T> GetDualSubspace()
        {
            return Subspace<T>.Create(
                GeometricProcessor,
                GeometricProcessor.Dual(VectorStorage).CreateKVector(GeometricProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return VectorStorage.GetMultivectorText();
        }
    }
}