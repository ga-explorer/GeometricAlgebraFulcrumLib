using System;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class LinMatrixStorageMappingUtils
    {
        
        public static ILinMatrixDenseStorage<T> MapScalarsIndicesUnion<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixDenseStorage<T> v1, ILinMatrixDenseStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() && v2.IsEmpty())
                return LinMatrixEmptyStorage<T>.EmptyStorage;

            var minCount1 = (ulong) Math.Min(v1.Count1, v2.Count1);
            var minCount2 = (ulong) Math.Min(v1.Count2, v2.Count2);

            var maxCount1 = (ulong) Math.Max(v1.Count1, v2.Count1);
            var maxCount2 = (ulong) Math.Max(v1.Count2, v2.Count2);

            var composer = 
                scalarProcessor.CreateLinMatrixDenseStorageComposer(
                    (int) maxCount1, 
                    (int) maxCount2
                );

            for (var key1 = 0UL; key1 < minCount1; key1++)
            for (var key2 = 0UL; key2 < minCount2; key2++)
                composer.SetTerm(
                    key1,
                    key2, 
                    valueMapping(
                        v1.GetScalar(key1, key2), 
                        v2.GetScalar(key1, key2)
                    )
                );

            if (v1.Count1 > v2.Count1)
            {
                if (v1.Count2 > v2.Count2)
                {
                    for (var key1 = 0UL; key1 < minCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                v1.GetScalar(key1, key2), 
                                scalarProcessor.ScalarZero
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = 0UL; key2 < minCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                v1.GetScalar(key1, key2), 
                                scalarProcessor.ScalarZero
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                v1.GetScalar(key1, key2), 
                                scalarProcessor.ScalarZero
                            )
                        );
                }
                else if (v2.Count2 > v1.Count2)
                {
                    for (var key1 = 0UL; key1 < minCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.ScalarZero,
                                v2.GetScalar(key1, key2)
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = 0UL; key2 < minCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                v1.GetScalar(key1, key2), 
                                scalarProcessor.ScalarZero
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.ScalarZero,
                                v2.GetScalar(key1, key2)
                            )
                        );
                }
            }
            else if (v2.Count > v1.Count)
            {
                if (v1.Count2 > v2.Count2)
                {
                    for (var key1 = 0UL; key1 < minCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                v1.GetScalar(key1, key2), 
                                scalarProcessor.ScalarZero
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = 0UL; key2 < minCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.ScalarZero,
                                v2.GetScalar(key1, key2)
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.ScalarZero,
                                v2.GetScalar(key1, key2)
                            )
                        );
                }
                else if (v2.Count2 > v1.Count2)
                {
                    for (var key1 = 0UL; key1 < minCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.ScalarZero,
                                v2.GetScalar(key1, key2)
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = 0UL; key2 < minCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.ScalarZero,
                                v2.GetScalar(key1, key2)
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.ScalarZero,
                                v2.GetScalar(key1, key2)
                            )
                        );
                }
            }

            return composer.RemoveZeroTerms().CreateLinMatrixDenseStorage();
        }
        
        public static ILinMatrixStorage<T> MapScalarsIndicesUnion<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> v1, ILinMatrixStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() && v2.IsEmpty())
                return LinMatrixEmptyStorage<T>.EmptyStorage;

            if (v1 is ILinMatrixDenseStorage<T> dv1 && v2 is ILinMatrixDenseStorage<T> dv2)
                return scalarProcessor.MapScalarsIndicesUnion(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateLinMatrixSparseStorageComposer();

            var keysSet = v1.GetIndicesUnion(v2);

            foreach (var (key1, key2) in keysSet)
                composer.SetTerm(
                    key1,
                    key2,
                    valueMapping(
                        v1.GetScalar(key1, key2, scalarProcessor.ScalarZero),
                        v2.GetScalar(key1, key2, scalarProcessor.ScalarZero)
                    )
                );

            return composer
                .RemoveZeroTerms()
                .CreateLinMatrixStorage();
        }
        
        public static ILinMatrixDenseStorage<T> MapScalarsIndicesIntersection<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixDenseStorage<T> v1, ILinMatrixDenseStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LinMatrixEmptyStorage<T>.EmptyStorage;

            var minCount1 = (ulong) Math.Min(v1.Count1, v2.Count1);
            var minCount2 = (ulong) Math.Min(v1.Count2, v2.Count2);

            var composer = scalarProcessor.CreateLinMatrixDenseStorageComposer((int) minCount1, (int) minCount2);

            for (var key1 = 0UL; key1 < minCount1; key1++)
            for (var key2 = 0UL; key2 < minCount2; key2++)
                composer.SetTerm(
                    key1,
                    key2, 
                    valueMapping(
                        v1.GetScalar(key1, key2), 
                        v2.GetScalar(key1, key2)
                    )
                );
            
            return composer.RemoveZeroTerms().CreateLinMatrixDenseStorage();
        }

        public static ILinMatrixStorage<T> MapScalarsIndicesIntersection<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> v1, ILinMatrixStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LinMatrixEmptyStorage<T>.EmptyStorage;

            if (v1 is ILinMatrixDenseStorage<T> dv1 && v2 is ILinMatrixDenseStorage<T> dv2)
                return scalarProcessor.MapScalarsIndicesIntersection(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateLinMatrixSparseStorageComposer();

            var keysSet = v1.GetIndicesIntersection(v2);

            foreach (var (key1, key2) in keysSet)
                composer.SetTerm(
                    key1,
                    key2,
                    valueMapping(
                        v1.GetScalar(key1, key2),
                        v2.GetScalar(key1, key2)
                    )
                );

            return composer
                .RemoveZeroTerms()
                .CreateLinMatrixStorage();
        }

        public static ILinMatrixDenseStorage<T> MapScalarsOuter<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorDenseStorage<T> v1, ILinVectorDenseStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LinMatrixEmptyStorage<T>.EmptyStorage;

            var count1 = v1.Count;
            var count2 = v2.Count;
            var composer = scalarProcessor.CreateLinMatrixDenseStorageComposer(count1, count2);

            foreach (var (key1, scalar1) in v1.GetIndexScalarRecords())
            foreach (var (key2, scalar2) in v2.GetIndexScalarRecords())
                composer.SetTerm(key1, key2, valueMapping(scalar1, scalar2));

            return composer.CreateLinMatrixDenseStorage();
        }

        public static ILinMatrixStorage<T> MapScalarsOuter<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinVectorStorage<T> v1, ILinVectorStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LinMatrixEmptyStorage<T>.EmptyStorage;

            if (v1 is ILinVectorDenseStorage<T> dv1 && v2 is ILinVectorDenseStorage<T> dv2)
                return scalarProcessor.MapScalarsOuter(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateLinMatrixSparseStorageComposer();

            foreach (var (key1, scalar1) in v1.GetIndexScalarRecords())
            foreach (var (key2, scalar2) in v2.GetIndexScalarRecords())
                composer.SetTerm(key1, key2, valueMapping(scalar1, scalar2));

            return composer.RemoveZeroTerms().CreateLinMatrixStorage();
        }


    }
}