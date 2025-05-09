using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;

public sealed partial class XGaFloat64HigherKVector :
    XGaFloat64KVector
{
    private readonly IReadOnlyDictionary<IndexSet, double> _idScalarDictionary;


    public override string MultivectorClassName
        => $"Generic {Grade}-Vector";

    public override int Count
        => _idScalarDictionary.Count;

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return Grade;
        }
    }

    public override int Grade { get; }

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Metric.CreateBasisBlade);

    public override IEnumerable<IndexSet> Ids 
        => _idScalarDictionary.Keys;

    public override IEnumerable<double> Scalars
        => _idScalarDictionary.Values;

    public override IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs
        => _idScalarDictionary;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64HigherKVector(XGaFloat64Processor processor, int grade)
        : base(processor)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary = new EmptyDictionary<IndexSet, double>();

        Grade = grade;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64HigherKVector(XGaFloat64Processor processor, KeyValuePair<IndexSet, double> basisScalarPair)
        : base(processor)
    {
        var grade = basisScalarPair.Key.Count;

        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary =
            new SingleItemDictionary<IndexSet, double>(basisScalarPair);

        Grade = grade;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64HigherKVector(XGaFloat64Processor processor, int grade, IReadOnlyDictionary<IndexSet, double> indexScalarDictionary)
        : base(processor)
    {
        if (grade < 3)
            throw new ArgumentOutOfRangeException(nameof(grade));

        _idScalarDictionary = indexScalarDictionary;

        Grade = grade;

        Debug.Assert(IsValid());
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidKVectorDictionary(Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<IndexSet, double> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(IndexSet key)
    {
        return !IsZero && _idScalarDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar GetScalarPart()
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector GetVectorPart()
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector GetBivectorPart()
    {
        return Processor.BivectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        return grade == Grade
            ? this
            : Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector GetPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector GetPart(Func<IndexSet, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.HigherKVector(Grade, idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector RemoveSmallTerms(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        if (Count <= 1) return this;

        var scalarThreshold = 
            zeroEpsilon.Abs() * Scalars.Max(s => s.Abs());

        return GetPart((double s) => 
            s <= -scalarThreshold || s >= scalarThreshold
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double Scalar()
    {
        return 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetBasisBladeScalar(IndexSet basisBlade)
    {
        return _idScalarDictionary.TryGetValue(basisBlade, out var scalar)
            ? scalar
            : 0d;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out double scalar)
    {
        scalar = 0d;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out double scalar)
    {
        if (basisBlade.Count == Grade && _idScalarDictionary.TryGetValue(basisBlade, out scalar))
            return true;

        scalar = 0d;
        return false;
    }


    public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, double>(
                    Metric.CreateBasisBlade(p.Key),
                    p.Value
                )
            );
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Simplify()
    {
        return IsZero
            ? Processor.ScalarZero
            : this;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return IdScalarPairs
            .OrderBy(p => p.Key)
            .Select(p => $"'{p.Value:G}'{p.Key}")
            .ConcatenateText(" + ");
    }
}