﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaVector<T> :
    XGaKVector<T>
{
    private readonly IReadOnlyDictionary<IndexSet, T> _idScalarDictionary;


    public override string MultivectorClassName
        => "Generic Vector";

    public override int Count
        => _idScalarDictionary.Count;

    public override IEnumerable<int> KVectorGrades
    {
        get
        {
            if (!IsZero) yield return 1;
        }
    }

    public override int Grade
        => 1;

    public override bool IsZero
        => _idScalarDictionary.Count == 0;

    public override IEnumerable<IndexSet> Ids
        => _idScalarDictionary.Keys;

    public override IEnumerable<T> Scalars
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _idScalarDictionary.Values;
    }
        
    public override IEnumerable<XGaBasisBlade> BasisBlades
        => _idScalarDictionary.Keys.Select(Processor.BasisBlade);

    public IEnumerable<KeyValuePair<int, T>> IndexScalarPairs
        => _idScalarDictionary.Select(p => 
            new KeyValuePair<int, T>(p.Key.FirstIndex, p.Value)
        );

    public override IEnumerable<KeyValuePair<IndexSet, T>> IdScalarPairs
        => _idScalarDictionary;
    
    public override IEnumerable<KeyValuePair<XGaBasisBlade, T>> BasisScalarPairs
    {
        get
        {
            return _idScalarDictionary.Select(p =>
                new KeyValuePair<XGaBasisBlade, T>(
                    Processor.BasisBlade(p.Key),
                    p.Value
                )
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaVector(XGaProcessor<T> processor)
        : base(processor)
    {
        _idScalarDictionary = new EmptyDictionary<IndexSet, T>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaVector(XGaProcessor<T> processor, KeyValuePair<IndexSet, T> basisScalarPair)
        : base(processor)
    {
        _idScalarDictionary =
            new SingleItemDictionary<IndexSet, T>(basisScalarPair);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal XGaVector(XGaProcessor<T> processor, IReadOnlyDictionary<IndexSet, T> idScalarDictionary)
        : base(processor)
    {
        _idScalarDictionary = idScalarDictionary;

        Debug.Assert(IsValid());
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return _idScalarDictionary.IsValidVectorDictionary(Processor);
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
    public Scalar<T> GetTermScalarByIndex(int index)
    {
        var id = index.ToUnitIndexSet();

        return GetBasisBladeScalar(id);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> GetScalarPart()
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetVectorPart()
    {
        return this;
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
    public override XGaVector<T> GetVectorPart(Func<int, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = 
            _idScalarDictionary.Where(term => 
                filterFunc(term.Key.FirstIndex)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetPart(Func<IndexSet, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetPart(Func<T, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Value)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaVector<T> GetPart(Func<IndexSet, T, bool> filterFunc)
    {
        if (IsZero) return this;

        var idScalarDictionary = _idScalarDictionary
            .Where(
                p => filterFunc(p.Key, p.Value)
            ).ToDictionary();

        return Processor.Vector(idScalarDictionary);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> Scalar()
    {
        return ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Scalar<T> GetBasisBladeScalar(IndexSet basisBladeId)
    {
        return _idScalarDictionary.TryGetValue(basisBladeId, out var scalar)
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
    public override bool TryGetBasisBladeScalarValue(IndexSet basisBladeId, out T scalar)
    {
        if (basisBladeId.IsUnitSet && _idScalarDictionary.TryGetValue(basisBladeId, out scalar))
            return true;

        scalar = ScalarProcessor.ZeroValue;
        return false;
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