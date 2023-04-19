using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Parametric.Frames;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.MathBase.Parametric.Curves.Sampled
{
    public sealed record SampledParametricCurveTreeSample3D
    {
        public SampledParametricCurveTreeLeaf3D LeafNode { get; }

        public int LeafNodeIndex
            => LeafNode.LeafListIndex;

        public double ParameterValue { get; }

        public double InterpolationValue { get; }

        public ParametricCurveLocalFrameInterpolationMethod FrameInterpolationMethod
            => LeafNode.ParentTree.FrameInterpolationMethod;


        internal SampledParametricCurveTreeSample3D(SampledParametricCurveTreeLeaf3D leafNode, double parameterValue)
        {
            LeafNode = leafNode;

            ParameterValue = parameterValue;

            InterpolationValue =
                (ParameterValue - leafNode.MinParameterValue) /
                (leafNode.MaxParameterValue - leafNode.MinParameterValue);
        }


        public Float64Tuple3D GetPoint()
        {
            if (ParameterValue == LeafNode.MinParameterValue)
                return LeafNode.Frame0.Point;

            if (ParameterValue == LeafNode.MaxParameterValue)
                return LeafNode.Frame1.Point;

            return InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);
        }

        public Float64Tuple3D GetTangent()
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

            var (axis, angle) =
                QuaternionUtils.GetRotationAxisAngle(
                    LeafNode.Frame0.Tangent,
                    LeafNode.Frame1.Tangent
                );

            return SquareMatrix4
                .CreateRotationMatrix3D(axis, InterpolationValue * angle)
                .TimesVector(LeafNode.Frame0.Tangent);
        }

        public ParametricCurveLocalFrame3D GetFrame()
        {
            if (ParameterValue == LeafNode.MinParameterValue)
                return LeafNode.Frame0;

            if (ParameterValue == LeafNode.MaxParameterValue)
                return LeafNode.Frame1;

            var point =
                InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);

            Float64Tuple3D normal1, normal2, tangent;

            if (FrameInterpolationMethod == ParametricCurveLocalFrameInterpolationMethod.TangentLinearInterpolation)
            {
                // Use linear interpolation to find the new tangent
                tangent = InterpolationValue.Lerp(LeafNode.Frame0.Tangent, LeafNode.Frame1.Tangent).ToUnitVector();

                // Use simple rotation to rotate the normals
                (normal1, normal2) =
                    SquareMatrix4
                        .CreateRotationMatrix3D(LeafNode.Frame0.Tangent, tangent)
                        .TimesVectors(
                            LeafNode.Frame0.Normal1,
                            LeafNode.Frame0.Normal2
                        );
            }
            else
            {
                // Use spherical linear interpolation on the whole frame
                var (axis, angle) =
                    QuaternionUtils.GetRotationAxisAngle(
                        LeafNode.Frame0.Tangent,
                        LeafNode.Frame1.Tangent
                    );

                (normal1, normal2, tangent) =
                    SquareMatrix4
                        .CreateRotationMatrix3D(axis, InterpolationValue * angle)
                        .TimesVectors(
                            LeafNode.Frame0.Normal1,
                            LeafNode.Frame0.Normal2,
                            LeafNode.Frame0.Tangent
                        );
            }

            return ParametricCurveLocalFrame3D.Create(
                ParameterValue,
                point,
                normal1,
                normal2,
                tangent
            );
        }
    }
}