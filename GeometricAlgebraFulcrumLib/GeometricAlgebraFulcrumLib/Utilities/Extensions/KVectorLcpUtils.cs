using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class KVectorLcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this IGeometricAlgebraProcessor<T> processor, ulong id1, ulong id2)
        {
            return processor.Lcp(
                processor.CreateKVectorStorageBasis(id1),
                processor.CreateKVectorStorageBasis(id2)
            ).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this IGeometricAlgebraProcessor<T> processor, ulong id1, ulong id2)
        {
            return processor.ELcp(
                processor.CreateKVectorStorageBasis(id1),
                processor.CreateKVectorStorageBasis(id2)
            ).CreateKVector(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, int mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this int mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, uint mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this uint mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, long mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this long mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, ulong mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this ulong mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, float mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this float mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, double mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this double mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, T mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    mv2
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this T mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1,
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, Scalar<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    mv2.ScalarValue
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this Scalar<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.ScalarValue,
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, GaVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Lcp(
                    mv1.KVectorStorage, 
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaVector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Lcp(
                    mv1.VectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, GaBivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Lcp(
                    mv1.KVectorStorage, 
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaBivector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Lcp(
                    mv1.BivectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Lcp<T>(this GaKVector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Lcp(
                    mv1.KVectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, int mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this int mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, uint mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this uint mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, long mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this long mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, ulong mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this ulong mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, float mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this float mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, double mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    processor.GetScalarFromNumber(mv2)
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this double mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, T mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    mv2
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this T mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1,
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, Scalar<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.KVectorStorage, 
                    mv2.ScalarValue
                )
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this Scalar<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Times(
                    mv1.ScalarValue,
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, GaVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.ELcp(
                    mv1.KVectorStorage, 
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaVector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.ELcp(
                    mv1.VectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, GaBivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.ELcp(
                    mv1.KVectorStorage, 
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaBivector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.ELcp(
                    mv1.BivectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ELcp<T>(this GaKVector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.ELcp(
                    mv1.KVectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }
    }
}