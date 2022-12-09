using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors
{
    public sealed record GaBivector<T> : 
        IKVectorStorageContainer<T>,
        IGeometricAlgebraElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator BivectorStorage<T>(GaBivector<T> v)
        {
            return v.BivectorStorage;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator -(GaBivector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Negative(v1.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(int v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(uint v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(long v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(ulong v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(float v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(double v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(T v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(Scalar<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(GaBivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(
                    v1.BivectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator +(LinVector<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Add(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator +(GaBivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(int v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(uint v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(long v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(ulong v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(float v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(double v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(T v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(Scalar<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(GaBivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(
                    v1.BivectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator -(LinVector<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Subtract(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator -(GaBivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(int v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(uint v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(long v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(ulong v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(float v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(double v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(T v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1, v2.BivectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(GaBivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator *(Scalar<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.BivectorStorage)
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(v1.VectorStorage, v2.BivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaBivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(v1.BivectorStorage, v2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator *(GaBivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(v1.BivectorStorage, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(int v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(uint v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(long v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(ulong v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(float v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(double v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(T v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(
                    v1, 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(GaBivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> operator /(Scalar<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaBivector<T>(
                processor,
                processor.Times(
                    v1.ScalarValue, 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaVector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage, 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaBivector<T> v1, GaVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.BivectorStorage, 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> operator /(GaBivector<T> v1, GaBivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Gp(
                    v1.BivectorStorage, 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );
        }


        public IScalarAlgebraProcessor<T> ScalarProcessor 
            => GeometricProcessor;

        public ILinearAlgebraProcessor<T> LinearProcessor 
            => GeometricProcessor;

        public IGeometricAlgebraProcessor<T> GeometricProcessor { get; }

        public BivectorStorage<T> BivectorStorage { get; }

        public Scalar<T> this[int index]
            => GeometricProcessor.CreateScalar(
                GeometricProcessor.GetTermScalarByIndex(BivectorStorage, (ulong) index)
            );
        
        public Scalar<T> this[ulong index]
            => GeometricProcessor.CreateScalar(
                GeometricProcessor.GetTermScalarByIndex(BivectorStorage, index)
            );
        
        public Scalar<T> this[int index1, int index2]
        {
            get
            {
                if (index1 == index2)
                    return GeometricProcessor.ScalarZero.CreateScalar(GeometricProcessor);

                var id = 
                    BasisBivectorUtils.BasisVectorIndicesToBivectorId(index1, index2);

                return index1 < index2
                    ? GeometricProcessor.GetTermScalar(BivectorStorage, id).CreateScalar(GeometricProcessor)
                    : GeometricProcessor.GetTermScalar(GeometricProcessor.Negative(BivectorStorage), id).CreateScalar(GeometricProcessor);
            }
        }
        
        public Scalar<T> this[ulong index1, ulong index2]
        {
            get
            {
                if (index1 == index2)
                    return GeometricProcessor.ScalarZero.CreateScalar(GeometricProcessor);

                var id = 
                    BasisBivectorUtils.BasisVectorIndicesToBivectorId(index1, index2);

                return index1 < index2
                    ? GeometricProcessor.GetTermScalar(BivectorStorage, id).CreateScalar(GeometricProcessor)
                    : GeometricProcessor.GetTermScalar(GeometricProcessor.Negative(BivectorStorage), id).CreateScalar(GeometricProcessor);
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaBivector([System.Diagnostics.CodeAnalysis.NotNull] IScalarAlgebraProcessor<T> processor, [System.Diagnostics.CodeAnalysis.NotNull] BivectorStorage<T> bivector)
        {
            GeometricProcessor = (IGeometricAlgebraProcessor<T>) processor;
            BivectorStorage = bivector;

            Debug.Assert(
                BivectorStorage.GetStoredBasisVectorsBitPattern() < GeometricProcessor.GaSpaceDimension
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal GaBivector([System.Diagnostics.CodeAnalysis.NotNull] IGeometricAlgebraProcessor<T> processor, [System.Diagnostics.CodeAnalysis.NotNull] BivectorStorage<T> bivector)
        {
            GeometricProcessor = processor;
            BivectorStorage = bivector;

            Debug.Assert(
                BivectorStorage.GetStoredBasisVectorsBitPattern() < GeometricProcessor.GaSpaceDimension
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return GeometricProcessor.IsZero(BivectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero()
        {
            return GeometricProcessor.IsNearZero(BivectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetStoredBasisVectorsBitPattern()
        {
            return BivectorStorage.GetStoredBasisVectorsBitPattern();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> AsKVector()
        {
            return new GaKVector<T>(GeometricProcessor, BivectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaMultivector<T> AsMultivector()
        {
            return new GaMultivector<T>(GeometricProcessor, BivectorStorage);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> Conjugate()
        {
            return new GaBivector<T>(
                GeometricProcessor, 
                GeometricProcessor.Conjugate(BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> Reverse()
        {
            return new GaBivector<T>(
                GeometricProcessor, 
                GeometricProcessor.Negative(BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> GradeInvolution()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> CliffordConjugate()
        {
            return new GaBivector<T>(
                GeometricProcessor, 
                GeometricProcessor.Negative(BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> Norm()
        {
            return GeometricProcessor
                .Norm(BivectorStorage)
                .CreateScalar(GeometricProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ENorm()
        {
            return GeometricProcessor
                .ENorm(BivectorStorage)
                .CreateScalar(GeometricProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> NormSquared()
        {
            return GeometricProcessor
                .NormSquared(BivectorStorage)
                .CreateScalar(GeometricProcessor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Scalar<T> ENormSquared()
        {
            return GeometricProcessor
                .ENormSquared(BivectorStorage)
                .CreateScalar(GeometricProcessor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> PseudoInverse()
        {
            return new GaBivector<T>(
                GeometricProcessor,
                GeometricProcessor.BladePseudoInverse(BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> Inverse()
        {
            return new GaBivector<T>(
                GeometricProcessor,
                GeometricProcessor.Divide(BivectorStorage, GeometricProcessor.SpSquared(BivectorStorage))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> EInverse()
        {
            return new GaBivector<T>(
                GeometricProcessor,
                GeometricProcessor.Divide(BivectorStorage, GeometricProcessor.ESp(BivectorStorage))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> Dual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.Dual(BivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> EDual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.EDual(BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> UnDual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.UnDual(BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaKVector<T> EUnDual()
        {
            return new GaKVector<T>(
                GeometricProcessor, 
                GeometricProcessor.EUnDual(BivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetScalars()
        {
            return BivectorStorage.GetScalars();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new GaBivector<T>(
                GeometricProcessor, 
                BivectorStorage.MapBivectorScalars(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> MapScalarsById(Func<ulong, T, T> scalarMapping)
        {
            return new GaBivector<T>(
                GeometricProcessor, 
                BivectorStorage.MapBivectorScalarsById(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaBivector<T> MapScalarsByGradeIndex(Func<uint, ulong, T, T> scalarMapping)
        {
            return new GaBivector<T>(
                GeometricProcessor, 
                BivectorStorage.MapBivectorScalarsByGradeIndex(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVectorStorage<T> GetKVectorStorage()
        {
            return BivectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> GetMultivectorStorage()
        {
            return BivectorStorage;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Subspace<T> GetSubspace()
        {
            return Subspace<T>.Create(
                GeometricProcessor,
                AsKVector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Subspace<T> GetDualSubspace()
        {
            return Subspace<T>.Create(
                GeometricProcessor,
                GeometricProcessor.Dual(BivectorStorage).CreateKVector(GeometricProcessor)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<GaVector<T>> GetVectorBasis()
        {
            var closestVector = GeometricProcessor.CreateVectorBasis(0);

            return GetVectorBasis(closestVector);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<GaVector<T>> GetVectorBasis(int closestBasisVectorIndex)
        {
            var closestVector = GeometricProcessor.CreateVectorBasis(closestBasisVectorIndex);

            return GetVectorBasis(closestVector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Pair<GaVector<T>> GetVectorBasis(GaVector<T> closestVector)
        {
            var e1 = closestVector.Lcp(this).DivideByNorm();
            var e2 = e1.Lcp(this);

            Debug.Assert((e1.Op(e2) - this).IsNearZero());

            return new Pair<GaVector<T>>(e1, e2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return BivectorStorage.GetMultivectorText();
        }
    }
}