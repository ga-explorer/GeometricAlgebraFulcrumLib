using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public sealed record LinMultivector3D<T> :
    ILinMultivector3D<T>
{
    public static LinMultivector3D<T> Zero(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(scalarProcessor);

    public static LinMultivector3D<T> E0(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinScalar3D<T>.E0(scalarProcessor));

    public static LinMultivector3D<T> E1(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinVector3D<T>.E1(scalarProcessor));

    public static LinMultivector3D<T> E2(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinVector3D<T>.E2(scalarProcessor));

    public static LinMultivector3D<T> E3(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinVector3D<T>.E3(scalarProcessor));

    public static LinMultivector3D<T> E12(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinBivector3D<T>.E12(scalarProcessor));

    public static LinMultivector3D<T> E21(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinBivector3D<T>.E21(scalarProcessor));

    public static LinMultivector3D<T> E13(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinBivector3D<T>.E13(scalarProcessor));

    public static LinMultivector3D<T> E31(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinBivector3D<T>.E31(scalarProcessor));

    public static LinMultivector3D<T> E23(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinBivector3D<T>.E23(scalarProcessor));

    public static LinMultivector3D<T> E32(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinBivector3D<T>.E32(scalarProcessor));

    public static LinMultivector3D<T> E123(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinTrivector3D<T>.E123(scalarProcessor));

    public static LinMultivector3D<T> NegativeE0(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinScalar3D<T>.NegativeE0(scalarProcessor));

    public static LinMultivector3D<T> NegativeE1(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinVector3D<T>.NegativeE1(scalarProcessor));

    public static LinMultivector3D<T> NegativeE2(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinVector3D<T>.NegativeE2(scalarProcessor));

    public static LinMultivector3D<T> NegativeE3(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinVector3D<T>.NegativeE3(scalarProcessor));

    public static LinMultivector3D<T> NegativeE123(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinTrivector3D<T>.NegativeE123(scalarProcessor));

    public static LinMultivector3D<T> InverseE123(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector3D<T>(LinTrivector3D<T>.InverseE123(scalarProcessor));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Create(LinScalar3D<T> kVector)
    {
        return new LinMultivector3D<T>(kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Create(LinVector3D<T> kVector)
    {
        return new LinMultivector3D<T>(kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Create(LinBivector3D<T> kVector)
    {
        return new LinMultivector3D<T>(kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Create(LinTrivector3D<T> kVector)
    {
        return new LinMultivector3D<T>(kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> Create(LinScalar3D<T> kVector0, LinVector3D<T> kVector1, LinBivector3D<T> kVector2, LinTrivector3D<T> kVector3)
    {
        return new LinMultivector3D<T>(
            kVector0,
            kVector1,
            kVector2,
            kVector3
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinMultivector3D<T>(LinScalar3D<T> mv)
    {
        return new LinMultivector3D<T>(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinMultivector3D<T>(LinVector3D<T> mv)
    {
        return new LinMultivector3D<T>(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinMultivector3D<T>(LinBivector3D<T> mv)
    {
        return new LinMultivector3D<T>(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinMultivector3D<T>(LinTrivector3D<T> mv)
    {
        return new LinMultivector3D<T>(mv);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator +(LinMultivector3D<T> mv)
    {
        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator -(LinMultivector3D<T> mv)
    {
        return new LinMultivector3D<T>(
            -mv.KVector0,
            -mv.KVector1,
            -mv.KVector2,
            -mv.KVector3
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator +(LinMultivector3D<T> mv1, LinScalar3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1.KVector0 + mv2,
            mv1.KVector1,
            mv1.KVector2,
            mv1.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator +(LinMultivector3D<T> mv1, LinVector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1.KVector0,
            mv1.KVector1 + mv2,
            mv1.KVector2,
            mv1.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator +(LinMultivector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2 + mv2,
            mv1.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator +(LinMultivector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2,
            mv1.KVector3 + mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator +(LinScalar3D<T> mv1, LinMultivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1 + mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator +(LinVector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv2.KVector0,
            mv1 + mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator +(LinBivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv2.KVector0,
            mv2.KVector1,
            mv1 + mv2.KVector2,
            mv2.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator +(LinTrivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv1 + mv2.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator +(LinMultivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1.KVector0 + mv2.KVector0,
            mv1.KVector1 + mv2.KVector1,
            mv1.KVector2 + mv2.KVector2,
            mv1.KVector3 + mv2.KVector3
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator -(LinMultivector3D<T> mv1, LinScalar3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1.KVector0 - mv2,
            mv1.KVector1,
            mv1.KVector2,
            mv1.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator -(LinMultivector3D<T> mv1, LinVector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1.KVector0,
            mv1.KVector1 - mv2,
            mv1.KVector2,
            mv1.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator -(LinMultivector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2 - mv2,
            mv1.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator -(LinMultivector3D<T> mv1, LinTrivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2,
            mv1.KVector3 - mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator -(LinScalar3D<T> mv1, LinMultivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1 - mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator -(LinVector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv2.KVector0,
            mv1 - mv2.KVector1,
            mv2.KVector2,
            mv2.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator -(LinBivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv2.KVector0,
            mv2.KVector1,
            mv1 - mv2.KVector2,
            mv2.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator -(LinTrivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2,
            mv1 - mv2.KVector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector3D<T> operator -(LinMultivector3D<T> mv1, LinMultivector3D<T> mv2)
    {
        return new LinMultivector3D<T>(
            mv1.KVector0 - mv2.KVector0,
            mv1.KVector1 - mv2.KVector1,
            mv1.KVector2 - mv2.KVector2,
            mv1.KVector3 - mv2.KVector3
        );
    }


    public IScalarProcessor<T> ScalarProcessor 
        => KVector0.ScalarProcessor;

    public int VSpaceDimensions
        => 3;

    public LinScalar3D<T> KVector0 { get; }

    public LinVector3D<T> KVector1 { get; }

    public LinBivector3D<T> KVector2 { get; }

    public LinTrivector3D<T> KVector3 { get; }

    public Scalar<T> Scalar
        => KVector0.Scalar;

    public Scalar<T> Scalar1
        => KVector1.Scalar1;

    public Scalar<T> Scalar2
        => KVector1.Scalar2;

    public Scalar<T> Scalar3
        => KVector1.Scalar3;

    public Scalar<T> Scalar12
        => KVector2.Scalar12;

    public Scalar<T> Scalar13
        => KVector2.Scalar13;

    public Scalar<T> Scalar23
        => KVector2.Scalar23;

    public Scalar<T> Scalar123
        => KVector3.Scalar123;

    public int Count
        => 8;

    public Scalar<T> this[int grade, int index]
    {
        get
        {
            return grade switch
            {
                0 => index == 0
                    ? KVector0.Scalar
                    : throw new IndexOutOfRangeException(nameof(index)),

                1 => index switch
                {
                    0 => KVector1.Scalar1,
                    1 => KVector1.Scalar2,
                    2 => KVector1.Scalar3,
                    _ => throw new IndexOutOfRangeException(nameof(index))
                },

                2 => index switch
                {
                    0 => KVector2.Scalar12,
                    1 => KVector2.Scalar13,
                    2 => KVector2.Scalar23,
                    _ => throw new IndexOutOfRangeException(nameof(index))
                },

                3 => index == 0
                    ? KVector3.Scalar123
                    : throw new IndexOutOfRangeException(nameof(index)),

                _ => throw new IndexOutOfRangeException(nameof(index))
            };
        }
    }

    public Scalar<T> this[int id]
    {
        get
        {
            return id switch
            {
                0 => KVector0.Scalar,

                1 => KVector1.Scalar1,
                2 => KVector1.Scalar2,
                4 => KVector1.Scalar3,

                3 => KVector2.Scalar12,
                5 => KVector2.Scalar13,
                6 => KVector2.Scalar23,

                7 => KVector3.Scalar123,

                _ => throw new IndexOutOfRangeException(nameof(id))
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector3D(IScalarProcessor<T> scalarProcessor)
    {
        KVector0 = LinScalar3D<T>.Zero(scalarProcessor);
        KVector1 = LinVector3D<T>.Zero(scalarProcessor);
        KVector2 = LinBivector3D<T>.Zero(scalarProcessor);
        KVector3 = LinTrivector3D<T>.Zero(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector3D(LinScalar3D<T> kVector0)
    {
        KVector0 = kVector0;
        KVector1 = LinVector3D<T>.Zero(kVector0.ScalarProcessor);
        KVector2 = LinBivector3D<T>.Zero(kVector0.ScalarProcessor);
        KVector3 = LinTrivector3D<T>.Zero(kVector0.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector3D(LinVector3D<T> kVector1)
    {
        KVector0 = LinScalar3D<T>.Zero(kVector1.ScalarProcessor);
        KVector1 = kVector1;
        KVector2 = LinBivector3D<T>.Zero(kVector1.ScalarProcessor);
        KVector3 = LinTrivector3D<T>.Zero(kVector1.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector3D(LinBivector3D<T> kVector2)
    {
        KVector0 = LinScalar3D<T>.Zero(kVector2.ScalarProcessor);
        KVector1 = LinVector3D<T>.Zero(kVector2.ScalarProcessor);
        KVector2 = kVector2;
        KVector3 = LinTrivector3D<T>.Zero(kVector2.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector3D(LinTrivector3D<T> kVector3)
    {
        KVector0 = LinScalar3D<T>.Zero(kVector3.ScalarProcessor);
        KVector1 = LinVector3D<T>.Zero(kVector3.ScalarProcessor);
        KVector2 = LinBivector3D<T>.Zero(kVector3.ScalarProcessor);
        KVector3 = kVector3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector3D(LinScalar3D<T> kVector0, LinBivector3D<T> kVector2)
    {
        KVector0 = kVector0;
        KVector1 = LinVector3D<T>.Zero(kVector0.ScalarProcessor);
        KVector2 = kVector2;
        KVector3 = LinTrivector3D<T>.Zero(kVector0.ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector3D(LinVector3D<T> kVector1, LinTrivector3D<T> kVector3)
    {
        KVector0 = LinScalar3D<T>.Zero(kVector1.ScalarProcessor);
        KVector1 = kVector1;
        KVector2 = LinBivector3D<T>.Zero(kVector1.ScalarProcessor);
        KVector3 = kVector3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector3D(LinScalar3D<T> kVector0, LinVector3D<T> kVector1, LinBivector3D<T> kVector2, LinTrivector3D<T> kVector3)
    {
        KVector0 = kVector0;
        KVector1 = kVector1;
        KVector2 = kVector2;
        KVector3 = kVector3;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return KVector0.IsValid() &&
               KVector1.IsValid() &&
               KVector2.IsValid() &&
               KVector3.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return KVector0.IsZero() &&
               KVector1.IsZero() &&
               KVector2.IsZero() &&
               KVector3.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return Norm().IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return KVector0.NormSquared() +
               KVector1.NormSquared() +
               KVector2.NormSquared() +
               KVector3.NormSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector3D<T> ToMultivector3D()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return NormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector3D<T> GradeInvolution()
    {
        return new LinMultivector3D<T>(
            KVector0,
            KVector1.GradeInvolution(),
            KVector2,
            KVector3.GradeInvolution()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector3D<T> Reverse()
    {
        return new LinMultivector3D<T>(
            KVector0,
            KVector1,
            KVector2.Reverse(),
            KVector3.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector3D<T> CliffordConjugate()
    {
        return new LinMultivector3D<T>(
            KVector0,
            KVector1.CliffordConjugate(),
            KVector2.CliffordConjugate(),
            KVector3
        );
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
        return $"({KVector0}) + {KVector1} + {KVector2} + {KVector3}";
    }

}