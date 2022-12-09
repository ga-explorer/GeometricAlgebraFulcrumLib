using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors
{
    public sealed record GaMultivector<T> : 
        IMultivectorStorageContainer<T>,
        IGeometricAlgebraElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Negative(v1.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(int v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(uint v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(long v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(ulong v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(float v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(double v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(T v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(Scalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(
                    v1.MultivectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(LinVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaKVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.KVectorStorage, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaMultivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.MultivectorStorage, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(int v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(uint v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(long v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(ulong v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(float v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(double v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(T v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(Scalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(
                    v1.MultivectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(LinVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaKVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2.KVectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaKVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.KVectorStorage, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaMultivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.MultivectorStorage, v2.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(int v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(uint v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(long v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(ulong v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(float v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(double v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(T v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(Scalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(LinVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    v2.VectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    v2.BivectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.BivectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    v2.KVectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaKVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaMultivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(int v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(uint v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(long v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(ulong v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(float v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(double v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(T v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(
                    v1, 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Divide(v1.MultivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(Scalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(
                    v1.ScalarValue, 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    processor.BladeInverse(v2.VectorStorage.CreateVectorStorage())
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(LinVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage.CreateVectorStorage(), 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage, 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.BivectorStorage, 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    processor.BladeInverse(v2.KVectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaKVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaMultivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    processor.BladeInverse(v2.MultivectorStorage)
                )
            );
        }

        
        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public IMultivectorStorage<T> MultivectorStorage { get; }

        public Scalar<T> this[ulong id] =>
            MultivectorStorage.TryGetTermScalar(id, out var scalar)
                ? scalar.CreateScalar(GeometricProcessor)
                : GeometricProcessor.ScalarZero.CreateScalar(GeometricProcessor);

        public Scalar<T> this[uint grade, ulong index] =>
            MultivectorStorage.TryGetTermScalar(grade, index, out var scalar)
                ? scalar.CreateScalar(GeometricProcessor)
                : GeometricProcessor.ScalarZero.CreateScalar(GeometricProcessor);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaMultivector([NotNull] IScalarAlgebraProcessor<T> processor, [NotNull] IMultivectorStorage<T> storage)
        {
            GeometricProcessor = (IGeometricAlgebraProcessor<T>) processor;
            MultivectorStorage = storage;

            Debug.Assert(
                MultivectorStorage.GetStoredBasisVectorsBitPattern() < GeometricProcessor.GaSpaceDimension
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaMultivector([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] IMultivectorStorage<T> storage)
        {
            GeometricProcessor = processor;
            MultivectorStorage = storage;

            Debug.Assert(
                MultivectorStorage.GetStoredBasisVectorsBitPattern() < GeometricProcessor.GaSpaceDimension
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return GeometricProcessor.IsZero(MultivectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero()
        {
            return GeometricProcessor.IsNearZero(MultivectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsScalar()
        {
            return MultivectorStorage.IsScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return MultivectorStorage.GetGrades();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetStoredBasisVectorsBitPattern()
        {
            return MultivectorStorage.GetStoredBasisVectorsBitPattern();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> GetScalarPart()
        {
            return MultivectorStorage.TryGetScalar(out var scalarValue)
                ? GeometricProcessor.CreateScalar(scalarValue)
                : GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaVector<T> GetVectorPart()
        {
            return MultivectorStorage.TryGetVectorPart(out var storage)
                ? GeometricProcessor.CreateVector(storage)
                : GeometricProcessor.CreateVectorZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> GetBivectorPart()
        {
            return MultivectorStorage.TryGetBivectorPart(out var storage)
                ? GeometricProcessor.CreateBivector(storage)
                : GeometricProcessor.CreateBivectorZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple<Scalar<T>, GaBivector<T>> GetScalarBivectorParts()
        {
            var (scalar, bivector) = 
                GeometricProcessor.GetScalarBivectorParts(MultivectorStorage);

            return new Tuple<Scalar<T>, GaBivector<T>>(
                GeometricProcessor.CreateScalar(scalar), 
                GeometricProcessor.CreateBivector(bivector)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> GetKVectorPart(uint grade)
        {
            return MultivectorStorage.TryGetKVectorPart(grade, out var storage)
                ? GeometricProcessor.CreateKVector(storage)
                : GeometricProcessor.CreateKVectorZero(grade);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> GetEvenPart()
        {
            var storage = MultivectorStorage.GetMultivectorPart(
                (grade, _, _) => grade.IsEven()
            );

            return new GaMultivector<T>(GeometricProcessor, storage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> GetEvenPart(uint maxGrade)
        {
            var storage = MultivectorStorage.GetMultivectorPart(
                (grade, _, _) => grade <= maxGrade && grade.IsEven()
            );

            return new GaMultivector<T>(GeometricProcessor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> GetOddPart()
        {
            var storage = MultivectorStorage.GetMultivectorPart(
                (grade, _, _) => grade.IsOdd()
            );

            return new GaMultivector<T>(GeometricProcessor, storage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> GetOddPart(uint maxGrade)
        {
            var storage = MultivectorStorage.GetMultivectorPart(
                (grade, _, _) => grade <= maxGrade && grade.IsOdd()
            );

            return new GaMultivector<T>(GeometricProcessor, storage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple<GaMultivector<T>, GaMultivector<T>> GetEvenOddParts()
        {
            var (mv1, mv2) = 
                MultivectorStorage.SplitEvenOddParts();

            return new Tuple<GaMultivector<T>, GaMultivector<T>>(
                mv1.CreateMultivector(GeometricProcessor),
                mv2.CreateMultivector(GeometricProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Conjugate()
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                GeometricProcessor.Conjugate(MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Reverse()
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                GeometricProcessor.Reverse(MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> GradeInvolution()
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                GeometricProcessor.GradeInvolution(MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> CliffordConjugate()
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                GeometricProcessor.CliffordConjugate(MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> Norm()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.Norm(MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ENorm()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.ENorm(MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> NormSquared()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.NormSquared(MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ENormSquared()
        {
            return Scalar<T>.Create(
                GeometricProcessor,
                GeometricProcessor.ENormSquared(MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> PseudoInverse()
        {
            return new GaMultivector<T>(
                GeometricProcessor,
                GeometricProcessor.BladePseudoInverse(MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Inverse()
        {
            return new GaMultivector<T>(
                GeometricProcessor,
                GeometricProcessor.VersorInverse(MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> EInverse()
        {
            return new GaMultivector<T>(
                GeometricProcessor,
                GeometricProcessor.EVersorInverse(MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> Dual()
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                GeometricProcessor.Dual(MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> EDual()
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                GeometricProcessor.EDual(MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> UnDual()
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                GeometricProcessor.UnDual(MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> EUnDual()
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                GeometricProcessor.EUnDual(MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return MultivectorStorage.GetScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                MultivectorStorage.MapScalars(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapScalarsById(Func<ulong, T, T> scalarMapping)
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                MultivectorStorage.MapScalarsById(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> MapScalarsByGradeIndex(Func<uint, ulong, T, T> scalarMapping)
        {
            return new GaMultivector<T>(
                GeometricProcessor, 
                MultivectorStorage.MapScalarsByGradeIndex(scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> GetMultivectorStorage()
        {
            return MultivectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return MultivectorStorage.GetMultivectorText();
        }
    }
}