using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
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
            var unitVectorsList = new List<GaVector<double>>(count);
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
                    var basisVectorImageNorm = basisVectorImage.ENorm().ScalarValue;

                    var differenceLengthSquared = 
                        (
                            basisVectorImage - 
                            processor.CreateVectorTerm(basisVectorIndex, basisVectorImageNorm)
                        ).ESpSquared();

                    if (differenceLengthSquared.ScalarValue < bestDifferenceLengthSquared) 
                        continue;

                    bestBasisVectorIndex = basisVectorIndex;
                    bestDifferenceLengthSquared = differenceLengthSquared.ScalarValue;
                }

                var bestBasisVectorImage = mappedBasisVectors[bestBasisVectorIndex];
                var bestBasisVectorImageNorm = bestBasisVectorImage.ENorm().ScalarValue;

                var unitVector = 
                    bestBasisVectorImage -
                    processor.CreateVectorTerm(bestBasisVectorIndex, bestBasisVectorImageNorm);

                var reflectionVectorFound = !unitVector.IsZero();
                if (reflectionVectorFound)
                {
                    unitVector /= unitVector.ENorm();
                    unitVectorsList.Add(unitVector);
                }

                foreach (var basisVectorIndex in basisVectorIndicesList)
                {
                    var reflectedVector = reflectionVectorFound
                        ? -unitVector
                            .EGp(mappedBasisVectors[basisVectorIndex])
                            .EGp(unitVector)
                            .GetVectorPart()
                        : mappedBasisVectors[basisVectorIndex];

                    var bestBasisVectorIndexULong = (ulong) bestBasisVectorIndex;

                    if (reflectedVector.VectorStorage.TryGetTermScalarByIndex(bestBasisVectorIndexULong, out var scalar))
                        vectorComposersArray[basisVectorIndex].SetTerm(bestBasisVectorIndexULong, scalar);

                    if (basisVectorIndex == bestBasisVectorIndex)
                        continue;

                    mappedBasisVectors[basisVectorIndex] = 
                        processor.CreateVector(
                            reflectedVector
                                .VectorStorage
                                .GetLinVectorIndexScalarStorage()
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
                    unitVectorsList
                );

            return new Tuple<PureVersorsSequence<double>, IOutermorphism<double>>(
                linearMapQ,
                linearMapR
            );
        }

        public static Tuple<PureVersorsSequence<T>, IOutermorphism<T>> GetHouseholderQRDecomposition<T>(this IGeometricAlgebraProcessor<T> processor, IOutermorphism<T> linearMap, int count)
        {
            var unitVectorsList = new List<GaVector<T>>(count);
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
                var xNorm = x.ENorm().ScalarValue;

                var unitVector = x - processor.CreateVectorTerm(i, xNorm);

                var reflectionVectorFound = !unitVector.IsZero();
                if (reflectionVectorFound)
                {
                    unitVector /= unitVector.ENorm();
                    unitVectorsList.Add(unitVector);
                }

                for (var j = i; j < count; j++)
                {
                    var reflectedVector = reflectionVectorFound
                        ? -unitVector
                            .EGp(mappedBasisVectors[j])
                            .EGp(unitVector)
                            .GetVectorPart()
                        : mappedBasisVectors[j];

                    var basisVectorIndex = (ulong) i;

                    if (reflectedVector.VectorStorage.TryGetTermScalarByIndex(basisVectorIndex, out var scalar))
                        vectorComposersArray[j].SetTerm(basisVectorIndex, scalar);

                    if (j > i)
                        mappedBasisVectors[j] = reflectedVector.VectorStorage.GetVectorPart(
                            index => index > basisVectorIndex
                        ).CreateVector(processor);
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
                    unitVectorsList
                );

            return new Tuple<PureVersorsSequence<T>, IOutermorphism<T>>(
                linearMapQ,
                linearMapR
            );
        }
    }
}