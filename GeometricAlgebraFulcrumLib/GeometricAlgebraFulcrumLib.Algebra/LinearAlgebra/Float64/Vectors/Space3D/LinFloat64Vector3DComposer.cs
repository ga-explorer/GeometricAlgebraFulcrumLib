using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public sealed class LinFloat64Vector3DComposer :
    ILinFloat64Vector3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3DComposer Create()
    {
        return new LinFloat64Vector3DComposer();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3DComposer Create(double x, double y, double z)
    {
        return new LinFloat64Vector3DComposer(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3DComposer Create(ITriplet<Float64Scalar> vector)
    {
        return new LinFloat64Vector3DComposer(
            vector.Item1,
            vector.Item2,
            vector.Item3
        );
    }


    public Float64Scalar X { get; set; }

    public Float64Scalar Y { get; set; }

    public Float64Scalar Z { get; set; }

    public int VSpaceDimensions
        => 3;

    public Float64Scalar Item1
        => X;

    public Float64Scalar Item2
        => Y;

    public Float64Scalar Item3
        => Z;

    public double this[int index]
    {
        get => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
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
    private LinFloat64Vector3DComposer()
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Vector3DComposer(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return X.IsValid() && Y.IsValid() && Z.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return X.IsZero() && Y.IsZero() && Z.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer Clear()
    {
        X = Float64Scalar.Zero;
        Y = Float64Scalar.Zero;
        Z = Float64Scalar.Zero;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer ClearTerm(int index)
    {
        if (index == 0)
            X = Float64Scalar.Zero;
        else if (index == 1)
            Y = Float64Scalar.Zero;
        else if (index == 2)
            Z = Float64Scalar.Zero;
        else
            throw new IndexOutOfRangeException();

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double GetTermScalarValue(int basisBlade)
    {
        return basisBlade switch
        {
            0 => X.ScalarValue,
            1 => Y.ScalarValue,
            2 => Z.ScalarValue,
            _ => 0
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer RemoveTerm(int basisBlade)
    {
        this[basisBlade] = 0;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer SetTerm(int basisBlade, double scalar)
    {
        this[basisBlade] = scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer SetTerm(LinBasisVector3D axis)
    {
        if (axis == LinBasisVector3D.Px)
            X = 1;

        else if (axis == LinBasisVector3D.Py)
            Y = 1;

        else if (axis == LinBasisVector3D.Pz)
            Z = 1;

        else if (axis == LinBasisVector3D.Nx)
            X = -1;

        else if (axis == LinBasisVector3D.Ny)
            Y = -1;

        else
            Z = -1;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer SetTerm(LinBasisVector3D axis, double scalar)
    {
        if (axis == LinBasisVector3D.Px)
            X = scalar;

        else if (axis == LinBasisVector3D.Py)
            Y = scalar;

        else if (axis == LinBasisVector3D.Pz)
            Z = scalar;

        else if (axis == LinBasisVector3D.Nx)
            X = -scalar;

        else if (axis == LinBasisVector3D.Ny)
            Y = -scalar;

        else
            Z = -scalar;

        return this;
    }

    public LinFloat64Vector3DComposer SetVector(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;

        return this;
    }

    public LinFloat64Vector3DComposer SetVector(ITriplet<Float64Scalar> vector)
    {
        X = vector.Item1;
        Y = vector.Item2;
        Z = vector.Item3;

        return this;
    }

    public LinFloat64Vector3DComposer SetVectorNegative(ITriplet<Float64Scalar> vector)
    {
        X = -vector.Item1;
        Y = -vector.Item2;
        Z = -vector.Item3;

        return this;
    }

    public LinFloat64Vector3DComposer SetVector(ITriplet<Float64Scalar> vector, double scalingFactor)
    {
        X = scalingFactor * vector.Item1;
        Y = scalingFactor * vector.Item2;
        Z = scalingFactor * vector.Item3;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer AddTerm(int basisBlade, double scalar)
    {
        this[basisBlade] += scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer AddTerm(LinBasisVector3D axis)
    {
        if (axis == LinBasisVector3D.Px)
            X += 1;

        else if (axis == LinBasisVector3D.Py)
            Y += 1;

        else if (axis == LinBasisVector3D.Pz)
            Z += 1;

        else if (axis == LinBasisVector3D.Nx)
            X -= 1;

        else if (axis == LinBasisVector3D.Ny)
            Y -= 1;

        else
            Z -= 1;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer AddTerm(LinBasisVector3D axis, double scalar)
    {
        if (axis == LinBasisVector3D.Px)
            X += scalar;

        else if (axis == LinBasisVector3D.Py)
            Y += scalar;

        else if (axis == LinBasisVector3D.Pz)
            Z += scalar;

        else if (axis == LinBasisVector3D.Nx)
            X -= scalar;

        else if (axis == LinBasisVector3D.Ny)
            Y -= scalar;

        else
            Z -= scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer AddVector(ITriplet<Float64Scalar> vector)
    {
        X += vector.Item1;
        Y += vector.Item2;
        Z += vector.Item3;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer AddVector(ITriplet<Float64Scalar> vector, double scalingFactor)
    {
        X += scalingFactor * vector.Item1;
        Y += scalingFactor * vector.Item2;
        Z += scalingFactor * vector.Item3;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer SubtractTerm(int basisBlade, double scalar)
    {
        this[basisBlade] -= scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer SubtractTerm(LinBasisVector3D axis)
    {
        if (axis == LinBasisVector3D.Px)
            X -= 1;

        else if (axis == LinBasisVector3D.Py)
            Y -= 1;

        else if (axis == LinBasisVector3D.Pz)
            Z -= 1;

        else if (axis == LinBasisVector3D.Nx)
            X += 1;

        else if (axis == LinBasisVector3D.Ny)
            Y += 1;

        else
            Z += 1;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer SubtractTerm(LinBasisVector3D axis, double scalar)
    {
        if (axis == LinBasisVector3D.Px)
            X -= scalar;

        else if (axis == LinBasisVector3D.Py)
            Y -= scalar;

        else if (axis == LinBasisVector3D.Pz)
            Z -= scalar;

        else if (axis == LinBasisVector3D.Nx)
            X += scalar;

        else if (axis == LinBasisVector3D.Ny)
            Y += scalar;

        else
            Z += scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer SubtractVector(ITriplet<Float64Scalar> vector)
    {
        X -= vector.Item1;
        Y -= vector.Item2;
        Z -= vector.Item3;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer SubtractVector(ITriplet<Float64Scalar> vector, double scalingFactor)
    {
        X -= scalingFactor * vector.Item1;
        Y -= scalingFactor * vector.Item2;
        Z -= scalingFactor * vector.Item3;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer AddTerm(int basisBlade, double scalar1, double scalar2)
    {
        this[basisBlade] += scalar1 * scalar2;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer SubtractTerm(int basisBlade, double scalar1, double scalar2)
    {
        this[basisBlade] -= scalar1 * scalar2;

        return this;
    }


    public LinFloat64Vector3DComposer MapScalars(Func<double, double> mappingFunction)
    {
        X = mappingFunction(X);
        Y = mappingFunction(Y);
        Z = mappingFunction(Z);

        return this;
    }

    public LinFloat64Vector3DComposer MapScalars(Func<int, double, double> mappingFunction)
    {
        X = mappingFunction(0, X);
        Y = mappingFunction(1, Y);
        Z = mappingFunction(2, Z);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer Negative()
    {
        X = -X;
        Y = -Y;
        Z = -Z;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer Times(double scalarFactor)
    {
        X *= scalarFactor;
        Y *= scalarFactor;
        Z *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer Divide(double scalarFactor)
    {
        scalarFactor = 1d / scalarFactor;

        X *= scalarFactor;
        Y *= scalarFactor;
        Z *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3DComposer DivideByENorm()
    {
        var scalarFactor = 1d / ENorm();

        X *= scalarFactor;
        Y *= scalarFactor;
        Z *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ENormSquared()
    {
        return X * X + Y * Y + Z * Z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double ENorm()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetVector()
    {
        return LinFloat64Vector3D.Create(X, Y, Z);
    }

}