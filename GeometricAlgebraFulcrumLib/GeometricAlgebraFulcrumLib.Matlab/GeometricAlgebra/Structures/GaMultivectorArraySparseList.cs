using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Structures;

public sealed class GaMultivectorArraySparseList : 
    IReadOnlyCollection<double>
{
    private readonly Dictionary<Pair<ulong>, double> _numbersDictionary
        = new Dictionary<Pair<ulong>, double>();


    public ulong GaSpaceDimension1 { get; }

    public ulong GaSpaceDimension2 { get; }

    public int Count 
        => _numbersDictionary.Count;

    public double this[ulong index1, ulong index2]
    {
        get
        {
            if (index1 >= GaSpaceDimension1)
                throw new IndexOutOfRangeException();

            if (index2 >= GaSpaceDimension2)
                throw new IndexOutOfRangeException();

            var index = new Pair<ulong>(index1, index2);

            return _numbersDictionary.TryGetValue(index, out var number) 
                ? number : 0d;
        }
        set
        {
            if (index1 >= GaSpaceDimension1)
                throw new IndexOutOfRangeException();

            if (index2 >= GaSpaceDimension2)
                throw new IndexOutOfRangeException();

            Debug.Assert(!double.IsNaN(value));

            var index = new Pair<ulong>(index1, index2);

            if (value == 0)
                _numbersDictionary.Remove(index);

            else if (_numbersDictionary.ContainsKey(index))
                _numbersDictionary[index] = value;

            else
                _numbersDictionary.Add(index, value);
        }
    }
        
    public IEnumerable<Pair<ulong>> StoredIDs
        => _numbersDictionary.Keys;

    public IEnumerable<double> StoredNumbers 
        => _numbersDictionary.Values;

    public IEnumerable<KeyValuePair<Pair<ulong>, double>> StoredIdScalarPairs
        => _numbersDictionary;


    
    public GaMultivectorArraySparseList(ulong gaSpaceDimension1, ulong gaSpaceDimension2)
    {
        GaSpaceDimension1 = gaSpaceDimension1;
        GaSpaceDimension2 = gaSpaceDimension2;
    }

    
    private GaMultivectorArraySparseList(ulong gaSpaceDimension1, ulong gaSpaceDimension2, Dictionary<Pair<ulong>, double> numbersDictionary)
    {
        GaSpaceDimension1 = gaSpaceDimension1;
        GaSpaceDimension2 = gaSpaceDimension2;
        _numbersDictionary = numbersDictionary;
    }


    
    public void Clear()
    {
        _numbersDictionary.Clear();
    }
        
    
    public bool ContainsStoredId(ulong id1, ulong id2)
    {
        var id = new Pair<ulong>(id1, id2);

        return _numbersDictionary.ContainsKey(id);
    }

    
    public bool TryGetStoredNumber(ulong id1, ulong id2, out double number)
    {
        var id = new Pair<ulong>(id1, id2);

        return _numbersDictionary.TryGetValue(id, out number);
    }


    public GaMultivectorArraySparseList MapNumbers(Func<double, double> numberMapping)
    {
        var numbersDictionary = new Dictionary<Pair<ulong>, double>();

        foreach (var (id, scalar) in _numbersDictionary.ToTuples())
        {
            var scalar1 = numberMapping(scalar);

            if (scalar1 != 0)
                numbersDictionary.Add(id, scalar1);
        }

        return new GaMultivectorArraySparseList(GaSpaceDimension1, GaSpaceDimension2, numbersDictionary);
    }
        
    public GaMultivectorArraySparseList MapNumbers(Func<ulong, ulong, double, double> numberMapping)
    {
        var numbersDictionary = new Dictionary<Pair<ulong>, double>();

        foreach (var (id, scalar) in _numbersDictionary.ToTuples())
        {
            var scalar1 = numberMapping(id.Item1, id.Item2, scalar);

            if (scalar1 != 0)
                numbersDictionary.Add(id, scalar1);
        }

        return new GaMultivectorArraySparseList(GaSpaceDimension1, GaSpaceDimension2, numbersDictionary);
    }


    
    public GaMultivectorArraySparseList FilterById(Func<ulong, ulong, bool> filterPredicate)
    {
        var numbersDictionary =
            _numbersDictionary
                .Where(p => filterPredicate(p.Key.Item1, p.Key.Item2))
                .ToDictionary(p => p.Key, p => p.Value);

        return new GaMultivectorArraySparseList(GaSpaceDimension1, GaSpaceDimension2, numbersDictionary);
    }
        
    
    public GaMultivectorArraySparseList FilterByNumber(Predicate<double> filterPredicate)
    {
        var numbersDictionary =
            _numbersDictionary
                .Where(p => filterPredicate(p.Value))
                .ToDictionary(p => p.Key, p => p.Value);

        return new GaMultivectorArraySparseList(GaSpaceDimension1, GaSpaceDimension2, numbersDictionary);
    }
        
    
    public GaMultivectorArraySparseList FilterByIdNumber(Func<ulong, ulong, double, bool> filterPredicate)
    {
        var numbersDictionary =
            _numbersDictionary
                .Where(p => filterPredicate(p.Key.Item1, p.Key.Item2, p.Value))
                .ToDictionary(p => p.Key, p => p.Value);

        return new GaMultivectorArraySparseList(GaSpaceDimension1, GaSpaceDimension2, numbersDictionary);
    }


    
    public IEnumerator<double> GetEnumerator()
    {
        for (var j = 0UL; j < GaSpaceDimension2; j++)
        for (var i = 0UL; i < GaSpaceDimension1; i++)
            yield return this[i, j];
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}