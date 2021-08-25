using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Binary;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary
{
    public static class GaProcessorAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv, T scalar2)
        {
            var scalar = mv.TryGetScalar(out var scalar1)
                ? scalarProcessor.Add(scalar1, scalar2)
                : scalar2;

            return scalar.CreateStorageScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar1, IGaStorageScalar<T> mv)
        {
            var scalar = mv.TryGetScalar(out var scalar2)
                ? scalarProcessor.Add(scalar1, scalar2)
                : scalar1;

            return scalar.CreateStorageScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv1, IGaStorageScalar<T> mv2)
        {
            if (mv1.TryGetScalar(out var scalar1))
                return mv2.TryGetScalar(out var scalar2)
                    ? scalarProcessor.Add(scalar1, scalar2).CreateStorageScalar()
                    : mv1;

            return mv2.IsEmpty()
                ? GaStorageScalar<T>.ZeroScalar
                : mv2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            return scalarProcessor.Add(
                mv1.IndexScalarList,
                mv2.IndexScalarList
            ).CreateStorageVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> mv1, IGaStorageBivector<T> mv2)
        {
            return scalarProcessor.Add(
                mv1.IndexScalarList,
                mv2.IndexScalarList
            ).CreateStorageBivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            return scalarProcessor.Add(
                mv1.Grade,
                mv1.IndexScalarList,
                mv2.Grade,
                mv2.IndexScalarList
            ).CreateStorageGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Add(scalarProcessor, s1, s2),

                IGaStorageVector<T> v1 when mv2 is IGaStorageVector<T> v2 => 
                    Add(scalarProcessor, v1, v2),

                IGaStorageBivector<T> bv1 when mv2 is IGaStorageBivector<T> bv2 => 
                    Add(scalarProcessor, bv1, bv2),

                IGaStorageKVector<T> kv1 when mv2 is IGaStorageKVector<T> kv2 => 
                    Add(scalarProcessor, kv1, kv2),

                IGaStorageMultivectorGraded<T> gmv1 when mv2 is IGaStorageMultivectorGraded<T> gmv2 =>
                    scalarProcessor.Add(
                        gmv1.GradeIndexScalarList, 
                        gmv2.GradeIndexScalarList
                    ).CreateStorageGradedMultivector(),

                _ => scalarProcessor
                        .CreateStorageSparseMultivectorComposer()
                        .SetTerms(mv1.GetIdScalarRecords())
                        .AddTerms(mv2.GetIdScalarRecords())
                        .RemoveZeroTerms()
                        .CreateStorageSparseMultivector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, T scalar2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => 
                    Add(scalarProcessor, s1, scalar2),

                IGaStorageMultivectorGraded<T> gmv1 =>
                    scalarProcessor
                        .CreateStorageGradedMultivectorComposer()
                        .SetTerms(gmv1.GradeIndexScalarList)
                        .AddTerm(0, scalar2)
                        .RemoveZeroTerms()
                        .CreateStorageSparseMultivector(),

                _ => 
                    scalarProcessor
                        .CreateStorageSparseMultivectorComposer()
                        .SetTerms(mv1.GetIdScalarRecords())
                        .AddTerm(0, scalar2)
                        .RemoveZeroTerms()
                        .CreateStorageSparseMultivector()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Add<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar2, IGaStorageMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => 
                    Add(scalarProcessor, scalar2, s1),

                IGaStorageMultivectorGraded<T> gmv1 =>
                    scalarProcessor
                        .CreateStorageGradedMultivectorComposer()
                        .SetTerm(0, scalar2)
                        .AddTerms(gmv1.GradeIndexScalarList)
                        .RemoveZeroTerms()
                        .CreateStorageSparseMultivector(),

                _ => 
                    scalarProcessor
                        .CreateStorageSparseMultivectorComposer()
                        .SetTerm(0, scalar2)
                        .AddTerms(mv1.GetIdScalarRecords())
                        .RemoveZeroTerms()
                        .CreateStorageSparseMultivector()
            };
        }
    }
}