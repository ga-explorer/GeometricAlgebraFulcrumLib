using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;

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
            Float64Scalar.Zero,
            Float64Scalar.Zero,
            Float64Scalar.Zero
        );

    public static LinFloat64Vector3D E1 { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.One,
            Float64Scalar.Zero,
            Float64Scalar.Zero
        );

    public static LinFloat64Vector3D E2 { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.Zero,
            Float64Scalar.One,
            Float64Scalar.Zero
        );

    public static LinFloat64Vector3D E3 { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.Zero,
            Float64Scalar.Zero,
            Float64Scalar.One
        );

    public static LinFloat64Vector3D NegativeE1 { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.NegativeOne,
            Float64Scalar.Zero,
            Float64Scalar.Zero
        );

    public static LinFloat64Vector3D NegativeE2 { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.Zero,
            Float64Scalar.NegativeOne,
            Float64Scalar.Zero
        );

    public static LinFloat64Vector3D NegativeE3 { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.Zero,
            Float64Scalar.Zero,
            Float64Scalar.NegativeOne
        );

    public static LinFloat64Vector3D Symmetric { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.One,
            Float64Scalar.One,
            Float64Scalar.One
        );

    public static LinFloat64Vector3D SymmetricNpp { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.NegativeOne,
            Float64Scalar.One,
            Float64Scalar.One
        );

    public static LinFloat64Vector3D SymmetricPnp { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.One,
            Float64Scalar.NegativeOne,
            Float64Scalar.One
        );

    public static LinFloat64Vector3D SymmetricPpn { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.One,
            Float64Scalar.One,
            Float64Scalar.NegativeOne
        );

    public static LinFloat64Vector3D SymmetricPnn { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.One,
            Float64Scalar.NegativeOne,
            Float64Scalar.NegativeOne
        );

    public static LinFloat64Vector3D SymmetricNpn { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.NegativeOne,
            Float64Scalar.One,
            Float64Scalar.NegativeOne
        );

    public static LinFloat64Vector3D SymmetricNnp { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.NegativeOne,
            Float64Scalar.NegativeOne,
            Float64Scalar.One
        );

    public static LinFloat64Vector3D NegativeSymmetric { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.NegativeOne,
            Float64Scalar.NegativeOne,
            Float64Scalar.NegativeOne
        );

    public static LinFloat64Vector3D UnitSymmetric { get; }
        = new LinFloat64Vector3D(
            Float64Scalar.InvSqrt3,
            Float64Scalar.InvSqrt3,
            Float64Scalar.InvSqrt3
        );

    public static LinFloat64Vector3D NegativeUnitSymmetric { get; }
        = new LinFloat64Vector3D(
            -Float64Scalar.InvSqrt3,
            -Float64Scalar.InvSqrt3,
            -Float64Scalar.InvSqrt3
        );

    public static IReadOnlyList<LinFloat64Vector3D> BasisVectors { get; }
        = new[] { E1, E2, E3 };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Create(int x, int y, int z)
    {
        return new LinFloat64Vector3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Create(float x, float y, float z)
    {
        return new LinFloat64Vector3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Create(double x, double y, double z)
    {
        return new LinFloat64Vector3D(x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Create(Float64Scalar x, Float64Scalar y, Float64Scalar z)
    {
        return new LinFloat64Vector3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Create(IFloat64Scalar x, IFloat64Scalar y, IFloat64Scalar z)
    {
        return new LinFloat64Vector3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D Create(ITriplet<double> tuple)
    {
        return new LinFloat64Vector3D(
            tuple.Item1,
            tuple.Item2,
            tuple.Item3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D CreateAffineVector(Float64Scalar x, Float64Scalar y)
    {
        return new LinFloat64Vector3D(x, y, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D CreateAffinePoint(Float64Scalar x, Float64Scalar y)
    {
        return new LinFloat64Vector3D(x, y, 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D CreateEqualXyz(double x)
    {
        var scalar = new Float64Scalar(x);

        return new LinFloat64Vector3D(scalar, scalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D CreateUnitVector(double x, double y, double z)
    {
        var s = x * x + y * y + z * z;

        if (s.IsZero()) return UnitSymmetric;

        s = 1d / Math.Sqrt(s);

        return new LinFloat64Vector3D(x * s, y * s, z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D VectorSymmetric(double vectorLength)
    {
        var scalar = new Float64Scalar(vectorLength / 3d.Sqrt());

        return new LinFloat64Vector3D(scalar, scalar, scalar);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator -(LinFloat64Vector3D v1)
    {
        return new LinFloat64Vector3D(-v1.X, -v1.Y, -v1.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator +(LinFloat64Vector3D v1, LinBasisVector3D v2)
    {
        return v1 + v2.ToLinVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator +(LinFloat64Vector3D v1, ILinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator +(LinBasisVector3D v1, LinFloat64Vector3D v2)
    {
        return v1.ToLinVector3D() + v2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator +(ILinFloat64Vector3D v1, LinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator +(LinFloat64Vector3D v1, LinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator -(LinFloat64Vector3D v1, LinBasisVector3D v2)
    {
        return v1 - v2.ToLinVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator -(LinFloat64Vector3D v1, ILinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator -(LinBasisVector3D v1, LinFloat64Vector3D v2)
    {
        return v1.ToLinVector3D() - v2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator -(ILinFloat64Vector3D v1, LinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator -(LinFloat64Vector3D v1, LinFloat64Vector3D v2)
    {
        return new LinFloat64Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator *(LinFloat64Vector3D v1, int s)
    {
        var x = v1.X * s;
        var y = v1.Y * s;
        var z = v1.Z * s;

        return new LinFloat64Vector3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator *(int s, LinFloat64Vector3D v1)
    {
        var x = s * v1.X;
        var y = s * v1.Y;
        var z = s * v1.Z;

        return new LinFloat64Vector3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator *(LinFloat64Vector3D v1, double s)
    {
        var x = v1.X * s;
        var y = v1.Y * s;
        var z = v1.Z * s;

        return new LinFloat64Vector3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator *(double s, LinFloat64Vector3D v1)
    {
        var x = s * v1.X;
        var y = s * v1.Y;
        var z = s * v1.Z;

        return new LinFloat64Vector3D(x, y, z);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator *(Float64Scalar s, LinFloat64Vector3D v1)
    {
        var x = s.ScalarValue * v1.X;
        var y = s.ScalarValue * v1.Y;
        var z = s.ScalarValue * v1.Z;

        return new LinFloat64Vector3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator *(IFloat64Scalar s, LinFloat64Vector3D v1)
    {
        var x = s.ScalarValue * v1.X;
        var y = s.ScalarValue * v1.Y;
        var z = s.ScalarValue * v1.Z;

        return new LinFloat64Vector3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinComplexVector3D operator *(LinFloat64Vector3D v1, Complex s)
    {
        return new LinComplexVector3D(v1.X * s, v1.Y * s, v1.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinComplexVector3D operator *(Complex s, LinFloat64Vector3D v1)
    {
        return new LinComplexVector3D(v1.X * s, v1.Y * s, v1.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator *(LinFloat64Vector3D v1, Float64Scalar s)
    {
        var x = v1.X * s;
        var y = v1.Y * s;
        var z = v1.Z * s;

        return new LinFloat64Vector3D(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator /(LinFloat64Vector3D v1, int s)
    {
        var d = 1.0d / s;

        return new LinFloat64Vector3D(v1.X * d, v1.Y * d, v1.Z * d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator /(LinFloat64Vector3D v1, double s)
    {
        s = 1.0d / s;

        return new LinFloat64Vector3D(v1.X * s, v1.Y * s, v1.Z * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator /(LinFloat64Vector3D v1, Float64Scalar s)
    {
        var d = s.Inverse();

        return new LinFloat64Vector3D(v1.X * d, v1.Y * d, v1.Z * d);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator /(LinFloat64Vector3D v1, IFloat64Scalar s)
    {
        return new LinFloat64Vector3D(
            v1.X / s.ScalarValue, 
            v1.Y / s.ScalarValue, 
            v1.Z * s.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator /(int s, LinFloat64Vector3D v1)
    {
        return (double)s * v1.Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator /(double s, LinFloat64Vector3D v1)
    {
        return s * v1.Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Vector3D operator /(Float64Scalar s, LinFloat64Vector3D v1)
    {
        return s * v1.Inverse();
    }


    public int VSpaceDimensions
        => 3;

    public int Grade
        => 1;

    public Float64Scalar X
        => Scalar1;

    public Float64Scalar Y
        => Scalar2;

    public Float64Scalar Z
        => Scalar3;

    public Float64Scalar Item1
        => X;

    public Float64Scalar Item2
        => Y;

    public Float64Scalar Item3
        => Z;

    public Float64Scalar Scalar
        => Float64Scalar.Zero;

    public Float64Scalar Scalar1 { get; }

    public Float64Scalar Scalar2 { get; }

    public Float64Scalar Scalar3 { get; }

    public Float64Scalar Scalar12
        => Float64Scalar.Zero;

    public Float64Scalar Scalar13
        => Float64Scalar.Zero;

    public Float64Scalar Scalar23
        => Float64Scalar.Zero;

    public Float64Scalar Scalar123
        => Float64Scalar.Zero;

    public int Count
        => 8;

    /// <summary>
    /// Get or set the ith component of this multivector
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Float64Scalar this[int index]
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
                _ => Float64Scalar.Zero
            };
        }
    }

    public double this[LinBasisVector3D axis]
    {
        get
        {
            return axis switch
            {
                LinBasisVector3D.Px => X.ScalarValue,
                LinBasisVector3D.Py => Y.ScalarValue,
                LinBasisVector3D.Pz => Z.ScalarValue,
                LinBasisVector3D.Nx => -X.ScalarValue,
                LinBasisVector3D.Ny => -Y.ScalarValue,
                LinBasisVector3D.Nz => -Z.ScalarValue,
                _ => 0.0d
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Vector3D(Float64Scalar scalar1, Float64Scalar scalar2, Float64Scalar scalar3)
    {
        Scalar1 = scalar1;
        Scalar2 = scalar2;
        Scalar3 = scalar3;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Vector3D(IFloat64Scalar scalar1, IFloat64Scalar scalar2, IFloat64Scalar scalar3)
    {
        Scalar1 = scalar1.ToScalar();
        Scalar2 = scalar2.ToScalar();
        Scalar3 = scalar3.ToScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D(ITriplet<Float64Scalar> v)
    {
        Scalar1 = v.Item1;
        Scalar2 = v.Item2;
        Scalar3 = v.Item3;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Scalar1.IsValid() &&
               Scalar2.IsValid() &&
               Scalar3.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return X.IsZero() &&
               Y.IsZero() &&
               Z.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero(double zeroEpsilon = 1E-12)
    {
        return Norm().IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar Norm()
    {
        return NormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar NormSquared()
    {
        return X.Square() + Y.Square() + Z.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Multivector3D ToMultivector3D()
    {
        return LinFloat64Multivector3D.Create(this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D Negative()
    {
        return new LinFloat64Vector3D(-Scalar1,
            -Scalar2,
            -Scalar3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GradeInvolution()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D Inverse()
    {
        var normSquared =
            NormSquared();

        return normSquared.IsZero()
            ? throw new DivideByZeroException()
            : this / normSquared.ScalarValue;
    }


    public LinFloat64Bivector3D DirectionToUnitNormal3D(LinFloat64Bivector3D? zeroNormal = null)
    {
        var norm = Norm();

        if (norm.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = 1d / norm.ScalarValue;

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

        var s = 1d / normSquared.ScalarValue;

        return LinFloat64Bivector3D.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinFloat64Bivector3D DirectionToNormal3D(Float64Scalar scalingFactor, LinFloat64Bivector3D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = scalingFactor / normSquared.ScalarValue;

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

        var s = 1d / norm.ScalarValue;

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

        var s = 1d / normSquared.ScalarValue;

        return LinFloat64Bivector3D.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }

    public LinFloat64Bivector3D NormalToDirection3D(Float64Scalar scalingFactor, LinFloat64Bivector3D? zeroNormal = null)
    {
        var normSquared = NormSquared();

        if (normSquared.IsZero())
            return zeroNormal ??
                   throw new DivideByZeroException();

        var s = scalingFactor / normSquared.ScalarValue;

        return LinFloat64Bivector3D.Create(
            Scalar3 * s,
            Scalar2 * -s,
            Scalar1 * s
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D Dual3D()
    {
        return LinFloat64Bivector3D.Create(
            -Scalar3,
            Scalar2,
            -Scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D Dual3D(Float64Scalar scalingFactor)
    {
        return LinFloat64Bivector3D.Create(
            -Scalar3 * scalingFactor,
            Scalar2 * scalingFactor,
            -Scalar1 * scalingFactor
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D UnDual3D()
    {
        return LinFloat64Bivector3D.Create(
            Scalar3,
            -Scalar2,
            Scalar1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D UnDual3D(Float64Scalar scalingFactor)
    {
        return LinFloat64Bivector3D.Create(
            Scalar3 * scalingFactor,
            -Scalar2 * scalingFactor,
            Scalar1 * scalingFactor
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<Float64Scalar> GetEnumerator()
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
        return $"({Scalar1:G10})<1> + ({Scalar2:G10})<2> + ({Scalar3:G10})<3>";
    }

}