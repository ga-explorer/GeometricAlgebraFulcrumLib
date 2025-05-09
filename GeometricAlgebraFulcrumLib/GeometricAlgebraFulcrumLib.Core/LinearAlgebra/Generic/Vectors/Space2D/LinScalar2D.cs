using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;

public sealed record LinScalar2D<T> :
    ILinMultivector2D<T>,
    IScalar<T>
{
    public static LinScalar2D<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        return new LinScalar2D<T>(scalarProcessor.Zero);
    }

    public static LinScalar2D<T> E0(IScalarProcessor<T> scalarProcessor)
    {
        return new LinScalar2D<T>(scalarProcessor.One);
    }

    public static LinScalar2D<T> NegativeE0(IScalarProcessor<T> scalarProcessor)
    {
        return new LinScalar2D<T>(scalarProcessor.MinusOne);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> Create(Scalar<T> scalar)
    {
        return new LinScalar2D<T>(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> Create(IScalarProcessor<T> scalarProcessor, T scalarValue)
    {
        var scalar = scalarProcessor.ScalarFromValue(scalarValue);

        return new LinScalar2D<T>(scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator +(LinScalar2D<T> v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator -(LinScalar2D<T> v1)
    {
        return new LinScalar2D<T>(-v1.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator +(LinScalar2D<T> v1, double v2)
    {
        return new LinScalar2D<T>(v1.Scalar + v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator +(double v1, LinScalar2D<T> v2)
    {
        return new LinScalar2D<T>(v1 + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator +(LinScalar2D<T> v1, LinScalar2D<T> v2)
    {
        return new LinScalar2D<T>(v1.Scalar + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator -(LinScalar2D<T> v1, double v2)
    {
        return new LinScalar2D<T>(v1.Scalar - v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator -(double v1, LinScalar2D<T> v2)
    {
        return new LinScalar2D<T>(v1 - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator -(LinScalar2D<T> v1, LinScalar2D<T> v2)
    {
        return new LinScalar2D<T>(v1.Scalar - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator *(LinScalar2D<T> v1, double v2)
    {
        return new LinScalar2D<T>(v1.Scalar * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator *(double v1, LinScalar2D<T> v2)
    {
        return new LinScalar2D<T>(v1 * v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator *(LinScalar2D<T> v1, LinScalar2D<T> v2)
    {
        return new LinScalar2D<T>(v1.Scalar * v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator /(LinScalar2D<T> v1, double v2)
    {
        return new LinScalar2D<T>(v1.Scalar / v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator /(double v1, LinScalar2D<T> v2)
    {
        return new LinScalar2D<T>(v1 / v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar2D<T> operator /(LinScalar2D<T> v1, LinScalar2D<T> v2)
    {
        return new LinScalar2D<T>(v1.Scalar / v2.Scalar);
    }

    
    public IScalarProcessor<T> ScalarProcessor 
        => Scalar.ScalarProcessor;
    
    public T ScalarValue 
        => Scalar.ScalarValue;

    public int VSpaceDimensions
        => 3;

    public Scalar<T> Scalar { get; }

    public Scalar<T> Scalar1
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar2
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar12
        => ScalarProcessor.Zero;

    public int Count
        => 4;

    public Scalar<T> this[int index]
    {
        get
        {
            if (index is < 0 or > 3)
                throw new IndexOutOfRangeException();

            return index == 0
                ? Scalar
                : ScalarProcessor.Zero;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinScalar2D(Scalar<T> scalar)
    {
        Scalar = scalar;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Scalar.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return Scalar.IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return Scalar.Abs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return Scalar.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector2D<T> ToMultivector2D()
    {
        return LinMultivector2D<T>.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> Negative()
    {
        return new LinScalar2D<T>(-Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> CliffordConjugate()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> Inverse()
    {
        return IsZero()
            ? throw new InvalidOperationException()
            : new LinScalar2D<T>(Scalar.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> DirectionToUnitNormal2D()
    {
        if (Scalar.IsZero())
            return LinBivector2D<T>.E12(ScalarProcessor);

        return Scalar.IsPositive()
            ? LinBivector2D<T>.E12(ScalarProcessor)
            : LinBivector2D<T>.E21(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> DirectionToNormal2D()
    {
        if (Scalar.IsZero())
            return LinBivector2D<T>.E12(ScalarProcessor);

        return LinBivector2D<T>.Create(1 / Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> DirectionToNormal2D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinBivector2D<T>.E12(ScalarProcessor);

        return LinBivector2D<T>.Create(scalingFactor / Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> NormalToUnitDirection2D()
    {
        if (Scalar.IsZero())
            return LinBivector2D<T>.E12(ScalarProcessor);

        return Scalar.IsPositive()
            ? LinBivector2D<T>.E12(ScalarProcessor)
            : LinBivector2D<T>.E21(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> NormalToDirection2D()
    {
        if (Scalar.IsZero())
            return LinBivector2D<T>.E12(ScalarProcessor);

        return LinBivector2D<T>.Create(1 / Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> NormalToDirection2D(T scalingFactor)
    {
        Debug.Assert(scalingFactor is not null);

        if (Scalar.IsZero())
            return LinBivector2D<T>.E12(ScalarProcessor);

        return LinBivector2D<T>.Create(scalingFactor / Scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> Dual2D()
    {
        return LinBivector2D<T>.Create(-Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> UnDual2D()
    {
        return LinBivector2D<T>.Create(Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ToScalar()
    {
        return Scalar;
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
    public override string ToString()
    {
        return $"({Scalar})<>";
    }

}