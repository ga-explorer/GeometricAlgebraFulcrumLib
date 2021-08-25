using System;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Binary;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Unary
{
    public static class GaScalarsGridProcessorMappingUtils
    {
        public static IGaListEvenDense<T> MapValuesKeysUnion<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEvenDense<T> v1, IGaListEvenDense<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() && v2.IsEmpty())
                return GaListEvenEmpty<T>.EmptyList;

            var minCount = (ulong) Math.Min(v1.Count, v2.Count);
            var maxCount = (ulong) Math.Max(v1.Count, v2.Count);

            var composer = scalarProcessor.CreateEvenListComposer((int) minCount);

            for (var i = 0UL; i < minCount; i++)
                composer.SetTerm(
                    i, 
                    valueMapping(v1.GetValue(i), v2.GetValue(i))
                );

            if (v1.Count > v2.Count)
            {
                for (var i = minCount; i < maxCount; i++)
                    composer.SetTerm(
                        i, 
                        valueMapping(v1.GetValue(i), scalarProcessor.GetZeroScalar())
                    );
            }
            else if (v2.Count > v1.Count)
            {
                for (var i = minCount; i < maxCount; i++)
                    composer.SetTerm(
                        i, 
                        valueMapping(scalarProcessor.GetZeroScalar(), v2.GetValue(i))
                    );
            }

            return composer.RemoveZeroTerms().CreateDenseEvenList();
        }
        
        public static IGaListEven<T> MapValuesKeysUnion<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> v1, IGaListEven<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return GaListEvenEmpty<T>.EmptyList;

            if (v1 is IGaListEvenDense<T> dv1 && v2 is IGaListEvenDense<T> dv2)
                return scalarProcessor.MapValuesKeysUnion(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateEvenListComposer();

            var keysSet = v1.GetKeysUnion(v2);

            foreach (var key in keysSet)
                composer.SetTerm(
                    key,
                    valueMapping(
                        v1.GetValue(key, scalarProcessor.GetZeroScalar),
                        v2.GetValue(key, scalarProcessor.GetZeroScalar)
                    )
                );

            return composer
                .RemoveZeroTerms()
                .CreateEvenList();
        }
        
        public static IGaGridEvenDense<T> MapValuesKeysUnion<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEvenDense<T> v1, IGaGridEvenDense<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() && v2.IsEmpty())
                return GaGridEvenEmpty<T>.EmptyGrid;

            var minCount1 = (ulong) Math.Min(v1.Count1, v2.Count1);
            var minCount2 = (ulong) Math.Min(v1.Count2, v2.Count2);

            var maxCount1 = (ulong) Math.Max(v1.Count1, v2.Count1);
            var maxCount2 = (ulong) Math.Max(v1.Count2, v2.Count2);

            var composer = scalarProcessor.CreateEvenGridComposer(
                (int) minCount1, 
                (int) minCount2
            );

            for (var key1 = 0UL; key1 < minCount1; key1++)
            for (var key2 = 0UL; key2 < minCount2; key2++)
                composer.SetTerm(
                    key1,
                    key2, 
                    valueMapping(
                        v1.GetValue(key1, key2), 
                        v2.GetValue(key1, key2)
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
                                v1.GetValue(key1, key2), 
                                scalarProcessor.GetZeroScalar()
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = 0UL; key2 < minCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                v1.GetValue(key1, key2), 
                                scalarProcessor.GetZeroScalar()
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                v1.GetValue(key1, key2), 
                                scalarProcessor.GetZeroScalar()
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
                                scalarProcessor.GetZeroScalar(),
                                v2.GetValue(key1, key2)
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = 0UL; key2 < minCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                v1.GetValue(key1, key2), 
                                scalarProcessor.GetZeroScalar()
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.GetZeroScalar(),
                                v2.GetValue(key1, key2)
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
                                v1.GetValue(key1, key2), 
                                scalarProcessor.GetZeroScalar()
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = 0UL; key2 < minCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.GetZeroScalar(),
                                v2.GetValue(key1, key2)
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.GetZeroScalar(),
                                v2.GetValue(key1, key2)
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
                                scalarProcessor.GetZeroScalar(),
                                v2.GetValue(key1, key2)
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = 0UL; key2 < minCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.GetZeroScalar(),
                                v2.GetValue(key1, key2)
                            )
                        );

                    for (var key1 = minCount1; key1 < maxCount1; key1++)
                    for (var key2 = minCount2; key2 < maxCount2; key2++)
                        composer.SetTerm(
                            key1,
                            key2, 
                            valueMapping(
                                scalarProcessor.GetZeroScalar(),
                                v2.GetValue(key1, key2)
                            )
                        );
                }
            }

            return composer.RemoveZeroTerms().CreateDenseEvenGrid();
        }
        
        public static IGaGridEven<T> MapValuesKeysUnion<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> v1, IGaGridEven<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return GaGridEvenEmpty<T>.EmptyGrid;

            if (v1 is IGaGridEvenDense<T> dv1 && v2 is IGaGridEvenDense<T> dv2)
                return scalarProcessor.MapValuesKeysUnion(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateEvenGridComposer();

            var keysSet = v1.GetKeysUnion(v2);

            foreach (var (key1, key2) in keysSet)
                composer.SetTerm(
                    key1,
                    key2,
                    valueMapping(
                        v1.GetValue(key1, key2, scalarProcessor.GetZeroScalar),
                        v2.GetValue(key1, key2, scalarProcessor.GetZeroScalar)
                    )
                );

            return composer
                .RemoveZeroTerms()
                .CreateEvenGrid();
        }

        public static IGaListEvenDense<T> MapValuesKeysIntersection<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEvenDense<T> v1, IGaListEvenDense<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return GaListEvenEmpty<T>.EmptyList;

            var count = (ulong) Math.Min(v1.Count, v2.Count);

            var composer = scalarProcessor.CreateEvenListComposer((int) count);

            for (var i = 0UL; i < count; i++)
                composer.SetTerm(
                    i, 
                    valueMapping(v1.GetValue(i), v2.GetValue(i))
                );

            return composer.RemoveZeroTerms().CreateDenseEvenList();
        }
        
        public static IGaListEven<T> MapValuesKeysIntersection<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> v1, IGaListEven<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return GaListEvenEmpty<T>.EmptyList;

            if (v1 is IGaListEvenDense<T> dv1 && v2 is IGaListEvenDense<T> dv2)
                return scalarProcessor.MapValuesKeysIntersection(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateEvenListComposer();

            var keysSet = v1.GetKeysIntersection(v2);

            foreach (var key in keysSet)
                composer.SetTerm(
                    key,
                    valueMapping(v1.GetValue(key), v2.GetValue(key))
                );

            return composer
                .RemoveZeroTerms()
                .CreateEvenList();
        }
        
        public static IGaGridEvenDense<T> MapValuesKeysIntersection<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEvenDense<T> v1, IGaGridEvenDense<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() && v2.IsEmpty())
                return GaGridEvenEmpty<T>.EmptyGrid;

            var minCount1 = (ulong) Math.Min(v1.Count1, v2.Count1);
            var minCount2 = (ulong) Math.Min(v1.Count2, v2.Count2);

            var composer = scalarProcessor.CreateEvenGridComposer((int) minCount1, (int) minCount2);

            for (var key1 = 0UL; key1 < minCount1; key1++)
            for (var key2 = 0UL; key2 < minCount2; key2++)
                composer.SetTerm(
                    key1,
                    key2, 
                    valueMapping(
                        v1.GetValue(key1, key2), 
                        v2.GetValue(key1, key2)
                    )
                );
            
            return composer.RemoveZeroTerms().CreateDenseEvenGrid();
        }

        public static IGaGridEven<T> MapValuesKeysIntersection<T>(this IGaScalarProcessor<T> scalarProcessor, IGaGridEven<T> v1, IGaGridEven<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return GaGridEvenEmpty<T>.EmptyGrid;

            if (v1 is IGaGridEvenDense<T> dv1 && v2 is IGaGridEvenDense<T> dv2)
                return scalarProcessor.MapValuesKeysIntersection(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateEvenGridComposer();

            var keysSet = v1.GetKeysIntersection(v2);

            foreach (var (key1, key2) in keysSet)
                composer.SetTerm(
                    key1,
                    key2,
                    valueMapping(
                        v1.GetValue(key1, key2),
                        v2.GetValue(key1, key2)
                    )
                );

            return composer
                .RemoveZeroTerms()
                .CreateEvenGrid();
        }

        public static IGaGridEvenDense<T> MapValuesOuter<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEvenDense<T> v1, IGaListEvenDense<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return GaGridEvenEmpty<T>.EmptyGrid;

            var count1 = v1.Count;
            var count2 = v2.Count;
            var composer = scalarProcessor.CreateEvenGridComposer(count1, count2);

            foreach (var (key1, scalar1) in v1.GetKeyValueRecords())
            foreach (var (key2, scalar2) in v2.GetKeyValueRecords())
                composer.SetTerm(key1, key2, valueMapping(scalar1, scalar2));

            return composer.CreateDenseEvenGrid();
        }

        public static IGaGridEven<T> MapValuesOuter<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> v1, IGaListEven<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return GaGridEvenEmpty<T>.EmptyGrid;

            if (v1 is IGaListEvenDense<T> dv1 && v2 is IGaListEvenDense<T> dv2)
                return scalarProcessor.MapValuesOuter(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateEvenGridComposer();

            foreach (var (key1, scalar1) in v1.GetKeyValueRecords())
            foreach (var (key2, scalar2) in v2.GetKeyValueRecords())
                composer.SetTerm(key1, key2, valueMapping(scalar1, scalar2));

            return composer.RemoveZeroTerms().CreateEvenGrid();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> evenList, Func<T, T> mappingFunc)
        {
            return scalarProcessor.Add(
                evenList
                    .GetValues()
                    .Select(mappingFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IGaScalarProcessor<T> scalarProcessor, IGaListEven<T> evenList, Func<ulong, T, T> mappingFunc)
        {
            return scalarProcessor.Add(
                evenList
                    .GetKeyValueRecords()
                    .Select(keyValue => 
                        mappingFunc(keyValue.Key, keyValue.Value)
                    )
            );
        }
    }
}