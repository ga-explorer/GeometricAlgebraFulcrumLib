using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorUtils
    {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Lcp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Lcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Rcp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Rcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Fdp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Fdp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Hip<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Hip(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Acp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Acp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> Cp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Cp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> ELcp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> ERcp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EFdp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EHip<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> EAcp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Multivector<T> ECp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }
        
        
    }
}