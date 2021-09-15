using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class VersorUtils
    {
        


        public static Tuple<PureVersorsSequence<double>, IOutermorphism<double>> GetHouseholderQRDecomposition(this IGeometricAlgebraProcessor<double> processor, IOutermorphism<double> linearMap, int count)
        {
            var unitVectorsList = new List<VectorStorage<double>>(count);
            var mappedBasisVectors =
                Enumerable
                    .Range(0, count)
                    .Select(i => linearMap.OmMapBasisVector((uint) i))
                    .ToArray();

            //Vector composers to construct r vectors
            var vectorComposersArray = new VectorSparseStorageComposer<double>[count];
            for (var i = 0; i < count; i++)
                vectorComposersArray[i] = 
                    processor.CreateVectorStorageComposer();

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
                                processor.CreateVectorTermStorage(basisVectorIndex, basisVectorImageNorm)
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
                    processor.CreateVectorTermStorage(bestBasisVectorIndex, bestBasisVectorImageNorm)
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

                    mappedBasisVectors[basisVectorIndex] = processor.CreateVectorStorage(reflectedVector.GetLinVectorIndexScalarStorage()
                            .GetIndexScalarRecords()
                            .Where(pair => pair.Index != bestBasisVectorIndexULong)
                            .CreateDictionary()
                    );
                }

                basisVectorIndicesList.Remove(bestBasisVectorIndex);
            }

            var linearMapRVectorsDictionary = new Dictionary<ulong, VectorStorage<double>>();
            for (var i = 0; i < vectorComposersArray.Length; i++)
            {
                var composer = vectorComposersArray[i];

                composer.RemoveZeroTerms();

                if (!composer.IsEmpty())
                    linearMapRVectorsDictionary.Add((ulong) i, composer.CreateVectorStorage());
            }

            var linearMapR =
                processor.CreateLinearMapOutermorphism(
                    //(uint) count,
                    linearMapRVectorsDictionary
                );

            unitVectorsList.Reverse();
            var linearMapQ = 
                PureVersorsSequence<double>.Create(
                    processor, 
                    unitVectorsList
                );

            return new Tuple<PureVersorsSequence<double>, IOutermorphism<double>>(
                linearMapQ,
                linearMapR
            );
        }

        public static Tuple<PureVersorsSequence<T>, IOutermorphism<T>> GetHouseholderQRDecomposition<T>(this IGeometricAlgebraProcessor<T> processor, IOutermorphism<T> linearMap, int count)
        {
            var unitVectorsList = new List<VectorStorage<T>>(count);
            var mappedBasisVectors =
                Enumerable
                    .Range(0, count)
                    .Select(i => linearMap.OmMapBasisVector((uint) i))
                    .ToArray();

            //Vector composers to construct r vectors
            var vectorComposersArray = new VectorSparseStorageComposer<T>[count];
            for (var i = 0; i < count; i++)
                vectorComposersArray[i] = 
                    processor.CreateVectorStorageComposer();

            //TODO: Select the order of basis vectors according to the largest
            //unit vector norm for higher numerical stability
            for (var i = 0; i < count; i++)
            {
                var x = mappedBasisVectors[i];
                var xNorm = processor.ENorm(x);

                var unitVector = processor.Subtract(
                    x,
                    processor.CreateVectorTermStorage(i, xNorm)
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

            var linearMapRVectorsDictionary = new Dictionary<ulong, VectorStorage<T>>();
            for (var i = 0; i < vectorComposersArray.Length; i++)
            {
                var composer = vectorComposersArray[i];

                composer.RemoveZeroTerms();

                if (!composer.IsEmpty())
                    linearMapRVectorsDictionary.Add((ulong) i, composer.CreateVectorStorage());
            }

            var linearMapR =
                processor.CreateLinearMapOutermorphism(
                    //(uint) count, 
                    linearMapRVectorsDictionary
                );

            unitVectorsList.Reverse();
            var linearMapQ = 
                PureVersorsSequence<T>.Create(
                    processor, 
                    unitVectorsList
                );

            return new Tuple<PureVersorsSequence<T>, IOutermorphism<T>>(
                linearMapQ,
                linearMapR
            );
        }
    }
}