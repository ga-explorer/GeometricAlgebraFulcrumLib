using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

public sealed partial class RGaFloat64GradedMultivector :
    RGaFloat64Multivector
{
    private readonly IReadOnlyDictionary<int, RGaFloat64KVector> _gradeKVectorDictionary;


    public override string MultivectorClassName
        => "Generic Graded Multivector";

    public override int Count
        => _gradeKVectorDictionary.Count == 0
            ? 0
            : _gradeKVectorDictionary.Values.Sum(kv => kv.Count);

    public override IEnumerable<int> KVectorGrades
        => _gradeKVectorDictionary.Keys;

    public int KVectorCount
        => _gradeKVectorDictionary.Count;

    public override bool IsZero
        => _gradeKVectorDictionary.Count == 0;

    public IEnumerable<RGaFloat64KVector> KVectors
        => _gradeKVectorDictionary.Values;

    public override IEnumerable<KeyValuePair<ulong, double>> IdScalarPairs
        => _gradeKVectorDictionary.Values.SelectMany(
            kv => kv.IdScalarPairs
        );

    public override IEnumerable<KeyValuePair<RGaBasisBlade, double>> BasisScalarPairs
        => _gradeKVectorDictionary.Values.SelectMany(
            kv => kv.BasisScalarPairs
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64GradedMultivector(RGaFloat64Processor metric)
        : base(metric)
    {
        _gradeKVectorDictionary = new EmptyDictionary<int, RGaFloat64KVector>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64GradedMultivector(RGaFloat64Processor metric, KeyValuePair<int, RGaFloat64KVector> gradeKVectorPair)
        : base(metric)
    {
        _gradeKVectorDictionary =
            new SingleItemDictionary<int, RGaFloat64KVector>(gradeKVectorPair);

        Debug.Assert(
            Processor.IsValidMultivectorDictionary(_gradeKVectorDictionary)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64GradedMultivector(RGaFloat64Processor metric, IReadOnlyDictionary<int, RGaFloat64KVector> kVectorDictionary)
        : base(metric)
    {
        _gradeKVectorDictionary = kVectorDictionary;

        Debug.Assert(
            Processor.IsValidMultivectorDictionary(_gradeKVectorDictionary)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsScalar()
    {
        return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsVector()
    {
        return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsBivector()
    {
        return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsTrivector()
    {
        return IsZero || _gradeKVectorDictionary.Keys.All(g => g == 3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsKVector(int grade)
    {
        return IsZero || _gradeKVectorDictionary.Keys.All(g => g == grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsOdd()
    {
        return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsOdd());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsOdd(int maxGrade)
    {
        return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsOdd(maxGrade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsEven()
    {
        return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsEven());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsEven(int maxGrade)
    {
        return !IsZero && _gradeKVectorDictionary.Keys.All(k => k.IsEven(maxGrade));
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetMinGrade()
    {
        return IsZero ? 0 : _gradeKVectorDictionary.Keys.Min();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetMaxGrade()
    {
        return IsZero ? 0 : _gradeKVectorDictionary.Keys.Max();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(ulong id)
    {
        if (IsZero) return false;

        var grade = id.Grade();

        return _gradeKVectorDictionary.TryGetValue(grade, out var kVector) &&
               kVector.ContainsKey(id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsScalarPart()
    {
        return _gradeKVectorDictionary.ContainsKey(0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsVectorPart()
    {
        return _gradeKVectorDictionary.ContainsKey(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsBivectorPart()
    {
        return _gradeKVectorDictionary.ContainsKey(2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKVectorPart(int grade)
    {
        return _gradeKVectorDictionary.ContainsKey(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsOddPart()
    {
        return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsOdd());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsOddPart(int maxGrade)
    {
        return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsOdd(maxGrade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsEvenPart()
    {
        return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsEven());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsEvenPart(int maxGrade)
    {
        return !IsZero && _gradeKVectorDictionary.Keys.Any(k => k.IsEven(maxGrade));
    }

    public override int GetKVectorCount()
    {
        return _gradeKVectorDictionary.Count;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Simplify()
    {
        return KVectorCount switch
        {
            0 => Processor.CreateZeroScalar(),
            1 => _gradeKVectorDictionary.Values.First().Simplify(),
            _ => this
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return Processor.IsValidMultivectorDictionary(_gradeKVectorDictionary);
    }

    public RGaFloat64Multivector MapKVectors(Func<RGaFloat64KVector, RGaFloat64KVector> kVectorMapping, bool simplify = true)
    {
        var kVectorDictionary = new Dictionary<int, RGaFloat64KVector>();

        foreach (var (grade, kVector) in _gradeKVectorDictionary)
        {
            var kVector1 = kVectorMapping(kVector);

            if (!kVector1.IsZero)
                kVectorDictionary.Add(grade, kVector1);
        }

        if (kVectorDictionary.Count == 0)
            return simplify
                ? Processor.CreateZeroScalar()
                : new RGaFloat64GradedMultivector(
                    Processor,
                        
                    new EmptyDictionary<int, RGaFloat64KVector>()
                );

        if (kVectorDictionary.Count == 1)
            return new RGaFloat64GradedMultivector(
                Processor,
                    
                new SingleItemDictionary<int, RGaFloat64KVector>(kVectorDictionary.First())
            );

        var mv = new RGaFloat64GradedMultivector(
            Processor, 
                
            kVectorDictionary
        );

        return simplify ? mv.Simplify() : mv;
    }

    public RGaFloat64Multivector MapKVectors(Func<int, RGaFloat64KVector, RGaFloat64KVector> kVectorMapping, bool simplify = true)
    {
        var kVectorDictionary = new Dictionary<int, RGaFloat64KVector>();

        foreach (var (grade, kVector) in _gradeKVectorDictionary)
        {
            var kVector1 = kVectorMapping(grade, kVector);

            if (!kVector1.IsZero)
                kVectorDictionary.Add(grade, kVector1);
        }

        if (kVectorDictionary.Count == 0)
            return simplify
                ? Processor.CreateZeroScalar()
                : new RGaFloat64GradedMultivector(
                    Processor,
                        
                    new EmptyDictionary<int, RGaFloat64KVector>()
                );

        if (kVectorDictionary.Count == 1)
            return new RGaFloat64GradedMultivector(
                Processor,
                    
                new SingleItemDictionary<int, RGaFloat64KVector>(kVectorDictionary.First())
            );

        var mv = new RGaFloat64GradedMultivector(
            Processor, 
                
            kVectorDictionary
        );

        return simplify ? mv.Simplify() : mv;
    }


    public override IEnumerable<RGaFloat64KVector> GetKVectorParts()
    {
        return _gradeKVectorDictionary.Values;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double Scalar()
    {
        if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
            return ((RGaFloat64Scalar)kVector).ScalarValue();

        return 0d;
    }

    public override double GetBasisBladeScalar(ulong basisBladeId)
    {
        var grade = basisBladeId.Grade();

        if (grade == 0)
            return Scalar();

        return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
            ? kVector.GetBasisBladeScalar(basisBladeId)
            : 0d;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out double scalar)
    {
        if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
            return kVector.TryGetScalarValue(out scalar);

        scalar = 0d;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetBasisBladeScalarValue(ulong basisBlade, out double scalar)
    {
        if (_gradeKVectorDictionary.TryGetValue(basisBlade.Grade(), out var kVector))
            return kVector.TryGetBasisBladeScalarValue(basisBlade, out scalar);

        scalar = 0d;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetKVector(int grade, out RGaFloat64KVector kVector)
    {
        return _gradeKVectorDictionary.TryGetValue(grade, out kVector);
    }


    public override IEnumerable<RGaBasisBlade> BasisBlades
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return _gradeKVectorDictionary
                .Values
                .SelectMany(kv => kv.BasisBlades);
        }
    }

    public override IEnumerable<ulong> Ids
        => _gradeKVectorDictionary.Values.SelectMany(kv => kv.Ids);

    public override IEnumerable<double> Scalars
        => _gradeKVectorDictionary
            .Values
            .SelectMany(kv => kv.Scalars);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar GetScalarPart()
    {
        if (_gradeKVectorDictionary.TryGetValue(0, out var kVector))
            return (RGaFloat64Scalar)kVector;

        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart()
    {
        if (_gradeKVectorDictionary.TryGetValue(1, out var kVector))
            return (RGaFloat64Vector)kVector;

        return Processor.CreateZeroVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc)
    {
        if (_gradeKVectorDictionary.TryGetValue(1, out var kVector))
            return ((RGaFloat64Vector)kVector).GetVectorPart(filterFunc);

        return Processor.CreateZeroVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<double, bool> filterFunc)
    {
        if (_gradeKVectorDictionary.TryGetValue(1, out var kVector))
            return ((RGaFloat64Vector)kVector).GetVectorPart(filterFunc);

        return Processor.CreateZeroVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<int, double, bool> filterFunc)
    {
        if (_gradeKVectorDictionary.TryGetValue(1, out var kVector))
            return ((RGaFloat64Vector)kVector).GetVectorPart(filterFunc);

        return Processor.CreateZeroVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector GetBivectorPart()
    {
        if (_gradeKVectorDictionary.TryGetValue(2, out var kVector))
            return (RGaFloat64Bivector)kVector;

        return Processor.CreateZeroBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
            ? (RGaFloat64HigherKVector)kVector
            : Processor.CreateZeroHigherKVector(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector GetKVectorPart(int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return _gradeKVectorDictionary.TryGetValue(grade, out var kVector)
            ? kVector
            : Processor.CreateZeroKVector(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector GetFirstKVectorPart()
    {
        return _gradeKVectorDictionary.Count == 0
            ? Processor.CreateZeroScalar()
            : _gradeKVectorDictionary.Values.First();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64GradedMultivector GetPart(Func<ulong, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarPairs = 
            IdScalarPairs.Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor
            .CreateComposer()
            .SetTerms(idScalarPairs)
            .GetMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64GradedMultivector GetPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarPairs = 
            IdScalarPairs.Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor
            .CreateComposer()
            .SetTerms(idScalarPairs)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64GradedMultivector GetPart(Func<ulong, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarPairs = 
            IdScalarPairs.Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor
            .CreateComposer()
            .SetTerms(idScalarPairs)
            .GetMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64GradedMultivector RemoveSmallTerms(double epsilon = 1e-12)
    {
        if (Count <= 1) return this;

        var scalarThreshold = 
            epsilon.Abs() * Scalars.Max(s => s.Abs());

        return GetPart((double s) => 
            s <= -scalarThreshold || s >= scalarThreshold
        );
    }

    public override RGaFloat64Multivector GetEvenPart()
    {
        if (IsZero)
            return Processor.CreateZeroScalar();

        var composer = Processor.CreateComposer();

        if (_gradeKVectorDictionary.TryGetValue(0, out var scalarPart))
            composer.SetScalar((RGaFloat64Scalar)scalarPart);

        if (_gradeKVectorDictionary.TryGetValue(2, out var bivectorPart))
            composer.SetMultivector(bivectorPart);

        var kVectors =
            _gradeKVectorDictionary.Values.Where(
                kv => kv.Grade > 3 && kv.Grade.IsEven()
            );

        foreach (var kVector in kVectors)
            composer.SetMultivector(kVector);

        return composer.GetSimpleMultivector();
    }

    public override RGaFloat64Multivector GetEvenPart(int maxGrade)
    {
        if (maxGrade < 0 || IsZero)
            return Processor.CreateZeroScalar();

        var composer = Processor.CreateComposer();

        if (_gradeKVectorDictionary.TryGetValue(0, out var scalarPart))
            composer.SetScalar((RGaFloat64Scalar)scalarPart);

        if (maxGrade < 2)
            return composer.GetSimpleMultivector();

        if (_gradeKVectorDictionary.TryGetValue(2, out var bivectorPart))
            composer.SetMultivector(bivectorPart);

        var kVectors =
            _gradeKVectorDictionary.Values.Where(
                kv => kv.Grade > 3 && kv.Grade.IsEven(maxGrade)
            );

        foreach (var kVector in kVectors)
            composer.SetMultivector(kVector);

        return composer.GetSimpleMultivector();
    }

    public override RGaFloat64Multivector GetOddPart()
    {
        if (IsZero)
            return Processor.CreateZeroScalar();

        var composer = Processor.CreateComposer();

        if (_gradeKVectorDictionary.TryGetValue(1, out var vectorPart))
            composer.SetMultivector(vectorPart);

        var kVectors =
            _gradeKVectorDictionary.Values.Where(
                kv => kv.Grade > 2 && kv.Grade.IsOdd()
            );

        foreach (var kVector in kVectors)
            composer.SetMultivector(kVector);

        return composer.GetSimpleMultivector();
    }

    public override RGaFloat64Multivector GetOddPart(int maxGrade)
    {
        if (maxGrade < 1 || IsZero)
            return Processor.CreateZeroScalar();

        var composer = Processor.CreateComposer();

        if (_gradeKVectorDictionary.TryGetValue(1, out var vectorPart))
            composer.SetMultivector(vectorPart);

        var kVectors =
            _gradeKVectorDictionary.Values.Where(
                kv => kv.Grade > 2 && kv.Grade.IsOdd(maxGrade)
            );

        foreach (var kVector in kVectors)
            composer.SetMultivector(kVector);

        return composer.GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return KVectors
            .OrderBy(p => p.Grade)
            .SelectMany(kVector =>
                kVector
                    .BasisScalarPairs
                    .OrderBy(p => p.Key.Grade)
                    .ThenBy(p => p.Key.Id)
                    .Select(p => $"({p.Value:G}){p.Key}")
            ).ConcatenateText(" + ");
    }
}