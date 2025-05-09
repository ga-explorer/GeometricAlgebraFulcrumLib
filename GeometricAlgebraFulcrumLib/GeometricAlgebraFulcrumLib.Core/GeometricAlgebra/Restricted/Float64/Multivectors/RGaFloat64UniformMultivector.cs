using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Core.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Core.Utilities.Text;

//using Open.Collections;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

/// <summary>
/// This is not intended to be an efficient implementation, but a correct
/// reference implementation for validation and performance comparison.
/// </summary>
public sealed partial class RGaFloat64UniformMultivector :
    RGaFloat64Multivector
{
    private readonly IReadOnlyDictionary<ulong, double> _idScalarDictionary;


    public override string MultivectorClassName
        => "Generic Uniform Multivector";

    public override int Count
        => _idScalarDictionary.Count;

    public override IEnumerable<int> KVectorGrades
        => _idScalarDictionary
            .Keys
            .Select(k => k.Grade())
            .Distinct();

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<RGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

    public override IEnumerable<ulong> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<double> Scalars
        => _idScalarDictionary.Values;

    public override IEnumerable<KeyValuePair<ulong, double>> IdScalarPairs
        => _idScalarDictionary;

    public override IEnumerable<KeyValuePair<RGaBasisBlade, double>> BasisScalarPairs
        => _idScalarDictionary.Select(p =>
            new KeyValuePair<RGaBasisBlade, double>(
                Processor.CreateBasisBlade(p.Key),
                p.Value
            )
        );



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64UniformMultivector(RGaFloat64Processor metric)
        : base(metric)
    {
        _idScalarDictionary =
            new EmptyDictionary<ulong, double>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64UniformMultivector(RGaFloat64Processor metric, KeyValuePair<ulong, double> basisScalarPair)
        : base(metric)
    {
        _idScalarDictionary =
            new SingleItemDictionary<ulong, double>(basisScalarPair);

        Debug.Assert(
            _idScalarDictionary.IsValidMultivectorDictionary()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaFloat64UniformMultivector(RGaFloat64Processor metric, IReadOnlyDictionary<ulong, double> idScalarDictionary)
        : base(metric)
    {
        _idScalarDictionary =
            idScalarDictionary;

        Debug.Assert(
            _idScalarDictionary.IsValidMultivectorDictionary()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidMultivectorDictionary();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsScalar()
    {
        return IsZero || _idScalarDictionary.Keys.All(id => id == 0UL);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsVector()
    {
        return IsZero || _idScalarDictionary.Keys.All(id => id.Grade() == 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsBivector()
    {
        return IsZero || _idScalarDictionary.Keys.All(id => id.Grade() == 2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsTrivector()
    {
        return IsZero || _idScalarDictionary.Keys.All(id => id.Grade() == 3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsKVector(int grade)
    {
        return IsZero || _idScalarDictionary.Keys.All(id => id.Grade() == grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsOdd()
    {
        return !IsZero && _idScalarDictionary.Keys.All(
            id => id.Grade().IsOdd()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsOdd(int maxGrade)
    {
        return !IsZero && _idScalarDictionary.Keys.All(
            id => id.Grade().IsOdd(maxGrade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsEven()
    {
        return !IsZero && _idScalarDictionary.Keys.All(
            id => id.Grade().IsEven()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsEven(int maxGrade)
    {
        return !IsZero && _idScalarDictionary.Keys.All(
            id => id.Grade().IsEven(maxGrade)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsScalarPart()
    {
        return _idScalarDictionary.Keys.Any(b => b == 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsVectorPart()
    {
        return _idScalarDictionary.Keys.Any(b => b.Grade() == 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsBivectorPart()
    {
        return _idScalarDictionary.Keys.Any(b => b.Grade() == 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKVectorPart(int grade)
    {
        return _idScalarDictionary.Keys.Any(b => b.Grade() == grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsOddPart()
    {
        return !IsZero && _idScalarDictionary.Keys.Any(
            id => id.Grade().IsOdd()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsOddPart(int maxGrade)
    {
        return !IsZero && _idScalarDictionary.Keys.Any(
            id => id.Grade().IsOdd(maxGrade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsEvenPart()
    {
        return !IsZero && _idScalarDictionary.Keys.Any(
            id => id.Grade().IsEven()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsEvenPart(int maxGrade)
    {
        return !IsZero && _idScalarDictionary.Keys.Any(
            id => id.Grade().IsEven(maxGrade)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyDictionary<ulong, double> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetMinGrade()
    {
        return IsZero ? 0 : _idScalarDictionary.Keys.Min(id => id.Grade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetMaxGrade()
    {
        return IsZero ? 0 : _idScalarDictionary.Keys.Max(id => id.Grade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(ulong key)
    {
        return !IsZero && _idScalarDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetKVectorCount()
    {
        return _idScalarDictionary
            .Keys
            .Select(k => k.Grade())
            .Distinct()
            .Count();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Simplify()
    {
        return this;
    }


    public override IEnumerable<RGaFloat64KVector> GetKVectorParts()
    {
        if (_idScalarDictionary.Count == 0)
            yield break;

        if (_idScalarDictionary.Count == 1)
        {
            yield return Processor.KVectorTerm(
                _idScalarDictionary.First()
            );

            yield break;
        }

        var gradeGroup =
            _idScalarDictionary.GroupBy(
                basisScalarPair => basisScalarPair.Key.Grade()
            );

        foreach (var gradeBasisScalarPairGroups in gradeGroup)
        {
            var grade = gradeBasisScalarPairGroups.Key;

            if (grade == 0)
            {
                yield return Processor.Scalar(
                    gradeBasisScalarPairGroups.First().Value
                );

                continue;
            }

            var idScalarDictionary = new Dictionary<ulong, double>();

            idScalarDictionary.AddRange(gradeBasisScalarPairGroups);

            yield return Processor.KVector(
                grade,
                idScalarDictionary
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double Scalar()
    {
        return _idScalarDictionary.GetValueOrDefault(0UL, 0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetBasisBladeScalar(ulong basisBladeId)
    {
        return _idScalarDictionary.GetValueOrDefault(basisBladeId, 0d);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out double scalar)
    {
        if (_idScalarDictionary.TryGetValue(0UL, out scalar))
            return true;

        scalar = 0d;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetBasisBladeScalarValue(ulong basisBlade, out double scalar)
    {
        if (_idScalarDictionary.TryGetValue(basisBlade, out scalar))
            return true;

        scalar = 0d;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar GetScalarPart()
    {
        return _idScalarDictionary.TryGetValue(0UL, out var scalarValue)
            ? Processor.Scalar(scalarValue)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart()
    {
        return Processor
            .CreateComposer()
            .SetTerms(_idScalarDictionary.Where(p => p.Key.Grade() == 1))
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc)
    {
        return Processor
            .CreateComposer()
            .SetTerms(_idScalarDictionary.Where(p => 
                    p.Key.Grade() == 1 &&
                    filterFunc(p.Key.FirstOneBitPosition())
                )
            )
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<double, bool> filterFunc)
    {
        return Processor
            .CreateComposer()
            .SetTerms(_idScalarDictionary.Where(p => 
                    p.Key.Grade() == 1 &&
                    filterFunc(p.Value)
                )
            )
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Vector GetVectorPart(Func<int, double, bool> filterFunc)
    {
        return Processor
            .CreateComposer()
            .SetTerms(_idScalarDictionary.Where(p => 
                    p.Key.Grade() == 1 &&
                    filterFunc(p.Key.FirstOneBitPosition(), p.Value)
                )
            )
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Bivector GetBivectorPart()
    {
        return Processor
            .CreateComposer()
            .SetTerms(_idScalarDictionary.Where(p => p.Key.Grade() == 2))
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return (RGaFloat64HigherKVector)Processor
            .CreateComposer()
            .SetTerms(_idScalarDictionary.Where(p => p.Key.Grade() == grade))
            .GetKVector(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64UniformMultivector GetPart(Func<ulong, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarPairs = 
            IdScalarPairs.Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor
            .CreateComposer()
            .SetTerms(idScalarPairs)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64UniformMultivector GetPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarPairs = 
            IdScalarPairs.Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor
            .CreateComposer()
            .SetTerms(idScalarPairs)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64UniformMultivector GetPart(Func<ulong, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarPairs = 
            IdScalarPairs.Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor
            .CreateComposer()
            .SetTerms(idScalarPairs)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (Count <= 1) return this;

        var scalarThreshold = 
            zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

        return GetPart((double s) => 
            s <= -scalarThreshold || s >= scalarThreshold
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector GetKVectorPart(int grade)
    {
        if (grade < 0)
            throw new ArgumentOutOfRangeException(nameof(grade));

        return grade switch
        {
            0 => GetScalarPart(),
            1 => GetVectorPart(),
            2 => GetBivectorPart(),
            _ => Processor
                .CreateComposer()
                .SetTerms(_idScalarDictionary.Where(p => p.Key.Grade() == grade))
                .GetKVector(grade)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector GetFirstKVectorPart()
    {
        return GetKVectorPart(GetMinGrade());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetEvenPart()
    {
        if (IsZero) return this;

        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade().IsEven())
                .ToDictionary(p => p.Key, p => p.Value);

        return Processor.UniformMultivector(
            idScalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetEvenPart(int maxGrade)
    {
        if (IsZero) return this;

        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade().IsEven(maxGrade))
                .ToDictionary(p => p.Key, p => p.Value);

        return Processor.UniformMultivector(
            idScalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetOddPart()
    {
        if (IsZero) return this;

        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade().IsOdd())
                .ToDictionary(p => p.Key, p => p.Value);

        return Processor.UniformMultivector(
            idScalarDictionary
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector GetOddPart(int maxGrade)
    {
        if (IsZero) return this;

        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade().IsOdd(maxGrade))
                .ToDictionary(p => p.Key, p => p.Value);

        return Processor.UniformMultivector(
            idScalarDictionary
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return BasisScalarPairs
            .OrderBy(p => p.Key)
            .Select(p => $"'{p.Value:G}'{p.Key}")
            .ConcatenateText(" + ");
    }
}