using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Factored;

public sealed class FGaFloat64Bivector :
    FGaFloat64KVector
{
    private readonly IReadOnlyDictionary<int, double> _indexScalarDictionary1;
    private readonly IReadOnlyDictionary<int, double> _indexScalarDictionary2;

    
    public int VSpaceDimensions 
        => Math.Max(
            _indexScalarDictionary1.Count == 0 ? 0 : _indexScalarDictionary1.Keys.Max(), 
            _indexScalarDictionary2.Count == 0 ? 0 : _indexScalarDictionary2.Keys.Max()
        );

    public override int Grade 
        => 2;
    
    //public IEnumerable<Tuple<int, int, double>> IndexScalarTuples
    //{
    //    get
    //    {
    //        var n1 = _scalarArray1.Count;
    //        var n2 = _scalarArray2.Count;

    //        if (n1 == n2)
    //        {
    //            for (var i1 = 0; i1 < n1; i1++)
    //            for (var i2 = i1 + 1; i2 < n2; i2++)
    //            {
    //                yield return new Tuple<int, int, double>(
    //                    i1, i2,
    //                    _scalarArray1[i1] * _scalarArray2[i2] - _scalarArray1[i2] * _scalarArray2[i1]
    //                );
    //            }
    //        }
    //    }
    //}

    public double this[int i1, int i2]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => GetItem(i1, i2);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FGaFloat64Bivector(FGaFloat64Processor processor, IReadOnlyDictionary<int, double> indexScalarDictionary1, IReadOnlyDictionary<int, double> indexScalarDictionary2)
        : base(processor)
    {
        _indexScalarDictionary1 = indexScalarDictionary1;
        _indexScalarDictionary2 = indexScalarDictionary2;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetItem(int i1, int i2)
    {
        if (i1 == i2)
            throw new InvalidOperationException();

        var a1 = _indexScalarDictionary1.GetValueOrDefault(i1, 0d);
        var a2 = _indexScalarDictionary2.GetValueOrDefault(i2, 0d);

        var b1 = _indexScalarDictionary1.GetValueOrDefault(i2, 0d);
        var b2 = _indexScalarDictionary2.GetValueOrDefault(i1, 0d);

        return a1 * a2 - b1 * b2;
    }

    
}