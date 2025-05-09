using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

public sealed record LinBivector3D<T> :
    ITriplet<Scalar<T>>,
    ILinKVector3D<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> Zero(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector3D<T>(
            scalarProcessor.Zero,
            scalarProcessor.Zero,
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> E12(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector3D<T>(
            scalarProcessor.One,
            scalarProcessor.Zero,
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> E21(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector3D<T>(
            scalarProcessor.MinusOne,
            scalarProcessor.Zero,
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> E13(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector3D<T>(
            scalarProcessor.Zero,
            scalarProcessor.One,
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> E31(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector3D<T>(
            scalarProcessor.Zero,
            scalarProcessor.MinusOne,
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> E23(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector3D<T>(
            scalarProcessor.Zero,
            scalarProcessor.Zero,
            scalarProcessor.One
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> E32(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector3D<T>(
            scalarProcessor.Zero,
            scalarProcessor.Zero,
            scalarProcessor.MinusOne
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> Symmetric(IScalarProcessor<T> scalarProcessor)
    {
        return new LinBivector3D<T>(
            scalarProcessor.One,
            scalarProcessor.One,
            scalarProcessor.One
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> UnitSymmetric(IScalarProcessor<T> scalarProcessor)
    {
        var invSqrt3 = scalarProcessor.One.Divide(scalarProcessor.Sqrt(3));

        return new LinBivector3D<T>(invSqrt3, invSqrt3, invSqrt3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IReadOnlyList<LinBivector3D<T>> BasisBivectors(IScalarProcessor<T> scalarProcessor)
    {
        return new[]
        {
            E12(scalarProcessor), 
            E13(scalarProcessor), 
            E23(scalarProcessor)
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> Create(Scalar<T> scalar12, Scalar<T> scalar13, Scalar<T> scalar23)
    {
        return new LinBivector3D<T>(scalar12, scalar13, scalar23);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator +(LinBivector3D<T> mv1)
    {
        return mv1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator -(LinBivector3D<T> mv1)
    {
        return new LinBivector3D<T>(
            -mv1.Scalar12,
            -mv1.Scalar13,
            -mv1.Scalar23
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator +(LinBivector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return new LinBivector3D<T>(
            mv1.Scalar12 + mv2.Scalar12,
            mv1.Scalar13 + mv2.Scalar13,
            mv1.Scalar23 + mv2.Scalar23
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator -(LinBivector3D<T> mv1, LinBivector3D<T> mv2)
    {
        return new LinBivector3D<T>(
            mv1.Scalar12 - mv2.Scalar12,
            mv1.Scalar13 - mv2.Scalar13,
            mv1.Scalar23 - mv2.Scalar23
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator *(LinBivector3D<T> mv1, int mv2)
    {
        return new LinBivector3D<T>(
            mv1.Scalar12 * mv2,
            mv1.Scalar13 * mv2,
            mv1.Scalar23 * mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator *(LinBivector3D<T> mv1, double mv2)
    {
        return new LinBivector3D<T>(
            mv1.Scalar12 * mv2,
            mv1.Scalar13 * mv2,
            mv1.Scalar23 * mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator *(LinBivector3D<T> mv1, T mv2)
    {
        Debug.Assert(mv2 is not null);

        return new LinBivector3D<T>(
            mv1.Scalar12 * mv2,
            mv1.Scalar13 * mv2,
            mv1.Scalar23 * mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator *(LinBivector3D<T> mv1, Scalar<T> mv2)
    {
        return new LinBivector3D<T>(
            mv1.Scalar12 * mv2,
            mv1.Scalar13 * mv2,
            mv1.Scalar23 * mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator *(int mv1, LinBivector3D<T> mv2)
    {
        return new LinBivector3D<T>(
            mv1 * mv2.Scalar12,
            mv1 * mv2.Scalar13,
            mv1 * mv2.Scalar23
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator *(double mv1, LinBivector3D<T> mv2)
    {
        return new LinBivector3D<T>(
            mv1 * mv2.Scalar12,
            mv1 * mv2.Scalar13,
            mv1 * mv2.Scalar23
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator *(T mv1, LinBivector3D<T> mv2)
    {
        Debug.Assert(mv1 is not null);

        return new LinBivector3D<T>(
            mv1 * mv2.Scalar12,
            mv1 * mv2.Scalar13,
            mv1 * mv2.Scalar23
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator *(Scalar<T> mv1, LinBivector3D<T> mv2)
    {
        return new LinBivector3D<T>(
            mv1 * mv2.Scalar12,
            mv1 * mv2.Scalar13,
            mv1 * mv2.Scalar23
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator /(LinBivector3D<T> mv1, int mv2)
    {
        return new LinBivector3D<T>(
            mv1.Scalar12 / mv2,
            mv1.Scalar13 / mv2,
            mv1.Scalar23 / mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator /(LinBivector3D<T> mv1, double mv2)
    {
        return new LinBivector3D<T>(
            mv1.Scalar12 / mv2,
            mv1.Scalar13 / mv2,
            mv1.Scalar23 / mv2
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator /(LinBivector3D<T> mv1, T mv2)
    {
        Debug.Assert(mv2 is not null);

        return new LinBivector3D<T>(
            mv1.Scalar12 / mv2,
            mv1.Scalar13 / mv2,
            mv1.Scalar23 / mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBivector3D<T> operator /(LinBivector3D<T> mv1, Scalar<T> mv2)
    {
        return new LinBivector3D<T>(
            mv1.Scalar12 / mv2,
            mv1.Scalar13 / mv2,
            mv1.Scalar23 / mv2
        );
    }


    public IScalarProcessor<T> ScalarProcessor 
        => Scalar12.ScalarProcessor;

    public int VSpaceDimensions
        => 3;

    public int Grade
        => 2;

    public Scalar<T> Item1
        => Scalar12;

    public Scalar<T> Item2
        => Scalar13;

    public Scalar<T> Item3
        => Scalar23;

    public Scalar<T> Xy
        => Scalar12;

    public Scalar<T> Yx
        => -Scalar12;

    public Scalar<T> Xz
        => Scalar13;

    public Scalar<T> Zx
        => -Scalar13;

    public Scalar<T> Yz
        => Scalar23;

    public Scalar<T> Zy
        => -Scalar23;

    public Scalar<T> Scalar
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar1
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar2
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar3
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar12 { get; }

    public Scalar<T> Scalar13 { get; }

    public Scalar<T> Scalar23 { get; }

    public Scalar<T> Scalar123
        => ScalarProcessor.Zero;

    public int Count
        => 8;

    /// <summary>
    /// Get or set the ith component of this multivector
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Scalar<T> this[int index]
    {
        get
        {
            if (index is < 0 or > 7)
                throw new IndexOutOfRangeException();

            return index switch
            {
                3 => Scalar12,
                5 => Scalar13,
                6 => Scalar23,
                _ => ScalarProcessor.Zero
            };
        }
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinBivector3D(IScalarProcessor<T> scalarProcessor, T scalar12, T scalar13, T scalar23)
    {
        Scalar12 = scalarProcessor.ScalarFromValue(scalar12);
        Scalar13 = scalarProcessor.ScalarFromValue(scalar13);
        Scalar23 = scalarProcessor.ScalarFromValue(scalar23);

        Debug.Assert(IsValid());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinBivector3D(Scalar<T> scalar12, Scalar<T> scalar13, Scalar<T> scalar23)
    {
        Scalar12 = scalar12;
        Scalar13 = scalar13;
        Scalar23 = scalar23;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar12.IsValid() &&
               Scalar13.IsValid() &&
               Scalar23.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Scalar12.IsZero() &&
               Scalar13.IsZero() &&
               Scalar23.IsZero();
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
        return Scalar12.Square() +
               Scalar13.Square() +
               Scalar23.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> ToUnitBivector(bool zeroAsSymmetric = true)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroAsSymmetric 
                ? UnitSymmetric(ScalarProcessor) 
                : Zero(ScalarProcessor);

        return this / normSquared.Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector3D<T> ToMultivector3D()
    {
        return LinMultivector3D<T>.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> Negative()
    {
        return new LinBivector3D<T>(
            -Scalar12,
            -Scalar13,
            -Scalar23
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> Negative(Scalar<T> scalingFactor)
    {
        return new LinBivector3D<T>(
            -Scalar12 * scalingFactor,
            -Scalar13 * scalingFactor,
            -Scalar23 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> Inverse()
    {
        return Negative(1d / NormSquared());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> DirectionToUnitNormal3D(LinVector3D<T>? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / norm;

        return LinVector3D<T>.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> DirectionToNormal3D(LinVector3D<T>? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / normSquared;

        return LinVector3D<T>.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> DirectionToNormal3D(double scalingFactor, LinVector3D<T>? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = scalingFactor / normSquared;

        return LinVector3D<T>.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> NormalToUnitDirection3D(LinVector3D<T>? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / norm;

        return LinVector3D<T>.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> NormalToDirection3D(LinVector3D<T>? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / normSquared;

        return LinVector3D<T>.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> NormalToDirection3D(double scalingFactor, LinVector3D<T>? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = scalingFactor / normSquared;

        return LinVector3D<T>.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> Dual3D()
    {
        return LinVector3D<T>.Create(
            Scalar23,
            -Scalar13,
            Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> Dual3D(double scalingFactor)
    {
        return LinVector3D<T>.Create(
            Scalar23 * scalingFactor,
            -Scalar13 * scalingFactor,
            Scalar12 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> UnDual3D()
    {
        return LinVector3D<T>.Create(
            -Scalar23,
            Scalar13,
            -Scalar12
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> UnDual3D(double scalingFactor)
    {
        return LinVector3D<T>.Create(
            -Scalar23 * scalingFactor,
            Scalar13 * scalingFactor,
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
        return $"({Scalar12})<1,2> + ({Scalar13})<1,3> + ({Scalar23})<2,3>";
    }

}