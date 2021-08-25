using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Products
{
    public static class GaProductOpUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaBasisBilinearProductResult Op(this IGaSignature signature, ulong id1, ulong id2)
        {
            return new GaBasisBilinearProductResult(
                GaBasisBladeProductUtils.OpSignature(id1, id2), 
                id1 ^ id2
            );
        }

        public static IGaStorageMultivector<double> Op(this GaSignatureLookup basisSignature, IGaStorageMultivector<double> mv1, IGaStorageMultivector<double> mv2)
        {
            var composer = 
                new GaStorageComposerMultivectorFloat64(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2.GetKeyValueRecords())
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
        
        public static IGaStorageBivector<T> VectorsOp<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> vector1, IGaListEven<T> vector2)
        {
            var storage = scalarProcessor.CreateStorageKVectorComposer();

            foreach (var (index1, scalar1) in vector1.GetKeyValueRecords())
            {
                foreach (var (index2, scalar2) in vector2.GetKeyValueRecords())
                {
                    if (index1 == index2)
                        continue;

                    storage.AddBivectorTerm(
                        index1, 
                        index2, 
                        scalarProcessor.Times(scalar1, scalar2)
                    );
                }
            }

            storage.RemoveZeroTerms();

            return storage.CreateStorageBivector();
        }
        
        private static IGaStorageBivector<T> OpAsGaBivectorStorage<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateStorageKVectorComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetKeyValueRecords())
            {
                foreach (var (index2, scalar2) in indexScalarPairs2.GetKeyValueRecords())
                {
                    if (index1 == index2)
                        continue;

                    composer.AddBivectorTerm(
                        index1, 
                        index2, 
                        scalarProcessor.Times(scalar1, scalar2)
                    );
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateStorageBivector();
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
                scalarProcessor.CreateStorageKVectorComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetKeyValueRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetKeyValueRecords())
                {
                    var id2 = index2.BasisBladeIndexToId(grade2);

                    var signature = 
                        GaBasisBladeProductUtils.OpSignature(id1, id2);

                    if (signature == 0) 
                        continue;

                    var index = (id1 ^ id2).BasisBladeIdToIndex();
                    var scalar = scalarProcessor.Times(scalar1, scalar2);

                    if (signature > 0)
                        composer.AddTerm(index, scalar);
                    else
                        composer.SubtractTerm(index, scalar);
                }
            }

            composer.RemoveZeroTerms();

            return composer.CreateStorageKVector(grade);
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
                scalarProcessor.CreateStorageGradedMultivectorComposer();

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarList();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarList();

            foreach (var (grade1, indexScalarPairs1) in gradeIndexScalarDictionary1.GetGradeListRecords())
            {
                foreach (var (grade2, indexScalarPairs2) in gradeIndexScalarDictionary2.GetGradeListRecords())
                {
                    var grade = grade2 + grade1;

                    if (grade > GaSpaceUtils.MaxVSpaceDimension)
                        continue;

                    foreach (var (index1, scalar1) in indexScalarPairs1.GetKeyValueRecords())
                    {
                        var id1 = index1.BasisBladeIndexToId(grade1);

                        foreach (var (index2, scalar2) in indexScalarPairs2.GetKeyValueRecords())
                        {
                            var id2 = index2.BasisBladeIndexToId(grade2);

                            var signature = 
                                GaBasisBladeProductUtils.OpSignature(id1, id2);

                            if (signature == 0) 
                                continue;

                            var index = (id1 ^ id2).BasisBladeIdToIndex();
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

            return composer.CreateStorageGradedMultivector();
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
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetKeyValueRecords())
                {
                    var signature = 
                        GaBasisBladeProductUtils.OpSignature(id1, id2);

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

            return composer.CreateStorageSparseMultivector();
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