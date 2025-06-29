﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;

public sealed class LinFloat64VectorComposer :
    IFloat64LinearAlgebraElement
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorComposer Create()
    {
        return new LinFloat64VectorComposer();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorComposer Create(int index, double scalar)
    {
        return new LinFloat64VectorComposer().SetTerm(index, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorComposer Create(LinFloat64Vector vector)
    {
        return new LinFloat64VectorComposer().SetVector(vector);
    }


    private Dictionary<int, double> _indexScalarDictionary
        = new Dictionary<int, double>();

    public int VSpaceDimensions
        => _indexScalarDictionary.Count == 0
            ? 0
            : _indexScalarDictionary.Max(p => p.Key) + 1;

    public bool IsZero
        => _indexScalarDictionary.Count == 0;

    public double this[int index]
    {
        get => GetTermScalarValue(index);
        set => SetTerm(index, value);
    }

    public IEnumerable<KeyValuePair<int, double>> IndexScalarPairs
        => _indexScalarDictionary;

    public IEnumerable<KeyValuePair<LinBasisVector, double>> BasisBladeScalarPairs
        => _indexScalarDictionary.Select(p =>
            new KeyValuePair<LinBasisVector, double>(
                p.Key.ToLinBasisVector(),
                p.Value
            )
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64VectorComposer()
    {
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _indexScalarDictionary.IsValidLinVectorDictionary();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer Clear()
    {
        _indexScalarDictionary.Clear();

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer ClearTerm(int index)
    {
        _indexScalarDictionary.Remove(index);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetTermScalarValue(int basisBlade)
    {
        return _indexScalarDictionary.TryGetValue(basisBlade, out var scalarValue)
            ? scalarValue
            : 0d;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer RemoveTerm(int basisBlade)
    {
        _indexScalarDictionary.Remove(basisBlade);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SetTerm(int basisBlade, double scalar)
    {
        Debug.Assert(scalar.IsValid());

        if (scalar.IsZero())
            _indexScalarDictionary.Remove(basisBlade);
        else
            _indexScalarDictionary.AddOrSet(basisBlade, scalar);

        return this;
    }

    public LinFloat64VectorComposer SetVector(LinFloat64Vector vector)
    {
        foreach (var (basis, scalar) in vector.IndexScalarPairs)
            SetTerm(basis, scalar);

        return this;
    }

    public LinFloat64VectorComposer SetVectorNegative(LinFloat64Vector vector)
    {
        foreach (var (basis, scalar) in vector.IndexScalarPairs)
            SetTerm(basis, -scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SetVector(LinBasisVector vector, double scalingFactor)
    {
        var index = vector.Index;
        var sign = vector.Sign;

        if (sign.IsPositive)
            SetTerm(index, scalingFactor);

        else if (sign.IsNegative)
            SetTerm(index, -scalingFactor);

        else
            RemoveTerm(index);

        return this;
    }

    public LinFloat64VectorComposer SetVector(LinFloat64Vector vector, double scalingFactor)
    {
        foreach (var (basis, scalar) in vector.IndexScalarPairs)
            SetTerm(basis, scalar * scalingFactor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddTerm(int basisBlade, double scalar)
    {
        Debug.Assert(scalar.IsValid());

        if (scalar.IsZero())
            return this;

        if (_indexScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = scalar1 + scalar;

            Debug.Assert(scalar2.IsValid());

            if (scalar2.IsZero())
                _indexScalarDictionary.Remove(basisBlade);
            else
                _indexScalarDictionary[basisBlade] = scalar2;
        }
        else
            _indexScalarDictionary.Add(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddVector(LinFloat64Vector vector)
    {
        foreach (var (basisBlade, scalar) in vector.IndexScalarPairs)
            AddTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddVector(LinBasisVector vector)
    {
        var index = vector.Index;
        var sign = vector.Sign;

        if (sign.IsPositive)
            AddTerm(index, 1);

        else if (sign.IsNegative)
            AddTerm(index, -1);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddVector(LinBasisVector vector, double scalingFactor)
    {
        var index = vector.Index;
        var sign = vector.Sign;

        if (sign.IsPositive)
            AddTerm(index, scalingFactor);

        else if (sign.IsNegative)
            AddTerm(index, -scalingFactor);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddVector(LinFloat64Vector vector, double scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IndexScalarPairs)
            AddTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractTerm(int basisBlade, double scalar)
    {
        Debug.Assert(scalar.IsValid());

        if (scalar.IsZero())
            return this;

        if (_indexScalarDictionary.TryGetValue(basisBlade, out var scalar1))
        {
            var scalar2 = scalar1 - scalar;

            Debug.Assert(scalar2.IsValid());

            if (scalar2.IsZero())
                _indexScalarDictionary.Remove(basisBlade);
            else
                _indexScalarDictionary[basisBlade] = scalar2;
        }
        else
            _indexScalarDictionary.Add(basisBlade, -scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basisBlade, scalar) in termList)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractVector(LinBasisVector vector)
    {
        var index = vector.Index;
        var sign = vector.Sign;

        if (sign.IsPositive)
            AddTerm(index, -1);

        else if (sign.IsNegative)
            AddTerm(index, 1);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractVector(LinFloat64Vector vector)
    {
        foreach (var (basisBlade, scalar) in vector.IndexScalarPairs)
            SubtractTerm(basisBlade, scalar);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractVector(LinFloat64Vector vector, double scalingFactor)
    {
        foreach (var (basisBlade, scalar) in vector.IndexScalarPairs)
            SubtractTerm(
                basisBlade,
                scalar * scalingFactor
            );

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SetTerm(LinBasisVector basisBlade)
    {
        if (basisBlade.IsZero)
            return RemoveTerm(basisBlade.Index);

        var scalar = basisBlade.IsPositive
            ? 1d : -1d;

        return SetTerm(
            basisBlade.Index,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SetTerm(LinBasisVector basisBlade, double scalar)
    {
        if (basisBlade.IsZero || scalar.IsZero())
            return RemoveTerm(basisBlade.Index);

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : -scalar;

        return SetTerm(
            basisBlade.Index,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SetTerm(LinFloat64VectorTerm term)
    {
        return SetTerm(term.Index, term.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SetTerm(LinFloat64VectorTerm term, IntegerSign scalar)
    {
        if (scalar.IsZero || term.IsZero)
            return RemoveTerm(term.Index);

        var scalar1 = scalar.IsPositive
            ? term.ScalarValue
            : -term.ScalarValue;

        return SetTerm(term.Index, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SetTerm(LinFloat64VectorTerm term, double scalar)
    {
        var scalar1 = term.ScalarValue * scalar;

        return SetTerm(term.Index, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SetTermNegative(LinFloat64VectorTerm term)
    {
        return SetTerm(
            term.Index,
            -term.ScalarValue
        );
    }

    public LinFloat64VectorComposer SetTerms(IEnumerable<double> termList)
    {
        var index = 0;
        foreach (var scalar in termList)
            SetTerm(index++, scalar);

        return this;
    }

    public LinFloat64VectorComposer SetTerms(IEnumerable<KeyValuePair<int, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    public LinFloat64VectorComposer SetTerms(IEnumerable<KeyValuePair<LinBasisVector, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis, scalar);

        return this;
    }

    public LinFloat64VectorComposer SetTerms(IEnumerable<LinFloat64VectorTerm> termList)
    {
        foreach (var (basis, scalar) in termList)
            SetTerm(basis.Index, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddTerm(int basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (scalar.IsZero())
            return this;

        return AddTerm(
            basisBlade,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddTerm(LinBasisVector basisBlade)
    {
        if (basisBlade.IsZero)
            return this;

        var scalar = basisBlade.IsPositive
            ? 1d : -1d;

        return AddTerm(
            basisBlade.Index,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddTerm(LinBasisVector basisBlade, double scalar)
    {
        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        var scalar1 = basisBlade.IsPositive
            ? scalar
            : -scalar;

        return AddTerm(
            basisBlade.Index,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddTerm(LinBasisVector basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        return AddTerm(
            basisBlade.Index,
            basisBlade.IsPositive ? scalar : -scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddTerm(LinFloat64VectorTerm term)
    {
        if (term.IsZero)
            return this;

        return AddTerm(term.Index, term.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddTerm(LinFloat64VectorTerm term, int scalar)
    {
        if (scalar == 0 || term.IsZero)
            return this;

        var scalar1 = scalar == 1
            ? term.ScalarValue
            : term.ScalarValue * scalar;

        return AddTerm(
            term.Index,
            scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddTerm(LinFloat64VectorTerm term, double scalar)
    {
        var scalar1 = term.ScalarValue * scalar;

        return scalar1.IsZero()
            ? this
            : AddTerm(term.Index, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer AddTermNegative(LinFloat64VectorTerm term)
    {
        return term.IsZero
            ? this
            : AddTerm(term.Index, -term.ScalarValue);
    }

    public LinFloat64VectorComposer AddTerms(IEnumerable<KeyValuePair<LinBasisVector, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }

    public LinFloat64VectorComposer AddTerms(IEnumerable<LinFloat64VectorTerm> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis.Index, scalar);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractTerm(int basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (scalar.IsZero())
            return this;

        return SubtractTerm(
            basisBlade,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractTerm(LinBasisVector basisBlade)
    {
        if (basisBlade.IsZero)
            return this;

        var scalar = basisBlade.IsPositive
            ? 1d
            : -1d;

        return SubtractTerm(
            basisBlade.Index,
            scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractTerm(LinBasisVector basisBlade, double scalar)
    {
        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        var scalar1 = basisBlade.IsPositive
            ? scalar : -scalar;

        return SubtractTerm(basisBlade.Index, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractTerm(LinBasisVector basisBlade, double scalar1, double scalar2)
    {
        var scalar = scalar1 * scalar2;

        if (basisBlade.IsZero || scalar.IsZero())
            return this;

        if (basisBlade.IsNegative)
            scalar = -scalar;

        return SubtractTerm(basisBlade.Index, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractTerm(LinFloat64VectorTerm term)
    {
        return term.IsZero
            ? this
            : SubtractTerm(term.Index, term.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractTerm(LinFloat64VectorTerm term, int scalar)
    {
        if (scalar == 0 || term.IsZero)
            return this;

        var scalar1 = term.ScalarValue * scalar;

        return SubtractTerm(term.Index, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractTerm(LinFloat64VectorTerm term, double scalar)
    {
        var scalar1 = term.ScalarValue * scalar;

        return scalar1.IsZero()
            ? this
            : SubtractTerm(term.Index, scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer SubtractTermNegative(LinFloat64VectorTerm term)
    {
        if (term.IsZero)
            return this;

        return SubtractTerm(
            term.Index,
            -term.ScalarValue
        );
    }

    public LinFloat64VectorComposer SubtractTerms(IEnumerable<KeyValuePair<LinBasisVector, double>> termList)
    {
        foreach (var (basis, scalar) in termList)
            AddTerm(basis, scalar);

        return this;
    }

    public LinFloat64VectorComposer SubtractTerms(IEnumerable<LinFloat64VectorTerm> termList)
    {
        foreach (var (basis, scalar) in termList)
            SubtractTerm(basis.Index, scalar);

        return this;
    }


    public LinFloat64VectorComposer MapScalars(Func<double, double> mappingFunction)
    {
        if (_indexScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<int, double>();

        foreach (var (id, scalar) in _indexScalarDictionary)
        {
            var scalar1 = mappingFunction(scalar);

            if (!scalar1.IsValid())
                throw new InvalidOperationException();

            if (!scalar1.IsZero())
                idScalarDictionary.Add(id, scalar1);
        }

        _indexScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinFloat64VectorComposer MapScalars(Func<int, double, double> mappingFunction)
    {
        if (_indexScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<int, double>();

        foreach (var (id, scalar) in _indexScalarDictionary)
        {
            var scalar1 = mappingFunction(id, scalar);

            if (!scalar1.IsValid())
                throw new InvalidOperationException();

            if (!scalar1.IsZero())
                idScalarDictionary.Add(id, scalar1);
        }

        _indexScalarDictionary = idScalarDictionary;

        return this;
    }

    public LinFloat64VectorComposer MapBasisVectors(Func<int, int> mappingFunction)
    {
        if (_indexScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<int, double>();

        foreach (var (id, scalar) in _indexScalarDictionary)
        {
            var id1 = mappingFunction(id);

            if (idScalarDictionary.TryGetValue(id1, out var scalar2))
            {
                var scalar1 = scalar2 + scalar;

                if (!scalar1.IsValid())
                    throw new InvalidOperationException();

                if (scalar1.IsZero())
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

    public LinFloat64VectorComposer MapBasisVectors(Func<int, double, int> mappingFunction)
    {
        if (_indexScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<int, double>();

        foreach (var (id, scalar) in _indexScalarDictionary)
        {
            var id1 = mappingFunction(id, scalar);

            if (idScalarDictionary.TryGetValue(id1, out var scalar2))
            {
                var scalar1 = scalar2 + scalar;

                if (!scalar1.IsValid())
                    throw new InvalidOperationException();

                if (scalar1.IsZero())
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

    public LinFloat64VectorComposer MapTerms(Func<int, double, KeyValuePair<int, double>> mappingFunction)
    {
        if (_indexScalarDictionary.Count == 0) return this;

        var idScalarDictionary = new Dictionary<int, double>();

        foreach (var (id, scalar) in _indexScalarDictionary)
        {
            var term1 = mappingFunction(id, scalar);

            if (term1.Value.IsZero())
                continue;

            if (idScalarDictionary.TryGetValue(term1.Key, out var scalar2))
            {
                var scalar1 = scalar2 + term1.Value;

                if (!scalar1.IsValid())
                    throw new InvalidOperationException();

                if (scalar1.IsZero())
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
    public LinFloat64VectorComposer Negative()
    {
        return MapScalars(s => -s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer Times(double scalarFactor)
    {
        return MapScalars(s => s * scalarFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorComposer Divide(double scalarFactor)
    {
        var s1 = 1d / scalarFactor;

        return MapScalars(s => s * s1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ENormSquared()
    {
        var scalarList =
            _indexScalarDictionary
                .Values
                .Select(s => s * s);

        return scalarList.Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ENorm()
    {
        return ENormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double VectorDot(IReadOnlyList<double> vector)
    {
        return _indexScalarDictionary.VectorDot(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double VectorDot(IReadOnlyDictionary<int, double> vector)
    {
        return _indexScalarDictionary.VectorDot(vector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector GetVector()
    {
        Debug.Assert(
            _indexScalarDictionary.Count == 0 ||
            _indexScalarDictionary.Keys.All(index => index >= 0)
        );

        return _indexScalarDictionary.CreateLinVector();
    }
    
    public LinFloat64VectorComposer AddComponentTimesTerms(IReadOnlyDictionary<int, double> mv1, IReadOnlyDictionary<int, double> mv2)
    {
        if (mv1.Count == 0 || mv2.Count == 0)
            return this;

        if (mv1.Count <= mv2.Count)
        {
            foreach (var (id, scalar1) in mv1)
            {
                if (!mv2.TryGetValue(id, out var scalar2))
                    continue;

                AddTerm(id, scalar1 * scalar2);
            }
        }
        else
        {
            foreach (var (id, scalar2) in mv2)
            {
                if (!mv1.TryGetValue(id, out var scalar1))
                    continue;

                AddTerm(id, scalar1 * scalar2);
            }
        }

        return this;
    }


}