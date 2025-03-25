using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64;


// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Adaptive;

public sealed record Float64AdaptivePath2DSample
{
    public Float64AdaptivePath2DLeaf LeafNode { get; }

    public int LeafNodeIndex
        => LeafNode.LeafListIndex;

    public double ParameterValue { get; }

    public double InterpolationValue { get; }

    public ParametricCurveLocalFrameInterpolationMethod FrameInterpolationMethod
        => LeafNode.ParentTree.FrameInterpolationMethod;


    internal Float64AdaptivePath2DSample(Float64AdaptivePath2DLeaf leafNode, double t)
    {
        LeafNode = leafNode;

        ParameterValue = t;

        InterpolationValue =
            (ParameterValue - leafNode.MinParameterValue) /
            (leafNode.MaxParameterValue - leafNode.MinParameterValue);
    }


    public LinFloat64Vector2D GetPoint()
    {
        if (ParameterValue == LeafNode.MinParameterValue)
            return LeafNode.Frame0.Point;

        if (ParameterValue == LeafNode.MaxParameterValue)
            return LeafNode.Frame1.Point;

        return InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);
    }

    public LinFloat64Vector2D GetTangent()
    {
        if (ParameterValue == LeafNode.MinParameterValue)
            return LeafNode.Frame0.Tangent;

        if (ParameterValue == LeafNode.MaxParameterValue)
            return LeafNode.Frame1.Tangent;

        if (FrameInterpolationMethod == ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation)
            return InterpolationValue.Lerp(
                LeafNode.Frame0.Tangent,
                LeafNode.Frame1.Tangent
            ).ToUnitLinVector2D();

        var angle =
            LeafNode.Frame0.Tangent.GetAngle(
                LeafNode.Frame1.Tangent
            );

        return SquareMatrix3
            .CreateRotationMatrix2D(angle.AngleTimes(InterpolationValue))
            .MapAffineVector(LeafNode.Frame0.Tangent);
    }

    public Float64Path2DLocalFrame GetFrame()
    {
        if (ParameterValue == LeafNode.MinParameterValue)
            return LeafNode.Frame0;

        if (ParameterValue == LeafNode.MaxParameterValue)
            return LeafNode.Frame1;

        var point =
            InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);

        LinFloat64Vector2D tangent;

        if (FrameInterpolationMethod == ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation)
        {
            // Use linear interpolation to find the new tangent
            tangent = InterpolationValue.Lerp(LeafNode.Frame0.Tangent, LeafNode.Frame1.Tangent).ToUnitLinVector2D();
        }
        else
        {
            // Use spherical linear interpolation on the whole frame
            var angle =
                LeafNode.Frame0.Tangent.GetAngle(
                    LeafNode.Frame1.Tangent
                );

            tangent =
                SquareMatrix3
                    .CreateRotationMatrix2D(angle.AngleTimes(InterpolationValue))
                    .MapAffineVector(
                        LeafNode.Frame0.Tangent
                    );
        }

        return Float64Path2DLocalFrame.Create(
            ParameterValue,
            point,
            tangent
        );
    }
}