using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;

/// <summary>
/// This represents a unit basis vector in a linear algebra
/// </summary>
public sealed record LinBasisVector
{
    public sealed class EqualityComparer :
        IEqualityComparer<LinBasisVector>
    {
        public static EqualityComparer Instance { get; }
            = new EqualityComparer();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EqualityComparer()
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(LinBasisVector? x, LinBasisVector? y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (ReferenceEquals(x, y))
                return true;

            return x.IsNegative.Equals(y.IsNegative) && x.Index.Equals(y.Index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(LinBasisVector obj)
        {
            return HashCode.Combine(obj.Sign, obj.Index);
        }
    }


    public static LinBasisVector Px { get; }
        = new LinBasisVector(0, false);

    public static LinBasisVector Py { get; }
        = new LinBasisVector(1, false);

    public static LinBasisVector Pz { get; }
        = new LinBasisVector(2, false);

    public static LinBasisVector Pw { get; }
        = new LinBasisVector(3, false);

    public static LinBasisVector Nx { get; }
        = new LinBasisVector(0, true);

    public static LinBasisVector Ny { get; }
        = new LinBasisVector(1, true);

    public static LinBasisVector Nz { get; }
        = new LinBasisVector(2, true);

    public static LinBasisVector Nw { get; }
        = new LinBasisVector(3, true);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector Positive(int basisVectorIndex)
    {
        return basisVectorIndex switch
        {
            0 => Px,
            1 => Py,
            2 => Pz,
            3 => Pw,
            _ => new LinBasisVector(basisVectorIndex, false)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector Negative(int basisVectorIndex)
    {
        return basisVectorIndex switch
        {
            0 => Nx,
            1 => Ny,
            2 => Nz,
            3 => Nw,
            _ => new LinBasisVector(basisVectorIndex, true)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector Create(int basisVectorIndex, bool isNegative)
    {
        return basisVectorIndex switch
        {
            0 => isNegative ? Nx : Px,
            1 => isNegative ? Ny : Py,
            2 => isNegative ? Nz : Pz,
            3 => isNegative ? Nw : Pw,
            _ => new LinBasisVector(basisVectorIndex, isNegative)
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector Create(int basisVectorIndex, IntegerSign sign)
    {
        return sign == IntegerSign.Zero
            ? throw new InvalidOperationException(nameof(sign))
            : basisVectorIndex switch
            {
                0 => sign.IsNegative ? Nx : Px,
                1 => sign.IsNegative ? Ny : Py,
                2 => sign.IsNegative ? Nz : Pz,
                3 => sign.IsNegative ? Nw : Pw,
                _ => new LinBasisVector(basisVectorIndex, sign.IsNegative)
            };
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static implicit operator LinBasisVector(int basisVectorIndex)
    //{
    //    return new LinBasisVector(basisVectorIndex, false);
    //}


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector operator +(LinBasisVector b1)
    {
        return b1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector operator -(LinBasisVector b1)
    {
        return Create(b1.Index, !b1.IsNegative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector operator *(LinBasisVector b1, IntegerSign s2)
    {
        return Create(b1.Index, b1.IsNegative ? -s2 : s2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinBasisVector operator *(IntegerSign s1, LinBasisVector b2)
    {
        return Create(b2.Index, b2.IsNegative ? -s1 : s1);
    }


    public IntegerSign Sign
        => IsNegative ? IntegerSign.Negative : IntegerSign.Positive;

    public int Index { get; }

    public bool IsNegative { get; }

    public bool IsZero
        => false;

    public bool IsPositive
        => !IsNegative;

    public bool IsNonNegative
        => !IsNegative;

    public bool IsNonZero
        => true;

    public bool IsNonPositive
        => IsNegative;

    public int VSpaceDimensions
        => Index + 1;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinBasisVector(int basisVectorIndex, bool isNegative)
    {
        Index = basisVectorIndex;
        IsNegative = isNegative;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int basisBladeIndex, out IntegerSign sign)
    {
        basisBladeIndex = Index;
        sign = Sign;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector ToPositiveBasis()
    {
        return IsPositive ? this : Create(Index, false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector ToNegativeBasis()
    {
        return IsNegative ? this : Create(Index, true);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector ShiftIndex(int offset, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 1)
            throw new InvalidOperationException();

        return Create(
            (Index + offset) % vSpaceDimensions, 
            IsNegative
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector Negative()
    {
        return Create(Index, !IsNegative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector Times(IntegerSign sign)
    {
        if (sign.IsZero)
            throw new InvalidOperationException();

        return sign.IsPositive ? this : Negative();
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public IntegerSign EGpSign(int basisBlade)
    //{
    //    var sign = _basisVector.EGpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public IntegerSign EGpSign(LinBasisVector basisBlade)
    //{
    //    var sign = _basisVector.EGpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public IntegerSign OpSign(int basisBlade)
    //{
    //    var sign = _basisVector.OpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public IntegerSign OpSign(LinBasisVector basisBlade)
    //{
    //    var sign = _basisVector.OpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public IntegerSign ESpSign(int basisBlade)
    //{
    //    var sign = _basisVector.ESpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public IntegerSign ESpSign(LinBasisVector basisBlade)
    //{
    //    var sign = _basisVector.ESpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetAngleCos(LinBasisVector v2)
    {
        return GetComponent(v2);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetAngleCos(ITriplet<Float64Scalar> v2)
    {
        return (v2.GetComponent(this) / v2.VectorENorm()).Clamp(-1d, 1d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Quaternion ToSystemNumericsQuaternion()
    {
        if (this == Px) return new Quaternion(1, 0, 0, 0);
        if (this == Nx) return new Quaternion(-1, 0, 0, 0);

        if (this == Py) return new Quaternion(0, 1, 0, 0);
        if (this == Ny) return new Quaternion(0, -1, 0, 0);

        if (this == Pz) return new Quaternion(0, 0, 1, 0);
        if (this == Nz) return new Quaternion(0, 0, -1, 0);

        throw new InvalidOperationException();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsParallelTo(LinBasisVector axis)
    {
        return Index == axis.Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsOppositeTo(LinBasisVector axis)
    {
        return Index == axis.Index &&
               Sign == -axis.Sign;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(LinBasisVector? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;

        return IsNegative == other.IsNegative && 
               Index == other.Index;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
    {
        return HashCode.Combine(Sign, Index);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorTerm ToVectorTerm(double scalar)
    {
        return new LinFloat64VectorTerm(
            this,
            scalar * Sign
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorTerm ToZeroVectorTerm()
    {
        return new LinFloat64VectorTerm(ToPositiveBasis(), 0d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorTerm ToPositiveUnitVectorTerm()
    {
        return new LinFloat64VectorTerm(ToPositiveBasis(), 1d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorTerm ToNegativeUnitVectorTerm()
    {
        return new LinFloat64VectorTerm(ToPositiveBasis(), -1d);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D ToLinVector2D()
    {
        if (IsPositive)
            return Index switch
            {
                0 => LinFloat64Vector2D.E1,
                1 => LinFloat64Vector2D.E2,
                _ => throw new InvalidOperationException()
            };

        return Index switch
        {
            0 => LinFloat64Vector2D.NegativeE1,
            1 => LinFloat64Vector2D.NegativeE2,
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector2D ToLinVector2D(double scalingFactor)
    {
        if (IsPositive)
            return Index switch
            {
                0 => LinFloat64Vector2D.Create(scalingFactor, 0),
                1 => LinFloat64Vector2D.Create(0, scalingFactor),
                _ => throw new InvalidOperationException()
            };

        return Index switch
        {
            0 => LinFloat64Vector2D.Create(-scalingFactor, 0),
            1 => LinFloat64Vector2D.Create(0, -scalingFactor),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D ToLinVector3D()
    {
        if (IsPositive)
            return Index switch
            {
                0 => LinFloat64Vector3D.E1,
                1 => LinFloat64Vector3D.E2,
                2 => LinFloat64Vector3D.E3,
                _ => throw new InvalidOperationException()
            };

        return Index switch
        {
            0 => LinFloat64Vector3D.NegativeE1,
            1 => LinFloat64Vector3D.NegativeE2,
            2 => LinFloat64Vector3D.NegativeE3,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D ToLinVector3D(double scalingFactor)
    {
        if (IsPositive)
            return Index switch
            {
                0 => LinFloat64Vector3D.Create(scalingFactor, 0, 0),
                1 => LinFloat64Vector3D.Create(0, scalingFactor, 0),
                2 => LinFloat64Vector3D.Create(0, 0, scalingFactor),
                _ => throw new InvalidOperationException()
            };

        return Index switch
        {
            0 => LinFloat64Vector3D.Create(-scalingFactor, 0, 0),
            1 => LinFloat64Vector3D.Create(0, -scalingFactor, 0),
            2 => LinFloat64Vector3D.Create(0, 0, -scalingFactor),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D ToLinVector4D()
    {
        if (IsPositive)
            return Index switch
            {
                0 => LinFloat64Vector4D.E1,
                1 => LinFloat64Vector4D.E2,
                2 => LinFloat64Vector4D.E3,
                3 => LinFloat64Vector4D.E4,
                _ => throw new InvalidOperationException()
            };

        return Index switch
        {
            0 => LinFloat64Vector4D.NegativeE1,
            1 => LinFloat64Vector4D.NegativeE2,
            2 => LinFloat64Vector4D.NegativeE3,
            3 => LinFloat64Vector4D.NegativeE4,
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector4D ToLinVector4D(double scalingFactor)
    {
        if (IsPositive)
            return Index switch
            {
                0 => LinFloat64Vector4D.Create(scalingFactor, 0, 0, 0),
                1 => LinFloat64Vector4D.Create(0, scalingFactor, 0, 0),
                2 => LinFloat64Vector4D.Create(0, 0, scalingFactor, 0),
                3 => LinFloat64Vector4D.Create(0, 0, 0, scalingFactor),
                _ => throw new InvalidOperationException()
            };

        return Index switch
        {
            0 => LinFloat64Vector4D.Create(-scalingFactor, 0, 0, 0),
            1 => LinFloat64Vector4D.Create(0, -scalingFactor, 0, 0),
            2 => LinFloat64Vector4D.Create(0, 0, -scalingFactor, 0),
            3 => LinFloat64Vector4D.Create(0, 0, 0, -scalingFactor),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector ToLinVector()
    {
        if (IsZero)
            return LinFloat64Vector.Zero;

        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(Index, IsPositive ? 1d : -1d);

        return LinFloat64Vector.Create(basisScalarDictionary);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector GetUnitNormalVector()
    {
        var rotatedVector = ToLinVector();

        if (rotatedVector.IsNearVectorBasis(0))
            return 1.CreateLinVector();

        // For smoother motions, find the quaternion q that
        // rotates e1 to vector, then use q to rotate e2
        return LinFloat64AxisToVectorRotation
            .CreateFromRotatedVector(0.ToLinBasisVector(), rotatedVector)
            .MapBasisVector(1);
    }

    /// <summary>
    /// Create a rotation quaternion given an axis and angle of rotation
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion RotationAxisAngleToQuaternion(LinFloat64Angle angle)
    {
        return LinFloat64Quaternion.CreateFromAxisAngle(this, angle);
    }

    public LinFloat64Vector3D VectorToVectorRotationVector(ILinFloat64Vector3D dstVector)
    {
        var (u, a) =
            VectorToVectorRotationAxisAngle(dstVector);

        return u.VectorTimes(a.RadiansValue / Math.Tau);
    }

    public Tuple<LinFloat64Vector3D, LinFloat64Angle> VectorToVectorRotationAxisAngle(ILinFloat64Vector3D dstVector)
    {
        Debug.Assert(
            dstVector.IsNearUnitVector()
        );

        var angleCos = VectorESp(dstVector);

        // The case where the two vectors are almost the same
        if (angleCos.IsNearOne())
            return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
                GetUnitNormal(3).ToLinVector3D(),
                LinFloat64PolarAngle.Angle0
            );

        // The case where the two vectors are almost opposite
        if (angleCos.IsNearNegativeOne())
            return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
                GetUnitNormal(3).ToLinVector3D(),
                LinFloat64PolarAngle.Angle180
            );

        // The general case
        return new Tuple<LinFloat64Vector3D, LinFloat64Angle>(
            VectorUnitCross(dstVector),
            angleCos.CosToPolarAngle()
        );
    }

    /// <summary>
    /// Create a rotation quaternion given a vector and its rotated version
    /// </summary>
    /// <param name="dstVector"></param>
    /// <returns></returns>
    public LinFloat64Quaternion VectorToVectorRotationQuaternion(LinBasisVector dstVector)
    {
        return ToLinVector3D().VectorToVectorRotationQuaternion(dstVector.ToLinVector3D());

        //var sqrt2Inv = 1d / Math.Sqrt(2d);

        //return srcVector switch
        //{
        //    LinBasisVector.Px => dstVector switch
        //    {
        //        LinBasisVector.Px => LinFloat64Quaternion.Identity,
        //        LinBasisVector.Py => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
        //        LinBasisVector.Pz => LinFloat64Quaternion.Create(sqrt2Inv, 0, -sqrt2Inv, 0),
        //        LinBasisVector.Nx => LinFloat64Quaternion.Create(0, 0, 0, 1),
        //        LinBasisVector.Ny => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, -sqrt2Inv),
        //        _ => LinFloat64Quaternion.Create(sqrt2Inv, 0, sqrt2Inv, 0),
        //    },

        //    LinBasisVector.Py => dstVector switch
        //    {
        //        LinBasisVector.Px => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, -sqrt2Inv),
        //        LinBasisVector.Py => LinFloat64Quaternion.Identity,
        //        LinBasisVector.Pz => LinFloat64Quaternion.Create(sqrt2Inv, sqrt2Inv, 0, 0),
        //        LinBasisVector.Nx => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
        //        LinBasisVector.Ny => LinFloat64Quaternion.Create(0, 1, 0, 0),
        //        _ => LinFloat64Quaternion.Create(sqrt2Inv, -sqrt2Inv, 0, 0),
        //    },

        //    LinBasisVector.Pz => dstVector switch
        //    {
        //        LinBasisVector.Px => LinFloat64Quaternion.Create(sqrt2Inv, 0, sqrt2Inv, 0),
        //        LinBasisVector.Py => LinFloat64Quaternion.Create(sqrt2Inv, -sqrt2Inv, 0, 0),
        //        LinBasisVector.Pz => LinFloat64Quaternion.Identity,
        //        LinBasisVector.Nx => LinFloat64Quaternion.Create(sqrt2Inv, 0, -sqrt2Inv, 0),
        //        LinBasisVector.Ny => LinFloat64Quaternion.Create(sqrt2Inv, sqrt2Inv, 0, 0),
        //        _ => LinFloat64Quaternion.Create(0, 0, 1, 0),
        //    },

        //    LinBasisVector.Nx => dstVector switch
        //    {
        //        LinBasisVector.Px => LinFloat64Quaternion.Create(0, 0, 0, 1),
        //        LinBasisVector.Py => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, -sqrt2Inv),
        //        LinBasisVector.Pz => LinFloat64Quaternion.Create(sqrt2Inv, 0, sqrt2Inv, 0),
        //        LinBasisVector.Nx => LinFloat64Quaternion.Identity,
        //        LinBasisVector.Ny => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
        //        _ => LinFloat64Quaternion.Create(sqrt2Inv, 0, -sqrt2Inv, 0),
        //    },

        //    LinBasisVector.Ny => dstVector switch
        //    {
        //        LinBasisVector.Px => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, sqrt2Inv),
        //        LinBasisVector.Py => LinFloat64Quaternion.Create(0, 1, 0, 0),
        //        LinBasisVector.Pz => LinFloat64Quaternion.Create(sqrt2Inv, -sqrt2Inv, 0, 0),
        //        LinBasisVector.Nx => LinFloat64Quaternion.Create(sqrt2Inv, 0, 0, -sqrt2Inv),
        //        LinBasisVector.Ny => LinFloat64Quaternion.Identity,
        //        _ => LinFloat64Quaternion.Create(sqrt2Inv, sqrt2Inv, 0, 0),
        //    },

        //    _ => dstVector switch
        //    {
        //        LinBasisVector.Px => LinFloat64Quaternion.Create(sqrt2Inv, 0, -sqrt2Inv, 0),
        //        LinBasisVector.Py => LinFloat64Quaternion.Create(sqrt2Inv, sqrt2Inv, 0, 0),
        //        LinBasisVector.Pz => LinFloat64Quaternion.Create(0, 0, 1, 0),
        //        LinBasisVector.Nx => LinFloat64Quaternion.Create(sqrt2Inv, 0, sqrt2Inv, 0),
        //        LinBasisVector.Ny => LinFloat64Quaternion.Create(sqrt2Inv, -sqrt2Inv, 0, 0),
        //        _ => LinFloat64Quaternion.Identity,
        //    },
        //};
    }

    /// <summary>
    /// Create a rotation quaternion given a vector and its rotated version
    /// </summary>
    /// <param name="dstVector"></param>
    /// <param name="zeroEpsilon"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion VectorToVectorRotationQuaternion(ILinFloat64Vector3D dstVector, double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var angleCos = GetAngleCos(dstVector);

        if (angleCos.IsNearOne(zeroEpsilon))
            return LinFloat64Quaternion.Identity;

        if (angleCos.IsNearMinusOne(zeroEpsilon))
            return LinFloat64Quaternion.CreateFromAxisAngle(
                GetUnitNormal3D(),
                LinFloat64PolarAngle.Angle0
            );

        return LinFloat64Quaternion.CreateFromAxisAngle(
            VectorUnitCross(dstVector),
            angleCos.CosToPolarAngle()
        );

        //var (u, a) =
        //    axis.CreateAxisToVectorRotationAxisAngle(unitVector);

        //return u.CreateQuaternion(a);

        ////This gives a correct quaternion but not the simplest one (the one with the smallest angle)
        ////var (nearestAxis, q2) =
        ////    unitVector.CreateNearestAxisToVectorRotationQuaternion();

        ////var q1 =
        ////    axis.CreateAxisToAxisRotationQuaternion(nearestAxis);

        ////return Tuple4D.ConcatenateText(q1, q2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64DirectionalScalingLinearMap3D CreateDirectionalScaling3D(double scalingFactor)
    {
        if (scalingFactor.IsZero())
            throw new ArgumentException(nameof(scalingFactor));

        // An identity map
        if (scalingFactor.IsNearOne())
            return LinFloat64IdentityLinearMap3D.Instance;

        // A hyper plane reflection using a normal basis vector
        if (scalingFactor.IsNearMinusOne())
            return LinFloat64HyperPlaneAxisReflection3D.Create(Index);

        // A general directional scaling using a basis vector
        return LinFloat64AxisDirectionalScaling3D.Create(scalingFactor, this);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64DirectionalScalingLinearMap4D CreateDirectionalScaling4D(double scalingFactor)
    {
        if (scalingFactor.IsZero())
            throw new ArgumentException(nameof(scalingFactor));

        // An identity map
        if (scalingFactor.IsNearOne())
            return LinFloat64IdentityLinearMap4D.Instance;

        // A hyper plane reflection using a normal basis vector
        if (scalingFactor.IsNearMinusOne())
            return LinFloat64HyperPlaneAxisReflection4D.Create(Index);

        // A general directional scaling using a basis vector
        return LinFloat64AxisDirectionalScaling4D.Create(scalingFactor, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64DirectionalScalingLinearMap CreateDirectionalScaling(int dimensions, double scalingFactor)
    {
        if (scalingFactor.IsZero())
            throw new ArgumentException(nameof(scalingFactor));

        // An identity map
        if (scalingFactor.IsNearOne())
            return LinFloat64IdentityLinearMap.Create(dimensions);

        // A hyper plane reflection using a normal basis vector
        if (scalingFactor.IsNearMinusOne())
            return LinFloat64HyperPlaneAxisReflection.Create(dimensions, this);

        // A general directional scaling using a basis vector
        return LinFloat64AxisDirectionalScaling.Create(scalingFactor, dimensions, this);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector GetNormal(int vSpaceDimensions)
    {
        return NextBasisVector(vSpaceDimensions);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector GetNormal3D()
    {
        return NextBasisVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector GetUnitNormal(int vSpaceDimensions)
    {
        return NextBasisVector(vSpaceDimensions);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector GetUnitNormal3D()
    {
        return NextBasisVector3D();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinBasisVector> GetNormalPair3D()
    {
        var e1 = NextBasisVector3D();
        var e2 = e1.NextBasisVector3D();

        return new Pair<LinBasisVector>(e1, e2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinBasisVector> GetUnitNormalPair3D()
    {
        return GetNormalPair3D();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector NextBasisVector(int vSpaceDimensions)
    {
        if (Index >= vSpaceDimensions)
            throw new InvalidOperationException();

        return new LinBasisVector(
            (Index + 1) % vSpaceDimensions, 
            IsNegative
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector NextBasisVector2D()
    {
        if (this == Px) return Py;
        if (this == Py) return Px;
        
        if (this == Nx) return Ny;
        if (this == Ny) return Nx;

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector NextBasisVector3D()
    {
        if (this == Px) return Py;
        if (this == Py) return Pz;
        if (this == Pz) return Px;
        if (this == Nx) return Ny;
        if (this == Ny) return Nz;
        if (this == Nz) return Nx;

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVector PrevBasisVector3D()
    {
        if (this == Px) return Pz;
        if (this == Py) return Px;
        if (this == Pz) return Py;
        if (this == Nx) return Nz;
        if (this == Ny) return Nx;
        if (this == Nz) return Ny;

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetX()
    {
        if (this == Px) return Float64Scalar.One;
        if (this == Nx) return Float64Scalar.NegativeOne;

        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetY()
    {
        if (this == Py) return Float64Scalar.One;
        if (this == Ny) return Float64Scalar.NegativeOne;

        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetZ()
    {
        if (this == Pz) return Float64Scalar.One;
        if (this == Nz) return Float64Scalar.NegativeOne;

        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetW()
    {
        if (this == Pw) return Float64Scalar.One;
        if (this == Nw) return Float64Scalar.NegativeOne;

        return Float64Scalar.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar GetComponent(LinBasisVector axis)
    {
        if (axis.Index != Index)
            return Float64Scalar.Zero;

        return axis.Sign == Sign
            ? Float64Scalar.One
            : Float64Scalar.MinusOne;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D DirectionToUnitNormal3D()
    {
        if (this == Px) return LinFloat64Bivector3D.E32;
        if (this == Nx) return LinFloat64Bivector3D.E23;
        if (this == Py) return LinFloat64Bivector3D.E13;
        if (this == Ny) return LinFloat64Bivector3D.E31;
        if (this == Pz) return LinFloat64Bivector3D.E21;
        if (this == Nz) return LinFloat64Bivector3D.E12;

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D DirectionToUnitNormal3D(Float64Scalar scalingFactor)
    {
        if (this == Px) return LinFloat64Bivector3D.E32 * scalingFactor;
        if (this == Nx) return LinFloat64Bivector3D.E23 * scalingFactor;
        if (this == Py) return LinFloat64Bivector3D.E13 * scalingFactor;
        if (this == Ny) return LinFloat64Bivector3D.E31 * scalingFactor;
        if (this == Pz) return LinFloat64Bivector3D.E21 * scalingFactor;
        if (this == Nz) return LinFloat64Bivector3D.E12 * scalingFactor;

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D NormalToUnitDirection3D()
    {
        if (this == Px) return LinFloat64Bivector3D.E23;
        if (this == Nx) return LinFloat64Bivector3D.E32;
        if (this == Py) return LinFloat64Bivector3D.E31;
        if (this == Ny) return LinFloat64Bivector3D.E13;
        if (this == Pz) return LinFloat64Bivector3D.E12;
        if (this == Nz) return LinFloat64Bivector3D.E21;

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector3D NormalToUnitDirection3D(Float64Scalar scalingFactor)
    {
        if (this == Px) return LinFloat64Bivector3D.E23 * scalingFactor;
        if (this == Nx) return LinFloat64Bivector3D.E32 * scalingFactor;
        if (this == Py) return LinFloat64Bivector3D.E31 * scalingFactor;
        if (this == Ny) return LinFloat64Bivector3D.E13 * scalingFactor;
        if (this == Pz) return LinFloat64Bivector3D.E12 * scalingFactor;
        if (this == Nz) return LinFloat64Bivector3D.E21 * scalingFactor;

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBasisVectorPair3D GetAxisPair3D()
    {
        if (this == Px) return LinBasisVectorPair3D.PyPz;
        if (this == Nx) return LinBasisVectorPair3D.PzPy;
        if (this == Py) return LinBasisVectorPair3D.PzPx;
        if (this == Ny) return LinBasisVectorPair3D.PxPz;
        if (this == Pz) return LinBasisVectorPair3D.PxPy;
        if (this == Nz) return LinBasisVectorPair3D.PyPx;

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsXAxis()
    {
        return this == Px || this == Nx;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsYAxis()
    {
        return this == Py || this == Ny;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZAxis()
    {
        return this == Pz || this == Nz;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsWAxis()
    {
        return this == Pw || this == Nw;
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Scalar VectorESp(ITriplet<Float64Scalar> v2)
    {
        if (this == Px) return v2.Item1;
        if (this == Nx) return -v2.Item1;

        if (this == Py) return v2.Item2;
        if (this == Ny) return -v2.Item2;

        if (this == Pz) return v2.Item3;
        if (this == Nz) return -v2.Item3;

        return Float64Scalar.Zero;
    }

    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D VectorCross(ITriplet<Float64Scalar> v2)
    {
        if (this == Px) return LinFloat64Vector3D.Create(0, -v2.Item3, v2.Item2);
        if (this == Nx) return LinFloat64Vector3D.Create(0, v2.Item3, -v2.Item2);

        if (this == Py) return LinFloat64Vector3D.Create(v2.Item3, 0, -v2.Item1);
        if (this == Ny) return LinFloat64Vector3D.Create(-v2.Item3, 0, v2.Item1);

        if (this == Pz) return LinFloat64Vector3D.Create(-v2.Item2, v2.Item1, 0);
        if (this == Nz) return LinFloat64Vector3D.Create(v2.Item2, -v2.Item1, 0);

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D VectorUnitCross(ITriplet<Float64Scalar> v2)
    {
        return VectorCross(v2).ToUnitLinVector3D();
    }

    public LinQuaternion<T> CreateAxisToAxisRotationQuaternion<T>(LinBasisVector axis2, IScalarProcessor<T> scalarProcessor)
    {
        var sqrt2Inv = scalarProcessor.One.Divide(scalarProcessor.Sqrt(2));
        var zero = scalarProcessor.Zero;
        var one = scalarProcessor.One;

        if (this == Px)
        {
            if (axis2 == Px) return LinQuaternion<T>.Identity(scalarProcessor);
            if (axis2 == Py) return LinQuaternion<T>.Create(zero, zero, sqrt2Inv, sqrt2Inv);
            if (axis2 == Pz) return LinQuaternion<T>.Create(zero, -sqrt2Inv, zero, sqrt2Inv);
            if (axis2 == Nx) return LinQuaternion<T>.Create(zero, zero, one, zero);
            if (axis2 == Ny) return LinQuaternion<T>.Create(zero, zero, -sqrt2Inv, sqrt2Inv);
            if (axis2 == Nz) return LinQuaternion<T>.Create(zero, sqrt2Inv, zero, sqrt2Inv);

            throw new InvalidOperationException();
        }

        if (this == Py)
        {
            if (axis2 == Px) return LinQuaternion<T>.Create(zero, zero, -sqrt2Inv, sqrt2Inv);
            if (axis2 == Py) return LinQuaternion<T>.Identity(scalarProcessor);
            if (axis2 == Pz) return LinQuaternion<T>.Create(sqrt2Inv, zero, zero, sqrt2Inv);
            if (axis2 == Nx) return LinQuaternion<T>.Create(zero, zero, sqrt2Inv, sqrt2Inv);
            if (axis2 == Ny) return LinQuaternion<T>.Create(one, zero, zero, zero);
            if (axis2 == Nz) return LinQuaternion<T>.Create(-sqrt2Inv, zero, zero, sqrt2Inv);

            throw new InvalidOperationException();
        }

        if (this == Pz)
        {
            if (axis2 == Px) return LinQuaternion<T>.Create(zero, sqrt2Inv, zero, sqrt2Inv);
            if (axis2 == Py) return LinQuaternion<T>.Create(-sqrt2Inv, zero, zero, sqrt2Inv);
            if (axis2 == Pz) return LinQuaternion<T>.Identity(scalarProcessor);
            if (axis2 == Nx) return LinQuaternion<T>.Create(zero, -sqrt2Inv, zero, sqrt2Inv);
            if (axis2 == Ny) return LinQuaternion<T>.Create(sqrt2Inv, zero, zero, sqrt2Inv);
            if (axis2 == Nz) return LinQuaternion<T>.Create(zero, one, zero, zero);

            throw new InvalidOperationException();
        }

        if (this == Nx)
        {
            if (axis2 == Px) return LinQuaternion<T>.Create(zero, zero, one, zero);
            if (axis2 == Py) return LinQuaternion<T>.Create(zero, zero, -sqrt2Inv, sqrt2Inv);
            if (axis2 == Pz) return LinQuaternion<T>.Create(zero, sqrt2Inv, zero, sqrt2Inv);
            if (axis2 == Nx) return LinQuaternion<T>.Identity(scalarProcessor);
            if (axis2 == Ny) return LinQuaternion<T>.Create(zero, zero, sqrt2Inv, sqrt2Inv);
            if (axis2 == Nz) return LinQuaternion<T>.Create(zero, -sqrt2Inv, zero, sqrt2Inv);

            throw new InvalidOperationException();
        }

        if (this == Ny)
        {
            if (axis2 == Px) return LinQuaternion<T>.Create(zero, zero, sqrt2Inv, sqrt2Inv);
            if (axis2 == Py) return LinQuaternion<T>.Create(one, zero, zero, zero);
            if (axis2 == Pz) return LinQuaternion<T>.Create(-sqrt2Inv, zero, zero, sqrt2Inv);
            if (axis2 == Nx) return LinQuaternion<T>.Create(zero, zero, -sqrt2Inv, sqrt2Inv);
            if (axis2 == Ny) return LinQuaternion<T>.Identity(scalarProcessor);
            if (axis2 == Nz) return LinQuaternion<T>.Create(sqrt2Inv, zero, zero, sqrt2Inv);

            throw new InvalidOperationException();
        }

        if (axis2 == Px)
        {
            return LinQuaternion<T>.Create(zero, -sqrt2Inv, zero, sqrt2Inv);
        }

        if (axis2 == Py)
        {
            return LinQuaternion<T>.Create(sqrt2Inv, zero, zero, sqrt2Inv);
        }

        if (axis2 == Pz)
        {
            return LinQuaternion<T>.Create(zero, one, zero, zero);
        }

        if (axis2 == Nx)
        {
            return LinQuaternion<T>.Create(zero, sqrt2Inv, zero, sqrt2Inv);
        }

        if (axis2 == Ny)
        {
            return LinQuaternion<T>.Create(-sqrt2Inv, zero, zero, sqrt2Inv);
        }

        return LinQuaternion<T>.Identity(scalarProcessor);
    }

    public LinVector3D<T> CreateAxisToVectorRotationVector<T>(ILinVector3D<T> unitVector)
    {
        var scalarProcessor = unitVector.ScalarProcessor;

        var (u, a) =
            CreateAxisToVectorRotationAxisAngle(unitVector);

        return u.VectorTimes(a.Radians.Divide(scalarProcessor.PiTimes2Value));
    }

    public Tuple<ILinVector3D<T>, LinPolarAngle<T>> CreateAxisToVectorRotationAxisAngle<T>(ILinVector3D<T> unitVector)
    {
        var scalarProcessor = unitVector.ScalarProcessor;

        //Debug.Assert(
        //    (unitVector.GetLengthSquared() - 1).IsNearZero()
        //);

        var dot12 = VectorESp(unitVector);

        // The case where the two vectors are almost the same
        if ((dot12 - 1d).IsNearZero())
            return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
                unitVector,
                LinPolarAngle<T>.Angle0(scalarProcessor)
            );

        // The case where the two vectors are almost opposite
        if ((dot12 + 1d).IsNearZero())
            return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
                GetUnitNormal3D().ToVector3D(scalarProcessor),
                LinPolarAngle<T>.Angle180(scalarProcessor)
            );

        // The general case
        return new Tuple<ILinVector3D<T>, LinPolarAngle<T>>(
            VectorUnitCross(unitVector),
            scalarProcessor.CreatePolarAngleFromRadians(dot12.ArcCos().ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinQuaternion<T> CreateAxisPairToVectorPairRotationQuaternion<T>(LinBasisVector axis2, ILinVector3D<T> unitVector1, ILinVector3D<T> unitVector2)
    {
        Debug.Assert(
            unitVector1.VectorENormSquared().IsNearOne() &&
            unitVector2.VectorENormSquared().IsNearOne()
        );

        var q1 =
            CreateAxisToVectorRotationQuaternion(unitVector1);

        Debug.Assert(
            (q1.RotateVector(this) - unitVector1).VectorENormSquared().IsNearZero()
        );

        var axis2Rotated =
            q1.RotateVector(axis2).ToUnitVector();

        var q2 =
            axis2Rotated.CreateVectorToVectorRotationQuaternion(unitVector2, unitVector1);

        var quaternion =
            q2.Concatenate(q1);

        Debug.Assert(
            (quaternion.RotateVector(this) - unitVector1).VectorENormSquared().IsNearZero()
        );

        Debug.Assert(
            (quaternion.RotateVector(axis2) - unitVector2).VectorENormSquared().IsNearZero()
        );

        return quaternion;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinQuaternion<T>> CreateAxisPairToVectorPairRotationQuaternionPair<T>(LinBasisVector axis2, ILinVector3D<T> unitVector1, ILinVector3D<T> unitVector2)
    {
        Debug.Assert(
            unitVector1.VectorENormSquared().IsNearOne() &&
            unitVector2.VectorENormSquared().IsNearOne()
        );

        var q1 =
            CreateAxisToVectorRotationQuaternion(unitVector1);

        var axis2Rotated =
            q1.RotateVector(axis2).ToUnitVector();

        var q2 =
            axis2Rotated.CreateVectorToVectorRotationQuaternion(unitVector2, unitVector1);

        Debug.Assert(
            (q1.Concatenate(q2).RotateVector(this) - unitVector1).VectorENormSquared().IsNearZero()
        );

        Debug.Assert(
            (q1.Concatenate(q2).RotateVector(axis2) - unitVector2).VectorENormSquared().IsNearZero()
        );

        return new Pair<LinQuaternion<T>>(q1, q2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinQuaternion<T> CreateAxisToVectorRotationQuaternion<T>(ILinVector3D<T> unitVector)
    {
        var (u, a) =
            CreateAxisToVectorRotationAxisAngle(unitVector);

        return u.CreateQuaternion(a);

        //This gives a correct quaternion but not the simplest one (the one with the smallest angle)
        //var (nearestAxis, q2) =
        //    unitVector.CreateNearestAxisToVectorRotationQuaternion();

        //var q1 =
        //    axis.CreateAxisToAxisRotationQuaternion(nearestAxis);

        //return Tuple4D.ConcatenateText(q1, q2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector2D<T> ToVector2D<T>(IScalarProcessor<T> scalarProcessor)
    {
        if (this == Px) return LinVector2D<T>.E1(scalarProcessor);
        if (this == Nx) return LinVector2D<T>.NegativeE1(scalarProcessor);
        if (this == Py) return LinVector2D<T>.E2(scalarProcessor);
        if (this == Ny) return LinVector2D<T>.NegativeE2(scalarProcessor);

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetX<T>(IScalarProcessor<T> scalarProcessor)
    {
        if (this == Px) return scalarProcessor.One;
        if (this == Nx) return scalarProcessor.MinusOne;

        return scalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetY<T>(IScalarProcessor<T> scalarProcessor)
    {
        if (this == Py) return scalarProcessor.One;
        if (this == Ny) return scalarProcessor.MinusOne;

        return scalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetZ<T>(IScalarProcessor<T> scalarProcessor)
    {
        if (this == Pz) return scalarProcessor.One;
        if (this == Nz) return scalarProcessor.MinusOne;

        return scalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetW<T>(IScalarProcessor<T> scalarProcessor)
    {
        if (this == Pw) return scalarProcessor.One;
        if (this == Nw) return scalarProcessor.MinusOne;

        return scalarProcessor.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> GetComponent<T>(LinBasisVector axis, IScalarProcessor<T> scalarProcessor)
    {
        if (axis.Index != Index)
            return scalarProcessor.Zero;

        return axis.Sign == Sign
            ? scalarProcessor.One
            : scalarProcessor.MinusOne;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> ToVector3D<T>(IScalarProcessor<T> scalarProcessor)
    {
        if (this == Px) return LinVector3D<T>.E1(scalarProcessor);
        if (this == Nx) return LinVector3D<T>.NegativeE1(scalarProcessor);
        if (this == Py) return LinVector3D<T>.E2(scalarProcessor);
        if (this == Ny) return LinVector3D<T>.NegativeE2(scalarProcessor);
        if (this == Pz) return LinVector3D<T>.E3(scalarProcessor);
        if (this == Nz) return LinVector3D<T>.NegativeE3(scalarProcessor);

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> ToVector3D<T>(Scalar<T> scalingFactor)
    {
        var zero = scalingFactor.ScalarProcessor.Zero;

        if (this == Px) return LinVector3D<T>.Create(scalingFactor, zero, zero);
        if (this == Nx) return LinVector3D<T>.Create(-scalingFactor, zero, zero);
        if (this == Py) return LinVector3D<T>.Create(zero, scalingFactor, zero);
        if (this == Ny) return LinVector3D<T>.Create(zero, -scalingFactor, zero);
        if (this == Pz) return LinVector3D<T>.Create(zero, zero, scalingFactor);
        if (this == Nz) return LinVector3D<T>.Create(zero, zero, -scalingFactor);

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> DirectionToUnitNormal3D<T>(IScalarProcessor<T> scalarProcessor)
    {
        if (this == Px) return LinBivector3D<T>.E32(scalarProcessor);
        if (this == Nx) return LinBivector3D<T>.E23(scalarProcessor);
        if (this == Py) return LinBivector3D<T>.E13(scalarProcessor);
        if (this == Ny) return LinBivector3D<T>.E31(scalarProcessor);
        if (this == Pz) return LinBivector3D<T>.E21(scalarProcessor);
        if (this == Nz) return LinBivector3D<T>.E12(scalarProcessor);

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> DirectionToUnitNormal3D<T>(Scalar<T> scalingFactor)
    {
        var scalarProcessor = scalingFactor.ScalarProcessor;

        if (this == Px) return LinBivector3D<T>.E32(scalarProcessor) * scalingFactor;
        if (this == Nx) return LinBivector3D<T>.E23(scalarProcessor) * scalingFactor;
        if (this == Py) return LinBivector3D<T>.E13(scalarProcessor) * scalingFactor;
        if (this == Ny) return LinBivector3D<T>.E31(scalarProcessor) * scalingFactor;
        if (this == Pz) return LinBivector3D<T>.E21(scalarProcessor) * scalingFactor;
        if (this == Nz) return LinBivector3D<T>.E12(scalarProcessor) * scalingFactor;

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> NormalToUnitDirection3D<T>(IScalarProcessor<T> scalarProcessor)
    {
        if (this == Px) return LinBivector3D<T>.E23(scalarProcessor);
        if (this == Nx) return LinBivector3D<T>.E32(scalarProcessor);
        if (this == Py) return LinBivector3D<T>.E31(scalarProcessor);
        if (this == Ny) return LinBivector3D<T>.E13(scalarProcessor);
        if (this == Pz) return LinBivector3D<T>.E12(scalarProcessor);
        if (this == Nz) return LinBivector3D<T>.E21(scalarProcessor);

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> NormalToUnitDirection3D<T>(Scalar<T> scalingFactor)
    {
        var scalarProcessor = scalingFactor.ScalarProcessor;

        if (this == Px) return LinBivector3D<T>.E23(scalarProcessor) * scalingFactor;
        if (this == Nx) return LinBivector3D<T>.E32(scalarProcessor) * scalingFactor;
        if (this == Py) return LinBivector3D<T>.E31(scalarProcessor) * scalingFactor;
        if (this == Ny) return LinBivector3D<T>.E13(scalarProcessor) * scalingFactor;
        if (this == Pz) return LinBivector3D<T>.E12(scalarProcessor) * scalingFactor;
        if (this == Nz) return LinBivector3D<T>.E21(scalarProcessor) * scalingFactor;

        throw new InvalidOperationException();
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> VectorESp<T>(ITriplet<Scalar<T>> v2)
    {
        if (this == Px) return v2.Item1;
        if (this == Py) return v2.Item2;
        if (this == Pz) return v2.Item3;
        if (this == Nx) return -v2.Item1;
        if (this == Ny) return -v2.Item2;
        if (this == Nz) return -v2.Item3;

        return v2.GetScalarProcessor().Zero;
    }

    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> VectorCross<T>(ITriplet<Scalar<T>> v2)
    {
        var zero = v2.GetScalarProcessor().Zero;

        if (this == Px) return LinVector3D<T>.Create(zero, -v2.Item3, v2.Item2);
        if (this == Py) return LinVector3D<T>.Create(v2.Item3, zero, -v2.Item1);
        if (this == Pz) return LinVector3D<T>.Create(-v2.Item2, v2.Item1, zero);
        if (this == Nx) return LinVector3D<T>.Create(zero, v2.Item3, -v2.Item2);
        if (this == Ny) return LinVector3D<T>.Create(-v2.Item3, zero, v2.Item1);
        if (this == Nz) return LinVector3D<T>.Create(v2.Item2, -v2.Item1, zero);

        throw new InvalidOperationException();
    }

    /// <summary>
    /// Returns the Euclidean cross product between the given vectors as a unit vector
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> VectorUnitCross<T>(ITriplet<Scalar<T>> v2)
    {
        return VectorCross(v2).ToUnitVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector4D<T> ToLinVector4D<T>(IScalarProcessor<T> scalarProcessor)
    {
        if (this == Px) return LinVector4D<T>.E1(scalarProcessor);
        if (this == Nx) return LinVector4D<T>.NegativeE1(scalarProcessor);
        if (this == Py) return LinVector4D<T>.E2(scalarProcessor);
        if (this == Ny) return LinVector4D<T>.NegativeE2(scalarProcessor);
        if (this == Pz) return LinVector4D<T>.E3(scalarProcessor);
        if (this == Nz) return LinVector4D<T>.NegativeE3(scalarProcessor);
        if (this == Pw) return LinVector4D<T>.E4(scalarProcessor);
        if (this == Nw) return LinVector4D<T>.NegativeE4(scalarProcessor);

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> ToZeroTerm<T>(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorTerm<T>(
            this,
            scalarProcessor.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> ToPositiveTerm<T>(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorTerm<T>(
            this,
            scalarProcessor.One
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> ToNegativeTerm<T>(IScalarProcessor<T> scalarProcessor)
    {
        return new LinVectorTerm<T>(
            this,
            scalarProcessor.MinusOne
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> ToTerm<T>(Scalar<T> scalar)
    {
        return new LinVectorTerm<T>(this, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorTerm<T> ToTerm<T>(IScalarProcessor<T> scalarProcessor, T scalarValue)
    {
        return new LinVectorTerm<T>(this, scalarProcessor, scalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
    {
        return $"({Sign})<{Index}>";
    }
}