using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.Multivectors;

public sealed partial class XGaScalar<T> :
    XGaKVector<T>,
    IScalar<T>
{
    private readonly Scalar<T> _scalar;
    
    public T ScalarValue 
        => _scalar.ScalarValue;

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
        => ScalarProcessor.IsOne(ScalarValue);
    
    public bool IsMinusOne
        => ScalarProcessor.IsMinusOne(ScalarValue);

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

    public override IEnumerable<T> Scalars
    {
        get
        {
            if (!IsZero) yield return ScalarValue;
        }
    }

    public override IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (!IsZero)
                yield return new KeyValuePair<XGaBasisBlade, T>(
                    Processor.BasisScalar,
                    ScalarValue
                );
        }
    }

    public override IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs
    {
        get
        {
            if (!IsZero)
                yield return new KeyValuePair<IndexSet, T>(
                    IndexSet.EmptySet,
                    ScalarValue
                );
        }
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaScalar(XGaProcessor<T> processor)
        : base(processor)
    {
        _scalar = processor.ScalarProcessor.Zero;
        IsZero = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaScalar(XGaProcessor<T> processor, T scalarValue)
        : base(processor)
    {
        Debug.Assert(
            processor.ScalarProcessor.IsValid(scalarValue)
        );

        _scalar = processor.ScalarProcessor.ScalarFromValue(scalarValue);
        IsZero = processor.ScalarProcessor.IsZero(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaScalar(XGaProcessor<T> processor, IScalar<T> scalar)
        : this(processor, scalar.ScalarValue)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaScalar(XGaProcessor<T> processor, IReadOnlyDictionary<IndexSet, T> idScalarDictionary)
        : base(processor)
    {
        _scalar = processor.ScalarProcessor.ScalarFromValue(
            idScalarDictionary.TryGetValue(IndexSet.EmptySet, out var scalar)
                ? scalar
                : processor.ScalarProcessor.ZeroValue
        );

        Debug.Assert(
            _scalar.IsValid()
        );

        IsZero = _scalar.IsZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return ScalarProcessor.IsValid(ScalarValue);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<IndexSet, T> GetIdScalarDictionary()
    {
        return IsZero
            ? new EmptyDictionary<IndexSet, T>()
            : new SingleItemDictionary<IndexSet, T>(IndexSet.EmptySet, _scalar.ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(IndexSet key)
    {
        return key.IsEmptySet && !IsZero;
    }
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ToScalar()
    {
        return _scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> GetScalarPart()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetVectorPart()
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> GetBivectorPart()
    {
        return Processor.BivectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> GetHigherKVectorPart(int grade)
    {
        return Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> GetPart(Func<IndexSet, bool> filterFunc)
    {
        return IsZero || filterFunc(IndexSet.EmptySet) 
            ? this 
            : Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> GetPart(Func<T, bool> filterFunc)
    {
        return IsZero || filterFunc(ScalarValue) 
            ? this 
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> GetPart(Func<IndexSet, T, bool> filterFunc)
    {
        return IsZero || filterFunc(IndexSet.EmptySet, ScalarValue) 
            ? this 
            : Processor.ScalarZero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> Scalar()
    {
        return _scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetBasisBladeScalar(IndexSet basisBladeId)
    {
        return basisBladeId.IsEmptySet
            ? _scalar
            : Processor.ScalarProcessor.Zero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out T scalar)
    {
        if (!IsZero)
        {
            scalar = ScalarValue;
            return true;
        }

        scalar = ScalarProcessor.ZeroValue;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out T scalar)
    {
        if (basisBlade.IsEmptySet)
        {
            scalar = ScalarValue;
            return true;
        }

        scalar = ScalarProcessor.ZeroValue;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Simplify()
    {
        return this;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool Equals(XGaScalar<T> other)
    {
        return Equals(_scalar, other._scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is XGaScalar<T> other && Equals(other);
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