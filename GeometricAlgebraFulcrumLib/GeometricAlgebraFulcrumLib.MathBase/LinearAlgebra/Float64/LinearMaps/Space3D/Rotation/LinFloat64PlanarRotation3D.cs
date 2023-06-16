using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.SubSpaces.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;

/// <summary>
/// This type of rotation is specified very similar to 2D rotations:
/// We use a pair of orthonormal vectors (u1, u2) to specify the plane of rotation
/// and an angle of rotation to fully specify the end-vector starting from u1.
/// </summary>
public sealed class LinFloat64PlanarRotation3D :
    LinFloat64Rotation3D,
    ILinFloat64Subspace3D
{
    public static LinFloat64PlanarRotation3D Identity { get; }
        = new LinFloat64PlanarRotation3D(
            Float64Vector3D.E1,
            Float64Vector3D.E2,
            Float64PlanarAngle.Angle0
        );

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromSpanningVectors(IFloat64Tuple3D spanningVector1, IFloat64Tuple3D spanningVector2, Float64PlanarAngle rotationAngle)
    {
        Debug.Assert(
            !spanningVector1.IsNearParallelTo(spanningVector2)
        );

        var basisVector1 =
            spanningVector1.ToUnitVector();

        var basisVector2 =
            spanningVector2.IsNearOppositeToUnit(basisVector1)
                ? basisVector1.GetUnitNormal()
                : spanningVector2.RejectOnUnitVector(basisVector1).ToUnitVector();

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromOrthogonalVectors(IFloat64Tuple3D spanningVector1, IFloat64Tuple3D spanningVector2, Float64PlanarAngle rotationAngle)
    {
        var basisVector1 = spanningVector1.ToUnitVector();
        var basisVector2 = spanningVector2.ToUnitVector();

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromOrthonormalVectors(IFloat64Tuple3D spanningVector1, IFloat64Tuple3D spanningVector2, Float64PlanarAngle rotationAngle)
    {
        var basisVector1 = spanningVector1.ToVector3D();
        var basisVector2 = spanningVector2.ToVector3D();

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromRotatedVector(IFloat64Tuple3D vector, IFloat64Tuple3D rotatedVector, bool useShortArc = true)
    {
        var basisVector1 =
            vector.ToUnitVector();

        var rotationAngle =
            useShortArc
                ? rotatedVector.GetAngleWithUnit(basisVector1)
                : Float64PlanarAngle.Angle360 - rotatedVector.GetAngleWithUnit(basisVector1);

        if (rotationAngle.IsNearStraight() || rotationAngle.IsNearZeroOrFullRotation())
            return new LinFloat64PlanarRotation3D(
                basisVector1,
                basisVector1.GetUnitNormal(),
                rotationAngle
            );

        var basisVector2 =
            useShortArc
                ? rotatedVector.RejectOnUnitVector(basisVector1).ToUnitVector()
                : rotatedVector.RejectOnUnitVector(basisVector1).ToNegativeUnitVector();

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromRotatedUnitVector(IFloat64Tuple3D vector, IFloat64Tuple3D rotatedVector, bool useShortArc = true)
    {
        var basisVector1 =
            vector.ToVector3D();

        var rotationAngle =
            useShortArc
                ? rotatedVector.GetAngleWithUnit(basisVector1)
                : Float64PlanarAngle.Angle360 - rotatedVector.GetAngleWithUnit(basisVector1);

        if (rotationAngle.IsNearStraight())
            return new LinFloat64PlanarRotation3D(
                basisVector1,
                basisVector1.GetUnitNormal(),
                rotationAngle
            );

        var basisVector2 =
            rotatedVector.RejectOnUnitVector(vector).ToUnitVector();

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromPlanarRotation(LinFloat64PlanarRotation3D rotation)
    {
        return new LinFloat64PlanarRotation3D(
            rotation.BasisVector1,
            rotation.BasisVector2,
            rotation.RotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromRotation(LinFloat64Rotation3D rotation)
    {
        if (rotation is LinFloat64IdentityLinearMap3D)
            return new LinFloat64PlanarRotation3D(
                Float64Vector3D.E1,
                Float64Vector3D.E2,
                Float64PlanarAngle.Angle0
            );

        if (rotation is LinFloat64PlanarRotation3D planarRotation)
            return new LinFloat64PlanarRotation3D(
                planarRotation.BasisVector1,
                planarRotation.BasisVector2,
                planarRotation.RotationAngle
            );

        throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromComplexEigenPair(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
    {
        var rotationAngle = Math.Atan2(
            eigenValue.Imaginary,
            eigenValue.Real
        ).RadiansToAngle();

        //TODO: Why is this the correct one, but not the reverse??!!
        var basisVector1 = eigenVector.Imaginary().ToVector3D(true);
        var basisVector2 = eigenVector.Real().ToVector3D(true);

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromComplexEigenPair(double realValue, double imagValue, double[] realVector, double[] imagVector)
    {
        var rotationAngle = Math.Atan2(
            imagValue,
            realValue
        ).RadiansToAngle();

        //TODO: Why is this the correct one, but not the reverse??!!
        var basisVector1 = imagVector.ToVector3D(true);
        var basisVector2 = realVector.ToVector3D(true);

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    
    public int SubspaceDimensions 
        => 2;

    public Float64Vector3D BasisVector1 { get; }

    public Float64Vector3D BasisVector2 { get; }
    
    public IEnumerable<Float64Vector3D> BasisVectors
    {
        get
        {
            yield return BasisVector1;
            yield return BasisVector2;
        }
    }

    /// <summary>
    /// The rotation a angle, automatically normalized in the range [0, 360] degrees
    /// </summary>
    public Float64PlanarAngle RotationAngle { get; }

    public double RotationAngleCos { get; }

    public double RotationAngleSin { get; }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64PlanarRotation3D(Float64Vector3D basisVector1, Float64Vector3D basisVector2, Float64PlanarAngle rotationAngle)
    {
        BasisVector1 = basisVector1;
        BasisVector2 = basisVector2;
        RotationAngle = rotationAngle.GetAngleInPositiveRange();
        RotationAngleCos = rotationAngle.Cos();
        RotationAngleSin = rotationAngle.Sin();

        Debug.Assert(
            IsValid()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BasisVector1.IsValid() &&
               BasisVector2.IsValid() &&
               RotationAngle.IsValid() &&
               BasisVector1.IsNearOrthonormalWith(BasisVector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsIdentity()
    {
        return (RotationAngle.Cos() - 1d).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsNearIdentity(double epsilon = 1e-12d)
    {
        return (RotationAngleCos - 1d).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearReflection(double epsilon = 1e-12d)
    {
        return (RotationAngleCos + 1d).IsNearZero(epsilon);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64Tuple3D GetUnitNormal()
    {
        return BasisVector1.VectorCross(BasisVector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Quaternion GetQuaternion()
    {
        return GetUnitNormal().AxisAngleToQuaternion(RotationAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Quaternion GetQuaternion(Float64PlanarAngle rotationAngle)
    {
        return GetUnitNormal().AxisAngleToQuaternion(rotationAngle);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> BasisESp(int axisIndex)
    {
        return new Pair<double>(
            BasisVector1[1 << axisIndex],
            BasisVector2[1 << axisIndex]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> BasisESp(LinUnitBasisVector3D axis)
    {
        return new Pair<double>(
            BasisVector1[axis],
            BasisVector2[axis]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> BasisESp(IFloat64Tuple3D vector)
    {
        return new Pair<double>(
            vector.X.Value * BasisVector1.X.Value +
            vector.Y.Value * BasisVector1.Y.Value +
            vector.Z.Value * BasisVector1.Z.Value,

            vector.X.Value * BasisVector2.X.Value +
            vector.Y.Value * BasisVector2.Y.Value +
            vector.Z.Value * BasisVector2.Z.Value
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector3D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex is >= 0 and < 3
        );

        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(basisIndex);

        var rotationAngleCosMinusOne = RotationAngleCos - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngleSin * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngleSin * vpx;

        // The final rotated vector
        return Float64Vector3DComposer
            .Create()
            .SetTerm(basisIndex, 1d)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D MapVector(LinUnitBasisVector3D vector)
    {
        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleCosMinusOne = RotationAngleCos - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngleSin * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngleSin * vpx;

        // The final rotated vector
        return Float64Vector3DComposer
            .Create()
            .SetTerm(vector)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector3D MapVector(IFloat64Tuple3D vector)
    {
        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleCosMinusOne = RotationAngleCos - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngleSin * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngleSin * vpx;

        // The final rotated vector
        return Float64Vector3DComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D MapBasisVector(int basisIndex, Float64PlanarAngle rotationAngle)
    {
        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(basisIndex);

        var rotationAngleSin = rotationAngle.Sin();
        var rotationAngleCosMinusOne = rotationAngle.Cos() - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - rotationAngleSin * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + rotationAngleSin * vpx;

        // The final rotated vector
        return Float64Vector3DComposer
            .Create()
            .SetTerm(basisIndex, 1d)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D MapVector(LinUnitBasisVector3D vector, Float64PlanarAngle rotationAngle)
    {
        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleSin = rotationAngle.Sin();
        var rotationAngleCosMinusOne = rotationAngle.Cos() - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - rotationAngleSin * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + rotationAngleSin * vpx;

        // The final rotated vector
        return Float64Vector3DComposer
            .Create()
            .SetTerm(vector)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D MapVector(IFloat64Tuple3D vector, Float64PlanarAngle rotationAngle)
    {
        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleSin = rotationAngle.Sin();
        var rotationAngleCosMinusOne = rotationAngle.Cos() - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - rotationAngleSin * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + rotationAngleSin * vpx;

        // The final rotated vector
        return Float64Vector3DComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D MapBasisVector1()
    {
        return Float64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, RotationAngleCos)
            .AddVector(BasisVector2, RotationAngleSin)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D MapBasisVector2()
    {
        return Float64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, -RotationAngleSin)
            .AddVector(BasisVector2, RotationAngleCos)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D MapBasisVector1(Float64PlanarAngle rotationAngle)
    {
        return Float64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, rotationAngle.Cos())
            .AddVector(BasisVector2, rotationAngle.Sin())
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D MapBasisVector2(Float64PlanarAngle rotationAngle)
    {
        return Float64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, -rotationAngle.Sin())
            .AddVector(BasisVector2, rotationAngle.Cos())
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64Vector3D> MapBasisVector1(Float64PlanarAngle angle1, Float64PlanarAngle angle2)
    {
        return new Pair<Float64Vector3D>(
            MapBasisVector1(angle1),
            MapBasisVector1(angle2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64Vector3D> MapBasisVector2(Float64PlanarAngle angle1, Float64PlanarAngle angle2)
    {
        return new Pair<Float64Vector3D>(
            MapBasisVector2(angle1),
            MapBasisVector2(angle2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64Vector3D> MapBasisVectors(Float64PlanarAngle rotationAngle)
    {
        return new Pair<Float64Vector3D>(
            MapBasisVector1(rotationAngle),
            MapBasisVector2(rotationAngle)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetVectorProjection(IFloat64Tuple3D vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        return Float64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, vpx)
            .AddVector(BasisVector2, vpy)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle GetVectorProjectionPolarAngle(IFloat64Tuple3D vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        return Math.Atan2(vpy, vpx).RadiansToAngle().GetAngleInPositiveRange();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetVectorRejection(IFloat64Tuple3D vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        return Float64Vector3DComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisVector1, -vpx)
            .AddVector(BasisVector2, -vpy)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D MapVectorProjection(IFloat64Tuple3D vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        // Compute the scalar factors of u1, u2
        var u1Scalar = RotationAngleCos * vpx - RotationAngleSin * vpy;
        var u2Scalar = RotationAngleCos * vpy + RotationAngleSin * vpx;

        return Float64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D MapVectorProjection(IFloat64Tuple3D vector, Float64PlanarAngle rotationAngle)
    {
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleCos = rotationAngle.Cos();
        var rotationAngleSin = rotationAngle.Sin();

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCos * vpx - rotationAngleSin * vpy;
        var u2Scalar = rotationAngleCos * vpy + rotationAngleSin * vpx;

        return Float64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation3D MapPlanarRotation(LinFloat64PlanarRotation3D planarRotation)
    {
        var basisVector1 = MapVector(planarRotation.BasisVector1);
        var basisVector2 = MapVector(planarRotation.BasisVector2);

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            planarRotation.RotationAngle
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetMiddleUnitVector1()
    {
        return MapBasisVector1(RotationAngle / 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector3D GetMiddleUnitVector2()
    {
        return MapBasisVector2(RotationAngle / 2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(IFloat64Tuple3D vector, double epsilon = 1E-12D)
    {
        return GetVectorRejection(vector).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace3D subspace, double epsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, epsilon));
    }

    /// <summary>
    /// Create the simplest planar rotation that aligns the plane of this rotation
    /// with the plane of the target rotation, the basis vectors might not be aligned.
    /// </summary>
    /// <param name="planarRotation2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation3D GetPlaneAlignmentRotationTo(LinFloat64PlanarRotation3D planarRotation2)
    {
        //TODO: Generalize this to n-dimensions
        return CreateFromRotatedVector(
            GetUnitNormal(),
            planarRotation2.GetUnitNormal()
        );
    }

    /// <summary>
    /// Create a planar rotation that rotates the basis vectors of this
    /// planar rotation into the basis vectors of the given planar rotation
    /// </summary>
    /// <param name="planarRotation2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation3D GetBasisAlignmentRotationTo(LinFloat64PlanarRotation3D planarRotation2)
    {
        //var r = this.ToSquareMatrix3().GetPlanarRotation3D();

        //Debug.Assert(
        //    r.NearContains(this) &&
        //    r.RotationAngle.IsNearEqual(RotationAngle)
        //);

        //TODO: Generalize this to n-dimensions
        var r1 = CreateFromRotatedVector(
            BasisVector1,
            planarRotation2.BasisVector1
        );
        
        var r2 = CreateFromRotatedVector(
            r1.MapVector(BasisVector2),
            planarRotation2.BasisVector2
        );
        
        var m = 
            r2.ToSquareMatrix3() * r1.ToSquareMatrix3();

        return m.GetPlanarRotation3D();
    }

    /// <summary>
    /// Find an intermediate planar rotation that interpolates this into the
    /// given planar rotation
    /// </summary>
    /// <param name="targetRotation"></param>
    /// <param name="tValue"></param>
    /// <returns></returns>
    public LinFloat64PlanarRotation3D InterpolateTo(LinFloat64PlanarRotation3D targetRotation, double tValue)
    {
        //var planeRotation = GetPlaneAlignmentRotationTo(targetRotation);
        var planeRotation = GetBasisAlignmentRotationTo(targetRotation);
        
        // This is only needed if we use plane alignment, not basis alignment
        var angleDelta = 
            targetRotation.GetVectorProjectionPolarAngle(
                planeRotation.MapVector(BasisVector1)
            );
        
        planeRotation = planeRotation.SetRotationAngle(
            tValue.Lerp(planeRotation.RotationAngle)
        );

        var basisVector1 = planeRotation.MapVector(BasisVector1);
        var basisVector2 = planeRotation.MapVector(BasisVector2);
        
        var rotationAngle = tValue.Lerp(
            RotationAngle, 
            targetRotation.RotationAngle - angleDelta
        );

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    /// <summary>
    /// Find intermediate planar rotations that interpolate this into the
    /// given planar rotation
    /// </summary>
    /// <param name="targetRotation"></param>
    /// <param name="count"></param>
    /// <param name="isPeriodicRange"></param>
    /// <returns></returns>
    public IEnumerable<LinFloat64PlanarRotation3D> InterpolateTo(LinFloat64PlanarRotation3D targetRotation, int count, bool isPeriodicRange = false)
    {
        var planeRotation = GetPlaneAlignmentRotationTo(targetRotation);
        //var planeRotation = GetBasisAlignmentRotationTo(targetRotation);
        var planeRotationAngle = planeRotation.RotationAngle;
        
        // This is only needed if we use plane alignment, not basis alignment
        var angleDelta =
            targetRotation.BasisVector1.GetUnitVectorsAngle(
                planeRotation.MapVector(BasisVector1)
            );

        var tValueList =
            0d.GetLinearRange(1d, count, isPeriodicRange);

        foreach (var tValue in tValueList)
        {
            var basisRotation =
                planeRotation.SetRotationAngle(
                    tValue.Lerp(planeRotationAngle)
                );

            var basisVector1 = basisRotation.MapVector(BasisVector1);
            var basisVector2 = basisRotation.MapVector(BasisVector2);
            
            var rotationAngle = tValue.Lerp(
                RotationAngle, 
                targetRotation.RotationAngle - angleDelta
            );

            yield return new LinFloat64PlanarRotation3D(
                basisVector1,
                basisVector2,
                rotationAngle
            );
        }
    }

    /// <summary>
    /// Get all planar rotations with the same plane of rotation and different angles
    /// evenly spaced in the range [0, this.RotationAngle]
    /// </summary>
    /// <param name="tValue"></param>
    /// <param name="invertRotation"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation3D InterpolateRotationAngle(double tValue, bool invertRotation = false)
    {
        var rotationAngle =
            tValue.Lerp(RotationAngle);

        return SetRotationAngle(rotationAngle, invertRotation);
    }

    /// <summary>
    /// Get all planar rotations with the same plane of rotation and different angles
    /// evenly spaced in the range [0, this.RotationAngle]
    /// </summary>
    /// <param name="count"></param>
    /// <param name="isPeriodicRange"></param>
    /// <param name="invertRotation"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64PlanarRotation3D> InterpolateRotationAngle(int count, bool isPeriodicRange = false, bool invertRotation = false)
    {
        var rotationAngleList =
            0d.GetLinearRange(RotationAngle.Degrees, count, isPeriodicRange)
                .Select(angle => angle.DegreesToAngle());

        return SetRotationAngle(rotationAngleList, invertRotation);
    }

    /// <summary>
    /// Create a new planar rotation with the same plane and different angle.
    /// If the flag invertRotation is true, this also swaps the two basis vectors
    /// of rotation
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <param name="invertRotation"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation3D SetRotationAngle(Float64PlanarAngle rotationAngle, bool invertRotation = false)
    {
        if (invertRotation)
            return new LinFloat64PlanarRotation3D(
                BasisVector2,
                BasisVector1,
                rotationAngle
            );

        return new LinFloat64PlanarRotation3D(
            BasisVector1,
            BasisVector2,
            rotationAngle
        );
    }

    /// <summary>
    /// Get all planar rotations with the same plane of rotation and different angles
    /// </summary>
    /// <param name="rotationAngleList"></param>
    /// <param name="invertRotation"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64PlanarRotation3D> SetRotationAngle(IEnumerable<Float64PlanarAngle> rotationAngleList, bool invertRotation = false)
    {
        return rotationAngleList.Select(angle =>
            SetRotationAngle(angle, invertRotation)
        );
    }

    /// <summary>
    /// Create the same planar rotation by rotating both basis vectors with
    /// the given angle in the plane of rotation
    /// </summary>
    /// <param name="rotationAngle"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation3D RotateBasisVectors(Float64PlanarAngle rotationAngle)
    {
        var (basisVector1, basisVector2) =
            MapBasisVectors(rotationAngle);

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            RotationAngle
        );
    }

    /// <summary>
    /// Create the same planar rotation by rotating both basis vectors so
    /// that the first basis vector is aligned with the projection of the
    /// given vector on the plane of rotation
    /// remains the same.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="assumeProjected"></param>
    /// <returns></returns>
    public LinFloat64PlanarRotation3D AlignBasisVector1(IFloat64Tuple3D vector, bool assumeProjected = false)
    {
        var basisVector1 =
            assumeProjected
                ? vector.ToVector3D()
                : GetVectorProjection(vector);

        if (vector.IsNearZero())
            return this;

        basisVector1 = basisVector1.ToUnitVector();

        var rotationAngle = BasisVector1.GetUnitVectorsAngle(basisVector1);

        return RotateBasisVectors(rotationAngle);
    }

    /// <summary>
    /// Create the same planar rotation by rotating both basis vectors so
    /// that the second basis vector is aligned with the projection of the
    /// given vector on the plane of rotation
    /// remains the same.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="assumeProjected"></param>
    /// <returns></returns>
    public LinFloat64PlanarRotation3D AlignBasisVector2(IFloat64Tuple3D vector, bool assumeProjected = false)
    {
        var basisVector2 =
            assumeProjected
                ? vector.ToVector3D()
                : GetVectorProjection(vector);

        if (vector.IsNearZero())
            return this;

        basisVector2 = basisVector2.ToUnitVector();

        var rotationAngle = BasisVector2.GetUnitVectorsAngle(basisVector2);

        return RotateBasisVectors(rotationAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation3D GetInversePlanarRotation()
    {
        return new LinFloat64PlanarRotation3D(
            BasisVector2,
            BasisVector1,
            RotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation3D GetInversePlanarRotation(Float64PlanarAngle rotationAngle)
    {
        return new LinFloat64PlanarRotation3D(
            BasisVector2,
            BasisVector1,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Rotation3D GetInverseRotation()
    {
        return GetInversePlanarRotation();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64HyperPlaneNormalReflection3D> GetHyperPlaneReflectionPair()
    {
        return new Pair<LinFloat64HyperPlaneNormalReflection3D>(
            LinFloat64HyperPlaneNormalReflection3D.Create(BasisVector1),
            LinFloat64HyperPlaneNormalReflection3D.Create(GetMiddleUnitVector1())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64HyperPlaneNormalReflectionSequence3D ToHyperPlaneReflectionSequence()
    {
        var reflection =
            LinFloat64HyperPlaneNormalReflectionSequence3D.Create();

        var (r1, r2) =
            GetHyperPlaneReflectionPair();

        reflection
            .AppendMap(r1)
            .AppendMap(r2);

        return reflection;
    }
}