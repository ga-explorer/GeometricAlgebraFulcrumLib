using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorOpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this int v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this uint v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this long v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this ulong v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this float v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this double v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this T v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this Scalar<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Op(
                    v1.MultivectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this LinVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Op(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, GaVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Op(
                    v1.MultivectorStorage, 
                    v2.VectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Op(
                    v1.VectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, GaBivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Op(
                    v1.MultivectorStorage, 
                    v2.BivectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaBivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Op(
                    v1.BivectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, GaKVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Op(
                    v1.MultivectorStorage, 
                    v2.KVectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaKVector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Op(
                    v1.KVectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> v1, GaMultivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new GaMultivector<T>(
                processor,
                processor.Op(
                    v1.MultivectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }
    }
}