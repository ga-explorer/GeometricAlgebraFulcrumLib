using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public sealed record LinFloat64Scalar2D :
    ILinFloat64Multivector2D,
    IFloat64Scalar
{
    public static LinFloat64Scalar2D Zero { get; }
        = new LinFloat64Scalar2D(0d);

    public static LinFloat64Scalar2D E0 { get; }
        = new LinFloat64Scalar2D(1d);

    public static LinFloat64Scalar2D NegativeE0 { get; }
        = new LinFloat64Scalar2D(-1d);


    
    public static LinFloat64Scalar2D Create(double scalar)
    {
        if (scalar.IsZero())
            return Zero;

        if (scalar.IsOne())
            return E0;

        if (scalar.IsMinusOne())
            return NegativeE0;

        return new LinFloat64Scalar2D(scalar);
    }


    
    public static LinFloat64Scalar2D operator +(LinFloat64Scalar2D v1)
    {
        return v1;
    }

    
    public static LinFloat64Scalar2D operator -(LinFloat64Scalar2D v1)
    {
        return new LinFloat64Scalar2D(-v1.Scalar);
    }

    
    public static LinFloat64Scalar2D operator +(LinFloat64Scalar2D v1, double v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar + v2);
    }

    
    public static LinFloat64Scalar2D operator +(double v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1 + v2.Scalar);
    }

    
    public static LinFloat64Scalar2D operator +(LinFloat64Scalar2D v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar + v2.Scalar);
    }

    
    public static LinFloat64Scalar2D operator -(LinFloat64Scalar2D v1, double v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar - v2);
    }

    
    public static LinFloat64Scalar2D operator -(double v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1 - v2.Scalar);
    }

    
    public static LinFloat64Scalar2D operator -(LinFloat64Scalar2D v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar - v2.Scalar);
    }

    
    public static LinFloat64Scalar2D operator *(LinFloat64Scalar2D v1, double v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar * v2);
    }

    
    public static LinFloat64Scalar2D operator *(double v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1 * v2.Scalar);
    }

    
    public static LinFloat64Scalar2D operator *(LinFloat64Scalar2D v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar * v2.Scalar);
    }

    
    public static LinFloat64Scalar2D operator /(LinFloat64Scalar2D v1, double v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar / v2);
    }

    
    public static LinFloat64Scalar2D operator /(double v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1 / v2.Scalar);
    }

    
    public static LinFloat64Scalar2D operator /(LinFloat64Scalar2D v1, LinFloat64Scalar2D v2)
    {
        return new LinFloat64Scalar2D(v1.Scalar / v2.Scalar);
    }


    public int VSpaceDimensions
        => 3;

    public double Scalar { get; }

    public double Scalar1
        => 0d;

    public double Scalar2
        => 0d;

    public double Scalar12
        => 0d;

    public int Count
        => 4;

    public double this[int index]
    {
        get
        {
            if (index is < 0 or > 3)
                throw new IndexOutOfRangeException();

            return index == 0
                ? Scalar
                : 0d;
        }
    }
    
    public double ScalarValue 
        => Scalar;


    
    private LinFloat64Scalar2D(double scalar)
    {
        Scalar = scalar;
    }


    
    public bool IsValid()
    {
        return Scalar.IsValid();
    }

    
    public bool IsZero()
    {
        return Scalar.IsZero();
    }

    
    public bool IsNearZero(double zeroEpsilon = 1E-12)
    {
        return Scalar.IsNearZero();
    }
    
    
    public double ToScalar()
    {
        return Scalar;
    }

    
    public double Norm()
    {
        return Scalar.Abs();
    }

    
    public double NormSquared()
    {
        return Scalar.Square();
    }

    
    public LinFloat64Multivector2D ToMultivector2D()
    {
        return LinFloat64Multivector2D.Create(this);
    }

    
    public LinFloat64Scalar2D Negative()
    {
        return new LinFloat64Scalar2D(-Scalar);
    }

    
    public LinFloat64Scalar2D GradeInvolution()
    {
        return this;
    }

    
    public LinFloat64Scalar2D Reverse()
    {
        return this;
    }

    
    public LinFloat64Scalar2D CliffordConjugate()
    {
        return this;
    }

    
    public LinFloat64Scalar2D Inverse()
    {
        return IsZero()
            ? throw new InvalidOperationException()
            : new LinFloat64Scalar2D(Scalar.Inverse());
    }


    
    public LinFloat64Bivector2D DirectionToUnitNormal2D()
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return Scalar.IsPositive()
            ? LinFloat64Bivector2D.E12
            : LinFloat64Bivector2D.E21;
    }

    
    public LinFloat64Bivector2D DirectionToNormal2D()
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return LinFloat64Bivector2D.Create(1d / Scalar);
    }

    
    public LinFloat64Bivector2D DirectionToNormal2D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return LinFloat64Bivector2D.Create(scalingFactor / Scalar);
    }

    
    public LinFloat64Bivector2D NormalToUnitDirection2D()
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return Scalar.IsPositive()
            ? LinFloat64Bivector2D.E12
            : LinFloat64Bivector2D.E21;
    }

    
    public LinFloat64Bivector2D NormalToDirection2D()
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return LinFloat64Bivector2D.Create(1d / Scalar);
    }

    
    public LinFloat64Bivector2D NormalToDirection2D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinFloat64Bivector2D.E12;

        return LinFloat64Bivector2D.Create(scalingFactor / Scalar);
    }


    
    public LinFloat64Bivector2D Dual2D()
    {
        return LinFloat64Bivector2D.Create(-Scalar);
    }

    
    public LinFloat64Bivector2D UnDual2D()
    {
        return LinFloat64Bivector2D.Create(Scalar);
    }

    
    
    public LinFloat64Scalar2D Op(LinFloat64Scalar2D mv2)
    {
        return Create(
            Scalar * mv2.Scalar
        );
    }

    
    public LinFloat64Vector2D Op(LinFloat64Vector2D mv2)
    {
        return LinFloat64Vector2D.Create(
            Scalar * mv2.Scalar1,
            Scalar * mv2.Scalar2
        );
    }

    
    public LinFloat64Bivector2D Op(LinFloat64Bivector2D mv2)
    {
        return LinFloat64Bivector2D.Create(
            Scalar * mv2.Scalar12
        );
    }

    
    public LinFloat64Multivector2D Op(LinFloat64Multivector2D mv2)
    {
        var mv = LinFloat64Multivector2D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += Op(mv2.KVector1);

        if (!mv2.KVector2.IsZero())
            mv += Op(mv2.KVector2);

        return mv;
    }


    
    public double Sp(LinFloat64Scalar2D mv2)
    {
        return Scalar * mv2.Scalar;
    }

    
    public double Sp(LinFloat64Vector2D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Bivector2D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Multivector2D mv2)
    {
        var mv = 0d;

        if (!IsZero() && !mv2.KVector0.IsZero())
            mv += Sp(mv2.KVector0);

        return mv;
    }




    
    public IEnumerator<double> GetEnumerator()
    {
        yield return Scalar;
        yield return Scalar1;
        yield return Scalar2;
        yield return Scalar12;
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