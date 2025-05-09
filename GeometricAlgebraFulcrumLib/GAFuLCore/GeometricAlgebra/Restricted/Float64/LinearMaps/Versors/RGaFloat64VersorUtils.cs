using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;

public static class RGaFloat64VersorUtils
{
    //public static Tuple<RGaPureVersorsSequence, IRGaOutermorphism> GetHouseholderQRDecomposition(this IRGaOutermorphism linearMap, int count)
    //{
    //    var unitVectorsList = new List<RGaVector>(count);
    //    var mappedBasisVectors =
    //        Enumerable
    //            .Range(0, count)
    //            .Select(linearMap.OmMapBasisVector)
    //            .ToArray();

    //    var metric = linearMap.Metric;
            
    //    //Vector composers to construct r vectors
    //    var vectorComposersArray = new LinVectorComposer[count];
            
    //    for (var i = 0; i < count; i++)
    //        vectorComposersArray[i] = new LinVectorComposer();

    //    var basisVectorIndicesList =
    //        Enumerable.Range(0, count).ToList();

    //    //unit vector norm for higher numerical stability
    //    for (var i = 0; i < count; i++)
    //    {
    //        //Find the basis vector with largest angle from its image for more numerical stability
    //        var bestBasisVectorIndex = 0;
    //        var bestDifferenceLengthSquared = -1d;
    //        foreach (var basisVectorIndex in basisVectorIndicesList)
    //        {
    //            var basisVectorImage = mappedBasisVectors[basisVectorIndex];
    //            var basisVectorImageNorm = basisVectorImage.ENorm().ScalarValue;

    //            var differenceLengthSquared =
    //                (
    //                    basisVectorImage -
    //                    metric.Vector(basisVectorIndex, basisVectorImageNorm)
    //                ).ESpSquared();

    //            if (differenceLengthSquared.ScalarValue < bestDifferenceLengthSquared)
    //                continue;

    //            bestBasisVectorIndex = basisVectorIndex;
    //            bestDifferenceLengthSquared = differenceLengthSquared.ScalarValue;
    //        }

    //        var bestBasisVectorImage = mappedBasisVectors[bestBasisVectorIndex];
    //        var bestBasisVectorImageNorm = bestBasisVectorImage.ENorm().ScalarValue;

    //        var unitVector =
    //            bestBasisVectorImage -
    //            metric.Vector(bestBasisVectorIndex, bestBasisVectorImageNorm);

    //        var reflectionVectorFound = !unitVector.IsZero;
    //        if (reflectionVectorFound)
    //        {
    //            unitVector /= unitVector.ENorm();
    //            unitVectorsList.Add(unitVector);
    //        }

    //        foreach (var basisVectorIndex in basisVectorIndicesList)
    //        {
    //            var reflectedVector = reflectionVectorFound
    //                ? -unitVector
    //                    .EGp(mappedBasisVectors[basisVectorIndex])
    //                    .EGp(unitVector)
    //                    .GetVectorPart()
    //                : mappedBasisVectors[basisVectorIndex];

    //            var bestBasisVectorId = bestBasisVectorIndex.BasisVectorIndexToId();
    //            if (reflectedVector.TryGetTermScalar(bestBasisVectorId, out var scalar))
    //                vectorComposersArray[basisVectorIndex].SetTerm(bestBasisVectorIndex, scalar);

    //            if (basisVectorIndex == bestBasisVectorIndex)
    //                continue;

    //            mappedBasisVectors[basisVectorIndex] =
    //                metric.Vector(
    //                    reflectedVector
    //                        .IdScalarPairs
    //                        .Where(pair => !pair.Key.Equals(bestBasisVectorId))
    //                        .ToDictionary(
    //                            p => p.Key, 
    //                            p => p.Value
    //                        )
    //                    );
    //        }

    //        basisVectorIndicesList.Remove(bestBasisVectorIndex);
    //    }

    //    var linearMapRVectorsDictionary = new Dictionary<int, LinVector>();
    //    for (var i = 0; i < vectorComposersArray.Length; i++)
    //    {
    //        var composer = vectorComposersArray[i];
                
    //        if (!composer.IsZero)
    //            linearMapRVectorsDictionary.Add(i, composer.GetVector());
    //    }

    //    var linearMapR =
    //        linearMapRVectorsDictionary
    //            .CreateLinUnilinearMap()
    //            .ToOutermorphism(metric);

    //    unitVectorsList.Reverse();
    //    var linearMapQ =
    //        RGaPureVersorsSequence.Create(unitVectorsList);

    //    return new Tuple<RGaPureVersorsSequence, IRGaOutermorphism>(
    //        linearMapQ,
    //        linearMapR
    //    );
    //}

    public static Tuple<RGaFloat64PureVersorsSequence, IRGaFloat64Outermorphism> GetHouseholderQRDecomposition(this IRGaFloat64Outermorphism linearMap, int count)
    {
        var metric = linearMap.Processor;

        var unitVectorsList = new List<RGaFloat64Vector>(count);
        var mappedBasisVectors =
            Enumerable
                .Range(0, count)
                .Select(linearMap.OmMapBasisVector)
                .ToArray();

        //Vector composers to construct r vectors
        var vectorComposersArray = new LinFloat64VectorComposer[count];
        for (var i = 0; i < count; i++)
            vectorComposersArray[i] = LinFloat64VectorComposer.Create();

        //TODO: Select the order of basis vectors according to the largest
        //unit vector norm for higher numerical stability
        for (var i = 0; i < count; i++)
        {
            var x = mappedBasisVectors[i];
            var xNorm = x.ENorm().ScalarValue;

            var unitVector = x - metric.VectorTerm(i, xNorm);

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
                var basisVectorId = basisVectorIndex.BasisVectorIndexToId();

                if (reflectedVector.TryGetBasisBladeScalarValue(basisVectorId, out var scalar))
                    vectorComposersArray[j].SetTerm(basisVectorIndex, scalar);

                if (j > i)
                    mappedBasisVectors[j] = reflectedVector.GetVectorPart(
                        (int index) => index > basisVectorIndex
                    );
            }
        }

        var linearMapRVectorsDictionary = new Dictionary<int, LinFloat64Vector>();
        for (var i = 0; i < vectorComposersArray.Length; i++)
        {
            var composer = vectorComposersArray[i];
                
            if (!composer.IsZero)
                linearMapRVectorsDictionary.Add(i, composer.GetVector());
        }

        var linearMapR =
            linearMapRVectorsDictionary
                .ToLinUnilinearMap()
                .ToOutermorphism(metric);

        unitVectorsList.Reverse();
        var linearMapQ =
            RGaFloat64PureVersorsSequence.Create(
                unitVectorsList
            );

        return new Tuple<RGaFloat64PureVersorsSequence, IRGaFloat64Outermorphism>(
            linearMapQ,
            linearMapR
        );
    }
}