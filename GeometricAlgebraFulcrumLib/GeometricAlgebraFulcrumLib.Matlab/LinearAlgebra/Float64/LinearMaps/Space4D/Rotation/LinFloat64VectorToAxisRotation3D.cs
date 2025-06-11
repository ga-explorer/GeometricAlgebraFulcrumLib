using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public sealed class LinFloat64VectorToAxisRotation4D :
    LinFloat64VectorToVectorRotationBase4D
{
    
    public static LinFloat64VectorToAxisRotation4D Create(ILinFloat64Vector4D u, LinBasisVector vAxis)
    {
        return new LinFloat64VectorToAxisRotation4D(
            LinFloat64Vector4DUtils.ToLinVector4D(u),
            vAxis.Index,
            vAxis.IsNegative
        );
    }

    
    public static LinFloat64VectorToAxisRotation4D Create(ILinFloat64Vector4D u, int vAxisIndex, bool vAxisNegative)
    {
        return new LinFloat64VectorToAxisRotation4D(
            LinFloat64Vector4DUtils.ToLinVector4D(u),
            vAxisIndex,
            vAxisNegative
        );
    }

    
    public static LinFloat64VectorToAxisRotation4D CreateToPositiveAxis(ILinFloat64Vector4D u, int vAxisIndex)
    {
        return new LinFloat64VectorToAxisRotation4D(
            LinFloat64Vector4DUtils.ToLinVector4D(u),
            vAxisIndex,
            false
        );
    }

    
    public static LinFloat64VectorToAxisRotation4D CreateToNegativeAxis(ILinFloat64Vector4D u, int vAxisIndex)
    {
        return new LinFloat64VectorToAxisRotation4D(
            LinFloat64Vector4DUtils.ToLinVector4D(u),
            vAxisIndex,
            true
        );
    }


    public LinBasisVector TargetAxis { get; }

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
                .AddTerm(TargetAxis.Index, TargetAxis.Sign.ToFloat64())
                .Times(1d / (1d + AngleCos))
                .GetVector();
    }


    
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
            .AddTerm(TargetAxis.Index, TargetAxis.IsNegative ? -vScalar : vScalar)
            .GetVector();
    }

    
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
            .SubtractTerm(TargetAxis.Index, TargetAxis.IsNegative ? -rsMinus : rsMinus)
            .GetVector();
    }

    
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
            .SubtractTerm(TargetAxis.Index, TargetAxis.IsNegative ? -rsMinus : rsMinus)
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
            .AddTerm(TargetAxis.Index, TargetAxis.IsNegative ? -vScalar : vScalar)
            .GetVector();
    }

    
    public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
    {
        return new LinFloat64AxisToVectorRotation4D(
            TargetAxis.Index,
            TargetAxis.IsNegative,
            SourceVector
        );
    }
}