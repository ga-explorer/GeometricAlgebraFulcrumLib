using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

public sealed partial class RGaScalar<T> :
    RGaKVector<T>,
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

    public override IEnumerable<RGaBasisBlade> BasisBlades
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (!IsZero) yield return Processor.BasisScalar;
        }
    }

    public override IEnumerable<ulong> Ids
    {
        get
        {
            if (!IsZero) yield return 0UL;
        }
    }

    public override IEnumerable<T> Scalars
    {
        get
        {
            if (!IsZero) yield return ScalarValue;
        }
    }

    public override IEnumerable<KeyValuePair<RGaBasisBlade, T>> BasisScalarPairs
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (!IsZero)
                yield return new KeyValuePair<RGaBasisBlade, T>(
                    Processor.BasisScalar,
                    ScalarValue
                );
        }
    }

    public override IEnumerable<KeyValuePair<ulong, T>> IdScalarPairs
    {
        get
        {
            if (!IsZero)
                yield return new KeyValuePair<ulong, T>(
                    0UL,
                    ScalarValue
                );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaScalar(RGaProcessor<T> processor, Scalar<T> scalar)
        : base(processor)
    {
        Debug.Assert(
            scalar.IsValid()
        );

        _scalar = scalar;
        IsZero = scalar.IsZero();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaScalar(RGaProcessor<T> processor)
        : base(processor)
    {
        _scalar = processor.ScalarProcessor.Zero;
        IsZero = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaScalar(RGaProcessor<T> processor, T scalarValue)
        : base(processor)
    {
        Debug.Assert(
            processor.ScalarProcessor.IsValid(scalarValue)
        );

        _scalar = processor.ScalarProcessor.ScalarFromValue(scalarValue);
        IsZero = processor.ScalarProcessor.IsZero(ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaScalar(RGaProcessor<T> processor, IReadOnlyDictionary<ulong, T> idScalarDictionary)
        : base(processor)
    {
        _scalar = processor.ScalarProcessor.ScalarFromValue(
            idScalarDictionary.TryGetValue(0UL, out var scalar)
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
    public Scalar<T> ToScalar()
    {
        return _scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> Scalar()
    {
        return _scalar;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<ulong, T> GetIdScalarDictionary()
    {
        return IsZero
            ? new EmptyDictionary<ulong, T>()
            : new SingleItemDictionary<ulong, T>(0, _scalar.ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(ulong key)
    {
        return key == 0UL && !IsZero;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> GetScalarPart()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaVector<T> GetVectorPart()
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaBivector<T> GetBivectorPart()
    {
        return Processor.BivectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaHigherKVector<T> GetHigherKVectorPart(int grade)
    {
        return Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> GetPart(Func<ulong, bool> filterFunc)
    {
        return IsZero || filterFunc(0) 
            ? this 
            : Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> GetPart(Func<T, bool> filterFunc)
    {
        return IsZero || filterFunc(ScalarValue) 
            ? this 
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> GetPart(Func<ulong, T, bool> filterFunc)
    {
        return IsZero || filterFunc(0, ScalarValue) 
            ? this 
            : Processor.ScalarZero;
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetBasisBladeScalar(ulong basisBladeId)
    {
        return basisBladeId == 0UL
            ? _scalar
            : ScalarProcessor.Zero;
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
    public override bool TryGetBasisBladeScalarValue(ulong basisBlade, out T scalar)
    {
        if (basisBlade == 0UL)
        {
            scalar = ScalarValue;
            return true;
        }

        scalar = ScalarProcessor.ZeroValue;
        return false;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Simplify()
    {
        return this;
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool Equals(RGaScalar<T> other)
    {
        return Equals(_scalar, other._scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is RGaScalar<T> other && Equals(other);
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