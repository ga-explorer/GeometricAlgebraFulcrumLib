//using System.Collections;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
//using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Scalars;
//using GeometricAlgebraFulcrumLib.Algebra.BasicMath.Tuples;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

//namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D;

//public class LinFloat64UnilinearMapComposer :
//    IReadOnlyDictionary<IndexPair, double>
//{
//    private Dictionary<IndexPair, double> _indexPairScalarDictionary
//        = new Dictionary<IndexPair, double>();

    
//    public int Count
//        => _indexPairScalarDictionary.Count;

//    public IEnumerable<IndexPair> Keys
//        => _indexPairScalarDictionary.Keys;

//    public IEnumerable<double> Values
//        => _indexPairScalarDictionary.Values;

//    public double this[IndexPair key]
//    {
//        get => _indexPairScalarDictionary.TryGetValue(key, out var mv)
//            ? mv : 0d;

//        set => SetTerm(key.Index1, key.Index2, value);
//    }
    
//    public double this[int index1, int index2]
//    {
//        get => _indexPairScalarDictionary.TryGetValue(
//                new IndexPair(index1, index2), 
//                out var mv
//            ) ? mv : 0d;

//        set => SetTerm(index1, index2, value);
//    }


//    
//    internal LinFloat64UnilinearMapComposer()
//    {
//    }


//    
//    public bool IsValid()
//    {
//        return _indexPairScalarDictionary.Values.All(
//            d => d.IsValid()
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer Clear()
//    {
//        _indexPairScalarDictionary.Clear();

//        return this;
//    }
    
//    
//    public LinFloat64UnilinearMapComposer RemoveTerm(int index1, int index2)
//    {
//        var index = new IndexPair(index1, index2);

//        _indexPairScalarDictionary.Remove(index);

//        return this;
//    }

//    
//    public bool ContainsKey(IndexPair key)
//    {
//        return _indexPairScalarDictionary.ContainsKey(key);
//    }

//    
//    public bool TryGetValue(IndexPair key, out double value)
//    {
//        return _indexPairScalarDictionary.TryGetValue(key, out value);
//    }
    

//    
//    public double GetTermScalarValue(int index1, int index2)
//    {
//        var index = new IndexPair(index1, index2);

//        return _indexPairScalarDictionary.TryGetValue(index, out var scalarValue)
//            ? scalarValue
//            : 0d;
//    }

//    
//    public double GetTermScalar(int index1, int index2)
//    {
//        return GetTermScalarValue(index1, index2);
//    }


//    
//    public LinFloat64UnilinearMapComposer SetTerm(int index1, int index2, double scalar)
//    {
//        Debug.Assert(scalar.IsValid());

//        var index = new IndexPair(index1, index2);

//        if (scalar.IsZero())
//        {
//            _indexPairScalarDictionary.Remove(index);
//            return this;
//        }

//        if (_indexPairScalarDictionary.ContainsKey(index))
//            _indexPairScalarDictionary[index] = scalar;
//        else
//            _indexPairScalarDictionary.Add(index, scalar);

//        return this;
//    }
    
//    
//    public LinFloat64UnilinearMapComposer SetColumnTerm(int index2, LinBasisVector basisVector)
//    {
//        if (basisVector.IsZero)
//            return RemoveTerm(basisVector.Index, index2);

//        var scalar = basisVector.IsPositive
//            ? 1d
//            : -1d;

//        return SetTerm(
//            basisVector.Index,
//            index2,
//            scalar
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer SetColumnTerm(int index2, LinBasisVector basisVector, double scalar)
//    {
//        if (basisVector.IsZero || scalar.IsZero())
//            return RemoveTerm(basisVector.Index, index2);

//        var scalar1 = basisVector.IsPositive
//            ? scalar
//            : -scalar;

//        return SetTerm(
//            basisVector.Index,
//            index2,
//            scalar1
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer SetColumnTerm(int index2, LinFloat64VectorTerm term)
//    {
//        return SetTerm(
//            term.Index,
//            index2,
//            term.ScalarValue
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer SetColumnTerm(int index2, LinFloat64VectorTerm term, IntegerSign scalar)
//    {
//        if (scalar.IsZero || term.IsZero)
//            return RemoveTerm(term.Index, index2);

//        var scalar1 = scalar.IsPositive
//            ? term.ScalarValue
//            : -term.ScalarValue;

//        return SetTerm(term.Index, index2, scalar1);
//    }

//    
//    public LinFloat64UnilinearMapComposer SetColumnTerm(int index2, LinFloat64VectorTerm term, double scalar)
//    {
//        var scalar1 = term.ScalarValue * scalar;

//        return SetTerm(term.Index, index2, scalar1);
//    }

//    
//    public LinFloat64UnilinearMapComposer SetColumnTermNegative(int index2, LinFloat64VectorTerm term)
//    {
//        return SetTerm(
//            term.Index,
//            index2,
//            -term.ScalarValue
//        );
//    }

//    public LinFloat64UnilinearMapComposer SetTerms(IEnumerable<KeyValuePair<IndexPair, double>> terms)
//    {
//        foreach (var term in terms)
//            SetTerm(term.Key.Index1, term.Key.Index2, term.Value);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SetColumnTerms(int index2, IEnumerable<KeyValuePair<int, double>> termList)
//    {
//        foreach (var (index1, scalar) in termList)
//            SetTerm(index1, index2, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SetColumnTerms(int index2, IEnumerable<KeyValuePair<LinBasisVector, double>> termList)
//    {
//        foreach (var (basisVector, scalar) in termList)
//            SetColumnTerm(index2, basisVector, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SetColumnTerms(int index2, IEnumerable<LinFloat64VectorTerm> termList)
//    {
//        foreach (var (basisVector, scalar) in termList)
//            SetTerm(basisVector.Index, index2, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SetColumnVector(int index2, Float64Tuple3D vector)
//    {
//        foreach (var (index1, scalar) in vector.IndexScalarPairs)
//            SetTerm(index1, index2, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SetColumnVectorNegative(int index2, Float64Tuple3D vector)
//    {
//        foreach (var (index1, scalar) in vector.IndexScalarPairs)
//            SetTerm(index1, index2, -scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SetColumnVector(int index2, Float64Tuple3D vector, double scalingFactor)
//    {
//        foreach (var (index1, scalar) in vector.IndexScalarPairs)
//            SetTerm(index1, index2, scalar * scalingFactor);

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SetColumnVector(int index2, IReadOnlyList<double> vector)
//    {
//        for (var index1 = 0; index1 < vector.Count; index1++)
//            SetTerm(index1, index2, vector[index1]);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SetColumnVector(int index2, IReadOnlyList<double> vector, double scalingFactor)
//    {
//        for (var index1 = 0; index1 < vector.Count; index1++)
//            SetTerm(
//                index1, 
//                index2, 
//                vector[index1] * scalingFactor
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SetColumnVectorNegative(int index2, IReadOnlyList<double> vector)
//    {
//        for (var index1 = 0; index1 < vector.Count; index1++)
//            SetTerm(
//                index1, 
//                index2, 
//                -vector[index1]
//            );

//        return this;
//    }

//    
//    public LinFloat64UnilinearMapComposer SetIdentityMap(int count)
//    {
//        _indexPairScalarDictionary =
//            count
//                .GetRange()
//                .ToDictionary(
//                    i => new IndexPair(i),
//                    _ => 1d
//                );

//        return this;
//    }
    
//    
//    public LinFloat64UnilinearMapComposer SetDiagonalMap(IReadOnlyList<double> vector)
//    {
//        _indexPairScalarDictionary = new Dictionary<IndexPair, double>();

//        for (var i = 0; i < vector.Count; i++)
//        {
//            var scalar = vector[i];

//            if (scalar.IsZero())
//                continue;

//            _indexPairScalarDictionary.Add(
//                new IndexPair(i), 
//                scalar
//            );
//        }

//        return this;
//    }
    
//    
//    public LinFloat64UnilinearMapComposer SetDiagonalMap(Float64Tuple3D vector)
//    {
//        _indexPairScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (i, scalar) in vector.IndexScalarPairs)
//        {
//            _indexPairScalarDictionary.Add(
//                new IndexPair(i), 
//                scalar
//            );
//        }

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SetMap(LinFloat64UnilinearMap map)
//    {
//        foreach (var ((index1, index2), scalar) in map.IndexScalarPairs)
//            SetTerm(index1, index2, scalar);

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SetMap(LinFloat64UnilinearMap map, double scalingFactor)
//    {
//        foreach (var ((index1, index2), scalar) in map.IndexScalarPairs)
//            SetTerm(
//                index1, 
//                index2, 
//                scalar * scalingFactor
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SetMapNegative(LinFloat64UnilinearMap map)
//    {
//        foreach (var ((index1, index2), scalar) in map.IndexScalarPairs)
//            SetTerm(
//                index1, 
//                index2, 
//                -scalar
//            );

//        return this;
//    }


//    
//    public LinFloat64UnilinearMapComposer AddTerm(int index1, int index2, double scalar)
//    {
//        Debug.Assert(scalar.IsValid());

//        if (scalar.IsZero())
//            return this;

//        var index = new IndexPair(index1, index2);

//        if (_indexPairScalarDictionary.TryGetValue(index, out var scalar1))
//        {
//            var scalar2 = scalar1 * scalar;

//            Debug.Assert(scalar2.IsValid());

//            if (scalar2.IsZero())
//                _indexPairScalarDictionary.Remove(index);
//            else
//                _indexPairScalarDictionary[index] = scalar2;
//        }
//        else
//            _indexPairScalarDictionary.Add(index, scalar);

//        return this;
//    }
    
//    
//    public LinFloat64UnilinearMapComposer AddTerm(int index1, int index2, double scalar1, double scalar2)
//    {
//        var scalar = scalar1 * scalar2;

//        if (scalar.IsZero())
//            return this;

//        return AddTerm(
//            index1,
//            index2,
//            scalar
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer AddColumnTerm(int index2, LinBasisVector basisVector)
//    {
//        if (basisVector.IsZero)
//            return this;

//        var scalar = basisVector.IsPositive
//            ? 1d
//            : -1d;

//        return AddTerm(
//            basisVector.Index,
//            index2,
//            scalar
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer AddColumnTerm(int index2, LinBasisVector index, double scalar)
//    {
//        if (index.IsZero || scalar.IsZero())
//            return this;

//        var scalar1 = index.IsPositive
//            ? scalar
//            : -scalar;

//        return AddTerm(
//            index.Index,
//            index2,
//            scalar1
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer AddColumnTerm(int index2, LinBasisVector index, double scalar1, double scalar2)
//    {
//        var scalar = scalar1 * scalar2;

//        if (index.IsZero || scalar.IsZero())
//            return this;

//        return AddTerm(
//            index.Index,
//            index2,
//            index.IsPositive ? scalar : -scalar
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer AddColumnTerm(int index2, LinFloat64VectorTerm term)
//    {
//        if (term.IsZero)
//            return this;

//        return AddTerm(
//            term.Index,
//            index2,
//            term.ScalarValue
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer AddColumnTerm(int index2, LinFloat64VectorTerm term, int scalar)
//    {
//        if (scalar == 0 || term.IsZero)
//            return this;

//        var scalar1 = scalar == 1
//            ? term.ScalarValue
//            : term.ScalarValue * scalar;

//        return AddTerm(
//            term.Index,
//            index2,
//            scalar1
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer AddColumnTerm(int index2, LinFloat64VectorTerm term, double scalar)
//    {
//        var scalar1 = term.ScalarValue * scalar;

//        if (scalar1.IsZero())
//            return this;

//        return AddTerm(
//            term.Index,
//            index2,
//            scalar1
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer AddColumnTermNegative(int index2, LinFloat64VectorTerm term)
//    {
//        if (term.IsZero)
//            return this;

//        return AddTerm(
//            term.Index,
//            index2,
//            -term.ScalarValue
//        );
//    }

//    public LinFloat64UnilinearMapComposer AddColumnTerms(int index2, IEnumerable<KeyValuePair<LinBasisVector, double>> termList)
//    {
//        foreach (var (basisVector, scalar) in termList)
//            AddColumnTerm(index2, basisVector, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer AddColumnTerms(int index2, IEnumerable<LinFloat64VectorTerm> termList)
//    {
//        foreach (var (basisVector, scalar) in termList)
//            AddTerm(basisVector.Index, index2, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer AddColumnTerms(int index2, IEnumerable<KeyValuePair<int, double>> termList)
//    {
//        foreach (var (index1, scalar) in termList)
//            AddTerm(index1, index2, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer AddColumnVector(int index2, Float64Tuple3D vector)
//    {
//        foreach (var (index1, scalar) in vector.IndexScalarPairs)
//            AddTerm(index1, index2, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer AddColumnVector(int index2, Float64Tuple3D vector, double scalingFactor)
//    {
//        foreach (var (index1, scalar) in vector.IndexScalarPairs)
//            AddTerm(
//                index1,
//                index2,
//                scalar * scalingFactor
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer AddColumnVectorNegative(int index2, Float64Tuple3D vector)
//    {
//        foreach (var (index1, scalar) in vector)
//            AddTerm(
//                index1, 
//                index2, 
//                -scalar
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer AddColumnVector(int index2, IReadOnlyList<double> vector)
//    {
//        for (var index1 = 0; index1 < vector.Count; index1++)
//            AddTerm(index1, index2, vector[index1]);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer AddColumnVector(int index2, IReadOnlyList<double> vector, double scalingFactor)
//    {
//        for (var index1 = 0; index1 < vector.Count; index1++)
//            AddTerm(
//                index1, 
//                index2, 
//                vector[index1] * scalingFactor
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer AddColumnVectorNegative(int index2, IReadOnlyList<double> vector)
//    {
//        for (var index1 = 0; index1 < vector.Count; index1++)
//            AddTerm(
//                index1, 
//                index2, 
//                -vector[index1]
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer AddIdentityMap(int count)
//    {
//        for (var i = 0; i < count; i++)
//            AddTerm(i, i, 1d);

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer AddDiagonalMap(IReadOnlyList<double> vector)
//    {
//        for (var i = 0; i < vector.Count; i++)
//        {
//            var scalar = vector[i];

//            if (scalar.IsZero())
//                continue;

//            AddTerm(i, i, scalar);
//        }

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer AddDiagonalMap(Float64Tuple3D vector)
//    {
//        foreach (var (i, scalar) in vector)
//            AddTerm(i, i, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer AddMap(LinFloat64UnilinearMap map)
//    {
//        foreach (var ((index1, index2), scalar) in map)
//            AddTerm(index1, index2, scalar);

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer AddMap(LinFloat64UnilinearMap map, double scalingFactor)
//    {
//        foreach (var ((index1, index2), scalar) in map)
//            AddTerm(
//                index1, 
//                index2, 
//                scalar * scalingFactor
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer AddMapNegative(LinFloat64UnilinearMap map)
//    {
//        foreach (var ((index1, index2), scalar) in map)
//            AddTerm(
//                index1, 
//                index2, 
//                -scalar
//            );

//        return this;
//    }


//    
//    public LinFloat64UnilinearMapComposer SubtractTerm(int index1, int index2, double scalar)
//    {
//        Debug.Assert(scalar.IsValid());

//        if (scalar.IsZero())
//            return this;

//        var index = new IndexPair(index1, index2);

//        if (_indexPairScalarDictionary.TryGetValue(index, out var scalar1))
//        {
//            var scalar2 = scalar1 * scalar;

//            Debug.Assert(scalar2.IsValid());

//            if (scalar2.IsZero())
//                _indexPairScalarDictionary.Remove(index);
//            else
//                _indexPairScalarDictionary[index] = scalar2;
//        }
//        else
//            _indexPairScalarDictionary.Add(index, -scalar);

//        return this;
//    }
    
//    
//    public LinFloat64UnilinearMapComposer SubtractTerm(int index1, int index2, double scalar1, double scalar2)
//    {
//        var scalar = scalar1 * scalar2;

//        if (scalar.IsZero())
//            return this;

//        return SubtractTerm(
//            index1,
//            index2,
//            scalar
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer SubtractColumnTerm(int index2, LinBasisVector index)
//    {
//        if (index.IsZero)
//            return this;

//        var scalar = index.IsPositive
//            ? 1d
//            : -1d;

//        return SubtractTerm(
//            index.Index,
//            index2,
//            scalar
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer SubtractColumnTerm(int index2, LinBasisVector index, double scalar)
//    {
//        if (index.IsZero || scalar.IsZero())
//            return this;

//        var scalar1 = index.IsPositive
//            ? scalar
//            : -scalar;

//        return SubtractTerm(
//            index.Index,
//            index2,
//            scalar1
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer SubtractColumnTerm(int index2, LinBasisVector index, double scalar1, double scalar2)
//    {
//        var scalar = scalar1 * scalar2;

//        if (index.IsZero || scalar.IsZero())
//            return this;

//        if (index.IsNegative)
//            scalar = -scalar;

//        return SubtractTerm(
//            index.Index,
//            index2,
//            scalar
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer SubtractColumnTerm(int index2, LinFloat64VectorTerm term)
//    {
//        if (term.IsZero)
//            return this;

//        return SubtractTerm(
//            term.Index,
//            index2,
//            term.ScalarValue
//        );
//    }

//    
//    public LinFloat64UnilinearMapComposer SubtractColumnTerm(int index2, LinFloat64VectorTerm term, int scalar)
//    {
//        if (scalar == 0 || term.IsZero)
//            return this;

//        var scalar1 = scalar == 1
//            ? term.ScalarValue
//            : term.ScalarValue * scalar;

//        return SubtractTerm(term.Index, index2, scalar1);
//    }

//    
//    public LinFloat64UnilinearMapComposer SubtractColumnTerm(int index2, LinFloat64VectorTerm term, double scalar)
//    {
//        var scalar1 = term.ScalarValue * scalar;

//        if (scalar1.IsZero())
//            return this;

//        return SubtractTerm(term.Index, index2, scalar1);
//    }

//    
//    public LinFloat64UnilinearMapComposer SubtractColumnTermNegative(int index2, LinFloat64VectorTerm term)
//    {
//        if (term.IsZero)
//            return this;

//        return SubtractTerm(
//            term.Index,
//            index2,
//            -term.ScalarValue
//        );
//    }

//    public LinFloat64UnilinearMapComposer SubtractColumnTerms(int index2, IEnumerable<KeyValuePair<LinBasisVector, double>> termList)
//    {
//        foreach (var (basisVector, scalar) in termList)
//            AddColumnTerm(index2, basisVector, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SubtractColumnTerms(int index2, IEnumerable<LinFloat64VectorTerm> termList)
//    {
//        foreach (var (basisVector, scalar) in termList)
//            SubtractTerm(basisVector.Index, index2, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SubtractColumnTerms(int index2, IEnumerable<KeyValuePair<int, double>> termList)
//    {
//        foreach (var (index1, scalar) in termList)
//            SubtractTerm(index1, index2, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SubtractColumnVector(int index2, Float64Tuple3D vector)
//    {
//        foreach (var (index1, scalar) in vector.IndexScalarPairs)
//            SubtractTerm(index1, index2, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SubtractColumnVector(int index2, Float64Tuple3D vector, double scalingFactor)
//    {
//        foreach (var (index1, scalar) in vector.IndexScalarPairs)
//            SubtractTerm(
//                index1,
//                index2,
//                scalar * scalingFactor
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SubtractColumnVectorNegative(int index2, Float64Tuple3D vector)
//    {
//        foreach (var (index1, scalar) in vector)
//            SubtractTerm(
//                index1, 
//                index2, 
//                -scalar
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SubtractColumnVector(int index2, IReadOnlyList<double> vector)
//    {
//        for (var index1 = 0; index1 < vector.Count; index1++)
//            SubtractTerm(index1, index2, vector[index1]);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SubtractColumnVector(int index2, IReadOnlyList<double> vector, double scalingFactor)
//    {
//        for (var index1 = 0; index1 < vector.Count; index1++)
//            SubtractTerm(
//                index1, 
//                index2, 
//                vector[index1] * scalingFactor
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SubtractColumnVectorNegative(int index2, IReadOnlyList<double> vector)
//    {
//        for (var index1 = 0; index1 < vector.Count; index1++)
//            SubtractTerm(
//                index1, 
//                index2, 
//                -vector[index1]
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SubtractIdentityMap(int count)
//    {
//        for (var i = 0; i < count; i++)
//            SubtractTerm(i, i, 1d);

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SubtractDiagonalMap(IReadOnlyList<double> vector)
//    {
//        for (var i = 0; i < vector.Count; i++)
//        {
//            var scalar = vector[i];

//            if (scalar.IsZero())
//                continue;

//            SubtractTerm(i, i, scalar);
//        }

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SubtractDiagonalMap(Float64Tuple3D vector)
//    {
//        foreach (var (i, scalar) in vector)
//            SubtractTerm(i, i, scalar);

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer SubtractMap(LinFloat64UnilinearMap map)
//    {
//        foreach (var ((index1, index2), scalar) in map)
//            SubtractTerm(index1, index2, scalar);

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SubtractMap(LinFloat64UnilinearMap map, double scalingFactor)
//    {
//        foreach (var ((index1, index2), scalar) in map.IndexScalarPairs)
//            SubtractTerm(
//                index1, 
//                index2, 
//                scalar * scalingFactor
//            );

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer SubtractMapNegative(LinFloat64UnilinearMap map)
//    {
//        foreach (var ((index1, index2), scalar) in map)
//            SubtractTerm(
//                index1, 
//                index2, 
//                -scalar
//            );

//        return this;
//    }


//    public LinFloat64UnilinearMapComposer MapScalars(Func<double, double> mappingFunction)
//    {
//        if (_indexPairScalarDictionary.Count == 0) return this;

//        var idScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (id, scalar) in _indexPairScalarDictionary)
//        {
//            var scalar1 = mappingFunction(scalar);

//            if (!scalar1.IsValid())
//                throw new InvalidOperationException();

//            if (!scalar1.IsZero())
//                idScalarDictionary.Add(id, scalar1);
//        }

//        _indexPairScalarDictionary = idScalarDictionary;

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer MapScalars(Func<int, int, double, double> mappingFunction)
//    {
//        if (_indexPairScalarDictionary.Count == 0) return this;

//        var idScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (id, scalar) in _indexPairScalarDictionary)
//        {
//            var (index1, index2) = id;
//            var scalar1 = mappingFunction(index1, index2, scalar);

//            if (!scalar1.IsValid())
//                throw new InvalidOperationException();

//            if (!scalar1.IsZero())
//                idScalarDictionary.Add(id, scalar1);
//        }

//        _indexPairScalarDictionary = idScalarDictionary;

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer MapScalars(Func<IndexPair, double, double> mappingFunction)
//    {
//        if (_indexPairScalarDictionary.Count == 0) return this;

//        var idScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (id, scalar) in _indexPairScalarDictionary)
//        {
//            var scalar1 = mappingFunction(id, scalar);

//            if (!scalar1.IsValid())
//                throw new InvalidOperationException();

//            if (!scalar1.IsZero())
//                idScalarDictionary.Add(id, scalar1);
//        }

//        _indexPairScalarDictionary = idScalarDictionary;

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer MapBasisVectors(Func<int, int> mappingFunction)
//    {
//        if (_indexPairScalarDictionary.Count == 0) return this;

//        var idScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (id, scalar) in _indexPairScalarDictionary)
//        {
//            var index1 = mappingFunction(id.Index1);
//            var index2 = mappingFunction(id.Index2);

//            var index = new IndexPair(index1, index2);

//            if (idScalarDictionary.TryGetValue(index, out var scalar2))
//            {
//                var scalar1 = scalar2 + scalar;

//                if (!scalar1.IsValid())
//                    throw new InvalidOperationException();

//                if (scalar1.IsZero())
//                    idScalarDictionary.Remove(index);
//                else
//                    idScalarDictionary[index] = scalar1;
//            }
//            else
//            {
//                idScalarDictionary.Add(index, scalar);
//            }
//        }

//        _indexPairScalarDictionary = idScalarDictionary;

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer MapBasisVectors(Func<int, int, IndexPair> mappingFunction)
//    {
//        if (_indexPairScalarDictionary.Count == 0) return this;

//        var idScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (id, scalar) in _indexPairScalarDictionary)
//        {
//            var index = mappingFunction(id.Index1, id.Index2);

//            if (idScalarDictionary.TryGetValue(index, out var scalar2))
//            {
//                var scalar1 = scalar2 + scalar;

//                if (!scalar1.IsValid())
//                    throw new InvalidOperationException();

//                if (scalar1.IsZero())
//                    idScalarDictionary.Remove(index);
//                else
//                    idScalarDictionary[index] = scalar1;
//            }
//            else
//            {
//                idScalarDictionary.Add(index, scalar);
//            }
//        }

//        _indexPairScalarDictionary = idScalarDictionary;

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer MapBasisVectors(Func<IndexPair, IndexPair> mappingFunction)
//    {
//        if (_indexPairScalarDictionary.Count == 0) return this;

//        var idScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (id, scalar) in _indexPairScalarDictionary)
//        {
//            var index = mappingFunction(id);

//            if (idScalarDictionary.TryGetValue(index, out var scalar2))
//            {
//                var scalar1 = scalar2 + scalar;

//                if (!scalar1.IsValid())
//                    throw new InvalidOperationException();

//                if (scalar1.IsZero())
//                    idScalarDictionary.Remove(index);
//                else
//                    idScalarDictionary[index] = scalar1;
//            }
//            else
//            {
//                idScalarDictionary.Add(index, scalar);
//            }
//        }

//        _indexPairScalarDictionary = idScalarDictionary;

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer MapBasisVectors(Func<int, double, int> mappingFunction)
//    {
//        if (_indexPairScalarDictionary.Count == 0) return this;

//        var idScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (id, scalar) in _indexPairScalarDictionary)
//        {
//            var index1 = mappingFunction(id.Index1, scalar);
//            var index2 = mappingFunction(id.Index2, scalar);

//            var index = new IndexPair(index1, index2);

//            if (idScalarDictionary.TryGetValue(index, out var scalar2))
//            {
//                var scalar1 = scalar2 + scalar;

//                if (!scalar1.IsValid())
//                    throw new InvalidOperationException();

//                if (scalar1.IsZero())
//                    idScalarDictionary.Remove(index);
//                else
//                    idScalarDictionary[index] = scalar1;
//            }
//            else
//            {
//                idScalarDictionary.Add(index, scalar);
//            }
//        }

//        _indexPairScalarDictionary = idScalarDictionary;

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer MapBasisVectors(Func<IndexPair, double, IndexPair> mappingFunction)
//    {
//        if (_indexPairScalarDictionary.Count == 0) return this;

//        var idScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (id, scalar) in _indexPairScalarDictionary)
//        {
//            var index = mappingFunction(id, scalar);

//            if (idScalarDictionary.TryGetValue(index, out var scalar2))
//            {
//                var scalar1 = scalar2 + scalar;

//                if (!scalar1.IsValid())
//                    throw new InvalidOperationException();

//                if (scalar1.IsZero())
//                    idScalarDictionary.Remove(index);
//                else
//                    idScalarDictionary[index] = scalar1;
//            }
//            else
//            {
//                idScalarDictionary.Add(index, scalar);
//            }
//        }

//        _indexPairScalarDictionary = idScalarDictionary;

//        return this;
//    }

//    public LinFloat64UnilinearMapComposer MapTerms(Func<int, int, double, KeyValuePair<IndexPair, double>> mappingFunction)
//    {
//        if (_indexPairScalarDictionary.Count == 0) return this;

//        var idScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (id, scalar) in _indexPairScalarDictionary)
//        {
//            var (termIndex, termScalar) = mappingFunction(id.Index1, id.Index2, scalar);

//            if (termScalar.IsZero())
//                continue;

//            if (idScalarDictionary.TryGetValue(termIndex, out var scalar2))
//            {
//                var scalar1 = scalar2 + termScalar;

//                if (!scalar1.IsValid())
//                    throw new InvalidOperationException();

//                if (scalar1.IsZero())
//                    idScalarDictionary.Remove(termIndex);
//                else
//                    idScalarDictionary[termIndex] = scalar1;
//            }
//            else
//            {
//                idScalarDictionary.Add(termIndex, termScalar);
//            }
//        }

//        _indexPairScalarDictionary = idScalarDictionary;

//        return this;
//    }
    
//    public LinFloat64UnilinearMapComposer MapTerms(Func<IndexPair, double, KeyValuePair<IndexPair, double>> mappingFunction)
//    {
//        if (_indexPairScalarDictionary.Count == 0) return this;

//        var idScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (id, scalar) in _indexPairScalarDictionary)
//        {
//            var (termIndex, termScalar) = mappingFunction(id, scalar);

//            if (termScalar.IsZero())
//                continue;

//            if (idScalarDictionary.TryGetValue(termIndex, out var scalar2))
//            {
//                var scalar1 = scalar2 + termScalar;

//                if (!scalar1.IsValid())
//                    throw new InvalidOperationException();

//                if (scalar1.IsZero())
//                    idScalarDictionary.Remove(termIndex);
//                else
//                    idScalarDictionary[termIndex] = scalar1;
//            }
//            else
//            {
//                idScalarDictionary.Add(termIndex, termScalar);
//            }
//        }

//        _indexPairScalarDictionary = idScalarDictionary;

//        return this;
//    }


//    
//    public LinFloat64UnilinearMapComposer Negative()
//    {
//        return MapScalars(s => -s);
//    }

//    
//    public LinFloat64UnilinearMapComposer Times(double scalarFactor)
//    {
//        return MapScalars(s => s * scalarFactor);
//    }

//    
//    public LinFloat64UnilinearMapComposer Divide(double scalarFactor)
//    {
//        return MapScalars(s => s / scalarFactor);
//    }

//    public LinFloat64UnilinearMapComposer Times(LinFloat64UnilinearMap map2)
//    {
//        var indexPairScalarDictionary = new Dictionary<IndexPair, double>();

//        foreach (var (index2, vector) in map2.GetMappedBasisVectors())
//        {
//            var vector1 = GetMappedColumnVector(vector);

//            foreach (var (index1, scalar) in vector1)
//                indexPairScalarDictionary.Add(
//                    new IndexPair(index1, index2), 
//                    scalar
//                );
//        }

//        _indexPairScalarDictionary = indexPairScalarDictionary;

//        return this;
//    }

//    
//    public LinFloat64UnilinearMapComposer Transpose()
//    {
//        return MapBasisVectors(index => index.Transpose());
//    }


//    public Float64Tuple3D GetMappedBasisVector(int index)
//    {
//        var composer = new Float64Tuple3DComposer();

//        foreach (var ((index1, index2), scalar1) in _indexPairScalarDictionary)
//        {
//            if (index2 != index || scalar1.IsZero())
//                continue;
            
//            composer.AddTerm(index1, scalar1);
//        }

//        return composer.GetVector();
//    }

//    public Float64Tuple3D GetMappedColumnVector(IReadOnlyList<double> vector)
//    {
//        var composer = new Float64Tuple3DComposer();

//        foreach (var ((index1, index2), scalar1) in _indexPairScalarDictionary)
//        {
//            if (index2 >= vector.Count || scalar1.IsZero())
//                continue;

//            var scalar2 = vector[index2];

//            if (scalar2.IsZero())
//                continue;

//            composer.AddTerm(
//                index1, 
//                scalar1 * scalar2
//            );
//        }

//        return composer.GetVector();
//    }

//    public Float64Tuple3D GetMappedColumnVector(Float64Tuple3D vector)
//    {
//        var composer = new Float64Tuple3DComposer();
        
//        foreach (var ((index1, index2), scalar1) in _indexPairScalarDictionary)
//        {
//            if (!vector.TryGetTermScalar(index2, out var scalar2))
//                continue;

//            composer.AddTerm(
//                index1, 
//                scalar1 * scalar2
//            );
//        }

//        return composer.GetVector();
//    }

//    public LinFloat64UnilinearMap GetMap()
//    {
//        return GetMapFromColumns();
//    }

//    public LinFloat64UnilinearMap GetMapFromRows()
//    {
//        if (_indexPairScalarDictionary.Count == 0)
//            return new LinFloat64UnilinearMap(
//                new EmptyDictionary<int, Float64Tuple3D>()
//            );

//        var indexVectorDictionary = new Dictionary<int, Float64Tuple3D>();

//        var group = _indexPairScalarDictionary.GroupBy(
//            p => p.Key.Item1
//        );

//        foreach (var g in group)
//        {
//            var index = g.Key;

//            var vector = g.ToDictionary(
//                p => p.Key.Item2,
//                p => p.Value
//            ).ToTuple3D();

//            indexVectorDictionary.Add(index, vector);
//        }

//        if (indexVectorDictionary.Count == 1)
//            return new LinFloat64UnilinearMap(
//                new SingleItemDictionary<int, Float64Tuple3D>(indexVectorDictionary.First())
//            );

//        return new LinFloat64UnilinearMap(
//            indexVectorDictionary
//        );
//    }

//    public LinFloat64UnilinearMap GetMapFromColumns()
//    {
//        if (_indexPairScalarDictionary.Count == 0)
//            return new LinFloat64UnilinearMap(
//                new EmptyDictionary<int, Float64Tuple3D>()
//            );

//        var indexVectorDictionary = new Dictionary<int, Float64Tuple3D>();

//        var group = _indexPairScalarDictionary.GroupBy(
//            p => p.Key.Item2
//        );

//        foreach (var g in group)
//        {
//            var index = g.Key;

//            var vector = g.ToDictionary(
//                p => p.Key.Item1,
//                p => p.Value
//            ).ToTuple3D();

//            indexVectorDictionary.Add(index, vector);
//        }

//        if (indexVectorDictionary.Count == 1)
//            return new LinFloat64UnilinearMap(
                
//                new SingleItemDictionary<int, Float64Tuple3D>(indexVectorDictionary.First())
//            );

//        return new LinFloat64UnilinearMap(
            
//            indexVectorDictionary
//        );
//    }

//    public double[,] GetMapArray(int rowCount, int colCount)
//    {
//        var mapArray =
//            new double[rowCount, colCount];

//        if (_indexPairScalarDictionary.Count == 0)
//            return mapArray;

//        foreach (var ((rowIndex, colIndex), scalar) in _indexPairScalarDictionary)
//        {
//            if (!scalar.IsValid())
//                throw new InvalidOperationException();

//            mapArray[rowIndex, colIndex] = scalar;
//        }

//        return mapArray;
//    }

//    
//    public IEnumerator<KeyValuePair<IndexPair, double>> GetEnumerator()
//    {
//        return _indexPairScalarDictionary.GetEnumerator();
//    }

//    
//    IEnumerator IEnumerable.GetEnumerator()
//    {
//        return GetEnumerator();
//    }
//}