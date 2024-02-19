using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib;
using DataStructuresLib.Dictionary;
using DataStructuresLib.Extensions;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

public sealed partial class XGaFloat64Bivector :
    XGaFloat64KVector
{
    private readonly IReadOnlyDictionary<IIndexSet, double> _idScalarDictionary;


    public override string MultivectorClassName
        => "Generic Bivector";

    public override int Count
        => _idScalarDictionary.Count;

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return 2;
        }
    }

    public override int Grade
        => 2;

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<IIndexSet> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<double> Scalars
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _idScalarDictionary.Values;
    }

    public override IEnumerable<KeyValuePair<IIndexSet, double>> IdScalarPairs
        => _idScalarDictionary;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Bivector(XGaFloat64Processor processor)
        : base(processor)
    {
        _idScalarDictionary = new EmptyDictionary<IIndexSet, double>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Bivector(XGaFloat64Processor processor, KeyValuePair<IIndexSet, double> basisScalarPair)
        : base(processor)
    {
        _idScalarDictionary =
            new SingleItemDictionary<IIndexSet, double>(basisScalarPair);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Bivector(XGaFloat64Processor processor, IReadOnlyDictionary<IIndexSet, double> scalarDictionary)
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
    public override IReadOnlyDictionary<IIndexSet, double> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(IIndexSet key)
    {
        return !IsZero && _idScalarDictionary.ContainsKey(key);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar GetScalarPart()
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Vector GetVectorPart()
    {
        return Processor.CreateZeroVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector GetBivectorPart()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector GetHigherKVectorPart(int grade)
    {
        return Processor.CreateZeroHigherKVector(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GetBivectorPart(Func<int, int, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = 
            _idScalarDictionary.Where(term => 
                filterFunc(term.Key.FirstIndex, term.Key.LastIndex)
            ).ToDictionary();

        return Processor.CreateBivector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GetPart(Func<IIndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.CreateBivector(idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GetPart(Func<double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.CreateBivector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GetPart(Func<IIndexSet, double, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.CreateBivector(idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector RemoveSmallTerms(double epsilon = 1e-12)
    {
        if (Count <= 1) return this;

        var scalarThreshold = 
            epsilon.Abs() * Scalars.Max(s => s.Abs());

        return GetPart((double s) => 
            s <= -scalarThreshold || s >= scalarThreshold
        );
    }


    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Metric.CreateBasisBlade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double Scalar()
    {
        return 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetBasisBladeScalar(IIndexSet basisBlade)
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
    public override bool TryGetBasisBladeScalarValue(IIndexSet basisBlade, out double scalar)
    {
        if (_idScalarDictionary.TryGetValue(basisBlade, out scalar))
            return true;

        scalar = 0d;
        return false;
    }


    public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, double>(Metric.CreateBasisBlade(p.Key), p.Value)
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Simplify()
    {
        return IsZero
            ? Processor.CreateZeroScalar()
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