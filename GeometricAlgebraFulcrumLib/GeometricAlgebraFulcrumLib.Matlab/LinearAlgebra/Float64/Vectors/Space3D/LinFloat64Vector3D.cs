using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

/// <inheritdoc cref="ILinFloat64Vector3D" />
/// <summary>
/// A 3-tuple of double precision coordinates
/// </summary>
public sealed record LinFloat64Vector3D :
    ILinFloat64Vector3D,
    ILinFloat64KVector3D
{
    public static LinFloat64Vector3D Zero { get; }
        = new LinFloat64Vector3D(
            0d,
            0d,
            0d
        );

    public static LinFloat64Vector3D E1 { get; }
        = new LinFloat64Vector3D(
            1d,
            0d,
            0d
        );

    public static LinFloat64Vector3D E2 { get; }
        = new LinFloat64Vector3D(
            0d,
            1d,
            0d
        );

    public static LinFloat64Vector3D E3 { get; }
        = new LinFloat64Vector3D(
            0d,
            0d,
            1d
        );

    public static LinFloat64Vector3D NegativeE1 { get; }
        = new LinFloat64Vector3D(
            -1d,
            0d,
            0d
        );

    public static LinFloat64Vector3D NegativeE2 { get; }
        = new LinFloat64Vector3D(
            0d,
            -1d,
            0d
        );

    public static LinFloat64Vector3D NegativeE3 { get; }
        = new LinFloat64Vector3D(
            0d,
            0d,
            -1d
        );

    public static LinFloat64Vector3D Symmetric { get; }
        = new LinFloat64Vector3D(
            1d,
            1d,
            1d
        );

    public static LinFloat64Vector3D SymmetricNpp { get; }
        = new LinFloat64Vector3D(
            -1d,
            1d,
            1d
        );

    public static LinFloat64Vector3D SymmetricPnp { get; }
        = new LinFloat64Vector3D(
            1d,
            -1d,
            1d
        );

    public static LinFloat64Vector3D SymmetricPpn { get; }
        = new LinFloat64Vector3D(
            1d,
            1d,
            -1d
        );

    public static LinFloat64Vector3D SymmetricPnn { get; }
        = new LinFloat64Vector3D(
            1d,
            -1d,
            -1d
        );

    public static LinFloat64Vector3D SymmetricNpn { get; }
        = new LinFloat64Vector3D(
            -1d,
            1d,
            -1d
        );

    public static LinFloat64Vector3D SymmetricNnp { get; }
        = new LinFloat64Vector3D(
            -1d,
            -1d,
            1d
        );

    public static LinFloat64Vector3D NegativeSymmetric { get; }
        = new LinFloat64Vector3D(
            -1d,
            -1d,
            -1d
        );

    public static LinFloat64Vector3D UnitSymmetric { get; }
        = new LinFloat64Vector3D(
            3d.Sqrt().Inverse(),
            3d.Sqrt().Inverse(),
            3d.Sqrt().Inverse()
        );

    public static LinFloat64Vector3D NegativeUnitSymmetric { get; }
        = new LinFloat64Vector3D(
            -3d.Sqrt().Inverse(),
            -3d.Sqrt().Inverse(),
            -3d.Sqrt().Inverse()
        );

    public static IReadOnlyList<LinFloat64Vector3D> BasisVectors { get; }
        = [E1, E2, E3];


    
    public static LinFloat64Vector3D Create(int x, int y, int z)
    {
        return new LinFloat64Vector3D(x, y, z);
    }

    
    public static LinFloat64Vector3D Create(float x, float y, float z)
    {
        return new LinFloat64Vector3D(x, y, z);
    }

    
    public static LinFloat64Vector3D Create(double x, double y, double z)
    {
        return new LinFloat64Vector3D(x, y, z);
    }

    
    public static LinFloat64Vector3D Create(IFloat64Scalar x, IFloat64Scalar y, IFloat64Scalar z)
    {
        return new LinFloat64Vector3D(x, y, z);
    }

    
    public static LinFloat64Vector3D Create(ITriplet<double> tuple)
    {
        return new LinFloat64Vector3D(
            tuple.Item1,
            tuple.Item2,
            tuple.Item3
        );
    }

    
    public static LinFloat64Vector3D CreateAffineVector(double x, double y)
    {
        return new LinFloat64Vector3D(x, y, 0);
    }

    
    public static LinFloat64Vector3D CreateAffinePoint(double x, double y)
    {
        return new LinFloat64Vector3D(x, y, 1);
    }

    
    public static LinFloat64Vector3D CreateEqualXyz(double x)
    {
        var scalar = x;

        return new LinFloat64Vector3D(scalar, scalar, scalar);
    }

    
    public static LinFloat64Vector3D CreateUnitVector(double x, double y, double z)
    {
        var s = x * x + y * y + z * z;

        if (s.IsZero()) return UnitSymmetric;

        s = 1d / Math.Sqrt(s);

        return new LinFloat64Vector3D(x * s, y * s, z * s);
    }

    
    public static LinFloat64Vector3D VectorSymmetric(double vectorLength)
    {
        var scalar = vectorLength / 3d.Sqrt();

        return new LinFloat64Vector3D(scalar, scalar, scalar);
    }


    
    public static LinFloat64Vector3D operator -(LinFloat64Vector3D v1)
    {
        return new LinFloat64Vector3D(-v1.X, -v1.Y, -v1.Z);
    }
    
    
    public static LinFloat64Vector3D operator +(LinFloat64Vector3D v1, LinBasisVector v2)
    {
        return v1 + v2.ToLinVector3D();
    }

    
    public static LinFloat64Vector3D operator +(LinFloat64Vector3D v1, ILinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }
    
    
    public static LinFloat64Vector3D operator +(LinBasisVector v1, LinFloat64Vector3D v2)
    {
        return v1.ToLinVector3D() + v2;
    }

    
    public static LinFloat64Vector3D operator +(ILinFloat64Vector3D v1, LinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    
    public static LinFloat64Vector3D operator +(LinFloat64Vector3D v1, LinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }
    
    
    public static LinFloat64Vector3D operator -(LinFloat64Vector3D v1, LinBasisVector v2)
    {
        return v1 - v2.ToLinVector3D();
    }

    
    public static LinFloat64Vector3D operator -(LinFloat64Vector3D v1, ILinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }
    
    
    public static LinFloat64Vector3D operator -(LinBasisVector v1, LinFloat64Vector3D v2)
    {
        return v1.ToLinVector3D() - v2;
    }

    
    public static LinFloat64Vector3D operator -(ILinFloat64Vector3D v1, LinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    
    public static LinFloat64Vector3D operator -(LinFloat64Vector3D v1, LinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    
    public static LinFloat64Vector3D operator *(LinFloat64Vector3D v1, int s)
    {
        var x = v1.X * s;
        var y = v1.Y * s;
        var z = v1.Z * s;

        return new LinFloat64Vector3D(x, y, z);
    }

    
    public static LinFloat64Vector3D operator *(int s, LinFloat64Vector3D v1)
    {
        var x = s * v1.X;
        var y = s * v1.Y;
        var z = s * v1.Z;

        return new LinFloat64Vector3D(x, y, z);
    }

    
    public static LinFloat64Vector3D operator *(LinFloat64Vector3D v1, double s)
    {
        var x = v1.X * s;
        var y = v1.Y * s;
        var z = v1.Z * s;

        return new LinFloat64Vector3D(x, y, z);
    }

    
    public static LinFloat64Vector3D operator *(double s, LinFloat64Vector3D v1)
    {
        var x = s * v1.X;
        var y = s * v1.Y;
        var z = s * v1.Z;

        return new LinFloat64Vector3D(x, y, z);
    }
    
    
    public static LinFloat64Vector3D operator *(IFloat64Scalar s, LinFloat64Vector3D v1)
    {
        var x = s.ScalarValue * v1.X;
        var y = s.ScalarValue * v1.Y;
        var z = s.ScalarValue * v1.Z;

        return new LinFloat64Vector3D(x, y, z);
    }

    
    public static LinComplexVector3D operator *(LinFloat64Vector3D v1, Complex s)
    {
        return new LinComplexVector3D(v1.X * s, v1.Y * s, v1.Z * s);
    }

    
    public static LinComplexVector3D operator *(Complex s, LinFloat64Vector3D v1)
    {
        return new LinComplexVector3D(v1.X * s, v1.Y * s, v1.Z * s);
    }

    
    public static LinFloat64Vector3D operator /(LinFloat64Vector3D v1, int s)
    {
        var d = 1.0d / s;

        return new LinFloat64Vector3D(v1.X * d, v1.Y * d, v1.Z * d);
    }

    
    public static LinFloat64Vector3D operator /(LinFloat64Vector3D v1, double s)
    {
        s = 1.0d / s;

        return new LinFloat64Vector3D(v1.X * s, v1.Y * s, v1.Z * s);
    }

    
    public static LinFloat64Vector3D operator /(LinFloat64Vector3D v1, IFloat64Scalar s)
    {
        return new LinFloat64Vector3D(
            v1.X / s.ScalarValue, 
            v1.Y / s.ScalarValue, 
            v1.Z * s.ScalarValue
        );
    }

    
    public static LinFloat64Vector3D operator /(int s, LinFloat64Vector3D v1)
    {
        return (double)s * v1.Inverse();
    }

    
    public static LinFloat64Vector3D operator /(double s, LinFloat64Vector3D v1)
    {
        return s * v1.Inverse();
    }


    public int VSpaceDimensions
        => 3;

    public int Grade
        => 1;

    public double X
        => Scalar1;

    public double Y
        => Scalar2;

    public double Z
        => Scalar3;

    public double Item1
        => X;

    public double Item2
        => Y;

    public double Item3
        => Z;

    public double Scalar
        => 0d;

    public double Scalar1 { get; }

    public double Scalar2 { get; }

    public double Scalar3 { get; }

    public double Scalar12
        => 0d;

    public double Scalar13
        => 0d;

    public double Scalar23
        => 0d;

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
                1 => Scalar1,
                2 => Scalar2,
                4 => Scalar3,
                _ => 0d
            };
        }
    }

    public double this[LinBasisVector axis]
    {
        get
        {
            if (axis == LinBasisVector.Px) return X;
            if (axis == LinBasisVector.Py) return Y;
            if (axis == LinBasisVector.Pz) return Z;
            if (axis == LinBasisVector.Nx) return -X;
            if (axis == LinBasisVector.Ny) return -Y;
            if (axis == LinBasisVector.Nz) return -Z;

            return 0.0d;
        }
    }


    
    private LinFloat64Vector3D(double scalar1, double scalar2, double scalar3)
    {
        Scalar1 = scalar1;
        Scalar2 = scalar2;
        Scalar3 = scalar3;
    }
    
    
    private LinFloat64Vector3D(IFloat64Scalar scalar1, IFloat64Scalar scalar2, IFloat64Scalar scalar3)
    {
        Scalar1 = scalar1.ToScalar();
        Scalar2 = scalar2.ToScalar();
        Scalar3 = scalar3.ToScalar();
    }

    
    public LinFloat64Vector3D(ITriplet<double> v)
    {
        Scalar1 = v.Item1;
        Scalar2 = v.Item2;
        Scalar3 = v.Item3;
    }


    
    public bool IsValid()
    {
        return Scalar1.IsValid() &&
               Scalar2.IsValid() &&
               Scalar3.IsValid();
    }

    
    public bool IsZero()
    {
        return X.IsZero() &&
               Y.IsZero() &&
               Z.IsZero();
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
        return X.Square() + Y.Square() + Z.Square();
    }

    
    public LinFloat64Multivector3D ToMultivector3D()
    {
        return LinFloat64Multivector3D.Create(this);
    }

    
    public LinFloat64Vector3D Negative()
    {
        return new LinFloat64Vector3D(-Scalar1,
            -Scalar2,
            -Scalar3);
    }

    
    public LinFloat64Vector3D GradeInvolution()
    {
        return Negative();
    }

    
    public LinFloat64Vector3D Reverse()
    {
        return this;
    }

    
    public LinFloat64Vector3D CliffordConjugate()
    {
        return Negative();
    }

    
    public LinFloat64Vector3D Inverse()
    {
        var normSquared =
            NormSquared();

        return normSquared.IsZero()
            ? throw new DivideByZeroException()
            : this / normSquared;
    }


    public LinFloat64Bivector3D DirectionToUnitNormal3D(LinFloat64Bivector3D? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = 1d / norm;

        return LinFloat64Bivector3D.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinFloat64Bivector3D DirectionToNormal3D(LinFloat64Bivector3D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = 1d / normSquared;

        return LinFloat64Bivector3D.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinFloat64Bivector3D DirectionToNormal3D(double scalingFactor, LinFloat64Bivector3D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = scalingFactor / normSquared;

        return LinFloat64Bivector3D.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinFloat64Bivector3D NormalToUnitDirection3D(LinFloat64Bivector3D? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = 1d / norm;

        return LinFloat64Bivector3D.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinFloat64Bivector3D NormalToDirection3D(LinFloat64Bivector3D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = 1d / normSquared;

        return LinFloat64Bivector3D.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinFloat64Bivector3D NormalToDirection3D(double scalingFactor, LinFloat64Bivector3D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();
            
        var s = scalingFactor / normSquared;

        return LinFloat64Bivector3D.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }


    
    public LinFloat64Bivector3D Dual3D()
    {
        return LinFloat64Bivector3D.Create(
            -Scalar3,
            Scalar2,
            -Scalar1
        );
    }

    
    public LinFloat64Bivector3D Dual3D(double scalingFactor)
    {
        return LinFloat64Bivector3D.Create(
            -Scalar3 * scalingFactor,
            Scalar2 * scalingFactor,
            -Scalar1 * scalingFactor
        );
    }

    
    public LinFloat64Bivector3D UnDual3D()
    {
        return LinFloat64Bivector3D.Create(
            Scalar3,
            -Scalar2,
            Scalar1
        );
    }

    
    public LinFloat64Bivector3D UnDual3D(double scalingFactor)
    {
        return LinFloat64Bivector3D.Create(
            Scalar3 * scalingFactor,
            -Scalar2 * scalingFactor,
            Scalar1 * scalingFactor
        );
    }

    
    
    public double Sp(LinFloat64Scalar3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Vector3D mv2)
    {
        return Scalar1 * mv2.Scalar1 +
               Scalar2 * mv2.Scalar2 +
               Scalar3 * mv2.Scalar3;
    }

    
    public double Sp(LinFloat64Bivector3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Trivector3D mv2)
    {
        return 0d;
    }

    
    public double Sp(LinFloat64Multivector3D mv2)
    {
        var mv = 0d;

        if (!IsZero() && !mv2.KVector1.IsZero())
            mv += Sp(mv2.KVector1);

        return mv;
    }

    
    
    public LinFloat64Vector3D Op(LinFloat64Scalar3D mv2)
    {
        return Create(
            Scalar1 * mv2.Scalar,
            Scalar2 * mv2.Scalar,
            Scalar3 * mv2.Scalar
        );
    }

    
    public LinFloat64Bivector3D Op(LinFloat64Vector3D mv2)
    {
        return LinFloat64Bivector3D.Create(
            Scalar1 * mv2.Scalar2 - Scalar2 * mv2.Scalar1,
            Scalar1 * mv2.Scalar3 - Scalar3 * mv2.Scalar1,
            Scalar2 * mv2.Scalar3 - Scalar3 * mv2.Scalar2
        );
    }

    
    public LinFloat64Trivector3D Op(LinFloat64Bivector3D mv2)
    {
        return LinFloat64Trivector3D.Create(
            Scalar1 * mv2.Scalar23 -
            Scalar2 * mv2.Scalar13 +
            Scalar3 * mv2.Scalar12
        );
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

        if (!mv2.KVector2.IsZero())
            mv += Op(mv2.KVector2);

        return mv;
    }

    
    public LinFloat64Vector3D Lcp(LinFloat64Bivector3D mv2)
    {
        var s1 =
            -Scalar2 * mv2.Scalar12 -
            Scalar3 * mv2.Scalar13;

        var s2 =
            Scalar1 * mv2.Scalar12 -
            Scalar3 * mv2.Scalar23;

        var s3 =
            Scalar1 * mv2.Scalar13 +
            Scalar2 * mv2.Scalar23;

        return Create(s1, s2, s3);
    }

    
    public LinFloat64Vector3D ProjectOnBivector(LinFloat64Bivector3D mv2)
    {
        return Lcp(mv2).Lcp(mv2.Inverse());
    }


    
    public LinFloat64Multivector3D Gp(LinFloat64Bivector3D mv2)
    {
        var s1 =
            -Scalar2 * mv2.Scalar12 -
            Scalar3 * mv2.Scalar13;

        var s2 =
            Scalar1 * mv2.Scalar12 -
            Scalar3 * mv2.Scalar23;

        var s3 =
            Scalar1 * mv2.Scalar13 +
            Scalar2 * mv2.Scalar23;

        var s123 =
            Scalar1 * mv2.Scalar23 -
            Scalar2 * mv2.Scalar13 +
            Scalar3 * mv2.Scalar12;

        return LinFloat64Multivector3D.Create(
            LinFloat64Scalar3D.Zero,
            Create(s1, s2, s3),
            LinFloat64Bivector3D.Zero,
            LinFloat64Trivector3D.Create(s123)
        );
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
        return $"({Scalar1:G10})<1> + ({Scalar2:G10})<2> + ({Scalar3:G10})<3>";
    }

}