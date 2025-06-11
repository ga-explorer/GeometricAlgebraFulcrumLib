using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

/// <summary>
/// An immutable 2-tuple of scalars
/// </summary>
public sealed record LinVector2D<T> :
    ILinVector2D<T>,
    ILinMultivector2D<T>
{
    public static LinVector2D<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector2D<T>(
            scalarProcessor.Zero,
            scalarProcessor.Zero
        );
    }

    public static LinVector2D<T> E1(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector2D<T>(
            scalarProcessor.One,
            scalarProcessor.Zero
        );
    }

    public static LinVector2D<T> E2(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector2D<T>(
            scalarProcessor.Zero,
            scalarProcessor.One
        );
    }

    public static LinVector2D<T> NegativeE1(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector2D<T>(
            scalarProcessor.MinusOne,
            scalarProcessor.Zero
        );
    }

    public static LinVector2D<T> NegativeE2(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector2D<T>(
            scalarProcessor.Zero,
            scalarProcessor.MinusOne
        );
    }

    public static LinVector2D<T> Symmetric(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVector2D<T>(
            scalarProcessor.One,
            scalarProcessor.One
        );
    }

    public static LinVector2D<T> UnitSymmetric(IScalarProcessor<T> scalarProcessor)
    {
        var invSqrt2 = scalarProcessor.One / scalarProcessor.Sqrt(scalarProcessor.TwoValue);
        
        return new LinVector2D<T>(invSqrt2, invSqrt2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Create(IScalarProcessor<T> scalarProcessor, int x, int y)
    {
        return new LinVector2D<T>(
            scalarProcessor.ScalarFromNumber(x), 
            scalarProcessor.ScalarFromNumber(y)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Create(IScalarProcessor<T> scalarProcessor, uint x, uint y)
    {
        return new LinVector2D<T>(
            scalarProcessor.ScalarFromNumber(x), 
            scalarProcessor.ScalarFromNumber(y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Create(IScalarProcessor<T> scalarProcessor, long x, long y)
    {
        return new LinVector2D<T>(
            scalarProcessor.ScalarFromNumber(x), 
            scalarProcessor.ScalarFromNumber(y)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Create(IScalarProcessor<T> scalarProcessor, ulong x, ulong y)
    {
        return new LinVector2D<T>(
            scalarProcessor.ScalarFromNumber(x), 
            scalarProcessor.ScalarFromNumber(y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Create(IScalarProcessor<T> scalarProcessor, float x, float y)
    {
        return new LinVector2D<T>(
            scalarProcessor.ScalarFromNumber(x), 
            scalarProcessor.ScalarFromNumber(y)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Create(IScalarProcessor<T> scalarProcessor, double x, double y)
    {
        return new LinVector2D<T>(
            scalarProcessor.ScalarFromNumber(x), 
            scalarProcessor.ScalarFromNumber(y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Create(IScalarProcessor<T> scalarProcessor, T x, T y)
    {
        return new LinVector2D<T>(
            scalarProcessor.ScalarFromValue(x), 
            scalarProcessor.ScalarFromValue(y)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Create(IScalar<T> x, IScalar<T> y)
    {
        return new LinVector2D<T>(x, y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> Create(IPair<Scalar<T>> tuple)
    {
        return new LinVector2D<T>(tuple.Item1, tuple.Item2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> CreateFromPolar(LinAngle<T> angle)
    {
        return new LinVector2D<T>(
            angle.Cos(),
            angle.Sin()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> CreateFromPolar(Scalar<T> length, LinAngle<T> angle)
    {
        return new LinVector2D<T>(
            length * angle.Cos(),
            length * angle.Sin()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> CreateEqualXy(Scalar<T> x)
    {
        return new LinVector2D<T>(x, x);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> CreateUnitVector(Scalar<T> x, Scalar<T> y)
    {
        var s = x * x + y * y;

        if (s.IsZero()) return UnitSymmetric(x.ScalarProcessor);

        s = 1 / s.Sqrt();

        return new LinVector2D<T>(x * s, y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> VectorSymmetric(Scalar<T> vectorLength)
    {
        var scalar = vectorLength / 2d.Sqrt();

        return new LinVector2D<T>(scalar, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator -(LinVector2D<T> v1)
    {
        return new LinVector2D<T>(-v1.X, -v1.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator +(LinVector2D<T> v1, LinVector2D<T> v2)
    {
        return new LinVector2D<T>(v1.X + v2.X, v1.Y + v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator +(LinVector2D<T> v1, ILinVector2D<T> v2)
    {
        return new LinVector2D<T>(v1.X + v2.X, v1.Y + v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator +(ILinVector2D<T> v1, LinVector2D<T> v2)
    {
        return new LinVector2D<T>(v1.X + v2.X, v1.Y + v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator -(LinVector2D<T> v1, LinVector2D<T> v2)
    {
        return new LinVector2D<T>(v1.X - v2.X, v1.Y - v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator -(LinVector2D<T> v1, ILinVector2D<T> v2)
    {
        return new LinVector2D<T>(v1.X - v2.X, v1.Y - v2.Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator -(ILinVector2D<T> v1, LinVector2D<T> v2)
    {
        return new LinVector2D<T>(v1.X - v2.X, v1.Y - v2.Y);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(LinVector2D<T> v1, int s)
    {
        var s1 = v1.ScalarProcessor.ScalarFromNumber(s);

        return new LinVector2D<T>(v1.X * s1, v1.Y * s1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(LinVector2D<T> v1, float s)
    {
        var s1 = v1.ScalarProcessor.ScalarFromNumber(s);

        return new LinVector2D<T>(v1.X * s1, v1.Y * s1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(LinVector2D<T> v1, double s)
    {
        var s1 = v1.ScalarProcessor.ScalarFromNumber(s);

        return new LinVector2D<T>(v1.X * s1, v1.Y * s1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(LinVector2D<T> v1, T s)
    {
        var s1 = v1.ScalarProcessor.ScalarFromValue(s);

        return new LinVector2D<T>(v1.X * s1, v1.Y * s1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(LinVector2D<T> v1, Scalar<T> s)
    {
        return new LinVector2D<T>(v1.X * s, v1.Y * s);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(int s, LinVector2D<T> v1)
    {
        return new LinVector2D<T>(v1.X * s, v1.Y * s);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(float s, LinVector2D<T> v1)
    {
        return new LinVector2D<T>(v1.X * s, v1.Y * s);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(double s, LinVector2D<T> v1)
    {
        return new LinVector2D<T>(v1.X * s, v1.Y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(T s, LinVector2D<T> v1)
    {
        Debug.Assert(s is not null);

        return new LinVector2D<T>(v1.X * s, v1.Y * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator *(Scalar<T> s, LinVector2D<T> v1)
    {
        return new LinVector2D<T>(v1.X * s, v1.Y * s);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator /(LinVector2D<T> v1, int s)
    {
        return new LinVector2D<T>(v1.X / s, v1.Y / s);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator /(LinVector2D<T> v1, float s)
    {
        return new LinVector2D<T>(v1.X / s, v1.Y / s);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator /(LinVector2D<T> v1, double s)
    {
        return new LinVector2D<T>(v1.X / s, v1.Y / s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator /(LinVector2D<T> v1, T s)
    {
        Debug.Assert(s is not null);

        return new LinVector2D<T>(v1.X / s, v1.Y / s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVector2D<T> operator /(LinVector2D<T> v1, Scalar<T> s)
    {
        return new LinVector2D<T>(v1.X / s, v1.Y / s);
    }

    
    public IScalarProcessor<T> ScalarProcessor 
        => X.ScalarProcessor;

    public int VSpaceDimensions
        => 2;

    public Scalar<T> Scalar
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar1
        => X;

    public Scalar<T> Scalar2
        => Y;

    public Scalar<T> Scalar12
        => ScalarProcessor.Zero;

    public int Count
        => 4;

    public Scalar<T> X { get; }

    public Scalar<T> Y { get; }

    public Scalar<T> Item1
        => X;

    public Scalar<T> Item2
        => Y;

    /// <summary>
    /// Get the ith component of this tuple
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Scalar<T> this[int index]
    {
        get
        {
            if (index is < 0 or > 3)
                throw new IndexOutOfRangeException();

            return index switch
            {
                1 => X,
                2 => Y,
                _ => X.ScalarProcessor.Zero
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinVector2D(IScalar<T> x, IScalar<T> y)
    {
        X = x.ToScalar();
        Y = y.ToScalar();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out Scalar<T> x, out Scalar<T> y)
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
    public bool IsNearZero()
    {
        return Norm().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinPolarAngle<T> GetPolarAngle()
    {
        return X.ArcTan2(Y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return (X.Square() + Y.Square()).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return X.Square() + Y.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector2D<T> ToMultivector2D()
    {
        return LinMultivector2D<T>.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Negative()
    {
        return new LinVector2D<T>(-Scalar1, -Scalar2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> GradeInvolution()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Inverse()
    {
        var normSquared = NormSquared();

        return normSquared.IsZero()
            ? throw new InvalidOperationException()
            : this / normSquared;
    }


    public LinVector2D<T> DirectionToUnitNormal2D(LinVector2D<T>? zeroNormal = null)
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

    public LinVector2D<T> DirectionToNormal2D(LinVector2D<T>? zeroNormal = null)
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

    public LinVector2D<T> DirectionToNormal2D(Scalar<T> scalingFactor, LinVector2D<T>? zeroNormal = null)
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

    public LinVector2D<T> NormalToUnitDirection2D(LinVector2D<T>? zeroNormal = null)
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

    public LinVector2D<T> NormalToDirection2D(LinVector2D<T>? zeroNormal = null)
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

    public LinVector2D<T> NormalToDirection2D(Scalar<T> scalingFactor, LinVector2D<T>? zeroNormal = null)
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Dual2D()
    {
        return Create(Scalar2, -Scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Dual2D(Scalar<T> scalingFactor)
    {
        return Create(
            Scalar2 * scalingFactor,
            -Scalar1 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> UnDual2D()
    {
        return Create(-Scalar2, Scalar1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> UnDual2D(Scalar<T> scalingFactor)
    {
        return Create(
            -Scalar2 * scalingFactor,
            Scalar1 * scalingFactor
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sp(LinScalar2D<T> mv2)
    {
        return ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sp(LinVector2D<T> mv2)
    {
        return Scalar1 * mv2.Scalar1 +
               Scalar2 * mv2.Scalar2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sp(LinBivector2D<T> mv2)
    {
        return ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sp(LinMultivector2D<T> mv2)
    {
        var mv = ScalarProcessor.Zero;

        if (!IsZero() && !mv2.KVector1.IsZero())
            mv += Sp(mv2.KVector1);

        return mv;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Op(LinScalar2D<T> mv2)
    {
        return Create(
            Scalar1 * mv2.Scalar,
            Scalar2 * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> Op(LinVector2D<T> mv2)
    {
        return LinBivector2D<T>.Create(
            Scalar1 * mv2.Scalar2 - Scalar2 * mv2.Scalar1
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> Op(LinBivector2D<T> mv2)
    {
        return LinScalar2D<T>.Zero(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector2D<T> Op(LinMultivector2D<T> mv2)
    {
        var mv = LinMultivector2D<T>.Zero(ScalarProcessor);

        if (!mv2.KVector0.IsZero())
            mv += Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += Op(mv2.KVector1);

        if (!mv2.KVector2.IsZero())
            mv += Op(mv2.KVector2);

        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Lcp(LinBivector2D<T> b2)
    {
        var s1 =
            -Scalar2 * b2.Scalar12;

        var s2 =
            Scalar1 * b2.Scalar12;

        return Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Rcp(LinVector2D<T> v2)
    {
        var s1 =
            Scalar12 * v2.Scalar2;

        var s2 =
            -Scalar12 * v2.Scalar1;

        return Create(s1, s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> ProjectOn(LinBivector2D<T> mv2)
    {
        return Lcp(mv2).Lcp(mv2.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> Gp(LinBivector2D<T> mv2)
    {
        var s1 =
            -Scalar2 * mv2.Scalar12;

        var s2 =
            Scalar1 * mv2.Scalar12;

        return Create(s1, s2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Scalar<T>> GetEnumerator()
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
        //var xText = ScalarProcessor.ToText(X.ScalarValue);

        return $"({X}, {Y})";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({X})<1> + ({Y})<2>";
    }
}