using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

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

        public static IGaMultivectorStorage<double> Op(this GaSignatureLookup basisSignature, IGaMultivectorStorage<double> mv1, IGaMultivectorStorage<double> mv2)
        {
            var composer = 
                new GaMultivectorFloat64StorageComposer(basisSignature);

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
                composer.AddOpTerm(id1, id2, scalar1, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv1, IGaScalarStorage<T> mv2)
        {
            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.Times(
                    mv1.GetScalar(scalarProcessor), 
                    mv2.GetScalar(scalarProcessor)
                )
            );
        }
        
        public static IGaBivectorStorage<T> VectorsOp<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> vector1, ILaVectorEvenStorage<T> vector2)
        {
            var storage = scalarProcessor.CreateKVectorStorageComposer();

            foreach (var (index1, scalar1) in vector1.GetIndexScalarRecords())
            {
                foreach (var (index2, scalar2) in vector2.GetIndexScalarRecords())
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

            return storage.CreateGaBivectorStorage();
        }
        
        private static IGaBivectorStorage<T> OpAsGaBivectorStorage<T>(IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv1, IGaVectorStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateKVectorStorageComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
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

            return composer.CreateGaBivectorStorage();
        }

        public static IGaBivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv1, IGaVectorStorage<T> mv2)
        {
            return OpAsGaBivectorStorage(scalarProcessor, mv1, mv2);
        }

        private static IGaKVectorStorage<T> OpAsGaKVectorStorage<T>(IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade2 + grade1;

            if (grade > GaSpaceUtils.MaxVSpaceDimension)
                return scalarProcessor.CreateStorageZeroScalar();

            var composer = 
                scalarProcessor.CreateKVectorStorageComposer();

            var indexScalarPairs1 = 
                mv1.IndexScalarList;

            var indexScalarPairs2 = 
                mv2.IndexScalarList;

            foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
            {
                var id1 = index1.BasisBladeIndexToId(grade1);

                foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
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

            return composer.CreateGaKVectorStorage(grade);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    Op(scalarProcessor, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    Op(scalarProcessor, vt1, vt2),

                _ => 
                    OpAsGaKVectorStorage(scalarProcessor, mv1, mv2)
            };
        }

        private static IGaMultivectorGradedStorage<T> OpAsGaMultivectorGradedStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv1, IGaMultivectorGradedStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateStorageGradedMultivectorComposer();

            var gradeIndexScalarDictionary1 = mv1.GetGradeIndexScalarList();
            var gradeIndexScalarDictionary2 = mv2.GetGradeIndexScalarList();

            foreach (var (grade1, indexScalarPairs1) in gradeIndexScalarDictionary1.GetGradeStorageRecords())
            {
                foreach (var (grade2, indexScalarPairs2) in gradeIndexScalarDictionary2.GetGradeStorageRecords())
                {
                    var grade = grade2 + grade1;

                    if (grade > GaSpaceUtils.MaxVSpaceDimension)
                        continue;

                    foreach (var (index1, scalar1) in indexScalarPairs1.GetIndexScalarRecords())
                    {
                        var id1 = index1.BasisBladeIndexToId(grade1);

                        foreach (var (index2, scalar2) in indexScalarPairs2.GetIndexScalarRecords())
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

            return composer.CreateGaMultivectorGradedStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv1, IGaMultivectorGradedStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    Op(scalarProcessor, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    Op(scalarProcessor, vt1, vt2),

                IGaKVectorStorage<T> kvt1 when mv2 is IGaKVectorStorage<T> kvt2 => 
                    Op(scalarProcessor, kvt1, kvt2),

                _ => 
                    OpAsGaMultivectorGradedStorage(scalarProcessor, mv1, mv2)
            };
        }

        private static IGaMultivectorSparseStorage<T> OpAsGaMultivectorTermsStorage<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            var composer = 
                scalarProcessor.CreateStorageSparseMultivectorComposer();

            var idScalarPairs1 = 
                mv1.GetIdScalarRecords();

            var idScalarPairs2 = 
                mv2.GetIdScalarList();

            foreach (var (id1, scalar1) in idScalarPairs1)
            {
                foreach (var (id2, scalar2) in idScalarPairs2.GetIndexScalarRecords())
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

            return composer.CreateGaMultivectorSparseStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv1, IGaMultivectorSparseStorage<T> mv2)
        {
            return OpAsGaMultivectorTermsStorage(scalarProcessor, mv1, mv2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    Op(scalarProcessor, s1, s2),

                IGaVectorStorage<T> vt1 when mv2 is IGaVectorStorage<T> vt2 => 
                    Op(scalarProcessor, vt1, vt2),

                IGaKVectorStorage<T> kvt1 when mv2 is IGaKVectorStorage<T> kvt2 => 
                    Op(scalarProcessor, kvt1, kvt2),

                IGaMultivectorGradedStorage<T> gmv1 when mv2 is IGaMultivectorGradedStorage<T> gmv2 => 
                    OpAsGaMultivectorGradedStorage(scalarProcessor, gmv1, gmv2),

                _ =>
                    OpAsGaMultivectorTermsStorage(scalarProcessor, mv1, mv2)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, params IGaVectorStorage<T>[] vectorStorageList)
        {
            return vectorStorageList
                .Aggregate(
                    (IGaKVectorStorage<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IGaVectorStorage<T>> vectorStorageList)
        {
            return vectorStorageList
                .Aggregate(
                    (IGaKVectorStorage<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, params IGaKVectorStorage<T>[] kVectorStorageList)
        {
            return kVectorStorageList
                .Aggregate(
                    (IGaKVectorStorage<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IGaKVectorStorage<T>> kVectorStorageList)
        {
            return kVectorStorageList
                .Aggregate(
                    (IGaKVectorStorage<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, params IGaMultivectorStorage<T>[] mvStoragesList)
        {
            return mvStoragesList
                .Aggregate(
                    (IGaMultivectorStorage<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Op<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IGaMultivectorStorage<T>> mvStoragesList)
        {
            return mvStoragesList
                .Aggregate(
                    (IGaMultivectorStorage<T>) scalarProcessor.CreateStorageBasisScalar(),
                    scalarProcessor.Op
                );
        }
    }
}