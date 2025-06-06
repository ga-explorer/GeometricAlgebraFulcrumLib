﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaBivector<T> :
    XGaKVector<T>
{
    private readonly IReadOnlyDictionary<IndexSet, T> _idScalarDictionary;


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

    public override IEnumerable<IndexSet> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<T> Scalars
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _idScalarDictionary.Values;
    }

    public override IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs
        => _idScalarDictionary;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaBivector(XGaProcessor<T> processor)
        : base(processor)
    {
        _idScalarDictionary = new EmptyDictionary<IndexSet, T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaBivector(XGaProcessor<T> processor, KeyValuePair<IndexSet, T> basisScalarPair)
        : base(processor)
    {
        _idScalarDictionary =
            new SingleItemDictionary<IndexSet, T>(basisScalarPair);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaBivector(XGaProcessor<T> processor, IReadOnlyDictionary<IndexSet, T> scalarDictionary)
        : base(processor)
    {
        _idScalarDictionary = scalarDictionary;

        Debug.Assert(IsValid());
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidBivectorDictionary(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IReadOnlyDictionary<IndexSet, T> GetIdScalarDictionary()
    {
        return _idScalarDictionary;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool ContainsKey(IndexSet key)
    {
        return !IsZero && _idScalarDictionary.ContainsKey(key);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> GetScalarPart()
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetVectorPart()
    {
        return Processor.VectorZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> GetBivectorPart()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> GetHigherKVectorPart(int grade)
    {
        return Processor.HigherKVectorZero(grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetBivectorPart(Func<int, int, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = 
            _idScalarDictionary.Where(term => 
                filterFunc(term.Key.FirstIndex, term.Key.LastIndex)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetPart(Func<T, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> GetPart(Func<IndexSet, T, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.Bivector(idScalarDictionary);
    }


    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Processor.CreateBasisBlade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> Scalar()
    {
        return ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetBasisBladeScalar(IndexSet basisBlade)
    {
        return _idScalarDictionary.TryGetValue(basisBlade, out var scalar)
            ? ScalarProcessor.ScalarFromValue(scalar)
            : ScalarProcessor.Zero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetScalarValue(out T scalar)
    {
        scalar = ScalarProcessor.ZeroValue;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBlade, out T scalar)
    {
        if (_idScalarDictionary.TryGetValue(basisBlade, out scalar))
            return true;

        scalar = ScalarProcessor.ZeroValue;
        return false;
    }


    public override IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, T>(Processor.CreateBasisBlade(p.Key), p.Value)
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Simplify()
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
            .Concatenate(" + ");
    }
}