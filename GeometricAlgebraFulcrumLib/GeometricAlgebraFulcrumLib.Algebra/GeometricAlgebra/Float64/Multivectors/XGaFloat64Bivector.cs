using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64Bivector :
    XGaFloat64KVector
{
    private readonly IReadOnlyDictionary<IndexSet, double> _idScalarDictionary;


    public override string MultivectorClassName
        => "Generic Bivector";

    public override int Count
        => _idScalarDictionary.Count;

    public override int Grade
        => 2;

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return 2;
        }
    }

    public override IEnumerable<IndexSet> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<double> Scalars 
        => _idScalarDictionary.Values;

    public override IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs
        => _idScalarDictionary;

    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Metric.BasisBlade);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Bivector(XGaFloat64Processor processor)
        : base(processor)
    {
        _idScalarDictionary = new EmptyDictionary<IndexSet, double>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Bivector(XGaFloat64Processor processor, KeyValuePair<IndexSet, double> basisScalarPair)
        : base(processor)
    {
        _idScalarDictionary =
            new SingleItemDictionary<IndexSet, double>(basisScalarPair);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Bivector(XGaFloat64Processor processor, IReadOnlyDictionary<IndexSet, double> scalarDictionary)
        : base(processor)
    {
        _idScalarDictionary = scalarDictionary;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidBivectorDictionary();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaFloat64Vector> GetVectorBasis()
    {
        var closestVector = Processor.VectorTerm(0);

        return GetVectorBasis(closestVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaFloat64Vector> GetVectorBasis(int closestBasisVectorIndex)
    {
        var closestVector = Processor.VectorTerm(closestBasisVectorIndex);

        return GetVectorBasis(closestVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaFloat64Vector> GetVectorBasis(XGaFloat64Vector closestVector)
    {
        var e1 = closestVector.Lcp(this).DivideByNorm();
        var e2 = e1.Lcp(this);

        Debug.Assert((e1.Op(e2) - this).IsNearZero());

        return new Pair<XGaFloat64Vector>(e1, e2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<IndexSet, double> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }

    public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, double>(Metric.BasisBlade(p.Key), p.Value)
            );
        }
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
    public override XGaFloat64Vector GetVectorPart(Func<int, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector GetVectorPart(Func<double, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector GetVectorPart(Func<int, double, bool> filterFunc)
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector GetBivectorPart()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        return Processor.HigherKVectorZero(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GetBivectorPart(Func<int, int, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary =
            _idScalarDictionary.Where(term =>
                filterFunc(term.Key.FirstIndex, term.Key.LastIndex)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector GetPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector GetPart(Func<IndexSet, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double Scalar()
    {
        return 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetBasisBladeScalar(IndexSet basisBlade)
    {
        return _idScalarDictionary.GetValueOrDefault(basisBlade, 0d);
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
        if (_idScalarDictionary.TryGetValue(basisBlade, out scalar))
            return true;

        scalar = 0d;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        if (IsZero) return "0";

        return BasisScalarPairs
            .OrderBy(p => p.Key.Id)
            .Select(p => $"{p.Value:G} {p.Key}")
            .ConcatenateText(" + ");
    }
}