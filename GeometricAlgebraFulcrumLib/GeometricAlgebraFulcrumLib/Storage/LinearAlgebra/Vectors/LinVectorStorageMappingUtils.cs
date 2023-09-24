using System;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    public static class LinVectorStorageMappingUtils
    {
        public static ILinVectorDenseStorage<T> MapScalarsIndicesUnion<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorDenseStorage<T> v1, ILinVectorDenseStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() && v2.IsEmpty())
                return LinVectorEmptyStorage<T>.EmptyStorage;

            var minCount = (ulong)Math.Min(v1.Count, v2.Count);
            var maxCount = (ulong)Math.Max(v1.Count, v2.Count);

            var composer = scalarProcessor.CreateVectorStorageComposer((int)maxCount);

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

            return composer.RemoveZeroTerms().CreateLinVectorDenseStorage();
        }

        public static ILinVectorStorage<T> MapScalarsIndicesUnion<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> v1, ILinVectorStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() && v2.IsEmpty())
                return LinVectorEmptyStorage<T>.EmptyStorage;

            if (v1 is ILinVectorDenseStorage<T> dv1 && v2 is ILinVectorDenseStorage<T> dv2)
                return scalarProcessor.MapScalarsIndicesUnion(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateVectorStorageComposer();

            var keysSet = v1.GetIndicesUnion(v2);

            foreach (var key in keysSet)
                composer.SetTerm(
                    key,
                    valueMapping(
                        v1.GetScalar(key, scalarProcessor.ScalarZero),
                        v2.GetScalar(key, scalarProcessor.ScalarZero)
                    )
                );

            return composer
                .RemoveZeroTerms()
                .CreateLinVectorStorage();
        }

        public static ILinVectorDenseStorage<T> MapScalars<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorDenseStorage<T> v1, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty())
                return LinVectorEmptyStorage<T>.EmptyStorage;

            var composer = scalarProcessor.CreateVectorStorageComposer(v1.Count);

            foreach (var (index, scalar) in v1.GetIndexScalarRecords())
                composer.SetTerm(
                    index,
                    valueMapping(scalar, scalar)
                );

            return composer.RemoveZeroTerms().CreateLinVectorDenseStorage();
        }

        public static ILinVectorDenseStorage<T> MapScalarsIndicesIntersection<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorDenseStorage<T> v1, ILinVectorDenseStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LinVectorEmptyStorage<T>.EmptyStorage;

            var count = (ulong)Math.Min(v1.Count, v2.Count);

            var composer = scalarProcessor.CreateVectorStorageComposer((int)count);

            for (var i = 0UL; i < count; i++)
                composer.SetTerm(
                    i,
                    valueMapping(v1.GetScalar(i), v2.GetScalar(i))
                );

            return composer.RemoveZeroTerms().CreateLinVectorDenseStorage();
        }

        public static ILinVectorStorage<T> MapScalars<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> v1, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty())
                return LinVectorEmptyStorage<T>.EmptyStorage;

            if (v1 is ILinVectorDenseStorage<T> dv1)
                return scalarProcessor.MapScalars(dv1, valueMapping);

            var composer = scalarProcessor.CreateVectorStorageComposer();

            foreach (var (index, scalar) in v1.GetIndexScalarRecords())
                composer.SetTerm(
                    index,
                    valueMapping(scalar, scalar)
                );

            return composer
                .RemoveZeroTerms()
                .CreateLinVectorStorage();
        }

        public static ILinVectorStorage<T> MapScalarsIndicesIntersection<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> v1, ILinVectorStorage<T> v2, Func<T, T, T> valueMapping)
        {
            if (v1.IsEmpty() || v2.IsEmpty())
                return LinVectorEmptyStorage<T>.EmptyStorage;

            if (v1 is ILinVectorDenseStorage<T> dv1 && v2 is ILinVectorDenseStorage<T> dv2)
                return scalarProcessor.MapScalarsIndicesIntersection(dv1, dv2, valueMapping);

            var composer = scalarProcessor.CreateVectorStorageComposer();

            var keysSet = v1.GetIndicesIntersection(v2);

            foreach (var key in keysSet)
                composer.SetTerm(
                    key,
                    valueMapping(v1.GetScalar(key), v2.GetScalar(key))
                );

            return composer
                .RemoveZeroTerms()
                .CreateLinVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vectorStorage, Func<T, T> mappingFunc)
        {
            return scalarProcessor.Add(
                vectorStorage
                    .GetScalars()
                    .Select(mappingFunc)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddMapped<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vectorStorage, Func<ulong, T, T> mappingFunc)
        {
            return scalarProcessor.Add(
                vectorStorage
                    .GetIndexScalarRecords()
                    .Select(keyValue =>
                        mappingFunc(keyValue.KvIndex, keyValue.Scalar)
                    )
            );
        }
    }
}