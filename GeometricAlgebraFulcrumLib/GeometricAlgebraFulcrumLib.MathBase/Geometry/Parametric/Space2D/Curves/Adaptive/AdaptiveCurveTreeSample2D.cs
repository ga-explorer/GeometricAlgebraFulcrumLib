using GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Frames;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.Parametric.Space2D.Curves.Adaptive
{
    public sealed record AdaptiveCurveTreeSample2D
    {
        public AdaptiveCurveTreeLeaf2D LeafNode { get; }

        public int LeafNodeIndex
            => LeafNode.LeafListIndex;

        public double ParameterValue { get; }

        public double InterpolationValue { get; }

        public ParametricCurveLocalFrameInterpolationMethod FrameInterpolationMethod
            => LeafNode.ParentTree.FrameInterpolationMethod;


        internal AdaptiveCurveTreeSample2D(AdaptiveCurveTreeLeaf2D leafNode, double parameterValue)
        {
            LeafNode = leafNode;

            ParameterValue = parameterValue;

            InterpolationValue =
                (ParameterValue - leafNode.MinParameterValue) /
                (leafNode.MaxParameterValue - leafNode.MinParameterValue);
        }


        public Float64Vector2D GetPoint()
        {
            if (ParameterValue == LeafNode.MinParameterValue)
                return LeafNode.Frame0.Point;

            if (ParameterValue == LeafNode.MaxParameterValue)
                return LeafNode.Frame1.Point;

            return InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);
        }

        public Float64Vector2D GetTangent()
        {
            if (ParameterValue == LeafNode.MinParameterValue)
                return LeafNode.Frame0.Tangent;

            if (ParameterValue == LeafNode.MaxParameterValue)
                return LeafNode.Frame1.Tangent;

            if (FrameInterpolationMethod == ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation)
                return InterpolationValue.Lerp(
                    LeafNode.Frame0.Tangent,
                    LeafNode.Frame1.Tangent
                ).ToUnitVector();

            var angle =
                LeafNode.Frame0.Tangent.GetVectorsAngle(
                    LeafNode.Frame1.Tangent
                );

            return SquareMatrix3
                .CreateRotationMatrix2D(InterpolationValue * angle)
                .MapAffineVector(LeafNode.Frame0.Tangent);
        }

        public ParametricCurveLocalFrame2D GetFrame()
        {
            if (ParameterValue == LeafNode.MinParameterValue)
                return LeafNode.Frame0;

            if (ParameterValue == LeafNode.MaxParameterValue)
                return LeafNode.Frame1;

            var point =
                InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);

            Float64Vector2D tangent;

            if (FrameInterpolationMethod == ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation)
            {
                // Use linear interpolation to find the new tangent
                tangent = InterpolationValue.Lerp(LeafNode.Frame0.Tangent, LeafNode.Frame1.Tangent).ToUnitVector();
            }
            else
            {
                // Use spherical linear interpolation on the whole frame
                var angle =
                    LeafNode.Frame0.Tangent.GetVectorsAngle(
                        LeafNode.Frame1.Tangent
                    );

                tangent =
                    SquareMatrix3
                        .CreateRotationMatrix2D(InterpolationValue * angle)
                        .MapAffineVector(
                            LeafNode.Frame0.Tangent
                        );
            }

            return ParametricCurveLocalFrame2D.Create(
                ParameterValue,
                point,
                tangent
            );
        }
    }
}