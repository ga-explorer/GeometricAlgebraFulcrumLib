using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

public sealed partial class XGaFloat64Scalar :
    XGaFloat64KVector,
    IFloat64Scalar
{
    private readonly double _scalar;
    
    public double ScalarValue 
        => _scalar;

    public override string MultivectorClassName
        => "Generic Scalar";

    public override int Count
        => IsZero ? 0 : 1;

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return 0;
        }
    }

    public override int Grade
        => 0;

    public override bool IsZero { get; }

    public bool IsOne
        => ScalarValue.IsOne();
    
    public bool IsMinusOne
        => ScalarValue.IsMinusOne();

    public override IEnumerable<XGaBasisBlade> BasisBlades
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (!IsZero) yield return Processor.BasisScalar;
        }
    }

    public override IEnumerable<IndexSet> Ids
    {
        get
        {
            if (!IsZero) yield return IndexSet.EmptySet;
        }
    }

    public override IEnumerable<double> Scalars
    {
        get
        {
            if (!IsZero) yield return ScalarValue;
        }
    }

    public override IEnumerable<KeyValuePair<XGaBasisBlade, double>> BasisScalarPairs
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (!IsZero)
                yield return new KeyValuePair<XGaBasisBlade, double>(
                    Metric.BasisScalar,
                    ScalarValue
                );
        }
    }

    public override IEnumerable<KeyValuePair<IndexSet, double>> IdScalarPairs
    {
        get
        {
            if (!IsZero)
                yield return new KeyValuePair<IndexSet, double>(
                    IndexSet.EmptySet,
                    ScalarValue
                );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Scalar(XGaFloat64Processor metric, double scalar)
        : base(metric)
    {
        Debug.Assert(
            scalar.IsValid()
        );

        _scalar = scalar;
        IsZero = scalar.IsZero();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Scalar(XGaFloat64Processor metric, IFloat64Scalar scalar)
        : base(metric)
    {
        Debug.Assert(
            scalar.IsValid()
        );

        _scalar = scalar.ToScalar();
        IsZero = scalar.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Scalar(XGaFloat64Processor metric)
        : base(metric)
    {
        _scalar = 0d;
        IsZero = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaFloat64Scalar(XGaFloat64Processor metric, IReadOnlyDictionary<IndexSet, double> idScalarDictionary)
        : base(metric)
    {
        _scalar = idScalarDictionary.TryGetValue(IndexSet.EmptySet, out var scalar)
            ? scalar : 0d;

        Debug.Assert(
            _scalar.IsValid()
        );

        IsZero = _scalar.IsZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return ScalarValue.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar ToScalar()
    {
        return Float64Scalar.Create(_scalar);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<IndexSet, double> GetIdScalarDictionary()
    {
        return IsZero
            ? new EmptyDictionary<IndexSet, double>()
            : new SingleItemDictionary<IndexSet, double>(IndexSet.EmptySet, _scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(IndexSet key)
    {
        return key.IsEmptySet && !IsZero;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar GetScalarPart()
    {
        return this;
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
        return Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar GetPart(Func<IndexSet, bool> filterFunc)
    {
        return IsZero || filterFunc(IndexSet.EmptySet) 
            ? this 
            : Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar GetPart(Func<double, bool> filterFunc)
    {
        return IsZero || filterFunc(ScalarValue) 
            ? this 
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar GetPart(Func<IndexSet, double, bool> filterFunc)
    {
        return IsZero || filterFunc(IndexSet.EmptySet, ScalarValue) 
            ? this 
            : Processor.ScalarZero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double Scalar()
    {
        return _scalar;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetBasisBladeScalar(IndexSet basisBladeId)
    {
        return basisBladeId.IsEmptySet
            ? _scalar
            : 0d;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out double scalar)
    {
        if (!IsZero)
        {
            scalar = ScalarValue;
            return true;
        }

        scalar = 0d;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out double scalar)
    {
        if (basisBlade.IsEmptySet)
        {
            scalar = ScalarValue;
            return true;
        }

        scalar = 0d;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Simplify()
    {
        return this;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool Equals(XGaFloat64Scalar other)
    {
        return Equals(_scalar, other._scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is XGaFloat64Scalar other && Equals(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return _scalar.GetHashCode();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return IsZero ? string.Empty : $"'{ScalarValue:G}'<>";
    }
}