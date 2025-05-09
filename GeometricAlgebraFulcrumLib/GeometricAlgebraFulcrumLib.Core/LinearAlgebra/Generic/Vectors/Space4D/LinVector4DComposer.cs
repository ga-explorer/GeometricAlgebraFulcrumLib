using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space4D;

public sealed class LinVector4DComposer<T> :
    ILinVector4D<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4DComposer<T> Create(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector4DComposer<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4DComposer<T> Create(Scalar<T> x, Scalar<T> y, Scalar<T> z, Scalar<T> w)
    {
        return new LinVector4DComposer<T>(x, y, z, w);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector4DComposer<T> Create(IQuad<Scalar<T>> vector)
    {
        return new LinVector4DComposer<T>(
            vector.Item1,
            vector.Item2,
            vector.Item3,
            vector.Item4
        );
    }


    public IScalarProcessor<T> ScalarProcessor 
        => X.ScalarProcessor;

    public Scalar<T> X { get; set; }

    public Scalar<T> Y { get; set; }

    public Scalar<T> Z { get; set; }

    public Scalar<T> W { get; set; }

    public int VSpaceDimensions
        => 4;

    public Scalar<T> Item1
        => X;

    public Scalar<T> Item2
        => Y;

    public Scalar<T> Item3
        => Z;

    public Scalar<T> Item4
        => W;

    public bool IsZero
        => X.IsZero() && Y.IsZero() && Z.IsZero() && W.IsZero();

    public Scalar<T> this[int index]
    {
        get => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            3 => W,
            _ => ScalarProcessor.Zero
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

    public IEnumerable<KeyValuePair<int, Scalar<T>>> IndexScalarPairs
    {
        get
        {
            if (!X.IsZero())
                yield return new KeyValuePair<int, Scalar<T>>(0, X);

            if (!Y.IsZero())
                yield return new KeyValuePair<int, Scalar<T>>(1, Y);

            if (!Z.IsZero())
                yield return new KeyValuePair<int, Scalar<T>>(2, Z);

            if (!W.IsZero())
                yield return new KeyValuePair<int, Scalar<T>>(3, W);
        }
    }

    public IEnumerable<KeyValuePair<LinBasisVector, Scalar<T>>> BasisBladeScalarPairs
        => IndexScalarPairs.Select(p =>
            new KeyValuePair<LinBasisVector, Scalar<T>>(
                p.Key.ToLinBasisVector(),
                p.Value
            )
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinVector4DComposer(IScalarProcessor<T> scalarProcessor)
    {
        var scalarZero = scalarProcessor.Zero;

        X = scalarZero;
        Y = scalarZero;
        Z = scalarZero;
        W = scalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal LinVector4DComposer(Scalar<T> x, Scalar<T> y, Scalar<T> z, Scalar<T> w)
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
    public LinVector4DComposer<T> Clear()
    {
        var scalarZero = ScalarProcessor.Zero;

        X = scalarZero;
        Y = scalarZero;
        Z = scalarZero;
        W = scalarZero;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> ClearTerm(int index)
    {
        if (index == 0)
            X = ScalarProcessor.Zero;
        else if (index == 1)
            Y = ScalarProcessor.Zero;
        else if (index == 2)
            Z = ScalarProcessor.Zero;
        else if (index == 3)
            W = ScalarProcessor.Zero;
        else
            throw new IndexOutOfRangeException();

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetTermScalarValue(int basisBlade)
    {
        return basisBlade switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            3 => W,
            _ => ScalarProcessor.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> RemoveTerm(int basisBlade)
    {
        this[basisBlade] = ScalarProcessor.Zero;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> SetTerm(int basisBlade, Scalar<T> scalar)
    {
        this[basisBlade] = scalar;

        return this;
    }

    public LinVector4DComposer<T> SetVector(IQuad<Scalar<T>> vector)
    {
        X = vector.Item1;
        Y = vector.Item2;
        Z = vector.Item3;
        W = vector.Item4;

        return this;
    }

    public LinVector4DComposer<T> SetVectorNegative(IQuad<Scalar<T>> vector)
    {
        X = -vector.Item1;
        Y = -vector.Item2;
        Z = -vector.Item3;
        W = -vector.Item4;

        return this;
    }

    public LinVector4DComposer<T> SetVector(IQuad<Scalar<T>> vector, Scalar<T> scalingFactor)
    {
        X = scalingFactor * vector.Item1;
        Y = scalingFactor * vector.Item2;
        Z = scalingFactor * vector.Item3;
        W = scalingFactor * vector.Item4;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> AddTerm(int basisBlade, Scalar<T> scalar)
    {
        this[basisBlade] += scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> AddVector(IQuad<Scalar<T>> vector)
    {
        X += vector.Item1;
        Y += vector.Item2;
        Z += vector.Item3;
        W += vector.Item4;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> AddVector(IQuad<Scalar<T>> vector, Scalar<T> scalingFactor)
    {
        X += scalingFactor * vector.Item1;
        Y += scalingFactor * vector.Item2;
        Z += scalingFactor * vector.Item3;
        W += scalingFactor * vector.Item4;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> SubtractTerm(int basisBlade, Scalar<T> scalar)
    {
        this[basisBlade] -= scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> SubtractVector(IQuad<Scalar<T>> vector)
    {
        X -= vector.Item1;
        Y -= vector.Item2;
        Z -= vector.Item3;
        W -= vector.Item4;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> SubtractVector(IQuad<Scalar<T>> vector, Scalar<T> scalingFactor)
    {
        X -= scalingFactor * vector.Item1;
        Y -= scalingFactor * vector.Item2;
        Z -= scalingFactor * vector.Item3;
        W -= scalingFactor * vector.Item4;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> SetTerm(LinSignedBasisVector basisBlade)
    {
        if (basisBlade.IsZero)
            return RemoveTerm(basisBlade.Index);

        var scalar = basisBlade.IsPositive
            ? ScalarProcessor.One 
            : ScalarProcessor.MinusOne;

        return SetTerm(
            basisBlade.Index,
            scalar
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> AddTerm(int basisBlade, Scalar<T> scalar1, Scalar<T> scalar2)
    {
        this[basisBlade] += scalar1 * scalar2;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> SubtractTerm(int basisBlade, Scalar<T> scalar1, Scalar<T> scalar2)
    {
        this[basisBlade] -= scalar1 * scalar2;

        return this;
    }


    public LinVector4DComposer<T> MapScalars(Func<Scalar<T>, Scalar<T>> mappingFunction)
    {
        X = mappingFunction(X);
        Y = mappingFunction(Y);
        Z = mappingFunction(Z);
        W = mappingFunction(W);

        return this;
    }

    public LinVector4DComposer<T> MapScalars(Func<int, Scalar<T>, Scalar<T>> mappingFunction)
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

    //    var idScalarDictionary = new Dictionary<int, Scalar<T>>();

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

    //public Float64Tuple4DComposer MapBasisVectors(Func<int, Scalar<T>, int> mappingFunction)
    //{
    //    if (_indexScalarDictionary.Count == 0) return this;

    //    var idScalarDictionary = new Dictionary<int, Scalar<T>>();

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

    //public Float64Tuple4DComposer MapTerms(Func<int, Scalar<T>, KeyValuePair<int, Scalar<T>>> mappingFunction)
    //{
    //    if (_indexScalarDictionary.Count == 0) return this;

    //    var idScalarDictionary = new Dictionary<int, Scalar<T>>();

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
    public LinVector4DComposer<T> Negative()
    {
        X = -X;
        Y = -Y;
        Z = -Z;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> Times(Scalar<T> scalarFactor)
    {
        X = scalarFactor * X;
        Y = scalarFactor * Y;
        Z = scalarFactor * Z;
        W = scalarFactor * W;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4DComposer<T> Divide(Scalar<T> scalarFactor)
    {
        scalarFactor = 1d / scalarFactor;

        X = scalarFactor * X;
        Y = scalarFactor * Y;
        Z = scalarFactor * Z;
        W = scalarFactor * W;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENormSquared()
    {
        return X * X + Y * Y + Z * Z + W * W;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENorm()
    {
        return (X * X + Y * Y + Z * Z + W * W).Sqrt();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4D<T> GetVector()
    {
        return LinVector4D<T>.Create(X, Y, Z, W);
    }

}