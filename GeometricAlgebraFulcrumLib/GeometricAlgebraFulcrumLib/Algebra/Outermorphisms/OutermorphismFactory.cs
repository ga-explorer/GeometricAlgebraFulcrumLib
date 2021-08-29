using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Computed;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms.Stored;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Algebra.Outermorphisms
{
    public static class OutermorphismFactory
    {
        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this ILaProcessor<T> arrayProcessor, uint vSpaceDimension, IReadOnlyDictionary<int, IGaVectorStorage<T>> basisVectorsSignatures)
        {
            var mappedBasisVectors = new IGaVectorStorage<T>[vSpaceDimension];

            foreach (var (index, scalar) in basisVectorsSignatures)
                mappedBasisVectors[index] = scalar;

            for (var i = 0; i < vSpaceDimension; i++) 
                mappedBasisVectors[i] ??= arrayProcessor.CreateGaVectorStorage();

            return new GaOmComputed<T>(
                arrayProcessor,
                mappedBasisVectors
            );
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this ILaProcessor<T> arrayProcessor, uint vSpaceDimension, IReadOnlyDictionary<uint, IGaVectorStorage<T>> basisVectorsSignatures)
        {
            var mappedBasisVectors = new IGaVectorStorage<T>[vSpaceDimension];

            foreach (var (index, scalar) in basisVectorsSignatures)
                mappedBasisVectors[index] = scalar;

            for (var i = 0; i < vSpaceDimension; i++) 
                mappedBasisVectors[i] ??= arrayProcessor.CreateGaVectorStorage();

            return new GaOmComputed<T>(
                arrayProcessor,
                mappedBasisVectors
            );
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this ILaProcessor<T> arrayProcessor, uint vSpaceDimension, IReadOnlyDictionary<ulong, IGaVectorStorage<T>> basisVectorsSignatures)
        {
            var mappedBasisVectors = new IGaVectorStorage<T>[vSpaceDimension];

            foreach (var (index, scalar) in basisVectorsSignatures)
                mappedBasisVectors[index] = scalar;

            for (var i = 0; i < vSpaceDimension; i++) 
                mappedBasisVectors[i] ??= arrayProcessor.CreateGaVectorStorage();

            return new GaOmComputed<T>(
                arrayProcessor,
                mappedBasisVectors
            );
        }


        public static GaOmComputedDiagonal<T> CreateDiagonalAutomorphism<T>(this ILaProcessor<T> arrayProcessor, IReadOnlyList<T> basisVectorsSignatures)
        {
            return new GaOmComputedDiagonal<T>(
                arrayProcessor,
                basisVectorsSignatures
            );
        }

        public static GaOmComputedDiagonal<T> CreateDiagonalAutomorphism<T>(this IReadOnlyList<T> basisVectorsSignatures, ILaProcessor<T> arrayProcessor)
        {
            return new GaOmComputedDiagonal<T>(
                arrayProcessor,
                basisVectorsSignatures
            );
        }


        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this ILaProcessor<T> arrayProcessor, T[,] mappedBasisVectorsArray)
        {
            var mappedBasisVectors =
                mappedBasisVectorsArray.ColumnsToVectorStoragesArray(
                    arrayProcessor
                );

            return new GaOmComputed<T>(arrayProcessor, mappedBasisVectors);
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this T[,] mappedBasisVectorsArray, ILaProcessor<T> arrayProcessor)
        {
            var mappedBasisVectors =
                mappedBasisVectorsArray.ColumnsToVectorStoragesArray(
                    arrayProcessor
                );

            return new GaOmComputed<T>(arrayProcessor, mappedBasisVectors);
        }


        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this ILaProcessor<T> arrayProcessor, IReadOnlyList<IGaVectorStorage<T>> mappedBasisVectors)
        {
            return new GaOmComputed<T>(
                arrayProcessor, 
                mappedBasisVectors
            );
        }

        public static GaOmComputed<T> CreateComputedOutermorphism<T>(this IReadOnlyList<IGaVectorStorage<T>> mappedBasisVectors, ILaProcessor<T> arrayProcessor)
        {
            return new GaOmComputed<T>(
                arrayProcessor, 
                mappedBasisVectors
            );
        }

        public static GaOmStored<T> CreateStoredOutermorphism<T>(this ILaProcessor<T> arrayProcessor, uint vSpaceDimension)
        {
            return new GaOmStored<T>(arrayProcessor, vSpaceDimension);
        }


        public static IGaOutermorphism<T> CreateComputedOutermorphism<T>(this ILaProcessor<T> arrayProcessor, int basisVectorsCount, Func<int, IGaVectorStorage<T>> basisVectorMapFunc)
        {
            var mappedBasisVectors = 
                Enumerable
                    .Range(0, basisVectorsCount)
                    .Select(basisVectorMapFunc)
                    .ToArray();

            return arrayProcessor.CreateComputedOutermorphism(mappedBasisVectors);
        }
        
        public static IGaOutermorphism<T> CreateComputedOutermorphism<T>(this ILaProcessor<T> arrayProcessor, int basisVectorsCount, Func<IGaVectorStorage<T>, IGaVectorStorage<T>> basisVectorMapFunc)
        {
            var mappedBasisVectors = 
                Enumerable
                    .Range(0, basisVectorsCount)
                    .Select(index =>
                        basisVectorMapFunc(arrayProcessor.CreateGaVectorStorage(index))
                    ).ToArray();

            return arrayProcessor.CreateComputedOutermorphism(mappedBasisVectors);
        }

    }
}