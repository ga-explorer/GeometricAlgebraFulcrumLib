using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.SubSpaces.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.LinearMaps.SpaceND.Rotation;

public abstract class LinFloat64PlanarRotation :
    LinFloat64SimpleRotation,
    ILinFloat64PlanarRotation
{
    public override int VSpaceDimensions
        => BasisVector1.VSpaceDimensions;

    public int SubspaceDimensions
        => 2;

    /// <summary>
    /// The unit vector where the rotation starts
    /// </summary>
    public abstract LinFloat64Vector BasisVector1 { get; }

    /// <summary>
    /// A scaled version of the orthogonal component (rejection) of
    /// RotatedBasisVector1 relative to BasisVector1
    /// </summary>
    public abstract LinFloat64Vector BasisVector2 { get; }

    public LinFloat64PolarAngle RotationAngle { get; }

    public double RotationAngleCos 
        => RotationAngle.CosValue;

    public double RotationAngleSin 
        => RotationAngle.SinValue;

    public IEnumerable<LinFloat64Vector> BasisVectors
    {
        get
        {
            yield return BasisVector1;
            yield return BasisVector2;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected LinFloat64PlanarRotation(LinFloat64PolarAngle rotationAngle)
    {
        RotationAngle = rotationAngle;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsValid()
    {
        return RotationAngle.IsValid() &&
               BasisVector1.IsValid() &&
               BasisVector2.IsValid() &&
               BasisVector1.IsNearOrthonormalWith(BasisVector2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsIdentity()
    {
        return (RotationAngleCos - 1d).IsZero();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override bool IsNearIdentity(double zeroEpsilon = 1e-12d)
    {
        return (RotationAngleCos - 1d).IsNearZero(zeroEpsilon);
    }


    public abstract Pair<double> BasisESp(int axisIndex);

    public abstract Pair<double> BasisESp(ILinSignedBasisVector axis);

    public abstract Pair<double> BasisESp(LinFloat64Vector vector);

    public abstract LinFloat64Vector MapBasisVector1();

    public abstract LinFloat64Vector MapBasisVector2();

    public abstract LinFloat64Vector MapBasisVector1(LinFloat64Angle rotationAngle);

    public abstract LinFloat64Vector MapBasisVector2(LinFloat64Angle rotationAngle);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64Vector> MapBasisVector1(LinFloat64Angle angle1, LinFloat64Angle angle2)
    {
        return new Pair<LinFloat64Vector>(
            MapBasisVector1(angle1),
            MapBasisVector1(angle2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64Vector> MapBasisVector2(LinFloat64Angle angle1, LinFloat64Angle angle2)
    {
        return new Pair<LinFloat64Vector>(
            MapBasisVector2(angle1),
            MapBasisVector2(angle2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64Vector> MapBasisVectors(LinFloat64Angle rotationAngle)
    {
        return new Pair<LinFloat64Vector>(
            MapBasisVector1(rotationAngle),
            MapBasisVector2(rotationAngle)
        );
    }


    public abstract LinFloat64Vector GetVectorProjection(LinFloat64Vector vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector vector)
    {
        return LinFloat64PolarAngle.CreateFromVector(BasisESp(vector));
    }

    public abstract LinFloat64Vector GetVectorRejection(LinFloat64Vector vector);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(LinFloat64Vector vector, double zeroEpsilon = 1E-12D)
    {
        return GetVectorRejection(vector).IsNearZero(zeroEpsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace subspace, double zeroEpsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, zeroEpsilon));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector MapVectorProjection(LinFloat64Vector vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        // Compute the scalar factors of u1, u2
        var u1Scalar = RotationAngleCos * vpx - RotationAngleSin * vpy;
        var u2Scalar = RotationAngleCos * vpy + RotationAngleSin * vpx;

        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector MapVectorProjection(LinFloat64Vector vector, LinFloat64DirectedAngle rotationAngle)
    {
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleCos = rotationAngle.Cos();
        var rotationAngleSin = rotationAngle.Sin();

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCos * vpx - rotationAngleSin * vpy;
        var u2Scalar = rotationAngleCos * vpy + rotationAngleSin * vpx;

        return LinFloat64VectorComposer
            .Create()
            .SetVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation MapPlanarRotation(LinFloat64PlanarRotation planarRotation)
    {
        var basisVector1 = MapVector(planarRotation.BasisVector1);
        var basisVector2 = MapVector(planarRotation.BasisVector2);

        return LinFloat64VectorToVectorRotation.CreateFromOrthonormalVectors(
            basisVector1,
            basisVector2,
            planarRotation.RotationAngle
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector GetMiddleUnitVector1()
    {
        return MapBasisVector1(RotationAngle.HalfPolarAngle());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Vector GetMiddleUnitVector2()
    {
        return MapBasisVector2(RotationAngle.HalfPolarAngle());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation GetBasisAlignmentRotationTo(LinFloat64PlanarRotation planarRotation2)
    {
        //var sameBasisVector1 = BasisVector1.IsEqual(planarRotation2.BasisVector1);
        //var sameBasisVector2 = BasisVector2.IsEqual(planarRotation2.BasisVector2);

        //if (sameBasisVector1 && sameBasisVector2)
        //    return SetRotationAngle(Float64PlanarAngle.Angle0);

        //if (sameBasisVector1)
        //    return LinFloat64PlanarRotation3D.CreateFromRotatedVector(
        //        BasisVector2,
        //        planarRotation2.BasisVector2
        //    );

        //if (sameBasisVector2)
        //    return LinFloat64PlanarRotation3D.CreateFromRotatedVector(
        //        BasisVector1,
        //        planarRotation2.BasisVector1
        //    );

        return GetInversePlanarRotation().MapPlanarRotation(planarRotation2);
    }

    public LinFloat64PlanarRotation InterpolateTo(LinFloat64PlanarRotation targetRotation, double tValue)
    {
        var basisRotation = GetBasisAlignmentRotationTo(targetRotation);

        basisRotation = basisRotation.SetRotationAngle(
            tValue.Lerp(basisRotation.RotationAngle).ToPolarAngle()
        );

        var basisVector1 = basisRotation.MapVector(BasisVector1);
        var basisVector2 = basisRotation.MapVector(BasisVector2);

        var rotationAngle = tValue.Lerp(
            RotationAngle,
            targetRotation.RotationAngle
        ).ToPolarAngle();

        return LinFloat64VectorToVectorRotation.CreateFromOrthonormalVectors(
            basisVector1,
            basisVector2,
            rotationAngle
        );
    }

    public IEnumerable<LinFloat64PlanarRotation> InterpolateTo(LinFloat64PlanarRotation targetRotation, int count, bool isPeriodicRange = false)
    {
        var basisRotationFull = GetBasisAlignmentRotationTo(targetRotation);
        var basisRotationFullAngle = basisRotationFull.RotationAngle;

        var tValueList =
            0d.GetLinearRange(1d, count, isPeriodicRange);

        foreach (var tValue in tValueList)
        {
            var basisRotation =
                basisRotationFull.SetRotationAngle(
                    tValue.Lerp(basisRotationFullAngle).ToPolarAngle()
                );

            var basisVector1 = basisRotation.MapVector(BasisVector1);
            var basisVector2 = basisRotation.MapVector(BasisVector2);

            var rotationAngle = tValue.Lerp(
                RotationAngle,
                targetRotation.RotationAngle
            ).ToPolarAngle();

            yield return LinFloat64VectorToVectorRotation.CreateFromOrthonormalVectors(
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
    public LinFloat64PlanarRotation InterpolateRotationAngle(double tValue, bool invertRotation = false)
    {
        return SetRotationAngle(
            tValue.Lerp(RotationAngle).ToPolarAngle(),
            invertRotation
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64PlanarRotation> InterpolateRotationAngle(int count, bool isPeriodicRange = false, bool invertRotation = false)
    {
        var rotationAngleList =
            0d.GetLinearRange(RotationAngle.DegreesValue, count, isPeriodicRange)
                .Select(angle => angle.DegreesToPolarAngle());

        return SetRotationAngle(rotationAngleList, invertRotation);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation SetRotationAngle(LinFloat64PolarAngle rotationAngle, bool invertRotation = false)
    {
        if (invertRotation)
            return LinFloat64VectorToVectorRotation.CreateFromOrthonormalVectors(
                BasisVector2,
                BasisVector1,
                rotationAngle
            );

        return LinFloat64VectorToVectorRotation.CreateFromOrthonormalVectors(
            BasisVector1,
            BasisVector2,
            rotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64PlanarRotation> SetRotationAngle(IEnumerable<LinFloat64PolarAngle> rotationAngleList, bool invertRotation = false)
    {
        return rotationAngleList.Select(angle =>
            SetRotationAngle(angle, invertRotation)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation RotateBasisVectors(LinFloat64Angle rotationAngle)
    {
        var (basisVector1, basisVector2) =
            MapBasisVectors(rotationAngle);

        return LinFloat64VectorToVectorRotation.CreateFromOrthonormalVectors(
            basisVector1,
            basisVector2,
            RotationAngle
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation AlignBasisVector1(LinFloat64Vector vector, bool assumeProjected = false)
    {
        var basisVector1 =
            assumeProjected
                ? vector
                : GetVectorProjection(vector);

        if (vector.IsNearZero())
            return this;

        basisVector1 = basisVector1.ToUnitLinVector();

        var rotationAngle = BasisVector1.GetUnitVectorsAngle(basisVector1);

        return RotateBasisVectors(rotationAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation AlignBasisVector2(LinFloat64Vector vector, bool assumeProjected = false)
    {
        var basisVector2 =
            assumeProjected
                ? vector
                : GetVectorProjection(vector);

        if (vector.IsNearZero())
            return this;

        basisVector2 = basisVector2.ToUnitLinVector();

        var rotationAngle = BasisVector2.GetUnitVectorsAngle(basisVector2);

        return RotateBasisVectors(rotationAngle);
    }


    public abstract LinFloat64PlanarRotation GetInversePlanarRotation();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public sealed override LinFloat64SimpleRotation GetInverseSimpleRotation()
    {
        return GetInversePlanarRotation();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<LinFloat64HyperPlaneNormalReflection> GetHyperPlaneReflectionPair()
    {
        return new Pair<LinFloat64HyperPlaneNormalReflection>(
            LinFloat64HyperPlaneNormalReflection.Create(BasisVector1),
            LinFloat64HyperPlaneNormalReflection.Create(GetMiddleUnitVector1())
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64HyperPlaneNormalReflectionSequence ToHyperPlaneReflectionSequence()
    {
        var reflection =
            LinFloat64HyperPlaneNormalReflectionSequence.Create(VSpaceDimensions);

        var (r1, r2) =
            GetHyperPlaneReflectionPair();

        reflection
            .AppendMap(r1)
            .AppendMap(r2);

        return reflection;
    }
}