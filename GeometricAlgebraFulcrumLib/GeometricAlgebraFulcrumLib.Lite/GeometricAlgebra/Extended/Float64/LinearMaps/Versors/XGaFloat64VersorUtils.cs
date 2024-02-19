using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Versors;

public static class XGaFloat64VersorUtils
{
    //public static Tuple<XGaFloat64PureVersorsSequence, IXGaFloat64Outermorphism> GetHouseholderQRDecomposition(this IXGaFloat64Outermorphism linearMap, int count)
    //{
    //    var unitVectorsList = new List<XGaFloat64Vector>(count);
    //    var mappedBasisVectors =
    //        Enumerable
    //            .Range(0, count)
    //            .Select(linearMap.OmMapBasisVector)
    //            .ToArray();

    //    var metric = linearMap.Processor;
            
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
    //                    metric.CreateVector(basisVectorIndex, basisVectorImageNorm)
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
    //            metric.CreateVector(bestBasisVectorIndex, bestBasisVectorImageNorm);

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

    //            var bestBasisVectorId = bestBasisVectorIndex.IndexToIndexSet();
    //            if (reflectedVector.TryGetTermScalar(bestBasisVectorId, out var scalar))
    //                vectorComposersArray[basisVectorIndex].SetTerm(bestBasisVectorIndex, scalar);

    //            if (basisVectorIndex == bestBasisVectorIndex)
    //                continue;

    //            mappedBasisVectors[basisVectorIndex] =
    //                metric.CreateVector(
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
    //        XGaFloat64PureVersorsSequence.Create(unitVectorsList);

    //    return new Tuple<XGaFloat64PureVersorsSequence, IXGaFloat64Outermorphism>(
    //        linearMapQ,
    //        linearMapR
    //    );
    //}

    public static Tuple<XGaFloat64PureVersorsSequence, IXGaFloat64Outermorphism> GetHouseholderQRDecomposition(this IXGaFloat64Outermorphism linearMap, int count)
    {
        var metric = linearMap.Processor;

        var unitVectorsList = new List<XGaFloat64Vector>(count);
        var mappedBasisVectors =
            Enumerable
                .Range(0, count)
                .Select(linearMap.OmMapBasisVector)
                .ToArray();

        //Vector composers to construct r vectors
        var vectorComposersArray = new Float64VectorComposer[count];
        for (var i = 0; i < count; i++)
            vectorComposersArray[i] = Float64VectorComposer.Create();

        //TODO: Select the order of basis vectors according to the largest
        //unit vector norm for higher numerical stability
        for (var i = 0; i < count; i++)
        {
            var x = mappedBasisVectors[i];
            var xNorm = x.ENorm().ScalarValue();

            var unitVector = x - metric.CreateTermVector(i, xNorm);

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
                var basisVectorId = basisVectorIndex.IndexToIndexSet();

                if (reflectedVector.TryGetBasisBladeScalarValue(basisVectorId, out var scalar))
                    vectorComposersArray[j].SetTerm(basisVectorIndex, scalar);

                if (j > i)
                    mappedBasisVectors[j] = reflectedVector.GetVectorPart(
                        index => index > basisVectorIndex
                    );
            }
        }

        var linearMapRVectorsDictionary = new Dictionary<int, Float64Vector>();
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
            XGaFloat64PureVersorsSequence.Create(
                unitVectorsList
            );

        return new Tuple<XGaFloat64PureVersorsSequence, IXGaFloat64Outermorphism>(
            linearMapQ,
            linearMapR
        );
    }
}