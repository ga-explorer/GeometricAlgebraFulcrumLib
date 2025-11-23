using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public sealed class LinFloat64Vector2DComposer :
    ILinFloat64Vector2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2DComposer Create()
    {
        return new LinFloat64Vector2DComposer();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2DComposer Create(double x, double y)
    {
        return new LinFloat64Vector2DComposer(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2DComposer Create(IPair<double> vector)
    {
        return new LinFloat64Vector2DComposer(
            vector.Item1,
            vector.Item2
        );
    }


    public double X { get; set; }

    public double Y { get; set; }

    public int VSpaceDimensions
        => 3;

    public double Item1
        => X;

    public double Item2
        => Y;

    public double this[int index]
    {
        get => index switch
        {
            0 => X,
            1 => Y,
            _ => 0
        };
        set
        {
            Debug.Assert(value.IsValid());

            if (index == 0)
                X = value;
            else if (index == 1)
                Y = value;
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
    private LinFloat64Vector2DComposer()
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Vector2DComposer(double x, double y)
    {
        X = x;
        Y = y;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return X.IsValid() && Y.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return X.IsZero() && Y.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer Clear()
    {
        X = Float64Scalar.Zero;
        Y = Float64Scalar.Zero;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer ClearTerm(int index)
    {
        if (index == 0)
            X = Float64Scalar.Zero;
        else if (index == 1)
            Y = Float64Scalar.Zero;
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
            _ => 0
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer RemoveTerm(int basisBlade)
    {
        this[basisBlade] = 0;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer SetTerm(int basisBlade, double scalar)
    {
        this[basisBlade] = scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer SetTerm(LinBasisVector axis)
    {
        if (axis == LinBasisVector.Px)
            X = 1;

        else if (axis == LinBasisVector.Py)
            Y = 1;

        else if (axis == LinBasisVector.Nx)
            X = -1;

        else if (axis == LinBasisVector.Ny)
            Y = -1;

        else
            throw new ArgumentOutOfRangeException(nameof(axis));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer SetTerm(LinBasisVector axis, double scalar)
    {
        if (axis == LinBasisVector.Px)
            X = scalar;

        else if (axis == LinBasisVector.Py)
            Y = scalar;

        else if (axis == LinBasisVector.Nx)
            X = -scalar;

        else if (axis == LinBasisVector.Ny)
            Y = -scalar;

        else
            throw new ArgumentOutOfRangeException(nameof(axis));

        return this;
    }

    public LinFloat64Vector2DComposer SetVector(double x, double y)
    {
        X = x;
        Y = y;

        return this;
    }

    public LinFloat64Vector2DComposer SetVector(IPair<double> vector)
    {
        X = vector.Item1;
        Y = vector.Item2;

        return this;
    }

    public LinFloat64Vector2DComposer SetVectorNegative(IPair<double> vector)
    {
        X = -vector.Item1;
        Y = -vector.Item2;

        return this;
    }

    public LinFloat64Vector2DComposer SetVector(IPair<double> vector, double scalingFactor)
    {
        X = scalingFactor * vector.Item1;
        Y = scalingFactor * vector.Item2;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer AddTerm(int basisBlade, double scalar)
    {
        this[basisBlade] += scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer AddTerm(LinBasisVector axis)
    {
        if (axis == LinBasisVector.Px)
            X += 1;

        else if (axis == LinBasisVector.Py)
            Y += 1;

        else if (axis == LinBasisVector.Nx)
            X -= 1;

        else if (axis == LinBasisVector.Ny)
            Y -= 1;

        else
            throw new ArgumentOutOfRangeException(nameof(axis));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer AddTerm(LinBasisVector axis, double scalar)
    {
        if (axis == LinBasisVector.Px)
            X += scalar;

        else if (axis == LinBasisVector.Py)
            Y += scalar;

        else if (axis == LinBasisVector.Nx)
            X -= scalar;

        else if (axis == LinBasisVector.Ny)
            Y -= scalar;

        else
            throw new ArgumentOutOfRangeException(nameof(axis));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer AddVector(IPair<double> vector)
    {
        X += vector.Item1;
        Y += vector.Item2;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer AddVector(IPair<double> vector, double scalingFactor)
    {
        X += scalingFactor * vector.Item1;
        Y += scalingFactor * vector.Item2;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer SubtractTerm(int basisBlade, double scalar)
    {
        this[basisBlade] -= scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer SubtractTerm(LinBasisVector axis)
    {
        if (axis == LinBasisVector.Px)
            X -= 1;

        else if (axis == LinBasisVector.Py)
            Y -= 1;

        else if (axis == LinBasisVector.Nx)
            X += 1;

        else if (axis == LinBasisVector.Ny)
            Y += 1;

        else
            throw new ArgumentOutOfRangeException(nameof(axis));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer SubtractTerm(LinBasisVector axis, double scalar)
    {
        if (axis == LinBasisVector.Px)
            X -= scalar;

        else if (axis == LinBasisVector.Py)
            Y -= scalar;

        else if (axis == LinBasisVector.Nx)
            X += scalar;

        else if (axis == LinBasisVector.Ny)
            Y += scalar;

        else
            throw new ArgumentOutOfRangeException(nameof(axis));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer SubtractVector(IPair<double> vector)
    {
        X -= vector.Item1;
        Y -= vector.Item2;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer SubtractVector(IPair<double> vector, double scalingFactor)
    {
        X -= scalingFactor * vector.Item1;
        Y -= scalingFactor * vector.Item2;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer AddTerm(int basisBlade, double scalar1, double scalar2)
    {
        this[basisBlade] += scalar1 * scalar2;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer SubtractTerm(int basisBlade, double scalar1, double scalar2)
    {
        this[basisBlade] -= scalar1 * scalar2;

        return this;
    }


    public LinFloat64Vector2DComposer MapScalars(Func<double, double> mappingFunction)
    {
        X = mappingFunction(X);
        Y = mappingFunction(Y);

        return this;
    }

    public LinFloat64Vector2DComposer MapScalars(Func<int, double, double> mappingFunction)
    {
        X = mappingFunction(0, X);
        Y = mappingFunction(1, Y);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer Negative()
    {
        X = -X;
        Y = -Y;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer Times(double scalarFactor)
    {
        X *= scalarFactor;
        Y *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer Divide(double scalarFactor)
    {
        scalarFactor = 1d / scalarFactor;

        X *= scalarFactor;
        Y *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2DComposer DivideByENorm()
    {
        var scalarFactor = 1d / ENorm();

        X *= scalarFactor;
        Y *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ENormSquared()
    {
        return X * X + Y * Y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ENorm()
    {
        return Math.Sqrt(X * X + Y * Y);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GetVector()
    {
        return LinFloat64Vector2D.Create(X, Y);
    }

}