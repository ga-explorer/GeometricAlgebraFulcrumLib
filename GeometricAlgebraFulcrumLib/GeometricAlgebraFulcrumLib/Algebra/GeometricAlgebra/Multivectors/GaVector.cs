using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors
{
    public sealed record GaVector<T> : 
        IVectorStorageContainer<T>,
        IGeometricAlgebraElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator VectorStorage<T>(GaVector<T> v)
        {
            return v.VectorStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator -(GaVector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Negative(v1.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(int v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(uint v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(long v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(ulong v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(float v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(double v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(T v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(Scalar<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator +(GaVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Add(
                    v1.VectorStorage.GetLinVectorIndexScalarStorage(), 
                    v2.VectorStorage
                ).CreateVectorStorage()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator +(LinVector<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Add(
                    v1.VectorStorage, 
                    v2.VectorStorage.GetLinVectorIndexScalarStorage()
                ).CreateVectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator +(GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(int v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(uint v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(long v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(ulong v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(float v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(double v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(T v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(Scalar<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator -(GaVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Subtract(
                    v1.VectorStorage.GetLinVectorIndexScalarStorage(), 
                    v2.VectorStorage
                ).CreateVectorStorage()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator -(LinVector<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Subtract(
                    v1.VectorStorage, 
                    v2.VectorStorage.GetLinVectorIndexScalarStorage()
                ).CreateVectorStorage()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator -(GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(int v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(double v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.VectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(T v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(v1, v2.VectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(GaVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.VectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator *(Scalar<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.VectorStorage)
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(v1.VectorStorage, v2.VectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(int v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(uint v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(long v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(ulong v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(float v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(double v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, v2)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(T v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    v1, 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(GaVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Divide(v1.VectorStorage, v2.ScalarValue)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> operator /(Scalar<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaVector<T>(
                processor,
                processor.Times(
                    v1.ScalarValue, 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
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
        internal GaVector([NotNull] IScalarAlgebraProcessor<T> processor, [NotNull] VectorStorage<T> vector)
        {
            GeometricProcessor = (IGeometricAlgebraProcessor<T>) processor;
            VectorStorage = vector;

            Debug.Assert(
                VectorStorage.GetStoredBasisVectorsBitPattern() < GeometricProcessor.GaSpaceDimension
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaVector([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] VectorStorage<T> vector)
        {
            GeometricProcessor = processor;
            VectorStorage = vector;

            Debug.Assert(
                VectorStorage.GetStoredBasisVectorsBitPattern() < GeometricProcessor.GaSpaceDimension
            );
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
        public ulong GetStoredBasisVectorsBitPattern()
        {
            return VectorStorage.GetStoredBasisVectorsBitPattern();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> AsKVector()
        {
            return new GaKVector<T>(GeometricProcessor, VectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> AsMultivector()
        {
            return new GaMultivector<T>(GeometricProcessor, VectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> Conjugate()
        {
            return new GaVector<T>(
                GeometricProcessor, 
                GeometricProcessor.Conjugate(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> Reverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GradeInvolution()
        {
            return new GaVector<T>(
                GeometricProcessor, 
                GeometricProcessor.Negative(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> CliffordConjugate()
        {
            return new GaVector<T>(
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
        public GaVector<T> DivideByNorm()
        {
            return new GaVector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByNorm(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> DivideByENorm()
        {
            return new GaVector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByENorm(VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> DivideByNormSquared()
        {
            return new GaVector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByNormSquared(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> DivideByENormSquared()
        {
            return new GaVector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByENormSquared(VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> PseudoInverse()
        {
            return new GaVector<T>(
                GeometricProcessor,
                GeometricProcessor.BladePseudoInverse(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> Inverse()
        {
            return new GaVector<T>(
                GeometricProcessor,
                GeometricProcessor.Divide(VectorStorage, GeometricProcessor.SpSquared(VectorStorage))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> EInverse()
        {
            return new GaVector<T>(
                GeometricProcessor,
                GeometricProcessor.Divide(VectorStorage, GeometricProcessor.ESp(VectorStorage))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> Dual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.Dual(VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> EDual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.EDual(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> UnDual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.UnDual(VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> EUnDual()
        {
            return new GaKVector<T>(
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
        public GaVector<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new GaVector<T>(
                GeometricProcessor, 
                VectorStorage.MapVectorScalars(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> MapScalarsById(Func<ulong, T, T> scalarMapping)
        {
            return new GaVector<T>(
                GeometricProcessor, 
                VectorStorage.MapVectorScalarsById(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> MapScalarsByGradeIndex(Func<uint, ulong, T, T> scalarMapping)
        {
            return new GaVector<T>(
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