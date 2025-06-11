using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public class XGaFloat64OutermorphismSequence :
    XGaFloat64OutermorphismBase,
    IXGaFloat64OutermorphismSequence,
    IReadOnlyList<IXGaFloat64Outermorphism>
{
    
    public static XGaFloat64OutermorphismSequence CreateIdentity(XGaFloat64Processor metric)
    {
        return new XGaFloat64OutermorphismSequence(metric);
    }

    
    public static XGaFloat64OutermorphismSequence Create(XGaFloat64Processor metric, params IXGaFloat64Outermorphism[] outermorphismsList)
    {
        return new XGaFloat64OutermorphismSequence(metric, outermorphismsList);
    }

    
    public static XGaFloat64OutermorphismSequence Create(XGaFloat64Processor metric, IEnumerable<IXGaFloat64Outermorphism> outermorphismsList)
    {
        return new XGaFloat64OutermorphismSequence(metric, outermorphismsList);
    }
        
        
    private readonly List<IXGaFloat64Outermorphism> _outermorphismList;

    public override XGaFloat64Processor Processor { get; }
        
    public int Count 
        => _outermorphismList.Count;

    public IXGaFloat64Outermorphism this[int index] 
        => _outermorphismList[index];
        

    private XGaFloat64OutermorphismSequence(XGaFloat64Processor metric)
    {
        _outermorphismList = new List<IXGaFloat64Outermorphism>();

        Processor = metric;
    }

    private XGaFloat64OutermorphismSequence(XGaFloat64Processor metric, IEnumerable<IXGaFloat64Outermorphism> versorsList)
    {
        _outermorphismList = new List<IXGaFloat64Outermorphism>(versorsList);
            
        Processor = metric;
    }


    
    public IXGaFloat64Outermorphism GetOutermorphism(int index)
    {
        return _outermorphismList[index];
    }

    
    public XGaFloat64OutermorphismSequence AppendOutermorphism(IXGaFloat64Outermorphism om)
    {
        _outermorphismList.Add(om);

        return this;
    }

    
    public XGaFloat64OutermorphismSequence PrependOutermorphism(IXGaFloat64Outermorphism om)
    {
        _outermorphismList.Insert(0, om);

        return this;
    }

    
    public XGaFloat64OutermorphismSequence InsertOutermorphism(int index, IXGaFloat64Outermorphism om)
    {
        _outermorphismList.Insert(index, om);

        return this;
    }

    
    public XGaFloat64OutermorphismSequence GetSubSequence(int startIndex, int count)
    {
        return new XGaFloat64OutermorphismSequence(
            Processor,
            _outermorphismList.Skip(startIndex).Take(count)
        );
    }

    
    public IEnumerable<IXGaFloat64Outermorphism> GetOutermorphisms()
    {
        return _outermorphismList;
    }
        
    
    public override bool IsValid()
    {
        return _outermorphismList.All(om => om.IsValid());
    }

    
    public override IXGaFloat64Outermorphism GetOmAdjoint()
    {
        return new XGaFloat64OutermorphismSequence(
            Processor,
            _outermorphismList.Select(om => om.GetOmAdjoint()).Reverse()
        );
    }

    
    public override XGaFloat64Vector OmMapBasisVector(int index)
    {
        var kVector = _outermorphismList[0].OmMapBasisVector(index);

        return _outermorphismList
            .Skip(1)
            .Aggregate(
                kVector,
                (v, om) => om.OmMap(v)
            );

        //return _outermorphismsList.OmMapBasisVector(index);
    }
        
    
    public override XGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        var v1 = OmMapBasisVector(index1);
        var v2 = OmMapBasisVector(index2);

        return v1.Op(v2);
    }

    
    public override XGaFloat64KVector OmMapBasisBlade(IndexSet id)
    {
        var kVector = _outermorphismList[0].OmMapBasisBlade(id);

        return _outermorphismList
            .Skip(1)
            .Aggregate(
                kVector,
                (v, om) => om.OmMap(v)
            );

        //return _outermorphismsList.OmMapBasisBlade(id);
    }
        
    
    public override XGaFloat64Vector OmMap(XGaFloat64Vector vector)
    {
        return _outermorphismList.OmMap(vector);
    }

    
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector)
    {
        return _outermorphismList.OmMap(bivector);
    }

    
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        return _outermorphismList.OmMap(kVector).GetHigherKVectorPart(kVector.Grade);
    }
        
    
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector)
    {
        return _outermorphismList.OmMap(multivector);
    }
        
    public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(
        int vSpaceDimensions)
    {
        if (_outermorphismList.Count == 0)
            throw new InvalidOperationException();

        var om1 = _outermorphismList[0];
        var omList = _outermorphismList.Skip(1);

        return om1
            .GetOmMappedBasisVectors(vSpaceDimensions)
            .Select(r => 
                new KeyValuePair<IndexSet, XGaFloat64Vector>(
                    r.Key,
                    omList.OmMap(r.Value)
                )
            ).Where(r => !r.Value.IsZero);
    }
        
    public override IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(
        int vSpaceDimensions)
    {
        if (_outermorphismList.Count == 0)
            throw new InvalidOperationException();

        var om1 = _outermorphismList[0];
        var omList = _outermorphismList.Skip(1);

        return om1
            .GetMappedBasisBlades(vSpaceDimensions)
            .Select(r => 
                new KeyValuePair<IndexSet, XGaFloat64Multivector>(
                    r.Key, 
                    omList.OmMap(r.Value)
                )
            ).Where(r => !r.Value.IsZero);
    }
        
    
    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        return vSpaceDimensions.CreateLinUnilinearMap(
            index => 
                OmMapBasisVector(index).ToLinVector()
        );
    }

    
    public IEnumerable<IXGaFloat64Outermorphism> GetLeafOutermorphisms()
    {
        foreach (var om in _outermorphismList)
        {
            if (om is IXGaFloat64OutermorphismSequence omSeq)
                foreach (var childOm in omSeq.GetLeafOutermorphisms())
                    yield return childOm;
            else
                yield return om;
        }
    }

    
    public IEnumerator<IXGaFloat64Outermorphism> GetEnumerator()
    {
        return _outermorphismList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}