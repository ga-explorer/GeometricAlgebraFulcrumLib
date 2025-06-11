using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public static class XGaFloat64MultivectorUtils
{
    public static IReadOnlyDictionary<IndexSet, double> ToValidXGaVectorDictionary(this IReadOnlyDictionary<int, double> inputDictionary)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        foreach (var pair in inputDictionary)
        {
            if (!pair.Value.IsValid())
                throw new InvalidOperationException();
            
            if(!pair.Value.IsZero())
                basisScalarDictionary.Add(pair.Key.ToUnitIndexSet(), pair.Value);
        }
        
        return basisScalarDictionary.Count switch
        {
            0 => new EmptyDictionary<IndexSet, double>(),
            1 => new SingleItemDictionary<IndexSet, double>(basisScalarDictionary.First()),
            _ => basisScalarDictionary
        };
    }

    public static IReadOnlyDictionary<IndexSet, double> ToValidXGaVectorDictionary(this IEnumerable<double> scalarList)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if(!scalar.IsZero())
                basisScalarDictionary.Add(index.ToUnitIndexSet(), scalar);

            index++;
        }

        return basisScalarDictionary.Count switch
        {
            0 => new EmptyDictionary<IndexSet, double>(),
            1 => new SingleItemDictionary<IndexSet, double>(basisScalarDictionary.First()),
            _ => basisScalarDictionary
        };
    }
    
    public static IReadOnlyDictionary<IndexSet, double> ToValidXGaBivectorDictionary(this IEnumerable<double> scalarList)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        var index = 0;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if(!scalar.IsZero())
                basisScalarDictionary.Add(
                    IndexSet.EncodeUInt64AsCombinadic(index, 2), 
                    scalar
                );

            index++;
        }

        return basisScalarDictionary.Count switch
        {
            0 => new EmptyDictionary<IndexSet, double>(),
            1 => new SingleItemDictionary<IndexSet, double>(basisScalarDictionary.First()),
            _ => basisScalarDictionary
        };
    }
    
    public static IReadOnlyDictionary<IndexSet, double> ToValidXGaKVectorDictionary(this IEnumerable<double> scalarList, int grade)
    {
        if (grade < 1)
            throw new InvalidOperationException();

        if (grade == 1)
            return scalarList.ToValidXGaVectorDictionary();

        if (grade == 2)
            return scalarList.ToValidXGaBivectorDictionary();

        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        var index = 0UL;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if(!scalar.IsZero())
                basisScalarDictionary.Add(
                    IndexSet.EncodeUInt64AsCombinadic(index, grade), 
                    scalar
                );

            index++;
        }

        return basisScalarDictionary.Count switch
        {
            0 => new EmptyDictionary<IndexSet, double>(),
            1 => new SingleItemDictionary<IndexSet, double>(basisScalarDictionary.First()),
            _ => basisScalarDictionary
        };
    }
    
    public static IReadOnlyDictionary<IndexSet, double> ToValidXGaUniformMultivectorDictionary(this IEnumerable<double> scalarList)
    {
        var basisScalarDictionary = IndexSetUtils.CreateIndexSetDictionary<double>();

        var index = 0UL;
        foreach (var scalar in scalarList)
        {
            if (!scalar.IsValid())
                throw new InvalidOperationException();

            if(!scalar.IsZero())
                basisScalarDictionary.Add(
                    IndexSet.CreateFromUInt64Pattern(index), 
                    scalar
                );

            index++;
        }

        return basisScalarDictionary.Count switch
        {
            0 => new EmptyDictionary<IndexSet, double>(),
            1 => new SingleItemDictionary<IndexSet, double>(basisScalarDictionary.First()),
            _ => basisScalarDictionary
        };
    }


    
    public static int GetVSpaceDimensions(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Max(mv => mv.VSpaceDimensions);
    }
    
    
    public static XGaFloat64Multivector Op(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Op(mv)
        );
    }

    
    public static XGaFloat64Multivector EGp(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.EGp(mv)
        );
    }

    
    public static XGaFloat64Multivector Gp(this IEnumerable<XGaFloat64Multivector> mvList)
    {
        return mvList.Skip(1).Aggregate(
            mvList.First(),
            (current, mv) => current.Gp(mv)
        );
    }
    

    
    public static XGaFloat64Multivector[,] GetMapTable(this IReadOnlyList<XGaFloat64Multivector> multivectorList, Func<XGaFloat64Multivector, XGaFloat64Multivector, XGaFloat64Multivector> multivectorMap)
    {
        return multivectorList.GetMapTable(
            multivectorList,
            multivectorMap
        );
    }

    public static XGaFloat64Multivector[,] GetMapTable(this IReadOnlyList<XGaFloat64Multivector> multivectorList1, IReadOnlyList<XGaFloat64Multivector> multivectorList2, Func<XGaFloat64Multivector, XGaFloat64Multivector, XGaFloat64Multivector> multivectorMap)
    {
        var rowCount = multivectorList1.Count;
        var colCount = multivectorList2.Count;

        var tableArray = new XGaFloat64Multivector[rowCount, colCount];

        for (var i = 0; i < rowCount; i++)
        {
            var b1 = multivectorList1[i];

            for (var j = 0; j < colCount; j++)
            {
                var b2 = multivectorList2[j];

                tableArray[i, j] = multivectorMap(b1, b2);
            }
        }

        return tableArray;
    }


}