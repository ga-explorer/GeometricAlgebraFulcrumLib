using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public sealed class LinVector3DComposer<T> :
    ILinVector3D<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3DComposer<T> Create(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector3DComposer<T>(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3DComposer<T> Create(IScalarProcessor<T> scalarProcessor, T x, T y, T z)
    {
        return new LinVector3DComposer<T>(scalarProcessor, x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3DComposer<T> Create(Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        return new LinVector3DComposer<T>(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector3DComposer<T> Create(ITriplet<Scalar<T>> vector)
    {
        return new LinVector3DComposer<T>(
            vector.Item1,
            vector.Item2,
            vector.Item3
        );
    }


    public IScalarProcessor<T> ScalarProcessor 
        => X.ScalarProcessor;

    public int VSpaceDimensions
        => 3;

    public Scalar<T> X { get; set; }

    public Scalar<T> Y { get; set; }

    public Scalar<T> Z { get; set; }
    
    public Scalar<T> Item1
        => X;

    public Scalar<T> Item2
        => Y;

    public Scalar<T> Item3
        => Z;

    public Scalar<T> this[int index]
    {
        get => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
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
    private LinVector3DComposer(IScalarProcessor<T> scalarProcessor)
    {
        X = scalarProcessor.Zero;
        Y = scalarProcessor.Zero;
        Z = scalarProcessor.Zero;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinVector3DComposer(IScalarProcessor<T> scalarProcessor, T x, T y, T z)
    {
        X = scalarProcessor.ScalarFromValue(x);
        Y = scalarProcessor.ScalarFromValue(y);
        Z = scalarProcessor.ScalarFromValue(z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinVector3DComposer(Scalar<T> x, Scalar<T> y, Scalar<T> z)
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
    public LinVector3DComposer<T> Clear()
    {
        X = ScalarProcessor.Zero;
        Y = ScalarProcessor.Zero;
        Z = ScalarProcessor.Zero;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> ClearTerm(int index)
    {
        if (index == 0)
            X = ScalarProcessor.Zero;
        else if (index == 1)
            Y = ScalarProcessor.Zero;
        else if (index == 2)
            Z = ScalarProcessor.Zero;
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
            _ => ScalarProcessor.Zero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> RemoveTerm(int basisBlade)
    {
        this[basisBlade] = ScalarProcessor.Zero;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> SetTerm(int basisBlade, Scalar<T> scalar)
    {
        this[basisBlade] = scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> SetTerm(LinBasisVector3D axis)
    {
        if (axis == LinBasisVector3D.Px)
            X = ScalarProcessor.One;

        else if (axis == LinBasisVector3D.Py)
            Y = ScalarProcessor.One;

        else if (axis == LinBasisVector3D.Pz)
            Z = ScalarProcessor.One;

        else if (axis == LinBasisVector3D.Nx)
            X = ScalarProcessor.MinusOne;

        else if (axis == LinBasisVector3D.Ny)
            Y = ScalarProcessor.MinusOne;

        else
            Z = ScalarProcessor.MinusOne;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> SetTerm(LinBasisVector3D axis, Scalar<T> scalar)
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

    public LinVector3DComposer<T> SetVector(Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        X = x;
        Y = y;
        Z = z;

        return this;
    }

    public LinVector3DComposer<T> SetVector(ITriplet<Scalar<T>> vector)
    {
        X = vector.Item1;
        Y = vector.Item2;
        Z = vector.Item3;

        return this;
    }

    public LinVector3DComposer<T> SetVectorNegative(ITriplet<Scalar<T>> vector)
    {
        X = -vector.Item1;
        Y = -vector.Item2;
        Z = -vector.Item3;

        return this;
    }

    public LinVector3DComposer<T> SetVector(ITriplet<Scalar<T>> vector, Scalar<T> scalingFactor)
    {
        X = scalingFactor * vector.Item1;
        Y = scalingFactor * vector.Item2;
        Z = scalingFactor * vector.Item3;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> AddTerm(int basisBlade, Scalar<T> scalar)
    {
        this[basisBlade] += scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> AddTerm(LinBasisVector3D axis)
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
    public LinVector3DComposer<T> AddTerm(LinBasisVector3D axis, Scalar<T> scalar)
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
    public LinVector3DComposer<T> AddVector(ITriplet<Scalar<T>> vector)
    {
        X += vector.Item1;
        Y += vector.Item2;
        Z += vector.Item3;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> AddVector(ITriplet<Scalar<T>> vector, Scalar<T> scalingFactor)
    {
        X += scalingFactor * vector.Item1;
        Y += scalingFactor * vector.Item2;
        Z += scalingFactor * vector.Item3;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> SubtractTerm(int basisBlade, Scalar<T> scalar)
    {
        this[basisBlade] -= scalar;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> SubtractTerm(LinBasisVector3D axis)
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
    public LinVector3DComposer<T> SubtractTerm(LinBasisVector3D axis, Scalar<T> scalar)
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
    public LinVector3DComposer<T> SubtractVector(ITriplet<Scalar<T>> vector)
    {
        X -= vector.Item1;
        Y -= vector.Item2;
        Z -= vector.Item3;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> SubtractVector(ITriplet<Scalar<T>> vector, Scalar<T> scalingFactor)
    {
        X -= scalingFactor * vector.Item1;
        Y -= scalingFactor * vector.Item2;
        Z -= scalingFactor * vector.Item3;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> AddTerm(int basisBlade, Scalar<T> scalar1, Scalar<T> scalar2)
    {
        this[basisBlade] += scalar1 * scalar2;

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> SubtractTerm(int basisBlade, Scalar<T> scalar1, Scalar<T> scalar2)
    {
        this[basisBlade] -= scalar1 * scalar2;

        return this;
    }


    public LinVector3DComposer<T> MapScalars(Func<Scalar<T>, Scalar<T>> mappingFunction)
    {
        X = mappingFunction(X);
        Y = mappingFunction(Y);
        Z = mappingFunction(Z);

        return this;
    }

    public LinVector3DComposer<T> MapScalars(Func<int, Scalar<T>, Scalar<T>> mappingFunction)
    {
        X = mappingFunction(0, X);
        Y = mappingFunction(1, Y);
        Z = mappingFunction(2, Z);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> Negative()
    {
        X = -X;
        Y = -Y;
        Z = -Z;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> Times(Scalar<T> scalarFactor)
    {
        X *= scalarFactor;
        Y *= scalarFactor;
        Z *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> Divide(Scalar<T> scalarFactor)
    {
        scalarFactor = 1d / scalarFactor;

        X *= scalarFactor;
        Y *= scalarFactor;
        Z *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3DComposer<T> DivideByENorm()
    {
        var scalarFactor = 1d / ENorm();

        X *= scalarFactor;
        Y *= scalarFactor;
        Z *= scalarFactor;

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENormSquared()
    {
        return X * X + Y * Y + Z * Z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ENorm()
    {
        return (X * X + Y * Y + Z * Z).Sqrt();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> GetVector()
    {
        return LinVector3D<T>.Create(X, Y, Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> ToVector3D()
    {
        return LinVector3D<T>.Create(X, Y, Z);
    }

    
}