using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;

using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Structures;

public sealed class GaMultivectorSparseList :
    IReadOnlyCollection<double>
{
    private readonly Dictionary<ulong, double> _numbersDictionary
        = new Dictionary<ulong, double>();


    public int Count 
        => _numbersDictionary.Count;

    public uint VSpaceDimensions 
        => (uint) BitOperations.TrailingZeroCount(GaSpaceDimensions);

    public ulong GaSpaceDimensions { get; }

    public double this[uint grade, ulong index]
    {
        get
        {
            var id = BasisBladeDataLookup.BasisBladeId(grade, index);

            return this[id];
        }
        set
        {
            var id = BasisBladeDataLookup.BasisBladeId(grade, index);

            this[id] = value;
        }
    }

    public double this[ulong id]
    {
        get
        {
            Debug.Assert(id < GaSpaceDimensions);

            return _numbersDictionary.TryGetValue(id, out var number) 
                ? number : 0d;
        }
        set
        {
            Debug.Assert(value.IsValid() && id < GaSpaceDimensions);

            if (_numbersDictionary.ContainsKey(id))
            {
                if (value == 0)
                    _numbersDictionary.Remove(id);
                else
                    _numbersDictionary[id] = value;
            }

            else if (value != 0)
                _numbersDictionary.Add(id, value);
        }
    }

    public int StoredPairsCount 
        => _numbersDictionary.Count;

    public IEnumerable<ulong> StoredIDs 
        => _numbersDictionary.Keys;

    public IEnumerable<double> StoredNumbers 
        => _numbersDictionary.Values;

    public IEnumerable<KeyValuePair<ulong, double>> StoredIdNumberPairs
        => _numbersDictionary;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivectorSparseList(ulong gaSpaceDimensions)
    {
        if (BitOperations.PopCount(gaSpaceDimensions) != 1)
            throw new ArgumentException(nameof(gaSpaceDimensions));

        GaSpaceDimensions = gaSpaceDimensions;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GaMultivectorSparseList(ulong gaSpaceDimensions, Dictionary<ulong, double> numbersDictionary)
    {
        if (BitOperations.PopCount(gaSpaceDimensions) != 1)
            throw new ArgumentException(nameof(gaSpaceDimensions));

        GaSpaceDimensions = gaSpaceDimensions;
        _numbersDictionary = numbersDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _numbersDictionary.Clear();
    }
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public GaFloat64SparseMultivectorList RemoveNumbers()
    //{
    //    var keys = 
    //        _numbersDictionary
    //            .Where(p => p.Value == 0d)
    //            .Select(p => p.Key)
    //            .ToArray();

    //    foreach (var key in keys)
    //        _numbersDictionary.Remove(key);

    //    return this;
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivectorSparseList RemoveNearZeroNumbers(double zeroEpsilon)
    {
        var keys = 
            _numbersDictionary
                .Where(p => p.Value.IsNearZero(zeroEpsilon))
                .Select(p => p.Key)
                .ToArray();

        foreach (var key in keys)
            _numbersDictionary.Remove(key);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsStoredId(ulong id)
    {
        return _numbersDictionary.ContainsKey(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetStoredNumber(ulong id, out double number)
    {
        return _numbersDictionary.TryGetValue(id, out number);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Tuple<ulong, double>> GetIdScalarRecords()
    {
        return _numbersDictionary.Select(
            p => new Tuple<ulong, double>(p.Key, p.Value)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivectorSparseList AddTerm(Tuple<ulong, double> idScalarRecord)
    {
        var (id, scalar) = idScalarRecord;

        Debug.Assert(scalar.IsValid() && id < GaSpaceDimensions);

        if (scalar == 0d) return this;

        if (_numbersDictionary.TryGetValue(id, out var scalar1))
        {
            scalar1 += scalar;

            if (scalar1 == 0d)
                _numbersDictionary.Remove(id);
            else
                _numbersDictionary[id] = scalar1;
        }
        else
            _numbersDictionary.Add(id, scalar);

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivectorSparseList SubtractTerm(Tuple<ulong, double> idScalarRecord)
    {
        var (id, scalar) = idScalarRecord;

        Debug.Assert(scalar.IsValid() && id < GaSpaceDimensions);

        if (scalar == 0d) return this;

        if (_numbersDictionary.TryGetValue(id, out var scalar1))
        {
            scalar1 -= scalar;

            if (scalar1 == 0d)
                _numbersDictionary.Remove(id);
            else
                _numbersDictionary[id] = scalar1;
        }
        else
            _numbersDictionary.Add(id, -scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivectorSparseList AddTerms(IEnumerable<Tuple<ulong, double>> idScalarRecords)
    {
        foreach (var (id, scalar) in idScalarRecords)
        {
            Debug.Assert(scalar.IsValid() && id < GaSpaceDimensions);

            if (scalar == 0) return this;

            if (_numbersDictionary.TryGetValue(id, out var scalar1))
            {
                scalar1 += scalar;

                if (scalar1 == 0d)
                    _numbersDictionary.Remove(id);
                else
                    _numbersDictionary[id] = scalar1;
            }
            else
                _numbersDictionary.Add(id, scalar);
        }

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivectorSparseList SubtractTerms(IEnumerable<Tuple<ulong, double>> idScalarRecords)
    {
        foreach (var (id, scalar) in idScalarRecords)
        {
            Debug.Assert(scalar.IsValid() && id < GaSpaceDimensions);

            if (scalar == 0) return this;

            if (_numbersDictionary.TryGetValue(id, out var scalar1))
            {
                scalar1 -= scalar;

                if (scalar1 == 0d)
                    _numbersDictionary.Remove(id);
                else
                    _numbersDictionary[id] = scalar1;
            }
            else
                _numbersDictionary.Add(id, -scalar);
        }

        return this;
    }


    public GaMultivectorSparseList MapNumbers(Func<double, double> numberMapping)
    {
        var numbersDictionary = new Dictionary<ulong, double>();

        foreach (var (id, scalar) in _numbersDictionary)
        {
            var scalar1 = numberMapping(scalar);

            if (scalar1 != 0)
                numbersDictionary.Add(id, scalar1);
        }

        return new GaMultivectorSparseList(GaSpaceDimensions, numbersDictionary);
    }
        
    public GaMultivectorSparseList MapNumbers(Func<ulong, double, double> numberMapping)
    {
        var numbersDictionary = new Dictionary<ulong, double>();

        foreach (var (id, scalar) in _numbersDictionary)
        {
            var scalar1 = numberMapping(id, scalar);

            if (scalar1 != 0)
                numbersDictionary.Add(id, scalar1);
        }

        return new GaMultivectorSparseList(GaSpaceDimensions, numbersDictionary);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivectorSparseList FilterById(Predicate<ulong> filterPredicate)
    {
        var numbersDictionary =
            _numbersDictionary
                .Where(p => filterPredicate(p.Key))
                .ToDictionary(p => p.Key, p => p.Value);

        return new GaMultivectorSparseList(GaSpaceDimensions, numbersDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivectorSparseList FilterByNumber(Predicate<double> filterPredicate)
    {
        var numbersDictionary =
            _numbersDictionary
                .Where(p => filterPredicate(p.Value))
                .ToDictionary(p => p.Key, p => p.Value);

        return new GaMultivectorSparseList(GaSpaceDimensions, numbersDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivectorSparseList FilterByIdNumber(Func<ulong, double, bool> filterPredicate)
    {
        var numbersDictionary =
            _numbersDictionary
                .Where(p => filterPredicate(p.Key, p.Value))
                .ToDictionary(p => p.Key, p => p.Value);

        return new GaMultivectorSparseList(GaSpaceDimensions, numbersDictionary);
    }

    public ulong GetMaxNumberMagnitudeId()
    {
        if (_numbersDictionary.Count == 0)
            throw new InvalidOperationException();

        var (maxValueId, maxValue) = _numbersDictionary.First();
        maxValue = maxValue.Abs();

        foreach (var (id, number) in _numbersDictionary)
        {
            var absNumber = number.Abs();

            if (absNumber <= maxValue) continue;

            maxValue = absNumber;
            maxValueId = id;
        }

        return maxValueId;
    }
        
    public ulong GetMinNumberMagnitudeId()
    {
        if (_numbersDictionary.Count == 0)
            throw new InvalidOperationException();

        var (minValueId, minValue) = _numbersDictionary.First();
        minValue = minValue.Abs();

        if (minValue == 0d) return minValueId;

        foreach (var (id, number) in _numbersDictionary)
        {
            var absNumber = number.Abs();

            if (absNumber >= minValue) continue;

            minValue = absNumber;
            minValueId = id;

            if (minValue == 0d) return minValueId;
        }

        return minValueId;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public GaMultivectorBinaryTrie CreateBinaryTrie()
    {
        return new GaMultivectorBinaryTrie(
            (int) VSpaceDimensions,
            _numbersDictionary
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<double> GetEnumerator()
    {
        for (var i = 0UL; i < GaSpaceDimensions; i++)
            yield return this[i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}