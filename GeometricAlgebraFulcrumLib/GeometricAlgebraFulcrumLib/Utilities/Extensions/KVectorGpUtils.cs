﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class KVectorGpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this KVector<T> mv1, int mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this int mv1, KVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this KVector<T> mv1, uint mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this uint mv1, KVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this KVector<T> mv1, long mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this long mv1, KVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this KVector<T> mv1, ulong mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this ulong mv1, KVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this KVector<T> mv1, float mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this float mv1, KVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this KVector<T> mv1, double mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this double mv1, KVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this KVector<T> mv1, T mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    mv2
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this T mv1, KVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    mv1,
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this KVector<T> mv1, Scalar<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    mv2.ScalarValue
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVector<T> Gp<T>(this Scalar<T> mv1, KVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new KVector<T>(
                processor, 
                processor.Times(
                    mv1.ScalarValue,
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this KVector<T> mv1, Vector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new Multivector<T>(
                processor, 
                processor.Gp(
                    mv1.KVectorStorage, 
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Vector<T> mv1, KVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Multivector<T>(
                processor, 
                processor.Gp(
                    mv1.VectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this KVector<T> mv1, Bivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new Multivector<T>(
                processor, 
                processor.Gp(
                    mv1.KVectorStorage, 
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this Bivector<T> mv1, KVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new Multivector<T>(
                processor, 
                processor.Gp(
                    mv1.BivectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Gp<T>(this KVector<T> mv1, KVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new Multivector<T>(
                processor, 
                processor.Gp(
                    mv1.KVectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }
    }
}