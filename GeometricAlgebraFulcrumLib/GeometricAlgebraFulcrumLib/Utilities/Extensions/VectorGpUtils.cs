using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class VectorGpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this GaVector<T> mv1, int mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this int mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this GaVector<T> mv1, uint mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this uint mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this GaVector<T> mv1, long mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this long mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this GaVector<T> mv1, ulong mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this ulong mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this GaVector<T> mv1, float mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this float mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this GaVector<T> mv1, double mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this double mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this GaVector<T> mv1, T mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    mv2
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this T mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1,
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this GaVector<T> mv1, Scalar<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    mv2.ScalarValue
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Gp<T>(this Scalar<T> mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.ScalarValue,
                    mv2.VectorStorage
                )
            );
        }
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Multivector<T> Gp<T>(this Vector<T> mv1, Bivector<T> mv2)
        //{
        //    var processor = mv1.GeometricProcessor;

        //    return new Multivector<T>(
        //        processor, 
        //        processor.Gp(
        //            mv1.VectorStorage, 
        //            mv2.BivectorStorage
        //        )
        //    );
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Multivector<T> Gp<T>(this Bivector<T> mv1, Vector<T> mv2)
        //{
        //    var processor = mv1.GeometricProcessor;

        //    return new Multivector<T>(
        //        processor, 
        //        processor.Gp(
        //            mv1.BivectorStorage, 
        //            mv2.VectorStorage
        //        )
        //    );
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Gp<T>(this GaVector<T> mv1, GaVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaMultivector<T>(
                processor, 
                processor.Gp(
                    mv1.VectorStorage, 
                    mv2.VectorStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this GaVector<T> mv1, int mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this int mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this GaVector<T> mv1, uint mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this uint mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this GaVector<T> mv1, long mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this long mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this GaVector<T> mv1, ulong mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this ulong mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this GaVector<T> mv1, float mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this float mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this GaVector<T> mv1, double mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this double mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this GaVector<T> mv1, T mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    mv2
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this T mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1,
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this GaVector<T> mv1, Scalar<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.VectorStorage, 
                    mv2.ScalarValue
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> EGp<T>(this Scalar<T> mv1, GaVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaVector<T>(
                processor, 
                processor.Times(
                    mv1.ScalarValue,
                    mv2.VectorStorage
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EGp<T>(this GaVector<T> mv1, GaBivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaMultivector<T>(
                processor, 
                processor.EGp(
                    mv1.VectorStorage, 
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EGp<T>(this GaBivector<T> mv1, GaVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaMultivector<T>(
                processor, 
                processor.EGp(
                    mv1.BivectorStorage, 
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EGp<T>(this GaVector<T> mv1, GaVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaMultivector<T>(
                processor, 
                processor.EGp(
                    mv1.VectorStorage, 
                    mv2.VectorStorage
                )
            );
        }
    }
}