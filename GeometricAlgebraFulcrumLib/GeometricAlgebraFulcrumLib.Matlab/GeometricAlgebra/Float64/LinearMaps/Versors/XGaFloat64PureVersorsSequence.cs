using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Versors;

public sealed class XGaFloat64PureVersorsSequence : 
    XGaFloat64VersorBase, 
    IXGaFloat64OutermorphismSequence,
    IReadOnlyList<XGaFloat64PureVersor>
{
    
    public static XGaFloat64PureVersorsSequence CreateIdentity(XGaFloat64Processor metric)
    {
        return new XGaFloat64PureVersorsSequence(metric);
    }

    
    public static XGaFloat64PureVersorsSequence Create(params XGaFloat64Vector[] vectorStorages)
    {
        return new XGaFloat64PureVersorsSequence(
            vectorStorages.Select(XGaFloat64PureVersor.Create).ToList()
        );
    }

    
    public static XGaFloat64PureVersorsSequence Create(IEnumerable<XGaFloat64Vector> vectorStorages)
    {
        return new XGaFloat64PureVersorsSequence(
            vectorStorages.Select(XGaFloat64PureVersor.Create).ToList()
        );
    }

    
    public static XGaFloat64PureVersorsSequence Create(params XGaFloat64PureVersor[] versors)
    {
        return new XGaFloat64PureVersorsSequence(versors.ToList());
    }

    
    public static XGaFloat64PureVersorsSequence Create(IEnumerable<XGaFloat64PureVersor> versors)
    {
        return new XGaFloat64PureVersorsSequence(versors.ToList());
    }


    private readonly List<XGaFloat64PureVersor> _versorsList;
        
        
    public int Count 
        => _versorsList.Count;

    public XGaFloat64PureVersor this[int index]
    {
        get => _versorsList[index];
        set => _versorsList[index] = 
            value 
            ?? throw new ArgumentNullException(nameof(value));
    }


    private XGaFloat64PureVersorsSequence(XGaFloat64Processor metric)
        : base(metric)
    {
        _versorsList = new List<XGaFloat64PureVersor>();
    }

    private XGaFloat64PureVersorsSequence(List<XGaFloat64PureVersor> versorsList)
        : base(versorsList.First().Processor)
    {
        _versorsList = versorsList;
    }


    
    public XGaFloat64PureVersor GetVersor(int index)
    {
        return _versorsList[index];
    }

    
    public XGaFloat64PureVersorsSequence AppendVersor(XGaFloat64PureVersor versor)
    {
        _versorsList.Add(versor);

        return this;
    }

    
    public XGaFloat64PureVersorsSequence PrependVersor(XGaFloat64PureVersor versor)
    {
        _versorsList.Insert(0, versor);

        return this;
    }

    
    public XGaFloat64PureVersorsSequence InsertVersor(int index, XGaFloat64PureVersor versor)
    {
        _versorsList.Insert(index, versor);

        return this;
    }

    
    public XGaFloat64PureVersorsSequence GetSubSequence(int startIndex, int count)
    {
        return new XGaFloat64PureVersorsSequence(
            _versorsList.Skip(startIndex).Take(count).ToList()
        );
    }

    public IEnumerable<XGaFloat64Multivector> GetReflections(XGaFloat64Multivector storage)
    {
        var v = storage;

        yield return v;

        foreach (var versor in _versorsList)
        {
            v = versor.Map(v);

            yield return v;
        }
    }


    public IEnumerable<double[,]> GetReflectionArrays(int rowsCount)
    {
        var f = 
            Processor.CreateFreeFrameOfBasis(rowsCount);

        yield return f.GetArray(rowsCount);

        foreach (var versor in _versorsList)
            yield return versor.OmMap(f).GetArray(rowsCount);
    }
        
    
    public XGaFloat64Versor GetFinalVersor()
    {
        var storage = 
            _versorsList
                .Skip(1)
                .Select(r => (XGaFloat64Multivector) r.Vector)
                .Aggregate(
                    (XGaFloat64Multivector) _versorsList[0].Vector, 
                    (current, rotor) => rotor.Gp(current)
                );

        return XGaFloat64Versor.Create(storage);
    }

    public XGaFloat64PureRotorSequence CreatePureRotorsSequence()
    {
        if (_versorsList.Count % 2 != 0)
            throw new InvalidOperationException();

        var rotorsCount = _versorsList.Count / 2;

        var simpleRotorsArray = new XGaFloat64PureRotor[rotorsCount];

        for (var i = 0; i < rotorsCount; i++)
        {
            var v1 = _versorsList[2 * i + 1].Vector;
            var v2 = _versorsList[2 * i].Vector;

            var scalar = v1.Sp(v2);
            var bivector = v1.Op(v2);

            simpleRotorsArray[i] = XGaFloat64PureRotor.Create(scalar.ScalarValue, bivector);
        }

        return XGaFloat64PureRotorSequence.Create(
            simpleRotorsArray
        );
    }


    
    public override bool IsValid()
    {
        return _versorsList.All(versor => versor.IsValid());
    }

    
    public override XGaFloat64Vector OmMap(XGaFloat64Vector mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    
    public override XGaFloat64Bivector OmMap(XGaFloat64Bivector mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    
    public override XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector)
    {
        return _versorsList
            .Aggregate(
                kVector, 
                (kv, rotor) => rotor.OmMap(kv)
            );
    }
        
    
    public override XGaFloat64Multivector OmMap(XGaFloat64Multivector mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    
    public override IXGaFloat64Versor GetVersorInverse()
    {
        return new XGaFloat64PureVersorsSequence(
            _versorsList
                .Select(r => r.GetPureDualVersorInverse())
                .Reverse()
                .ToList()
        );
    }

    
    public override XGaFloat64Multivector GetMultivector()
    {
        return _versorsList
            .Select(r => r.Vector)
            .Gp();
    }

    
    public override XGaFloat64Multivector GetMultivectorReverse()
    {
        return ((IEnumerable<XGaFloat64PureVersor>) _versorsList)
            .Reverse()
            .Select(r => r.Vector)
            .Gp();
    }

    
    public override XGaFloat64Multivector GetMultivectorInverse()
    {
        return ((IEnumerable<XGaFloat64PureVersor>) _versorsList)
            .Reverse()
            .Select(r => r.Vector.Inverse())
            .Gp();
    }
        
    
    public IEnumerator<XGaFloat64PureVersor> GetEnumerator()
    {
        return _versorsList.GetEnumerator();
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    public IEnumerable<IXGaFloat64Outermorphism> GetLeafOutermorphisms()
    {
        return _versorsList;
    }
}