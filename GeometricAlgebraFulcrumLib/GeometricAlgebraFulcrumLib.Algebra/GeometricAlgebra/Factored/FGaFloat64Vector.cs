using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Factored;

public sealed class FGaFloat64Vector :
    FGaFloat64KVector
{
    private readonly IReadOnlyDictionary<int, double> _indexScalarDictionary;
    

    public int VSpaceDimensions 
        => _indexScalarDictionary.Count == 0 ? 0 : _indexScalarDictionary.Keys.Max();

    public override int Grade 
        => 1;
    
    public IEnumerable<double> Scalars 
        => _indexScalarDictionary.Values;

    public IEnumerable<int> Indices
        => _indexScalarDictionary.Keys;

    public IEnumerable<Tuple<int, double>> IndexScalarTuples
        => _indexScalarDictionary.Select(p => 
            new Tuple<int, double>(p.Key, p.Value)
        );

    public double this[int i]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => GetItem(i);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FGaFloat64Vector(FGaFloat64Processor processor, IReadOnlyDictionary<int, double> indexScalarDictionary)
        : base(processor)
    {
        _indexScalarDictionary = indexScalarDictionary;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetItem(int i)
    {
        return _indexScalarDictionary.GetValueOrDefault(i, 0d);
    }


    //public FGaFloat64Bivector Gp(FGaFloat64Vector v2)
    //{
    //    var n = Math.Max(VSpaceDimensions, v2.VSpaceDimensions);

    //    var s = new FGaFloat64Scalar(Processor);
    //    var bv = new FGaFloat64Bivector(Processor, n);

    //    for (var i = 0; i < VSpaceDimensions; i++)
    //    {
    //        var s1 = GetItem(i);

    //        if (s1.IsZero()) continue;

    //        for (var j = 0; j < i; j++)
    //        {
    //            var s2 = v2.GetItem(j);

    //            if (s2.IsZero()) continue;

    //            var scalar = s1 * s2 * Processor.GpSign();

    //            bv.SetItem(j, i, -scalar);
    //        }

    //        {
    //            var s2 = v2.GetItem(i);

    //            if (!s2.IsZero())
    //            {
    //                var scalar = s1 * s2 * Processor.SpSquaredSign();

    //                s.ScalarValue += scalar;
    //            }
    //        }

    //        for (var j = i + 1; j < v2.VSpaceDimensions; j++)
    //        {
    //            var s2 = v2.GetItem(j);

    //            if (s2.IsZero()) continue;

    //            var scalar = s1 * s2 * Processor.GpSign();

    //            bv.SetItem(i, j, scalar);
    //        }
    //    }

    //    return bv;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FGaFloat64Bivector Op(FGaFloat64Vector v2)
    {
        return new FGaFloat64Bivector(Processor, _indexScalarDictionary, v2._indexScalarDictionary);
    }
    

    public FGaFloat64Scalar ESp(FGaFloat64Vector v2)
    {
        var n = Math.Min(VSpaceDimensions, v2.VSpaceDimensions);

        var s = 0d;

        for (var i = 0; i < n; i++)
        {
            var s1 = GetItem(i);
            var s2 = v2.GetItem(i);

            s += s1 * s2;
        }

        return new FGaFloat64Scalar(Processor, s);
    }

    public FGaFloat64Scalar Sp(FGaFloat64Vector v2)
    {
        var n = Math.Min(VSpaceDimensions, v2.VSpaceDimensions);

        var s = 0d;

        for (var i = 0; i < n; i++)
        {
            var sig = Processor.Signature(i);

            if (sig.IsZero) continue;

            var s1 = GetItem(i);
            var s2 = v2.GetItem(i);

            if (sig.IsPositive)
                s += s1 * s2;
            else
                s -= s1 * s2;
        }

        return new FGaFloat64Scalar(Processor, s);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FGaFloat64Scalar ELcp(FGaFloat64Vector v2)
    {
        return ESp(v2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FGaFloat64Scalar Lcp(FGaFloat64Vector v2)
    {
        return Sp(v2);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FGaFloat64Scalar ERcp(FGaFloat64Vector v2)
    {
        return ESp(v2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FGaFloat64Scalar Rcp(FGaFloat64Vector v2)
    {
        return Sp(v2);
    }
}