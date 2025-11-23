using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

/// <summary>
/// An immutable 2-tuple of double precision numbers
/// </summary>
public readonly record struct LinFloat64Vector2D :
    ILinFloat64Vector2D,
    ILinFloat64Multivector2D
{
    public static ToleranceComparer DefaultToleranceComparer { get; }
        = new ToleranceComparer(12);


    public sealed record ToleranceComparer :
        IEqualityComparer<LinFloat64Vector2D>
    {
        public int DecimalPlaces { get; }

        public double Tolerance { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ToleranceComparer(int decimalPlaces = 12)
        {
            if (decimalPlaces is < 3 or > 30)
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces));

            DecimalPlaces = decimalPlaces;
            Tolerance = Math.Pow(10, -decimalPlaces);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ToleranceComparer(double tolerance = Float64Utils.DefaultTolerance)
            : this((int)Math.Ceiling(-Math.Abs(tolerance).Log10()))
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(LinFloat64Vector2D a, LinFloat64Vector2D b)
        {
            return (a - b).L2Norm() < Tolerance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(LinFloat64Vector2D v)
        {
            return HashCode.Combine(
                Math.Round(v.X, DecimalPlaces - 3), 
                Math.Round(v.Y, DecimalPlaces - 3)
            );
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<LinFloat64Vector2D, T> CreateToleranceDictionary<T>(int decimalPlaces = 12)
    {
        return new Dictionary<LinFloat64Vector2D, T>(
            new ToleranceComparer(decimalPlaces)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Dictionary<LinFloat64Vector2D, T> CreateToleranceDictionary<T>(double tolerance = Float64Utils.DefaultTolerance)
    {
        return new Dictionary<LinFloat64Vector2D, T>(
            new ToleranceComparer(tolerance)
        );
    }


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
    public static LinFloat64Vector2D Create(IPair<double> tuple)
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
    public static implicit operator (double, double)(LinFloat64Vector2D v)
    {
        return (v.X, v.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinFloat64Vector2D((double, double) v)
    {
        return Create(v.Item1, v.Item2);
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
                _ => 0
            };
        }

    }
    
    Float64Scalar IReadOnlyList<Float64Scalar>.this[int index] 
        => this[index];


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Vector2D(double x, double y)
    {
        X = x;
        Y = y;

        Debug.Assert(IsValid());
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
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
    public bool IsNearZero(double tolerance = Float64Utils.DefaultTolerance)
    {
        return L2Norm().IsNearZero(tolerance);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Angle GetPolarAngle()
    {
        return LinFloat64PolarAngle.CreateFromVector(X, Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double L2Norm()
    {
        return (X.Square() + Y.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public double L2NormSquared()
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
        var normSquared = L2NormSquared();

        return normSquared.IsZero()
            ? throw new InvalidOperationException()
            : this / normSquared;
    }


    public LinFloat64Vector2D DirectionToUnitNormal2D(LinFloat64Vector2D? zeroNormal = null)
    {
        var norm = L2Norm();

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
        var normSquared = L2NormSquared();

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
        var normSquared = L2NormSquared();

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
        var norm = L2NormSquared();

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
        var normSquared = L2NormSquared();

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
        var normSquared = L2NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = scalingFactor / normSquared;

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
    public Float64Scalar Sp(LinFloat64Scalar2D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Sp(LinFloat64Vector2D mv2)
    {
        return Scalar1 * mv2.Scalar1 +
               Scalar2 * mv2.Scalar2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Sp(LinFloat64Bivector2D mv2)
    {
        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Sp(LinFloat64Multivector2D mv2)
    {
        var mv = 0d;

        if (!IsZero() && !mv2.KVector1.IsZero())
            mv += Sp(mv2.KVector1);

        return mv;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Op(LinFloat64Scalar2D mv2)
    {
        return Create(
            Scalar1 * mv2.Scalar,
            Scalar2 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D Op(LinFloat64Vector2D mv2)
    {
        return LinFloat64Bivector2D.Create(
            Scalar1 * mv2.Scalar2 - Scalar2 * mv2.Scalar1
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Scalar2D Op(LinFloat64Bivector2D mv2)
    {
        return LinFloat64Scalar2D.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Lcp(LinFloat64Bivector2D b2)
    {
        var s1 =
            -Scalar2 * b2.Scalar12;

        var s2 =
            Scalar1 * b2.Scalar12;

        return Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D ProjectOn(LinFloat64Bivector2D mv2)
    {
        return Lcp(mv2).Lcp(mv2.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D Gp(LinFloat64Bivector2D mv2)
    {
        var s1 =
            -Scalar2 * mv2.Scalar12;

        var s2 =
            Scalar1 * mv2.Scalar12;

        return Create(s1, s2);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public DenseMatrix OuterProduct(LinFloat64Vector2D v)
    {
        var m = DenseMatrix.Create(2, 2, 0.0);

        m[0, 0] = X * v.X;
        m[0, 1] = X * v.Y;
        
        m[1, 0] = Y * v.X;
        m[1, 1] = Y * v.Y;
        
        return m;
    }


    public LinFloat64Vector2D ElementWiseMin(LinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(
            Math.Min(X, v2.X),
            Math.Min(Y, v2.Y)
        );
    }
    
    public LinFloat64Vector2D ElementWiseMax(LinFloat64Vector2D v2)
    {
        return new LinFloat64Vector2D(
            Math.Max(X, v2.X),
            Math.Max(Y, v2.Y)
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
    public string ToTupleString(int decimalPlaces = 12)
    {
        return $"({double.Round(X, decimalPlaces):G10}, {double.Round(Y, decimalPlaces):G10})";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({X:G10})<1> + ({Y:G10})<2>";
    }

}