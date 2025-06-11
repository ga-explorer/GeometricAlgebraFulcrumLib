using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.LinearMaps;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;

public sealed class LinVector<T> :
    IReadOnlyDictionary<int, T>
{
    public static LinVector<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector<T>(scalarProcessor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator -(LinVector<T> mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator +(LinVector<T> mv1, LinVector<T> mv2)
    {
        return mv1.Add(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator -(LinVector<T> mv1, LinVector<T> mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(LinVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.ScalarProcessor.CreateZeroLinVector();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(IntegerSign mv1, LinVector<T> mv2)
    {
        if (mv1.IsZero)
            return mv2.ScalarProcessor.CreateZeroLinVector();

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(LinVector<T> mv1, int mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(int mv1, LinVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(LinVector<T> mv1, uint mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(uint mv1, LinVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(LinVector<T> mv1, long mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(long mv1, LinVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(LinVector<T> mv1, ulong mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(ulong mv1, LinVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(LinVector<T> mv1, float mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(float mv1, LinVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(LinVector<T> mv1, double mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(double mv1, LinVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(LinVector<T> mv1, T mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(T mv1, LinVector<T> mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(LinVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator *(Scalar<T> mv1, LinVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator /(LinVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator /(LinVector<T> mv1, int mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator /(LinVector<T> mv1, uint mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator /(LinVector<T> mv1, long mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator /(LinVector<T> mv1, ulong mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator /(LinVector<T> mv1, float mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator /(LinVector<T> mv1, double mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator /(LinVector<T> mv1, T mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector<T> operator /(LinVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }


    private readonly IReadOnlyDictionary<int, T> _indexScalarDictionary;


    public string VectorClassName
        => "Generic Linear Vector";

    public IScalarProcessor<T> ScalarProcessor { get; }

    /// <summary>
    /// The dimensions of the base vector space, dynamically determined from
    /// stored terms
    /// </summary>
    public int VSpaceDimensions
        => IsZero ? 0 : Indices.Max(id => id) + 1;

    public int Count
        => _indexScalarDictionary.Count;

    public bool IsZero
        => _indexScalarDictionary.Count == 0;

    public T this[int index]
        => GetTermScalar(index).ScalarValue;

    public IEnumerable<int> Indices
        => _indexScalarDictionary.Keys;

    public IEnumerable<T> Scalars
        => _indexScalarDictionary.Values;

    public IEnumerable<KeyValuePair<int, T>> IndexScalarPairs
        => _indexScalarDictionary;

    public IEnumerable<int> Keys
        => _indexScalarDictionary.Keys;

    public IEnumerable<T> Values
        => _indexScalarDictionary.Values;

    public IEnumerable<KeyValuePair<LinBasisVector, T>> BasisScalarPairs
    {
        get
        {
            return _indexScalarDictionary.Select(p =>
                new KeyValuePair<LinBasisVector, T>(
                    p.Key.ToLinBasisVector(),
                    p.Value
                )
            );
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinVector(IScalarProcessor<T> scalarProcessor)
    {
        _indexScalarDictionary = new EmptyDictionary<int, T>();

        ScalarProcessor = scalarProcessor;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinVector(IScalarProcessor<T> scalarProcessor, KeyValuePair<int, T> basisScalarPair)
    {
        _indexScalarDictionary =
            new SingleItemDictionary<int, T>(basisScalarPair);

        ScalarProcessor = scalarProcessor;

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinVector(IScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<int, T> idScalarDictionary)
    {
        _indexScalarDictionary = idScalarDictionary;

        ScalarProcessor = scalarProcessor;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return _indexScalarDictionary.IsValidLinVectorDictionary(ScalarProcessor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsKey(int key)
    {
        return !IsZero && _indexScalarDictionary.ContainsKey(key);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetValue(int key, out T value)
    {
        return _indexScalarDictionary.TryGetValue(key, out value);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyDictionary<int, T> GetIndexScalarDictionary()
    {
        return _indexScalarDictionary;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinVectorTerm<T>> GetTerms()
    {
        return _indexScalarDictionary.Select(p =>
            ScalarProcessor.CreateLinTerm(p.Key, p.Value)
        );
    }

    public IEnumerable<LinBasisVector> BasisVectors
        => _indexScalarDictionary.Keys.Select(index => index.ToLinBasisVector());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetTermScalar(int basisVectorIndex)
    {
        return _indexScalarDictionary.TryGetValue(basisVectorIndex, out var scalar)
            ? ScalarProcessor.ScalarFromValue(scalar)
            : ScalarProcessor.Zero;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetTermScalar(int basisVectorIndex, out T scalar)
    {
        if (_indexScalarDictionary.TryGetValue(basisVectorIndex, out scalar))
            return true;

        scalar = ScalarProcessor.ZeroValue;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> GetSubVector(int vSpaceDimensions)
    {
        return _indexScalarDictionary
            .Where(p => p.Key < vSpaceDimensions)
            .ToDictionary()
            .CreateLinVector(ScalarProcessor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> Negative()
    {
        return IsZero
            ? ScalarProcessor.CreateZeroLinVector()
            : MapScalars(s => ScalarProcessor.Negative(s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> Times(T scalar)
    {
        if (IsZero || ScalarProcessor.IsOne(scalar))
            return this;

        return ScalarProcessor.IsZero(scalar)
            ? ScalarProcessor.CreateZeroLinVector()
            : MapScalars(s => ScalarProcessor.Times(s, scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> Divide(T scalar)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> DivideByENormSquared()
    {
        var scalar = ENormSquared().ScalarValue;

        return MapScalars(s => ScalarProcessor.Divide(s, scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> DivideByENorm()
    {
        var scalar = ENorm().ScalarValue;

        return MapScalars(s => ScalarProcessor.Divide(s, scalar).ScalarValue);
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
    public LinVector<T> Add(LinVectorTerm<T> mv2)
    {
        if (IsZero)
            return mv2.ToVector();

        if (mv2.IsZero)
            return this;

        return ScalarProcessor
            .CreateLinVectorComposer()
            .SetVector(this)
            .AddTerm(mv2)
            .GetVector();
    }

    public LinVector<T> Add(LinVector<T> mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return ScalarProcessor
            .CreateLinVectorComposer()
            .SetVector(this)
            .AddVector(mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> Subtract(LinVectorTerm<T> mv2)
    {
        if (IsZero)
            return mv2.Negative().ToVector();

        if (mv2.IsZero)
            return this;

        return ScalarProcessor
            .CreateLinVectorComposer()
            .SetVector(this)
            .SubtractTerm(mv2)
            .GetVector();
    }

    public LinVector<T> Subtract(LinVector<T> mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return ScalarProcessor
            .CreateLinVectorComposer()
            .SetVector(this)
            .SubtractVector(mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ESp(LinVectorTerm<T> mv2)
    {
        var scalar2 = mv2.ScalarValue;

        return TryGetTermScalar(mv2.BasisVector.Index, out var scalar1)
            ? ScalarProcessor.Times(scalar1, scalar2)
            : ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ESp(LinVector<T> mv2)
    {
        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetScalar();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ToXGaVector(XGaProcessor<T> processor)
    {
        var idScalarDictionary =
            GetIndexScalarDictionary().ToDictionary(
                p => p.Key.ToUnitIndexSet(),
                p => p.Value
            );

        return processor.Vector(idScalarDictionary);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetAngleCos(LinVector<T> vector2)
    {
        var uuDot = ENormSquared();
        var vvDot = vector2.ENormSquared();
        var uvDot = ESp(vector2);

        var norm = (uuDot * vvDot).Sqrt();

        return norm.IsZero()
            ? ScalarProcessor.Zero
            : uvDot / norm;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetAngleCosWithUnit(LinVector<T> vector2)
    {
        //Debug.Assert(
        //    vector2.IsNearUnit()
        //);

        var uuDot = ENormSquared();
        var uvDot = ESp(vector2);

        var norm = uuDot.Sqrt();

        return norm.IsZero()
            ? ScalarProcessor.Zero
            : uvDot / norm;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetUnitVectorsAngleCos(LinVector<T> vector2)
    {
        return ESp(vector2);
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public LinPolarAngle<T> GetAngleWithUnit(LinBasisVector vector2)
    //{
    //    return vector1.GetAngleCosWithUnit(vector2).ArcCos();
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> GetAngleWithUnit(LinVector<T> vector2)
    {
        return GetAngleCosWithUnit(vector2).ArcCos();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> GetUnitVectorsAngle(LinVector<T> vector2)
    {
        return GetUnitVectorsAngleCos(vector2).ArcCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> GetAngle(LinVector<T> vector2, bool assumeUnitVectors)
    {
        var v12Sp = ESp(vector2);

        var angle = assumeUnitVectors
            ? v12Sp
            : v12Sp / (ENormSquared() * vector2.ENormSquared()).Sqrt();

        return angle.ArcCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> GetAngle(LinVector<T> vector2)
    {
        return GetAngleCos(vector2).ArcCos();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinUnilinearMap<T> ToDiagonalLinUnilinearMap()
    {
        var scalarProcessor = ScalarProcessor;

        var indexVectorDictionary =
            this.ToDictionary(
                p => p.Key,
                p => scalarProcessor.CreateLinVector(p.Key, p.Value)
            );

        return scalarProcessor.CreateLinUnilinearMap(indexVectorDictionary);
    }

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Scalar<T> GetAngleCosWithUnit(LinBasisVector vector2)
    //{
    //    Debug.Assert(
    //        vector2.Sign.IsNotZero
    //    );

    //    var uuDot = ENormSquared();
    //    var uvDot = ESp(vector2);

    //    var norm = uuDot.Sqrt();

    //    return norm.IsZero()
    //        ? ScalarProcessor.Zero
    //        : uvDot / norm;
    //}
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> ToComposer()
    {
        return new LinVectorComposer<T>(ScalarProcessor).SetVector(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> NegativeToComposer()
    {
        return new LinVectorComposer<T>(ScalarProcessor).SetVectorNegative(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorComposer<T> ToComposer(T scalingFactor)
    {
        return new LinVectorComposer<T>(ScalarProcessor).SetVector(this, scalingFactor);
    }
    
    public T[] ToArray(int vSpaceDimensions)
    {
        if (vSpaceDimensions < VSpaceDimensions)
            throw new ArgumentException(nameof(vSpaceDimensions));

        var array = ScalarProcessor.CreateArrayZero1D(vSpaceDimensions);

        foreach (var (index, scalar) in IndexScalarPairs)
            array[index] = scalar;

        return array;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> EInverse()
    {
        return Divide(
            ESp(this).ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> MapScalars(Func<T, T> scalarMapping)
    {
        var termList =
            IndexScalarPairs.Select(
                term => new KeyValuePair<int, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return ScalarProcessor
            .CreateLinVectorComposer()
            .SetTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> MapScalars(Func<int, T, T> scalarMapping)
    {
        var termList =
            IndexScalarPairs.Select(
                term => new KeyValuePair<int, T>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return ScalarProcessor
            .CreateLinVectorComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> MapBasisVectors(Func<int, int> basisMapping, bool simplify = true)
    {
        var termList =
            IndexScalarPairs.Select(
                term => new KeyValuePair<int, T>(
                    basisMapping(term.Key),
                    term.Value
                )
            );

        return ScalarProcessor
            .CreateLinVectorComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> MapBasisVectors(Func<int, T, int> basisMapping, bool simplify = true)
    {
        var termList =
            IndexScalarPairs.Select(
                term => new KeyValuePair<int, T>(
                    basisMapping(term.Key, term.Value),
                    term.Value
                )
            );

        return ScalarProcessor
            .CreateLinVectorComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> MapTerms(Func<int, T, KeyValuePair<int, T>> termMapping, bool simplify = true)
    {
        var termList =
            IndexScalarPairs.Select(
                term => termMapping(term.Key, term.Value)
            );

        return ScalarProcessor
            .CreateLinVectorComposer()
            .AddTerms(termList)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinAngle<T> GetEuclideanAngle(LinVector<T> v2, bool assumeUnitVectors = false)
    {
        var v12Sp = ESp(v2);

        var angleCos = assumeUnitVectors
            ? v12Sp
            : v12Sp / (ENormSquared() * v2.ENormSquared()).Sqrt();

        return angleCos.ArcCos();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> GetBisector(LinVector<T> v2, bool assumeEqualNormVectors = false)
    {
        return (this + v2).Divide(ScalarProcessor.TwoValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector<T> GetUnitBisector(LinVector<T> v2, bool assumeEqualNormVectors = false)
    {
        var bisector = assumeEqualNormVectors
            ? this + v2
            : DivideByENorm() + v2.DivideByENorm();

        return bisector.DivideByENorm();
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetArrayText()
    {
        return this.ToArray().GetArrayText();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<KeyValuePair<int, T>> GetEnumerator()
    {
        return _indexScalarDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return BasisScalarPairs
            .OrderBy(p => p.Key)
            .Select(p => $"'{p.Value:G}'{p.Key}")
            .Concatenate(" + ");
    }

}