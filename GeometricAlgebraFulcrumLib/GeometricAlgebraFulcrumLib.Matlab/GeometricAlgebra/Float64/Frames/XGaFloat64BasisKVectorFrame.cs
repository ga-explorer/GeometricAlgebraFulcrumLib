using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Combibnations;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Frames;

public class XGaFloat64BasisKVectorFrame :
    IXGaFloat64KVectorFrame
{
    
    internal static XGaFloat64BasisKVectorFrame Create(XGaFloat64Processor metric, int vSpaceDimensions)
    {
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var kVectorArray =
            gaSpaceDimensions
                .GetRange()
                .Select(id => metric.KVectorTerm(id.ToUInt64IndexSet())
                ).ToArray();

        return new XGaFloat64BasisKVectorFrame(kVectorArray);
    }

    
    internal static XGaFloat64BasisKVectorFrame Create(IEnumerable<XGaFloat64KVector> kVectorList)
    {
        var kVectorArray =
            kVectorList.ToArray();

        return new XGaFloat64BasisKVectorFrame(kVectorArray);
    }

    internal static XGaFloat64BasisKVectorFrame CreateFrom(XGaFloat64BasisVectorFrame vectorFrame)
    {
        var metric = vectorFrame.Processor;
        var vSpaceDimensions = vectorFrame.VSpaceDimensions;
        var gaSpaceDimensions = 1UL << vSpaceDimensions;

        var kVectorArray = new XGaFloat64KVector[gaSpaceDimensions];

        kVectorArray[0] = metric.ScalarOne;

        for (var index = 0; index < vectorFrame.Count; index++)
            kVectorArray[1ul << index] = vectorFrame[index];

        for (var grade = 2; grade <= vSpaceDimensions; grade++)
        {
            var kvSpaceDimensions =
                vSpaceDimensions.GetBinomialCoefficient(grade);

            for (var index = 0UL; index < kvSpaceDimensions; index++)
            {
                var id = BasisBladeUtils.BasisBladeGradeIndexToId((uint) grade, index);

                var (basisVectorId, basisBladeId) =
                    id.SplitByLargestBasisVectorId();

                kVectorArray[id.ToUInt64()] = kVectorArray[basisBladeId.ToUInt64()].Op(
                    kVectorArray[basisVectorId.ToUInt64()]
                );
            }
        }

        return new XGaFloat64BasisKVectorFrame(kVectorArray);
    }


    private readonly IReadOnlyList<XGaFloat64KVector> _kVectorArray;


    public XGaFloat64Processor Processor 
        => _kVectorArray[0].Processor;
        
    public XGaMetric Metric 
        => _kVectorArray[0].Metric;

    public int VSpaceDimensions 
        => _kVectorArray.Max(kv => kv.VSpaceDimensions);

    public int Count
        => _kVectorArray.Count;

    public XGaFloat64KVector this[int index] 
        => _kVectorArray[index];


    
    private XGaFloat64BasisKVectorFrame(IReadOnlyList<XGaFloat64KVector> kVectorArray)
    {
        _kVectorArray = kVectorArray;
    }


    
    public XGaFloat64BasisKVectorFrame MapAsBasisUsing(Func<XGaFloat64KVector, XGaFloat64KVector> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new XGaFloat64BasisKVectorFrame(vectorArray);
    }


    
    public bool IsValid()
    {
        return _kVectorArray.All(kv => kv.IsValid());
    }

    
    public IEnumerator<XGaFloat64KVector> GetEnumerator()
    {
        return _kVectorArray.GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}