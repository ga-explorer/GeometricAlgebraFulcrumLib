using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space3D.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Reflection;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.SpaceND.Scaling;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Dictionary;
using GeometricAlgebraFulcrumLib.Matlab.Structures.System;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;

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


        
        private EqualityComparer()
        {
        }


        
        public bool Equals(LinBasisVector? x, LinBasisVector? y)
        {
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            if (ReferenceEquals(x, y))
                return true;

            return x.IsNegative.Equals(y.IsNegative) && x.Index.Equals(y.Index);
        }

        
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


    //
    //public static implicit operator LinBasisVector(int basisVectorIndex)
    //{
    //    return new LinBasisVector(basisVectorIndex, false);
    //}


    
    public static LinBasisVector operator +(LinBasisVector b1)
    {
        return b1;
    }

    
    public static LinBasisVector operator -(LinBasisVector b1)
    {
        return Create(b1.Index, !b1.IsNegative);
    }

    
    public static LinBasisVector operator *(LinBasisVector b1, IntegerSign s2)
    {
        return Create(b1.Index, b1.IsNegative ? -s2 : s2);
    }

    
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


    
    private LinBasisVector(int basisVectorIndex, bool isNegative)
    {
        Index = basisVectorIndex;
        IsNegative = isNegative;
    }
    
    
    public void Deconstruct(out int basisBladeIndex, out IntegerSign sign)
    {
        basisBladeIndex = Index;
        sign = Sign;
    }


    
    public bool IsValid()
    {
        return true;
    }

    
    
    public LinBasisVector ToPositiveBasis()
    {
        return IsPositive ? this : Create(Index, false);
    }

    
    public LinBasisVector ToNegativeBasis()
    {
        return IsNegative ? this : Create(Index, true);
    }

    
    public LinBasisVector ShiftIndex(int offset, int vSpaceDimensions)
    {
        if (vSpaceDimensions < 1)
            throw new InvalidOperationException();

        return Create(
            (Index + offset) % vSpaceDimensions, 
            IsNegative
        );
    }

    
    public LinBasisVector Negative()
    {
        return Create(Index, !IsNegative);
    }

    
    public LinBasisVector Times(IntegerSign sign)
    {
        if (sign.IsZero)
            throw new InvalidOperationException();

        return sign.IsPositive ? this : Negative();
    }

    //
    //public IntegerSign EGpSign(int basisBlade)
    //{
    //    var sign = _basisVector.EGpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    //
    //public IntegerSign EGpSign(LinBasisVector basisBlade)
    //{
    //    var sign = _basisVector.EGpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    //
    //public IntegerSign OpSign(int basisBlade)
    //{
    //    var sign = _basisVector.OpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    //
    //public IntegerSign OpSign(LinBasisVector basisBlade)
    //{
    //    var sign = _basisVector.OpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    //
    //public IntegerSign ESpSign(int basisBlade)
    //{
    //    var sign = _basisVector.ESpSign(basisBlade);

    //    return IsNegative ? -sign : sign;
    //}

    //
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
    
    public double GetAngleCos(LinBasisVector v2)
    {
        return GetComponent(v2);
    }

    /// <summary>
    /// Find the angle between this vector and another
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public double GetAngleCos(ITriplet<double> v2)
    {
        return (v2.GetComponent(this) / v2.VectorENorm()).Clamp(-1d, 1d);
    }

    
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


    
    public bool IsParallelTo(LinBasisVector axis)
    {
        return Index == axis.Index;
    }

    
    public bool IsOppositeTo(LinBasisVector axis)
    {
        return Index == axis.Index &&
               Sign == -axis.Sign;
    }

    
    public bool Equals(LinBasisVector? other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;

        return IsNegative == other.IsNegative && 
               Index == other.Index;
    }

    
    public override int GetHashCode()
    {
        return HashCode.Combine(Sign, Index);
    }


    
    public LinFloat64VectorTerm ToVectorTerm(double scalar)
    {
        return new LinFloat64VectorTerm(
            this,
            scalar * Sign
        );
    }
    
    
    public LinFloat64VectorTerm ToZeroVectorTerm()
    {
        return new LinFloat64VectorTerm(ToPositiveBasis(), 0d);
    }

    
    public LinFloat64VectorTerm ToPositiveUnitVectorTerm()
    {
        return new LinFloat64VectorTerm(ToPositiveBasis(), 1d);
    }

    
    public LinFloat64VectorTerm ToNegativeUnitVectorTerm()
    {
        return new LinFloat64VectorTerm(ToPositiveBasis(), -1d);
    }


    
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

    
    public LinFloat64Vector ToLinVector()
    {
        if (IsZero)
            return LinFloat64Vector.Zero;

        var basisScalarDictionary =
            new SingleItemDictionary<int, double>(Index, IsPositive ? 1d : -1d);

        return LinFloat64Vector.Create(basisScalarDictionary);
    }


    
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
    
    public LinFloat64Quaternion RotationAxisAngleToQuaternion(LinFloat64Angle angle)
    {
        return LinFloat64Quaternion.CreateFromAxisAngle(this, angle);
    }

    public LinFloat64Vector3D VectorToVectorRotationVector(ILinFloat64Vector3D dstVector)
    {
        var (u, a) =
            VectorToVectorRotationAxisAngle(dstVector);

        return u.VectorTimes(a.RadiansValue / (2 * Math.PI));
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
        if (angleCos.IsNearMinusOne())
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

    
    public LinBasisVector GetNormal(int vSpaceDimensions)
    {
        return NextBasisVector(vSpaceDimensions);
    }
    
    
    public LinBasisVector GetNormal3D()
    {
        return NextBasisVector3D();
    }

    
    public LinBasisVector GetUnitNormal(int vSpaceDimensions)
    {
        return NextBasisVector(vSpaceDimensions);
    }
    
    
    public LinBasisVector GetUnitNormal3D()
    {
        return NextBasisVector3D();
    }

    
    public Pair<LinBasisVector> GetNormalPair3D()
    {
        var e1 = NextBasisVector3D();
        var e2 = e1.NextBasisVector3D();

        return new Pair<LinBasisVector>(e1, e2);
    }

    
    public Pair<LinBasisVector> GetUnitNormalPair3D()
    {
        return GetNormalPair3D();
    }
    
    
    public LinBasisVector NextBasisVector(int vSpaceDimensions)
    {
        if (Index >= vSpaceDimensions)
            throw new InvalidOperationException();

        return new LinBasisVector(
            (Index + 1) % vSpaceDimensions, 
            IsNegative
        );
    }
    
    
    public LinBasisVector NextBasisVector2D()
    {
        if (this == Px) return Py;
        if (this == Py) return Px;
        
        if (this == Nx) return Ny;
        if (this == Ny) return Nx;

        throw new InvalidOperationException();
    }

    
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

    
    public double GetX()
    {
        if (this == Px) return 1d;
        if (this == Nx) return -1d;

        return 0d;
    }

    
    public double GetY()
    {
        if (this == Py) return 1d;
        if (this == Ny) return -1d;

        return 0d;
    }

    
    public double GetZ()
    {
        if (this == Pz) return 1d;
        if (this == Nz) return -1d;

        return 0d;
    }

    
    public double GetW()
    {
        if (this == Pw) return 1d;
        if (this == Nw) return -1d;

        return 0d;
    }

    
    public double GetComponent(LinBasisVector axis)
    {
        if (axis.Index != Index)
            return 0d;

        return axis.Sign == Sign
            ? 1d
            : -1;
    }

    
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

    
    public LinFloat64Bivector3D DirectionToUnitNormal3D(double scalingFactor)
    {
        if (this == Px) return LinFloat64Bivector3D.E32 * scalingFactor;
        if (this == Nx) return LinFloat64Bivector3D.E23 * scalingFactor;
        if (this == Py) return LinFloat64Bivector3D.E13 * scalingFactor;
        if (this == Ny) return LinFloat64Bivector3D.E31 * scalingFactor;
        if (this == Pz) return LinFloat64Bivector3D.E21 * scalingFactor;
        if (this == Nz) return LinFloat64Bivector3D.E12 * scalingFactor;

        throw new InvalidOperationException();
    }

    
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

    
    public LinFloat64Bivector3D NormalToUnitDirection3D(double scalingFactor)
    {
        if (this == Px) return LinFloat64Bivector3D.E23 * scalingFactor;
        if (this == Nx) return LinFloat64Bivector3D.E32 * scalingFactor;
        if (this == Py) return LinFloat64Bivector3D.E31 * scalingFactor;
        if (this == Ny) return LinFloat64Bivector3D.E13 * scalingFactor;
        if (this == Pz) return LinFloat64Bivector3D.E12 * scalingFactor;
        if (this == Nz) return LinFloat64Bivector3D.E21 * scalingFactor;

        throw new InvalidOperationException();
    }

    
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

    
    public bool IsXAxis()
    {
        return this == Px || this == Nx;
    }

    
    public bool IsYAxis()
    {
        return this == Py || this == Ny;
    }

    
    public bool IsZAxis()
    {
        return this == Pz || this == Nz;
    }
    
    
    public bool IsWAxis()
    {
        return this == Pw || this == Nw;
    }

    /// <summary>
    /// The Euclidean dot product between the given vectors
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public double VectorESp(ITriplet<double> v2)
    {
        if (this == Px) return v2.Item1;
        if (this == Nx) return -v2.Item1;

        if (this == Py) return v2.Item2;
        if (this == Ny) return -v2.Item2;

        if (this == Pz) return v2.Item3;
        if (this == Nz) return -v2.Item3;

        return 0d;
    }

    /// <summary>
    /// The Euclidean cross product between the given vectors
    /// </summary>
    /// <param name="v2"></param>
    /// <returns></returns>
    
    public LinFloat64Vector3D VectorCross(ITriplet<double> v2)
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
    
    public LinFloat64Vector3D VectorUnitCross(ITriplet<double> v2)
    {
        return VectorCross(v2).ToUnitLinVector3D();
    }

    
    public override string ToString()
    {
        return $"({Sign})<{Index}>";
    }
}