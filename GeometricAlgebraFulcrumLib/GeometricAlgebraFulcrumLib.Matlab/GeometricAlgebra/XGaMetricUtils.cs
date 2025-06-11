using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra;

public static class XGaMetricUtils
{
    
    public static bool IsValidVectorDictionary(this IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, double>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, double> dict &&
                 dict.Key.Count == 1 &&
                 dict.Value.IsValid() &&
                 !dict.Value.IsZero(),

            _ => basisScalarDictionary.All(p =>
                p.Key.Count == 1 &&
                p.Value.IsValid() &&
                !p.Value.IsZero()
            )
        };
    }

    
    public static bool IsValidBivectorDictionary(this IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, double>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, double> dict &&
                 dict.Key.Count == 2 &&
                 dict.Value.IsValid() &&
                 !dict.Value.IsZero(),

            _ => basisScalarDictionary.All(p =>
                p.Key.Count == 2 &&
                p.Value.IsValid() &&
                !p.Value.IsZero()
            )
        };
    }

    
    public static bool IsValidKVectorDictionary(this IReadOnlyDictionary<IndexSet, double> basisScalarDictionary, int grade)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, double>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, double> dict &&
                 dict.Key.Count == grade &&
                 dict.Value.IsValid() &&
                 !dict.Value.IsZero(),

            _ => basisScalarDictionary.All(p =>
                p.Key.Count == grade &&
                p.Value.IsValid() &&
                !p.Value.IsZero()
            )
        };
    }

    
    public static bool IsValidMultivectorDictionary(this IReadOnlyDictionary<IndexSet, double> basisScalarDictionary)
    {
        return basisScalarDictionary.Count switch
        {
            0 => basisScalarDictionary is EmptyDictionary<IndexSet, double>,

            1 => basisScalarDictionary is SingleItemDictionary<IndexSet, double> dict &&
                 dict.Value.IsValid() &&
                 !dict.Value.IsZero(),

            _ => basisScalarDictionary.All(p =>
                p.Value.IsValid() &&
                !p.Value.IsZero()
            )
        };
    }

    
    
    public static IEnumerable<KeyValuePair<ulong, T>> OrderByGradeIndex<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList)
    {
        var termsArray = termsList.ToArray();

        if (termsArray.Length == 0)
            return termsArray;

        var bitsCount = termsArray
            .Max(t => t.Key)
            .LastOneBitPosition() + 1;

        if (bitsCount == 0)
            return termsArray;

        return termsArray
            .OrderBy(t => t.Key.Grade())
            .ThenByDescending(t => t.Key.ReverseBits(bitsCount));
    }

    
    public static IEnumerable<KeyValuePair<IndexSet, T>> OrderByGradeIndex<T>(this IEnumerable<KeyValuePair<IndexSet, T>> termsList)
    {
        var termsArray = 
            termsList.ToArray();

        if (termsArray.Length == 0)
            return termsArray;

        var maxIndex = 
            termsArray.Max(t => 
                t.Key.VSpaceDimensions()
            ) - 1;

        if (maxIndex <= 0)
            return termsArray;

        return termsArray
            .OrderBy(t => t.Key.Grade())
            .ThenByDescending(t => t.Key.MapIndicesByValue(i => maxIndex - i));
    }

    
    public static IEnumerable<KeyValuePair<ulong, T>> OrderById<T>(this IEnumerable<KeyValuePair<ulong, T>> termsList)
    {
        return termsList.OrderBy(
            t => t.Key
        );
    }
        
    
    public static IEnumerable<KeyValuePair<IndexSet, T>> OrderById<T>(this IEnumerable<KeyValuePair<IndexSet, T>> termsList)
    {
        return termsList.OrderBy(
            t => t.Key
        );
    }
}