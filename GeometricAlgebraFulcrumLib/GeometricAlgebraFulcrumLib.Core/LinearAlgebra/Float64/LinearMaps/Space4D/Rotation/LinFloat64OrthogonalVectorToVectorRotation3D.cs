using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space4D;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public sealed class LinFloat64OrthogonalVectorToVectorRotation4D :
    LinFloat64VectorToVectorRotationBase4D
{
    public override LinFloat64Vector4D SourceVector { get; }

    public override LinFloat64Vector4D TargetOrthogonalVector
        => TargetVector;

    public override LinFloat64Vector4D TargetVector { get; }

    public override double AngleCos
        => 0d;

    public override LinFloat64PolarAngle Angle
        => LinFloat64PolarAngle.Angle90;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64OrthogonalVectorToVectorRotation4D(ILinFloat64Vector4D sourceVector, ILinFloat64Vector4D targetVector)
    {
        Debug.Assert(
            sourceVector.IsNearUnit() &&
            targetVector.IsNearUnit() &&
            targetVector.IsNearOrthogonalTo(sourceVector)
        );

        SourceVector = LinFloat64Vector4DUtils.ToLinVector4D(sourceVector);
        TargetVector = LinFloat64Vector4DUtils.ToLinVector4D(targetVector);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return
            SourceVector.IsNearUnit() &&
            TargetVector.IsNearUnit() &&
            TargetVector.IsNearOrthogonalTo(SourceVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsIdentity()
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsNearIdentity(double zeroEpsilon = 1e-12d)
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D ProjectOnRotationPlane(LinFloat64Vector4D vector)
    {
        var (xuDot, xvDot) =
            vector.VectorESp(SourceVector, TargetVector);

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(SourceVector, xuDot)
            .AddVector(TargetVector, xvDot)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        var r = TargetVector[basisIndex];
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
        var (r, s) = vector.VectorESp(TargetVector, SourceVector);
        var rsPlus = r + s;
        var rsMinus = r - s;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(vector)
            .AddVector(SourceVector, -rsPlus)
            .AddVector(TargetVector, -rsMinus)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapVectorProjection(LinFloat64Vector4D vector)
    {
        var (r, s) = vector.VectorESp(TargetOrthogonalVector, SourceVector);

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(TargetVector, s)
            .SubtractVector(SourceVector, r)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
    {
        return new LinFloat64OrthogonalVectorToVectorRotation4D(
            TargetVector,
            SourceVector
        );
    }
}