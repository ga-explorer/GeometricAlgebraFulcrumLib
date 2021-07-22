using System;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Geometry.Euclidean
{
    public static class GaEuclideanGeometryUtils
    {
        
        

        public static Tuple<GaFactoredVersor<double>, IGaOutermorphism<double>> GetHouseholderQRDecomposition(this IGaProcessor<double> processor, IGaOutermorphism<double> linearMap, int count)
        {
            var unitVectorsList = new List<IGasVector<double>>(count);
            var mappedBasisVectors =
                Enumerable
                    .Range(0, count)
                    .Select(i => linearMap.MapBasisVector((uint) i))
                    .ToArray();

            //Vector composers to construct r vectors
            var vectorComposersArray = new GaKVectorStorageComposer<double>[count];
            for (var i = 0; i < count; i++)
                vectorComposersArray[i] = 
                    new GaKVectorStorageComposer<double>(processor, 1);

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
                        processor.CreateVector(basisVectorIndex, basisVectorImageNorm)
                    ).ESp();

                    if (differenceLengthSquared < bestDifferenceLengthSquared) 
                        continue;

                    bestBasisVectorIndex = basisVectorIndex;
                    bestDifferenceLengthSquared = differenceLengthSquared;
                }

                var bestBasisVectorImage = mappedBasisVectors[bestBasisVectorIndex];
                var bestBasisVectorImageNorm = bestBasisVectorImage.ENorm();

                var unitVector = bestBasisVectorImage.Subtract(
                    processor.CreateVector(bestBasisVectorIndex, bestBasisVectorImageNorm)
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
                            .GetVectorPart(processor.Negative)
                        : mappedBasisVectors[basisVectorIndex];

                    var bestBasisVectorIndexULong = (ulong) bestBasisVectorIndex;

                    if (reflectedVector.TryGetTermScalarByIndex(bestBasisVectorIndexULong, out var scalar))
                        vectorComposersArray[basisVectorIndex].SetTerm(bestBasisVectorIndexULong, scalar);

                    if (basisVectorIndex == bestBasisVectorIndex)
                        continue;

                    mappedBasisVectors[basisVectorIndex] = processor.CreateVector(reflectedVector
                            .GetIndexScalarPairs()
                            .Where(pair => pair.Key != bestBasisVectorIndexULong)
                            .CopyToDictionary()
                    );
                }

                basisVectorIndicesList.Remove(bestBasisVectorIndex);
            }

            var linearMapRVectorsDictionary = new Dictionary<ulong, IGasVector<double>>();
            for (var i = 0; i < vectorComposersArray.Length; i++)
            {
                var composer = vectorComposersArray[i];

                composer.RemoveZeroTerms();

                if (!composer.IsEmpty())
                    linearMapRVectorsDictionary.Add((ulong) i, composer.GetVectorStorage());
            }

            var linearMapR =
                processor.CreateComputedOutermorphism(
                    (uint) count,
                    linearMapRVectorsDictionary
                );

            unitVectorsList.Reverse();
            var linearMapQ = 
                GaFactoredVersor<double>.Create(
                    processor, 
                    unitVectorsList
                );

            return new Tuple<GaFactoredVersor<double>, IGaOutermorphism<double>>(
                linearMapQ,
                linearMapR
            );
        }

        public static Tuple<GaFactoredVersor<T>, IGaOutermorphism<T>> GetHouseholderQRDecomposition<T>(this IGaProcessor<T> processor, IGaOutermorphism<T> linearMap, int count)
        {
            var unitVectorsList = new List<IGasVector<T>>(count);
            var mappedBasisVectors =
                Enumerable
                    .Range(0, count)
                    .Select(i => linearMap.MapBasisVector((uint) i))
                    .ToArray();

            //Vector composers to construct r vectors
            var vectorComposersArray = new GaKVectorStorageComposer<T>[count];
            for (var i = 0; i < count; i++)
                vectorComposersArray[i] = 
                    new GaKVectorStorageComposer<T>(processor, 1);

            //TODO: Select the order of basis vectors according to the largest
            //unit vector norm for higher numerical stability
            for (var i = 0; i < count; i++)
            {
                var x = mappedBasisVectors[i];
                var xNorm = x.ENorm();

                var unitVector = x.Subtract(
                    processor.CreateVector(i, xNorm)
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
                            .GetVectorPart(processor.Negative)
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

            var linearMapRVectorsDictionary = new Dictionary<ulong, IGasVector<T>>();
            for (var i = 0; i < vectorComposersArray.Length; i++)
            {
                var composer = vectorComposersArray[i];

                composer.RemoveZeroTerms();

                if (!composer.IsEmpty())
                    linearMapRVectorsDictionary.Add((ulong) i, composer.GetVectorStorage());
            }

            var linearMapR =
                processor.CreateComputedOutermorphism(
                    (uint) count, 
                    linearMapRVectorsDictionary
                );

            unitVectorsList.Reverse();
            var linearMapQ = 
                GaFactoredVersor<T>.Create(
                    processor, 
                    unitVectorsList
                );

            return new Tuple<GaFactoredVersor<T>, IGaOutermorphism<T>>(
                linearMapQ,
                linearMapR
            );
        }
    }
}