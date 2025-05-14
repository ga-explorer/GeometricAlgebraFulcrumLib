using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;

public class XGaOutermorphismSequence<T> :
    XGaOutermorphismBase<T>,
    IXGaOutermorphismSequence<T>,
    IReadOnlyList<IXGaOutermorphism<T>>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaOutermorphismSequence<T> CreateIdentity(XGaProcessor<T> processor)
    {
        return new XGaOutermorphismSequence<T>(processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaOutermorphismSequence<T> Create(XGaProcessor<T> processor, params IXGaOutermorphism<T>[] outermorphismsList)
    {
        return new XGaOutermorphismSequence<T>(processor, outermorphismsList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaOutermorphismSequence<T> Create(XGaProcessor<T> processor, IEnumerable<IXGaOutermorphism<T>> outermorphismsList)
    {
        return new XGaOutermorphismSequence<T>(processor, outermorphismsList);
    }
        
        
    private readonly List<IXGaOutermorphism<T>> _outermorphismList;

    public override XGaProcessor<T> Processor { get; }

    public int Count 
        => _outermorphismList.Count;

    public IXGaOutermorphism<T> this[int index] 
        => _outermorphismList[index];
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaOutermorphismSequence(XGaProcessor<T> processor)
    {
        _outermorphismList = new List<IXGaOutermorphism<T>>();

        Processor = processor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private XGaOutermorphismSequence(XGaProcessor<T> processor, IEnumerable<IXGaOutermorphism<T>> versorsList)
    {
        _outermorphismList = new List<IXGaOutermorphism<T>>(versorsList);
            
        Processor = processor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IXGaOutermorphism<T> GetOutermorphism(int index)
    {
        return _outermorphismList[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaOutermorphismSequence<T> AppendOutermorphism(IXGaOutermorphism<T> om)
    {
        _outermorphismList.Add(om);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaOutermorphismSequence<T> PrependOutermorphism(IXGaOutermorphism<T> om)
    {
        _outermorphismList.Insert(0, om);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaOutermorphismSequence<T> InsertOutermorphism(int index, IXGaOutermorphism<T> om)
    {
        _outermorphismList.Insert(index, om);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaOutermorphismSequence<T> GetSubSequence(int startIndex, int count)
    {
        return new XGaOutermorphismSequence<T>(
            Processor,
            _outermorphismList.Skip(startIndex).Take(count)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IXGaOutermorphism<T>> GetOutermorphisms()
    {
        return _outermorphismList;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _outermorphismList.All(om => om.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaOutermorphism<T> GetOmAdjoint()
    {
        return new XGaOutermorphismSequence<T>(
            Processor,
            _outermorphismList.Select(om => om.GetOmAdjoint()).Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> OmMapBasisVector(int index)
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
    public override XGaBivector<T> OmMapBasisBivector(int index1, int index2)
    {
        var v1 = OmMapBasisVector(index1);
        var v2 = OmMapBasisVector(index2);

        return v1.Op(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> OmMapBasisBlade(IndexSet id)
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
    public override XGaVector<T> OmMap(XGaVector<T> vector)
    {
        return _outermorphismList.OmMap(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMap(XGaBivector<T> bivector)
    {
        return _outermorphismList.OmMap(bivector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
    {
        return _outermorphismList.OmMap(kVector).GetHigherKVectorPart(kVector.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> OmMap(XGaMultivector<T> multivector)
    {
        return _outermorphismList.OmMap(multivector);
    }
        
    public override IEnumerable<KeyValuePair<IndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions)
    {
        if (_outermorphismList.Count == 0)
            throw new InvalidOperationException();

        var om1 = _outermorphismList[0];
        var omList = _outermorphismList.Skip(1);

        return om1
            .GetOmMappedBasisVectors(vSpaceDimensions)
            .Select(r => 
                new KeyValuePair<IndexSet, XGaVector<T>>(
                    r.Key,
                    omList.OmMap(r.Value)
                )
            ).Where(r => !r.Value.IsZero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions)
    {
        return ScalarProcessor.CreateLinUnilinearMap(
            vSpaceDimensions,
            index => OmMapBasisVector(index).ToLinVector()
        );
    }

    public override IEnumerable<KeyValuePair<IndexSet, XGaMultivector<T>>> GetMappedBasisBlades(
        int vSpaceDimensions)
    {
        if (_outermorphismList.Count == 0)
            throw new InvalidOperationException();

        var om1 = _outermorphismList[0];
        var omList = _outermorphismList.Skip(1);

        return om1
            .GetMappedBasisBlades(vSpaceDimensions)
            .Select(r => 
                new KeyValuePair<IndexSet, XGaMultivector<T>>(
                    r.Key, 
                    omList.OmMap(r.Value)
                )
            ).Where(r => !r.Value.IsZero);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IXGaOutermorphism<T>> GetLeafOutermorphisms()
    {
        foreach (var om in _outermorphismList)
        {
            if (om is IXGaOutermorphismSequence<T> omSeq)
                foreach (var childOm in omSeq.GetLeafOutermorphisms())
                    yield return childOm;
            else
                yield return om;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<IXGaOutermorphism<T>> GetEnumerator()
    {
        return _outermorphismList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}