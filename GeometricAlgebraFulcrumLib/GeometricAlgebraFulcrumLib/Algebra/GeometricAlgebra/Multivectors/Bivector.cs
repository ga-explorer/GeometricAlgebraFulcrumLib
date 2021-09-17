using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors
{
    public sealed record Bivector<T> : 
        IMultivectorStorageContainer<T>,
        IGeometricAlgebraElement<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator -(Bivector<T> v1)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Negative(v1.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Bivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(int v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Bivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(uint v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Bivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(long v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Bivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(ulong v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Bivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(float v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Bivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(double v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Bivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(T v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Bivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Scalar<T> v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.ScalarValue, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Bivector<T> v1, Vector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Vector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(v1.VectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(Bivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(
                    v1.BivectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator +(LinVector<T> v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Add(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator +(Bivector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Add(v1.BivectorStorage, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Bivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(int v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Bivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(uint v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Bivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(long v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Bivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(ulong v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Bivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(float v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Bivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(double v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Bivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(T v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Bivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Scalar<T> v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.ScalarValue, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Bivector<T> v1, Vector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.VectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Vector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(v1.VectorStorage, v2.BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(Bivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(
                    v1.BivectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator -(LinVector<T> v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Subtract(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator -(Bivector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Subtract(v1.BivectorStorage, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(Bivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(int v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(Bivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(uint v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(Bivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(long v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(Bivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(ulong v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(Bivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(float v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(Bivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(double v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.BivectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(Bivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(T v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(v1, v2.BivectorStorage)
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(Bivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator *(Scalar<T> v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.BivectorStorage)
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator *(Vector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(v1.VectorStorage, v2.BivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator *(Bivector<T> v1, Vector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(v1.BivectorStorage, v2.VectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator *(Bivector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(v1.BivectorStorage, v2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(Bivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(int v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(Bivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(uint v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(Bivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(long v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(Bivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(ulong v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(Bivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(float v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(Bivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(double v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(
                    processor.GetScalarFromNumber(v1), 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(Bivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(T v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(
                    v1, 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(Bivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Divide(v1.BivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> operator /(Scalar<T> v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Bivector<T>(
                processor,
                processor.Times(
                    v1.ScalarValue, 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator /(Vector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage, 
                    processor.BladeInverse(v2.BivectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator /(Bivector<T> v1, Vector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.BivectorStorage, 
                    processor.BladeInverse(v2.VectorStorage)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> operator /(Bivector<T> v1, Bivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
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


        internal Bivector([NotNull] IScalarAlgebraProcessor<T> processor, [NotNull] BivectorStorage<T> bivector)
        {
            GeometricProcessor = (IGeometricAlgebraProcessor<T>) processor;
            BivectorStorage = bivector;
        }
        
        internal Bivector([NotNull] IGeometricAlgebraProcessor<T> processor, [NotNull] BivectorStorage<T> bivector)
        {
            GeometricProcessor = processor;
            BivectorStorage = bivector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> AsBivector()
        {
            return new Bivector<T>(GeometricProcessor, BivectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Multivector<T> AsMultivector()
        {
            return new Multivector<T>(GeometricProcessor, BivectorStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> Reverse()
        {
            return new Bivector<T>(
                GeometricProcessor, 
                GeometricProcessor.Negative(BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> GradeInvolution()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> CliffordConjugate()
        {
            return new Bivector<T>(
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
        public Bivector<T> Inverse()
        {
            return new Bivector<T>(
                GeometricProcessor,
                GeometricProcessor.Divide(BivectorStorage, GeometricProcessor.Sp(BivectorStorage))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> EInverse()
        {
            return new Bivector<T>(
                GeometricProcessor,
                GeometricProcessor.Divide(BivectorStorage, GeometricProcessor.ESp(BivectorStorage))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> Dual()
        {
            return new KVector<T>(
                GeometricProcessor, 
                GeometricProcessor.Dual(BivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> EDual()
        {
            return new KVector<T>(
                GeometricProcessor, 
                GeometricProcessor.EDual(BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> UnDual()
        {
            return new KVector<T>(
                GeometricProcessor, 
                GeometricProcessor.UnDual(BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public KVector<T> EUnDual()
        {
            return new KVector<T>(
                GeometricProcessor, 
                GeometricProcessor.EUnDual(BivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> MapScalars(Func<T, T> scalarMapping)
        {
            return new Bivector<T>(
                GeometricProcessor, 
                BivectorStorage.MapBivectorScalars(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> MapScalarsById(Func<ulong, T, T> scalarMapping)
        {
            return new Bivector<T>(
                GeometricProcessor, 
                BivectorStorage.MapBivectorScalarsById(scalarMapping)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Bivector<T> MapScalarsByGradeIndex(Func<uint, ulong, T, T> scalarMapping)
        {
            return new Bivector<T>(
                GeometricProcessor, 
                BivectorStorage.MapBivectorScalarsByGradeIndex(scalarMapping)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IMultivectorStorage<T> GetMultivectorStorage()
        {
            return BivectorStorage;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return BivectorStorage.GetMultivectorText();
        }
    }
}