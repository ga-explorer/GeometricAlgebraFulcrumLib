using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic.Vectors.Space3D;

/// <summary>
/// This class can represent a 3D quaternion
/// https://en.wikipedia.org/wiki/Quaternion
/// </summary>
public sealed record LinQuaternion<T> :
    ILinMultivector3D<T>
{
    public static LinQuaternion<T> Identity(IScalarProcessor<T> scalarProcessor)
        => new LinQuaternion<T>(
            scalarProcessor.Zero, 
            scalarProcessor.Zero, 
            scalarProcessor.Zero, 
            scalarProcessor.One
        );
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> Create(IScalarProcessor<T> scalarProcessor, float iScalar, float jScalar, float kScalar, float scalar)
    {
        return new LinQuaternion<T>(
            scalarProcessor.ScalarFromNumber(iScalar), 
            scalarProcessor.ScalarFromNumber(jScalar), 
            scalarProcessor.ScalarFromNumber(kScalar), 
            scalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> Create(IScalarProcessor<T> scalarProcessor, double iScalar, double jScalar, double kScalar, double scalar)
    {
        return new LinQuaternion<T>(
            scalarProcessor.ScalarFromNumber(iScalar), 
            scalarProcessor.ScalarFromNumber(jScalar), 
            scalarProcessor.ScalarFromNumber(kScalar), 
            scalarProcessor.ScalarFromNumber(scalar)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> Create(IScalarProcessor<T> scalarProcessor, T iScalar, T jScalar, T kScalar, T scalar)
    {
        return new LinQuaternion<T>(scalarProcessor, iScalar, jScalar, kScalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> Create(Scalar<T> iScalar, Scalar<T> jScalar, Scalar<T> kScalar, Scalar<T> scalar)
    {
        return new LinQuaternion<T>(iScalar, jScalar, kScalar, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> Create(Scalar<T> scalar, LinBivector3D<T> bivectorPart)
    {
        return new LinQuaternion<T>(bivectorPart, scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> Create(Scalar<T> scalar)
    {
        return new LinQuaternion<T>(LinBivector3D<T>.Zero(scalar.ScalarProcessor), scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> Create(LinBivector3D<T> bivectorPart)
    {
        return new LinQuaternion<T>(bivectorPart, bivectorPart.ScalarProcessor.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> Create(IScalarProcessor<T> scalarProcessor, Quaternion v)
    {
        return new LinQuaternion<T>(
            scalarProcessor.ScalarFromNumber(-v.X),
            scalarProcessor.ScalarFromNumber(-v.Y),
            scalarProcessor.ScalarFromNumber(-v.Z),
            scalarProcessor.ScalarFromNumber(v.W)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> CreateFromPlaneAndAngle(LinBivector3D<T> bivector, LinPolarAngle<T> angle)
    {
        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var bivectorPart =
            bivector * (halfAngleSin / bivector.Norm());

        return new LinQuaternion<T>(
            bivectorPart,
            halfAngleCos
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> CreateFromNormalAndAngle(LinBasisVector axis, LinPolarAngle<T> angle)
    {
        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var vector =
            axis.ToVector3D(-halfAngleSin);

        return new LinQuaternion<T>(
            vector.X,
            vector.Y,
            vector.Z,
            halfAngleCos
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> CreateFromNormalAndAngle(LinVector3D<T> axis, LinPolarAngle<T> angle)
    {
        var (halfAngleCos, halfAngleSin) = angle.HalfPolarAngle();

        var vector =
            axis.SetLength(-halfAngleSin);

        return new LinQuaternion<T>(
            vector.X,
            vector.Y,
            vector.Z,
            halfAngleCos
        );
    }

    ///// <summary>
    ///// Creates a quaternion from the specified rotation matrix.
    ///// </summary>
    ///// <param name="matrix">The rotation matrix.</param>
    ///// <returns>The newly created quaternion.</returns>
    //public static LinQuaternion<T> CreateFromRotationMatrix(Matrix4x4 matrix)
    //{
    //    var trace = matrix.M11 + matrix.M22 + matrix.M33;

    //    if (trace > 0.0f)
    //    {
    //        var s = Math.Sqrt(trace + 1d);
    //        var invS = 0.5d / s;

    //        return new LinQuaternion<T>(
    //            (matrix.M23 - matrix.M32) * invS,
    //            (matrix.M31 - matrix.M13) * invS,
    //            (matrix.M12 - matrix.M21) * invS,
    //            s * 0.5f
    //        );
    //    }

    //    if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
    //    {
    //        var s = Math.Sqrt(1d + matrix.M11 - matrix.M22 - matrix.M33);
    //        var invS = 0.5d / s;

    //        return new LinQuaternion<T>(
    //            0.5d * s,
    //            (matrix.M12 + matrix.M21) * invS,
    //            (matrix.M13 + matrix.M31) * invS,
    //            (matrix.M23 - matrix.M32) * invS
    //        );
    //    }

    //    if (matrix.M22 > matrix.M33)
    //    {
    //        var s = Math.Sqrt(1d + matrix.M22 - matrix.M11 - matrix.M33);
    //        var invS = 0.5d / s;

    //        return new LinQuaternion<T>(
    //            (matrix.M21 + matrix.M12) * invS,
    //            0.5d * s,
    //            (matrix.M32 + matrix.M23) * invS,
    //            (matrix.M31 - matrix.M13) * invS
    //        );
    //    }
    //    else
    //    {
    //        var s = Math.Sqrt(1d + matrix.M33 - matrix.M11 - matrix.M22);
    //        var invS = 0.5d / s;

    //        return new LinQuaternion<T>(
    //            (matrix.M31 + matrix.M13) * invS,
    //            (matrix.M32 + matrix.M23) * invS,
    //            0.5d * s,
    //            (matrix.M12 - matrix.M21) * invS
    //        );
    //    }
    //}

    /// <summary>
    /// Creates a new quaternion from the given yaw, pitch, and roll.
    /// </summary>
    /// <param name="yaw">The yaw angle, in radians, around the Y axis.</param>
    /// <param name="pitch">The pitch angle, in radians, around the X axis.</param>
    /// <param name="roll">The roll angle, in radians, around the Z axis.</param>
    /// <returns>The resulting quaternion.</returns>
    public static LinQuaternion<T> CreateFromYawPitchRoll(Scalar<T> yaw, Scalar<T> pitch, Scalar<T> roll)
    {
        //  Roll first, about axis the object is facing, then
        //  pitch upward, then yaw to face into the new heading
        var halfRoll = roll / 2;
        var sr = halfRoll.Sin();
        var cr = halfRoll.Cos();

        var halfPitch = pitch / 2;
        var sp = halfPitch.Sin();
        var cp = halfPitch.Cos();

        var halfYaw = yaw / 2;
        var sy = halfYaw.Sin();
        var cy = halfYaw.Cos();

        return new LinQuaternion<T>(
            cy * sp * cr + sy * cp * sr,
            sy * cp * cr - cy * sp * sr,
            cy * cp * sr - sy * sp * cr,
            cy * cp * cr + sy * sp * sr
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> operator -(LinQuaternion<T> v1)
    {
        return new LinQuaternion<T>(-v1.ScalarI, -v1.ScalarJ, -v1.ScalarK, -v1.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> operator +(LinQuaternion<T> v1, LinQuaternion<T> v2)
    {
        return new LinQuaternion<T>(v1.ScalarI + v2.ScalarI, v1.ScalarJ + v2.ScalarJ, v1.ScalarK + v2.ScalarK, v1.Scalar + v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> operator -(LinQuaternion<T> v1, LinQuaternion<T> v2)
    {
        return new LinQuaternion<T>(v1.ScalarI - v2.ScalarI, v1.ScalarJ - v2.ScalarJ, v1.ScalarK - v2.ScalarK, v1.Scalar - v2.Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> operator *(LinQuaternion<T> v1, double s)
    {
        return new LinQuaternion<T>(v1.ScalarI * s, v1.ScalarJ * s, v1.ScalarK * s, v1.Scalar * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> operator *(double s, LinQuaternion<T> v1)
    {
        return new LinQuaternion<T>(v1.ScalarI * s, v1.ScalarJ * s, v1.ScalarK * s, v1.Scalar * s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinQuaternion<T> operator /(LinQuaternion<T> v1, double s)
    {
        Debug.Assert(!s.IsNearZero());

        s = 1.0d / s;
        return new LinQuaternion<T>(v1.ScalarI * s, v1.ScalarJ * s, v1.ScalarK * s, v1.Scalar * s);
    }

    /// <summary>
    /// Quaternion product
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="q2"></param>
    /// <returns></returns>
    public static LinQuaternion<T> operator *(LinQuaternion<T> value1, LinQuaternion<T> q2)
    {
        var q1X = value1.ScalarI;
        var q1Y = value1.ScalarJ;
        var q1Z = value1.ScalarK;
        var q1W = value1.Scalar;

        var q2X = q2.ScalarI;
        var q2Y = q2.ScalarJ;
        var q2Z = q2.ScalarK;
        var q2W = q2.Scalar;

        // cross(av, bv)
        var cx = q1Y * q2Z - q1Z * q2Y;
        var cy = q1Z * q2X - q1X * q2Z;
        var cz = q1X * q2Y - q1Y * q2X;

        var dot = q1X * q2X + q1Y * q2Y + q1Z * q2Z;

        return new LinQuaternion<T>(
            q1X * q2W + q2X * q1W + cx,
            q1Y * q2W + q2Y * q1W + cy,
            q1Z * q2W + q2Z * q1W + cz,
            q1W * q2W - dot
        );
    }

    /// <summary>
    /// Quaternion division
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="q2"></param>
    /// <returns></returns>
    public static LinQuaternion<T> operator /(LinQuaternion<T> value1, LinQuaternion<T> q2)
    {
        var q1X = value1.ScalarI;
        var q1Y = value1.ScalarJ;
        var q1Z = value1.ScalarK;
        var q1W = value1.Scalar;

        //-------------------------------------
        // Inverse part.
        var ls =
            q2.ScalarI * q2.ScalarI + q2.ScalarJ * q2.ScalarJ +
            q2.ScalarK * q2.ScalarK + q2.Scalar * q2.Scalar;
        var invNorm = 1.0f / ls;

        var q2X = -q2.ScalarI * invNorm;
        var q2Y = -q2.ScalarJ * invNorm;
        var q2Z = -q2.ScalarK * invNorm;
        var q2W = q2.Scalar * invNorm;

        //-------------------------------------
        // Multiply part.

        // cross(av, bv)
        var cx = q1Y * q2Z - q1Z * q2Y;
        var cy = q1Z * q2X - q1X * q2Z;
        var cz = q1X * q2Y - q1Y * q2X;

        var dot = q1X * q2X + q1Y * q2Y + q1Z * q2Z;

        return new LinQuaternion<T>(
            q1X * q2W + q2X * q1W + cx,
            q1Y * q2W + q2Y * q1W + cy,
            q1Z * q2W + q2Z * q1W + cz,
            q1W * q2W - dot
        );
    }


    public IScalarProcessor<T> ScalarProcessor 
        => ScalarI.ScalarProcessor;

    public int VSpaceDimensions 
        => 3;

    /// <summary>
    /// The 1st component of this tuple. If this tuple holds a quaternion, this is the 1st component
    /// of its imaginary (i.e. vector) part
    /// </summary>
    public Scalar<T> ScalarI { get; }

    /// <summary>
    /// The 2nd component of this tuple. If this tuple holds a quaternion, this is the 2nd component
    /// of its imaginary (i.e. vector) part
    /// </summary>
    public Scalar<T> ScalarJ { get; }

    /// <summary>
    /// The 3rd component of this tuple. If this tuple holds a quaternion, this is the 3rd component
    /// of its imaginary (i.e. vector) part
    /// </summary>
    public Scalar<T> ScalarK { get; }

    /// <summary>
    /// The 4th component of this tuple. If this tuple holds a quaternion, this is its scalar part
    /// </summary>
    public Scalar<T> Scalar { get; }

    public Scalar<T> Scalar1
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar2
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar3
        => ScalarProcessor.Zero;

    public Scalar<T> Scalar12
        => -ScalarK;

    public Scalar<T> Scalar13
        => ScalarJ;

    public Scalar<T> Scalar23
        => -ScalarI;

    public Scalar<T> Scalar123
        => ScalarProcessor.Zero;

    public Scalar<T> Item1
        => ScalarI;

    public Scalar<T> Item2
        => ScalarJ;

    public Scalar<T> Item3
        => ScalarK;

    public Scalar<T> Item4
        => Scalar;

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
                0 => Scalar,
                3 => -ScalarK,
                5 => ScalarK,
                6 => -ScalarI,
                _ => ScalarProcessor.Zero
            };
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinQuaternion(IScalarProcessor<T> scalarProcessor, T iScalar, T jScalar, T kScalar, T scalar)
    {
        ScalarI = scalarProcessor.ScalarFromValue(iScalar);
        ScalarJ = scalarProcessor.ScalarFromValue(jScalar);
        ScalarK = scalarProcessor.ScalarFromValue(kScalar);
        Scalar = scalarProcessor.ScalarFromValue(scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinQuaternion(Scalar<T> iScalar, Scalar<T> jScalar, Scalar<T> kScalar, Scalar<T> scalar)
    {
        ScalarI = iScalar;
        ScalarJ = jScalar;
        ScalarK = kScalar;
        Scalar = scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinQuaternion(LinBivector3D<T> bivectorPart, Scalar<T> scalar)
    {
        ScalarI = -bivectorPart.Scalar23;
        ScalarJ = bivectorPart.Scalar13;
        ScalarK = -bivectorPart.Scalar12;
        Scalar = scalar;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinQuaternion(ILinMultivector3D<T> scalarBivector)
    {
        ScalarI = -scalarBivector.Scalar23;
        ScalarJ = scalarBivector.Scalar13;
        ScalarK = -scalarBivector.Scalar12;
        Scalar = scalarBivector.Scalar;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return ScalarI.IsValid() &&
               ScalarJ.IsValid() &&
               ScalarK.IsValid() &&
               Scalar.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero()
    {
        return Scalar.IsZero() &&
               ScalarI.IsZero() &&
               ScalarJ.IsZero() &&
               ScalarK.IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearZero()
    {
        return Scalar.IsNearZero() &&
               ScalarI.IsNearZero() &&
               ScalarJ.IsNearZero() &&
               ScalarK.IsNearZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNormalized()
    {
        return NormSquared().IsOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearNormalized()
    {
        return NormSquared().IsNearOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsIdentity()
    {
        return ScalarI.IsZero() &&
               ScalarJ.IsZero() &&
               ScalarK.IsZero() &&
               Scalar.IsOne();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearIdentity()
    {
        return (this - Identity(ScalarProcessor)).NormSquared().IsNearZero();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> GetNormalVector()
    {
        return LinVector3D<T>.Create(
            -ScalarI,
            -ScalarJ,
            -ScalarK
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> Norm()
    {
        return NormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormInverse()
    {
        return NormSquared().Sqrt().Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquared()
    {
        return ScalarI.Square() +
               ScalarJ.Square() +
               ScalarK.Square() +
               Scalar.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> NormSquaredInverse()
    {
        return NormSquared().Inverse();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinBivector3D<T> GetBivector()
    {
        return LinBivector3D<T>.Create(Scalar12, Scalar13, Scalar23);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Tuple<LinScalar3D<T>, LinBivector3D<T>> GetScalarAndBivector()
    {
        return new Tuple<LinScalar3D<T>, LinBivector3D<T>>(
            LinScalar3D<T>.Create(Scalar),
            LinBivector3D<T>.Create(Scalar12, Scalar13, Scalar23)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMultivector3D<T> ToMultivector3D()
    {
        return LinMultivector3D<T>.Create(
            LinScalar3D<T>.Create(Scalar),
            LinVector3D<T>.Zero(ScalarProcessor),
            LinBivector3D<T>.Create(Scalar12, Scalar13, Scalar23),
            LinTrivector3D<T>.Zero(ScalarProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Scalar<T> ESp(LinQuaternion<T> q2)
    {
        return ScalarI * q2.ScalarI +
               ScalarJ * q2.ScalarJ +
               ScalarK * q2.ScalarK +
               Scalar * q2.Scalar;
    }


    /// <summary>
    /// Quaternion product
    /// </summary>
    /// <param name="q2"></param>
    /// <returns></returns>
    public LinQuaternion<T> Times(LinQuaternion<T> q2)
    {
        // cross(av, bv)
        var cx = ScalarJ * q2.ScalarK - ScalarK * q2.ScalarJ;
        var cy = ScalarK * q2.ScalarI - ScalarI * q2.ScalarK;
        var cz = ScalarI * q2.ScalarJ - ScalarJ * q2.ScalarI;

        var dot =
            ScalarI * q2.ScalarI +
            ScalarJ * q2.ScalarJ +
            ScalarK * q2.ScalarK;

        return new LinQuaternion<T>(
            ScalarI * q2.Scalar + q2.ScalarI * Scalar + cx,
            ScalarJ * q2.Scalar + q2.ScalarJ * Scalar + cy,
            ScalarK * q2.Scalar + q2.ScalarK * Scalar + cz,
            Scalar * q2.Scalar - dot
        );
    }

    /// <summary>
    /// Quaternion division
    /// </summary>
    /// <param name="q2"></param>
    /// <returns></returns>
    public LinQuaternion<T> Divide(LinQuaternion<T> q2)
    {
        //-------------------------------------
        // Inverse part.
        var invNorm = q2.NormInverse();

        var q2X = -q2.ScalarI * invNorm;
        var q2Y = -q2.ScalarJ * invNorm;
        var q2Z = -q2.ScalarK * invNorm;
        var q2W = q2.Scalar * invNorm;

        //-------------------------------------
        // Multiply part.

        // cross(av, bv)
        var cx = ScalarJ * q2Z - ScalarK * q2Y;
        var cy = ScalarK * q2X - ScalarI * q2Z;
        var cz = ScalarI * q2Y - ScalarJ * q2X;

        var dot = ScalarI * q2X + ScalarJ * q2Y + ScalarK * q2Z;

        return new LinQuaternion<T>(
            ScalarI * q2W + q2X * Scalar + cx,
            ScalarJ * q2W + q2Y * Scalar + cy,
            ScalarK * q2W + q2Z * Scalar + cz,
            Scalar * q2W - dot
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinQuaternion<T> Conjugate()
    {
        return new LinQuaternion<T>(
            -ScalarI,
            -ScalarJ,
            -ScalarK,
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinQuaternion<T> Reverse()
    {
        return new LinQuaternion<T>(
            -ScalarI,
            -ScalarJ,
            -ScalarK,
            Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinQuaternion<T> Inverse()
    {
        //  -1   (       a              -v       )
        // q   = ( -------------   ------------- )
        //       (  a^2 + |v|^2  ,  a^2 + |v|^2  )
        var invNormSquared = NormSquaredInverse();

        return new LinQuaternion<T>(
            -ScalarI * invNormSquared,
            -ScalarJ * invNormSquared,
            -ScalarK * invNormSquared,
            Scalar * invNormSquared
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinQuaternion<T> Normalize()
    {
        var invNorm = NormInverse();

        return new LinQuaternion<T>(
            ScalarI * invNorm,
            ScalarJ * invNorm,
            ScalarK * invNorm,
            Scalar * invNorm
        );
    }

    public LinQuaternion<T> Lerp(LinQuaternion<T> q2, Scalar<T> t)
    {
        var t1 = 1 - t;

        var dot =
            ScalarI * q2.ScalarI +
            ScalarJ * q2.ScalarJ +
            ScalarK * q2.ScalarK +
            Scalar * q2.Scalar;

        var r =
            dot >= 0d
                ? new LinQuaternion<T>(
                    t1 * ScalarI + t * q2.ScalarI,
                    t1 * ScalarJ + t * q2.ScalarJ,
                    t1 * ScalarK + t * q2.ScalarK,
                    t1 * Scalar + t * q2.Scalar
                ) :
                new LinQuaternion<T>(
                    t1 * ScalarI - t * q2.ScalarI,
                    t1 * ScalarJ - t * q2.ScalarJ,
                    t1 * ScalarK - t * q2.ScalarK,
                    t1 * Scalar - t * q2.Scalar
                );

        // Normalize it.
        return r.Normalize();
    }

    public LinQuaternion<T> Slerp(LinQuaternion<T> q2, Scalar<T> t)
    {
        var cosOmega =
            ScalarI * q2.ScalarI +
            ScalarJ * q2.ScalarJ +
            ScalarK * q2.ScalarK +
            Scalar * q2.Scalar;

        var flip = false;

        if (cosOmega < 0d)
        {
            flip = true;
            cosOmega = -cosOmega;
        }

        Scalar<T> s1, s2;

        if (cosOmega.IsNearOne())
        {
            // Too close, do straight linear interpolation.
            s1 = 1 - t;
            s2 = flip ? -t : t;
        }
        else
        {
            var omega = cosOmega.ArcCos();
            var invSinOmega = 1d / omega.Sin();

            s1 = ((1 - t) * omega).Sin() * invSinOmega;
            s2 = flip
                ? -(t * omega).Sin() * invSinOmega
                : (t * omega).Sin() * invSinOmega;
        }

        return new LinQuaternion<T>(
            s1 * ScalarI + s2 * q2.ScalarI,
            s1 * ScalarJ + s2 * q2.ScalarJ,
            s1 * ScalarK + s2 * q2.ScalarK,
            s1 * Scalar + s2 * q2.Scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinQuaternion<T> Concatenate(LinQuaternion<T> q2)
    {
        // Concatenate rotation is actually q2 * q1 instead of q1 * q2.
        // So that's why q2 goes q1 and value1 goes q2.

        // cross(av, bv)
        var cx = q2.ScalarJ * ScalarK - q2.ScalarK * ScalarJ;
        var cy = q2.ScalarK * ScalarI - q2.ScalarI * ScalarK;
        var cz = q2.ScalarI * ScalarJ - q2.ScalarJ * ScalarI;

        var dot =
            q2.ScalarI * ScalarI +
            q2.ScalarJ * ScalarJ +
            q2.ScalarK * ScalarK;

        return new LinQuaternion<T>(
            q2.ScalarI * Scalar + ScalarI * q2.Scalar + cx,
            q2.ScalarJ * Scalar + ScalarJ * q2.Scalar + cy,
            q2.ScalarK * Scalar + ScalarK * q2.Scalar + cz,
            q2.Scalar * Scalar - dot
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinQuaternion<T> Concatenate(LinQuaternion<T> q2, LinQuaternion<T> quaternion3)
    {
        return Concatenate(q2).Concatenate(quaternion3);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinQuaternion<T> Concatenate(params LinQuaternion<T>[] quaternionList)
    {
        return quaternionList.Aggregate(
            this,
            Concatenate
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Scalar<T>> GetHalfAngleCosSin(bool assumeNormalized = false)
    {
        var scalar = assumeNormalized
            ? Scalar
            : Scalar / Norm();

        var sinHalfAngle = (1d - scalar.Square()).Sqrt();

        return new Pair<Scalar<T>>(scalar, sinHalfAngle);
    }

    public Tuple<LinAngle<T>, LinBivector3D<T>> GetAngleAndBivector(bool assumeNormalized = false)
    {
        var quaternion = assumeNormalized
            ? this
            : Normalize();

        var (scalar, bivector) =
            quaternion.GetScalarAndBivector();

        var angle = scalar.ArcCos();

        return new Tuple<LinAngle<T>, LinBivector3D<T>>(
            angle,
            bivector / angle.Sin()
        );
    }

    public Tuple<LinAngle<T>, LinVector3D<T>> GetAngleAndNormal(bool assumeNormalized = false)
    {
        var quaternion = assumeNormalized
            ? this
            : Normalize();

        var (scalar, bivector) =
            quaternion.GetScalarAndBivector();

        var angle = scalar.ArcCos();

        var normal =
            bivector.DirectionToUnitNormal3D(LinVector3D<T>.UnitSymmetric(ScalarProcessor));

        return new Tuple<LinAngle<T>, LinVector3D<T>>(angle, normal);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> ToRotationVector(bool assumeNormalized = false)
    {
        var (angle, normal) = GetAngleAndNormal(assumeNormalized);

        var length =
            angle.Radians.Divide(ScalarProcessor.PiTimes2Value);

        return normal.SetLength(length);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SquareMatrix3<T> ToSquareMatrix3()
    {
        var normSquared = NormSquared();

        if (normSquared.IsNearZero())
            throw new InvalidOperationException();

        var s = 2d / normSquared;

        return new SquareMatrix3<T>(ScalarProcessor)
        {
            Scalar00 = 1d - s * (ScalarJ * ScalarJ + ScalarK * ScalarK),
            Scalar10 = s * (ScalarI * ScalarJ - Scalar * ScalarK),
            Scalar20 = s * (ScalarI * ScalarK + Scalar * ScalarJ),

            Scalar01 = s * (ScalarI * ScalarJ + Scalar * ScalarK),
            Scalar11 = 1d - s * (ScalarI * ScalarI + ScalarK * ScalarK),
            Scalar21 = s * (ScalarJ * ScalarK - Scalar * ScalarI),

            Scalar02 = s * (ScalarI * ScalarK - Scalar * ScalarJ),
            Scalar12 = s * (ScalarJ * ScalarK + Scalar * ScalarI),
            Scalar22 = 1d - s * (ScalarI * ScalarI + ScalarJ * ScalarJ)
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<LinVector3D<T>> RotateBasisVectors()
    {
        var normSquared = NormSquared();

        if (normSquared.IsNearZero())
            throw new InvalidOperationException();

        var s = 2d / normSquared;

        var v1 = LinVector3D<T>.Create(
            1d - s * (ScalarJ * ScalarJ + ScalarK * ScalarK),
            s * (ScalarI * ScalarJ - Scalar * ScalarK),
            s * (ScalarI * ScalarK + Scalar * ScalarJ)
        );

        var v2 = LinVector3D<T>.Create(
            s * (ScalarI * ScalarJ + Scalar * ScalarK),
            1d - s * (ScalarI * ScalarI + ScalarK * ScalarK),
            s * (ScalarJ * ScalarK - Scalar * ScalarI)
        );

        var v3 = LinVector3D<T>.Create(
            s * (ScalarI * ScalarK - Scalar * ScalarJ),
            s * (ScalarJ * ScalarK + Scalar * ScalarI),
            1d - s * (ScalarI * ScalarI + ScalarJ * ScalarJ)
        );

        return new Triplet<LinVector3D<T>>(v1, v2, v3);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> RotateVector(Scalar<T> x, Scalar<T> y, Scalar<T> z)
    {
        return RotateVector(
            LinVector3D<T>.Create(x, y, z)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> RotateVector(LinBasisVector axis)
    {
        return ToSquareMatrix3() * axis.ToVector3D(ScalarProcessor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVector3D<T> RotateVector(ILinVector3D<T> vector)
    {
        return ToSquareMatrix3() * vector;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinVector3D<T>> RotateVectors(LinBasisVector axis1, LinBasisVector axis2)
    {
        var rotationMatrix =
            ToSquareMatrix3();

        return new Pair<LinVector3D<T>>(
            rotationMatrix * axis1.ToVector3D(ScalarProcessor),
            rotationMatrix * axis2.ToVector3D(ScalarProcessor)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinVector3D<T>> RotateVectors(ILinVector3D<T> vector1, ILinVector3D<T> vector2)
    {
        var rotationMatrix =
            ToSquareMatrix3();

        return new Pair<LinVector3D<T>>(
            rotationMatrix * vector1,
            rotationMatrix * vector2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triplet<LinVector3D<T>> RotateVectors(ILinVector3D<T> vector1, ILinVector3D<T> vector2, ILinVector3D<T> vector3)
    {
        var rotationMatrix =
            ToSquareMatrix3();

        return new Triplet<LinVector3D<T>>(
            rotationMatrix * vector1,
            rotationMatrix * vector2,
            rotationMatrix * vector3
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IReadOnlyList<LinVector3D<T>> RotateVectors(params ILinVector3D<T>[] vectorArray)
    {
        var rotationMatrix =
            ToSquareMatrix3();

        return vectorArray
            .Select(vector => rotationMatrix * vector)
            .ToImmutableArray();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinVector3D<T>> RotateVectors(IEnumerable<ILinVector3D<T>> vectorList)
    {
        var rotationMatrix =
            ToSquareMatrix3();

        return vectorList.Select(vector => rotationMatrix * vector);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public Quaternion ToSystemNumericsQuaternion()
    //{
    //    return new Quaternion(
    //        (float)ScalarI,
    //        (float)ScalarJ,
    //        (float)ScalarK,
    //        (float)Scalar
    //    );
    //}

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
        return $"'{Scalar}'<> + '{ScalarK}'<1,2> + '{ScalarI}'<2,3> + '{ScalarJ}'<3,1>";
    }
}