using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public sealed class LinFloat64VectorToAxisRotation4D :
    LinFloat64VectorToVectorRotationBase4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToAxisRotation4D Create(ILinFloat64Vector4D u, LinUnitBasisVector4D vAxis)
    {
        return new LinFloat64VectorToAxisRotation4D(
            LinFloat64Vector4DUtils.ToLinVector4D(u),
            vAxis.GetIndex(),
            vAxis.IsNegative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToAxisRotation4D Create(ILinFloat64Vector4D u, int vAxisIndex, bool vAxisNegative)
    {
        return new LinFloat64VectorToAxisRotation4D(
            LinFloat64Vector4DUtils.ToLinVector4D(u),
            vAxisIndex,
            vAxisNegative
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToAxisRotation4D CreateToPositiveAxis(ILinFloat64Vector4D u, int vAxisIndex)
    {
        return new LinFloat64VectorToAxisRotation4D(
            LinFloat64Vector4DUtils.ToLinVector4D(u),
            vAxisIndex,
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToAxisRotation4D CreateToNegativeAxis(ILinFloat64Vector4D u, int vAxisIndex)
    {
        return new LinFloat64VectorToAxisRotation4D(
            LinFloat64Vector4DUtils.ToLinVector4D(u),
            vAxisIndex,
            true
        );
    }


    public LinUnitBasisVector4D TargetAxis { get; }

    public override LinFloat64Vector4D SourceVector { get; }

    public override LinFloat64Vector4D TargetOrthogonalVector { get; }

    public override LinFloat64Vector4D TargetVector { get; }

    public override double AngleCos { get; }

    public override LinFloat64PolarAngle Angle
        => AngleCos.ArcCos().RadiansToPolarAngle();


    private LinFloat64VectorToAxisRotation4D(LinFloat64Vector4D sourceVector, int targetAxisIndex, bool targetAxisNegative)
    {
        Debug.Assert(
            sourceVector.IsNearUnit()
        );

        SourceVector = sourceVector;
        TargetAxis = targetAxisIndex.ToAxis4D(targetAxisNegative);
        TargetVector = TargetAxis.ToLinVector4D();

        AngleCos = Float64Utils.Clamp(SourceVector.VectorESp(TargetAxis), -1d, 1d);

        Debug.Assert(
            !AngleCos.IsNearMinusOne()
        );

        //TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);

        TargetOrthogonalVector =
            LinFloat64Vector4DComposer
                .Create()
                .SetVector(SourceVector, -AngleCos)
                .AddTerm(TargetAxis.GetIndex(), TargetAxis.GetSign().ToFloat64())
                .Times(1d / (1d + AngleCos))
                .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return
            SourceVector.IsNearUnit() &&
            !AngleCos.IsNearMinusOne();
    }

    public override LinFloat64Vector4D ProjectOnRotationPlane(LinFloat64Vector4D vector)
    {
        var xuDot = vector.VectorESp(SourceVector);
        var xvDot = vector.VectorESp(TargetAxis);
        var bivectorNormSquaredInv = 1d / (1d - AngleCos * AngleCos);

        var uScalar = (xuDot - xvDot * AngleCos) * bivectorNormSquaredInv;
        var vScalar = (xvDot - xuDot * AngleCos) * bivectorNormSquaredInv;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(SourceVector, uScalar)
            .AddTerm(TargetAxis.GetIndex(), TargetAxis.IsNegative() ? -vScalar : vScalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        var r = TargetOrthogonalVector[basisIndex];
        var s = SourceVector[basisIndex];
        var rsPlus = r + s;
        var rsMinus = r - s;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(SourceVector, -rsPlus)
            .AddTerm(basisIndex, 1d)
            .SubtractTerm(TargetAxis.GetIndex(), TargetAxis.IsNegative() ? -rsMinus : rsMinus)
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
            .SubtractTerm(TargetAxis.GetIndex(), TargetAxis.IsNegative() ? -rsMinus : rsMinus)
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
            .AddTerm(TargetAxis.GetIndex(), TargetAxis.IsNegative() ? -vScalar : vScalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
    {
        return new LinFloat64AxisToVectorRotation4D(
            TargetAxis.GetIndex(),
            TargetAxis.IsNegative(),
            SourceVector
        );
    }
}