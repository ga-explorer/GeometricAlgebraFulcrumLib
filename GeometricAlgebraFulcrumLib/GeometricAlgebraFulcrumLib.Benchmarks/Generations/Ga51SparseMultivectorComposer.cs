using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Benchmarks.Generations;

public sealed class Ga51SparseMultivectorComposer
{
    public static Ga51SparseMultivectorComposer Create()
    {
        return new Ga51SparseMultivectorComposer();
    }


    private readonly Dictionary<ulong, double> _idScalarDictionary;

    
    public RGaFloat64ConformalProcessor Processor { get; }
        = RGaFloat64ConformalProcessor.Instance;

    public int VSpaceDimensions 
        => 6;
    
    public int SparseCount 
        => _idScalarDictionary.Count;

    public double this[ulong id]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _idScalarDictionary.TryGetValue(id, out var scalar)
            ? scalar : 0d;

        set
        {
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
    public Ga51SparseMultivectorComposer()
    {
        _idScalarDictionary = new Dictionary<ulong, double>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51SparseMultivectorComposer(Dictionary<ulong, double> idScalarDictionary)
    {
        _idScalarDictionary = idScalarDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarByGradeIndex(int grade, ulong index)
    {
        var id = BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

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
    public IEnumerable<KeyValuePair<ulong, double>> GetIdScalarPairs()
    {
        return _idScalarDictionary;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51SparseMultivectorComposer AddTerm(ulong id, double scalar)
    {
        Debug.Assert(scalar.IsValid());

        if (scalar == 0d)
            return this;

        if (_idScalarDictionary.TryGetValue(id, out var scalar1))
        {
            var scalar2 = scalar1 + scalar;

            Debug.Assert(scalar2.IsValid());

            if (scalar2 == 0d)
                _idScalarDictionary.Remove(id);
            else
                _idScalarDictionary[id] = scalar2;
        }
        else
            _idScalarDictionary.Add(id, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51SparseMultivector ToMultivector()
    {
        var groupList =
            _idScalarDictionary
                .Where(p => p.Value != 0)
                .GroupBy(idScalarPair => idScalarPair.Key.Grade());

        var gradeKVectorDictionary = new Dictionary<int, Ga51SparseKVector>();

        foreach (var group in groupList)
        {
            var idScalarDictionary = group.ToDictionary(
                idScalarPair => idScalarPair.Key,
                idScalarPair => idScalarPair.Value
            );

            var grade = group.Key;
            var kVector = new Ga51SparseKVector(grade, idScalarDictionary);

            gradeKVectorDictionary.Add(grade, kVector);
        }

        return new Ga51SparseMultivector(gradeKVectorDictionary);
    }


}