using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraLib.Algebra.Signatures;
using GeometricAlgebraLib.Storage;
using GeometricAlgebraLib.Storage.Composers;

namespace GeometricAlgebraLib.Geometry.Euclidean
{
    public static class GaEuclideanGeometryUtils
    {
        
        public static T[,] GetArray<T>(this IGaVectorsLinearMap<T> linearMap, int rowsCount, int colsCount)
        {
            var scalarProcessor = linearMap.ScalarProcessor;
            var array = new T[rowsCount, colsCount];

            for (var j = 0; j < colsCount; j++)
            {
                var mappedBasisVector = linearMap.MapBasisVector(j);

                for (var i = 0; i < rowsCount; i++)
                    array[i, j] = mappedBasisVector.TryGetTermScalarByIndex((ulong) i, out var scalar)
                        ? scalar
                        : scalarProcessor.ZeroScalar;
            }

            return array;
        }

        public static Tuple<GaEuclideanFactoredVersor<double>, GaVectorsLinearMap<double>> GetHouseholderQRDecomposition(this IGaVectorsLinearMap<double> linearMap, int count)
        {
            var scalarProcessor = linearMap.ScalarProcessor;
            var unitVectorsList = new List<IGaVectorStorage<double>>(count);
            var mappedBasisVectors =
                Enumerable
                    .Range(0, count)
                    .Select(linearMap.MapBasisVector)
                    .ToArray();

            //Vector composers to construct r vectors
            var vectorComposersArray = new GaKVectorStorageComposer<double>[count];
            for (var i = 0; i < count; i++)
                vectorComposersArray[i] = 
                    new GaKVectorStorageComposer<double>(scalarProcessor, 1);

            var basisVectorIndicesList = 
                Enumerable.Range(0, count).ToList();

            //unit vector norm for higher numerical stability
            for (var i = 0; i < count; i++)
            {
                //Find the basis vector with largest angle from its image for more numerical stability
                var bestBasisVectorIndex = 0;
                var bestDifferenceLengthSquared = -1d;
                foreach (var basisVectorIndex in basisVectorIndicesList)
                {
                    var basisVectorImage = mappedBasisVectors[basisVectorIndex];
                    var basisVectorImageNorm = basisVectorImage.ENorm();

                    var differenceLengthSquared = basisVectorImage.Subtract(
                        GaVectorTermStorage<double>.Create(scalarProcessor, basisVectorIndex, basisVectorImageNorm)
                    ).ESp();

                    if (differenceLengthSquared < bestDifferenceLengthSquared) 
                        continue;

                    bestBasisVectorIndex = basisVectorIndex;
                    bestDifferenceLengthSquared = differenceLengthSquared;
                }

                var bestBasisVectorImage = mappedBasisVectors[bestBasisVectorIndex];
                var bestBasisVectorImageNorm = bestBasisVectorImage.ENorm();

                var unitVector = bestBasisVectorImage.Subtract(
                    GaVectorTermStorage<double>.Create(scalarProcessor, bestBasisVectorIndex, bestBasisVectorImageNorm)
                ).GetVectorPart();

                var reflectionVectorFound = !unitVector.IsZero();
                if (reflectionVectorFound)
                {
                    unitVector = unitVector.Divide(unitVector.ENorm()).GetVectorPart();
                    unitVectorsList.Add(unitVector);
                }

                foreach (var basisVectorIndex in basisVectorIndicesList)
                {
                    var reflectedVector = reflectionVectorFound
                        ? unitVector
                            .EGp(mappedBasisVectors[basisVectorIndex])
                            .EGp(unitVector)
                            .GetVectorPart(scalarProcessor.Negative)
                        : mappedBasisVectors[basisVectorIndex];

                    var bestBasisVectorIndexULong = (ulong) bestBasisVectorIndex;

                    if (reflectedVector.TryGetTermScalarByIndex(bestBasisVectorIndexULong, out var scalar))
                        vectorComposersArray[basisVectorIndex].SetTerm(bestBasisVectorIndexULong, scalar);

                    if (basisVectorIndex == bestBasisVectorIndex)
                        continue;

                    mappedBasisVectors[basisVectorIndex] = GaVectorStorage<double>.Create(
                        scalarProcessor,
                        reflectedVector
                            .GetIndexScalarPairs()
                            .Where(pair => pair.Key != bestBasisVectorIndexULong)
                            .CopyToDictionary()
                    );
                }

                basisVectorIndicesList.Remove(bestBasisVectorIndex);
            }

            var linearMapRVectorsDictionary = new Dictionary<ulong, IGaVectorStorage<double>>();
            for (var i = 0; i < vectorComposersArray.Length; i++)
            {
                var composer = vectorComposersArray[i];

                composer.RemoveZeroTerms();

                if (!composer.IsEmpty())
                    linearMapRVectorsDictionary.Add((ulong) i, composer.GetVectorStorage());
            }

            var linearMapR =
                GaVectorsLinearMap<double>.Create(scalarProcessor, linearMapRVectorsDictionary);

            unitVectorsList.Reverse();
            var linearMapQ = 
                GaEuclideanFactoredVersor<double>.Create(unitVectorsList);

            return new Tuple<GaEuclideanFactoredVersor<double>, GaVectorsLinearMap<double>>(
                linearMapQ,
                linearMapR
            );
        }

        public static Tuple<GaEuclideanFactoredVersor<T>, GaVectorsLinearMap<T>> GetHouseholderQRDecomposition<T>(this IGaVectorsLinearMap<T> linearMap, int count)
        {
            var scalarProcessor = linearMap.ScalarProcessor;
            var unitVectorsList = new List<IGaVectorStorage<T>>(count);
            var mappedBasisVectors =
                Enumerable
                    .Range(0, count)
                    .Select(linearMap.MapBasisVector)
                    .ToArray();

            //Vector composers to construct r vectors
            var vectorComposersArray = new GaKVectorStorageComposer<T>[count];
            for (var i = 0; i < count; i++)
                vectorComposersArray[i] = 
                    new GaKVectorStorageComposer<T>(scalarProcessor, 1);

            //TODO: Select the order of basis vectors according to the largest
            //unit vector norm for higher numerical stability
            for (var i = 0; i < count; i++)
            {
                var x = mappedBasisVectors[i];
                var xNorm = x.ENorm();

                var unitVector = x.Subtract(
                    GaVectorTermStorage<T>.Create(scalarProcessor, i, xNorm)
                ).GetVectorPart();

                var reflectionVectorFound = !unitVector.IsZero();
                if (reflectionVectorFound)
                {
                    unitVector = unitVector.Divide(unitVector.ENorm()).GetVectorPart();
                    unitVectorsList.Add(unitVector);
                }

                for (var j = i; j < count; j++)
                {
                    var reflectedVector = reflectionVectorFound
                        ? unitVector
                            .EGp(mappedBasisVectors[j])
                            .EGp(unitVector)
                            .GetVectorPart(scalarProcessor.Negative)
                        : mappedBasisVectors[j];

                    var basisVectorIndex = (ulong) i;

                    if (reflectedVector.TryGetTermScalarByIndex(basisVectorIndex, out var scalar))
                        vectorComposersArray[j].SetTerm(basisVectorIndex, scalar);

                    if (j > i)
                        mappedBasisVectors[j] = reflectedVector.GetVectorPart(
                            index => index > basisVectorIndex
                        );
                }
            }

            var linearMapRVectorsDictionary = new Dictionary<ulong, IGaVectorStorage<T>>();
            for (var i = 0; i < vectorComposersArray.Length; i++)
            {
                var composer = vectorComposersArray[i];

                composer.RemoveZeroTerms();

                if (!composer.IsEmpty())
                    linearMapRVectorsDictionary.Add((ulong) i, composer.GetVectorStorage());
            }

            var linearMapR =
                GaVectorsLinearMap<T>.Create(scalarProcessor, linearMapRVectorsDictionary);

            unitVectorsList.Reverse();
            var linearMapQ = 
                GaEuclideanFactoredVersor<T>.Create(unitVectorsList);

            return new Tuple<GaEuclideanFactoredVersor<T>, GaVectorsLinearMap<T>>(
                linearMapQ,
                linearMapR
            );
        }
    }
}