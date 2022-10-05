using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class KVectorRcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Rcp<T>(this IGeometricAlgebraProcessor<T> processor, ulong id1, ulong id2)
        {
            return processor.Rcp(
                processor.CreateKVectorStorageBasis(id1),
                processor.CreateKVectorStorageBasis(id2)
            ).CreateKVector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ERcp<T>(this IGeometricAlgebraProcessor<T> processor, ulong id1, ulong id2)
        {
            return processor.ERcp(
                processor.CreateKVectorStorageBasis(id1),
                processor.CreateKVectorStorageBasis(id2)
            ).CreateKVector(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, int mv2)
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
        public static GaKVector<T> Rcp<T>(this int mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, uint mv2)
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
        public static GaKVector<T> Rcp<T>(this uint mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, long mv2)
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
        public static GaKVector<T> Rcp<T>(this long mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, ulong mv2)
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
        public static GaKVector<T> Rcp<T>(this ulong mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, float mv2)
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
        public static GaKVector<T> Rcp<T>(this float mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, double mv2)
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
        public static GaKVector<T> Rcp<T>(this double mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, T mv2)
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
        public static GaKVector<T> Rcp<T>(this T mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, Scalar<T> mv2)
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
        public static GaKVector<T> Rcp<T>(this Scalar<T> mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, GaVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Rcp(
                    mv1.KVectorStorage, 
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Rcp<T>(this GaVector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Rcp(
                    mv1.VectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, GaBivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Rcp(
                    mv1.KVectorStorage, 
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Rcp<T>(this GaBivector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Rcp(
                    mv1.BivectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> Rcp<T>(this GaKVector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.Rcp(
                    mv1.KVectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, int mv2)
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
        public static GaKVector<T> ERcp<T>(this int mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, uint mv2)
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
        public static GaKVector<T> ERcp<T>(this uint mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, long mv2)
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
        public static GaKVector<T> ERcp<T>(this long mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, ulong mv2)
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
        public static GaKVector<T> ERcp<T>(this ulong mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, float mv2)
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
        public static GaKVector<T> ERcp<T>(this float mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, double mv2)
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
        public static GaKVector<T> ERcp<T>(this double mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, T mv2)
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
        public static GaKVector<T> ERcp<T>(this T mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, Scalar<T> mv2)
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
        public static GaKVector<T> ERcp<T>(this Scalar<T> mv1, GaKVector<T> mv2)
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
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, GaVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.ERcp(
                    mv1.KVectorStorage, 
                    mv2.VectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ERcp<T>(this GaVector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.ERcp(
                    mv1.VectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, GaBivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.ERcp(
                    mv1.KVectorStorage, 
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ERcp<T>(this GaBivector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.ERcp(
                    mv1.BivectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaKVector<T> ERcp<T>(this GaKVector<T> mv1, GaKVector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return new GaKVector<T>(
                processor, 
                processor.ERcp(
                    mv1.KVectorStorage, 
                    mv2.KVectorStorage
                )
            );
        }
    }
}