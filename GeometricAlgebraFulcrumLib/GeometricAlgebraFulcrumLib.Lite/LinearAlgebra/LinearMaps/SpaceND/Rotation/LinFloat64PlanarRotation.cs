using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Reflection;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.SubSpaces.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND.Rotation;

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
    public abstract Float64Vector BasisVector1 { get; }

    /// <summary>
    /// A scaled version of the orthogonal component (rejection) of
    /// RotatedBasisVector1 relative to BasisVector1
    /// </summary>
    public abstract Float64Vector BasisVector2 { get; }

    public Float64PlanarAngle RotationAngle { get; }

    public double RotationAngleCos { get; }

    public double RotationAngleSin { get; }
        
    public IEnumerable<Float64Vector> BasisVectors
    {
        get
        {
            yield return BasisVector1;
            yield return BasisVector2;
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected LinFloat64PlanarRotation(Float64PlanarAngle rotationAngle)
    {
        RotationAngle = rotationAngle;
        RotationAngleCos = rotationAngle.Cos();
        RotationAngleSin = rotationAngle.Sin();
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
    public sealed override bool IsNearIdentity(double epsilon = 1e-12d)
    {
        return (RotationAngleCos - 1d).IsNearZero(epsilon);
    }
        

    public abstract Pair<double> BasisESp(int axisIndex);
        
    public abstract Pair<double> BasisESp(ILinSignedBasisVector axis);

    public abstract Pair<double> BasisESp(Float64Vector vector);

    public abstract Float64Vector MapBasisVector1();

    public abstract Float64Vector MapBasisVector2();
        
    public abstract Float64Vector MapBasisVector1(Float64PlanarAngle rotationAngle);

    public abstract Float64Vector MapBasisVector2(Float64PlanarAngle rotationAngle);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64Vector> MapBasisVector1(Float64PlanarAngle angle1, Float64PlanarAngle angle2)
    {
        return new Pair<Float64Vector>(
            MapBasisVector1(angle1),
            MapBasisVector1(angle2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64Vector> MapBasisVector2(Float64PlanarAngle angle1, Float64PlanarAngle angle2)
    {
        return new Pair<Float64Vector>(
            MapBasisVector2(angle1),
            MapBasisVector2(angle2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<Float64Vector> MapBasisVectors(Float64PlanarAngle rotationAngle)
    {
        return new Pair<Float64Vector>(
            MapBasisVector1(rotationAngle),
            MapBasisVector2(rotationAngle)
        );
    }


    public abstract Float64Vector GetVectorProjection(Float64Vector vector);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64PlanarAngle GetVectorProjectionPolarAngle(Float64Vector vector)
    {
        var (vpx, vpy) = BasisESp(vector);

        return Math.Atan2(vpy, vpx).RadiansToAngle().GetAngleInPositiveRange();
    }

    public abstract Float64Vector GetVectorRejection(Float64Vector vector);
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(Float64Vector vector, double epsilon = 1E-12D)
    {
        return GetVectorRejection(vector).IsNearZero(epsilon);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NearContains(ILinFloat64Subspace subspace, double epsilon = 1E-12)
    {
        return subspace.VSpaceDimensions <= VSpaceDimensions &&
               subspace.BasisVectors.All(v => NearContains(v, epsilon));
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector MapVectorProjection(Float64Vector vector)
    {
        var (vpx, vpy) = BasisESp(vector);
        
        // Compute the scalar factors of u1, u2
        var u1Scalar = RotationAngleCos * vpx - RotationAngleSin * vpy;
        var u2Scalar = RotationAngleCos * vpy + RotationAngleSin * vpx;

        return Float64VectorComposer
            .Create()
            .SetVector(BasisVector1, u1Scalar)
            .AddVector(BasisVector2, u2Scalar)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector MapVectorProjection(Float64Vector vector, Float64PlanarAngle rotationAngle)
    {
        var (vpx, vpy) = BasisESp(vector);

        var rotationAngleCos = rotationAngle.Cos();
        var rotationAngleSin = rotationAngle.Sin();

        // Compute the scalar factors of u1, u2
        var u1Scalar = rotationAngleCos * vpx - rotationAngleSin * vpy;
        var u2Scalar = rotationAngleCos * vpy + rotationAngleSin * vpx;

        return Float64VectorComposer
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
    public Float64Vector GetMiddleUnitVector1()
    {
        return MapBasisVector1(RotationAngle / 2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64Vector GetMiddleUnitVector2()
    {
        return MapBasisVector2(RotationAngle / 2);
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
            tValue.Lerp(basisRotation.RotationAngle)
        );

        var basisVector1 = basisRotation.MapVector(BasisVector1);
        var basisVector2 = basisRotation.MapVector(BasisVector2);
            
        var rotationAngle = tValue.Lerp(
            RotationAngle, 
            targetRotation.RotationAngle
        );

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
                    tValue.Lerp(basisRotationFullAngle)
                );

            var basisVector1 = basisRotation.MapVector(BasisVector1);
            var basisVector2 = basisRotation.MapVector(BasisVector2);
                
            var rotationAngle = tValue.Lerp(
                RotationAngle, 
                targetRotation.RotationAngle
            );

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
            tValue.Lerp(RotationAngle), 
            invertRotation
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<LinFloat64PlanarRotation> InterpolateRotationAngle(int count, bool isPeriodicRange = false, bool invertRotation = false)
    {
        var rotationAngleList =
            0d.GetLinearRange(RotationAngle.Degrees, count, isPeriodicRange)
                .Select(angle => angle.DegreesToAngle());

        return SetRotationAngle(rotationAngleList, invertRotation);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation SetRotationAngle(Float64PlanarAngle rotationAngle, bool invertRotation = false)
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
    public IEnumerable<LinFloat64PlanarRotation> SetRotationAngle(IEnumerable<Float64PlanarAngle> rotationAngleList, bool invertRotation = false)
    {
        return rotationAngleList.Select(angle => 
            SetRotationAngle(angle, invertRotation)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation RotateBasisVectors(Float64PlanarAngle rotationAngle)
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
    public LinFloat64PlanarRotation AlignBasisVector1(Float64Vector vector, bool assumeProjected = false)
    {
        var basisVector1 = 
            assumeProjected
                ? vector
                : GetVectorProjection(vector);

        if (vector.IsNearZero())
            return this;

        basisVector1 = basisVector1.ToUnitVector();

        var rotationAngle = BasisVector1.GetUnitVectorsAngle(basisVector1);

        return RotateBasisVectors(rotationAngle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PlanarRotation AlignBasisVector2(Float64Vector vector, bool assumeProjected = false)
    {
        var basisVector2 = 
            assumeProjected
                ? vector
                : GetVectorProjection(vector);

        if (vector.IsNearZero())
            return this;

        basisVector2 = basisVector2.ToUnitVector();

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