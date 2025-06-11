using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.Space3D.Rotation;

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
            LinFloat64Vector3D.E1,
            LinFloat64Vector3D.E2,
            LinFloat64PolarAngle.Angle0
        );

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromSpanningVectors(ILinFloat64Vector3D spanningVector1, ILinFloat64Vector3D spanningVector2, LinFloat64PolarAngle rotationAngle)
    {
        Debug.Assert(
            !spanningVector1.IsNearParallelTo(spanningVector2)
        );

        var basisVector1 =
            spanningVector1.ToUnitLinVector3D();

        var basisVector2 =
            spanningVector2.IsNearOppositeToUnit(basisVector1)
                ? basisVector1.GetUnitNormal()
                : spanningVector2.RejectOnUnitVector(basisVector1).ToUnitLinVector3D();

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromOrthogonalVectors(ILinFloat64Vector3D spanningVector1, ILinFloat64Vector3D spanningVector2, LinFloat64PolarAngle rotationAngle)
    {
        var basisVector1 = spanningVector1.ToUnitLinVector3D();
        var basisVector2 = spanningVector2.ToUnitLinVector3D();

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromOrthonormalVectors(ILinFloat64Vector3D spanningVector1, ILinFloat64Vector3D spanningVector2, LinFloat64PolarAngle rotationAngle)
    {
        var basisVector1 = spanningVector1.ToLinVector3D();
        var basisVector2 = spanningVector2.ToLinVector3D();

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromRotatedVector(ILinFloat64Vector3D vector, ILinFloat64Vector3D rotatedVector, bool useShortArc = true)
    {
        var basisVector1 =
            vector.ToUnitLinVector3D();

        var rotationAngle =
            useShortArc
                ? rotatedVector.GetAngleWithUnit(basisVector1)
                : LinFloat64PolarAngle.Angle360 - rotatedVector.GetAngleWithUnit(basisVector1);

        if (rotationAngle.IsNearStraight() || rotationAngle.IsNearZeroOrFull())
            return new LinFloat64PlanarRotation3D(
                basisVector1,
                basisVector1.GetUnitNormal(),
                rotationAngle.ToPolarAngle()
            );

        var basisVector2 =
            useShortArc
                ? rotatedVector.RejectOnUnitVector(basisVector1).ToUnitLinVector3D()
                : rotatedVector.RejectOnUnitVector(basisVector1).NegativeUnitVector();

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle.ToPolarAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromRotatedUnitVector(ILinFloat64Vector3D vector, ILinFloat64Vector3D rotatedVector, bool useShortArc = true)
    {
        var basisVector1 =
            vector.ToLinVector3D();

        var rotationAngle =
            useShortArc
                ? rotatedVector.GetAngleWithUnit(basisVector1)
                : LinFloat64PolarAngle.Angle360 - rotatedVector.GetAngleWithUnit(basisVector1);

        if (rotationAngle.IsNearStraight())
            return new LinFloat64PlanarRotation3D(
                basisVector1,
                basisVector1.GetUnitNormal(),
                rotationAngle.ToPolarAngle()
            );

        var basisVector2 =
            rotatedVector.RejectOnUnitVector(vector).ToUnitLinVector3D();

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle.ToPolarAngle()
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
                LinFloat64Vector3D.E1,
                LinFloat64Vector3D.E2,
                LinFloat64PolarAngle.Angle0
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
        var rotationAngle = eigenValue.GetPhaseAsPolarAngle();

        //TODO: Why is this the correct one, but not the reverse??!!
        var basisVector1 = eigenVector.Imaginary().ToLinVector3D(true);
        var basisVector2 = eigenVector.Real().ToLinVector3D(true);

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PlanarRotation3D CreateFromComplexEigenPair(double realValue, double imagValue, double[] realVector, double[] imagVector)
    {
        var rotationAngle = LinFloat64PolarAngle.CreateFromVector(realValue, imagValue);

        //TODO: Why is this the correct one, but not the reverse??!!
        var basisVector1 = imagVector.ToLinVector3D(true);
        var basisVector2 = realVector.ToLinVector3D(true);

        return new LinFloat64PlanarRotation3D(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }


    public int SubspaceDimensions
        => 2;

    public LinFloat64Vector3D BasisVector1 { get; }

    public LinFloat64Vector3D BasisVector2 { get; }

    public IEnumerable<LinFloat64Vector3D> BasisVectors
    {
        get
        {
            yield return BasisVector1;
            yield return BasisVector2;
        }
    }

    /// <summary>
    /// The rotation an angle, automatically normalized in the range [0, 360] degrees
    /// </summary>
    public LinFloat64PolarAngle RotationAngle { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64PlanarRotation3D(LinFloat64Vector3D basisVector1, LinFloat64Vector3D basisVector2, LinFloat64PolarAngle rotationAngle)
    {
        BasisVector1 = basisVector1;
        BasisVector2 = basisVector2;
        RotationAngle = rotationAngle;

        //Debug.Assert(IsValid());
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
    public override bool IsNearIdentity(double zeroEpsilon = 1e-12d)
    {
        return RotationAngle.IsNearZeroOrFull(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsNearReflection(double zeroEpsilon = 1e-12d)
    {
        return RotationAngle.IsNearStraight(zeroEpsilon);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinFloat64Vector3D GetUnitNormal()
    {
        return BasisVector1.VectorCross(BasisVector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Quaternion GetQuaternion()
    {
        return GetUnitNormal().RotationAxisAngleToQuaternion(RotationAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Quaternion GetQuaternion(LinFloat64Angle rotationAngle)
    {
        return GetUnitNormal().RotationAxisAngleToQuaternion(rotationAngle);
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
    public Pair<double> BasisESp(LinBasisVector axis)
    {
        return new Pair<double>(
            BasisVector1[axis],
            BasisVector2[axis]
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<double> BasisESp(ILinFloat64Vector3D vector)
    {
        return new Pair<double>(
            vector.X.ScalarValue * BasisVector1.X.ScalarValue +
            vector.Y.ScalarValue * BasisVector1.Y.ScalarValue +
            vector.Z.ScalarValue * BasisVector1.Z.ScalarValue,

            vector.X.ScalarValue * BasisVector2.X.ScalarValue +
            vector.Y.ScalarValue * BasisVector2.Y.ScalarValue +
            vector.Z.ScalarValue * BasisVector2.Z.ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex is >= 0 and < 3
        );

        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(basisIndex);

        var rotationAngleCosMinusOne = RotationAngle.CosValue - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngle.SinValue * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngle.SinValue * vpx;

        // The final rotated vector
        return LinFloat64Vector3DComposer
            .Create()
            .SetTerm(basisIndex, 1d)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(LinBasisVector vector)
    {
        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleCosMinusOne = RotationAngle.CosValue - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngle.SinValue * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngle.SinValue * vpx;

        // The final rotated vector
        return LinFloat64Vector3DComposer
            .Create()
            .SetTerm(vector)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector)
    {
        // Compute the projection components of the given vector on
        // the orthonormal basis vectors defining the plane of rotation
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleCosMinusOne = RotationAngle.CosValue - 1d;

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCosMinusOne * vpx - RotationAngle.SinValue * vpy;
        var u2Scalar = rotationAngleCosMinusOne * vpy + RotationAngle.SinValue * vpx;

        // The final rotated vector
        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapBasisVector(int basisIndex, LinFloat64Angle rotationAngle)
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
        return LinFloat64Vector3DComposer
            .Create()
            .SetTerm(basisIndex, 1d)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(LinBasisVector vector, LinFloat64Angle rotationAngle)
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
        return LinFloat64Vector3DComposer
            .Create()
            .SetTerm(vector)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVector(ILinFloat64Vector3D vector, LinFloat64Angle rotationAngle)
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
        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapBasisVector1()
    {
        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, RotationAngle.CosValue)
            .AddVector(BasisVector2, RotationAngle.SinValue)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapBasisVector2()
    {
        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, -RotationAngle.SinValue)
            .AddVector(BasisVector2, RotationAngle.CosValue)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapBasisVector1(LinFloat64Angle rotationAngle)
    {
        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, rotationAngle.CosValue)
            .AddVector(BasisVector2, rotationAngle.SinValue)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapBasisVector2(LinFloat64Angle rotationAngle)
    {
        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, -rotationAngle.SinValue)
            .AddVector(BasisVector2, rotationAngle.CosValue)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64Vector3D> MapBasisVector1(LinFloat64Angle angle1, LinFloat64Angle angle2)
    {
        return new Pair<LinFloat64Vector3D>(
            MapBasisVector1(angle1),
            MapBasisVector1(angle2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64Vector3D> MapBasisVector2(LinFloat64Angle angle1, LinFloat64Angle angle2)
    {
        return new Pair<LinFloat64Vector3D>(
            MapBasisVector2(angle1),
            MapBasisVector2(angle2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64Vector3D> MapBasisVectors(LinFloat64Angle rotationAngle)
    {
        return new Pair<LinFloat64Vector3D>(
            MapBasisVector1(rotationAngle),
            MapBasisVector2(rotationAngle)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetVectorProjection(ILinFloat64Vector3D vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, vpx)
            .AddVector(BasisVector2, vpy)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(ILinFloat64Vector3D vector)
    {
        return LinFloat64PolarAngle.CreateFromVector(
            BasisESp(vector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetVectorRejection(ILinFloat64Vector3D vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(vector)
            .AddVector(BasisVector1, -vpx)
            .AddVector(BasisVector2, -vpy)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVectorProjection(ILinFloat64Vector3D vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        // Compute the scalar factors of u1, u2
        var u1Scalar = RotationAngle.CosValue * vpx - RotationAngle.SinValue * vpy;
        var u2Scalar = RotationAngle.CosValue * vpy + RotationAngle.SinValue * vpx;

        return LinFloat64Vector3DComposer
            .Create()
            .SetVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D MapVectorProjection(ILinFloat64Vector3D vector, LinFloat64Angle rotationAngle)
    {
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleCos = rotationAngle.Cos();
        var rotationAngleSin = rotationAngle.Sin();

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCos * vpx - rotationAngleSin * vpy;
        var u2Scalar = rotationAngleCos * vpy + rotationAngleSin * vpx;

        return LinFloat64Vector3DComposer
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
    public LinFloat64Vector3D GetMiddleUnitVector1()
    {
        return MapBasisVector1(RotationAngle.HalfPolarAngle());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector3D GetMiddleUnitVector2()
    {
        return MapBasisVector2(RotationAngle.HalfPolarAngle());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Vector3D vector, double zeroEpsilon = 1E-12D)
    {
        return GetVectorRejection(vector).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace3D subspace, double zeroEpsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, zeroEpsilon));
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
            tValue.Lerp(planeRotation.RotationAngle).ToPolarAngle()
        );

        var basisVector1 = planeRotation.MapVector(BasisVector1);
        var basisVector2 = planeRotation.MapVector(BasisVector2);

        var rotationAngle = tValue.Lerp(
            RotationAngle,
            targetRotation.RotationAngle.AngleSubtract(angleDelta.RadiansValue)
        ).ToPolarAngle();

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
                    tValue.Lerp(planeRotationAngle).ToPolarAngle()
                );

            var basisVector1 = basisRotation.MapVector(BasisVector1);
            var basisVector2 = basisRotation.MapVector(BasisVector2);

            var rotationAngle = tValue.Lerp(
                RotationAngle,
                targetRotation.RotationAngle.AngleSubtract(angleDelta.RadiansValue)
            ).ToPolarAngle();

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
            tValue.Lerp(RotationAngle).ToPolarAngle();

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
            0d.GetLinearRange(RotationAngle.DegreesValue, count, isPeriodicRange)
                .Select(angle => angle.DegreesToPolarAngle());

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
    public LinFloat64PlanarRotation3D SetRotationAngle(LinFloat64PolarAngle rotationAngle, bool invertRotation = false)
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
    public IEnumerable<LinFloat64PlanarRotation3D> SetRotationAngle(IEnumerable<LinFloat64PolarAngle> rotationAngleList, bool invertRotation = false)
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
    public LinFloat64PlanarRotation3D RotateBasisVectors(LinFloat64Angle rotationAngle)
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
    public LinFloat64PlanarRotation3D AlignBasisVector1(ILinFloat64Vector3D vector, bool assumeProjected = false)
    {
        var basisVector1 =
            assumeProjected
                ? vector.ToLinVector3D()
                : GetVectorProjection(vector);

        if (vector.IsNearZero())
            return this;

        basisVector1 = basisVector1.ToUnitLinVector3D();

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
    public LinFloat64PlanarRotation3D AlignBasisVector2(ILinFloat64Vector3D vector, bool assumeProjected = false)
    {
        var basisVector2 =
            assumeProjected
                ? vector.ToLinVector3D()
                : GetVectorProjection(vector);

        if (vector.IsNearZero())
            return this;

        basisVector2 = basisVector2.ToUnitLinVector3D();

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
    public LinFloat64PlanarRotation3D GetInversePlanarRotation(LinFloat64PolarAngle rotationAngle)
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