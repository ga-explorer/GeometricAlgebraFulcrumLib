using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Products
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

        public static IGasMultivector<double> Op(this GaSignatureLookup basisSignature, IGasMultivector<double> mv1, IGasMultivector<double> mv2)
        {
            var composer = 
                new GaMultivectorFloat64Composer(basisSignature);

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
        public static IGasScalar<T> Op<T>(this IGasScalar<T> mv1, IGasScalar<T> mv2)
        {
            return mv1.ScalarProcessor.CreateScalar(
                mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivectorTerm<T> Op<T>(this IGasVectorTerm<T> mv1, IGasVectorTerm<T> mv2)
        {
            if (mv1.Index == mv2.Index)
                return mv1.ScalarProcessor.CreateZeroBivector();

            return mv1.ScalarProcessor.CreateBivector(
                mv1.Index, 
                mv2.Index, 
                mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar)
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasKVectorTerm<T> OpAsGaKVectorTermStorage<T>(this IGasKVectorTerm<T> mv1, IGasKVectorTerm<T> mv2)
        {
            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade2 + grade1;

            if (grade > GaSpaceUtils.MaxVSpaceDimension)
                return mv1.ScalarProcessor.CreateZeroScalar();

            var signature = 
                GaBasisUtils.OpSignature(mv1.Id, mv2.Id);

            if (signature == 0)
                return mv1.ScalarProcessor.CreateZeroKVector(grade);

            var scalar = 
                signature > 0
                    ? mv1.ScalarProcessor.Times(mv1.Scalar, mv2.Scalar)
                    : mv1.ScalarProcessor.NegativeTimes(mv1.Scalar, mv2.Scalar);

            return mv1.ScalarProcessor.CreateKVector(mv1.Id ^ mv2.Id, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVectorTerm<T> Op<T>(this IGasKVectorTerm<T> mv1, IGasKVectorTerm<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Op(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Op(vt1, vt2),

                _ => 
                    OpAsGaKVectorTermStorage(mv1, mv2)
            };
        }
        
        public static IGasBivector<T> VectorsOp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
        {
            var storage = new GaBivectorStorageComposer<T>(scalarProcessor);

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

            return storage.GetBivectorStorage();
        }
        
        private static IGasBivector<T> OpAsGaBivectorStorage<T>(this IGasVector<T> mv1, IGasVector<T> mv2)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            var composer = 
                new GaBivectorStorageComposer<T>(scalarProcessor);

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var indexScalarPairs2 = 
                mv2.GetIndexScalarDictionary();

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

            return composer.GetBivectorStorage();
        }

        public static IGasBivector<T> Op<T>(this IGasVector<T> mv1, IGasVector<T> mv2)
        {
            return mv1 is IGasVectorTerm<T> vt1 && mv2 is IGasVectorTerm<T> vt2
                ? Op(vt1, vt2)
                : OpAsGaBivectorStorage(mv1, mv2);
        }

        private static IGasKVector<T> OpAsGaKVectorStorage<T>(this IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var grade1 = mv1.Grade;
            var grade2 = mv2.Grade;
            var grade = grade2 + grade1;

            if (grade > GaSpaceUtils.MaxVSpaceDimension)
                return scalarProcessor.CreateZeroScalar();

            var composer = 
                new GaKVectorStorageComposer<T>(scalarProcessor, grade);

            var indexScalarPairs1 = 
                mv1.GetIndexScalarPairs();

            var indexScalarPairs2 = 
                mv2.GetIndexScalarDictionary();

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

            return composer.GetKVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> Op<T>(this IGasKVector<T> mv1, IGasKVector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Op(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Op(vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    Op(kvt1, kvt2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    Op(v1, v2),

                _ => 
                    OpAsGaKVectorStorage(mv1, mv2)
            };
        }

        private static IGasGradedMultivector<T> OpAsGaMultivectorGradedStorage<T>(this IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorGradedStorageComposer<T>(scalarProcessor);

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

            return composer.GetCompactGradedMultivector();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasGradedMultivector<T> Op<T>(this IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Op(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Op(vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    Op(kvt1, kvt2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    Op(v1, v2),

                IGasKVector<T> kv1 when mv2 is IGasKVector<T> kv2 => 
                    OpAsGaKVectorStorage(kv1, kv2),

                _ => 
                    OpAsGaMultivectorGradedStorage(mv1, mv2)
            };
        }

        private static IGasTermsMultivector<T> OpAsGaMultivectorTermsStorage<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var scalarProcessor = 
                mv1.ScalarProcessor;

            var composer = 
                new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

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

            return composer.GetCompactTermsStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasTermsMultivector<T> Op<T>(this IGasTermsMultivector<T> mv1, IGasTermsMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Op(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Op(vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    Op(kvt1, kvt2),

                _ => 
                    OpAsGaMultivectorTermsStorage(mv1, mv2)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Op<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Op(s1, s2),

                IGasVectorTerm<T> vt1 when mv2 is IGasVectorTerm<T> vt2 => 
                    Op(vt1, vt2),

                IGasKVectorTerm<T> kvt1 when mv2 is IGasKVectorTerm<T> kvt2 => 
                    Op(kvt1, kvt2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    OpAsGaBivectorStorage(v1, v2),

                IGasKVector<T> kv1 when mv2 is IGasKVector<T> kv2 => 
                    OpAsGaKVectorStorage(kv1, kv2),

                IGasGradedMultivector<T> gmv1 when mv2 is IGasGradedMultivector<T> gmv2 => 
                    OpAsGaMultivectorGradedStorage(gmv1, gmv2),

                _ =>
                    OpAsGaMultivectorTermsStorage(mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, params IGasVector<T>[] vectorStorageList)
        {
            return vectorStorageList
                .Aggregate(
                    (IGasKVector<T>) scalarProcessor.CreateBasisScalar(),
                    (acc, basisVector) => acc.Op(basisVector)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<IGasVector<T>> vectorStorageList)
        {
            return vectorStorageList
                .Aggregate(
                    (IGasKVector<T>) scalarProcessor.CreateBasisScalar(),
                    (acc, basisVector) => acc.Op(basisVector)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, params IGasKVector<T>[] kVectorStorageList)
        {
            return kVectorStorageList
                .Aggregate(
                    (IGasKVector<T>) scalarProcessor.CreateBasisScalar(),
                    (acc, basisVector) => acc.Op(basisVector)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasKVector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<IGasKVector<T>> kVectorStorageList)
        {
            return kVectorStorageList
                .Aggregate(
                    (IGasKVector<T>) scalarProcessor.CreateBasisScalar(),
                    (acc, basisVector) => acc.Op(basisVector)
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, params IGasMultivector<T>[] mvStoragesList)
        {
            return mvStoragesList
                .Aggregate(
                    (IGasMultivector<T>) scalarProcessor.CreateBasisScalar(),
                    (mv1, mv2) => mv1.Op(mv2)
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<IGasMultivector<T>> mvStoragesList)
        {
            return mvStoragesList
                .Aggregate(
                    (IGasMultivector<T>) scalarProcessor.CreateBasisScalar(),
                    (mv1, mv2) => mv1.Op(mv2)
                );
        }
    }
}