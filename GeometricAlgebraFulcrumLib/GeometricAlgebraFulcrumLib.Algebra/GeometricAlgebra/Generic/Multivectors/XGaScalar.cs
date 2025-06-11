using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

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
            if (!IsZero) yield return Processor.UnitBasisScalar;
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
                    Processor.UnitBasisScalar,
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
    public override XGaVector<T> GetVectorPart(Func<int, bool> filter)
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
    public override XGaScalar<T> GetPart(Func<IndexSet, bool> filterFunc)
    {
        return IsZero || filterFunc(IndexSet.EmptySet) 
            ? this 
            : Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> GetPart(Func<T, bool> filterFunc)
    {
        return IsZero || filterFunc(ScalarValue) 
            ? this 
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> GetPart(Func<IndexSet, T, bool> filterFunc)
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
    public override XGaGradedMultivectorComposer<T> ToComposer()
    {
        return Processor.CreateMultivectorComposer().SetScalarTerm(ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> NegativeToComposer()
    {
        return Processor.CreateMultivectorComposer().SetScalarTerm(Negative());
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaGradedMultivectorComposer<T> ToComposer(T scalingFactor)
    {
        return Processor.CreateMultivectorComposer().SetScalarTerm(Times(scalingFactor));
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Convert(XGaFloat64Processor processor)
    {
        return processor.Scalar(
            ScalarProcessor.ToFloat64(ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Convert(XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        return processor.Scalar(
            scalarMapping(ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T2> Convert<T2>(XGaProcessor<T2> processor, Func<T, T2> scalarMapping)
    {
        return new XGaScalar<T2>(
            processor,
            scalarMapping(ScalarValue)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> MapScalars(ScalarTransformer<T> transformer)
    {
        return Processor.Scalar(
            transformer.MapScalarValue(ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ReflectOn(XGaKVector<T> subspace)
    {
        Debug.Assert(subspace.IsNearBlade());

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ReflectDirectOnDirect(XGaKVector<T> subspace)
    {
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ReflectDirectOnDual(XGaKVector<T> subspace)
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ReflectDualOnDirect(XGaKVector<T> subspace, int vSpaceDimensions)
    {
        var n = subspace.Grade + vSpaceDimensions;

        return n.IsOdd() ? -this : this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ReflectDualOnDual(XGaKVector<T> subspace)
    {
        return subspace.IsOdd() ? -this : this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ProjectOn(XGaKVector<T> subspace, bool useSubspaceInverse = false)
    {
        Debug.Assert(subspace.IsNearBlade());
        
        var subspaceInverse = 
            useSubspaceInverse 
                ? subspace.PseudoInverse() 
                : subspace;

        return Fdp(subspaceInverse).Gp(subspace).GetScalarPart();
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
        if (IsZero)
            return $"'{ScalarProcessor.Zero.ToText()}'<>";

        return BasisScalarPairs
            .OrderBy(p => p.Key.Id.Count)
            .ThenBy(p => p.Key.Id)
            .Select(p => $"'{ScalarProcessor.ToText(p.Value)}'{p.Key}")
            .Concatenate(" + ");
    }
}