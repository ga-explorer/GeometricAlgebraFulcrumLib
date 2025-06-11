using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public sealed record LinFloat64Bivector3D :
    ITriplet<double>,
    ILinFloat64KVector3D
{
    public static LinFloat64Bivector3D Zero { get; }
        = new LinFloat64Bivector3D(
            0d,
            0d,
            0d
        );

    public static LinFloat64Bivector3D E12 { get; }
        = new LinFloat64Bivector3D(
            1d,
            0d,
            0d
        );

    public static LinFloat64Bivector3D E21 { get; }
        = new LinFloat64Bivector3D(
            -1d,
            0d,
            0d
        );

    public static LinFloat64Bivector3D E13 { get; }
        = new LinFloat64Bivector3D(
            0d,
            1d,
            0d
        );

    public static LinFloat64Bivector3D E31 { get; }
        = new LinFloat64Bivector3D(
            0d,
            -1d,
            0d
        );

    public static LinFloat64Bivector3D E23 { get; }
        = new LinFloat64Bivector3D(
            0d,
            0d,
            1d
        );

    public static LinFloat64Bivector3D E32 { get; }
        = new LinFloat64Bivector3D(
            0d,
            0d,
            -1d
        );

    public static LinFloat64Bivector3D Symmetric { get; }
        = new LinFloat64Bivector3D(
            1d,
            1d,
            1d
        );

    public static LinFloat64Bivector3D UnitSymmetric { get; }
        = new LinFloat64Bivector3D(
            3d.Sqrt().Inverse(),
            3d.Sqrt().Inverse(),
            3d.Sqrt().Inverse()
        );

    public static IReadOnlyList<LinFloat64Bivector3D> BasisBivectors { get; }
        = [E12, E13, E23];


    
    public static LinFloat64Bivector3D Create(double scalar12, double scalar13, double scalar23)
    {
        return new LinFloat64Bivector3D(scalar12, scalar13, scalar23);
    }


    
    public static LinFloat64Bivector3D operator +(LinFloat64Bivector3D mv1)
    {
        return mv1;
    }

    
    public static LinFloat64Bivector3D operator -(LinFloat64Bivector3D mv1)
    {
        return new LinFloat64Bivector3D(
            -mv1.Scalar12,
            -mv1.Scalar13,
            -mv1.Scalar23
        );
    }


    
    public static LinFloat64Bivector3D operator +(LinFloat64Bivector3D mv1, LinFloat64Bivector3D mv2)
    {
        return new LinFloat64Bivector3D(
            mv1.Scalar12 + mv2.Scalar12,
            mv1.Scalar13 + mv2.Scalar13,
            mv1.Scalar23 + mv2.Scalar23
        );
    }

    
    public static LinFloat64Bivector3D operator -(LinFloat64Bivector3D mv1, LinFloat64Bivector3D mv2)
    {
        return new LinFloat64Bivector3D(
            mv1.Scalar12 - mv2.Scalar12,
            mv1.Scalar13 - mv2.Scalar13,
            mv1.Scalar23 - mv2.Scalar23
        );
    }


    
    public static LinFloat64Bivector3D operator *(LinFloat64Bivector3D mv1, double mv2)
    {
        return new LinFloat64Bivector3D(
            mv1.Scalar12 * mv2,
            mv1.Scalar13 * mv2,
            mv1.Scalar23 * mv2
        );
    }

    
    public static LinFloat64Bivector3D operator *(double mv1, LinFloat64Bivector3D mv2)
    {
        return new LinFloat64Bivector3D(
            mv1 * mv2.Scalar12,
            mv1 * mv2.Scalar13,
            mv1 * mv2.Scalar23
        );
    }

    
    public static LinFloat64Bivector3D operator /(LinFloat64Bivector3D mv1, double mv2)
    {
        mv2 = 1d / mv2;

        return new LinFloat64Bivector3D(
            mv1.Scalar12 * mv2,
            mv1.Scalar13 * mv2,
            mv1.Scalar23 * mv2
        );
    }


    public int VSpaceDimensions
        => 3;

    public int Grade
        => 2;

    public double Item1
        => Scalar12;

    public double Item2
        => Scalar13;

    public double Item3
        => Scalar23;

    public double Xy
        => Scalar12;

    public double Yx
        => -Scalar12;

    public double Xz
        => Scalar13;

    public double Zx
        => -Scalar13;

    public double Yz
        => Scalar23;

    public double Zy
        => -Scalar23;

    public double Scalar
        => 0d;

    public double Scalar1
        => 0d;

    public double Scalar2
        => 0d;

    public double Scalar3
        => 0d;

    public double Scalar12 { get; }

    public double Scalar13 { get; }

    public double Scalar23 { get; }

    public double Scalar123
        => 0d;

    public int Count
        => 8;

    /// <summary>
    /// Get or set the ith component of this multivector
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public double this[int index]
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
                _ => 0d
            };
        }
    }


    
    private LinFloat64Bivector3D(double scalar12, double scalar13, double scalar23)
    {
        Scalar12 = scalar12;
        Scalar13 = scalar13;
        Scalar23 = scalar23;

        Debug.Assert(IsValid());
    }


    
    public bool IsValid()
    {
        return Scalar12.IsValid() &&
               Scalar13.IsValid() &&
               Scalar23.IsValid();
    }

    
    public bool IsZero()
    {
        return Scalar12.IsZero() &&
               Scalar13.IsZero() &&
               Scalar23.IsZero();
    }

    
    public bool IsNearZero(double zeroEpsilon = 1E-12)
    {
        return Norm().IsNearZero(zeroEpsilon);
    }

    
    public double Norm()
    {
        return NormSquared().Sqrt();
    }

    
    public double NormSquared()
    {
        return Scalar12.Square() +
               Scalar13.Square() +
               Scalar23.Square();
    }

    
    public LinFloat64Bivector3D ToUnitBivector(bool zeroAsSymmetric = true)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroAsSymmetric ? UnitSymmetric : Zero;

        return this / normSquared.Sqrt();
    }

    
    public LinFloat64Multivector3D ToMultivector3D()
    {
        return LinFloat64Multivector3D.Create(this);
    }

    
    public LinFloat64Bivector3D Negative()
    {
        return new LinFloat64Bivector3D(
            -Scalar12,
            -Scalar13,
            -Scalar23
        );
    }

    
    public LinFloat64Bivector3D Negative(double scalingFactor)
    {
        return new LinFloat64Bivector3D(
            -Scalar12 * scalingFactor,
            -Scalar13 * scalingFactor,
            -Scalar23 * scalingFactor
        );
    }

    
    public LinFloat64Bivector3D GradeInvolution()
    {
        return this;
    }

    
    public LinFloat64Bivector3D Reverse()
    {
        return Negative();
    }

    
    public LinFloat64Bivector3D CliffordConjugate()
    {
        return Negative();
    }

    
    public LinFloat64Bivector3D Inverse()
    {
        return Negative(1d / NormSquared());
    }


    
    public LinFloat64Vector3D DirectionToUnitNormal3D(LinFloat64Vector3D zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / norm;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    
    public LinFloat64Vector3D DirectionToNormal3D(LinFloat64Vector3D zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / normSquared;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    
    public LinFloat64Vector3D DirectionToNormal3D(double scalingFactor, LinFloat64Vector3D zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = scalingFactor / normSquared;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    
    public LinFloat64Vector3D NormalToUnitDirection3D(LinFloat64Vector3D zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / norm;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    
    public LinFloat64Vector3D NormalToDirection3D(LinFloat64Vector3D zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = 1d / normSquared;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }

    
    public LinFloat64Vector3D NormalToDirection3D(double scalingFactor, LinFloat64Vector3D zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal
                   ?? throw new DivideByZeroException();

        var s = scalingFactor / normSquared;

        return LinFloat64Vector3D.Create(
            Scalar23 * s,
            Scalar13 * -s,
            Scalar12 * s
        );
    }


    
    public LinFloat64Vector3D Dual3D()
    {
        return LinFloat64Vector3D.Create(
            Scalar23,
            -Scalar13,
            Scalar12
        );
    }

    
    public LinFloat64Vector3D Dual3D(double scalingFactor)
    {
        return LinFloat64Vector3D.Create(
            Scalar23 * scalingFactor,
            -Scalar13 * scalingFactor,
            Scalar12 * scalingFactor
        );
    }

    
    public LinFloat64Vector3D UnDual3D()
    {
        return LinFloat64Vector3D.Create(
            -Scalar23,
            Scalar13,
            -Scalar12
        );
    }

    
    public LinFloat64Vector3D UnDual3D(double scalingFactor)
    {
        return LinFloat64Vector3D.Create(
            -Scalar23 * scalingFactor,
            Scalar13 * scalingFactor,
            -Scalar12 * scalingFactor
        );
    }

    
    
    public double Sp(LinFloat64Scalar3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Vector3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Bivector3D mv2)
    {
        return -(Scalar12 * mv2.Scalar12 +
                 Scalar13 * mv2.Scalar13 +
                 Scalar23 * mv2.Scalar23);
    }

    
    public double Sp(LinFloat64Trivector3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Multivector3D mv2)
    {
        var mv = 0d;

        if (!IsZero() && !mv2.KVector2.IsZero())
            mv += Sp(mv2.KVector2);

        return mv;
    }
    
    
    public LinFloat64Bivector3D Op(LinFloat64Scalar3D mv2)
    {
        return Create(
            Scalar12 * mv2.Scalar,
            Scalar13 * mv2.Scalar,
            Scalar23 * mv2.Scalar
        );
    }
    
    
    public LinFloat64Trivector3D Op(LinFloat64Vector3D mv2)
    {
        return LinFloat64Trivector3D.Create(
            Scalar12 * mv2.Scalar3 -
            Scalar13 * mv2.Scalar2 +
            Scalar23 * mv2.Scalar1
        );
    }

    
    public LinFloat64Scalar3D Op(LinFloat64Bivector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }

    
    public LinFloat64Scalar3D Op(LinFloat64Trivector3D mv2)
    {
        return LinFloat64Scalar3D.Zero;
    }
    
    
    public LinFloat64Multivector3D Op(LinFloat64Multivector3D mv2)
    {
        var mv = LinFloat64Multivector3D.Zero;

        if (!mv2.KVector0.IsZero())
            mv += Op(mv2.KVector0);

        if (!mv2.KVector1.IsZero())
            mv += Op(mv2.KVector1);

        return mv;
    }
    
    
    public LinFloat64Vector3D Rcp(LinFloat64Vector3D mv2)
    {
        var s1 =
            Scalar12 * mv2.Scalar2 +
            Scalar13 * mv2.Scalar3;

        var s2 =
            -Scalar12 * mv2.Scalar1 +
            Scalar23 * mv2.Scalar3;

        var s3 =
            -Scalar13 * mv2.Scalar1 -
            Scalar23 * mv2.Scalar2;

        return LinFloat64Vector3D.Create(s1, s2, s3);
    }
    
    
    public LinFloat64Multivector3D Gp(LinFloat64Vector3D mv2)
    {
        var s1 =
            Scalar12 * mv2.Scalar2 +
            Scalar13 * mv2.Scalar3;

        var s2 =
            -Scalar12 * mv2.Scalar1 +
            Scalar23 * mv2.Scalar3;

        var s3 =
            -Scalar13 * mv2.Scalar1 -
            Scalar23 * mv2.Scalar2;

        var s123 =
            Scalar12 * mv2.Scalar3 -
            Scalar13 * mv2.Scalar2 +
            Scalar23 * mv2.Scalar1;

        return LinFloat64Multivector3D.Create(
            LinFloat64Scalar3D.Zero,
            LinFloat64Vector3D.Create(s1, s2, s3),
            Zero,
            LinFloat64Trivector3D.Create(s123)
        );
    }


    
    public XGaFloat64Bivector ToXGaBivector()
    {
        return XGaFloat64Processor
            .Euclidean
            .CreateBivectorComposer()
            .SetBivectorTerm(0, 1, Xy)
            .SetBivectorTerm(0, 2, Xz)
            .SetBivectorTerm(1, 2, Yz)
            .GetBivector();
    }
    
    
    public XGaFloat64Bivector ToXGaBivector(XGaFloat64Processor processor)
    {
        return processor
            .CreateBivectorComposer()
            .SetBivectorTerm(0, 1, Xy)
            .SetBivectorTerm(0, 2, Xz)
            .SetBivectorTerm(1, 2, Yz)
            .GetBivector();
    }
    
    
    public IEnumerator<double> GetEnumerator()
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

    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    
    public override string ToString()
    {
        return $"({Scalar12:G10})<1,2> + ({Scalar13:G10})<1,3> + ({Scalar23:G10})<2,3>";
    }

}