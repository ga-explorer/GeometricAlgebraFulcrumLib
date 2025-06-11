using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public sealed record LinScalar3D<T> :
    ILinKVector3D<T>,
    IScalar<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        return new LinScalar3D<T>(scalarProcessor.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> E0(IScalarProcessor<T> scalarProcessor)
    {
        return new LinScalar3D<T>(scalarProcessor.One);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> NegativeE0(IScalarProcessor<T> scalarProcessor)
    {
        return new LinScalar3D<T>(scalarProcessor.MinusOne);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> Create(Scalar<T> scalar)
    {
        return new LinScalar3D<T>(scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator +(LinScalar3D<T> v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator -(LinScalar3D<T> v1)
    {
        return new LinScalar3D<T>(-v1.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator +(LinScalar3D<T> v1, double v2)
    {
        return new LinScalar3D<T>(v1.Scalar + v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator +(double v1, LinScalar3D<T> v2)
    {
        return new LinScalar3D<T>(v1 + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator +(LinScalar3D<T> v1, LinScalar3D<T> v2)
    {
        return new LinScalar3D<T>(v1.Scalar + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator -(LinScalar3D<T> v1, double v2)
    {
        return new LinScalar3D<T>(v1.Scalar - v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator -(double v1, LinScalar3D<T> v2)
    {
        return new LinScalar3D<T>(v1 - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator -(LinScalar3D<T> v1, LinScalar3D<T> v2)
    {
        return new LinScalar3D<T>(v1.Scalar - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator *(LinScalar3D<T> v1, double v2)
    {
        return new LinScalar3D<T>(v1.Scalar * v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator *(double v1, LinScalar3D<T> v2)
    {
        return new LinScalar3D<T>(v1 * v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator *(LinScalar3D<T> v1, LinScalar3D<T> v2)
    {
        return new LinScalar3D<T>(v1.Scalar * v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator /(LinScalar3D<T> v1, double v2)
    {
        return new LinScalar3D<T>(v1.Scalar / v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator /(double v1, LinScalar3D<T> v2)
    {
        return new LinScalar3D<T>(v1 / v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinScalar3D<T> operator /(LinScalar3D<T> v1, LinScalar3D<T> v2)
    {
        return new LinScalar3D<T>(v1.Scalar / v2.Scalar);
    }


    public IScalarProcessor<T> ScalarProcessor 
        => Scalar.ScalarProcessor;
    
    public T ScalarValue 
        => Scalar.ScalarValue;

    public int VSpaceDimensions
        => 3;

    public int Grade
        => 0;

    public Scalar<T> Scalar { get; }

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

    public Scalar<T> Scalar123
        => ScalarProcessor.Zero;

    public int Count
        => 8;

    public Scalar<T> this[int index]
    {
        get
        {
            if (index is < 0 or > 7)
                throw new IndexOutOfRangeException();

            return index == 0
                ? Scalar
                : ScalarProcessor.Zero;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinScalar3D(IScalarProcessor<T> scalarProcessor, T scalar)
    {
        Scalar = scalarProcessor.ScalarFromValue(scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinScalar3D(Scalar<T> scalar)
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
    public LinMultivector3D<T> ToMultivector3D()
    {
        return LinMultivector3D<T>.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> Negative()
    {
        return new LinScalar3D<T>(-Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> CliffordConjugate()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> Inverse()
    {
        return IsZero()
            ? throw new InvalidOperationException()
            : new LinScalar3D<T>(Scalar.Inverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> DirectionToUnitNormal3D()
    {
        if (Scalar.IsZero())
            return LinTrivector3D<T>.E123(ScalarProcessor);

        return Scalar.IsPositive()
            ? LinTrivector3D<T>.E123(ScalarProcessor)
            : LinTrivector3D<T>.NegativeE123(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> DirectionToNormal3D()
    {
        if (Scalar.IsZero())
            return LinTrivector3D<T>.E123(ScalarProcessor);

        return LinTrivector3D<T>.Create(1d / Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> DirectionToNormal3D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinTrivector3D<T>.E123(ScalarProcessor);

        return LinTrivector3D<T>.Create(scalingFactor / Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> NormalToUnitDirection3D()
    {
        if (Scalar.IsZero())
            return LinTrivector3D<T>.E123(ScalarProcessor);

        return Scalar.IsPositive()
            ? LinTrivector3D<T>.E123(ScalarProcessor)
            : LinTrivector3D<T>.NegativeE123(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> NormalToDirection3D()
    {
        if (Scalar.IsZero())
            return LinTrivector3D<T>.E123(ScalarProcessor);

        return LinTrivector3D<T>.Create(1d / Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> NormalToDirection3D(double scalingFactor)
    {
        if (Scalar.IsZero())
            return LinTrivector3D<T>.E123(ScalarProcessor);

        return LinTrivector3D<T>.Create(scalingFactor / Scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> Dual()
    {
        return LinTrivector3D<T>.Create(Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> UnDual()
    {
        return LinTrivector3D<T>.Create(Scalar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ToScalar()
    {
        return Scalar;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sp(LinScalar3D<T> mv2)
    {
        return Scalar * mv2.Scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sp(LinVector3D<T> mv2)
    {
        return ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sp(LinBivector3D<T> mv2)
    {
        return ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sp(LinTrivector3D<T> mv2)
    {
        return ScalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Sp(LinMultivector3D<T> mv2)
    {
        var mv = ScalarProcessor.Zero;

        if (!IsZero() && !mv2.KVector0.IsZero())
            mv += Sp(mv2.KVector0);

        return mv;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinScalar3D<T> Op(LinScalar3D<T> mv2)
    {
        return Create(
            Scalar * mv2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> Op(LinVector3D<T> mv2)
    {
        return LinVector3D<T>.Create(
            Scalar * mv2.Scalar1,
            Scalar * mv2.Scalar2,
            Scalar * mv2.Scalar3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> Op(LinBivector3D<T> mv2)
    {
        return LinBivector3D<T>.Create(
            Scalar * mv2.Scalar12,
            Scalar * mv2.Scalar13,
            Scalar * mv2.Scalar23
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinTrivector3D<T> Op(LinTrivector3D<T> mv2)
    {
        return LinTrivector3D<T>.Create(
            Scalar * mv2.Scalar123
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector3D<T> Op(LinMultivector3D<T> mv2)
    {
        var mv = LinMultivector3D<T>.Zero(ScalarProcessor);

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
        return $"({Scalar})<>";
    }


}