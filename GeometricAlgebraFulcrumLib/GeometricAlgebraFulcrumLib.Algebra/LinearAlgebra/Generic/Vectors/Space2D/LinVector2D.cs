using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

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
    public static LinVector2D<T> Create(Scalar<T> x, Scalar<T> y)
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
    private LinVector2D(Scalar<T> x, Scalar<T> y)
    {
        X = x;
        Y = y;
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