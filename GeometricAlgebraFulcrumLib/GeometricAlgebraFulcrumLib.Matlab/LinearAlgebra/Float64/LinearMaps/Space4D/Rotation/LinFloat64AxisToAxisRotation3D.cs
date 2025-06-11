using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.LinearMaps.Space4D.Rotation;

public sealed class LinFloat64AxisToAxisRotation4D :
    LinFloat64VectorToVectorRotationBase4D
{
    public LinBasisVector SourceAxis { get; }

    public LinBasisVector TargetAxis { get; }

    public override LinFloat64Vector4D SourceVector { get; }

    public override LinFloat64Vector4D TargetOrthogonalVector
        => TargetVector;

    public override LinFloat64Vector4D TargetVector { get; }

    public override double AngleCos
        => 0;

    public override LinFloat64PolarAngle Angle
        => LinFloat64PolarAngle.Angle90;


    
    public LinFloat64AxisToAxisRotation4D(int uAxisIndex, bool uAxisNegative, int vAxisIndex, bool vAxisNegative)
    {
        Debug.Assert(
            uAxisIndex != vAxisIndex
        );

        SourceAxis = uAxisIndex.ToAxis4D(uAxisNegative);
        TargetAxis = vAxisIndex.ToAxis4D(vAxisNegative);

        SourceVector = SourceAxis.ToLinVector4D();
        TargetVector = TargetAxis.ToLinVector4D();
    }


    
    public override bool IsValid()
    {
        return SourceAxis.Index != TargetAxis.Index;
    }

    
    public override bool IsIdentity()
    {
        return SourceAxis.Index == TargetAxis.Index &&
               SourceAxis.IsNegative == TargetAxis.IsNegative;
    }

    
    public override bool IsNearIdentity(double zeroEpsilon = 1e-12d)
    {
        return SourceAxis.Index == TargetAxis.Index &&
               SourceAxis.IsNegative == TargetAxis.IsNegative;
    }

    
    public override LinFloat64Vector4D ProjectOnRotationPlane(LinFloat64Vector4D vector)
    {
        return LinFloat64Vector4DComposer
            .Create()
            .SetTerm(SourceAxis.Index, vector[SourceAxis.Index])
            .SetTerm(TargetAxis.Index, vector[TargetAxis.Index])
            .GetVector();
    }

    
    public override LinFloat64Vector4D MapBasisVector(int basisIndex)
    {
        var s = basisIndex == SourceAxis.Index ? SourceAxis.IsNegative ? -1d : 1d : 0d;
        var r = basisIndex == TargetAxis.Index ? TargetAxis.IsNegative ? -1d : 1d : 0d;
        var rsPlus = r + s;
        var rsMinus = r - s;

        return LinFloat64Vector4DComposer
            .Create()
            .SetTerm(basisIndex, 1d)
            .SubtractTerm(
                SourceAxis.Index,
                SourceAxis.IsNegative ? -rsPlus : rsPlus
            ).SubtractTerm(
                TargetAxis.Index,
                TargetAxis.IsNegative ? -rsMinus : rsMinus
            ).GetVector();
    }

    
    public override LinFloat64Vector4D MapVector(ILinFloat64Vector4D vector)
    {
        var r = TargetAxis.IsNegative
            ? -vector.GetItem(TargetAxis.Index)
            : vector.GetItem(TargetAxis.Index);

        var s = SourceAxis.IsNegative
            ? -vector.GetItem(SourceAxis.Index)
            : vector.GetItem(SourceAxis.Index);

        var rsPlus = r + s;
        var rsMinus = r - s;

        return LinFloat64Vector4DComposer
            .Create()
            .SetVector(vector)
            .SubtractTerm(
                SourceAxis.Index,
                SourceAxis.IsNegative ? -rsPlus : rsPlus
            ).SubtractTerm(
                TargetAxis.Index,
                TargetAxis.IsNegative ? -rsMinus : rsMinus
            ).GetVector();
    }

    
    public override LinFloat64Vector4D MapVectorProjection(LinFloat64Vector4D vector)
    {
        var r = TargetAxis.IsNegative
            ? -vector[TargetAxis.Index]
            : vector[TargetAxis.Index];

        var s = SourceAxis.IsNegative
            ? -vector[SourceAxis.Index]
            : vector[SourceAxis.Index];

        return LinFloat64Vector4DComposer
            .Create()
            .SetTerm(
                SourceAxis.Index,
                SourceAxis.IsNegative ? r : -r
            ).SetTerm(
                TargetAxis.Index,
                TargetAxis.IsNegative ? -s : s
            ).GetVector();
    }

    
    public override LinFloat64SimpleRotationBase4D GetSimpleVectorRotationInverse()
    {
        return new LinFloat64AxisToAxisRotation4D(
            TargetAxis.Index,
            TargetAxis.IsNegative,
            SourceAxis.Index,
            SourceAxis.IsNegative
        );
    }
}