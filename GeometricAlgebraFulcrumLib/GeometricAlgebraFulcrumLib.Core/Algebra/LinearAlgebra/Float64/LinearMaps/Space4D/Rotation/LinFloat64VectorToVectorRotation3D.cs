using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public sealed class LinFloat64VectorToVectorRotation4D :
    LinFloat64VectorToVectorRotationBase4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation4D CreateIdentity()
    {
        var u = LinFloat64Vector4D.E1;

        return new LinFloat64VectorToVectorRotation4D(u, u);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation4D CreateIdentity(ILinFloat64Vector4D sourceVector)
    {
        return new LinFloat64VectorToVectorRotation4D(
            LinFloat64Vector4DUtils.ToLinVector4D(sourceVector),
            LinFloat64Vector4DUtils.ToLinVector4D(sourceVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation4D Create(ILinFloat64Vector4D sourceVector, ILinFloat64Vector4D targetVector)
    {
        return new LinFloat64VectorToVectorRotation4D(
            LinFloat64Vector4DUtils.ToLinVector4D(sourceVector),
            LinFloat64Vector4DUtils.ToLinVector4D(targetVector)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation4D Create(LinFloat64Vector4D sourceVector, LinFloat64Vector4D targetVector, LinFloat64Angle angle)
    {
        if (angle.RadiansValue.IsNearZero())
            return new LinFloat64VectorToVectorRotation4D(sourceVector, sourceVector);

        // Compute a rotated version of v in the u-v rotational plane by the given angle
        var vFinal = sourceVector.RotateToUnitLinVector4D(targetVector, angle);

        return new LinFloat64VectorToVectorRotation4D(sourceVector, vFinal);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation4D CreateFromComplexEigenPair(Complex eigenValue, MathNet.Numerics.LinearAlgebra.Vector<Complex> eigenVector)
    {
        var angle = eigenValue.GetPhaseAsPolarAngle();

        //TODO: Why is this the correct one, but not the reverse??!!
        var u = eigenVector.Imaginary().ToLinVector4D();
        var v = eigenVector.Real().ToLinVector4D();

        return Create(u, v, angle);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToVectorRotation4D CreateFromComplexEigenPair(double realValue, double imagValue, double[] realVector, double[] imagVector)
    {
        var angle = new Complex(realValue, imagValue).GetPhaseAsPolarAngle();

        //TODO: Why is this the correct one, but not the reverse??!!
        var u = imagVector.ToLinVector4D();
        var v = realVector.ToLinVector4D();

        return Create(u, v, angle);
    }


    public override LinFloat64Vector4D SourceVector { get; }

    public override LinFloat64Vector4D TargetOrthogonalVector { get; }

    public override LinFloat64Vector4D TargetVector { get; }

    public override double AngleCos { get; }

    public override LinFloat64PolarAngle Angle
        => AngleCos.ArcCos().RadiansToPolarAngle();


    internal LinFloat64VectorToVectorRotation4D(Triplet<double[]> rotationVectors)
    {
        var (sourceVector, targetOrthogonalVector, targetVector) =
            rotationVectors;

        Debug.Assert(
            sourceVector.Length == targetVector.Length &&
            sourceVector.GetVectorNormSquared().IsNearOne() &&
            targetVector.GetVectorNormSquared().IsNearOne()
        );

        SourceVector = sourceVector.ToLinVector4D();
        TargetVector = targetVector.ToLinVector4D();

        AngleCos = TargetVector.VectorESp(SourceVector).Clamp(-1d, 1d);

        Debug.Assert(
            !AngleCos.IsNearMinusOne()
        );

        TargetOrthogonalVector = targetOrthogonalVector.ToLinVector4D();

        Debug.Assert(
            (TargetOrthogonalVector - (TargetVector - AngleCos * SourceVector) / (1d + AngleCos)).VectorENormSquared().IsNearZero()
        );
    }

    private LinFloat64VectorToVectorRotation4D(LinFloat64Vector4D sourceVector, LinFloat64Vector4D targetVector)
    {
        Debug.Assert(
            sourceVector.IsNearUnit() &&
            targetVector.IsNearUnit()
        );

        SourceVector = sourceVector;
        TargetVector = targetVector;

        AngleCos = TargetVector.VectorESp(SourceVector).Clamp(-1d, 1d);

        Debug.Assert(
            !AngleCos.IsNearMinusOne()
        );

        TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return
            SourceVector.IsNearUnit() &&
            TargetVector.IsNearUnit() &&
            !AngleCos.IsNearMinusOne();
    }

    public override LinFloat64Vector4D ProjectOnRotationPlane(LinFloat64Vector4D vector)
    {
        var (xuDot, xvDot) = vector.VectorESp(SourceVector, TargetVector);
        var bivectorNormSquaredInv = 1d / (1d - AngleCos * AngleCos);

        var uScalar = (xuDot - xvDot * AngleCos) * bivectorNormSquaredInv;
        var vScalar = (xvDot - xuDot * AngleCos) * bivectorNormSquaredInv;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(SourceVector, uScalar)
            .AddVector(TargetVector, vScalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        //var r = vector.ESp(TargetOrthogonalVector);
        //var s = vector.ESp(SourceVector);

        //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

        var r = TargetOrthogonalVector[basisIndex];
        var s = SourceVector[basisIndex];
        var rsPlus = r + s;
        var rsMinus = r - s;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(SourceVector, -rsPlus)
            .AddVector(TargetVector, -rsMinus)
            .AddTerm(basisIndex, 1d)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        //var r = vector.ESp(TargetOrthogonalVector);
        //var s = vector.ESp(SourceVector);

        //return vector - (r + s) * SourceVector - (r - s) * TargetVector;

        var (r, s) = vector.VectorESp(TargetOrthogonalVector, SourceVector);
        var rsPlus = r + s;
        var rsMinus = r - s;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(vector)
            .AddVector(SourceVector, -rsPlus)
            .AddVector(TargetVector, -rsMinus)
            .GetVector();
    }

    public override LinFloat64Vector4D MapVectorProjection(LinFloat64Vector4D vector)
    {
        var (r, s) = vector.VectorESp(TargetOrthogonalVector, SourceVector);

        var uScalar = r / (AngleCos - 1d);
        var vScalar = s - uScalar * AngleCos;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(SourceVector, uScalar)
            .AddVector(TargetVector, vScalar)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64VectorToVectorRotation4D GetVectorToVectorRotationInverse()
    {
        return new LinFloat64VectorToVectorRotation4D(
            TargetVector,
            SourceVector
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
    {
        return GetVectorToVectorRotationInverse();
    }
}