using System.Collections;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.Vectors.Space2D;

public sealed record LinMultivector2D<T> :
    ILinMultivector2D<T>
{
    public static LinMultivector2D<T> Zero(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector2D<T>(scalarProcessor);

    public static LinMultivector2D<T> E0(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector2D<T>(LinScalar2D<T>.E0(scalarProcessor));

    public static LinMultivector2D<T> E1(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector2D<T>(LinVector2D<T>.E1(scalarProcessor));

    public static LinMultivector2D<T> E2(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector2D<T>(LinVector2D<T>.E2(scalarProcessor));

    public static LinMultivector2D<T> E12(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector2D<T>(LinBivector2D<T>.E12(scalarProcessor));

    public static LinMultivector2D<T> E21(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector2D<T>(LinBivector2D<T>.E21(scalarProcessor));

    public static LinMultivector2D<T> NegativeE0(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector2D<T>(LinScalar2D<T>.NegativeE0(scalarProcessor));

    public static LinMultivector2D<T> NegativeE1(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector2D<T>(LinVector2D<T>.NegativeE1(scalarProcessor));

    public static LinMultivector2D<T> NegativeE2(IScalarProcessor<T> scalarProcessor)
        => new LinMultivector2D<T>(LinVector2D<T>.NegativeE2(scalarProcessor));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Create(LinScalar2D<T> kVector)
    {
        return new LinMultivector2D<T>(kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Create(LinVector2D<T> kVector)
    {
        return new LinMultivector2D<T>(kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Create(LinBivector2D<T> kVector)
    {
        return new LinMultivector2D<T>(kVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> Create(LinScalar2D<T> kVector0, LinVector2D<T> kVector1, LinBivector2D<T> kVector2)
    {
        return new LinMultivector2D<T>(
            kVector0,
            kVector1,
            kVector2
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinMultivector2D<T>(LinScalar2D<T> mv)
    {
        return new LinMultivector2D<T>(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinMultivector2D<T>(LinVector2D<T> mv)
    {
        return new LinMultivector2D<T>(mv);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LinMultivector2D<T>(LinBivector2D<T> mv)
    {
        return new LinMultivector2D<T>(mv);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator +(LinMultivector2D<T> mv)
    {
        return mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator -(LinMultivector2D<T> mv)
    {
        return new LinMultivector2D<T>(
            -mv.KVector0,
            -mv.KVector1,
            -mv.KVector2
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator +(LinMultivector2D<T> mv1, LinScalar2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv1.KVector0 + mv2,
            mv1.KVector1,
            mv1.KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator +(LinMultivector2D<T> mv1, LinVector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv1.KVector0,
            mv1.KVector1 + mv2,
            mv1.KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator +(LinMultivector2D<T> mv1, LinBivector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2 + mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator +(LinScalar2D<T> mv1, LinMultivector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv1 + mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator +(LinVector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv2.KVector0,
            mv1 + mv2.KVector1,
            mv2.KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator +(LinBivector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv2.KVector0,
            mv2.KVector1,
            mv1 + mv2.KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator +(LinMultivector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv1.KVector0 + mv2.KVector0,
            mv1.KVector1 + mv2.KVector1,
            mv1.KVector2 + mv2.KVector2
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator -(LinMultivector2D<T> mv1, LinScalar2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv1.KVector0 - mv2,
            mv1.KVector1,
            mv1.KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator -(LinMultivector2D<T> mv1, LinVector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv1.KVector0,
            mv1.KVector1 - mv2,
            mv1.KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator -(LinMultivector2D<T> mv1, LinBivector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv1.KVector0,
            mv1.KVector1,
            mv1.KVector2 - mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator -(LinScalar2D<T> mv1, LinMultivector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv1 - mv2.KVector0,
            mv2.KVector1,
            mv2.KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator -(LinVector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv2.KVector0,
            mv1 - mv2.KVector1,
            mv2.KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator -(LinBivector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv2.KVector0,
            mv2.KVector1,
            mv1 - mv2.KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMultivector2D<T> operator -(LinMultivector2D<T> mv1, LinMultivector2D<T> mv2)
    {
        return new LinMultivector2D<T>(
            mv1.KVector0 - mv2.KVector0,
            mv1.KVector1 - mv2.KVector1,
            mv1.KVector2 - mv2.KVector2
        );
    }


    public IScalarProcessor<T> ScalarProcessor 
        => KVector0.ScalarProcessor;

    public int VSpaceDimensions
        => 3;

    public LinScalar2D<T> KVector0 { get; }

    public LinVector2D<T> KVector1 { get; }

    public LinBivector2D<T> KVector2 { get; }

    public Scalar<T> Scalar
        => KVector0.Scalar;

    public Scalar<T> Scalar1
        => KVector1.Scalar1;

    public Scalar<T> Scalar2
        => KVector1.Scalar2;

    public Scalar<T> Scalar12
        => KVector2.Scalar12;

    public int Count
        => 4;

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
                    _ => throw new IndexOutOfRangeException(nameof(index))
                },

                2 => index switch
                {
                    0 => KVector2.Scalar12,
                    _ => throw new IndexOutOfRangeException(nameof(index))
                },

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

                3 => KVector2.Scalar12,

                _ => throw new IndexOutOfRangeException(nameof(id))
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector2D(IScalarProcessor<T> scalarProcessor)
    {
        KVector0 = LinScalar2D<T>.Zero(scalarProcessor);
        KVector1 = LinVector2D<T>.Zero(scalarProcessor);
        KVector2 = LinBivector2D<T>.Zero(scalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector2D(LinScalar2D<T> kVector0)
    {
        KVector0 = kVector0;
        KVector1 = LinVector2D<T>.Zero(ScalarProcessor);
        KVector2 = LinBivector2D<T>.Zero(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector2D(LinVector2D<T> kVector1)
    {
        KVector0 = LinScalar2D<T>.Zero(ScalarProcessor);
        KVector1 = kVector1;
        KVector2 = LinBivector2D<T>.Zero(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector2D(LinBivector2D<T> kVector2)
    {
        KVector0 = LinScalar2D<T>.Zero(ScalarProcessor);
        KVector1 = LinVector2D<T>.Zero(ScalarProcessor);
        KVector2 = kVector2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector2D(LinScalar2D<T> kVector0, LinBivector2D<T> kVector2)
    {
        KVector0 = kVector0;
        KVector1 = LinVector2D<T>.Zero(ScalarProcessor);
        KVector2 = kVector2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinMultivector2D(LinScalar2D<T> kVector0, LinVector2D<T> kVector1, LinBivector2D<T> kVector2)
    {
        KVector0 = kVector0;
        KVector1 = kVector1;
        KVector2 = kVector2;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return KVector0.IsValid() &&
               KVector1.IsValid() &&
               KVector2.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return KVector0.IsZero() &&
               KVector1.IsZero() &&
               KVector2.IsZero();
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
               KVector2.NormSquared();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector2D<T> ToMultivector2D()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return NormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector2D<T> GradeInvolution()
    {
        return new LinMultivector2D<T>(
            KVector0,
            KVector1.GradeInvolution(),
            KVector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector2D<T> Reverse()
    {
        return new LinMultivector2D<T>(
            KVector0,
            KVector1,
            KVector2.Reverse()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector2D<T> CliffordConjugate()
    {
        return new LinMultivector2D<T>(
            KVector0,
            KVector1.CliffordConjugate(),
            KVector2.CliffordConjugate()
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
        return $"{KVector0} + {KVector1} + {KVector2}";
    }

}