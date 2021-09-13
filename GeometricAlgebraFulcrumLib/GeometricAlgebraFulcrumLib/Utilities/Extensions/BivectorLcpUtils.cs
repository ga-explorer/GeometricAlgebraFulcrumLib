using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class BivectorLcpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this Bivector<T> mv1, int mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> Lcp<T>(this int mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this Bivector<T> mv1, uint mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> Lcp<T>(this uint mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this Bivector<T> mv1, long mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> Lcp<T>(this long mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this Bivector<T> mv1, ulong mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> Lcp<T>(this ulong mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this Bivector<T> mv1, float mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> Lcp<T>(this float mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this Bivector<T> mv1, double mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> Lcp<T>(this double mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this Bivector<T> mv1, T mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> Lcp<T>(this T mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    mv1,
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this Bivector<T> mv1, Scalar<T> mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> Lcp<T>(this Scalar<T> mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    mv1.ScalarValue,
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this Bivector<T> mv1, Vector<T> mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> Lcp<T>(this Vector<T> mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Vector<T>(
                processor, 
                processor.Lcp(
                    mv1.VectorStorage, 
                    mv2.BivectorStorage
                ).GetVectorPart()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> Lcp<T>(this Bivector<T> mv1, Bivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor.CreateScalar(
                processor.Sp(mv1.BivectorStorage, mv2.BivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this Bivector<T> mv1, int mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> ELcp<T>(this int mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this Bivector<T> mv1, uint mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> ELcp<T>(this uint mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this Bivector<T> mv1, long mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> ELcp<T>(this long mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this Bivector<T> mv1, ulong mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> ELcp<T>(this ulong mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this Bivector<T> mv1, float mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> ELcp<T>(this float mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this Bivector<T> mv1, double mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> ELcp<T>(this double mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    processor.GetScalarFromNumber(mv1),
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this Bivector<T> mv1, T mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> ELcp<T>(this T mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    mv1,
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this Bivector<T> mv1, Scalar<T> mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Bivector<T> ELcp<T>(this Scalar<T> mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Bivector<T>(
                processor, 
                processor.Times(
                    mv1.ScalarValue,
                    mv2.BivectorStorage
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this Bivector<T> mv1, Vector<T> mv2)
        {
            return mv1.GeometricProcessor.CreateScalarZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector<T> ELcp<T>(this Vector<T> mv1, Bivector<T> mv2)
        {
            var processor = mv2.GeometricProcessor;

            return new Vector<T>(
                processor, 
                processor.ELcp(
                    mv1.VectorStorage, 
                    mv2.BivectorStorage
                ).GetVectorPart()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Scalar<T> ELcp<T>(this Bivector<T> mv1, Bivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor.CreateScalar(
                processor.ESp(mv1.BivectorStorage, mv2.BivectorStorage)
            );
        }
    }
}