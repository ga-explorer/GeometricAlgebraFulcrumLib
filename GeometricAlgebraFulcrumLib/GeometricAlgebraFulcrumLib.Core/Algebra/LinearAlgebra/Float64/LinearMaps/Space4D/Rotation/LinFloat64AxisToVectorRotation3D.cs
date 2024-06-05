using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public sealed class LinFloat64AxisToVectorRotation4D :
    LinFloat64VectorToVectorRotationBase4D
{
    public LinUnitBasisVector4D SourceAxis { get; }

    public override LinFloat64Vector4D SourceVector { get; }

    public override LinFloat64Vector4D TargetOrthogonalVector { get; }

    public override LinFloat64Vector4D TargetVector { get; }

    public override double AngleCos { get; }

    public override LinFloat64PolarAngle Angle
        => AngleCos.ArcCos().RadiansToPolarAngle();


    public LinFloat64AxisToVectorRotation4D(int uAxisIndex, bool uAxisNegative, ILinFloat64Vector4D v)
    {
        Debug.Assert(
            v.IsNearUnit()
        );

        SourceAxis = uAxisIndex.ToAxis4D(uAxisNegative);
        SourceVector = SourceAxis.ToLinVector4D();
        TargetVector = LinFloat64Vector4DUtils.ToLinVector4D(v);

        AngleCos = Float64Utils.Clamp(TargetVector.VectorESp(SourceAxis), -1d, 1d);

        Debug.Assert(
            !AngleCos.IsNearMinusOne()
        );

        TargetOrthogonalVector =
            LinFloat64Vector4DComposer
                .Create()
                .SetVector(TargetVector)
                .SubtractTerm(SourceAxis.GetIndex(), SourceAxis.IsNegative() ? -AngleCos : AngleCos)
                .Times(1d / (1d + AngleCos))
                .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return
            TargetVector.IsNearUnit() &&
            !AngleCos.IsNearMinusOne();
    }

    public override LinFloat64Vector4D ProjectOnRotationPlane(LinFloat64Vector4D vector)
    {
        var xuDot = vector.VectorESp(SourceAxis);
        var xvDot = vector.VectorESp(TargetVector);
        var bivectorNormSquaredInv = 1d / (1d - AngleCos * AngleCos);

        var uScalar = (xuDot - xvDot * AngleCos) * bivectorNormSquaredInv;
        var vScalar = (xvDot - xuDot * AngleCos) * bivectorNormSquaredInv;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(TargetVector, vScalar)
            .AddTerm(SourceAxis.GetIndex(), SourceAxis.IsNegative() ? -uScalar : uScalar)
            .GetVector();
    }

    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0 && basisIndex < VSpaceDimensions
        );

        var r = TargetOrthogonalVector[basisIndex];
        var s =
            basisIndex == SourceAxis.GetIndex()
                ? SourceAxis.GetSign().ToFloat64()
                : 0d;

        var rsPlus = r + s;
        var rsMinus = r - s;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(TargetVector, -rsMinus)
            .AddTerm(basisIndex, 1d)
            .SubtractTerm(SourceAxis.GetIndex(), SourceAxis.IsNegative() ? -rsPlus : rsPlus)
            .GetVector();
    }

    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        var r = vector.VectorESp(TargetOrthogonalVector);

        var s = SourceAxis.IsNegative()
            ? -vector.GetItem(SourceAxis.GetIndex())
            : vector.GetItem(SourceAxis.GetIndex());

        var rsPlus = r + s;
        var rsMinus = r - s;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(vector)
            .SubtractVector(TargetVector, rsMinus)
            .SubtractTerm(
                SourceAxis.GetIndex(),
                SourceAxis.IsNegative() ? -rsPlus : rsPlus
            ).GetVector();
    }

    public override LinFloat64Vector4D MapVectorProjection(LinFloat64Vector4D vector)
    {
        var r = vector.VectorESp(TargetOrthogonalVector);
        var s = SourceAxis.IsNegative() ? -vector[SourceAxis.GetIndex()] : vector[SourceAxis.GetIndex()];

        var uScalar = r / (AngleCos - 1);
        var vScalar = s - uScalar * AngleCos;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(TargetVector, vScalar)
            .AddTerm(SourceAxis.GetIndex(), SourceAxis.IsNegative() ? -uScalar : uScalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
    {
        return LinFloat64VectorToAxisRotation4D.Create(
            TargetVector,
            SourceAxis.GetIndex(),
            SourceAxis.IsNegative()
        );
    }
}