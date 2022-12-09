using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors
{
    public static class GaMultivectorUtils
    {

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Multivector<T> Lcp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    var processor = mv1.GeometricProcessor;

        //    return processor
        //        .Lcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
        //        .CreateMultivector(processor);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Multivector<T> Rcp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    var processor = mv1.GeometricProcessor;

        //    return processor
        //        .Rcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
        //        .CreateMultivector(processor);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Fdp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Fdp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Hip<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Hip(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Acp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Acp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Cp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .Cp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }



        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Multivector<T> ELcp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    var processor = mv1.GeometricProcessor;

        //    return processor
        //        .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
        //        .CreateMultivector(processor);
        //}

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Multivector<T> ERcp<T>(this Multivector<T> mv1, Multivector<T> mv2)
        //{
        //    var processor = mv1.GeometricProcessor;

        //    return processor
        //        .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
        //        .CreateMultivector(processor);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EFdp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EHip<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EAcp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ECp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.GeometricProcessor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateMultivector(processor);
        }


    }
}