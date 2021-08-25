using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Factories;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils
{
    public static class GaMultivectorUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Reverse<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.Reverse(mv1.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> GradeInvolution<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.GradeInvolution(mv1.MultivectorStorage)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> CliffordConjugate<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return new GaMultivector<T>(
                processor,
                processor.CliffordConjugate(mv1.MultivectorStorage)
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ScalarPart<T>(this GaMultivector<T> mv1)
        {
            if (!mv1.MultivectorStorage.TryGetTermScalar(0, out var scalar))
                scalar = mv1.Processor.GetZeroScalar();

            return new GaScalar<T>(mv1.Processor, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> VectorPart<T>(this GaMultivector<T> mv1)
        {
            return new GaMultivector<T>(
                mv1.Processor,
                mv1.MultivectorStorage.GetVectorPart()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> BivectorPart<T>(this GaMultivector<T> mv1)
        {
            return new GaMultivector<T>(
                mv1.Processor,
                mv1.MultivectorStorage.GetBivectorPart()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> KVectorPart<T>(this GaMultivector<T> mv1, uint grade)
        {
            return new GaMultivector<T>(
                mv1.Processor,
                mv1.MultivectorStorage.GetKVectorPart(grade)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> OddKVectorsPart<T>(this GaMultivector<T> mv1)
        {
            var composer = 
                mv1.Processor.CreateStorageGradedMultivectorComposer();

            composer.AddTerms(
                mv1.MultivectorStorage
                    .GetGradeIndexScalarRecords()
                    .Where(term => term.Grade.IsOdd())
            );

            return new GaMultivector<T>(
                mv1.Processor,
                composer.CreateStorageGradedMultivector()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EvenKVectorsPart<T>(this GaMultivector<T> mv1)
        {
            var composer = 
                mv1.Processor.CreateStorageGradedMultivectorComposer();

            composer.AddTerms(
                mv1.MultivectorStorage
                    .GetGradeIndexScalarRecords()
                    .Where(term => term.Grade.IsEven())
            );

            return new GaMultivector<T>(
                mv1.Processor,
                composer.CreateStorageGradedMultivector()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Sp<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .Sp(mv1.MultivectorStorage)
                .CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Sp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .Sp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> Norm<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .Norm(mv1.MultivectorStorage)
                .CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> NormSquared<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .NormSquared(mv1.MultivectorStorage)
                .CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ESp<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .ESp(mv1.MultivectorStorage)
                .CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ESp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .ESp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ENorm<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .ENorm(mv1.MultivectorStorage)
                .CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaScalar<T> ENormSquared<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .ENormSquared(mv1.MultivectorStorage)
                .CreateScalar(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Op<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .Op(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Gp<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .Gp(mv1.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Gp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .Gp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> GpReverse<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .GpReverse(mv1.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> GpReverse<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .GpReverse(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Lcp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .Lcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Rcp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .Rcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Fdp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .Fdp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Hip<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .Hip(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Acp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .Acp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Cp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .Cp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> Dual<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .Dual(mv1.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> UnDual<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .UnDual(mv1.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> BladeInverse<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .BladeInverse(mv1.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> VersorInverse<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .VersorInverse(mv1.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EGp<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .EGp(mv1.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EGp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .EGp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EGpReverse<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .EGpReverse(mv1.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EGpReverse<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .EGpReverse(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ELcp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ERcp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EFdp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EHip<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EAcp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> ECp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        {
            var processor = mv1.Processor;

            return processor
                .ELcp(mv1.MultivectorStorage, mv2.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EDual<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .EDual(mv1.MultivectorStorage, mv1.VSpaceDimension)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EUnDual<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .EUnDual(mv1.MultivectorStorage, mv1.VSpaceDimension)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EBladeInverse<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .EBladeInverse(mv1.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivector<T> EVersorInverse<T>(this GaMultivector<T> mv1)
        {
            var processor = mv1.Processor;

            return processor
                .EVersorInverse(mv1.MultivectorStorage)
                .CreateGenericMultivector(processor);
        }
    }
}