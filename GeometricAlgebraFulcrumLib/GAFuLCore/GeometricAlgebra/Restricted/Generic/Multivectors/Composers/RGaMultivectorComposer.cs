using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using Open.Collections;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;

public sealed class RGaMultivectorComposer<T> :
    IRGaElement<T>
{
    private Dictionary<ulong, T> _idScalarDictionary
        = new Dictionary<ulong, T>();


    public RGaProcessor<T> Processor { get; }

    public IScalarProcessor<T> ScalarProcessor
        => Processor.ScalarProcessor;

    public RGaMetric Metric
        => Processor;

    public bool IsZero
        => _idScalarDictionary.Count == 0;

    public T this[ulong id]
    {
        get => GetTermScalarValue(id);
        set => SetTerm(id, value);
    }

    public IEnumerable<KeyValuePair<ulong, T>> IdScalarPairs
        => _idScalarDictionary;

    public IEnumerable<KeyValuePair<RGaBasisBlade, T>> BasisBladeScalarPairs
        => _idScalarDictionary.Select(p =>
            new KeyValuePair<RGaBasisBlade, T>(
                Processor.CreateBasisBlade(p.Key),
                p.Value
            )
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal RGaMultivectorComposer(RGaProcessor<T> processor)
    {
        Processor = processor;
    }


    public bool IsValid()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> Clear()
    {
        _idScalarDictionary = new Dictionary<ulong, T>();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> ClearScalarTerm()
    {
        _idScalarDictionary.Remove(
            0UL
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> ClearVectorTerm(int index)
    {
        _idScalarDictionary.Remove(
            1UL << index
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> ClearBivectorTerm(int index1, int index2)
    {
        _idScalarDictionary.Remove(
            BasisBivectorUtils.IndexPairToBivectorId(index1, index2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> ClearTerm(IPair<int> indexPair)
    {
        _idScalarDictionary.Remove(
            BasisBivectorUtils.IndexPairToBivectorId(indexPair.Item1, indexPair.Item2)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> ClearTerm(ulong basisBladeId)
    {
        _idScalarDictionary.Remove(basisBladeId);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalarTermScalarValue()
    {
        var key = 0UL;

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetVectorTermScalarValue(int index)
    {
        var key = 1UL << index;

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetBivectorTermScalarValue(int index1, int index2)
    {
        var key = BasisBivectorUtils.IndexPairToBivectorId(index1, index2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTermScalarValue(IPair<int> indexPair)
    {
        var key = BasisBivectorUtils.IndexPairToBivectorId(indexPair.Item1, indexPair.Item2);

        return _idScalarDictionary.TryGetValue(key, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTermScalarValue(ulong basisBlade)
    {
        return _idScalarDictionary.TryGetValue(basisBlade, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetScalarTermScalar()
    {
        return ScalarProcessor.ScalarFromValue(
            GetScalarTermScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetVectorTermScalar(int index)
    {
        return ScalarProcessor.ScalarFromValue(
            GetVectorTermScalarValue(index)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetBivectorTermScalar(int index1, int index2)
    {
        return ScalarProcessor.ScalarFromValue(
            GetBivectorTermScalarValue(index1, index2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetBivectorTermScalar(IPair<int> indexPair)
    {
        return ScalarProcessor.ScalarFromValue(
            GetTermScalarValue(indexPair)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetTermScalar(ulong basisBlade)
    {
        return ScalarProcessor.ScalarFromValue(
            GetTermScalarValue(basisBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> RemoveTerm(ulong basisBlade)
    {
        _idScalarDictionary.Remove(basisBlade);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetScalarTerm(T scalar)
    {
        SetTerm(0UL, scalar);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetScalarTerm(IScalar<T> scalar)
    {
        SetTerm(0UL, scalar.ScalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetVectorTerm(int index, int scalar)
    {
        return SetVectorTerm(
            index, 
            ScalarProcessor.ValueFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetVectorTerm(int index, uint scalar)
    {
        return SetVectorTerm(
            index, 
            ScalarProcessor.ValueFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetVectorTerm(int index, long scalar)
    {
        return SetVectorTerm(
            index, 
            ScalarProcessor.ValueFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetVectorTerm(int index, ulong scalar)
    {
        return SetVectorTerm(
            index, 
            ScalarProcessor.ValueFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetVectorTerm(int index, float scalar)
    {
        return SetVectorTerm(
            index, 
            ScalarProcessor.ValueFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetVectorTerm(int index, double scalar)
    {
        return SetVectorTerm(
            index, 
            ScalarProcessor.ValueFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetVectorTerm(int index, T scalar)
    {
        SetTerm(1UL << index, scalar);

        return this;
    }
      
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetVectorTerm(int index, IScalar<T> scalar)
    {
        SetTerm(1UL << index, scalar.ScalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, double scalar)
    {
        SetTerm(
            BasisBivectorUtils.IndexPairToBivectorId(index1, index2),
            ScalarProcessor.ValueFromNumber(scalar)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetBivectorTerm(int index1, int index2, T scalar)
    {
        SetTerm(
            BasisBivectorUtils.IndexPairToBivectorId(index1, index2),
            scalar
        );

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetTrivectorTerm(int index1, int index2, int index3, double scalar)
    {
        SetTerm(
            BasisBladeUtils.IndexTripletToTrivectorId(index1, index2, index3),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetBivectorTerm(IPair<int> indexPair, T scalar)
    {
        SetTerm(
            BasisBivectorUtils.IndexPairToBivectorId(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetTerm(ulong basisBlade, double scalar)
    {
        return SetTerm(
            basisBlade, 
            ScalarProcessor.ValueFromNumber(scalar)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetTerm(ulong basisBlade, string scalar)
    {
        return SetTerm(
            basisBlade, 
            ScalarProcessor.ScalarFromText(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetTerm(ulong basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
        {
            _idScalarDictionary.Remove(basisBlade);
            return this;
        }

        if (_idScalarDictionary.ContainsKey(basisBlade))
            _idScalarDictionary[basisBlade] = scalar;
        else
            _idScalarDictionary.Add(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetTerm(ulong basisBlade, Scalar<T> scalar)
    {
        return SetTerm(basisBlade, scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetTerm(ulong basisBlade, IScalar<T> scalar)
    {
        return SetTerm(basisBlade, scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SetVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (index, scalar) in termList)
            SetVectorTerm(index, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetScalar(RGaScalar<T> scalar)
    {
        SetScalarTerm(
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetScalarNegative(RGaScalar<T> scalar)
    {
        SetScalarTerm(
            ScalarProcessor.Negative(scalar.ScalarValue)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetScalar(RGaScalar<T> scalar, T scalingFactor)
    {
        SetScalarTerm(
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );

        return this;
    }

    public RGaMultivectorComposer<T> SetMultivector(RGaMultivector<T> multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, scalar);

        return this;
    }

    public RGaMultivectorComposer<T> SetMultivectorNegative(RGaMultivector<T> multivector)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, ScalarProcessor.Negative(scalar));

        return this;
    }

    public RGaMultivectorComposer<T> SetMultivector(RGaMultivector<T> multivector, T scalingFactor)
    {
        foreach (var (basis, scalar) in multivector.IdScalarPairs)
            SetTerm(basis, ScalarProcessor.Times(scalar, scalingFactor));

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddScalarTerm(T scalar)
    {
        AddTerm(0UL, scalar);

        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddScalarTerm(IScalar<T> scalar)
    {
        AddTerm(0UL, scalar.ScalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddVectorTerm(int index, T scalar)
    {
        AddTerm(
            1UL << index,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddBivectorTerm(int index1, int index2, T scalar)
    {
        AddTerm(
            BasisBivectorUtils.IndexPairToBivectorId(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddTerm(IPair<int> indexPair, T scalar)
    {
        AddTerm(
            BasisBivectorUtils.IndexPairToBivectorId(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddTerm(ulong basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
            return this;

        if (_idScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Add(scalar1, scalar).ScalarValue;

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _idScalarDictionary.Remove(basisBlade);
            else
                _idScalarDictionary[basisBlade] = scalar2;
        }
        else
            _idScalarDictionary.Add(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddTerm(ulong basisBlade, IScalar<T> scalar)
    {
        return AddTerm(basisBlade, scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            AddVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddTerms(IEnumerable<KeyValuePair<ulong, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddScalar(RGaScalar<T> scalar)
    {
        if (scalar.IsZero)
            return this;

        AddTerm(
            0UL,
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddScalar(RGaScalar<T> scalar, T scalingFactor)
    {
        AddTerm(
            0UL,
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddMultivector(RGaMultivector<T> vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddMultivector(RGaMultivector<T> vector, T scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            AddTerm(
                basisBlade,
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractScalarTerm(T scalar)
    {
        SubtractTerm(
            0UL,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractVectorTerm(int index, T scalar)
    {
        SubtractTerm(
            1UL << index,
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractBivectorTerm(int index1, int index2, T scalar)
    {
        SubtractTerm(
            BasisBivectorUtils.IndexPairToBivectorId(index1, index2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractTerm(IPair<int> indexPair, T scalar)
    {
        SubtractTerm(
            BasisBivectorUtils.IndexPairToBivectorId(indexPair.Item1, indexPair.Item2),
            scalar
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractTerm(ulong basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
            return this;

        if (_idScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Subtract(scalar1, scalar).ScalarValue;

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _idScalarDictionary.Remove(basisBlade);
            else
                _idScalarDictionary[basisBlade] = scalar2;
        }
        else
            _idScalarDictionary.Add(basisBlade, ScalarProcessor.Negative(scalar).ScalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractTerm(ulong basisBlade, IScalar<T> scalar)
    {
        return SubtractTerm(basisBlade, scalar.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractVectorTerms(int firstIndex, IEnumerable<T> scalarList)
    {
        var i = firstIndex;
        foreach (var scalar in scalarList)
            SubtractVectorTerm(i++, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractVectorTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractVectorTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<ulong, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractTerm(basisBlade, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractScalar(RGaScalar<T> scalar)
    {
        if (scalar.IsZero)
            return this;

        SubtractTerm(
            0UL,
            scalar.ScalarValue
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractScalar(RGaScalar<T> scalar, T scalingFactor)
    {
        SubtractTerm(
            0UL,
            ScalarProcessor.Times(scalar.ScalarValue, scalingFactor)
        );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractMultivector(RGaMultivector<T> vector)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractMultivector(RGaMultivector<T> vector, T scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IdScalarPairs)
            SubtractTerm(
                basisBlade,
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetTerm(RGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero)
            return RemoveTerm(basisBlade.Id);

        var scalar = basisBlade.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return SetTerm(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SetTerm(RGaSignedBasisBlade basisBlade, T scalar)
    {
        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return RemoveTerm(basisBlade.Id);

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return SetTerm(
            basisBlade.Id,
            scalar1
        );
    }

    public RGaMultivectorComposer<T> SetTerms(IEnumerable<KeyValuePair<ulong, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    public RGaMultivectorComposer<T> SetTerms(IEnumerable<KeyValuePair<RGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddTerm(ulong basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (scalar.IsZero())
            return this;

        return AddTerm(
            basisBlade,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddTerm(IRGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero)
            return this;

        var scalar = basisBlade.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return AddTerm(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddTerm(IRGaSignedBasisBlade basisBlade, T scalar)
    {
        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return AddTerm(
            basisBlade.Id,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddTerm(IRGaSignedBasisBlade basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        return AddTerm(
            basisBlade.Id,
            basisBlade.IsPositive ? scalar : scalar.Negative()
        );
    }

    public RGaMultivectorComposer<T> AddTerms(IEnumerable<KeyValuePair<RGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddEGpTerm(ulong id, T scalar1, T scalar2)
    {
        var term = Processor.EGp(id, id);
        var scalar = term.IsPositive
            ? ScalarProcessor.Times(scalar1, scalar2)
            : ScalarProcessor.NegativeTimes(scalar1, scalar2);

        return AddScalarTerm(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddEGpTerm(KeyValuePair<ulong, T> term1, KeyValuePair<ulong, T> term2)
    {
        var term = Processor.EGp(term1.Key, term2.Key);
        var scalar = term.IsPositive
            ? ScalarProcessor.Times(term1.Value, term2.Value)
            : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

        return AddTerm(term.Id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddGpTerm(ulong id, T scalar1, T scalar2)
    {
        var term = Processor.Gp(id, id);

        if (term.IsZero) return this;

        var scalar = term.IsPositive
            ? ScalarProcessor.Times(scalar1, scalar2)
            : ScalarProcessor.NegativeTimes(scalar1, scalar2);

        return AddScalarTerm(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> AddGpTerm(KeyValuePair<ulong, T> term1, KeyValuePair<ulong, T> term2)
    {
        var term = Processor.Gp(term1.Key, term2.Key);

        if (term.IsZero) return this;

        var scalar = term.IsPositive
            ? ScalarProcessor.Times(term1.Value, term2.Value)
            : ScalarProcessor.NegativeTimes(term1.Value, term2.Value);

        return AddTerm(term.Id, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractTerm(ulong basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (scalar.IsZero())
            return this;

        return SubtractTerm(
            basisBlade,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractTerm(RGaSignedBasisBlade basisBlade)
    {
        if (basisBlade.IsZero)
            return this;

        var scalar = basisBlade.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return SubtractTerm(
            basisBlade.Id,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractTerm(RGaSignedBasisBlade basisBlade, T scalar)
    {
        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return SubtractTerm(
            basisBlade.Id,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> SubtractTerm(RGaSignedBasisBlade basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        if (basisBlade.IsNegative)
            scalar = scalar.Negative();

        return SubtractTerm(
            basisBlade.Id,
            scalar
        );
    }

    public RGaMultivectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<RGaSignedBasisBlade, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }


    public RGaMultivectorComposer<T> MapScalars(Func<T, T> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<ulong, T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = id;
            var scalar1 = mappingFunction(scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id1, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public RGaMultivectorComposer<T> MapScalars(Func<ulong, T, T> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<ulong, T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = id;
            var scalar1 = mappingFunction(id, scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id1, scalar1);
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public RGaMultivectorComposer<T> MapBasisBlades(Func<ulong, ulong> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<ulong, T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = mappingFunction(id);

            if (idScalarDictionary.TryGetValue(id1, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(id1);
                else
                    idScalarDictionary[id1] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(id1, scalar);
            }
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public RGaMultivectorComposer<T> MapBasisBlades(Func<ulong, T, ulong> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<ulong, T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var id1 = mappingFunction(id, scalar);

            if (idScalarDictionary.TryGetValue(id1, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, scalar).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(id1);
                else
                    idScalarDictionary[id1] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(id1, scalar);
            }
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }

    public RGaMultivectorComposer<T> MapTerms(Func<ulong, T, KeyValuePair<ulong, T>> mappingFunction)
    {
        if (_idScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<ulong, T>();

        foreach (var (id, scalar) in _idScalarDictionary)
        {
            var term1 = mappingFunction(id, scalar);

            if (ScalarProcessor.IsZero(term1.Value))
                continue;

            if (idScalarDictionary.TryGetValue(term1.Key, out var scalar2))
            {
                var scalar1 = ScalarProcessor.Add(scalar2, term1.Value).ScalarValue;

                if (!ScalarProcessor.IsValid(scalar1))
                    throw new InvalidOperationException();

                if (ScalarProcessor.IsZero(scalar1))
                    idScalarDictionary.Remove(term1.Key);
                else
                    idScalarDictionary[term1.Key] = scalar1;
            }
            else
            {
                idScalarDictionary.Add(term1.Key, term1.Value);
            }
        }

        _idScalarDictionary = idScalarDictionary;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> Negative()
    {
        return MapScalars(s => ScalarProcessor.Negative(s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> Times(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> Divide(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> Reverse()
    {
        return MapScalars((id, scalar) =>
            id.Grade().ReverseIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> GradeInvolution()
    {
        return MapScalars((id, scalar) =>
            id.Grade().GradeInvolutionIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> CliffordConjugate()
    {
        return MapScalars((id, scalar) =>
            id.Grade().CliffordConjugateIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivectorComposer<T> Conjugate()
    {
        return MapScalars((id, scalar) =>
            ScalarProcessor.Times(
                Processor.HermitianConjugateSign(id),
                scalar
            ).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENormSquared()
    {
        var scalarList =
            _idScalarDictionary
                .Values
                .Select(s => ScalarProcessor.Times(s, s));

        return ScalarProcessor.Add(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENorm()
    {
        return ENormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        var scalarList =
            _idScalarDictionary
                .Select(p =>
                    ScalarProcessor.Times(Processor.Signature(p.Key), p.Value, p.Value)
                );

        return ScalarProcessor.Add(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return ENormSquared().SqrtOfAbs();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> GetScalarPart()
    {
        return Processor.Scalar(
            GetScalarTermScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetVectorPart()
    {
        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade() == 1)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.Vector(idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> GetBivectorPart()
    {
        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade() == 2)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.Bivector(idScalarDictionary);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaHigherKVector<T> GetHigherKVectorPart(int grade)
    {
        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade() == grade)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.HigherKVector(grade, idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetKVectorPart(int grade)
    {
        if (grade < 0)
            throw new InvalidOperationException();

        if (grade == 0)
            return GetScalarPart();

        if (grade == 1)
            return GetVectorPart();

        if (grade == 2)
            return GetBivectorPart();

        var idScalarDictionary =
            _idScalarDictionary
                .Where(p => p.Key.Grade() == grade)
                .ToDictionary(
                    p => p.Key,
                    p => p.Value
                );

        return Processor.HigherKVector(grade, idScalarDictionary);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> GetScalar()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Count == 1 && _idScalarDictionary.First().Key == 0
        );

        return _idScalarDictionary.TryGetValue(0UL, out var scalar)
            ? Processor.Scalar(scalar)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GetVector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Grade() == 1)
        );

        return Processor.Vector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> GetBivector()
    {
        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Grade() == 2)
        );

        return Processor.Bivector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaHigherKVector<T> GetHigherKVector(int grade)
    {
        Debug.Assert(
            grade >= 3 &&
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Grade() == grade)
        );

        return Processor.HigherKVector(grade, _idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> GetKVector(int grade)
    {
        if (grade < 0)
            throw new InvalidOperationException();

        if (grade == 0)
            return GetScalar();

        if (grade == 1)
            return GetVector();

        if (grade == 2)
            return GetBivector();

        Debug.Assert(
            _idScalarDictionary.Count == 0 ||
            _idScalarDictionary.Keys.All(id => id.Grade() == grade)
        );

        return Processor.KVector(grade, _idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaGradedMultivector<T> GetMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.MultivectorZero;

        if (_idScalarDictionary.Count == 1)
            return Processor.Multivector(

                _idScalarDictionary.First()
            );

        var gradeGroup =
            _idScalarDictionary.GroupBy(
                basisScalarPair => basisScalarPair.Key.Grade()
            );

        var gradeKVectorDictionary = new Dictionary<int, RGaKVector<T>>();

        foreach (var gradeBasisScalarPairGroups in gradeGroup)
        {
            var grade = gradeBasisScalarPairGroups.Key;

            if (grade == 0)
            {
                var scalar = Processor.Scalar(

                    gradeBasisScalarPairGroups.First().Value
                );

                gradeKVectorDictionary.Add(grade, scalar);

                continue;
            }

            var idScalarDictionary = new Dictionary<ulong, T>();

            idScalarDictionary.AddRange(gradeBasisScalarPairGroups);

            var kVector = Processor.KVector(

                grade,
                idScalarDictionary
            );

            gradeKVectorDictionary.Add(grade, kVector);
        }

        return Processor.Multivector(gradeKVectorDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> GetUniformMultivector()
    {
        return Processor.UniformMultivector(_idScalarDictionary);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> GetSimpleMultivector()
    {
        if (_idScalarDictionary.Count == 0)
            return Processor.ScalarZero;

        if (_idScalarDictionary.Count == 1)
            return Processor.KVectorTerm(

                _idScalarDictionary.First()
            );

        var gradeGroup =
            _idScalarDictionary.GroupBy(
                basisScalarPair => basisScalarPair.Key.Grade()
            );

        var gradeKVectorDictionary = new Dictionary<int, RGaKVector<T>>();

        foreach (var gradeBasisScalarPairGroups in gradeGroup)
        {
            var grade = gradeBasisScalarPairGroups.Key;

            if (grade == 0)
            {
                var scalar = Processor.Scalar(

                    gradeBasisScalarPairGroups.First().Value
                );

                gradeKVectorDictionary.Add(grade, scalar);

                continue;
            }

            var idScalarDictionary = new Dictionary<ulong, T>();

            idScalarDictionary.AddRange(gradeBasisScalarPairGroups);

            var kVector = Processor.KVector(

                grade,
                idScalarDictionary
            );

            gradeKVectorDictionary.Add(grade, kVector);
        }

        return gradeKVectorDictionary.Count == 1
            ? gradeKVectorDictionary.First().Value
            : Processor.Multivector(gradeKVectorDictionary);
    }

}