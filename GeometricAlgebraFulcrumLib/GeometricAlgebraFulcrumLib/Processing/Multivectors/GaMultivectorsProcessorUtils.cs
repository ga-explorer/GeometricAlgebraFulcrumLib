using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors
{
    public static class GaMultivectorsProcessorUtils
    {
        public static T GetAngle<T>(this IGaVectorStorage<T> v1, IGaVectorStorage<T> v2)
        {
            var scalarProcessor = v1.ScalarProcessor;

            return scalarProcessor.ArcCos(
                scalarProcessor.Divide(
                    v1.ESp(v2),
                    scalarProcessor.Sqrt(
                        scalarProcessor.Times(v1.ESp(), v2.ESp())
                    )
                )
            );
        }


        public static bool IsEuclideanRotor<T>(this IGaMultivectorStorage<T> storage)
        {
            if (storage.GetGrades().Any(grade => grade % 2 != 0))
                return false;

            return storage.EGpReverse()
                .Subtract(storage.ScalarProcessor.OneScalar)
                .IsZero();
        }

        public static bool IsSimpleEuclideanRotor<T>(this IGaMultivectorStorage<T> storage)
        {
            if (storage.GetGrades().Any(grade => grade != 0 && grade != 2))
                return false;

            return storage.EGpReverse()
                .Subtract(storage.ScalarProcessor.OneScalar)
                .IsZero();
        }


        public static IGaMultivectorStorage<T> Gp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorProcessor<T> processor)
        {
            return processor.Gp(mv1);
        }

        public static IGaMultivectorStorage<T> GpReverse<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorProcessor<T> processor)
        {
            return processor.GpReverse(mv1);
        }

        public static IGaMultivectorStorage<T> Gp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorProcessor<T> processor)
        {
            return processor.Gp(mv1, mv2);
        }

        public static IGaMultivectorStorage<T> GpReverse<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorProcessor<T> processor)
        {
            return processor.GpReverse(mv1, mv2);
        }

        public static T Sp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorProcessor<T> processor)
        {
            return processor.Sp(mv1);
        }

        public static T Sp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorProcessor<T> processor)
        {
            return processor.Sp(mv1, mv2);
        }

        public static IGaMultivectorStorage<T> BladeInverse<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorProcessor<T> processor)
        {
            return processor.BladeInverse(mv1);
        }

        public static IGaMultivectorStorage<T> VersorInverse<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorProcessor<T> processor)
        {
            return processor.VersorInverse(mv1);
        }

        public static IGaMultivectorStorage<T> Dual<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorProcessor<T> processor)
        {
            return processor.Dual(mv1);
        }

        public static IGaMultivectorStorage<T> UnDual<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorProcessor<T> processor)
        {
            return processor.UnDual(mv1);
        }

        public static T NormSquared<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorProcessor<T> processor)
        {
            return processor.NormSquared(mv1);
        }

        public static T Norm<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorProcessor<T> processor)
        {
            return processor.Norm(mv1);
        }

        public static IGaMultivectorStorage<T> Lcp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorProcessor<T> processor)
        {
            return processor.Lcp(mv1, mv2);
        }

        public static IGaMultivectorStorage<T> Rcp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorProcessor<T> processor)
        {
            return processor.Rcp(mv1, mv2);
        }

        public static IGaMultivectorStorage<T> Fdp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorProcessor<T> processor)
        {
            return processor.Fdp(mv1, mv2);
        }

        public static IGaMultivectorStorage<T> Hip<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorProcessor<T> processor)
        {
            return processor.Hip(mv1, mv2);
        }

        public static IGaMultivectorStorage<T> Acp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorProcessor<T> processor)
        {
            return processor.Acp(mv1, mv2);
        }

        public static IGaMultivectorStorage<T> Cp<T>(this IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2, IGaMultivectorProcessor<T> processor)
        {
            return processor.Cp(mv1, mv2);
        }



        public static IGaMultivectorStorage<T> Add<T>(this T scalar1, IGaMultivectorStorage<T> mv2)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(mv2.ScalarProcessor);

            composer.SetTerm(0, scalar1);

            composer.AddTerms(mv2.GetIdScalarDictionary());

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }
        
        public static IGaMultivectorStorage<T> Subtract<T>(this T scalar1, IGaMultivectorStorage<T> mv2)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(mv2.ScalarProcessor);

            composer.SetTerm(0, scalar1);

            composer.SubtractTerms(mv2.GetIdScalarDictionary());

            composer.RemoveZeroTerms();

            return composer.GetCompactStorage();
        }

        public static IGaMultivectorStorage<T> Times<T>(this T scalar1, IGaMultivectorStorage<T> mv2)
        {
            return mv2.Times(scalar1);
        }
        

        public static IGaKVectorStorage<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<IGaVectorStorage<T>> vectorStorageList)
        {
            return vectorStorageList
                .Aggregate(
                    (IGaKVectorStorage<T>) GaScalarTermStorage<T>.CreateBasisScalar(scalarProcessor),
                    (acc, basisVector) => acc.Op(basisVector)
                );
        }
        
        public static IGaKVectorStorage<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<IGaKVectorStorage<T>> kVectorStorageList)
        {
            return kVectorStorageList
                .Aggregate(
                    (IGaKVectorStorage<T>) GaScalarTermStorage<T>.CreateBasisScalar(scalarProcessor),
                    (acc, basisVector) => acc.Op(basisVector)
                );
        }
        
        public static IGaMultivectorStorage<T> Op<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<IGaMultivectorStorage<T>> mvStoragesList)
        {
            return mvStoragesList
                .Aggregate(
                    (IGaMultivectorStorage<T>) GaScalarTermStorage<T>.CreateBasisScalar(scalarProcessor),
                    (mv1, mv2) => mv1.Op(mv2)
                );
        }

        public static IGaBivectorStorage<T> VectorsOp<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> vector1, IReadOnlyList<T> vector2)
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
        

        //private static IGaMultivectorStorage<T> EGp<T>(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs1, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs2)
        //{
        //    var composer = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

        //    var idScalarDictionary2 = idScalarPairs2.ToDictionary(
        //        pair => pair.Key,
        //        pair => pair.Value
        //    );

        //    foreach (var (id1, scalar1) in idScalarPairs1)
        //    {
        //        foreach (var (id2, scalar2) in idScalarDictionary2)
        //        {
        //            var id = id1 ^ id2;
        //            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
        //                ? scalarProcessor.NegativeTimes(scalar1, scalar2)
        //                : scalarProcessor.Times(scalar1, scalar2);

        //            composer.AddTerm(id, scalar);
        //        }    
        //    }

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> EGp<T>(this IGaMultivectorStorage<T> mvStorage1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    var scalarProcessor = mvStorage1.ScalarProcessor;

        //    return mvStorage1 switch
        //    {
        //        GaScalarTermStorage<T> scalarStorage1 => mvStorage2 switch
        //        {
        //            GaScalarTermStorage<T> scalarStorage2 => GaScalarTermStorage<T>.Create(scalarProcessor, scalarProcessor.Times(scalarStorage1.Scalar, scalarStorage2.Scalar)),
        //            _ => Times(scalarStorage1.Scalar, mvStorage2)
        //        },

        //        IGaKVectorStorage<T> kVectorStorage1 => mvStorage2 switch
        //        {
        //            GaScalarTermStorage<T> scalarStorage2 => kVectorStorage1.Times(scalarStorage2.Scalar),
        //            _ => EGp(scalarProcessor, mvStorage1.GetIdScalarPairs(), mvStorage2.GetIdScalarPairs())
        //        },

        //        GaMultivectorGradedStorage<T> multivectorStorage1 => mvStorage2 switch
        //        {
        //            GaScalarTermStorage<T> scalarStorage2 => multivectorStorage1.Times(scalarStorage2.Scalar),
        //            _ => EGp(scalarProcessor, mvStorage1.GetIdScalarPairs(), mvStorage2.GetIdScalarPairs())
        //        },

        //        _ => mvStorage2 switch
        //        {
        //            GaScalarTermStorage<T> scalarStorage2 => mvStorage1.Times(scalarStorage2.Scalar),
        //            _ => EGp(scalarProcessor, mvStorage1.GetIdScalarPairs(), mvStorage2.GetIdScalarPairs())
        //        }
        //    };
        //}

        //public static GaMultivector<T> EGp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    return new(
        //        EGp(mv1.Storage, mv2.Storage)
        //    );
        //}

        //public static GaMultivector<T> Gp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaMultivectorsProcessorBase<T> processor)
        //{
        //    return new(
        //        processor.Gp(mv1.Storage, mv2.Storage)
        //    );
        //}

        
        //private static IGaMultivectorStorage<T> EGpSquared<T>(IGaScalarProcessor<T> scalarProcessor, T scalar1)
        //{
        //    return GaScalarTermStorage<T>.Create(
        //        scalarProcessor,
        //        scalarProcessor.Times(scalar1, scalar1)
        //    );
        //}

        //private static IGaMultivectorStorage<T> EGpSquared<T>(IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalars1)
        //{
        //    return GaScalarTermStorage<T>.Create(
        //        scalarProcessor,
        //        ESp(scalarProcessor, scalars1)
        //    );
        //}

        //private static IGaMultivectorStorage<T> EGpSquared<T>(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, T> idScalarDictionary1)
        //{
        //    var composer = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

        //    foreach (var (id1, scalar1) in idScalarDictionary1)
        //    {
        //        foreach (var (id2, scalar2) in idScalarDictionary1)
        //        {
        //            var id = id1 ^ id2;
        //            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
        //                ? scalarProcessor.NegativeTimes(scalar1, scalar2)
        //                : scalarProcessor.Times(scalar1, scalar2);

        //            composer.AddTerm(id, scalar);
        //        }    
        //    }

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> EGpSquared<T>(this IGaMultivectorStorage<T> storage1)
        //{
        //    var scalarProcessor = storage1.ScalarProcessor;

        //    return storage1 switch
        //    {
        //        IGaScalarStorage<T> scalarStorage1 => EGpSquared(scalarProcessor, scalarStorage1.Scalar),
        //        IGaVectorStorage<T> vectorStorage1 => EGpSquared(scalarProcessor, vectorStorage1.GetScalars()),
        //        _ => EGpSquared(scalarProcessor, storage1.GetIdScalarDictionary())
        //    };
        //}

        //public static GaMultivector<T> EGpSquared<T>(this GaMultivector<T> mv1)
        //{
        //    return new(
        //        EGpSquared(mv1.Storage)
        //    );
        //}

        //public static GaMultivector<T> GpSquared<T>(this GaMultivector<T> mv1, GaMultivectorsProcessorBase<T> processor)
        //{
        //    return new(
        //        processor.Gp(mv1.Storage)
        //    );
        //}


        //private static IGaMultivectorStorage<T> EGpReverse<T>(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, T> idScalarDictionary1)
        //{
        //    var composer = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

        //    foreach (var (id1, scalar1) in idScalarDictionary1)
        //    {
        //        foreach (var (id2, scalar2) in idScalarDictionary1)
        //        {
        //            var id = id1 ^ id2;
        //            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
        //                ? scalarProcessor.NegativeTimes(scalar1, scalar2)
        //                : scalarProcessor.Times(scalar1, scalar2);

        //            if (id2.BasisBladeIdHasNegativeReverse())
        //                composer.SubtractTerm(id, scalar);
        //            else
        //                composer.AddTerm(id, scalar);
        //        }    
        //    }

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> EGpReverse<T>(this IGaMultivectorStorage<T> storage1)
        //{
        //    var scalarProcessor = storage1.ScalarProcessor;

        //    return storage1 switch
        //    {
        //        IGaScalarStorage<T> scalarStorage1 => EGpSquared(scalarProcessor, scalarStorage1.Scalar),
        //        IGaVectorStorage<T> vectorStorage1 => EGpSquared(scalarProcessor, vectorStorage1.GetScalars()),
        //        _ => EGpReverse(scalarProcessor, storage1.GetIdScalarDictionary())
        //    };
        //}

        //public static GaMultivector<T> EGpReverse<T>(this GaMultivector<T> mv1)
        //{
        //    return new(
        //        EGpReverse(mv1.Storage)
        //    );
        //}

        //public static GaMultivector<T> GpReverse<T>(this GaMultivector<T> mv1, GaMultivectorsProcessorBase<T> processor)
        //{
        //    return new(
        //        processor.GpReverse(mv1.Storage)
        //    );
        //}


        //private static T ESp<T>(IGaScalarProcessor<T> scalarProcessor, int grade, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        //{
        //    var composer = new GaScalarStorageComposer<T>(scalarProcessor);

        //    if (grade.GradeHasNegativeReverse())
        //    {
        //        foreach (var (index, scalar) in indexScalarDictionary)
        //        {
        //            var id = GaBasisUtils.BasisBladeId(grade, index);

        //            if (GaBasisUtils.IsNegativeEGp(id))
        //                composer.AddScalar(scalarProcessor.Times(scalar, scalar));
        //            else
        //                composer.SubtractScalar(scalarProcessor.Times(scalar, scalar));
        //        }
        //    }
        //    else
        //    {
        //        foreach (var (index, scalar) in indexScalarDictionary)
        //        {
        //            var id = GaBasisUtils.BasisBladeId(grade, index);

        //            if (GaBasisUtils.IsNegativeEGp(id))
        //                composer.SubtractScalar(scalarProcessor.Times(scalar, scalar));
        //            else
        //                composer.AddScalar(scalarProcessor.Times(scalar, scalar));
        //        }
        //    }

        //    return composer.Scalar;
        //}

        //private static T ESp<T>(IGaScalarProcessor<T> scalarProcessor, int grade1, IReadOnlyDictionary<ulong, T> indexScalarDictionary1, int grade2, IReadOnlyDictionary<ulong, T> indexScalarDictionary2)
        //{
        //    if (grade1 != grade2)
        //        return scalarProcessor.ZeroScalar;

        //    var composer = new GaScalarStorageComposer<T>(scalarProcessor);

        //    if (grade1.GradeHasNegativeReverse())
        //    {
        //        foreach (var (index, scalar1) in indexScalarDictionary1)
        //        {
        //            if (!indexScalarDictionary2.TryGetValue(index, out var scalar2))
        //                continue;

        //            var id = GaBasisUtils.BasisBladeId(grade1, index);
        //            var scalar = scalarProcessor.Times(scalar1, scalar2);
                    
        //            if (GaBasisUtils.IsNegativeEGp(id))
        //                composer.AddScalar(scalar);
        //            else
        //                composer.SubtractScalar(scalar);
        //        }
        //    }
        //    else
        //    {
        //        foreach (var (index, scalar1) in indexScalarDictionary1)
        //        {
        //            if (!indexScalarDictionary2.TryGetValue(index, out var scalar2))
        //                continue;

        //            var id = GaBasisUtils.BasisBladeId(grade1, index);
        //            var scalar = scalarProcessor.Times(scalar1, scalar2);
                    
        //            if (GaBasisUtils.IsNegativeEGp(id))
        //                composer.SubtractScalar(scalar);
        //            else
        //                composer.AddScalar(scalar);
        //        }
        //    }

        //    return composer.Scalar;
        //}

        //private static T ESp<T>(IGaScalarProcessor<T> scalarProcessor, int grade1, IReadOnlyDictionary<ulong, T> indexScalarDictionary1, IReadOnlyDictionary<int, Dictionary<ulong, T>> gradeIndexScalarDictionary2)
        //{
        //    return gradeIndexScalarDictionary2.TryGetValue(grade1, out var indexScalarDictionary2) 
        //        ? ESp(scalarProcessor, grade1, indexScalarDictionary1, grade1, indexScalarDictionary2) 
        //        : scalarProcessor.ZeroScalar;
        //}

        //private static T ESp<T>(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, Dictionary<ulong, T>> gradeIndexScalarDictionary1, int grade2, IReadOnlyDictionary<ulong, T> indexScalarDictionary2)
        //{
        //    return gradeIndexScalarDictionary1.TryGetValue(grade2, out var indexScalarDictionary1) 
        //        ? ESp(scalarProcessor, grade2, indexScalarDictionary1, grade2, indexScalarDictionary2) 
        //        : scalarProcessor.ZeroScalar;
        //}

        //private static T ESp<T>(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, Dictionary<ulong, T>> gradeIndexScalarDictionary1, IReadOnlyDictionary<int, Dictionary<ulong, T>> gradeIndexScalarDictionary2)
        //{
        //    var composer = new GaScalarStorageComposer<T>(scalarProcessor);

        //    foreach (var (grade1, indexScalarDictionary1) in gradeIndexScalarDictionary1)
        //    {
        //        if (!gradeIndexScalarDictionary2.TryGetValue(grade1, out var indexScalarDictionary2))
        //            continue;

        //        composer.AddScalar(
        //            ESp(scalarProcessor, grade1, indexScalarDictionary1, grade1, indexScalarDictionary2) 
        //        );
        //    }

        //    return composer.Scalar;
        //}

        //private static T ESp<T>(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        //{
        //    var composer = new GaScalarStorageComposer<T>(scalarProcessor);

        //    foreach (var (id, scalar) in idScalarPairs)
        //    {
        //        if (id.BasisBladeIdHasNegativeReverse())
        //            composer.SubtractScalar(scalarProcessor.Times(scalar, scalar));
        //        else
        //            composer.AddScalar(scalarProcessor.Times(scalar, scalar));
        //    }

        //    return composer.Scalar;
        //}

        //private static T ESp<T>(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs1, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs2)
        //{
        //    var composer = new GaScalarStorageComposer<T>(scalarProcessor);

        //    var idScalarDictionary2 = idScalarPairs2.ToDictionary(
        //        pair => pair.Key,
        //        pair => pair.Value
        //    );

        //    foreach (var (id1, scalar1) in idScalarPairs1)
        //    {
        //        if (!idScalarDictionary2.TryGetValue(id1, out var scalar2))
        //            continue;

        //        var scalar = scalarProcessor.Times(scalar1, scalar2);

        //        if (id1.BasisBladeIdHasNegativeReverse())
        //            composer.SubtractScalar(scalar);
        //        else
        //            composer.AddScalar(scalar);
        //    }

        //    return composer.Scalar;
        //}
        
        //public static T ESp<T>(this IGaMultivectorStorage<T> mvStorage)
        //{
        //    var scalarProcessor = mvStorage.ScalarProcessor;

        //    return mvStorage switch
        //    {
        //        GaScalarTermStorage<T> scalarStorage => 
        //            scalarProcessor.Times(scalarStorage.Scalar, scalarStorage.Scalar),

        //        IGaKVectorStorage<T> kVectorStorage => 
        //            ESp(scalarProcessor, kVectorStorage.Grade, kVectorStorage.GetIndexScalarDictionary()),

        //        GaMultivectorGradedStorage<T> multivectorStorage => 
        //            ESp(scalarProcessor, multivectorStorage.GetGradeIndexScalarDictionary()),

        //        _ => 
        //            ESp(scalarProcessor, mvStorage.GetIdScalarPairs())
        //    };
        //}

        //public static T ESp<T>(this IGaMultivectorStorage<T> mvStorage1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    var scalarProcessor = mvStorage1.ScalarProcessor;

        //    return mvStorage1 switch
        //    {
        //        GaScalarTermStorage<T> scalarStorage1 => mvStorage2 switch
        //        {
        //            GaScalarTermStorage<T> scalarStorage2 => scalarProcessor.Times(scalarStorage1.Scalar, scalarStorage2.Scalar),
        //            _ => scalarProcessor.Times(scalarStorage1.Scalar, mvStorage2.GetTermScalar(0))
        //        },

        //        IGaKVectorStorage<T> kVectorStorage1 => mvStorage2 switch
        //        {
        //            GaScalarTermStorage<T> scalarStorage2 => scalarProcessor.Times(kVectorStorage1.GetTermScalar(0), scalarStorage2.Scalar),
        //            IGaKVectorStorage<T> kVectorStorage2 => ESp(scalarProcessor, kVectorStorage1.Grade, kVectorStorage1.GetIndexScalarDictionary(), kVectorStorage2.Grade, kVectorStorage2.GetIndexScalarDictionary()),
        //            GaMultivectorGradedStorage<T> multivectorStorage2 => ESp(scalarProcessor, kVectorStorage1.Grade, kVectorStorage1.GetIndexScalarDictionary(), multivectorStorage2.GetGradeIndexScalarDictionary()),
        //            _ => ESp(scalarProcessor, mvStorage1.GetIdScalarPairs(), mvStorage2.GetIdScalarPairs())
        //        },

        //        GaMultivectorGradedStorage<T> multivectorStorage1 => mvStorage2 switch
        //        {
        //            GaScalarTermStorage<T> scalarStorage2 => scalarProcessor.Times(multivectorStorage1.GetTermScalar(0), scalarStorage2.Scalar),
        //            IGaKVectorStorage<T> kVectorStorage2 => ESp(scalarProcessor, multivectorStorage1.GetGradeIndexScalarDictionary(), kVectorStorage2.Grade, kVectorStorage2.GetIndexScalarDictionary()),
        //            GaMultivectorGradedStorage<T> multivectorStorage2 => ESp(scalarProcessor, multivectorStorage1.GetGradeIndexScalarDictionary(), multivectorStorage2.GetGradeIndexScalarDictionary()),
        //            _ => ESp(scalarProcessor, mvStorage1.GetIdScalarPairs(), mvStorage2.GetIdScalarPairs())
        //        },

        //        _ => mvStorage2 switch
        //        {
        //            GaScalarTermStorage<T> scalarStorage2 => scalarProcessor.Times(mvStorage1.GetTermScalar(0), scalarStorage2.Scalar),
        //            _ => ESp(scalarProcessor, mvStorage1.GetIdScalarPairs(), mvStorage2.GetIdScalarPairs())
        //        }
        //    };
        //}

        //public static T ESp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    return ESp(mv1.Storage, mv2.Storage);
        //}

        //public static T Sp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaMultivectorsProcessorBase<T> processor)
        //{
        //    return processor.Sp(mv1.Storage, mv2.Storage);
        //}


        //private static T ESpSquared<T>(IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalars1)
        //{
        //    return scalars1
        //        .Select(scalar => scalarProcessor.Times(scalar, scalar))
        //        .Aggregate(
        //            scalarProcessor.ZeroScalar, 
        //            scalarProcessor.Add
        //        );
        //}

        //private static T ESpSquared<T>(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, T> idScalarDictionary1)
        //{
        //    var composer = new GaScalarStorageComposer<T>(scalarProcessor);

        //    foreach (var (id1, scalar1) in idScalarDictionary1)
        //    {
        //        var scalar = scalarProcessor.Times(scalar1, scalar1);

        //        if (GaBasisUtils.IsNegativeEGp(id1, id1))
        //            composer.SubtractScalar(scalar);
        //        else
        //            composer.AddScalar(scalar);
        //    }

        //    return composer.Scalar;
        //}

        //public static T ESpSquared<T>(this IGaMultivectorStorage<T> storage1)
        //{
        //    var scalarProcessor = storage1.ScalarProcessor;

        //    return storage1 switch
        //    {
        //        IGaScalarStorage<T> scalarStorage1 => scalarProcessor.Times(scalarStorage1.Scalar, scalarStorage1.Scalar),
        //        IGaVectorStorage<T> vectorStorage1 => ESp(scalarProcessor, vectorStorage1.GetScalars()),
        //        _ => ESp(scalarProcessor, storage1.GetIdScalarDictionary())
        //    };
        //}

        //public static T ESpSquared<T>(this GaMultivector<T> mv1)
        //{
        //    return ESp(mv1.Storage);
        //}

        //public static T SpSquared<T>(this GaMultivector<T> mv1, GaMultivectorsProcessorBase<T> processor)
        //{
        //    return processor.Sp(mv1.Storage);
        //}

        
        //private static T ESpReverse<T>(IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, T> idScalarDictionary1)
        //{
        //    var composer = new GaScalarStorageComposer<T>(scalarProcessor);

        //    foreach (var (id1, scalar1) in idScalarDictionary1)
        //    {
        //        var scalar = scalarProcessor.Times(scalar1, scalar1);

        //        if (id1.BasisBladeIdHasNegativeReverse() == GaBasisUtils.IsNegativeEGp(id1, id1))
        //            composer.AddScalar(scalar);
        //        else
        //            composer.SubtractScalar(scalar);
        //    }

        //    return composer.Scalar;
        //}

        //public static T ESpReverse<T>(this IGaMultivectorStorage<T> storage1)
        //{
        //    var scalarProcessor = storage1.ScalarProcessor;

        //    return storage1 switch
        //    {
        //        IGaScalarStorage<T> scalarStorage1 => scalarProcessor.Times(scalarStorage1.Scalar, scalarStorage1.Scalar),
        //        IGaVectorStorage<T> vectorStorage1 => ESp(scalarProcessor, vectorStorage1.GetScalars()),
        //        _ => ESpReverse(scalarProcessor, storage1.GetIdScalarDictionary())
        //    };
        //}

        //public static T ESpReverse<T>(this GaMultivector<T> mv1)
        //{
        //    return ESpReverse(mv1.Storage);
        //}

        //public static T SpReverse<T>(this GaMultivector<T> mv1, GaMultivectorsProcessorBase<T> processor)
        //{
        //    return processor.SpReverse(mv1.Storage);
        //}
        
        
        //private static IGaMultivectorStorage<T> ELcp<T>(IGaScalarProcessor<T> scalarProcessor, int grade1, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs1, int grade2, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs2)
        //{
        //    if (grade1 > grade2)
        //        return GaScalarTermStorage<T>.CreateZero(scalarProcessor);

        //    var composer = new GaKVectorStorageComposer<T>(scalarProcessor, grade2 - grade1);

        //    var idScalarDictionary2 = indexScalarPairs2.ToDictionary(
        //        pair => GaBasisUtils.BasisBladeId(grade2, pair.Key),
        //        pair => pair.Value
        //    );

        //    foreach (var (index1, scalar1) in indexScalarPairs1)
        //    {
        //        var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

        //        foreach (var (id2, scalar2) in idScalarDictionary2)
        //        {
        //            if (!GaBasisUtils.IsNonZeroELcp(id1, id2))
        //                continue;

        //            var id = id1 ^ id2;
        //            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
        //                ? scalarProcessor.NegativeTimes(scalar1, scalar2)
        //                : scalarProcessor.Times(scalar1, scalar2);

        //            composer.AddTerm(id.BasisBladeIndex(), scalar);
        //        }    
        //    }

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> GptELcp<T>(this IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        //{
        //    var composer = new GaMultivectorTermsStorageComposer<T>(storage1.ScalarProcessor);

        //    composer.AddTerms(
        //        GaGbtProductsStack2<T>.Create(storage1, storage2)
        //            .GetELcpIdScalarPairs()
        //    );

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> ELcp<T>(this IGaMultivectorStorage<T> mvStorage1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    var scalarProcessor = mvStorage1.ScalarProcessor;

        //    return mvStorage1 switch
        //    {
        //        IGaKVectorStorage<T> kVectorStorage1 => mvStorage2 switch
        //        {
        //            IGaKVectorStorage<T> kVectorStorage2 => ELcp(scalarProcessor, kVectorStorage1.Grade, kVectorStorage1.GetIndexScalarPairs(), kVectorStorage2.Grade, kVectorStorage2.GetIndexScalarPairs()),
        //            _ => GptELcp(mvStorage1, mvStorage2)
        //        },

        //        _ => GptELcp(mvStorage1, mvStorage2)
        //    };
        //}
        
        //public static IGaMultivectorStorage<T> ELcp<T>(this IGaMultivectorStorage<T> mvStorage1, T mv2)
        //{
        //    return ELcp(mvStorage1, GaScalarTermStorage<T>.Create(mvStorage1.ScalarProcessor, mv2));
        //}
        
        //public static IGaMultivectorStorage<T> ELcp<T>(this T mv1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    return ELcp(GaScalarTermStorage<T>.Create(mvStorage2.ScalarProcessor, mv1), mvStorage2);
        //}

        //public static GaMultivector<T> ELcp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    return new(
        //        ELcp(mv1.Storage, mv2.Storage)
        //    );
        //}
        

        //private static IGaMultivectorStorage<T> ERcp<T>(IGaScalarProcessor<T> scalarProcessor, int grade1, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs1, int grade2, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs2)
        //{
        //    if (grade1 < grade2)
        //        return GaScalarTermStorage<T>.CreateZero(scalarProcessor);

        //    var composer = new GaKVectorStorageComposer<T>(scalarProcessor, grade1 - grade2);

        //    var idScalarDictionary2 = indexScalarPairs2.ToDictionary(
        //        pair => GaBasisUtils.BasisBladeId(grade2, pair.Key),
        //        pair => pair.Value
        //    );

        //    foreach (var (index1, scalar1) in indexScalarPairs1)
        //    {
        //        var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

        //        foreach (var (id2, scalar2) in idScalarDictionary2)
        //        {
        //            if (!GaBasisUtils.IsNonZeroERcp(id1, id2))
        //                continue;

        //            var id = id1 ^ id2;
        //            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
        //                ? scalarProcessor.NegativeTimes(scalar1, scalar2)
        //                : scalarProcessor.Times(scalar1, scalar2);

        //            composer.AddTerm(id.BasisBladeIndex(), scalar);
        //        }    
        //    }

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> GptERcp<T>(this IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        //{
        //    var composer = new GaMultivectorTermsStorageComposer<T>(storage1.ScalarProcessor);

        //    composer.AddTerms(
        //        GaGbtProductsStack2<T>.Create(storage1, storage2)
        //            .GetERcpIdScalarPairs()
        //    );

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> ERcp<T>(this IGaMultivectorStorage<T> mvStorage1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    var scalarProcessor = mvStorage1.ScalarProcessor;

        //    return mvStorage1 switch
        //    {
        //        IGaKVectorStorage<T> kVectorStorage1 => mvStorage2 switch
        //        {
        //            IGaKVectorStorage<T> kVectorStorage2 => ERcp(scalarProcessor, kVectorStorage1.Grade, kVectorStorage1.GetIndexScalarPairs(), kVectorStorage2.Grade, kVectorStorage2.GetIndexScalarPairs()),
        //            _ => GptERcp(mvStorage1, mvStorage2)
        //        },

        //        _ => GptERcp(mvStorage1, mvStorage2)
        //    };
        //}
        
        //public static IGaMultivectorStorage<T> ERcp<T>(this IGaMultivectorStorage<T> mvStorage1, T mv2)
        //{
        //    return ERcp(mvStorage1, GaScalarTermStorage<T>.Create(mvStorage1.ScalarProcessor, mv2));
        //}
        
        //public static IGaMultivectorStorage<T> ERcp<T>(this T mv1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    return ERcp(GaScalarTermStorage<T>.Create(mvStorage2.ScalarProcessor, mv1), mvStorage2);
        //}

        //public static GaMultivector<T> ERcp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    return new(
        //        ERcp(mv1.Storage, mv2.Storage)
        //    );
        //}
        
        
        //private static IGaMultivectorStorage<T> EHip<T>(IGaScalarProcessor<T> scalarProcessor, int grade1, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs1, int grade2, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs2)
        //{
        //    if (grade1 == 0 || grade2 == 0)
        //        return GaScalarTermStorage<T>.CreateZero(scalarProcessor);

        //    var composer = new GaKVectorStorageComposer<T>(
        //        scalarProcessor, 
        //        Math.Abs(grade1 - grade2)
        //    );

        //    var idScalarDictionary2 = indexScalarPairs2.ToDictionary(
        //        pair => GaBasisUtils.BasisBladeId(grade2, pair.Key),
        //        pair => pair.Value
        //    );

        //    foreach (var (index1, scalar1) in indexScalarPairs1)
        //    {
        //        var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

        //        foreach (var (id2, scalar2) in idScalarDictionary2)
        //        {
        //            if (!GaBasisUtils.IsNonZeroEHip(id1, id2))
        //                continue;

        //            var id = id1 ^ id2;
        //            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
        //                ? scalarProcessor.NegativeTimes(scalar1, scalar2)
        //                : scalarProcessor.Times(scalar1, scalar2);

        //            composer.AddTerm(id.BasisBladeIndex(), scalar);
        //        }    
        //    }

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> GptEHip<T>(this IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        //{
        //    var composer = new GaMultivectorTermsStorageComposer<T>(storage1.ScalarProcessor);

        //    composer.AddTerms(
        //        GaGbtProductsStack2<T>.Create(storage1, storage2)
        //            .GetEHipIdScalarPairs()
        //    );

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> EHip<T>(this IGaMultivectorStorage<T> mvStorage1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    var scalarProcessor = mvStorage1.ScalarProcessor;

        //    return mvStorage1 switch
        //    {
        //        IGaKVectorStorage<T> kVectorStorage1 => mvStorage2 switch
        //        {
        //            IGaKVectorStorage<T> kVectorStorage2 => EHip(scalarProcessor, kVectorStorage1.Grade, kVectorStorage1.GetIndexScalarPairs(), kVectorStorage2.Grade, kVectorStorage2.GetIndexScalarPairs()),
        //            _ => GptEHip(mvStorage1, mvStorage2)
        //        },

        //        _ => GptEHip(mvStorage1, mvStorage2)
        //    };
        //}
        
        //public static IGaMultivectorStorage<T> EHip<T>(this IGaMultivectorStorage<T> mvStorage1, T mv2)
        //{
        //    return GaScalarTermStorage<T>.CreateZero(mvStorage1.ScalarProcessor);
        //}
        
        //public static IGaMultivectorStorage<T> EHip<T>(this T mv1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    return GaScalarTermStorage<T>.CreateZero(mvStorage2.ScalarProcessor);
        //}

        //public static GaMultivector<T> EHip<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    return new(
        //        EHip(mv1.Storage, mv2.Storage)
        //    );
        //}
        
        
        //private static IGaMultivectorStorage<T> EFdp<T>(IGaScalarProcessor<T> scalarProcessor, int grade1, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs1, int grade2, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs2)
        //{
        //    var composer = new GaKVectorStorageComposer<T>(
        //        scalarProcessor, 
        //        Math.Abs(grade1 - grade2)
        //    );

        //    var idScalarDictionary2 = indexScalarPairs2.ToDictionary(
        //        pair => GaBasisUtils.BasisBladeId(grade2, pair.Key),
        //        pair => pair.Value
        //    );

        //    foreach (var (index1, scalar1) in indexScalarPairs1)
        //    {
        //        var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

        //        foreach (var (id2, scalar2) in idScalarDictionary2)
        //        {
        //            if (!GaBasisUtils.IsNonZeroEFdp(id1, id2))
        //                continue;

        //            var id = id1 ^ id2;
        //            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
        //                ? scalarProcessor.NegativeTimes(scalar1, scalar2)
        //                : scalarProcessor.Times(scalar1, scalar2);

        //            composer.AddTerm(id.BasisBladeIndex(), scalar);
        //        }    
        //    }

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> GptEFdp<T>(this IGaMultivectorStorage<T> storage1, IGaMultivectorStorage<T> storage2)
        //{
        //    var composer = new GaMultivectorTermsStorageComposer<T>(storage1.ScalarProcessor);

        //    composer.AddTerms(
        //        GaGbtProductsStack2<T>.Create(storage1, storage2)
        //            .GetEFdpIdScalarPairs()
        //    );

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> EFdp<T>(this IGaMultivectorStorage<T> mvStorage1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    var scalarProcessor = mvStorage1.ScalarProcessor;

        //    return mvStorage1 switch
        //    {
        //        IGaKVectorStorage<T> kVectorStorage1 => mvStorage2 switch
        //        {
        //            IGaKVectorStorage<T> kVectorStorage2 => EFdp(scalarProcessor, kVectorStorage1.Grade, kVectorStorage1.GetIndexScalarPairs(), kVectorStorage2.Grade, kVectorStorage2.GetIndexScalarPairs()),
        //            _ => GptEFdp(mvStorage1, mvStorage2)
        //        },

        //        _ => GptEFdp(mvStorage1, mvStorage2)
        //    };
        //}
        
        //public static IGaMultivectorStorage<T> EFdp<T>(this IGaMultivectorStorage<T> mvStorage1, T mv2)
        //{
        //    return EFdp(mvStorage1, GaScalarTermStorage<T>.Create(mvStorage1.ScalarProcessor, mv2));
        //}
        
        //public static IGaMultivectorStorage<T> EFdp<T>(this T mv1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    return EFdp(GaScalarTermStorage<T>.Create(mvStorage2.ScalarProcessor, mv1), mvStorage2);
        //}

        //public static GaMultivector<T> EFdp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    return new(
        //        EFdp(mv1.Storage, mv2.Storage)
        //    );
        //}
        
        
        //private static IGaMultivectorStorage<T> EAcp<T>(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs1, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs2)
        //{
        //    var composer = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

        //    var idScalarDictionary2 = idScalarPairs2.ToDictionary(
        //        pair => pair.Key,
        //        pair => pair.Value
        //    );

        //    foreach (var (id1, scalar1) in idScalarPairs1)
        //    {
        //        foreach (var (id2, scalar2) in idScalarDictionary2)
        //        {
        //            if (!GaBasisUtils.IsNonZeroEAcp(id1, id2))
        //                continue;

        //            var id = id1 ^ id2;
        //            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
        //                ? scalarProcessor.NegativeTimes(scalar1, scalar2)
        //                : scalarProcessor.Times(scalar1, scalar2);

        //            composer.AddTerm(id, scalar);
        //        }    
        //    }

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> EAcp<T>(this IGaMultivectorStorage<T> mvStorage1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    var scalarProcessor = mvStorage1.ScalarProcessor;

        //    return EAcp(
        //        scalarProcessor, 
        //        mvStorage1.GetIdScalarPairs(), 
        //        mvStorage2.GetIdScalarPairs()
        //    );
        //}

        //public static GaMultivector<T> EAcp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    return new(
        //        EAcp(mv1.Storage, mv2.Storage)
        //    );
        //}

        //public static GaMultivector<T> Acp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaMultivectorsProcessorBase<T> processor)
        //{
        //    return new(
        //        processor.Acp(mv1.Storage, mv2.Storage)
        //    );
        //}
        

        //private static IGaMultivectorStorage<T> ECp<T>(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs1, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs2)
        //{
        //    var composer = new GaMultivectorTermsStorageComposer<T>(scalarProcessor);

        //    var idScalarDictionary2 = idScalarPairs2.ToDictionary(
        //        pair => pair.Key,
        //        pair => pair.Value
        //    );

        //    foreach (var (id1, scalar1) in idScalarPairs1)
        //    {
        //        foreach (var (id2, scalar2) in idScalarDictionary2)
        //        {
        //            if (!GaBasisUtils.IsNonZeroECp(id1, id2))
        //                continue;

        //            var id = id1 ^ id2;
        //            var scalar = GaBasisUtils.IsNegativeEGp(id1, id2)
        //                ? scalarProcessor.NegativeTimes(scalar1, scalar2)
        //                : scalarProcessor.Times(scalar1, scalar2);

        //            composer.AddTerm(id, scalar);
        //        }    
        //    }

        //    composer.RemoveZeroTerms();

        //    return composer.GetCompactStorage();
        //}

        //public static IGaMultivectorStorage<T> ECp<T>(this IGaMultivectorStorage<T> mvStorage1, IGaMultivectorStorage<T> mvStorage2)
        //{
        //    var scalarProcessor = mvStorage1.ScalarProcessor;

        //    return ECp(
        //        scalarProcessor, 
        //        mvStorage1.GetIdScalarPairs(), 
        //        mvStorage2.GetIdScalarPairs()
        //    );
        //}
        
        //public static IGaMultivectorStorage<T> ECp<T>(this IGaMultivectorStorage<T> mvStorage1, T mv2)
        //{
        //    return GaScalarTermStorage<T>.CreateZero(mvStorage1.ScalarProcessor);
        //}

        //public static GaMultivector<T> ECp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2)
        //{
        //    return new(
        //        ECp(mv1.Storage, mv2.Storage)
        //    );
        //}

        //public static GaMultivector<T> Cp<T>(this GaMultivector<T> mv1, GaMultivector<T> mv2, GaMultivectorsProcessorBase<T> processor)
        //{
        //    return new(
        //        processor.Cp(mv1.Storage, mv2.Storage)
        //    );
        //}

        
        //private static T ENormSquared<T>(IGaScalarProcessor<T> scalarProcessor, IEnumerable<T> scalars1)
        //{
        //    return
        //        scalars1
        //            .Select(scalar => scalarProcessor.Times(scalar, scalar))
        //            .Aggregate(
        //                scalarProcessor.ZeroScalar, 
        //                scalarProcessor.Add
        //            );
        //}

        //private static T ENormSquared<T>(IGaScalarProcessor<T> scalarProcessor, int grade1, IEnumerable<KeyValuePair<ulong, T>> indexScalarPairs1)
        //{
        //    var composer = new GaScalarStorageComposer<T>(scalarProcessor);

        //    var gradeHasNegativeReverse = grade1.GradeHasNegativeReverse();

        //    foreach (var (index1, scalar1) in indexScalarPairs1)
        //    {
        //        var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

        //        var scalar = gradeHasNegativeReverse ^ GaBasisUtils.IsNegativeEGp(id1, id1)
        //            ? scalarProcessor.Times(scalar1, scalar1)
        //            : scalarProcessor.NegativeTimes(scalar1, scalar1);

        //        composer.AddScalar(scalar);
        //    }

        //    return composer.Scalar;
        //}

        //private static T ENormSquared<T>(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<int, Dictionary<ulong, T>>> gradeIndexScalarDictionaryPairs1)
        //{
        //    var composer = new GaScalarStorageComposer<T>(scalarProcessor);

        //    foreach (var (grade1, indexScalarPairs1) in gradeIndexScalarDictionaryPairs1)
        //    {
        //        var gradeHasNegativeReverse = grade1.GradeHasNegativeReverse();

        //        foreach (var (index1, scalar1) in indexScalarPairs1)
        //        {
        //            var id1 = GaBasisUtils.BasisBladeId(grade1, index1);

        //            var scalar = gradeHasNegativeReverse ^ GaBasisUtils.IsNegativeEGp(id1, id1)
        //                ? scalarProcessor.Times(scalar1, scalar1)
        //                : scalarProcessor.NegativeTimes(scalar1, scalar1);

        //            composer.AddScalar(scalar);
        //        }
        //    }

        //    return composer.Scalar;
        //}
        
        //private static T ENormSquared<T>(IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs1)
        //{
        //    var composer = new GaScalarStorageComposer<T>(scalarProcessor);

        //    foreach (var (id1, scalar1) in idScalarPairs1)
        //    {
        //        var scalar = id1.BasisBladeIdHasNegativeReverse() ^ GaBasisUtils.IsNegativeEGp(id1, id1)
        //            ? scalarProcessor.Times(scalar1, scalar1)
        //            : scalarProcessor.NegativeTimes(scalar1, scalar1);

        //        composer.AddScalar(scalar);
        //    }

        //    return composer.Scalar;
        //}

        //public static T ENormSquared<T>(this IGaMultivectorStorage<T> storage1)
        //{
        //    var scalarProcessor = storage1.ScalarProcessor;

        //    return storage1 switch
        //    {
        //        IGaScalarStorage<T> scalarStorage1 => scalarProcessor.Times(scalarStorage1.Scalar, scalarStorage1.Scalar),
        //        IGaVectorStorage<T> vectorStorage1 => ENormSquared(scalarProcessor, vectorStorage1.GetScalars()),
        //        IGaKVectorStorage<T> kVectorStorage1 => ENormSquared(scalarProcessor, kVectorStorage1.Grade, kVectorStorage1.GetIndexScalarPairs()),
        //        GaMultivectorGradedStorage<T> gradedMultivectorStorage1 => ENormSquared(scalarProcessor, gradedMultivectorStorage1.GetGradeIndexScalarDictionary()),
        //        _ => ENormSquared(scalarProcessor, storage1.GetIdScalarPairs())
        //    };
        //}

        //public static T ENormSquared<T>(this GaMultivector<T> mv1)
        //{
        //    return ENormSquared(mv1.Storage);
        //}
        
        //public static T NormSquared<T>(this GaMultivector<T> mv1, GaMultivectorsProcessorBase<T> processor)
        //{
        //    return processor.NormSquared(mv1.Storage);
        //}

        //public static T NormSquared<T>(this IGaMultivectorStorage<T> storage1, IGaMultivectorProcessor<T> processor)
        //{
        //    return processor.NormSquared(storage1);
        //}

        //public static T ENorm<T>(this IGaMultivectorStorage<T> storage1)
        //{
        //    return storage1.ScalarProcessor.SqrtOfAbs(
        //        ENormSquared(storage1)
        //    );
        //}

        //public static T ENorm<T>(this GaMultivector<T> mv1)
        //{
        //    return mv1.ScalarProcessor.SqrtOfAbs(
        //        ENormSquared(mv1.Storage)
        //    );
        //}

        //public static T Norm<T>(this IGaMultivectorStorage<T> storage1, IGaMultivectorProcessor<T> processor)
        //{
        //    return processor.Norm(storage1);
        //}
    }
}