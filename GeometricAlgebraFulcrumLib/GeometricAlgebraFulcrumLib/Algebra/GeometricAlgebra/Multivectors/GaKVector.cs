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
    public sealed record GaKVector<T> : 
        IKVectorStorageContainer<T>,
        IGeometricAlgebraElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator KVectorStorage<T>(GaKVector<T> v)
        {
            return v.KVectorStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator -(GaKVector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Negative(v1.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(int v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(uint v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(long v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(ulong v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(float v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(double v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(T v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(Scalar<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(
                    v1.KVectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(LinVector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(int v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(uint v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(long v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(ulong v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(float v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(double v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(T v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(Scalar<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(
                    v1.KVectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(LinVector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2.KVectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(int v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(uint v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(long v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(ulong v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(float v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(double v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(T v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(GaKVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.KVectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator *(Scalar<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaKVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(LinVector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaKVector<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    v2.VectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaVector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage, 
                    v2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaKVector<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    v2.BivectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaBivector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.BivectorStorage, 
                    v2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaKVector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    v2.KVectorStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(int v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(uint v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(long v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(ulong v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(float v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(double v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(T v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(
                    v1, 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(GaKVector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Divide(v1.KVectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> operator /(Scalar<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaKVector<T>(
                processor,
                processor.Times(
                    v1.ScalarValue, 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaKVector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    processor.BladeInverse(v2.VectorStorage.CreateVectorStorage())
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(LinVector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage.CreateVectorStorage(), 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaKVector<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaVector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage, 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaKVector<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaBivector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.BivectorStorage, 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaKVector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }

        
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public KVectorStorage<T> KVectorStorage { get; }

        public uint Grade 
            => KVectorStorage.Grade;

        public Scalar<T> this[int index]
            => GeometricProcessor.CreateScalar(
                GeometricProcessor.GetTermScalarByIndex(KVectorStorage, (ulong) index)
            );
        
        public Scalar<T> this[ulong index]
            => GeometricProcessor
                .GetTermScalarByIndex(KVectorStorage, index)
                .CreateScalar(GeometricProcessor);


        internal GaKVector([NotNull] IScalarAlgebraProcessor<T> processor, [NotNull] KVectorStorage<T> storage)
        {
            GeometricProcessor = (IGeometricAlgebraProcessor<T>) processor;
            KVectorStorage = storage;
        }
        
        internal GaKVector([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] KVectorStorage<T> storage)
        {
            GeometricProcessor = processor;
            KVectorStorage = storage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return GeometricProcessor.IsZero(KVectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero()
        {
            return GeometricProcessor.IsNearZero(KVectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T AsScalarValue()
        {
            return KVectorStorage.Grade == 0
                ? GeometricProcessor.GetScalar(KVectorStorage)
                : throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> AsScalar()
        {
            return KVectorStorage.Grade == 0
                ? Scalar<T>.Create(GeometricProcessor, GeometricProcessor.GetScalar(KVectorStorage))
                : throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VectorStorage<T> AsVectorStorage()
        {
            return KVectorStorage.Grade == 1
                ? KVectorStorage.GetVectorPart()
                : throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> AsVector()
        {
            return KVectorStorage.Grade == 1
                ? new GaVector<T>(GeometricProcessor, KVectorStorage.GetVectorPart())
                : throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BivectorStorage<T> AsBivectorStorage()
        {
            return KVectorStorage.Grade == 2
                ? KVectorStorage.GetBivectorPart()
                : throw new InvalidOperationException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> AsBivector()
        {
            return KVectorStorage.Grade == 2
                ? new GaBivector<T>(GeometricProcessor, KVectorStorage.GetBivectorPart())
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> AsMultivectorStorage()
        {
            return KVectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> AsMultivector()
        {
            return new GaMultivector<T>(GeometricProcessor, KVectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> Conjugate()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.Conjugate(KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> Reverse()
        {
            return Grade.ReverseIsNegativeOfGrade()
                ? new GaKVector<T>(GeometricProcessor, GeometricProcessor.Negative(KVectorStorage))
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> GradeInvolution()
        {
            return Grade.GradeInvolutionIsNegativeOfGrade()
                ? new GaKVector<T>(GeometricProcessor, GeometricProcessor.Negative(KVectorStorage))
                : this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> CliffordConjugate()
        {
            return Grade.CliffordConjugateIsNegativeOfGrade()
                ? new GaKVector<T>(GeometricProcessor, GeometricProcessor.Negative(KVectorStorage))
                : this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> Norm()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.Norm(KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ENorm()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.ENorm(KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> NormSquared()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.NormSquared(KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ENormSquared()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.ENormSquared(KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> DivideByNorm()
        {
            return new GaKVector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByNorm(KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> DivideByENorm()
        {
            return new GaKVector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByENorm(KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> DivideByNormSquared()
        {
            return new GaKVector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByNormSquared(KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> DivideByENormSquared()
        {
            return new GaKVector<T>(
                GeometricProcessor,
                GeometricProcessor.DivideByENormSquared(KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> PseudoInverse()
        {
            return new GaKVector<T>(
                GeometricProcessor,
                GeometricProcessor.BladePseudoInverse(KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> Inverse()
        {
            return new GaKVector<T>(
                GeometricProcessor,
                GeometricProcessor.Divide(KVectorStorage, GeometricProcessor.SpSquared(KVectorStorage))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> EInverse()
        {
            return new GaKVector<T>(
                GeometricProcessor,
                GeometricProcessor.Divide(KVectorStorage, GeometricProcessor.ESp(KVectorStorage))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> Dual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.Dual(KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> EDual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.EDual(KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> UnDual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.UnDual(KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> EUnDual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.EUnDual(KVectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return KVectorStorage.GetScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                KVectorStorage.MapKVectorScalars(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> MapScalarsById(Func<ulong, T, T> scalarMapping)
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                KVectorStorage.MapKVectorScalarsById(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> MapScalarsByGradeIndex(Func<uint, ulong, T, T> scalarMapping)
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                KVectorStorage.MapKVectorScalarsByGradeIndex(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorStorage()
        {
            return KVectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> GetMultivectorStorage()
        {
            return KVectorStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Subspace<T> GetSubspace()
        {
            return Subspace<T>.Create(
                GeometricProcessor,
                this
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Subspace<T> GetDualSubspace()
        {
            return Subspace<T>.Create(
                GeometricProcessor,
                GeometricProcessor.Dual(KVectorStorage).CreateKVector(GeometricProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return KVectorStorage.GetMultivectorText();
        }
    }
}