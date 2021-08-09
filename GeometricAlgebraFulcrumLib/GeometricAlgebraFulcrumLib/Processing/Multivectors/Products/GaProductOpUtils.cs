using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductOpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult Op(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                GaBasisUtils.OpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static IGaStorageMultivector<double> Op(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1, IGaStorageMultivector<double> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorFloat64(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2)
                composer.AddOpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv1, IGaStorageScalar<T> mv2)
        {
            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.Times(
                    mv1.GetScalar(scalarProcessor), 
                    mv2.GetScalar(scalarProcessor)
                )
            );
        }
        
        public static IGaStorageBivector<T> VectorsOp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            var storage = new GaStorageComposerBivector<T>(scalarProcessor);

            for (var index1 = 0; index1 < vector1.Count; index1++)
            {
                var scalar1 = vector1[index1];

                for (var index2 = 0; index2 < vector2.Count; index2++)
                {
                    if (index1 == index2)
                        continue;

                    var scalar2 = vector2[index2];

                    storage.AddTerm(
                        index1, 
                        index2, 
                        scalarProcessor.Times(scalar1, scalar2)
                    );
                }
            }

            storage.RemoveZeroTerms();

            return storage.GetBivector();
        }
        
        private static IGaStorageBivector<T> OpAsGaBivectorStorage<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            var composer = 
                new GaStorageComposerBivector<T>(scalarProcessor);

            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var indexScalarPairs2 = 
                mv2.IndexScalarDictionary;

            foreach (var (index1, scalar1) in indexScalarPairs1)
            {
                foreach (var (index2, scalar2) in indexScalarPairs2)
                {
                    if (index1 == index2)
                        continue;

                    composer.AddTerm(
                        index1, 
                        index2, 
                        scalarProcessor.Times(scalar1, scalar2)
                    );
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetBivector();
        }

        public static IGaStorageBivector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            return OpAsGaBivectorStorage(scalarProcessor, mv1, mv2);
        }

        private static IGaStorageKVector<T> OpAsGaKVectorStorage<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade2 + grade1;

            if (grade > GaSpaceUtils.MaxVSpaceDimension)
                return scalarProcessor.CreateStorageZeroScalar();

            var composer = 
                new GaStorageComposerKVector<T>(scalarProcessor, grade);

            var indexScalarPairs1 = 
                mv1.IndexScalarDictionary;

            var indexScalarPairs2 = 
                mv2.IndexScalarDictionary;

            foreach (var (index1, scalar1) in indexScalarPairs1)
            {
                var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

                foreach (var (index2, scalar2) in indexScalarPairs2)
                {
                    var id2 = GaBasisUtils.BasisBladeId(grade2, index2);

                    var signature = 
                        GaBasisUtils.OpSignature(id1, id2);

                    if (signature == 0) 
                        continue;

                    var index = (id1 ^ id2).BasisBladeIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(index, scalar);
                    else
                        composer.SubtractTerm(index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetKVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv1, IGaStorageKVector<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Op(scalarProcessor, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    Op(scalarProcessor, vt1, vt2),

                _ => 
                    OpAsGaKVectorStorage(scalarProcessor, mv1, mv2)
            };
        }

        private static IGaStorageMultivectorGraded<T> OpAsGaMultivectorGradedStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorGraded<T>(scalarProcessor);

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarDictionary();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarDictionary();

            foreach (var (grade1, indexScalarPairs1) in gradeIndexScalarDictionary1)
            {
                foreach (var (grade2, indexScalarPairs2) in gradeIndexScalarDictionary2)
                {
                    var grade = grade2 + grade1;

                    if (grade > GaSpaceUtils.MaxVSpaceDimension)
                        continue;

                    foreach (var (index1, scalar1) in indexScalarPairs1)
                    {
                        var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

                        foreach (var (index2, scalar2) in indexScalarPairs2)
                        {
                            var id2 = GaBasisUtils.BasisBladeId(grade2, index2);

                            var signature = 
                                GaBasisUtils.OpSignature(id1, id2);

                            if (signature == 0) 
                                continue;

                            var index = (id1 ^ id2).BasisBladeIndex();
                            var scalar = scalarProcessor.Times(scalar1, scalar2);

                            if (signature > 0)
                                composer.AddTerm(grade, index, scalar);
                            else
                                composer.SubtractTerm(grade, index, scalar);
                        }
                    }
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetGradedMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Op(scalarProcessor, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    Op(scalarProcessor, vt1, vt2),

                IGaStorageKVector<T> kvt1 when mv2 is IGaStorageKVector<T> kvt2 => 
                    Op(scalarProcessor, kvt1, kvt2),

                _ => 
                    OpAsGaMultivectorGradedStorage(scalarProcessor, mv1, mv2)
            };
        }

        private static IGaStorageMultivectorSparse<T> OpAsGaMultivectorTermsStorage<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorSparse<T>(scalarProcessor);

            var idScalarPairs1 = 
                mv1.GetIdScalarPairs();

            var idScalarPairs2 = 
                mv2.GetIdScalarDictionary();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2)
                {
                    var signature = 
                        GaBasisUtils.OpSignature(id1, id2);

                    if (signature == 0) 
                        continue;

                    var id = id1 ^ id2;
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(id, scalar);
                    else
                        composer.SubtractTerm(id, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.GetSparseMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv1, IGaStorageMultivectorSparse<T> mv2)
        {
            return OpAsGaMultivectorTermsStorage(scalarProcessor, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Op(scalarProcessor, s1, s2),

                IGaStorageVector<T> vt1 when mv2 is IGaStorageVector<T> vt2 => 
                    Op(scalarProcessor, vt1, vt2),

                IGaStorageKVector<T> kvt1 when mv2 is IGaStorageKVector<T> kvt2 => 
                    Op(scalarProcessor, kvt1, kvt2),

                IGaStorageMultivectorGraded<T> gmv1 when mv2 is IGaStorageMultivectorGraded<T> gmv2 => 
                    OpAsGaMultivectorGradedStorage(scalarProcessor, gmv1, gmv2),

                _ =>
                    OpAsGaMultivectorTermsStorage(scalarProcessor, mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, params IGaStorageVector<T>[] vectorStorageList)
        {
            return vectorStorageList
                .Aggregate(
                    (IGaStorageKVector<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<IGaStorageVector<T>> vectorStorageList)
        {
            return vectorStorageList
                .Aggregate(
                    (IGaStorageKVector<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, params IGaStorageKVector<T>[] kVectorStorageList)
        {
            return kVectorStorageList
                .Aggregate(
                    (IGaStorageKVector<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<IGaStorageKVector<T>> kVectorStorageList)
        {
            return kVectorStorageList
                .Aggregate(
                    (IGaStorageKVector<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, params IGaStorageMultivector<T>[] mvStoragesList)
        {
            return mvStoragesList
                .Aggregate(
                    (IGaStorageMultivector<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<IGaStorageMultivector<T>> mvStoragesList)
        {
            return mvStoragesList
                .Aggregate(
                    (IGaStorageMultivector<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
    }
}