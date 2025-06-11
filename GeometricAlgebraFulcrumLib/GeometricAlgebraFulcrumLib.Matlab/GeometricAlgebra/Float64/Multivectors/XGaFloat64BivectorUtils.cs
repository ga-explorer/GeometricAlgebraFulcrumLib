using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public static class XGaFloat64BivectorUtils
{
    
    internal static Dictionary<IndexSet, double> CreateBivectorDictionary(this IReadOnlyDictionary<IndexPair, double> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (key, value) in inputDictionary.ToTuples())
            basisScalarDictionary.Add(key.ToPairIndexSet(), value);

        return basisScalarDictionary;
    }

    
    internal static Dictionary<IndexSet, double> CreateBivectorDictionary(this IReadOnlyDictionary<Int32Pair, double> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var (key, value) in inputDictionary.ToTuples())
            basisScalarDictionary.Add(key.ToPairIndexSet(), value);

        return basisScalarDictionary;
    }

    

}