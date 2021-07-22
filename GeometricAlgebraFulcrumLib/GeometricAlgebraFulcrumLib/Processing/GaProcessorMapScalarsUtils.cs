namespace GeometricAlgebraFulcrumLib.Processing
{
    //public static class GaProcessorMapScalarsUtils
    //{
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static IGasScalar<T> MapScalars<T>(this IGasScalar<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return mv.ScalarProcessor.CreateScalar(
    //            mappingFunc(mv.Scalar)
    //        );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static IGasVectorTerm<T> MapScalars<T>(this IGasVectorTerm<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return mv.ScalarProcessor.CreateVector(
    //            mv.Index,
    //            mappingFunc(mv.Scalar)
    //        );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static IGasBivectorTerm<T> MapScalars<T>(this IGasBivectorTerm<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return mv.ScalarProcessor.CreateBivector(
    //            mv.Index,
    //            mappingFunc(mv.Scalar)
    //        );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    private static GaKVectorTermStorage<T> MapScalarsAsGaKVectorTermStorage<T>(IGasKVectorTerm<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return new GaKVectorTermStorage<T>(
    //            mv.ScalarProcessor,
    //            mv.BasisBlade,
    //            mappingFunc(mv.Scalar)
    //        );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static IGasKVectorTerm<T> MapScalars<T>(this IGasKVectorTerm<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return mv switch
    //        {
    //            IGasScalar<T> s => MapScalars(s, mappingFunc),
    //            IGasVectorTerm<T> vt => MapScalars(vt, mappingFunc),
    //            IGasBivectorTerm<T> bvt => MapScalars(bvt, mappingFunc),
    //            _ => MapScalarsAsGaKVectorTermStorage(mv, mappingFunc)
    //        };
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    private static GaVectorStorage<T> MapScalarsAsGaVectorStorage<T>(this IGasVector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        var scalarProcessor = mv.ScalarProcessor;

    //        return scalarProcessor.CreateVector(
    //            mv
    //                .GetIndexScalarDictionary()
    //                .CopyToDictionary(mappingFunc)
    //        );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static IGasVector<T> MapScalars<T>(this IGasVector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return mv is IGasVectorTerm<T> vt
    //            ? MapScalars(vt, mappingFunc)
    //            : MapScalarsAsGaVectorStorage(mv, mappingFunc);
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    private static GaBivectorStorage<T> MapScalarsAsGaBivectorStorage<T>(this IGasBivector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        var scalarProcessor = mv.ScalarProcessor;

    //        return scalarProcessor.CreateBivector(
    //            mv
    //                .GetIndexScalarDictionary()
    //                .CopyToDictionary(
    //                    scalar1 => mappingFunc(scalar1)
    //                )
    //        );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static IGasBivector<T> MapScalars<T>(this IGasBivector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return mv is IGasBivectorTerm<T> bvt
    //            ? MapScalars(bvt, mappingFunc)
    //            : MapScalarsAsGaBivectorStorage(mv, mappingFunc);
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    private static GaKVectorStorage<T> MapScalarsAsGaKVectorStorage<T>(this IGasKVector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        var grade = mv.Grade;
    //        var scalarProcessor = mv.ScalarProcessor;
    //        var indexScalarDictionary = 
    //            mv
    //                .GetIndexScalarDictionary()
    //                .CopyToDictionary(
    //                    mappingFunc
    //                );

    //        return new GaKVectorStorage<T>(
    //            scalarProcessor,
    //            grade,
    //            indexScalarDictionary,
    //            mv.MaxBasisBladeId
    //        );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static IGasKVector<T> MapScalars<T>(this IGasKVector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return mv switch
    //        {
    //            IGasScalar<T> s => MapScalars(s, mappingFunc),
    //            IGasVectorTerm<T> vt => MapScalars(vt, mappingFunc),
    //            IGasBivectorTerm<T> bvt => MapScalars(bvt, mappingFunc),
    //            IGasKVectorTerm<T> kvt => MapScalarsAsGaKVectorTermStorage(kvt, mappingFunc),
    //            IGasVector<T> v => MapScalarsAsGaVectorStorage(v, mappingFunc),
    //            IGasBivector<T> bv => MapScalarsAsGaBivectorStorage(bv, mappingFunc),
    //            _ => MapScalarsAsGaKVectorStorage(mv, mappingFunc)
    //        };
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    private static GaMultivectorGradedStorage<T> MapScalarsAsGaMultivectorGradedStorage<T>(this IGasGradedMultivector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        var scalarProcessor = mv.ScalarProcessor;
    //        var gradeIndexScalarDictionary =
    //            mv
    //                .GetGradeIndexScalarDictionary()
    //                .CopyToDictionary(indexScalarDictionary => 
    //                    indexScalarDictionary.CopyToDictionary(scalar1 => 
    //                        mappingFunc(scalar1)
    //                    )
    //                );

    //        return new GaMultivectorGradedStorage<T>(
    //            scalarProcessor,
    //            gradeIndexScalarDictionary,
    //            mv.MaxBasisBladeId
    //        );
    //    }
        
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static IGasGradedMultivector<T> MapScalars<T>(this IGasGradedMultivector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return mv switch
    //        {
    //            IGasScalar<T> s => MapScalars(s, mappingFunc),
    //            IGasVectorTerm<T> vt => MapScalars(vt, mappingFunc),
    //            IGasBivectorTerm<T> bvt => MapScalars(bvt, mappingFunc),
    //            IGasKVectorTerm<T> kvt => MapScalarsAsGaKVectorTermStorage(kvt, mappingFunc),
    //            IGasVector<T> v => MapScalarsAsGaVectorStorage(v, mappingFunc),
    //            IGasBivector<T> bv => MapScalarsAsGaBivectorStorage(bv, mappingFunc),
    //            IGasKVector<T> kv => MapScalarsAsGaKVectorStorage(kv, mappingFunc),
    //            _ => MapScalarsAsGaMultivectorGradedStorage(mv, mappingFunc)
    //        };
    //    }
        
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    private static GaMultivectorTermsStorage<T> MapScalarsAsGaMultivectorTermsStorage<T>(this IGasTermsMultivector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        var scalarProcessor = mv.ScalarProcessor;
    //        var idScalarDictionary = 
    //            mv
    //                .GetIdScalarDictionary()
    //                .CopyToDictionary(
    //                    scalar1 => mappingFunc(scalar1)
    //                );

    //        return new GaMultivectorTermsStorage<T>(
    //            scalarProcessor,
    //            idScalarDictionary,
    //            mv.MaxBasisBladeId
    //        );
    //    }

    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static IGasTermsMultivector<T> MapScalars<T>(this IGasTermsMultivector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return mv switch
    //        {
    //            IGasScalar<T> s => MapScalars(s, mappingFunc),
    //            IGasVectorTerm<T> vt => MapScalars(vt, mappingFunc),
    //            IGasBivectorTerm<T> bvt => MapScalars(bvt, mappingFunc),
    //            IGasKVectorTerm<T> kvt => MapScalarsAsGaKVectorTermStorage(kvt, mappingFunc),
    //            _ => MapScalarsAsGaMultivectorTermsStorage(mv, mappingFunc)
    //        };
    //    }
        
    //    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    //    public static IGasMultivector<T> MapScalars<T>(this IGasMultivector<T> mv, Func<T, T> mappingFunc)
    //    {
    //        return mv switch
    //        {
    //            IGasScalar<T> s => MapScalars(s, mappingFunc),
    //            IGasVectorTerm<T> vt => MapScalars(vt, mappingFunc),
    //            IGasBivectorTerm<T> bvt => MapScalars(bvt, mappingFunc),
    //            IGasKVectorTerm<T> kvt => MapScalarsAsGaKVectorTermStorage(kvt, mappingFunc),
    //            IGasVector<T> v => MapScalarsAsGaVectorStorage(v, mappingFunc),
    //            IGasBivector<T> bv => MapScalarsAsGaBivectorStorage(bv, mappingFunc),
    //            IGasKVector<T> kv => MapScalarsAsGaKVectorStorage(kv, mappingFunc),
    //            IGasGradedMultivector<T> gmv => MapScalarsAsGaMultivectorGradedStorage(gmv, mappingFunc),
    //            _ => MapScalarsAsGaMultivectorTermsStorage((IGasTermsMultivector<T>) mv, mappingFunc)
    //        };
    //    }
    //}
}