using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps;

public sealed class XGaFloat64DiagonalUnilinearMap :
    IXGaFloat64UnilinearMap,
    IReadOnlyDictionary<IndexSet, double>
{
    public XGaFloat64Multivector DiagonalMultivector { get; }
        
    public XGaFloat64Processor Processor 
        => DiagonalMultivector.Processor;
        
    public XGaMetric Metric 
        => DiagonalMultivector.Metric;

    public int Count 
        => DiagonalMultivector.Count;

    public IEnumerable<IndexSet> Keys 
        => DiagonalMultivector.Ids;

    public IEnumerable<double> Values 
        => DiagonalMultivector.Scalars;

    public double this[IndexSet key] 
        => DiagonalMultivector[key];


    
    internal XGaFloat64DiagonalUnilinearMap(XGaFloat64Multivector diagonalMultivector)
    {
        DiagonalMultivector = diagonalMultivector;
    }


    
    public bool IsValid()
    {
        return DiagonalMultivector.IsValid();
    }
    
    
    public bool ContainsKey(IndexSet key)
    {
        return DiagonalMultivector.ContainsKey(key);
    }

    
    public bool TryGetValue(IndexSet key, out double value)
    {
        return DiagonalMultivector.TryGetValue(key, out value);
    }

    
    public IXGaFloat64UnilinearMap GetAdjoint()
    {
        return this;
    }

    
    public XGaFloat64Multivector MapBasisBlade(IndexSet id)
    {
        return DiagonalMultivector.TryGetValue(id, out var scalar)
            ? Processor.KVectorTerm(id, scalar)
            : Processor.ScalarZero;
    }

    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateMultivectorComposer();

        if (Count <= multivector.Count)
        {
            foreach (var (id, mv) in DiagonalMultivector.ToTuples())
            {
                if (!multivector.TryGetBasisBladeScalarValue(id, out var scalar))
                    continue;

                composer.AddTerm(id, mv * scalar);
            }
        }
        else
        {
            foreach (var (id, scalar) in multivector.ToTuples())
            {
                if (!DiagonalMultivector.TryGetValue(id, out var mv))
                    continue;

                composer.AddTerm(id, mv * scalar);
            }
        }

        return composer.GetMultivector();
    }

    
    public IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return DiagonalMultivector
            .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
            .Select(p => 
                new KeyValuePair<IndexSet, XGaFloat64Multivector>(
                    p.Key, 
                    Processor.KVectorTerm(p.Key, p.Value)
                )
            );
    }
        
    public double[,] GetMultivectorMapArray(int rowCount, int colCount)
    {
        var minSize = DiagonalMultivector.VSpaceDimensions;

        if (rowCount < minSize || colCount < minSize)
            throw new InvalidOperationException();
            
        var mapArray = new double[rowCount, colCount];
            
        foreach (var (id, scalar) in DiagonalMultivector.ToTuples())
        {
            var index = id.ToInt32();

            mapArray[index, index] = scalar;
        }

        return mapArray;
    }

    
    public IEnumerator<KeyValuePair<IndexSet, double>> GetEnumerator()
    {
        return DiagonalMultivector.GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}