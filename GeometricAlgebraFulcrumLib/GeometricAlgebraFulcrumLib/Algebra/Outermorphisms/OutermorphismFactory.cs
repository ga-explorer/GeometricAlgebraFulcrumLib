using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Computed;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Stored;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms
{
    public static class OutermorphismFactory
    {
        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension, IReadOnlyDictionary<int, IGaStorageVector<T>> basisVectorsSignatures)
        {
            var mappedBasisVectors = new IGaStorageVector<T>[vSpaceDimension];

            foreach (var (index, scalar) in basisVectorsSignatures)
                mappedBasisVectors[index] = scalar;

            for (var i = 0; i < vSpaceDimension; i++) 
                mappedBasisVectors[i] ??= scalarProcessor.CreateStorageZeroVector();

            return new GaOmComputed<T>(
                scalarProcessor,
                mappedBasisVectors
            );
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension, IReadOnlyDictionary<uint, IGaStorageVector<T>> basisVectorsSignatures)
        {
            var mappedBasisVectors = new IGaStorageVector<T>[vSpaceDimension];

            foreach (var (index, scalar) in basisVectorsSignatures)
                mappedBasisVectors[index] = scalar;

            for (var i = 0; i < vSpaceDimension; i++) 
                mappedBasisVectors[i] ??= scalarProcessor.CreateStorageZeroVector();

            return new GaOmComputed<T>(
                scalarProcessor,
                mappedBasisVectors
            );
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension, IReadOnlyDictionary<ulong, IGaStorageVector<T>> basisVectorsSignatures)
        {
            var mappedBasisVectors = new IGaStorageVector<T>[vSpaceDimension];

            foreach (var (index, scalar) in basisVectorsSignatures)
                mappedBasisVectors[index] = scalar;

            for (var i = 0; i < vSpaceDimension; i++) 
                mappedBasisVectors[i] ??= scalarProcessor.CreateStorageZeroVector();

            return new GaOmComputed<T>(
                scalarProcessor,
                mappedBasisVectors
            );
        }


        public static GaOmComputedDiagonal<T> CreateDiagonalAutomorphism<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<T> basisVectorsSignatures)
        {
            return new GaOmComputedDiagonal<T>(
                scalarProcessor,
                basisVectorsSignatures
            );
        }

        public static GaOmComputedDiagonal<T> CreateDiagonalAutomorphism<T>(this IReadOnlyList<T> basisVectorsSignatures, IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaOmComputedDiagonal<T>(
                scalarProcessor,
                basisVectorsSignatures
            );
        }


        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IGaScalarProcessor<T> scalarProcessor, T[,] mappedBasisVectorsArray)
        {
            var mappedBasisVectors =
                mappedBasisVectorsArray.ColumnsToVectorStoragesArray(
                    scalarProcessor
                );

            return new GaOmComputed<T>(scalarProcessor, mappedBasisVectors);
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this T[,] mappedBasisVectorsArray, IGaScalarProcessor<T> scalarProcessor)
        {
            var mappedBasisVectors =
                mappedBasisVectorsArray.ColumnsToVectorStoragesArray(
                    scalarProcessor
                );

            return new GaOmComputed<T>(scalarProcessor, mappedBasisVectors);
        }


        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyList<IGaStorageVector<T>> mappedBasisVectors)
        {
            return new GaOmComputed<T>(
                scalarProcessor, 
                mappedBasisVectors
            );
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IReadOnlyList<IGaStorageVector<T>> mappedBasisVectors, IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaOmComputed<T>(
                scalarProcessor, 
                mappedBasisVectors
            );
        }

        public static GaOmStored<T> CreateStoredOutermorphism<T>(this IGaScalarProcessor<T> scalarProcessor, uint vSpaceDimension)
        {
            return new GaOmStored<T>(scalarProcessor, vSpaceDimension);
        }


        public static IGaOutermorphism<T> CreateComputedOutermorphism<T>(this IGaScalarProcessor<T> scalarProcessor, int basisVectorsCount, Func<int, IGaStorageVector<T>> basisVectorMapFunc)
        {
            var mappedBasisVectors = 
                Enumerable
                    .Range(0, basisVectorsCount)
                    .Select(basisVectorMapFunc)
                    .ToArray();

            return scalarProcessor.CreateComputedOutermorphism(mappedBasisVectors);
        }
        
        public static IGaOutermorphism<T> CreateComputedOutermorphism<T>(this IGaScalarProcessor<T> scalarProcessor, int basisVectorsCount, Func<IGaStorageVector<T>, IGaStorageVector<T>> basisVectorMapFunc)
        {
            var mappedBasisVectors = 
                Enumerable
                    .Range(0, basisVectorsCount)
                    .Select(index =>
                        basisVectorMapFunc(scalarProcessor.CreateStorageBasisVector(index))
                    ).ToArray();

            return scalarProcessor.CreateComputedOutermorphism(mappedBasisVectors);
        }

    }
}