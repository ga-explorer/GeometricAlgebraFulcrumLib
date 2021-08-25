using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures.Composers;
using GeometricAlgebraFulcrumLib.Structures.Factories;

namespace GeometricAlgebraFulcrumLib.Geometry.Versors
{
    public static class GaVersorsUtils
    {
        public static Tuple<GaFactoredVersor<double>, IGaOutermorphism<double>> GetHouseholderQRDecomposition(this IGaProcessor<double> processor, IGaOutermorphism<double> linearMap, int count)
        {
            var unitVectorsList = new List<IGaStorageVector<double>>(count);
            var mappedBasisVectors =
                Enumerable
                    .Range(0, count)
                    .Select(i => linearMap.MapBasisVector((uint) i))
                    .ToArray();

            //Vector composers to construct r vectors
            var vectorComposersArray = new GaListEvenComposerSparse<double>[count];
            for (var i = 0; i < count; i++)
                vectorComposersArray[i] = 
                    processor.CreateStorageKVectorComposer();

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
                    var basisVectorImageNorm = processor.ENorm(basisVectorImage);

                    var differenceLengthSquared = 
                        processor.ESp(
                            processor.Subtract(
                                basisVectorImage,
                                processor.CreateStorageVector(basisVectorIndex, basisVectorImageNorm)
                            )
                        );

                    if (differenceLengthSquared < bestDifferenceLengthSquared) 
                        continue;

                    bestBasisVectorIndex = basisVectorIndex;
                    bestDifferenceLengthSquared = differenceLengthSquared;
                }

                var bestBasisVectorImage = mappedBasisVectors[bestBasisVectorIndex];
                var bestBasisVectorImageNorm = processor.ENorm(bestBasisVectorImage);

                var unitVector = processor.Subtract(
                    bestBasisVectorImage,
                    processor.CreateStorageVector(bestBasisVectorIndex, bestBasisVectorImageNorm)
                ).GetVectorPart();

                var reflectionVectorFound = !processor.IsZero(unitVector);
                if (reflectionVectorFound)
                {
                    unitVector = processor.Divide(unitVector, processor.ENorm(unitVector)).GetVectorPart();
                    unitVectorsList.Add(unitVector);
                }

                foreach (var basisVectorIndex in basisVectorIndicesList)
                {
                    var reflectedVector = reflectionVectorFound
                        ? processor.Negative(
                            processor.EGp(
                                unitVector,
                                mappedBasisVectors[basisVectorIndex], 
                                unitVector
                            ).GetVectorPart()
                        )
                        : mappedBasisVectors[basisVectorIndex];

                    var bestBasisVectorIndexULong = (ulong) bestBasisVectorIndex;

                    if (reflectedVector.TryGetTermScalarByIndex(bestBasisVectorIndexULong, out var scalar))
                        vectorComposersArray[basisVectorIndex].SetTerm(bestBasisVectorIndexULong, scalar);

                    if (basisVectorIndex == bestBasisVectorIndex)
                        continue;

                    mappedBasisVectors[basisVectorIndex] = processor.CreateStorageVector(reflectedVector
                            .IndexScalarList
                            .GetKeyValueRecords()
                            .Where(pair => pair.Key != bestBasisVectorIndexULong)
                            .CreateDictionary()
                    );
                }

                basisVectorIndicesList.Remove(bestBasisVectorIndex);
            }

            var linearMapRVectorsDictionary = new Dictionary<ulong, IGaStorageVector<double>>();
            for (var i = 0; i < vectorComposersArray.Length; i++)
            {
                var composer = vectorComposersArray[i];

                composer.RemoveZeroTerms();

                if (!composer.IsEmpty())
                    linearMapRVectorsDictionary.Add((ulong) i, composer.CreateStorageVector());
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
            var unitVectorsList = new List<IGaStorageVector<T>>(count);
            var mappedBasisVectors =
                Enumerable
                    .Range(0, count)
                    .Select(i => linearMap.MapBasisVector((uint) i))
                    .ToArray();

            //Vector composers to construct r vectors
            var vectorComposersArray = new GaListEvenComposerSparse<T>[count];
            for (var i = 0; i < count; i++)
                vectorComposersArray[i] = 
                    processor.CreateStorageKVectorComposer();

            //TODO: Select the order of basis vectors according to the largest
            //unit vector norm for higher numerical stability
            for (var i = 0; i < count; i++)
            {
                var x = mappedBasisVectors[i];
                var xNorm = processor.ENorm(x);

                var unitVector = processor.Subtract(
                    x,
                    processor.CreateStorageVector(i, xNorm)
                ).GetVectorPart();

                var reflectionVectorFound = !processor.IsZero(unitVector);
                if (reflectionVectorFound)
                {
                    unitVector = processor.Divide(unitVector, processor.ENorm(unitVector)).GetVectorPart();
                    unitVectorsList.Add(unitVector);
                }

                for (var j = i; j < count; j++)
                {
                    var reflectedVector = reflectionVectorFound
                        ? processor.Negative(
                            processor.EGp(
                                    unitVector, 
                                    mappedBasisVectors[j], 
                                    unitVector
                                ).GetVectorPart()
                        )
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

            var linearMapRVectorsDictionary = new Dictionary<ulong, IGaStorageVector<T>>();
            for (var i = 0; i < vectorComposersArray.Length; i++)
            {
                var composer = vectorComposersArray[i];

                composer.RemoveZeroTerms();

                if (!composer.IsEmpty())
                    linearMapRVectorsDictionary.Add((ulong) i, composer.CreateStorageVector());
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