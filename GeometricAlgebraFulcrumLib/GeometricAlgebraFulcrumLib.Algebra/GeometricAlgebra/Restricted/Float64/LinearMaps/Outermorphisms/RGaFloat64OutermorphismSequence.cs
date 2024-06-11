using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;

public class RGaFloat64OutermorphismSequence :
    RGaFloat64OutermorphismBase,
    IRGaFloat64OutermorphismSequence,
    IReadOnlyList<IRGaFloat64Outermorphism>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64OutermorphismSequence CreateIdentity(RGaFloat64Processor metric)
    {
        return new RGaFloat64OutermorphismSequence(metric);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64OutermorphismSequence Create(RGaFloat64Processor metric, params IRGaFloat64Outermorphism[] outermorphismsList)
    {
        return new RGaFloat64OutermorphismSequence(metric, outermorphismsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64OutermorphismSequence Create(RGaFloat64Processor metric, IEnumerable<IRGaFloat64Outermorphism> outermorphismsList)
    {
        return new RGaFloat64OutermorphismSequence(metric, outermorphismsList);
    }
        
        
    private readonly List<IRGaFloat64Outermorphism> _outermorphismList;

    public override RGaFloat64Processor Processor { get; }
        
    public int Count 
        => _outermorphismList.Count;

    public IRGaFloat64Outermorphism this[int index] 
        => _outermorphismList[index];
        

    private RGaFloat64OutermorphismSequence(RGaFloat64Processor metric)
    {
        _outermorphismList = new List<IRGaFloat64Outermorphism>();

        Processor = metric;
    }

    private RGaFloat64OutermorphismSequence(RGaFloat64Processor metric, IEnumerable<IRGaFloat64Outermorphism> versorsList)
    {
        _outermorphismList = new List<IRGaFloat64Outermorphism>(versorsList);
            
        Processor = metric;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IRGaFloat64Outermorphism GetOutermorphism(int index)
    {
        return _outermorphismList[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64OutermorphismSequence AppendOutermorphism(IRGaFloat64Outermorphism om)
    {
        _outermorphismList.Add(om);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64OutermorphismSequence PrependOutermorphism(IRGaFloat64Outermorphism om)
    {
        _outermorphismList.Insert(0, om);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64OutermorphismSequence InsertOutermorphism(int index, IRGaFloat64Outermorphism om)
    {
        _outermorphismList.Insert(index, om);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64OutermorphismSequence GetSubSequence(int startIndex, int count)
    {
        return new RGaFloat64OutermorphismSequence(
            Processor,
            _outermorphismList.Skip(startIndex).Take(count)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IRGaFloat64Outermorphism> GetOutermorphisms()
    {
        return _outermorphismList;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _outermorphismList.All(om => om.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaFloat64Outermorphism GetOmAdjoint()
    {
        return new RGaFloat64OutermorphismSequence(
            Processor,
            _outermorphismList.Select(om => om.GetOmAdjoint()).Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector OmMapBasisVector(int index)
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector OmMapBasisBivector(int index1, int index2)
    {
        var v1 = OmMapBasisVector(index1);
        var v2 = OmMapBasisVector(index2);

        return v1.Op(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector OmMapBasisBlade(ulong id)
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector OmMap(RGaFloat64Vector vector)
    {
        return _outermorphismList.OmMap(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector)
    {
        return _outermorphismList.OmMap(bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
    {
        return _outermorphismList.OmMap(kVector).GetHigherKVectorPart(kVector.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector)
    {
        return _outermorphismList.OmMap(multivector);
    }
        
    public override IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        if (_outermorphismList.Count == 0)
            throw new InvalidOperationException();

        var om1 = _outermorphismList[0];
        var omList = _outermorphismList.Skip(1);

        return om1
            .GetOmMappedBasisVectors(vSpaceDimensions)
            .Select(r => 
                new KeyValuePair<ulong, RGaFloat64Vector>(
                    r.Key,
                    omList.OmMap(r.Value)
                )
            ).Where(r => !r.Value.IsZero);
    }

    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions)
    {
        if (_outermorphismList.Count == 0)
            throw new InvalidOperationException();

        var om1 = _outermorphismList[0];
        var omList = _outermorphismList.Skip(1);

        return om1
            .GetMappedBasisBlades(vSpaceDimensions)
            .Select(r =>
                new KeyValuePair<ulong, RGaFloat64Multivector>(
                    r.Key, 
                    omList.OmMap(r.Value)
                )
            ).Where(r => !r.Value.IsZero);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IRGaFloat64Outermorphism> GetLeafOutermorphisms()
    {
        foreach (var om in _outermorphismList)
        {
            if (om is IRGaFloat64OutermorphismSequence omSeq)
                foreach (var childOm in omSeq.GetLeafOutermorphisms())
                    yield return childOm;
            else
                yield return om;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<IRGaFloat64Outermorphism> GetEnumerator()
    {
        return _outermorphismList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}