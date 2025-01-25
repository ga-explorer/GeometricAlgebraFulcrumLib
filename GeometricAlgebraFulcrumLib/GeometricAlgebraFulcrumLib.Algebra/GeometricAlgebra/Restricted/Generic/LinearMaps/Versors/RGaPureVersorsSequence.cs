using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Versors;

public sealed class RGaPureVersorsSequence<T> : 
    RGaVersorBase<T>, 
    IRGaOutermorphismSequence<T>,
    IReadOnlyList<RGaPureVersor<T>>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureVersorsSequence<T> CreateIdentity(RGaProcessor<T> processor)
    {
        return new RGaPureVersorsSequence<T>(processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureVersorsSequence<T> Create(params RGaVector<T>[] vectorStorages)
    {
        return new RGaPureVersorsSequence<T>(
            vectorStorages.Select(RGaPureVersor<T>.Create).ToList()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureVersorsSequence<T> Create(IEnumerable<RGaVector<T>> vectorStorages)
    {
        return new RGaPureVersorsSequence<T>(
            vectorStorages.Select(RGaPureVersor<T>.Create).ToList()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureVersorsSequence<T> Create(params RGaPureVersor<T>[] versors)
    {
        return new RGaPureVersorsSequence<T>(versors.ToList());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaPureVersorsSequence<T> Create(IEnumerable<RGaPureVersor<T>> versors)
    {
        return new RGaPureVersorsSequence<T>(versors.ToList());
    }


    private readonly List<RGaPureVersor<T>> _versorsList;
        
        
    public int Count 
        => _versorsList.Count;

    public RGaPureVersor<T> this[int index]
    {
        get => _versorsList[index];
        set => _versorsList[index] = 
            value 
            ?? throw new ArgumentNullException(nameof(value));
    }


    private RGaPureVersorsSequence(RGaProcessor<T> processor)
        : base(processor)
    {
        _versorsList = new List<RGaPureVersor<T>>();
    }

    private RGaPureVersorsSequence(List<RGaPureVersor<T>> versorsList)
        : base(versorsList.First().Processor)
    {
        _versorsList = versorsList;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaPureVersor<T> GetVersor(int index)
    {
        return _versorsList[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaPureVersorsSequence<T> AppendVersor(RGaPureVersor<T> versor)
    {
        _versorsList.Add(versor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaPureVersorsSequence<T> PrependVersor(RGaPureVersor<T> versor)
    {
        _versorsList.Insert(0, versor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaPureVersorsSequence<T> InsertVersor(int index, RGaPureVersor<T> versor)
    {
        _versorsList.Insert(index, versor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaPureVersorsSequence<T> GetSubSequence(int startIndex, int count)
    {
        return new RGaPureVersorsSequence<T>(
            _versorsList.Skip(startIndex).Take(count).ToList()
        );
    }

    public IEnumerable<RGaMultivector<T>> GetReflections(RGaMultivector<T> storage)
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
    public RGaVersor<T> GetFinalVersor()
    {
        var storage = 
            _versorsList
                .Skip(1)
                .Select(r => (RGaMultivector<T>) r.Vector)
                .Aggregate(
                    (RGaMultivector<T>) _versorsList[0].Vector, 
                    (current, rotor) => rotor.Gp(current)
                );

        return RGaVersor<T>.Create(storage);
    }

    public RGaPureRotorsSequence<T> CreatePureRotorsSequence()
    {
        if (_versorsList.Count % 2 != 0)
            throw new InvalidOperationException();

        var rotorsCount = _versorsList.Count / 2;

        var simpleRotorsArray = new RGaPureRotor<T>[rotorsCount];

        for (var i = 0; i < rotorsCount; i++)
        {
            var v1 = _versorsList[2 * i + 1].Vector;
            var v2 = _versorsList[2 * i].Vector;

            var scalar = v1.Sp(v2);
            var bivector = v1.Op(v2);

            simpleRotorsArray[i] = RGaPureRotor<T>.Create(scalar.ScalarValue, bivector);
        }

        return RGaPureRotorsSequence<T>.Create(
            simpleRotorsArray
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _versorsList.All(versor => versor.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaVector<T> OmMap(RGaVector<T> mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaBivector<T> OmMap(RGaBivector<T> mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector)
    {
        return _versorsList
            .Aggregate(
                kVector, 
                (kv, rotor) => rotor.OmMap(kv)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> OmMap(RGaMultivector<T> mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaVersor<T> GetVersorInverse()
    {
        return new RGaPureVersorsSequence<T>(
            _versorsList
                .Select(r => r.GetPureDualVersorInverse())
                .Reverse()
                .ToList()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivector()
    {
        return _versorsList
            .Select(r => r.Vector)
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivectorReverse()
    {
        return ((IEnumerable<RGaPureVersor<T>>) _versorsList)
            .Reverse()
            .Select(r => r.Vector)
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> GetMultivectorInverse()
    {
        return ((IEnumerable<RGaPureVersor<T>>) _versorsList)
            .Reverse()
            .Select(r => r.Vector.Inverse())
            .Gp();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<RGaPureVersor<T>> GetEnumerator()
    {
        return _versorsList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IRGaOutermorphism<T>> GetLeafOutermorphisms()
    {
        return _versorsList;
    }
}