using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;

/// <summary>
/// An immutable 2-tuple of double precision numbers
/// </summary>
public sealed record LinFloat64Vector2D :
    ILinFloat64Vector2D,
    ILinFloat64Multivector2D
{
    public static LinFloat64Vector2D Zero { get; }
        = new LinFloat64Vector2D(
            Float64Scalar.Zero,
            Float64Scalar.Zero
        );

    public static LinFloat64Vector2D E1 { get; }
        = new LinFloat64Vector2D(
            Float64Scalar.One,
            Float64Scalar.Zero
        );

    public static LinFloat64Vector2D E2 { get; }
        = new LinFloat64Vector2D(
            Float64Scalar.Zero,
            Float64Scalar.One
        );

    public static LinFloat64Vector2D NegativeE1 { get; }
        = new LinFloat64Vector2D(
            Float64Scalar.NegativeOne,
            Float64Scalar.Zero
        );

    public static LinFloat64Vector2D NegativeE2 { get; }
        = new LinFloat64Vector2D(
            Float64Scalar.Zero,
            Float64Scalar.NegativeOne
        );

    public static LinFloat64Vector2D Symmetric { get; }
        = new LinFloat64Vector2D(
            Float64Scalar.One,
            Float64Scalar.One
        );

    public static LinFloat64Vector2D UnitSymmetric { get; }
        = new LinFloat64Vector2D(
            Float64Scalar.InvSqrt2,
            Float64Scalar.InvSqrt2
        );


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Create(int x, int y)
    {
        return new LinFloat64Vector2D(Float64Scalar.Create(x), Float64Scalar.Create(y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Create(long x, long y)
    {
        return new LinFloat64Vector2D(Float64Scalar.Create(x), Float64Scalar.Create(y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Create(float x, float y)
    {
        return new LinFloat64Vector2D(Float64Scalar.Create(x), Float64Scalar.Create(y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Create(double x, double y)
    {
        return new LinFloat64Vector2D(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Create(Float64Scalar x, Float64Scalar y)
    {
        return new LinFloat64Vector2D(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Create(IPair<double> tuple)
    {
        return new LinFloat64Vector2D(tuple.Item1, tuple.Item2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D Create(IPair<Float64Scalar> tuple)
    {
        return new LinFloat64Vector2D(tuple.Item1, tuple.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D CreateFromPolar(LinFloat64Angle angle)
    {
        return new LinFloat64Vector2D(
            angle.Cos(),
            angle.Sin()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D CreateFromPolar(Float64Scalar length, LinFloat64Angle angle)
    {
        return new LinFloat64Vector2D(
            length * angle.Cos(),
            length * angle.Sin()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D CreateEqualXy(double x)
    {
        var scalar = new Float64Scalar(x);

        return new LinFloat64Vector2D(scalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D CreateUnitVector(double x, double y)
    {
        var s = x * x + y * y;

        if (s.IsZero()) return UnitSymmetric;

        s = 1d / Math.Sqrt(s);

        return new LinFloat64Vector2D(x * s, y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D VectorSymmetric(double vectorLength)
    {
        var scalar = new Float64Scalar(vectorLength / 2d.Sqrt());

        return new LinFloat64Vector2D(scalar, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator -(LinFloat64Vector2D v1)
    {
        return new LinFloat64Vector2D(-v1.X, -v1.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator +(LinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator +(LinFloat64Vector2D v1, ILinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator +(ILinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator -(LinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator -(LinFloat64Vector2D v1, ILinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator -(ILinFloat64Vector2D v1, LinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator *(LinFloat64Vector2D v1, double s)
    {
        return new LinFloat64Vector2D(v1.X * s, v1.Y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator *(double s, LinFloat64Vector2D v1)
    {
        return new LinFloat64Vector2D(v1.X * s, v1.Y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector2D operator /(LinFloat64Vector2D v1, double s)
    {
        s = 1d / s;

        return new LinFloat64Vector2D(v1.X * s, v1.Y * s);
    }


    public int VSpaceDimensions
        => 2;

    public Float64Scalar Scalar
        => Float64Scalar.Zero;

    public Float64Scalar Scalar1
        => X;

    public Float64Scalar Scalar2
        => Y;

    public Float64Scalar Scalar12
        => Float64Scalar.Zero;

    public int Count
        => 4;

    public Float64Scalar X { get; }

    public Float64Scalar Y { get; }

    public Float64Scalar Item1
        => X;

    public Float64Scalar Item2
        => Y;

    /// <summary>
    /// Get the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Float64Scalar this[int index]
    {
        get
        {
            if (index is < 0 or > 3)
                throw new IndexOutOfRangeException();

            return index switch
            {
                1 => X,
                2 => Y,
                _ => Float64Scalar.Zero
            };
        }

    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Vector2D(double x, double y)
    {
        X = Float64Scalar.Create(x);
        Y = Float64Scalar.Create(y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Vector2D(Float64Scalar x, Float64Scalar y)
    {
        X = x;
        Y = y;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Vector2D(IFloat64Scalar x, IFloat64Scalar y)
    {
        X = x.ToScalar();
        Y = y.ToScalar();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out double x, out double y)
    {
        x = X.ScalarValue;
        y = Y.ScalarValue;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return X.IsValid() &&
               Y.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return X.IsZero() &&
               Y.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double zeroEpsilon = 1E-12)
    {
        return Norm().IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Angle GetPolarAngle()
    {
        return LinFloat64PolarAngle.CreateFromVector(X.ScalarValue, Y.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Norm()
    {
        return (X.Square() + Y.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar NormSquared()
    {
        return X.Square() + Y.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Multivector2D ToMultivector2D()
    {
        return LinFloat64Multivector2D.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Negative()
    {
        return new LinFloat64Vector2D(-Scalar1, -Scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D GradeInvolution()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Inverse()
    {
        var normSquared = NormSquared();

        return normSquared.IsZero()
            ? throw new InvalidOperationException()
            : this / normSquared.ScalarValue;
    }


    public LinFloat64Vector2D DirectionToUnitNormal2D(LinFloat64Vector2D? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / norm.ScalarValue;

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

        var s = 1d / normSquared.ScalarValue;

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

        var s = scalingFactor / normSquared.ScalarValue;

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

        var s = 1d / norm.ScalarValue;

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

        var s = 1d / normSquared.ScalarValue;

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

        var s = scalingFactor / normSquared.ScalarValue;

        return Create(
            Scalar2 * s,
            Scalar1 * -s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Dual2D()
    {
        return Create(Scalar2, -Scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Dual2D(double scalingFactor)
    {
        return Create(
            Scalar2 * scalingFactor,
            -Scalar1 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D UnDual2D()
    {
        return Create(-Scalar2, Scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D UnDual2D(double scalingFactor)
    {
        return Create(
            -Scalar2 * scalingFactor,
            Scalar1 * scalingFactor
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Scalar> GetEnumerator()
    {
        yield return Scalar;
        yield return Scalar1;
        yield return Scalar2;
        yield return Scalar12;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToTupleString()
    {
        return $"({X:G10}, {Y:G10})";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({X:G10})<1> + ({Y:G10})<2>";
    }
}