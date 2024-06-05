using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Frames;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Versors;

public sealed class RGaFloat64PureVersorsSequence : 
    RGaFloat64VersorBase, 
    IRGaFloat64OutermorphismSequence,
    IReadOnlyList<RGaFloat64PureVersor>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64PureVersorsSequence CreateIdentity(RGaFloat64Processor metric)
    {
        return new RGaFloat64PureVersorsSequence(metric);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64PureVersorsSequence Create(params RGaFloat64Vector[] vectorStorages)
    {
        return new RGaFloat64PureVersorsSequence(
            vectorStorages.Select(RGaFloat64PureVersor.Create).ToList()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64PureVersorsSequence Create(IEnumerable<RGaFloat64Vector> vectorStorages)
    {
        return new RGaFloat64PureVersorsSequence(
            vectorStorages.Select(RGaFloat64PureVersor.Create).ToList()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64PureVersorsSequence Create(params RGaFloat64PureVersor[] versors)
    {
        return new RGaFloat64PureVersorsSequence(versors.ToList());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64PureVersorsSequence Create(IEnumerable<RGaFloat64PureVersor> versors)
    {
        return new RGaFloat64PureVersorsSequence(versors.ToList());
    }


    private readonly List<RGaFloat64PureVersor> _versorsList;
        
        
    public int Count 
        => _versorsList.Count;

    public RGaFloat64PureVersor this[int index]
    {
        get => _versorsList[index];
        set => _versorsList[index] = 
            value 
            ?? throw new ArgumentNullException(nameof(value));
    }


    private RGaFloat64PureVersorsSequence(RGaFloat64Processor metric)
        : base(metric)
    {
        _versorsList = new List<RGaFloat64PureVersor>();
    }

    private RGaFloat64PureVersorsSequence(List<RGaFloat64PureVersor> versorsList)
        : base(versorsList.First().Processor)
    {
        _versorsList = versorsList;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64PureVersor GetVersor(int index)
    {
        return _versorsList[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64PureVersorsSequence AppendVersor(RGaFloat64PureVersor versor)
    {
        _versorsList.Add(versor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64PureVersorsSequence PrependVersor(RGaFloat64PureVersor versor)
    {
        _versorsList.Insert(0, versor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64PureVersorsSequence InsertVersor(int index, RGaFloat64PureVersor versor)
    {
        _versorsList.Insert(index, versor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64PureVersorsSequence GetSubSequence(int startIndex, int count)
    {
        return new RGaFloat64PureVersorsSequence(
            _versorsList.Skip(startIndex).Take(count).ToList()
        );
    }

    public IEnumerable<RGaFloat64Multivector> GetReflections(RGaFloat64Multivector storage)
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Versor GetFinalVersor()
    {
        var storage = 
            _versorsList
                .Skip(1)
                .Select(r => (RGaFloat64Multivector) r.Vector)
                .Aggregate(
                    (RGaFloat64Multivector) _versorsList[0].Vector, 
                    (current, rotor) => rotor.Gp(current)
                );

        return RGaFloat64Versor.Create(storage);
    }

    public RGaFloat64PureRotorsSequence CreatePureRotorsSequence()
    {
        if (_versorsList.Count % 2 != 0)
            throw new InvalidOperationException();

        var rotorsCount = _versorsList.Count / 2;

        var simpleRotorsArray = new RGaFloat64PureRotor[rotorsCount];

        for (var i = 0; i < rotorsCount; i++)
        {
            var v1 = _versorsList[2 * i + 1].Vector;
            var v2 = _versorsList[2 * i].Vector;

            var scalar = v1.Sp(v2);
            var bivector = v1.Op(v2);

            simpleRotorsArray[i] = RGaFloat64PureRotor.Create(scalar.ScalarValue, bivector);
        }

        return RGaFloat64PureRotorsSequence.Create(
            simpleRotorsArray
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _versorsList.All(versor => versor.IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector OmMap(RGaFloat64Vector mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector OmMap(RGaFloat64Bivector mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (bv, rotor) => rotor.OmMap(bv)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector)
    {
        return _versorsList
            .Aggregate(
                kVector, 
                (kv, rotor) => rotor.OmMap(kv)
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector OmMap(RGaFloat64Multivector mv)
    {
        return _versorsList
            .Aggregate(
                mv, 
                (current, rotor) => rotor.OmMap(current)
            );
    }

    public override LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IRGaFloat64Versor GetVersorInverse()
    {
        return new RGaFloat64PureVersorsSequence(
            _versorsList
                .Select(r => r.GetPureDualVersorInverse())
                .Reverse()
                .ToList()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivector()
    {
        return _versorsList
            .Select(r => r.Vector)
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivectorReverse()
    {
        return ((IEnumerable<RGaFloat64PureVersor>) _versorsList)
            .Reverse()
            .Select(r => r.Vector)
            .Gp();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetMultivectorInverse()
    {
        return ((IEnumerable<RGaFloat64PureVersor>) _versorsList)
            .Reverse()
            .Select(r => r.Vector.Inverse())
            .Gp();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<RGaFloat64PureVersor> GetEnumerator()
    {
        return _versorsList.GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<IRGaFloat64Outermorphism> GetLeafOutermorphisms()
    {
        return _versorsList;
    }
}