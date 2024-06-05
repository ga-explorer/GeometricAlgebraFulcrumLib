using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public sealed record LinTrivector3D<T> :
    ILinKVector3D<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        return new LinTrivector3D<T>(scalarProcessor.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> E123(IScalarProcessor<T> scalarProcessor)
    {
        return new LinTrivector3D<T>(scalarProcessor.One);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> NegativeE123(IScalarProcessor<T> scalarProcessor)
    {
        return new LinTrivector3D<T>(scalarProcessor.MinusOne);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> InverseE123(IScalarProcessor<T> scalarProcessor)
    {
        return new LinTrivector3D<T>(scalarProcessor.MinusOne);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> Create(IScalarProcessor<T> scalarProcessor, T scalar123)
    {
        return new LinTrivector3D<T>(scalarProcessor, scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> Create(Scalar<T> scalar123)
    {
        return new LinTrivector3D<T>(scalar123);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> operator +(LinTrivector3D<T> mv)
    {
        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> operator -(LinTrivector3D<T> mv)
    {
        return new LinTrivector3D<T>(
            -mv.Scalar123
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> operator +(LinTrivector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return new LinTrivector3D<T>(
            mv1.Scalar123 + mv2.Scalar123
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> operator -(LinTrivector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return new LinTrivector3D<T>(
            mv1.Scalar123 - mv2.Scalar123
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> operator *(LinTrivector3D<T> mv1, double mv2)
    {
        return new LinTrivector3D<T>(
            mv1.Scalar123 * mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> operator *(double mv1, LinTrivector3D<T> mv2)
    {
        return new LinTrivector3D<T>(
            mv1 * mv2.Scalar123
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> operator /(LinTrivector3D<T> mv1, double mv2)
    {
        return new LinTrivector3D<T>(
            mv1.Scalar123 / mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinTrivector3D<T> operator /(double mv1, LinTrivector3D<T> mv2)
    {
        return new LinTrivector3D<T>(
            mv1 / -mv2.Scalar123
        );
    }


    public IScalarProcessor<T> ScalarProcessor 
        => Scalar123.ScalarProcessor;

    public int VSpaceDimensions
        => 3;

    public int Grade
        => 3;

    public Scalar<T> Scalar
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar1
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar2
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar3
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar12
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar13
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar23
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar123 { get; }

    public int Count
        => 8;

    public Scalar<T> this[int index]
    {
        get
        {
            if (index is < 0 or > 7)
                throw new IndexOutOfRangeException();

            return index == 7
                ? Scalar123
                : ScalarProcessor.Zero;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinTrivector3D(IScalarProcessor<T> scalarProcessor, T scalar123)
    {
        Scalar123 = scalarProcessor.ScalarFromValue(scalar123);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinTrivector3D(Scalar<T> scalar123)
    {
        Scalar123 = scalar123;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar123.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Scalar123.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return Scalar123.IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return Scalar123.Abs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return Scalar123.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector3D<T> ToMultivector3D()
    {
        return LinMultivector3D<T>.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> Negative()
    {
        return new LinTrivector3D<T>(-Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> GradeInvolution()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> CliffordConjugate()
    {
        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> DirectionToUnitNormal3D()
    {
        if (Scalar123.IsZero())
            return LinScalar3D<T>.E0(ScalarProcessor);

        return Scalar123.IsPositive()
            ? LinScalar3D<T>.E0(ScalarProcessor)
            : LinScalar3D<T>.NegativeE0(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> DirectionToNormal3D()
    {
        if (Scalar123.IsZero())
            return LinScalar3D<T>.E0(ScalarProcessor);

        return LinScalar3D<T>.Create(1d / Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> DirectionToNormal3D(double scalingFactor)
    {
        if (Scalar123.IsZero())
            return LinScalar3D<T>.E0(ScalarProcessor);

        return LinScalar3D<T>.Create(scalingFactor / Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> NormalToUnitDirection3D()
    {
        if (Scalar123.IsZero())
            return LinScalar3D<T>.E0(ScalarProcessor);

        return Scalar123.IsPositive()
            ? LinScalar3D<T>.E0(ScalarProcessor)
            : LinScalar3D<T>.NegativeE0(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> NormalToDirection3D()
    {
        if (Scalar123.IsZero())
            return LinScalar3D<T>.E0(ScalarProcessor);

        return LinScalar3D<T>.Create(1d / Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> NormalToDirection3D(double scalingFactor)
    {
        if (Scalar123.IsZero())
            return LinScalar3D<T>.E0(ScalarProcessor);

        return LinScalar3D<T>.Create(scalingFactor / Scalar123);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> Dual3D()
    {
        return LinScalar3D<T>.Create(-Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> Dual3D(double scalingFactor)
    {
        return LinScalar3D<T>.Create(-Scalar123 * scalingFactor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> UnDual3D()
    {
        return LinScalar3D<T>.Create(Scalar123);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> UnDual3D(double scalingFactor)
    {
        return LinScalar3D<T>.Create(Scalar123 * scalingFactor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Scalar<T>> GetEnumerator()
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({Scalar123})<1,2,3>";
    }

}