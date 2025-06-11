using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public sealed class LinFloat64Vector3DComposer :
    ILinFloat64Vector3D
{
    
    public static LinFloat64Vector3DComposer Create()
    {
        return new LinFloat64Vector3DComposer();
    }

    
    public static LinFloat64Vector3DComposer Create(double x, double y, double z)
    {
        return new LinFloat64Vector3DComposer(x, y, z);
    }

    
    public static LinFloat64Vector3DComposer Create(ITriplet<double> vector)
    {
        return new LinFloat64Vector3DComposer(
            vector.Item1,
            vector.Item2,
            vector.Item3
        );
    }


    public double X { get; set; }

    public double Y { get; set; }

    public double Z { get; set; }

    public int VSpaceDimensions
        => 3;

    public double Item1
        => X;

    public double Item2
        => Y;

    public double Item3
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


    
    private LinFloat64Vector3DComposer()
    {
    }

    
    private LinFloat64Vector3DComposer(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }


    
    public bool IsValid()
    {
        return X.IsValid() && Y.IsValid() && Z.IsValid();
    }

    
    public bool IsZero()
    {
        return X.IsZero() && Y.IsZero() && Z.IsZero();
    }

    
    public LinFloat64Vector3DComposer Clear()
    {
        X = 0d;
        Y = 0d;
        Z = 0d;

        return this;
    }

    
    public LinFloat64Vector3DComposer ClearTerm(int index)
    {
        if (index == 0)
            X = 0d;
        else if (index == 1)
            Y = 0d;
        else if (index == 2)
            Z = 0d;
        else
            throw new IndexOutOfRangeException();

        return this;
    }


    
    public double GetTermScalarValue(int basisBlade)
    {
        return basisBlade switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => 0
        };
    }

    
    public LinFloat64Vector3DComposer RemoveTerm(int basisBlade)
    {
        this[basisBlade] = 0;

        return this;
    }


    
    public LinFloat64Vector3DComposer SetTerm(int basisBlade, double scalar)
    {
        this[basisBlade] = scalar;

        return this;
    }

    
    public LinFloat64Vector3DComposer SetTerm(LinBasisVector axis)
    {
        if (axis == LinBasisVector.Px)
            X = 1;

        else if (axis == LinBasisVector.Py)
            Y = 1;

        else if (axis == LinBasisVector.Pz)
            Z = 1;

        else if (axis == LinBasisVector.Nx)
            X = -1;

        else if (axis == LinBasisVector.Ny)
            Y = -1;

        else
            Z = -1;

        return this;
    }

    
    public LinFloat64Vector3DComposer SetTerm(LinBasisVector axis, double scalar)
    {
        if (axis == LinBasisVector.Px)
            X = scalar;

        else if (axis == LinBasisVector.Py)
            Y = scalar;

        else if (axis == LinBasisVector.Pz)
            Z = scalar;

        else if (axis == LinBasisVector.Nx)
            X = -scalar;

        else if (axis == LinBasisVector.Ny)
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

    public LinFloat64Vector3DComposer SetVector(ITriplet<double> vector)
    {
        X = vector.Item1;
        Y = vector.Item2;
        Z = vector.Item3;

        return this;
    }

    public LinFloat64Vector3DComposer SetVectorNegative(ITriplet<double> vector)
    {
        X = -vector.Item1;
        Y = -vector.Item2;
        Z = -vector.Item3;

        return this;
    }

    public LinFloat64Vector3DComposer SetVector(ITriplet<double> vector, double scalingFactor)
    {
        X = scalingFactor * vector.Item1;
        Y = scalingFactor * vector.Item2;
        Z = scalingFactor * vector.Item3;

        return this;
    }

    
    public LinFloat64Vector3DComposer AddTerm(int basisBlade, double scalar)
    {
        this[basisBlade] += scalar;

        return this;
    }

    
    public LinFloat64Vector3DComposer AddTerm(LinBasisVector axis)
    {
        if (axis == LinBasisVector.Px)
            X += 1;

        else if (axis == LinBasisVector.Py)
            Y += 1;

        else if (axis == LinBasisVector.Pz)
            Z += 1;

        else if (axis == LinBasisVector.Nx)
            X -= 1;

        else if (axis == LinBasisVector.Ny)
            Y -= 1;

        else
            Z -= 1;

        return this;
    }

    
    public LinFloat64Vector3DComposer AddTerm(LinBasisVector axis, double scalar)
    {
        if (axis == LinBasisVector.Px)
            X += scalar;

        else if (axis == LinBasisVector.Py)
            Y += scalar;

        else if (axis == LinBasisVector.Pz)
            Z += scalar;

        else if (axis == LinBasisVector.Nx)
            X -= scalar;

        else if (axis == LinBasisVector.Ny)
            Y -= scalar;

        else
            Z -= scalar;

        return this;
    }

    
    public LinFloat64Vector3DComposer AddVector(ITriplet<double> vector)
    {
        X += vector.Item1;
        Y += vector.Item2;
        Z += vector.Item3;

        return this;
    }

    
    public LinFloat64Vector3DComposer AddVector(ITriplet<double> vector, double scalingFactor)
    {
        X += scalingFactor * vector.Item1;
        Y += scalingFactor * vector.Item2;
        Z += scalingFactor * vector.Item3;

        return this;
    }

    
    public LinFloat64Vector3DComposer SubtractTerm(int basisBlade, double scalar)
    {
        this[basisBlade] -= scalar;

        return this;
    }

    
    public LinFloat64Vector3DComposer SubtractTerm(LinBasisVector axis)
    {
        if (axis == LinBasisVector.Px)
            X -= 1;

        else if (axis == LinBasisVector.Py)
            Y -= 1;

        else if (axis == LinBasisVector.Pz)
            Z -= 1;

        else if (axis == LinBasisVector.Nx)
            X += 1;

        else if (axis == LinBasisVector.Ny)
            Y += 1;

        else
            Z += 1;

        return this;
    }

    
    public LinFloat64Vector3DComposer SubtractTerm(LinBasisVector axis, double scalar)
    {
        if (axis == LinBasisVector.Px)
            X -= scalar;

        else if (axis == LinBasisVector.Py)
            Y -= scalar;

        else if (axis == LinBasisVector.Pz)
            Z -= scalar;

        else if (axis == LinBasisVector.Nx)
            X += scalar;

        else if (axis == LinBasisVector.Ny)
            Y += scalar;

        else
            Z += scalar;

        return this;
    }

    
    public LinFloat64Vector3DComposer SubtractVector(ITriplet<double> vector)
    {
        X -= vector.Item1;
        Y -= vector.Item2;
        Z -= vector.Item3;

        return this;
    }

    
    public LinFloat64Vector3DComposer SubtractVector(ITriplet<double> vector, double scalingFactor)
    {
        X -= scalingFactor * vector.Item1;
        Y -= scalingFactor * vector.Item2;
        Z -= scalingFactor * vector.Item3;

        return this;
    }


    
    public LinFloat64Vector3DComposer AddTerm(int basisBlade, double scalar1, double scalar2)
    {
        this[basisBlade] += scalar1 * scalar2;

        return this;
    }


    
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


    
    public LinFloat64Vector3DComposer Negative()
    {
        X = -X;
        Y = -Y;
        Z = -Z;

        return this;
    }

    
    public LinFloat64Vector3DComposer Times(double scalarFactor)
    {
        X *= scalarFactor;
        Y *= scalarFactor;
        Z *= scalarFactor;

        return this;
    }

    
    public LinFloat64Vector3DComposer Divide(double scalarFactor)
    {
        scalarFactor = 1d / scalarFactor;

        X *= scalarFactor;
        Y *= scalarFactor;
        Z *= scalarFactor;

        return this;
    }

    
    public LinFloat64Vector3DComposer DivideByENorm()
    {
        var scalarFactor = 1d / ENorm();

        X *= scalarFactor;
        Y *= scalarFactor;
        Z *= scalarFactor;

        return this;
    }

    
    public double ENormSquared()
    {
        return X * X + Y * Y + Z * Z;
    }

    
    public double ENorm()
    {
        return Math.Sqrt(X * X + Y * Y + Z * Z);
    }


    
    public LinFloat64Vector3D GetVector()
    {
        return LinFloat64Vector3D.Create(X, Y, Z);
    }

}