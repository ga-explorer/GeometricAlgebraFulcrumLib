using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

/// <summary>
/// An immutable 2-tuple of double precision numbers
/// </summary>
public sealed record LinFloat64Vector2D :
    ILinFloat64Vector2D,
    ILinFloat64Multivector2D
{
    public static LinFloat64Vector2D Zero { get; }
        = new LinFloat64Vector2D(
            0d,
            0d
        );

    public static LinFloat64Vector2D E1 { get; }
        = new LinFloat64Vector2D(
            1d,
            0d
        );

    public static LinFloat64Vector2D E2 { get; }
        = new LinFloat64Vector2D(
            0d,
            1d
        );

    public static LinFloat64Vector2D NegativeE1 { get; }
        = new LinFloat64Vector2D(
            -1d,
            0d
        );

    public static LinFloat64Vector2D NegativeE2 { get; }
        = new LinFloat64Vector2D(
            0d,
            -1d
        );

    public static LinFloat64Vector2D Symmetric { get; }
        = new LinFloat64Vector2D(
            1d,
            1d
        );

    public static LinFloat64Vector2D UnitSymmetric { get; }
        = new LinFloat64Vector2D(
            2d.Sqrt().Inverse(),
            2d.Sqrt().Inverse()
        );


    
    public static LinFloat64Vector2D Create(int x, int y)
    {
        return new LinFloat64Vector2D(x, y);
    }

    
    public static LinFloat64Vector2D Create(long x, long y)
    {
        return new LinFloat64Vector2D(x, y);
    }

    
    public static LinFloat64Vector2D Create(float x, float y)
    {
        return new LinFloat64Vector2D(x, y);
    }

    
    public static LinFloat64Vector2D Create(double x, double y)
    {
        return new LinFloat64Vector2D(x, y);
    }

    
    public static LinFloat64Vector2D Create(IPair<double> tuple)
    {
        return new LinFloat64Vector2D(tuple.Item1, tuple.Item2);
    }
    
    
    public static LinFloat64Vector2D CreateFromPolar(LinFloat64Angle angle)
    {
        return new LinFloat64Vector2D(
            angle.Cos(),
            angle.Sin()
        );
    }

    
    public static LinFloat64Vector2D CreateFromPolar(double length, LinFloat64Angle angle)
    {
        return new LinFloat64Vector2D(
            length * angle.Cos(),
            length * angle.Sin()
        );
    }

    
    public static LinFloat64Vector2D CreateEqualXy(double x)
    {
        var scalar = x;

        return new LinFloat64Vector2D(scalar, scalar);
    }

    
    public static LinFloat64Vector2D CreateUnitVector(double x, double y)
    {
        var s = x * x + y * y;

        if (s.IsZero()) return UnitSymmetric;

        s = 1d / Math.Sqrt(s);

        return new LinFloat64Vector2D(x * s, y * s);
    }

    
    public static LinFloat64Vector2D VectorSymmetric(double vectorLength)
    {
        var scalar = vectorLength / 2d.Sqrt();

        return new LinFloat64Vector2D(scalar, scalar);
    }


    
    public static LinFloat64Vector2D operator -(LinFloat64Vector2D v1)
    {
        return new LinFloat64Vector2D(-v1.X, -v1.Y);
    }

    
    public static LinFloat64Vector2D operator +(LinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
    }

    
    public static LinFloat64Vector2D operator +(LinFloat64Vector2D v1, ILinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
    }

    
    public static LinFloat64Vector2D operator +(ILinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
    }

    
    public static LinFloat64Vector2D operator -(LinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
    }

    
    public static LinFloat64Vector2D operator -(LinFloat64Vector2D v1, ILinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
    }

    
    public static LinFloat64Vector2D operator -(ILinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
    }

    
    public static LinFloat64Vector2D operator *(LinFloat64Vector2D v1, double s)
    {
        return new LinFloat64Vector2D(v1.X * s, v1.Y * s);
    }

    
    public static LinFloat64Vector2D operator *(double s, LinFloat64Vector2D v1)
    {
        return new LinFloat64Vector2D(v1.X * s, v1.Y * s);
    }

    
    public static LinFloat64Vector2D operator /(LinFloat64Vector2D v1, double s)
    {
        s = 1d / s;

        return new LinFloat64Vector2D(v1.X * s, v1.Y * s);
    }


    public int VSpaceDimensions
        => 2;

    public double Scalar
        => 0d;

    public double Scalar1
        => X;

    public double Scalar2
        => Y;

    public double Scalar12
        => 0d;

    public int Count
        => 4;

    public double X { get; }

    public double Y { get; }

    public double Item1
        => X;

    public double Item2
        => Y;

    /// <summary>
    /// Get the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public double this[int index]
    {
        get
        {
            if (index is < 0 or > 3)
                throw new IndexOutOfRangeException();

            return index switch
            {
                1 => X,
                2 => Y,
                _ => 0d
            };
        }

    }


    
    private LinFloat64Vector2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    
    private LinFloat64Vector2D(IFloat64Scalar x, IFloat64Scalar y)
    {
        X = x.ToScalar();
        Y = y.ToScalar();
    }


    
    public void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
    }


    
    public bool IsValid()
    {
        return X.IsValid() &&
               Y.IsValid();
    }

    
    public bool IsZero()
    {
        return X.IsZero() &&
               Y.IsZero();
    }

    
    public bool IsNearZero(double zeroEpsilon = 1E-12)
    {
        return Norm().IsNearZero(zeroEpsilon);
    }

    
    public LinFloat64Angle GetPolarAngle()
    {
        return LinFloat64PolarAngle.CreateFromVector(X, Y);
    }

    
    public double Norm()
    {
        return (X.Square() + Y.Square()).Sqrt();
    }

    
    public double NormSquared()
    {
        return X.Square() + Y.Square();
    }

    
    public LinFloat64Multivector2D ToMultivector2D()
    {
        return LinFloat64Multivector2D.Create(this);
    }

    
    public LinFloat64Vector2D Negative()
    {
        return new LinFloat64Vector2D(-Scalar1, -Scalar2);
    }

    
    public LinFloat64Vector2D GradeInvolution()
    {
        return Negative();
    }

    
    public LinFloat64Vector2D Reverse()
    {
        return this;
    }

    
    public LinFloat64Vector2D CliffordConjugate()
    {
        return Negative();
    }

    
    public LinFloat64Vector2D Inverse()
    {
        var normSquared = NormSquared();

        return normSquared.IsZero()
            ? throw new InvalidOperationException()
            : this / normSquared;
    }


    public LinFloat64Vector2D DirectionToUnitNormal2D(LinFloat64Vector2D? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / norm;

        return Create(
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinFloat64Vector2D DirectionToNormal2D(LinFloat64Vector2D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / normSquared;

        return Create(
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinFloat64Vector2D DirectionToNormal2D(double scalingFactor, LinFloat64Vector2D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = scalingFactor / normSquared;

        return Create(
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinFloat64Vector2D NormalToUnitDirection2D(LinFloat64Vector2D? zeroNormal = null)
    {
        var norm = NormSquared();

        if (norm.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / norm;

        return Create(
            Scalar2 * s,
            Scalar1 * -s
        );
    }

    public LinFloat64Vector2D NormalToDirection2D(LinFloat64Vector2D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / normSquared;

        return Create(
            Scalar2 * s,
            Scalar1 * -s
        );
    }

    public LinFloat64Vector2D NormalToDirection2D(double scalingFactor, LinFloat64Vector2D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = scalingFactor / normSquared;

        return Create(
            Scalar2 * s,
            Scalar1 * -s
        );
    }


    
    public LinFloat64Vector2D Dual2D()
    {
        return Create(Scalar2, -Scalar1);
    }

    
    public LinFloat64Vector2D Dual2D(double scalingFactor)
    {
        return Create(
            Scalar2 * scalingFactor,
            -Scalar1 * scalingFactor
        );
    }

    
    public LinFloat64Vector2D UnDual2D()
    {
        return Create(-Scalar2, Scalar1);
    }

    
    public LinFloat64Vector2D UnDual2D(double scalingFactor)
    {
        return Create(
            -Scalar2 * scalingFactor,
            Scalar1 * scalingFactor
        );
    }


    
    public double Sp(LinFloat64Scalar2D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Vector2D mv2)
    {
        return Scalar1 * mv2.Scalar1 +
               Scalar2 * mv2.Scalar2;
    }

    
    public double Sp(LinFloat64Bivector2D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Multivector2D mv2)
    {
        var mv = 0d;

        if (!IsZero() && !mv2.KVector1.IsZero())
            mv += Sp(mv2.KVector1);

        return mv;
    }

    
    
    public LinFloat64Vector2D Op(LinFloat64Scalar2D mv2)
    {
        return Create(
            Scalar1 * mv2.Scalar,
            Scalar2 * mv2.Scalar
        );
    }

    
    public LinFloat64Bivector2D Op(LinFloat64Vector2D mv2)
    {
        return LinFloat64Bivector2D.Create(
            Scalar1 * mv2.Scalar2 - Scalar2 * mv2.Scalar1
        );
    }
    
    
    public LinFloat64Scalar2D Op(LinFloat64Bivector2D mv2)
    {
        return LinFloat64Scalar2D.Zero;
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

    
    public LinFloat64Vector2D Lcp(LinFloat64Bivector2D b2)
    {
        var s1 =
            -Scalar2 * b2.Scalar12;

        var s2 =
            Scalar1 * b2.Scalar12;

        return Create(s1, s2);
    }

    
    public LinFloat64Vector2D ProjectOn(LinFloat64Bivector2D mv2)
    {
        return Lcp(mv2).Lcp(mv2.Inverse());
    }


    
    public LinFloat64Vector2D Gp(LinFloat64Bivector2D mv2)
    {
        var s1 =
            -Scalar2 * mv2.Scalar12;

        var s2 =
            Scalar1 * mv2.Scalar12;

        return Create(s1, s2);
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

    
    public string ToTupleString()
    {
        return $"({X:G10}, {Y:G10})";
    }

    
    public override string ToString()
    {
        return $"({X:G10})<1> + ({Y:G10})<2>";
    }
}