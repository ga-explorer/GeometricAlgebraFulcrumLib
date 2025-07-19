using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Dense;

public sealed class RGaFloat64Vector :
    RGaFloat64KVector
{
    private readonly double[] _scalarArray;
    

    public int VSpaceDimensions 
        => _scalarArray.Length;

    public override int Grade 
        => 1;
    
    public IEnumerable<double> Scalars 
        => _scalarArray;

    public IEnumerable<int> Indices
        => Enumerable.Range(0, VSpaceDimensions);

    public IEnumerable<Tuple<int, double>> IndexScalarTuples
        => Enumerable
            .Range(0, VSpaceDimensions)
            .Select(i => new Tuple<int, double>(i, _scalarArray[i]));

    public double this[int i]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _scalarArray[i];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => SetItem(i, value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector(RGaFloat64Processor processor, int vSpaceDimensions)
        : base(processor)
    {
        if (vSpaceDimensions < 3)
            throw new InvalidOperationException();

        _scalarArray = new double[vSpaceDimensions];
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetItem(int i)
    {
        return _scalarArray[i];
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector SetItem(int i, double value)
    {
        if (!value.IsValid())
            throw new InvalidOperationException();

        _scalarArray[i] = value;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector AddItem(int i, double value)
    {
        if (!value.IsValid())
            throw new InvalidOperationException();

        _scalarArray[i] += value;

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector SubtractItem(int i, double value)
    {
        if (!value.IsValid())
            throw new InvalidOperationException();

        _scalarArray[i] -= value;

        return this;
    }

    //public RGaFloat64Bivector Gp(RGaFloat64Vector v2)
    //{
    //    var n = Math.Max(VSpaceDimensions, v2.VSpaceDimensions);

    //    var s = new RGaFloat64Scalar(Processor);
    //    var bv = new RGaFloat64Bivector(Processor, n);

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


    public RGaFloat64Bivector Op(RGaFloat64Vector v2)
    {
        var n = Math.Max(VSpaceDimensions, v2.VSpaceDimensions);

        var bv = new RGaFloat64Bivector(Processor, n);

        for (var i = 0; i < VSpaceDimensions; i++)
        {
            var s1 = GetItem(i);

            if (s1.IsZero()) continue;

            for (var j = 0; j < i; j++)
            {
                var s2 = v2.GetItem(j);

                if (s2.IsZero()) continue;

                var scalar = s1 * s2;

                bv.SetItem(j, i, -scalar);
            }

            for (var j = i + 1; j < v2.VSpaceDimensions; j++)
            {
                var s2 = v2.GetItem(j);

                if (s2.IsZero()) continue;

                var scalar = s1 * s2;

                bv.SetItem(i, j, scalar);
            }
        }

        return bv;
    }
    

    public RGaFloat64Scalar ESp(RGaFloat64Vector v2)
    {
        var n = Math.Min(VSpaceDimensions, v2.VSpaceDimensions);

        var s = 0d;

        for (var i = 0; i < n; i++)
        {
            var s1 = GetItem(i);
            var s2 = v2.GetItem(i);

            s += s1 * s2;
        }

        return new RGaFloat64Scalar(Processor, s);
    }

    public RGaFloat64Scalar Sp(RGaFloat64Vector v2)
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

        return new RGaFloat64Scalar(Processor, s);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ELcp(RGaFloat64Vector v2)
    {
        return ESp(v2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Lcp(RGaFloat64Vector v2)
    {
        return Sp(v2);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ERcp(RGaFloat64Vector v2)
    {
        return ESp(v2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Rcp(RGaFloat64Vector v2)
    {
        return Sp(v2);
    }
}