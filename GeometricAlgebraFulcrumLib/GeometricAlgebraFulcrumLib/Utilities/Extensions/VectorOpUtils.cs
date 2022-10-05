using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class VectorOpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaVector<T> Op<T>(this GaVector<T> mv1, int mv2)
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
        public static GaVector<T> Op<T>(this int mv1, GaVector<T> mv2)
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
        public static GaVector<T> Op<T>(this GaVector<T> mv1, uint mv2)
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
        public static GaVector<T> Op<T>(this uint mv1, GaVector<T> mv2)
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
        public static GaVector<T> Op<T>(this GaVector<T> mv1, long mv2)
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
        public static GaVector<T> Op<T>(this long mv1, GaVector<T> mv2)
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
        public static GaVector<T> Op<T>(this GaVector<T> mv1, ulong mv2)
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
        public static GaVector<T> Op<T>(this ulong mv1, GaVector<T> mv2)
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
        public static GaVector<T> Op<T>(this GaVector<T> mv1, float mv2)
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
        public static GaVector<T> Op<T>(this float mv1, GaVector<T> mv2)
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
        public static GaVector<T> Op<T>(this GaVector<T> mv1, double mv2)
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
        public static GaVector<T> Op<T>(this double mv1, GaVector<T> mv2)
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
        public static GaVector<T> Op<T>(this GaVector<T> mv1, T mv2)
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
        public static GaVector<T> Op<T>(this T mv1, GaVector<T> mv2)
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
        public static GaVector<T> Op<T>(this GaVector<T> mv1, Scalar<T> mv2)
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
        public static GaVector<T> Op<T>(this Scalar<T> mv1, GaVector<T> mv2)
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
        public static GaKVector<T> Op<T>(this GaVector<T> mv1, GaBivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Op(
                    mv1.VectorStorage, 
                    mv2.BivectorStorage
                )
            );
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static KVector<T> Op<T>(this Bivector<T> mv1, Vector<T> mv2)
        //{
        //    var processor = mv1.GeometricProcessor;

        //    return new KVector<T>(
        //        processor, 
        //        processor.Op(
        //            mv1.BivectorStorage, 
        //            mv2.VectorStorage
        //        )
        //    );
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBivector<T> Op<T>(this GaVector<T> mv1, GaVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaBivector<T>(
                processor, 
                processor.Op(
                    mv1.VectorStorage, 
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Op<T>(this IEnumerable<GaVector<T>> mvList)
        {
            var mvArray = mvList.ToArray();
            var processor = mvArray[0].GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Op(
                    mvArray.Select(mv => mv.VectorStorage)
                )
            );
        }
    }
}