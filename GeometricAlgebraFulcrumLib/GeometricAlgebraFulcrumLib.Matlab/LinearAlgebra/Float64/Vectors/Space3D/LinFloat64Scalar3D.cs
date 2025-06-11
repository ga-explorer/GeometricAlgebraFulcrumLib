using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public sealed record LinFloat64Scalar3D :
    ILinFloat64KVector3D,
    IFloat64Scalar
{
    public static LinFloat64Scalar3D Zero { get; }
        = new LinFloat64Scalar3D(0d);

    public static LinFloat64Scalar3D E0 { get; }
        = new LinFloat64Scalar3D(1d);

    public static LinFloat64Scalar3D NegativeE0 { get; }
        = new LinFloat64Scalar3D(-1d);


    
    public static LinFloat64Scalar3D Create(double scalar)
    {
        if (scalar.IsZero())
            return Zero;

        if (scalar.IsOne())
            return E0;

        if (scalar.IsMinusOne())
            return NegativeE0;

        return new LinFloat64Scalar3D(scalar);
    }


    
    public static LinFloat64Scalar3D operator +(LinFloat64Scalar3D v1)
    {
        return v1;
    }

    
    public static LinFloat64Scalar3D operator -(LinFloat64Scalar3D v1)
    {
        return new LinFloat64Scalar3D(-v1.Scalar);
    }

    
    public static LinFloat64Scalar3D operator +(LinFloat64Scalar3D v1, double v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar + v2);
    }

    
    public static LinFloat64Scalar3D operator +(double v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1 + v2.Scalar);
    }

    
    public static LinFloat64Scalar3D operator +(LinFloat64Scalar3D v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar + v2.Scalar);
    }

    
    public static LinFloat64Scalar3D operator -(LinFloat64Scalar3D v1, double v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar - v2);
    }

    
    public static LinFloat64Scalar3D operator -(double v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1 - v2.Scalar);
    }

    
    public static LinFloat64Scalar3D operator -(LinFloat64Scalar3D v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar - v2.Scalar);
    }

    
    public static LinFloat64Scalar3D operator *(LinFloat64Scalar3D v1, double v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar * v2);
    }

    
    public static LinFloat64Scalar3D operator *(double v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1 * v2.Scalar);
    }

    
    public static LinFloat64Scalar3D operator *(LinFloat64Scalar3D v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar * v2.Scalar);
    }

    
    public static LinFloat64Scalar3D operator /(LinFloat64Scalar3D v1, double v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar / v2);
    }

    
    public static LinFloat64Scalar3D operator /(double v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1 / v2.Scalar);
    }

    
    public static LinFloat64Scalar3D operator /(LinFloat64Scalar3D v1, LinFloat64Scalar3D v2)
    {
        return new LinFloat64Scalar3D(v1.Scalar / v2.Scalar);
    }


    public int VSpaceDimensions
        => 3;

    public int Grade
        => 0;

    public double Scalar { get; }

    public double Scalar1
        => 0d;

    public double Scalar2
        => 0d;

    public double Scalar3
        => 0d;

    public double Scalar12
        => 0d;

    public double Scalar13
        => 0d;

    public double Scalar23
        => 0d;

    public double Scalar123
        => 0d;

    public int Count
        => 8;

    public double this[int index]
    {
        get
        {
            if (index is < 0 or > 7)
                throw new IndexOutOfRangeException();

            return index == 0
                ? Scalar
                : 0d;
        }
    }

    public double ScalarValue 
        => Scalar;


    
    private LinFloat64Scalar3D(double scalar)
    {
        Scalar = scalar;
    }


    
    public bool IsValid()
    {
        return Scalar.IsValid();
    }

    
    public double ToScalar()
    {
        return Scalar;
    }

    
    public bool IsZero()
    {
        return Scalar.IsZero();
    }

    
    public bool IsNearZero(double zeroEpsilon = 1E-12)
    {
        return Scalar.IsNearZero();
    }

    
    public double Norm()
    {
        return Scalar.Abs();
    }

    
    public double NormSquared()
    {
        return Scalar.Square();
    }

    
    public LinFloat64Multivector3D ToMultivector3D()
    {
        return LinFloat64Multivector3D.Create(this);
    }

    
    public LinFloat64Scalar3D Negative()
    {
        return new LinFloat64Scalar3D(-Scalar);
    }

    
    public LinFloat64Scalar3D GradeInvolution()
    {
        return this;
    }

    
    public LinFloat64Scalar3D Reverse()
    {
        return this;
    }

    
    public LinFloat64Scalar3D CliffordConjugate()
    {
        return this;
    }

    
    public LinFloat64Scalar3D Inverse()
    {
        return IsZero()
            ? throw new InvalidOperationException()
            : new LinFloat64Scalar3D(Scalar.Inverse());
    }


    
    public LinFloat64Trivector3D DirectionToUnitNormal3D()
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return Scalar.IsPositive()
            ? LinFloat64Trivector3D.E123
            : LinFloat64Trivector3D.NegativeE123;
    }

    
    public LinFloat64Trivector3D DirectionToNormal3D()
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return LinFloat64Trivector3D.Create(1d / Scalar);
    }

    
    public LinFloat64Trivector3D DirectionToNormal3D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return LinFloat64Trivector3D.Create(scalingFactor / Scalar);
    }

    
    public LinFloat64Trivector3D NormalToUnitDirection3D()
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return Scalar.IsPositive()
            ? LinFloat64Trivector3D.E123
            : LinFloat64Trivector3D.NegativeE123;
    }

    
    public LinFloat64Trivector3D NormalToDirection3D()
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return LinFloat64Trivector3D.Create(1d / Scalar);
    }

    
    public LinFloat64Trivector3D NormalToDirection3D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinFloat64Trivector3D.E123;

        return LinFloat64Trivector3D.Create(scalingFactor / Scalar);
    }


    
    public LinFloat64Trivector3D Dual()
    {
        return LinFloat64Trivector3D.Create(Scalar);
    }

    
    public LinFloat64Trivector3D UnDual()
    {
        return LinFloat64Trivector3D.Create(Scalar);
    }


    
    public double Sp(LinFloat64Scalar3D mv2)
    {
        return Scalar * mv2.Scalar;
    }

    
    public double Sp(LinFloat64Vector3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Bivector3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Trivector3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Multivector3D mv2)
    {
        var mv = 0d;

        if (!IsZero() && !mv2.KVector0.IsZero())
            mv += Sp(mv2.KVector0);

        return mv;
    }
    
    
    public LinFloat64Scalar3D Op(LinFloat64Scalar3D mv2)
    {
        return Create(
            Scalar * mv2.Scalar
        );
    }

    
    public LinFloat64Vector3D Op(LinFloat64Vector3D mv2)
    {
        return LinFloat64Vector3D.Create(
            Scalar * mv2.Scalar1,
            Scalar * mv2.Scalar2,
            Scalar * mv2.Scalar3
        );
    }

    
    public LinFloat64Bivector3D Op(LinFloat64Bivector3D mv2)
    {
        return LinFloat64Bivector3D.Create(
            Scalar * mv2.Scalar12,
            Scalar * mv2.Scalar13,
            Scalar * mv2.Scalar23
        );
    }

    
    public LinFloat64Trivector3D Op(LinFloat64Trivector3D mv2)
    {
        return LinFloat64Trivector3D.Create(
            Scalar * mv2.Scalar123
        );
    }

    
    public LinFloat64Multivector3D Op(LinFloat64Multivector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += Op(mv2.KVector1);

        if (!mv2.KVector2.IsZero())
            mv += Op(mv2.KVector2);

        if (!mv2.KVector3.IsZero())
            mv += Op(mv2.KVector3);

        return mv;
    }


    
    public IEnumerator<double> GetEnumerator()
    {
        yield return Scalar;
        yield return Scalar1;
        yield return Scalar2;
        yield return Scalar12;
        yield return Scalar3;
        yield return Scalar13;
        yield return Scalar23;
        yield return Scalar123;
    }

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    public override string ToString()
    {
        return $"({Scalar})<>";
    }

}