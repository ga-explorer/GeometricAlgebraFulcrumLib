using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;


namespace GeometricAlgebraFulcrumLib.Benchmarks.Generations;

public sealed class Ga51SparseMultivector
{
    private readonly Dictionary<int, Ga51SparseKVector> _gradeKVectorDictionary;


    public XGaFloat64ConformalProcessor Processor { get; }
        = XGaFloat64ConformalProcessor.Instance;

    public int VSpaceDimensions 
        => 6;
    
    public int SparseCount 
        => _gradeKVectorDictionary.Count;

    public double this[ulong id]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            var grade = id.Grade();

            return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
                ? kVector[id]
                : 0d;
        }
        set
        {
            var grade = id.Grade();

            if (_gradeKVectorDictionary.TryGetValue(grade, out var kVector))
            {
                kVector[id] = value;

                return;
            }

            if (value != 0)
            {
                var idScalarDictionary = new Dictionary<ulong, double>()
                {
                    {id, value}
                };

                _gradeKVectorDictionary.Add(
                    grade, 
                    new Ga51SparseKVector(grade, idScalarDictionary)
                );
            }
        }
    }
    
    public double this[int grade, ulong index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
                ? kVector[(ulong)BasisBladeUtils.BasisBladeGradeIndexToId(grade, index)]
                : 0d;
        set
        {
            if (_gradeKVectorDictionary.TryGetValue(grade, out var kVector))
            {
                kVector[(ulong)BasisBladeUtils.BasisBladeGradeIndexToId(grade, index)] = value;

                return;
            }

            if (value != 0)
            {
                var idScalarDictionary = new Dictionary<ulong, double>()
                {
                    {(ulong)BasisBladeUtils.BasisBladeGradeIndexToId(grade, index), value}
                };

                _gradeKVectorDictionary.Add(
                    grade, 
                    new Ga51SparseKVector(grade, idScalarDictionary)
                );
            }
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51SparseMultivector()
    {
        _gradeKVectorDictionary = new Dictionary<int, Ga51SparseKVector>();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51SparseMultivector(Dictionary<int, Ga51SparseKVector> gradeKVectorDictionary)
    {
        _gradeKVectorDictionary = gradeKVectorDictionary;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Ga51SparseKVector GetKVector(int grade)
    {
        return _gradeKVectorDictionary[grade];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarByGradeIndex(int grade, ulong index)
    {
        var id = (ulong)BasisBladeUtils.BasisBladeGradeIndexToId(grade, index);

        return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
            ? kVector.GetScalarById(id) : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetScalarById(ulong id)
    {
        var grade = id.Grade();

        return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
            ? kVector.GetScalarById(id) : 0d;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<KeyValuePair<ulong, double>> GetIdScalarPairs()
    {
        return _gradeKVectorDictionary.Values.SelectMany(
            kVector => kVector.GetIdScalarPairs()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Ga51SparseKVector> GetKVectors()
    {
        return _gradeKVectorDictionary.Values;
    }


    public Ga51SparseMultivector Gp(Ga51SparseMultivector mv2)
    {
        var composer = Ga51SparseMultivectorComposer.Create();

        foreach (var (id1, scalar1) in GetIdScalarPairs())
        foreach (var (id2, scalar2) in mv2.GetIdScalarPairs())
        {
            var basisBlade = Processor.Gp((IndexSet)id1, (IndexSet)id2);

            if (basisBlade.IsZero) continue;
            
            var scalar = basisBlade.IsPositive
                ? scalar1 * scalar2
                : -(scalar1 * scalar2);

            composer.AddTerm((ulong)basisBlade.Id, scalar);
        }

        return composer.ToMultivector();
    }
    
    public Ga51SparseMultivector Op(Ga51SparseMultivector mv2)
    {
        var mv = new Ga51SparseMultivector();

        foreach (var kVector1 in _gradeKVectorDictionary.Values)
        {
            foreach (var kVector2 in mv2._gradeKVectorDictionary.Values)
            {
                if (kVector1.Grade + kVector2.Grade > VSpaceDimensions)
                    continue;

                foreach (var (id1, scalar1) in kVector1.GetIdScalarPairs())
                foreach (var (id2, scalar2) in kVector2.GetIdScalarPairs())
                {
                    var basisBlade = Processor.Op((IndexSet)id1, (IndexSet)id2);

                    if (basisBlade.IsZero) continue;

                    mv[(ulong)basisBlade.Id] += basisBlade.Sign * scalar1 * scalar2;
                }
            }
        }

        return mv;
    }
}