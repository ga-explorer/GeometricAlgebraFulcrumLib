﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Versors;

public static class XGaVersorUtils
{
    public static Tuple<XGaPureVersorsSequence<double>, IXGaOutermorphism<double>> GetHouseholderQRDecomposition(this IXGaOutermorphism<double> linearMap, int count)
    {
        var unitVectorsList = new List<XGaVector<double>>(count);
        var mappedBasisVectors =
            Enumerable
                .Range(0, count)
                .Select(linearMap.OmMapBasisVector)
                .ToArray();

        var processor = linearMap.Processor;
        var scalarProcessor = linearMap.ScalarProcessor;

        //Vector composers to construct r vectors
        var vectorComposersArray = new LinVectorComposer<double>[count];
            
        for (var i = 0; i < count; i++)
            vectorComposersArray[i] = scalarProcessor.CreateLinVectorComposer();

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
                    processor.VectorTerm(basisVectorIndex, basisVectorImageNorm)
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
                processor.VectorTerm(bestBasisVectorIndex, bestBasisVectorImageNorm);

            var reflectionVectorFound = !unitVector.IsZero;
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

                var bestBasisVectorId = bestBasisVectorIndex.ToUnitIndexSet();
                if (reflectedVector.TryGetBasisBladeScalarValue(bestBasisVectorId, out var scalar))
                    vectorComposersArray[basisVectorIndex].SetTerm(bestBasisVectorIndex, scalar);

                if (basisVectorIndex == bestBasisVectorIndex)
                    continue;

                mappedBasisVectors[basisVectorIndex] =
                    processor.Vector(
                        reflectedVector
                            .IdScalarPairs
                            .Where(pair => !pair.Key.Equals(bestBasisVectorId))
                            .ToDictionary(
                                p => p.Key, 
                                p => p.Value
                            )
                    );
            }

            basisVectorIndicesList.Remove(bestBasisVectorIndex);
        }

        var linearMapRVectorsDictionary = new Dictionary<int, LinVector<double>>();
        for (var i = 0; i < vectorComposersArray.Length; i++)
        {
            var composer = vectorComposersArray[i];
                
            if (!composer.IsZero)
                linearMapRVectorsDictionary.Add(i, composer.GetVector());
        }

        var linearMapR =
            linearMapRVectorsDictionary
                .CreateLinUnilinearMap(scalarProcessor)
                .ToOutermorphism(processor);

        unitVectorsList.Reverse();
        var linearMapQ =
            XGaPureVersorsSequence<double>.Create(
                unitVectorsList
            );

        return new Tuple<XGaPureVersorsSequence<double>, IXGaOutermorphism<double>>(
            linearMapQ,
            linearMapR
        );
    }

    public static Tuple<XGaPureVersorsSequence<T>, IXGaOutermorphism<T>> GetHouseholderQRDecomposition<T>(this IXGaOutermorphism<T> linearMap, int count)
    {
        var processor = linearMap.Processor;
        var scalarProcessor = linearMap.ScalarProcessor;

        var unitVectorsList = new List<XGaVector<T>>(count);
        var mappedBasisVectors =
            Enumerable
                .Range(0, count)
                .Select(linearMap.OmMapBasisVector)
                .ToArray();

        //Vector composers to construct r vectors
        var vectorComposersArray = new LinVectorComposer<T>[count];
        for (var i = 0; i < count; i++)
            vectorComposersArray[i] = scalarProcessor.CreateLinVectorComposer();

        //TODO: Select the order of basis vectors according to the largest
        //unit vector norm for higher numerical stability
        for (var i = 0; i < count; i++)
        {
            var x = mappedBasisVectors[i];
            var xNorm = x.ENorm().ScalarValue;

            var unitVector = x - processor.VectorTerm(i, xNorm);

            var reflectionVectorFound = !unitVector.IsZero;
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

                var basisVectorIndex = i;
                var basisVectorId = basisVectorIndex.ToUnitIndexSet();

                if (reflectedVector.TryGetBasisBladeScalarValue(basisVectorId, out var scalar))
                    vectorComposersArray[j].SetTerm(basisVectorIndex, scalar);

                if (j > i)
                    mappedBasisVectors[j] = reflectedVector.GetVectorPart(
                        index => index > basisVectorIndex
                    );
            }
        }

        var linearMapRVectorsDictionary = new Dictionary<int, LinVector<T>>();
        for (var i = 0; i < vectorComposersArray.Length; i++)
        {
            var composer = vectorComposersArray[i];
                
            if (!composer.IsZero)
                linearMapRVectorsDictionary.Add(i, composer.GetVector());
        }

        var linearMapR =
            linearMapRVectorsDictionary
                .CreateLinUnilinearMap(scalarProcessor)
                .ToOutermorphism(processor);

        unitVectorsList.Reverse();
        var linearMapQ =
            XGaPureVersorsSequence<T>.Create(
                unitVectorsList
            );

        return new Tuple<XGaPureVersorsSequence<T>, IXGaOutermorphism<T>>(
            linearMapQ,
            linearMapR
        );
    }
}