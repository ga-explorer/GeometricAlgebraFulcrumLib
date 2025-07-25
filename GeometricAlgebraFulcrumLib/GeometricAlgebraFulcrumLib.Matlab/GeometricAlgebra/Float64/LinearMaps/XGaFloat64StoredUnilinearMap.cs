﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps;

public class XGaFloat64StoredUnilinearMap :
    IXGaFloat64UnilinearMap,
    IReadOnlyDictionary<IndexSet, XGaFloat64Multivector>
{
    private readonly IReadOnlyDictionary<IndexSet, XGaFloat64Multivector> _basisMapDictionary;

    public XGaFloat64Processor Processor { get; }
        
    public XGaMetric Metric 
        => Processor;

    public int Count 
        => _basisMapDictionary.Count;
    
    public IEnumerable<IndexSet> Keys 
        => _basisMapDictionary.Keys;

    public IEnumerable<XGaFloat64Multivector> Values 
        => _basisMapDictionary.Values;
    
    public XGaFloat64Multivector this[IndexSet key] 
        => _basisMapDictionary.TryGetValue(key, out var mv)
            ? mv : Processor.ScalarZero;


    
    internal XGaFloat64StoredUnilinearMap(XGaFloat64Processor processor, IReadOnlyDictionary<IndexSet, XGaFloat64Multivector> idMultivectorDictionary)
    {
        Processor = processor;
        _basisMapDictionary = idMultivectorDictionary;

        Debug.Assert(
            IsValid()
        );
    }


    
    public bool IsValid()
    {
        return _basisMapDictionary.Values.All(
            d => d.IsValid()
        );
    }
    
    
    public bool ContainsKey(IndexSet key)
    {
        return _basisMapDictionary.ContainsKey(key);
    }

    
    public bool TryGetValue(IndexSet key, out XGaFloat64Multivector value)
    {
        return _basisMapDictionary.TryGetValue(key, out value);
    }
    
    public IXGaFloat64UnilinearMap GetAdjoint()
    {
        throw new NotImplementedException();
    }

    
    public XGaFloat64Multivector MapBasisBlade(IndexSet id)
    {
        return _basisMapDictionary.TryGetValue(id, out var mv)
            ? mv
            : Processor.ScalarZero;
    }

    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
    {
        var composer = Processor.CreateMultivectorComposer();

        if (Count <= multivector.Count)
        {
            foreach (var (id, mv) in _basisMapDictionary.ToTuples())
            {
                if (!multivector.TryGetBasisBladeScalarValue(id, out var scalar))
                    continue;

                composer.AddMultivectorScaled(mv, scalar);
            }
        }
        else
        {
            foreach (var (id, scalar) in multivector.ToTuples())
            {
                if (!_basisMapDictionary.TryGetValue(id, out var mv))
                    continue;

                composer.AddMultivectorScaled(mv, scalar);
            }
        }

        return composer.GetMultivector();
    }

    
    public IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return _basisMapDictionary
            .Where(p => p.Key.VSpaceDimensions() <= vSpaceDimensions)
            .Select(p => 
                new KeyValuePair<IndexSet, XGaFloat64Multivector>(p.Key, p.Value)
            );
    }
        
    public double[,] GetMultivectorMapArray(int rowCount, int colCount)
    {
        var mapArray = 
            new double[rowCount, colCount];

        if (_basisMapDictionary.Count == 0)
            return mapArray;

        var minRowCount = 
            _basisMapDictionary.Values.Max(v => v.VSpaceDimensions);

        if (rowCount < minRowCount)
            throw new InvalidOperationException();

        var maxId = _basisMapDictionary.Keys.Max();

        var minColCount = maxId.ToUInt64();

        if ((ulong) colCount < minColCount)
            throw new InvalidOperationException();

        foreach (var (colId, vector) in _basisMapDictionary.ToTuples())
        {
            var colIndex = colId.ToInt32();

            foreach (var (rowId, scalar) in vector.ToTuples())
            {
                var rowIndex = rowId.ToInt32();

                mapArray[rowIndex, colIndex] = scalar;
            }
        }

        return mapArray;
    }

    
    public IEnumerator<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetEnumerator()
    {
        return _basisMapDictionary.GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}