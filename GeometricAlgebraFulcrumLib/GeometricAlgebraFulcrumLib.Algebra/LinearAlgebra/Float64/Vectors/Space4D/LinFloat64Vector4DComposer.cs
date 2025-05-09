using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

public sealed class LinFloat64Vector4DComposer :
    IQuad<Float64Scalar>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4DComposer Create()
    {
        return new LinFloat64Vector4DComposer();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4DComposer Create(double x, double y, double z, double w)
    {
        return new LinFloat64Vector4DComposer(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector4DComposer Create(IQuad<Float64Scalar> vector)
    {
        return new LinFloat64Vector4DComposer(
            vector.Item1,
            vector.Item2,
            vector.Item3,
            vector.Item4
        );
    }


    public Float64Scalar X { get; set; }

    public Float64Scalar Y { get; set; }

    public Float64Scalar Z { get; set; }

    public Float64Scalar W { get; set; }

    public int VSpaceDimensions
        => 4;

    public Float64Scalar Item1
        => X;

    public Float64Scalar Item2
        => Y;

    public Float64Scalar Item3
        => Z;

    public Float64Scalar Item4
        => W;

    public bool IsZero
        => X.IsZero() && Y.IsZero() && Z.IsZero() && W.IsZero();

    public double this[int index]
    {
        get => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            3 => W,
            _ => 0
        };
        set
        {
            Debug.Assert(value.IsValid());

            if (index == 0)
                X = value;
            else if (index == 1)
                Y = value;
            else if (index == 2)
                Z = value;
            else if (index == 3)
                W = value;
            else
                throw new IndexOutOfRangeException();
        }
    }

    public IEnumerable<KeyValuePair<int, double>> IndexScalarPairs
    {
        get
        {
            if (!X.IsZero())
                yield return new KeyValuePair<int, double>(0, X);

            if (!Y.IsZero())
                yield return new KeyValuePair<int, double>(1, Y);

            if (!Z.IsZero())
                yield return new KeyValuePair<int, double>(2, Z);

            if (!W.IsZero())
                yield return new KeyValuePair<int, double>(3, W);
        }
    }

    public IEnumerable<KeyValuePair<LinBasisVector, double>> BasisBladeScalarPairs
        => IndexScalarPairs.Select(p =>
            new KeyValuePair<LinBasisVector, double>(
                p.Key.ToLinBasisVector(),
                p.Value
            )
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinFloat64Vector4DComposer()
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinFloat64Vector4DComposer(double x, double y, double z, double w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return X.IsValid() && Y.IsValid() && Z.IsValid() && W.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer Clear()
    {
        X = 0;
        Y = 0;
        Z = 0;
        W = 0;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer ClearTerm(int index)
    {
        if (index == 0)
            X = 0;
        else if (index == 1)
            Y = 0;
        else if (index == 2)
            Z = 0;
        else if (index == 3)
            W = 0;
        else
            throw new IndexOutOfRangeException();

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetTermScalarValue(int basisBlade)
    {
        return basisBlade switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            3 => W,
            _ => 0
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer RemoveTerm(int basisBlade)
    {
        this[basisBlade] = 0;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer SetTerm(int basisBlade, double scalar)
    {
        this[basisBlade] = scalar;

        return this;
    }

    public LinFloat64Vector4DComposer SetVector(IQuad<Float64Scalar> vector)
    {
        X = vector.Item1;
        Y = vector.Item2;
        Z = vector.Item3;
        W = vector.Item4;

        return this;
    }

    public LinFloat64Vector4DComposer SetVectorNegative(IQuad<Float64Scalar> vector)
    {
        X = -vector.Item1;
        Y = -vector.Item2;
        Z = -vector.Item3;
        W = -vector.Item4;

        return this;
    }

    public LinFloat64Vector4DComposer SetVector(IQuad<Float64Scalar> vector, double scalingFactor)
    {
        X = scalingFactor * vector.Item1;
        Y = scalingFactor * vector.Item2;
        Z = scalingFactor * vector.Item3;
        W = scalingFactor * vector.Item4;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer AddTerm(int basisBlade, double scalar)
    {
        this[basisBlade] += scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer AddVector(IQuad<Float64Scalar> vector)
    {
        X += vector.Item1;
        Y += vector.Item2;
        Z += vector.Item3;
        W += vector.Item4;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer AddVector(IQuad<Float64Scalar> vector, double scalingFactor)
    {
        X += scalingFactor * vector.Item1;
        Y += scalingFactor * vector.Item2;
        Z += scalingFactor * vector.Item3;
        W += scalingFactor * vector.Item4;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer SubtractTerm(int basisBlade, double scalar)
    {
        this[basisBlade] -= scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer SubtractVector(IQuad<Float64Scalar> vector)
    {
        X -= vector.Item1;
        Y -= vector.Item2;
        Z -= vector.Item3;
        W -= vector.Item4;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer SubtractVector(IQuad<Float64Scalar> vector, double scalingFactor)
    {
        X -= scalingFactor * vector.Item1;
        Y -= scalingFactor * vector.Item2;
        Z -= scalingFactor * vector.Item3;
        W -= scalingFactor * vector.Item4;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer SetTerm(LinSignedBasisVector basisBlade)
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
    public LinFloat64Vector4DComposer AddTerm(int basisBlade, double scalar1, double scalar2)
    {
        this[basisBlade] += scalar1 * scalar2;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer SubtractTerm(int basisBlade, double scalar1, double scalar2)
    {
        this[basisBlade] -= scalar1 * scalar2;

        return this;
    }


    public LinFloat64Vector4DComposer MapScalars(Func<double, double> mappingFunction)
    {
        X = mappingFunction(X);
        Y = mappingFunction(Y);
        Z = mappingFunction(Z);
        W = mappingFunction(W);

        return this;
    }

    public LinFloat64Vector4DComposer MapScalars(Func<int, double, double> mappingFunction)
    {
        X = mappingFunction(0, X);
        Y = mappingFunction(1, Y);
        Z = mappingFunction(2, Z);
        W = mappingFunction(2, W);

        return this;
    }

    //public Float64Tuple4DComposer MapBasisVectors(Func<int, int> mappingFunction)
    //{
    //    if (_indexScalarDictionary.Count == 0) return this;

    //    var idScalarDictionary = new Dictionary<int, double>();

    //    foreach (var (id, scalar) in _indexScalarDictionary)
    //    {
    //        var id1 = mappingFunction(id);

    //        if (idScalarDictionary.TryGetValue(id1, out var scalar2))
    //        {
    //            var scalar1 = scalar2 + scalar;

    //            if (!scalar1.IsValid())
    //                throw new InvalidOperationException();

    //            if (scalar1.IsZero())
    //                idScalarDictionary.Remove(id1);
    //            else
    //                idScalarDictionary[id1] = scalar1;
    //        }
    //        else
    //        {
    //            idScalarDictionary.Add(id1, scalar);
    //        }
    //    }

    //    _indexScalarDictionary = idScalarDictionary;

    //    return this;
    //}

    //public Float64Tuple4DComposer MapBasisVectors(Func<int, double, int> mappingFunction)
    //{
    //    if (_indexScalarDictionary.Count == 0) return this;

    //    var idScalarDictionary = new Dictionary<int, double>();

    //    foreach (var (id, scalar) in _indexScalarDictionary)
    //    {
    //        var id1 = mappingFunction(id, scalar);

    //        if (idScalarDictionary.TryGetValue(id1, out var scalar2))
    //        {
    //            var scalar1 = scalar2 + scalar;

    //            if (!scalar1.IsValid())
    //                throw new InvalidOperationException();

    //            if (scalar1.IsZero())
    //                idScalarDictionary.Remove(id1);
    //            else
    //                idScalarDictionary[id1] = scalar1;
    //        }
    //        else
    //        {
    //            idScalarDictionary.Add(id1, scalar);
    //        }
    //    }

    //    _indexScalarDictionary = idScalarDictionary;

    //    return this;
    //}

    //public Float64Tuple4DComposer MapTerms(Func<int, double, KeyValuePair<int, double>> mappingFunction)
    //{
    //    if (_indexScalarDictionary.Count == 0) return this;

    //    var idScalarDictionary = new Dictionary<int, double>();

    //    foreach (var (id, scalar) in _indexScalarDictionary)
    //    {
    //        var term1 = mappingFunction(id, scalar);

    //        if (term1.Value.IsZero())
    //            continue;

    //        if (idScalarDictionary.TryGetValue(term1.Key, out var scalar2))
    //        {
    //            var scalar1 = scalar2 + term1.Value;

    //            if (!scalar1.IsValid())
    //                throw new InvalidOperationException();

    //            if (scalar1.IsZero())
    //                idScalarDictionary.Remove(term1.Key);
    //            else
    //                idScalarDictionary[term1.Key] = scalar1;
    //        }
    //        else
    //        {
    //            idScalarDictionary.Add(term1.Key, term1.Value);
    //        }
    //    }

    //    _indexScalarDictionary = idScalarDictionary;

    //    return this;
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer Negative()
    {
        X = -X;
        Y = -Y;
        Z = -Z;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer Times(double scalarFactor)
    {
        X = scalarFactor * X;
        Y = scalarFactor * Y;
        Z = scalarFactor * Z;
        W = scalarFactor * W;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4DComposer Divide(double scalarFactor)
    {
        scalarFactor = 1d / scalarFactor;

        X = scalarFactor * X;
        Y = scalarFactor * Y;
        Z = scalarFactor * Z;
        W = scalarFactor * W;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ENormSquared()
    {
        return X * X + Y * Y + Z * Z + W * W;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ENorm()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D GetVector()
    {
        return LinFloat64Vector4D.Create(X, Y, Z, W);
    }

}