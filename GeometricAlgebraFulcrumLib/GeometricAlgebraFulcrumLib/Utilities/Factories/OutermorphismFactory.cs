using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class OutermorphismFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Outermorphism<T> CreateOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, OutermorphismStorage<T> omStorage)
        {
            return new Outermorphism<T>(geometricProcessor, omStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Outermorphism<T> CreateOutermorphism<T>(this OutermorphismStorage<T> omStorage, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new Outermorphism<T>(geometricProcessor, omStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearMapOutermorphism<T> CreateOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, ILinUnilinearMap<T> linearMap)
        {
            return new LinearMapOutermorphism<T>(geometricProcessor, linearMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearMapOutermorphism<T> CreateOutermorphism<T>(this ILinUnilinearMap<T> linearMap, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new LinearMapOutermorphism<T>(geometricProcessor, linearMap);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DiagonalLinearMapOutermorphism<T> CreateDiagonalAutomorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyList<T> basisVectorsSignatures)
        {
            return new DiagonalLinearMapOutermorphism<T>(
                geometricProcessor,
                basisVectorsSignatures
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DiagonalLinearMapOutermorphism<T> CreateDiagonalAutomorphism<T>(this IReadOnlyList<T> basisVectorsSignatures, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            return new DiagonalLinearMapOutermorphism<T>(
                geometricProcessor,
                basisVectorsSignatures
            );
        }


        //public static LinearMapOutermorphism<T> CreateComputedOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<int, VectorStorage<T>> basisVectorsSignatures)
        //{
        //    var vSpaceDimension = geometricProcessor.VSpaceDimension;
        //    var mappedBasisVectors = new VectorStorage<T>[vSpaceDimension];

        //    foreach (var (index, scalar) in basisVectorsSignatures)
        //        mappedBasisVectors[index] = scalar;

        //    for (var i = 0; i < vSpaceDimension; i++)
        //        mappedBasisVectors[i] ??= geometricProcessor.CreateVectorZeroStorage();

        //    return new LinearMapOutermorphism<T>(
        //        geometricProcessor,
        //        mappedBasisVectors
        //    );
        //}

        //public static LinearMapOutermorphism<T> CreateComputedOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, uint vSpaceDimension, IReadOnlyDictionary<uint, VectorStorage<T>> basisVectorsSignatures)
        //{
        //    var mappedBasisVectors = new VectorStorage<T>[vSpaceDimension];

        //    foreach (var (index, scalar) in basisVectorsSignatures)
        //        mappedBasisVectors[index] = scalar;

        //    for (var i = 0; i < vSpaceDimension; i++)
        //        mappedBasisVectors[i] ??= geometricProcessor.CreateVectorZeroStorage();

        //    return new LinearMapOutermorphism<T>(
        //        geometricProcessor,
        //        mappedBasisVectors
        //    );
        //}

        public static LinearMapOutermorphism<T> CreateLinearMapOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IReadOnlyDictionary<ulong, VectorStorage<T>> basisVectorsSignatures)
        {
            var matrix = basisVectorsSignatures.ToDictionary(
                r => r.Key,
                r => r.Value.GetLinVectorIndexScalarStorage()
            ).CreateLinMatrixColumnsListStorage();
            
            return geometricProcessor
                .CreateLinUnilinearMap(matrix)
                .CreateOutermorphism(geometricProcessor);
        }



        public static LinearMapOutermorphism<T> CreateLinearMapOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, T[,] mappedBasisVectorsArray)
        {
            var matrix =
                mappedBasisVectorsArray.CreateLinMatrixDenseStorage();
            
            return geometricProcessor
                .CreateLinUnilinearMap(matrix)
                .CreateOutermorphism(geometricProcessor);
        }

        public static LinearMapOutermorphism<T> CreateLinearMapOutermorphism<T>(this T[,] mappedBasisVectorsArray, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            var matrix =
                mappedBasisVectorsArray.CreateLinMatrixDenseStorage();
            
            return geometricProcessor
                .CreateLinUnilinearMap(matrix)
                .CreateOutermorphism(geometricProcessor);
        }


        public static LinearMapOutermorphism<T> CreateLinearMapOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<VectorStorage<T>> mappedBasisVectors)
        {
            ILinVectorStorage<ILinVectorStorage<T>> storage =
                mappedBasisVectors
                    .Select(v => v.GetLinVectorIndexScalarStorage())
                    .CreateLinVectorArrayStorage();

            var matrix = storage.CreateLinMatrixColumnsListStorage();
            
            return geometricProcessor
                .CreateLinUnilinearMap(matrix)
                .CreateOutermorphism(geometricProcessor);
        }

        public static LinearMapOutermorphism<T> CreateLinearMapOutermorphism<T>(this IEnumerable<VectorStorage<T>> mappedBasisVectors, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            ILinVectorStorage<ILinVectorStorage<T>> storage = mappedBasisVectors
                .Select(v => v.GetLinVectorIndexScalarStorage())
                .CreateLinVectorArrayStorage();

            var matrix = storage.CreateLinMatrixColumnsListStorage();

            return geometricProcessor
                .CreateLinUnilinearMap(matrix)
                .CreateOutermorphism(geometricProcessor);
        }


        public static IOutermorphism<T> CreateComputedOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int basisVectorsCount, Func<int, VectorStorage<T>> basisVectorMapFunc)
        {
            var mappedBasisVectors = 
                Enumerable
                    .Range(0, basisVectorsCount)
                    .Select(basisVectorMapFunc)
                    .ToArray();

            return geometricProcessor.CreateLinearMapOutermorphism(mappedBasisVectors);
        }
        
        public static IOutermorphism<T> CreateComputedOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, int basisVectorsCount, Func<VectorStorage<T>, VectorStorage<T>> basisVectorMapFunc)
        {
            var mappedBasisVectors = 
                Enumerable
                    .Range(0, basisVectorsCount)
                    .Select(index =>
                        basisVectorMapFunc(geometricProcessor.CreateVectorStorageBasis(index))
                    ).ToArray();

            return geometricProcessor.CreateLinearMapOutermorphism(mappedBasisVectors);
        }
    }
}