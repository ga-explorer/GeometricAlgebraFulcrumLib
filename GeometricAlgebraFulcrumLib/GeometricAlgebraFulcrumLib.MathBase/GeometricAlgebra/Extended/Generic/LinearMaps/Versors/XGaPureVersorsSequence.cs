using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Frames;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;

public sealed class XGaPureVersorsSequence<T> : 
    XGaVersorBase<T>, 
    IXGaOutermorphismSequence<T>,
    IReadOnlyList<XGaPureVersor<T>>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureVersorsSequence<T> CreateIdentity(XGaProcessor<T> processor)
    {
        return new XGaPureVersorsSequence<T>(processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureVersorsSequence<T> Create(params XGaVector<T>[] vectorStorages)
    {
        return new XGaPureVersorsSequence<T>(
            vectorStorages.Select(XGaPureVersor<T>.Create).ToList()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureVersorsSequence<T> Create(IEnumerable<XGaVector<T>> vectorStorages)
    {
        return new XGaPureVersorsSequence<T>(
            vectorStorages.Select(XGaPureVersor<T>.Create).ToList()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureVersorsSequence<T> Create(params XGaPureVersor<T>[] versors)
    {
        return new XGaPureVersorsSequence<T>(versors.ToList());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaPureVersorsSequence<T> Create(IEnumerable<XGaPureVersor<T>> versors)
    {
        return new XGaPureVersorsSequence<T>(versors.ToList());
    }


    private readonly List<XGaPureVersor<T>> _versorsList;
        
        
    public int Count 
        => _versorsList.Count;

    public XGaPureVersor<T> this[int index]
    {
        get => _versorsList[index];
        set => _versorsList[index] = 
            value 
            ?? throw new ArgumentNullException(nameof(value));
    }


    private XGaPureVersorsSequence(XGaProcessor<T> processor)
        : base(processor)
    {
        _versorsList = new List<XGaPureVersor<T>>();
    }

    private XGaPureVersorsSequence(List<XGaPureVersor<T>> versorsList)
        : base(versorsList.First().Processor)
    {
        _versorsList = versorsList;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureVersor<T> GetVersor(int index)
    {
        return _versorsList[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureVersorsSequence<T> AppendVersor(XGaPureVersor<T> versor)
    {
        _versorsList.Add(versor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureVersorsSequence<T> PrependVersor(XGaPureVersor<T> versor)
    {
        _versorsList.Insert(0, versor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureVersorsSequence<T> InsertVersor(int index, XGaPureVersor<T> versor)
    {
        _versorsList.Insert(index, versor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaPureVersorsSequence<T> GetSubSequence(int startIndex, int count)
    {
        return new XGaPureVersorsSequence<T>(
            _versorsList.Skip(startIndex).Take(count).ToList()
        );
    }

    public IEnumerable<XGaMultivector<T>> GetReflections(XGaMultivector<T> storage)
    {
        var v = storage;

        yield return v;

        foreach (var versor in _versorsList)
        {
            v = versor.Map(v);

            yield return v;
        }
    }


    public IEnumerable<T[,]> GetReflectionArrays(int rowsCount)
    {
        var f = 
            Processor.CreateFreeFrameOfBasis(rowsCount);

        yield return f.GetArray(rowsCount);

        foreach (var versor in _versorsList)
            yield return versor.OmMap(f).GetArray(rowsCount);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVersor<T> GetFinalVersor()
    {
        var storage = 
            _versorsList
                .Skip(1)
                .Select(r => (XGaMultivector<T>) r.Vector)
                .Aggregate(
                    (XGaMultivector<T>) _versorsList[0].Vector, 
                    (current, rotor) => rotor.Gp(current)
                );

        return XGaVersor<T>.Create(storage);
    }

    public XGaPureRotorsSequence<T> CreatePureRotorsSequence()
    {
        if (_versorsList.Count % 2 != 0)
            throw new InvalidOperationException();

        var rotorsCount = _versorsList.Count / 2;

        var simpleRotorsArray = new XGaPureRotor<T>[rotorsCount];

        for (var i = 0; i < rotorsCount; i++)
        {
            var v1 = _versorsList[2 * i + 1].Vector;
            var v2 = _versorsList[2 * i].Vector;

            var scalar = v1.Sp(v2);
            var bivector = v1.Op(v2);

            simpleRotorsArray[i] = XGaPureRotor<T>.Create(scalar.ScalarValue(), bivector);
        }

        return XGaPureRotorsSequence<T>.Create(
            simpleRotorsArray
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _versorsList.All(versor => versor.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> OmMap(XGaVector<T> mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> OmMap(XGaBivector<T> mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector)
    {
        return _versorsList
            .Aggregate(
                kVector, 
                (kv, rotor) => rotor.OmMap(kv)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> OmMap(XGaMultivector<T> mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IXGaVersor<T> GetVersorInverse()
    {
        return new XGaPureVersorsSequence<T>(
            _versorsList
                .Select(r => r.GetPureDualVersorInverse())
                .Reverse()
                .ToList()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivector()
    {
        return _versorsList
            .Select(r => r.Vector)
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivectorReverse()
    {
        return ((IEnumerable<XGaPureVersor<T>>) _versorsList)
            .Reverse()
            .Select(r => r.Vector)
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GetMultivectorInverse()
    {
        return ((IEnumerable<XGaPureVersor<T>>) _versorsList)
            .Reverse()
            .Select(r => r.Vector.Inverse())
            .Gp();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<XGaPureVersor<T>> GetEnumerator()
    {
        return _versorsList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IXGaOutermorphism<T>> GetLeafOutermorphisms()
    {
        return _versorsList;
    }
}