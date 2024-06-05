using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;

public sealed record LinBivector2D<T> :
    ILinMultivector2D<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector2D<T>(scalarProcessor.Zero);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> E12(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector2D<T>(scalarProcessor.One);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> E21(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector2D<T>(scalarProcessor.MinusOne);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> Create(Scalar<T> scalar12)
    {
        return new LinBivector2D<T>(scalar12);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> operator +(LinBivector2D<T> mv1)
    {
        return mv1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> operator -(LinBivector2D<T> mv1)
    {
        return new LinBivector2D<T>(-mv1.Scalar12);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> operator +(LinBivector2D<T> mv1, LinBivector2D<T> mv2)
    {
        return new LinBivector2D<T>(mv1.Scalar12 + mv2.Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> operator -(LinBivector2D<T> mv1, LinBivector2D<T> mv2)
    {
        return new LinBivector2D<T>(mv1.Scalar12 - mv2.Scalar12);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> operator *(LinBivector2D<T> mv1, double mv2)
    {
        return new LinBivector2D<T>(mv1.Scalar12 * mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> operator *(double mv1, LinBivector2D<T> mv2)
    {
        return new LinBivector2D<T>(mv1 * mv2.Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector2D<T> operator /(LinBivector2D<T> mv1, double mv2)
    {
        mv2 = 1d / mv2;

        return new LinBivector2D<T>(mv1.Scalar12 * mv2);
    }


    public IScalarProcessor<T> ScalarProcessor 
        => Scalar12.ScalarProcessor;

    public int VSpaceDimensions
        => 2;

    public T Item1
        => Scalar12.ScalarValue;

    public Scalar<T> Xy
        => Scalar12;

    public Scalar<T> Yx
        => -Scalar12;

    public Scalar<T> Scalar
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar1
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar2
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar12 { get; }

    public int Count
        => 4;

    /// <summary>
    /// Get or set the ith component of this multivector
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
                3 => Scalar12,
                _ => ScalarProcessor.Zero
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinBivector2D(IScalar<T> scalar12)
    {
        Scalar12 = scalar12.ToScalar();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar12.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Scalar12.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return Norm().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return NormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return Scalar12.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> ToUnitBivector(bool zeroAsSymmetric = true)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroAsSymmetric ? E12(ScalarProcessor) : Zero(ScalarProcessor);

        return E12(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector2D<T> ToMultivector2D()
    {
        return LinMultivector2D<T>.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> Negative()
    {
        return new LinBivector2D<T>(-Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> Negative(T scalingFactor)
    {
        Debug.Assert(scalingFactor is not null);

        return new LinBivector2D<T>(
            -Scalar12 * scalingFactor
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> Negative(Scalar<T> scalingFactor)
    {
        return new LinBivector2D<T>(
            -Scalar12.Times(scalingFactor)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> Negative(IScalar<T> scalingFactor)
    {
        return new LinBivector2D<T>(
            -Scalar12.Times(scalingFactor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector2D<T> Inverse()
    {
        return Negative(-1 / NormSquared());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> DirectionToUnitNormal2D()
    {
        if (Scalar12.IsZero())
            return LinScalar2D<T>.E0(ScalarProcessor);

        return Scalar12.IsPositive()
            ? LinScalar2D<T>.E0(ScalarProcessor)
            : LinScalar2D<T>.NegativeE0(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> DirectionToNormal2D()
    {
        if (Scalar12.IsZero())
            return LinScalar2D<T>.E0(ScalarProcessor);

        return LinScalar2D<T>.Create(1d / Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> DirectionToNormal2D(T scalingFactor)
    {
        Debug.Assert(scalingFactor is not null);

        if (Scalar12.IsZero())
            return LinScalar2D<T>.E0(ScalarProcessor);

        return LinScalar2D<T>.Create(scalingFactor / Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> NormalToUnitDirection2D()
    {
        if (Scalar12.IsZero())
            return LinScalar2D<T>.E0(ScalarProcessor);

        return Scalar12.IsPositive()
            ? LinScalar2D<T>.E0(ScalarProcessor)
            : LinScalar2D<T>.NegativeE0(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> NormalToDirection2D()
    {
        if (Scalar12.IsZero())
            return LinScalar2D<T>.E0(ScalarProcessor);

        return LinScalar2D<T>.Create(1d / Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> NormalToDirection2D(T scalingFactor)
    {
        Debug.Assert(scalingFactor is not null);

        if (Scalar12.IsZero())
            return LinScalar2D<T>.E0(ScalarProcessor);

        return LinScalar2D<T>.Create(scalingFactor / Scalar12);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> Dual2D()
    {
        return LinScalar2D<T>.Create(Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> Dual2D(T scalingFactor)
    {
        Debug.Assert(scalingFactor is not null);

        return LinScalar2D<T>.Create(
            Scalar12 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> UnDual2D()
    {
        return LinScalar2D<T>.Create(-Scalar12);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar2D<T> UnDual2D(T scalingFactor)
    {
        Debug.Assert(scalingFactor is not null);

        return LinScalar2D<T>.Create(
            -Scalar12 * scalingFactor
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
    public override string ToString()
    {
        return $"({Scalar12})<1,2>";
    }

}