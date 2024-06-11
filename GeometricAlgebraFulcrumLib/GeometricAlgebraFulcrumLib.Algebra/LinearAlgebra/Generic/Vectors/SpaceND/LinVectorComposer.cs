using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;

public sealed class LinVectorComposer<T> :
    ILinearAlgebraElement<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorComposer<T> Create(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorComposer<T>(scalarProcessor);
    }


    private Dictionary<int, T> _indexScalarDictionary
        = new Dictionary<int, T>();


    public int VSpaceDimensions
        => _indexScalarDictionary.Count == 0
            ? 0
            : _indexScalarDictionary.Max(p => p.Key) + 1;

    public IScalarProcessor<T> ScalarProcessor { get; }

    public bool IsZero
        => _indexScalarDictionary.Count == 0;

    public T this[int index]
    {
        get => GetTermScalarValue(index);
        set => SetTerm(index, value);
    }

    public IEnumerable<KeyValuePair<int, T>> IndexScalarPairs
        => _indexScalarDictionary;

    public IEnumerable<KeyValuePair<LinBasisVector, T>> BasisBladeScalarPairs
        => _indexScalarDictionary.Select(p =>
            new KeyValuePair<LinBasisVector, T>(
                p.Key.ToLinBasisVector(),
                p.Value
            )
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinVectorComposer(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _indexScalarDictionary.IsValidLinVectorDictionary(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> Clear()
    {
        _indexScalarDictionary.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> ClearTerm(int index)
    {
        _indexScalarDictionary.Remove(index);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetTermScalarValue(int basisBlade)
    {
        return _indexScalarDictionary.TryGetValue(basisBlade, out var scalarValue)
            ? scalarValue
            : ScalarProcessor.ZeroValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetTermScalar(int basisBlade)
    {
        return ScalarProcessor.ScalarFromValue(
            GetTermScalarValue(basisBlade)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> RemoveTerm(int basisBlade)
    {
        _indexScalarDictionary.Remove(basisBlade);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SetTerm(int basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
        {
            _indexScalarDictionary.Remove(basisBlade);
            return this;
        }

        if (_indexScalarDictionary.ContainsKey(basisBlade))
            _indexScalarDictionary[basisBlade] = scalar;
        else
            _indexScalarDictionary.Add(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SetTerm(int basisBlade, IScalar<T> scalar)
    {
        return SetTerm(basisBlade, scalar.ScalarValue);
    }

    public LinVectorComposer<T> SetVector(LinVector<T> multivector)
    {
        foreach (var (basis, scalar) in multivector.IndexScalarPairs)
            SetTerm(basis, scalar);

        return this;
    }

    public LinVectorComposer<T> SetVectorNegative(LinVector<T> multivector)
    {
        foreach (var (basis, scalar) in multivector.IndexScalarPairs)
            SetTerm(basis, ScalarProcessor.Negative(scalar));

        return this;
    }

    public LinVectorComposer<T> SetVector(LinVector<T> multivector, T scalingFactor)
    {
        foreach (var (basis, scalar) in multivector.IndexScalarPairs)
            SetTerm(basis, ScalarProcessor.Times(scalar, scalingFactor));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddTerm(int basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
            return this;

        if (_indexScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Add(scalar1, scalar).ScalarValue;

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _indexScalarDictionary.Remove(basisBlade);
            else
                _indexScalarDictionary[basisBlade] = scalar2;
        }
        else
            _indexScalarDictionary.Add(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddTerm(int basisBlade, IScalar<T> scalar)
    {
        return AddTerm(basisBlade, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddVector(LinVector<T> vector)
    {
        foreach (var (basisBlade, scalar) in vector.IndexScalarPairs)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddVector(LinVector<T> vector, T scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IndexScalarPairs)
            AddTerm(
                basisBlade,
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractTerm(int basisBlade, T scalar)
    {
        Debug.Assert(ScalarProcessor.IsValid(scalar));

        if (ScalarProcessor.IsZero(scalar))
            return this;

        if (_indexScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = ScalarProcessor.Subtract(scalar1, scalar).ScalarValue;

            Debug.Assert(ScalarProcessor.IsValid(scalar2));

            if (ScalarProcessor.IsZero(scalar2))
                _indexScalarDictionary.Remove(basisBlade);
            else
                _indexScalarDictionary[basisBlade] = scalar2;
        }
        else
            _indexScalarDictionary.Add(basisBlade, ScalarProcessor.Negative(scalar).ScalarValue);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractTerm(int basisBlade, IScalar<T> scalar)
    {
        return SubtractTerm(basisBlade, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractVector(LinVector<T> vector)
    {
        foreach (var (basisBlade, scalar) in vector.IndexScalarPairs)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractVector(LinVector<T> vector, T scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IndexScalarPairs)
            SubtractTerm(
                basisBlade,
                ScalarProcessor.Times(scalar, scalingFactor)
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SetTerm(LinSignedBasisVector basisBlade)
    {
        if (basisBlade.IsZero)
            return RemoveTerm(basisBlade.Index);

        var scalar = basisBlade.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return SetTerm(
            basisBlade.Index,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SetTerm(LinSignedBasisVector basisBlade, T scalar)
    {
        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return RemoveTerm(basisBlade.Index);

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return SetTerm(
            basisBlade.Index,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SetTerm(LinSignedBasisVector basisBlade, IScalar<T> scalar)
    {
        return SetTerm(basisBlade, scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SetTerm(LinVectorTerm<T> term)
    {
        return SetTerm(term.Index, term.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SetTerm(LinVectorTerm<T> term, IntegerSign scalar)
    {
        if (scalar.IsZero || term.IsZero)
            return RemoveTerm(term.Index);

        var scalar1 = scalar.IsPositive
            ? term.ScalarValue
            : ScalarProcessor.Negative(term.ScalarValue).ScalarValue;

        return SetTerm(term.Index, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SetTerm(LinVectorTerm<T> term, T scalar)
    {
        var scalar1 = ScalarProcessor.Times(term.ScalarValue, scalar);

        return SetTerm(term.Index, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SetTermNegative(LinVectorTerm<T> term)
    {
        return SetTerm(
            term.Index,
            ScalarProcessor.Negative(term.ScalarValue)
        );
    }

    public LinVectorComposer<T> SetTerms(IEnumerable<KeyValuePair<int, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    public LinVectorComposer<T> SetTerms(IEnumerable<KeyValuePair<LinSignedBasisVector, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    public LinVectorComposer<T> SetTerms(IEnumerable<LinVectorTerm<T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis.Index, scalar.ScalarValue);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddTerm(int basisBlade, T scalar1, T scalar2)
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
    public LinVectorComposer<T> AddTerm(ILinSignedBasisVector basisBlade)
    {
        if (basisBlade.IsZero)
            return this;

        var scalar = basisBlade.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return AddTerm(
            basisBlade.Index,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddTerm(ILinSignedBasisVector basisBlade, T scalar)
    {
        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return AddTerm(
            basisBlade.Index,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddTerm(ILinSignedBasisVector basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        return AddTerm(
            basisBlade.Index,
            basisBlade.IsPositive ? scalar : scalar.Negative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddTerm(LinVectorTerm<T> term)
    {
        if (term.IsZero)
            return this;

        return AddTerm(term.Index, term.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddTerm(LinVectorTerm<T> term, int scalar)
    {
        if (scalar == 0 || term.IsZero)
            return this;

        var scalar1 = scalar == 1
            ? term.ScalarValue
            : ScalarProcessor.Times(term.ScalarValue, scalar).ScalarValue;

        return AddTerm(
            term.Index,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddTerm(LinVectorTerm<T> term, T scalar)
    {
        var scalar1 = ScalarProcessor.Times(term.ScalarValue, scalar);

        if (scalar1.IsZero())
            return this;

        return AddTerm(
            term.Index,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> AddTermNegative(LinVectorTerm<T> term)
    {
        if (term.IsZero)
            return this;

        return AddTerm(
            term.Index,
            ScalarProcessor.Negative(term.ScalarValue)
        );
    }

    public LinVectorComposer<T> AddTerms(IEnumerable<KeyValuePair<LinSignedBasisVector, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }

    public LinVectorComposer<T> AddTerms(IEnumerable<LinVectorTerm<T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis.Index, scalar.ScalarValue);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractTerm(int basisBlade, T scalar1, T scalar2)
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
    public LinVectorComposer<T> SubtractTerm(LinSignedBasisVector basisBlade)
    {
        if (basisBlade.IsZero)
            return this;

        var scalar = basisBlade.IsPositive
            ? ScalarProcessor.OneValue
            : ScalarProcessor.MinusOneValue;

        return SubtractTerm(
            basisBlade.Index,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractTerm(LinSignedBasisVector basisBlade, T scalar)
    {
        if (basisBlade.IsZero || ScalarProcessor.IsZero(scalar))
            return this;

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : ScalarProcessor.Negative(scalar).ScalarValue;

        return SubtractTerm(
            basisBlade.Index,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractTerm(LinSignedBasisVector basisBlade, T scalar1, T scalar2)
    {
        var scalar = ScalarProcessor.Times(scalar1, scalar2);

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        if (basisBlade.IsNegative)
            scalar = scalar.Negative();

        return SubtractTerm(
            basisBlade.Index,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractTerm(LinVectorTerm<T> term)
    {
        if (term.IsZero)
            return this;

        return SubtractTerm(
            term.Index,
            term.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractTerm(LinVectorTerm<T> term, int scalar)
    {
        if (scalar == 0 || term.IsZero)
            return this;

        var scalar1 = scalar == 1
            ? term.ScalarValue
            : ScalarProcessor.Times(term.ScalarValue, scalar).ScalarValue;

        return SubtractTerm(term.Index, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractTerm(LinVectorTerm<T> term, T scalar)
    {
        var scalar1 = ScalarProcessor.Times(term.ScalarValue, scalar);

        if (scalar1.IsZero())
            return this;

        return SubtractTerm(term.Index, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> SubtractTermNegative(LinVectorTerm<T> term)
    {
        if (term.IsZero)
            return this;

        return SubtractTerm(
            term.Index,
            ScalarProcessor.Negative(term.ScalarValue)
        );
    }

    public LinVectorComposer<T> SubtractTerms(IEnumerable<KeyValuePair<LinSignedBasisVector, T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }

    public LinVectorComposer<T> SubtractTerms(IEnumerable<LinVectorTerm<T>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SubtractTerm(basis.Index, scalar.ScalarValue);

        return this;
    }


    public LinVectorComposer<T> MapScalars(Func<T, T> mappingFunction)
    {
        if (_indexScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<int, T>();

        foreach (var (id, scalar) in _indexScalarDictionary)
        {
            var id1 = id;
            var scalar1 = mappingFunction(scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id1, scalar1);
        }

        _indexScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinVectorComposer<T> MapScalars(Func<int, T, T> mappingFunction)
    {
        if (_indexScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<int, T>();

        foreach (var (id, scalar) in _indexScalarDictionary)
        {
            var id1 = id;
            var scalar1 = mappingFunction(id, scalar);

            if (!ScalarProcessor.IsValid(scalar1))
                throw new InvalidOperationException();

            if (!ScalarProcessor.IsZero(scalar1))
                idScalarDictionary.Add(id1, scalar1);
        }

        _indexScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinVectorComposer<T> MapBasisVectors(Func<int, int> mappingFunction)
    {
        if (_indexScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<int, T>();

        foreach (var (id, scalar) in _indexScalarDictionary)
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

        _indexScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinVectorComposer<T> MapBasisVectors(Func<int, T, int> mappingFunction)
    {
        if (_indexScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<int, T>();

        foreach (var (id, scalar) in _indexScalarDictionary)
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

        _indexScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinVectorComposer<T> MapTerms(Func<int, T, KeyValuePair<int, T>> mappingFunction)
    {
        if (_indexScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<int, T>();

        foreach (var (id, scalar) in _indexScalarDictionary)
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

        _indexScalarDictionary = idScalarDictionary;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> Negative()
    {
        return MapScalars(s => ScalarProcessor.Negative(s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> Times(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> Divide(T scalarFactor)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalarFactor).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENormSquared()
    {
        var scalarList =
            _indexScalarDictionary
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
    public LinVector<T> GetVector()
    {
        Debug.Assert(
            _indexScalarDictionary.Count == 0 ||
            _indexScalarDictionary.Keys.All(index => index >= 0)
        );

        return ScalarProcessor.CreateLinVector(_indexScalarDictionary);
    }

}