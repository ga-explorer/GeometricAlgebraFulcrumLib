using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.LinearMaps;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Outermorphisms;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class OutermorphismFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Outermorphism<T> CreateOutermorphism<T>(this ILinearAlgebraProcessor<T> linearProcessor, IOutermorphismStorage<T> omStorage)
        {
            return new Outermorphism<T>(linearProcessor, omStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Outermorphism<T> CreateOutermorphism<T>(this IOutermorphismStorage<T> omStorage, ILinearAlgebraProcessor<T> linearProcessor)
        {
            return new Outermorphism<T>(linearProcessor, omStorage);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LinearMapOutermorphism<T> CreateOutermorphism<T>(this ILinUnilinearMap<T> linearMap)
        {
            return new LinearMapOutermorphism<T>(linearMap);
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
                .CreateOutermorphism();
        }



        public static LinearMapOutermorphism<T> CreateLinearMapOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, T[,] mappedBasisVectorsArray)
        {
            var matrix =
                mappedBasisVectorsArray.CreateLinMatrixDenseStorage();
            
            return geometricProcessor
                .CreateLinUnilinearMap(matrix)
                .CreateOutermorphism();
        }

        public static LinearMapOutermorphism<T> CreateLinearMapOutermorphism<T>(this T[,] mappedBasisVectorsArray, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            var matrix =
                mappedBasisVectorsArray.CreateLinMatrixDenseStorage();
            
            return geometricProcessor
                .CreateLinUnilinearMap(matrix)
                .CreateOutermorphism();
        }


        public static LinearMapOutermorphism<T> CreateLinearMapOutermorphism<T>(this IGeometricAlgebraProcessor<T> geometricProcessor, IEnumerable<VectorStorage<T>> mappedBasisVectors)
        {
            var matrix =
                mappedBasisVectors
                    .Select(v => v.GetLinVectorIndexScalarStorage())
                    .CreateLinVectorArrayStorage()
                    .CreateLinMatrixColumnsListStorage();
            
            return geometricProcessor
                .CreateLinUnilinearMap(matrix)
                .CreateOutermorphism();
        }

        public static LinearMapOutermorphism<T> CreateLinearMapOutermorphism<T>(this IEnumerable<VectorStorage<T>> mappedBasisVectors, IGeometricAlgebraProcessor<T> geometricProcessor)
        {
            var matrix =
                mappedBasisVectors
                    .Select(v => v.GetLinVectorIndexScalarStorage())
                    .CreateLinVectorArrayStorage()
                    .CreateLinMatrixColumnsListStorage();

            return geometricProcessor
                .CreateLinUnilinearMap(matrix)
                .CreateOutermorphism();
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
                        basisVectorMapFunc(geometricProcessor.CreateVectorBasisStorage(index))
                    ).ToArray();

            return geometricProcessor.CreateLinearMapOutermorphism(mappedBasisVectors);
        }
    }
}