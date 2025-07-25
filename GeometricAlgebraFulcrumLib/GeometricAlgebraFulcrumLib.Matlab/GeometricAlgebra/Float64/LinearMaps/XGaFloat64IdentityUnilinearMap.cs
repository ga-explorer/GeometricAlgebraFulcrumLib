﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps;

public sealed class XGaFloat64IdentityUnilinearMap :
    IXGaFloat64UnilinearMap
{
    public XGaFloat64Processor Processor { get; }
        
    public XGaMetric Metric 
        => Processor;


    
    internal XGaFloat64IdentityUnilinearMap(XGaFloat64Processor processor)
    {
        Processor = processor;
    }


    
    public bool IsValid()
    {
        return true;
    }
    
    
    public IXGaFloat64UnilinearMap GetAdjoint()
    {
        return this;
    }

    
    public XGaFloat64Multivector MapBasisBlade(IndexSet id)
    {
        return Processor.KVectorTerm(id, 1d);
    }

    
    public XGaFloat64Multivector Map(XGaFloat64Multivector multivector)
    {
        return multivector;
    }

    
    public IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        return Processor
            .GetBasisBladeIds(vSpaceDimensions)
            .Select(id => 
                new KeyValuePair<IndexSet, XGaFloat64Multivector>(
                    id, 
                    Processor.KVectorTerm(id, 1d)
                )
            );
    }
        
    public double[,] GetMultivectorMapArray(int rowCount, int colCount)
    {
        var mapArray = 
            new double[rowCount, colCount];

        var n = Math.Min(rowCount, colCount);

        for (var i = 0; i < n; i++)
            mapArray[i, i] = 1d;

        return mapArray;
    }
}