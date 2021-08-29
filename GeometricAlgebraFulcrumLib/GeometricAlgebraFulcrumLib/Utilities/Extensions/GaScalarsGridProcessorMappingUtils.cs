using System;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaScalarsGridProcessorMappingUtils
    {
        public static ILaVectorDenseEvenStorage<T> MapValuesKeysUnion<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorDenseEvenStorage<T> v1, ILaVectorDenseEvenStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() && v2.IsEmpty())
                return LaVectorEmptyStorage<T>.ZeroStorage;

            var minCount = (ulong) Math.Min(v1.Count, v2.Count);
            var maxCount = (ulong) Math.Max(v1.Count, v2.Count);

            var composer = scalarProcessor.CreateLaVectorDenseEvenStorageComposer((int) minCount);

            for (var i = 0UL; i < minCount; i++)
                composer.SetTerm(
                    i, 
                    valueMapping(v1.GetScalar(i), v2.GetScalar(i))
                );

            if (v1.Count > v2.Count)
            {
                for (var i = minCount; i < maxCount; i++)
                    composer.SetTerm(
                        i, 
                        valueMapping(v1.GetScalar(i), scalarProcessor.ScalarZero)
                    );
            }
            else if (v2.Count > v1.Count)
            {
                for (var i = minCount; i < maxCount; i++)
                    composer.SetTerm(
                        i, 
                        valueMapping(scalarProcessor.ScalarZero, v2.GetScalar(i))
                    );
            }

            return composer.RemoveZeroTerms().CreateLaVectorDenseStorage();
        }
        
        public static ILaVectorEvenStorage<T> MapValuesKeysUnion<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> v1, ILaVectorEvenStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LaVectorEmptyStorage<T>.ZeroStorage;

            if (v1 is ILaVectorDenseEvenStorage<T> dv1 && v2 is ILaVectorDenseEvenStorage<T> dv2)
                return scalarProcessor.MapValuesKeysUnion(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateLaVectorSparseEvenStorageComposer();

            var keysSet = v1.GetKeysUnion(v2);

            foreach (var key in keysSet)
                composer.SetTerm(
                    key,
                    valueMapping(
                        v1.GetValue(key, scalarProcessor.ScalarZero),
                        v2.GetValue(key, scalarProcessor.ScalarZero)
                    )
                );

            return composer
                .RemoveZeroTerms()
                .CreateLaVectorEvenStorage();
        }
        
        public static ILaMatrixDenseEvenStorage<T> MapValuesKeysUnion<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixDenseEvenStorage<T> v1, ILaMatrixDenseEvenStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() && v2.IsEmpty())
                return LaMatrixEmptyStorage<T>.EmptyMatrix;

            var minCount1 = (ulong) Math.Min(v1.Count1, v2.Count1);
            var minCount2 = (ulong) Math.Min(v1.Count2, v2.Count2);

            var maxCount1 = (ulong) Math.Max(v1.Count1, v2.Count1);
            var maxCount2 = (ulong) Math.Max(v1.Count2, v2.Count2);

            var composer = scalarProcessor.CreateLaMatrixDenseEvenStorageComposer(
                (int) minCount1, 
                (int) minCount2
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

            return composer.RemoveZeroTerms().CreateDenseEvenGrid();
        }
        
        public static ILaMatrixEvenStorage<T> MapValuesKeysUnion<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> v1, ILaMatrixEvenStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LaMatrixEmptyStorage<T>.EmptyMatrix;

            if (v1 is ILaMatrixDenseEvenStorage<T> dv1 && v2 is ILaMatrixDenseEvenStorage<T> dv2)
                return scalarProcessor.MapValuesKeysUnion(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateLaMatrixSparseEvenStorageComposer();

            var keysSet = v1.GetKeysUnion(v2);

            foreach (var (key1, key2) in keysSet)
                composer.SetTerm(
                    key1,
                    key2,
                    valueMapping(
                        v1.GetValue(key1, key2, scalarProcessor.ScalarZero),
                        v2.GetValue(key1, key2, scalarProcessor.ScalarZero)
                    )
                );

            return composer
                .RemoveZeroTerms()
                .CreateEvenGrid();
        }

        public static ILaVectorDenseEvenStorage<T> MapValuesKeysIntersection<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorDenseEvenStorage<T> v1, ILaVectorDenseEvenStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LaVectorEmptyStorage<T>.ZeroStorage;

            var count = (ulong) Math.Min(v1.Count, v2.Count);

            var composer = scalarProcessor.CreateLaVectorDenseEvenStorageComposer((int) count);

            for (var i = 0UL; i < count; i++)
                composer.SetTerm(
                    i, 
                    valueMapping(v1.GetScalar(i), v2.GetScalar(i))
                );

            return composer.RemoveZeroTerms().CreateLaVectorDenseStorage();
        }
        
        public static ILaVectorEvenStorage<T> MapValuesKeysIntersection<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> v1, ILaVectorEvenStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LaVectorEmptyStorage<T>.ZeroStorage;

            if (v1 is ILaVectorDenseEvenStorage<T> dv1 && v2 is ILaVectorDenseEvenStorage<T> dv2)
                return scalarProcessor.MapValuesKeysIntersection(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateLaVectorSparseEvenStorageComposer();

            var keysSet = v1.GetKeysIntersection(v2);

            foreach (var key in keysSet)
                composer.SetTerm(
                    key,
                    valueMapping(v1.GetScalar(key), v2.GetScalar(key))
                );

            return composer
                .RemoveZeroTerms()
                .CreateLaVectorEvenStorage();
        }
        
        public static ILaMatrixDenseEvenStorage<T> MapValuesKeysIntersection<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixDenseEvenStorage<T> v1, ILaMatrixDenseEvenStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() && v2.IsEmpty())
                return LaMatrixEmptyStorage<T>.EmptyMatrix;

            var minCount1 = (ulong) Math.Min(v1.Count1, v2.Count1);
            var minCount2 = (ulong) Math.Min(v1.Count2, v2.Count2);

            var composer = scalarProcessor.CreateLaMatrixDenseEvenStorageComposer((int) minCount1, (int) minCount2);

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
            
            return composer.RemoveZeroTerms().CreateDenseEvenGrid();
        }

        public static ILaMatrixEvenStorage<T> MapValuesKeysIntersection<T>(this IScalarProcessor<T> scalarProcessor, ILaMatrixEvenStorage<T> v1, ILaMatrixEvenStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LaMatrixEmptyStorage<T>.EmptyMatrix;

            if (v1 is ILaMatrixDenseEvenStorage<T> dv1 && v2 is ILaMatrixDenseEvenStorage<T> dv2)
                return scalarProcessor.MapValuesKeysIntersection(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateLaMatrixSparseEvenStorageComposer();

            var keysSet = v1.GetKeysIntersection(v2);

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
                .CreateEvenGrid();
        }

        public static ILaMatrixDenseEvenStorage<T> MapValuesOuter<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorDenseEvenStorage<T> v1, ILaVectorDenseEvenStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LaMatrixEmptyStorage<T>.EmptyMatrix;

            var count1 = v1.Count;
            var count2 = v2.Count;
            var composer = scalarProcessor.CreateLaMatrixDenseEvenStorageComposer(count1, count2);

            foreach (var (key1, scalar1) in v1.GetIndexScalarRecords())
            foreach (var (key2, scalar2) in v2.GetIndexScalarRecords())
                composer.SetTerm(key1, key2, valueMapping(scalar1, scalar2));

            return composer.CreateDenseEvenGrid();
        }

        public static ILaMatrixEvenStorage<T> MapValuesOuter<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> v1, ILaVectorEvenStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LaMatrixEmptyStorage<T>.EmptyMatrix;

            if (v1 is ILaVectorDenseEvenStorage<T> dv1 && v2 is ILaVectorDenseEvenStorage<T> dv2)
                return scalarProcessor.MapValuesOuter(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateLaMatrixSparseEvenStorageComposer();

            foreach (var (key1, scalar1) in v1.GetIndexScalarRecords())
            foreach (var (key2, scalar2) in v2.GetIndexScalarRecords())
                composer.SetTerm(key1, key2, valueMapping(scalar1, scalar2));

            return composer.RemoveZeroTerms().CreateEvenGrid();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> evenList, Func<T, T> mappingFunc)
        {
            return scalarProcessor.Add(
                evenList
                    .GetScalars()
                    .Select(mappingFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IScalarProcessor<T> scalarProcessor, ILaVectorEvenStorage<T> evenList, Func<ulong, T, T> mappingFunc)
        {
            return scalarProcessor.Add(
                evenList
                    .GetIndexScalarRecords()
                    .Select(keyValue => 
                        mappingFunc(keyValue.Index, keyValue.Scalar)
                    )
            );
        }
    }
}