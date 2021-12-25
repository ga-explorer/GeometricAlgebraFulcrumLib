using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorGpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this int v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this uint v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this long v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this ulong v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this float v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this double v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this T v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Scalar<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this LinVector<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    v2.VectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Vector<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.VectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    v2.BivectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Bivector<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.BivectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, KVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    v2.KVectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this KVector<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.KVectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> v1, Multivector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Gp(
                    v1.MultivectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Multivector<T> mv1)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Gp(mv1.MultivectorStorage)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> GpReverse<T>(this Multivector<T> mv1)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .GpReverse(mv1.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> GpReverse<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .GpReverse(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, int v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this int v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, uint v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this uint v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this long v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, long v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, ulong v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this ulong v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, float v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this float v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, double v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, processor.GetScalarFromNumber(v2))
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this double v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(processor.GetScalarFromNumber(v1), v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, T v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this T v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1, v2.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, Scalar<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.MultivectorStorage, v2.ScalarValue)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Scalar<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.Times(v1.ScalarValue, v2.MultivectorStorage)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, LinVector<T> v2)
        {
            var processor = v1.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.EGp(
                    v1.MultivectorStorage, 
                    v2.VectorStorage.CreateVectorStorage()
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this LinVector<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.EGp(
                    v1.VectorStorage.CreateVectorStorage(), 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, Vector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.EGp(
                    v1.MultivectorStorage, 
                    v2.VectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Vector<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.EGp(
                    v1.VectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, Bivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.EGp(
                    v1.MultivectorStorage, 
                    v2.BivectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Bivector<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.EGp(
                    v1.BivectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, KVector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.EGp(
                    v1.MultivectorStorage, 
                    v2.KVectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this KVector<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.EGp(
                    v1.KVectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> v1, Multivector<T> v2)
        {
            var processor = v2.GeometricProcessor;

            return new Multivector<T>(
                processor,
                processor.EGp(
                    v1.MultivectorStorage, 
                    v2.MultivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGp<T>(this Multivector<T> mv1)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .EGp(mv1.MultivectorStorage)
                .CreateMultivector(processor);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGpReverse<T>(this Multivector<T> mv1)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .EGpReverse(mv1.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EGpReverse<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .EGpReverse(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }
    }
}