using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.BitManipulation;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Frames;

public class XGaFloat64BasisVectorFrame :
    IXGaFloat64VectorFrame
{
    
    internal static XGaFloat64BasisVectorFrame Create(XGaFloat64Processor metric, int vSpaceDimensions)
    {
        var vectorArray =
            vSpaceDimensions
                .GetRange()
                .Select(index => 
                    metric.VectorTerm(index)
                ).ToArray();

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }

    
    internal static XGaFloat64BasisVectorFrame Create(IEnumerable<XGaFloat64Vector> vectorList)
    {
        var vectorArray =
            vectorList.ToArray();

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }


    private readonly IReadOnlyList<XGaFloat64Vector> _vectorList;

        
    public XGaFloat64Processor Processor 
        => _vectorList[0].Processor;

    public XGaMetric Metric 
        => _vectorList[0].Metric;

    public int VSpaceDimensions 
        => _vectorList.Max(v => v.VSpaceDimensions);

    public int Count 
        => _vectorList.Count;

    public XGaFloat64Vector this[int index] 
        => _vectorList[index];


    
    private XGaFloat64BasisVectorFrame(IReadOnlyList<XGaFloat64Vector> vectorList)
    {
        _vectorList = vectorList;
    }


    
    public bool IsValid()
    {
        return _vectorList.All(v => v.IsValid());
    }

    /// <summary>
    /// See "Geometric Algebra for Computer Science" section 3.8
    /// </summary>
    /// <returns></returns>
    public XGaFloat64BasisVectorFrame GetReciprocalVectorFrame()
    {
        var pseudoScalarInv =
            _vectorList.Op(Processor).Inverse();

        var vectorArray = new XGaFloat64Vector[Count];

        for (var i = 0; i < Count; i++)
        {
            //TODO: This can be made more efficient
            var vectorList = _vectorList.ToList();
            vectorList.RemoveAt(i);

            var b =
                vectorList.Op(Processor).Lcp(pseudoScalarInv).GetVectorPart();

            vectorArray[i] = i.IsEven() ? b : b.Negative();
        }

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }

    
    public XGaFloat64BasisVectorFrame MapAsBasisUsing(Func<XGaFloat64Vector, XGaFloat64Vector> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(this[index]))
                .ToArray();

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }

    
    public XGaFloat64BasisVectorFrame MapAsBasisUsing(Func<int, XGaFloat64Vector, XGaFloat64Vector> vectorMapping)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => vectorMapping(index, this[index]))
                .ToArray();

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }

    
    public XGaFloat64BasisVectorFrame MapUsing(IXGaFloat64Outermorphism om)
    {
        var vectorArray =
            Count
                .GetRange()
                .Select(index => om.OmMap(this[index]))
                .ToArray();

        return new XGaFloat64BasisVectorFrame(vectorArray);
    }

    
    
    public XGaFloat64BasisKVectorFrame CreateBasisKVectorFrame()
    {
        return XGaFloat64BasisKVectorFrame.CreateFrom(this);
    }

    
    public XGaFloat64BasisMultivectorFrame CreateBasisMultivectorFrame()
    {
        return XGaFloat64BasisMultivectorFrame.CreateFrom(this);
    }


    /// <summary>
    /// See paper "Calculation of Elements of Spin Groups Using Method
    /// of Averaging in Clifford’s Geometric Algebra"
    /// </summary>
    /// <param name="targetFrame"></param>
    /// <returns></returns>
    public XGaFloat64Rotor CreateRotorToFrame(XGaFloat64BasisVectorFrame targetFrame)
    {
        var metric = Processor;

        var mvFrame1 = CreateBasisKVectorFrame().MapAsBasisUsing(mv => mv.Inverse());
        var mvFrame2 = targetFrame.CreateBasisKVectorFrame();

        var rotorMultivector =
            mvFrame2
                .Zip(mvFrame1, (vector, kVector) => (vector, kVector))
                .Select(vectorPair => vectorPair.vector.Gp(vectorPair.kVector))
                .Aggregate(
                    (XGaFloat64Multivector) metric.GradedMultivectorZero,
                    (mv1, mv2) => mv1.Add(mv2)
                );

        rotorMultivector /= rotorMultivector.Sp(rotorMultivector.Reverse()).Sqrt();

        return XGaFloat64Rotor.Create(rotorMultivector);
    }


    
    public IEnumerator<XGaFloat64Vector> GetEnumerator()
    {
        return _vectorList.GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}