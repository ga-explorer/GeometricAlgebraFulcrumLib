using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;


// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Adaptive;

public sealed record Float64AdaptivePath3DSample
{
    public Float64AdaptivePath3DLeaf LeafNode { get; }

    public int LeafNodeIndex
        => LeafNode.LeafListIndex;

    public double ParameterValue { get; }

    public double InterpolationValue { get; }

    public ParametricCurveLocalFrameInterpolationMethod FrameInterpolationMethod
        => LeafNode.ParentTree.FrameInterpolationMethod;


    internal Float64AdaptivePath3DSample(Float64AdaptivePath3DLeaf leafNode, double parameterValue)
    {
        LeafNode = leafNode;

        ParameterValue = parameterValue;

        InterpolationValue =
            (ParameterValue - leafNode.MinParameterValue) /
            (leafNode.MaxParameterValue - leafNode.MinParameterValue);
    }


    public LinFloat64Vector3D GetPoint()
    {
        if (ParameterValue == LeafNode.MinParameterValue)
            return LeafNode.Frame0.Point;

        if (ParameterValue == LeafNode.MaxParameterValue)
            return LeafNode.Frame1.Point;

        return InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);
    }

    public LinFloat64Vector3D GetTangent()
    {
        if (ParameterValue == LeafNode.MinParameterValue)
            return LeafNode.Frame0.Tangent;

        if (ParameterValue == LeafNode.MaxParameterValue)
            return LeafNode.Frame1.Tangent;

        if (FrameInterpolationMethod == ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation)
            return InterpolationValue.Lerp(
                LeafNode.Frame0.Tangent,
                LeafNode.Frame1.Tangent
            ).ToUnitLinVector3D();

        var (axis, angle) =
            LeafNode.Frame0.Tangent.VectorToVectorRotationAxisAngle(
                LeafNode.Frame1.Tangent
            );

        return SquareMatrix4
            .CreateRotationMatrix3D(axis, angle.AngleTimes(InterpolationValue))
            .MapAffineVector(LeafNode.Frame0.Tangent);
    }

    public Float64Path3DLocalFrame GetFrame()
    {
        if (ParameterValue == LeafNode.MinParameterValue)
            return LeafNode.Frame0;

        if (ParameterValue == LeafNode.MaxParameterValue)
            return LeafNode.Frame1;

        var point =
            InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);

        LinFloat64Vector3D normal1, normal2, tangent;

        if (FrameInterpolationMethod == ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation)
        {
            // Use linear interpolation to find the new tangent
            tangent = InterpolationValue.Lerp(LeafNode.Frame0.Tangent, LeafNode.Frame1.Tangent).ToUnitLinVector3D();

            // Use simple rotation to rotate the normals
            (normal1, normal2) =
                SquareMatrix4
                    .CreateRotationMatrix3D(LeafNode.Frame0.Tangent, tangent)
                    .MapAffineVectors(
                        LeafNode.Frame0.Normal1,
                        LeafNode.Frame0.Normal2
                    );
        }
        else
        {
            // Use spherical linear interpolation on the whole frame
            var (axis, angle) =
                LeafNode.Frame0.Tangent.VectorToVectorRotationAxisAngle(
                    LeafNode.Frame1.Tangent
                );

            (normal1, normal2, tangent) =
                SquareMatrix4
                    .CreateRotationMatrix3D(axis, angle.AngleTimes(InterpolationValue))
                    .MapAffineVectors(
                        LeafNode.Frame0.Normal1,
                        LeafNode.Frame0.Normal2,
                        LeafNode.Frame0.Tangent
                    );
        }

        return Float64Path3DLocalFrame.Create(
            ParameterValue,
            point,
            tangent,
            normal1,
            normal2
        );
    }
}