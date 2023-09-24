using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Generations;

public sealed class Ga51SparseKVector
{
    public static Ga51SparseKVector CreateZero(int grade)
    {
        return new Ga51SparseKVector(grade);
    }


    private readonly Dictionary<ulong, double> _idScalarDictionary;

    
    public RGaFloat64ConformalProcessor Processor { get; }
        = RGaFloat64ConformalProcessor.Instance;

    public int VSpaceDimensions 
        => 6;

    public int Grade { get; }

    public int SparseCount 
        => _idScalarDictionary.Count;

    public double this[ulong id]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _idScalarDictionary.TryGetValue(id, out var scalar)
                ? scalar : 0d;

        set
        {
            if (id.Grade() != Grade)
                throw new InvalidOperationException();

            if (value == 0d)
            {
                _idScalarDictionary.Remove(id);

                return;
            }

            if (_idScalarDictionary.ContainsKey(id))
                _idScalarDictionary[id] = value;
            else
                _idScalarDictionary.Add(id, value);
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51SparseKVector(int grade)
    {
        _idScalarDictionary = new Dictionary<ulong, double>();
        Grade = grade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51SparseKVector(int grade, Dictionary<ulong, double> idScalarDictionary)
    {
        _idScalarDictionary = idScalarDictionary;
        Grade = grade;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarByIndex(int index)
    {
        var id = BasisBladeUtils.BasisBladeGradeIndexToId(
            Grade, 
            (ulong) index
        );

        return _idScalarDictionary.TryGetValue(id, out var scalar)
            ? scalar : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarById(ulong id)
    {
        return _idScalarDictionary.TryGetValue(id, out var scalar)
            ? scalar : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<int, double>> GetIndexScalarPairs()
    {
        return _idScalarDictionary.Select(p =>
            new KeyValuePair<int, double>(
                (int) p.Key.BasisBladeIdToIndex(),
                p.Value
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, double>> GetIdScalarPairs()
    {
        return _idScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51SparseKVector Negative()
    {
        var idScalarDictionary = _idScalarDictionary.ToDictionary(
            p => p.Key,
            p => -p.Value
        );

        return new Ga51SparseKVector(Grade, idScalarDictionary);
    }


}