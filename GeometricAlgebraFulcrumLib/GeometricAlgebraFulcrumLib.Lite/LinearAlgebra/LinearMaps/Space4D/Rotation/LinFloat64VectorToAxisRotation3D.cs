using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.Space4D.Rotation;

public sealed class LinFloat64VectorToAxisRotation4D :
    LinFloat64VectorToVectorRotationBase4D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToAxisRotation4D Create(IFloat64Vector4D u, LinUnitBasisVector4D vAxis)
    {
        return new LinFloat64VectorToAxisRotation4D(
            u.ToTuple4D(),
            vAxis.GetIndex(),
            vAxis.IsNegative()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToAxisRotation4D Create(IFloat64Vector4D u, int vAxisIndex, bool vAxisNegative)
    {
        return new LinFloat64VectorToAxisRotation4D(
            u.ToTuple4D(),
            vAxisIndex,
            vAxisNegative
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToAxisRotation4D CreateToPositiveAxis(IFloat64Vector4D u, int vAxisIndex)
    {
        return new LinFloat64VectorToAxisRotation4D(
            u.ToTuple4D(),
            vAxisIndex,
            false
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64VectorToAxisRotation4D CreateToNegativeAxis(IFloat64Vector4D u, int vAxisIndex)
    {
        return new LinFloat64VectorToAxisRotation4D(
            u.ToTuple4D(),
            vAxisIndex,
            true
        );
    }


    public LinUnitBasisVector4D TargetAxis { get; }

    public override Float64Vector4D SourceVector { get; }

    public override Float64Vector4D TargetOrthogonalVector { get; }

    public override Float64Vector4D TargetVector { get; }

    public override double AngleCos { get; }

    public override Float64PlanarAngle Angle
        => AngleCos.ArcCos();


    private LinFloat64VectorToAxisRotation4D(Float64Vector4D sourceVector, int targetAxisIndex, bool targetAxisNegative)
    {
        Debug.Assert(
            sourceVector.IsNearUnit()
        );

        SourceVector = sourceVector;
        TargetAxis = targetAxisIndex.ToAxis4D(targetAxisNegative);
        TargetVector = TargetAxis.ToTuple4D();

        AngleCos = Float64Utils.Clamp(SourceVector.ESp(TargetAxis), -1d, 1d);

        Debug.Assert(
            !AngleCos.IsNearMinusOne()
        );

        //TargetOrthogonalVector = (TargetVector - AngleCos * SourceVector) / (1d + AngleCos);

        TargetOrthogonalVector =
            Float64Vector4DComposer
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
    
    public override Float64Vector4D ProjectOnRotationPlane(Float64Vector4D vector)
    {
        var xuDot = vector.ESp(SourceVector);
        var xvDot = vector.ESp(TargetAxis);
        var bivectorNormSquaredInv = 1d / (1d - AngleCos * AngleCos);

        var uScalar = (xuDot - xvDot * AngleCos) * bivectorNormSquaredInv;
        var vScalar = (xvDot - xuDot * AngleCos) * bivectorNormSquaredInv;

        return Float64Vector4DComposer
            .Create()
            .SetVector(SourceVector, uScalar)
            .AddTerm(TargetAxis.GetIndex(), TargetAxis.IsNegative() ? -vScalar : vScalar)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector4D MapBasisVector(int basisIndex)
    {
        Debug.Assert(
            basisIndex >= 0
        );

        var r = TargetOrthogonalVector[basisIndex];
        var s = SourceVector[basisIndex];
        var rsPlus = r + s;
        var rsMinus = r - s;

        return Float64Vector4DComposer
            .Create()
            .SetVector(SourceVector, -rsPlus)
            .AddTerm(basisIndex, 1d)
            .SubtractTerm(TargetAxis.GetIndex(), TargetAxis.IsNegative() ? -rsMinus : rsMinus)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Vector4D MapVector(IFloat64Vector4D vector)
    {
        //var r = vector.ESp(TargetOrthogonalVector);
        //var s = vector.ESp(SourceVector);

        //return vector - (r + s) * SourceVector - (r - s) * TargetVector;
            
        var (r, s) = vector.ESp(TargetOrthogonalVector, SourceVector);
        var rsPlus = r + s;
        var rsMinus = r - s;
        
        return Float64Vector4DComposer
            .Create()
            .SetVector(vector)
            .AddVector(SourceVector, -rsPlus)
            .SubtractTerm(TargetAxis.GetIndex(), TargetAxis.IsNegative() ? -rsMinus : rsMinus)
            .GetVector();
    }
    
    public override Float64Vector4D MapVectorProjection(Float64Vector4D vector)
    {
        var (r, s) = vector.ESp(TargetOrthogonalVector, SourceVector);
            
        var uScalar = r / (AngleCos - 1d);
        var vScalar = s - uScalar * AngleCos;

        return Float64Vector4DComposer
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