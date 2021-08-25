using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Computed;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Stored;
using GeometricAlgebraFulcrumLib.Processing.ScalarsGrids;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms
{
    public static class OutermorphismFactory
    {
        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IGaScalarsGridProcessor<T> arrayProcessor, uint vSpaceDimension, IReadOnlyDictionary<int, IGaStorageVector<T>> basisVectorsSignatures)
        {
            var mappedBasisVectors = new IGaStorageVector<T>[vSpaceDimension];

            foreach (var (index, scalar) in basisVectorsSignatures)
                mappedBasisVectors[index] = scalar;

            for (var i = 0; i < vSpaceDimension; i++) 
                mappedBasisVectors[i] ??= arrayProcessor.CreateStorageZeroVector();

            return new GaOmComputed<T>(
                arrayProcessor,
                mappedBasisVectors
            );
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IGaScalarsGridProcessor<T> arrayProcessor, uint vSpaceDimension, IReadOnlyDictionary<uint, IGaStorageVector<T>> basisVectorsSignatures)
        {
            var mappedBasisVectors = new IGaStorageVector<T>[vSpaceDimension];

            foreach (var (index, scalar) in basisVectorsSignatures)
                mappedBasisVectors[index] = scalar;

            for (var i = 0; i < vSpaceDimension; i++) 
                mappedBasisVectors[i] ??= arrayProcessor.CreateStorageZeroVector();

            return new GaOmComputed<T>(
                arrayProcessor,
                mappedBasisVectors
            );
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IGaScalarsGridProcessor<T> arrayProcessor, uint vSpaceDimension, IReadOnlyDictionary<ulong, IGaStorageVector<T>> basisVectorsSignatures)
        {
            var mappedBasisVectors = new IGaStorageVector<T>[vSpaceDimension];

            foreach (var (index, scalar) in basisVectorsSignatures)
                mappedBasisVectors[index] = scalar;

            for (var i = 0; i < vSpaceDimension; i++) 
                mappedBasisVectors[i] ??= arrayProcessor.CreateStorageZeroVector();

            return new GaOmComputed<T>(
                arrayProcessor,
                mappedBasisVectors
            );
        }


        public static GaOmComputedDiagonal<T> CreateDiagonalAutomorphism<T>(this IGaScalarsGridProcessor<T> arrayProcessor, IReadOnlyList<T> basisVectorsSignatures)
        {
            return new GaOmComputedDiagonal<T>(
                arrayProcessor,
                basisVectorsSignatures
            );
        }

        public static GaOmComputedDiagonal<T> CreateDiagonalAutomorphism<T>(this IReadOnlyList<T> basisVectorsSignatures, IGaScalarsGridProcessor<T> arrayProcessor)
        {
            return new GaOmComputedDiagonal<T>(
                arrayProcessor,
                basisVectorsSignatures
            );
        }


        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IGaScalarsGridProcessor<T> arrayProcessor, T[,] mappedBasisVectorsArray)
        {
            var mappedBasisVectors =
                mappedBasisVectorsArray.ColumnsToVectorStoragesArray(
                    arrayProcessor
                );

            return new GaOmComputed<T>(arrayProcessor, mappedBasisVectors);
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this T[,] mappedBasisVectorsArray, IGaScalarsGridProcessor<T> arrayProcessor)
        {
            var mappedBasisVectors =
                mappedBasisVectorsArray.ColumnsToVectorStoragesArray(
                    arrayProcessor
                );

            return new GaOmComputed<T>(arrayProcessor, mappedBasisVectors);
        }


        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IGaScalarsGridProcessor<T> arrayProcessor, IReadOnlyList<IGaStorageVector<T>> mappedBasisVectors)
        {
            return new GaOmComputed<T>(
                arrayProcessor, 
                mappedBasisVectors
            );
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IReadOnlyList<IGaStorageVector<T>> mappedBasisVectors, IGaScalarsGridProcessor<T> arrayProcessor)
        {
            return new GaOmComputed<T>(
                arrayProcessor, 
                mappedBasisVectors
            );
        }

        public static GaOmStored<T> CreateStoredOutermorphism<T>(this IGaScalarsGridProcessor<T> arrayProcessor, uint vSpaceDimension)
        {
            return new GaOmStored<T>(arrayProcessor, vSpaceDimension);
        }


        public static IGaOutermorphism<T> CreateComputedOutermorphism<T>(this IGaScalarsGridProcessor<T> arrayProcessor, int basisVectorsCount, Func<int, IGaStorageVector<T>> basisVectorMapFunc)
        {
            var mappedBasisVectors = 
                Enumerable
                    .Range(0, basisVectorsCount)
                    .Select(basisVectorMapFunc)
                    .ToArray();

            return arrayProcessor.CreateComputedOutermorphism(mappedBasisVectors);
        }
        
        public static IGaOutermorphism<T> CreateComputedOutermorphism<T>(this IGaScalarsGridProcessor<T> arrayProcessor, int basisVectorsCount, Func<IGaStorageVector<T>, IGaStorageVector<T>> basisVectorMapFunc)
        {
            var mappedBasisVectors = 
                Enumerable
                    .Range(0, basisVectorsCount)
                    .Select(index =>
                        basisVectorMapFunc(arrayProcessor.CreateStorageBasisVector(index))
                    ).ToArray();

            return arrayProcessor.CreateComputedOutermorphism(mappedBasisVectors);
        }

    }
}