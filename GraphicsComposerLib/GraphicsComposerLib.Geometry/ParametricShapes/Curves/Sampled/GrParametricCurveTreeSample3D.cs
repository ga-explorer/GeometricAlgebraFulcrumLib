using System;
using System.Diagnostics.CodeAnalysis;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves.Sampled
{
    public sealed record GrParametricCurveTreeSample3D
    {
        public GrParametricCurveTreeLeaf3D LeafNode { get; }

        public double ParameterValue { get; }
        
        public double InterpolationValue { get; }

        public GrCurveFrameInterpolationMethod FrameInterpolationMethod 
            => LeafNode.ParentTree.FrameInterpolationMethod;


        internal GrParametricCurveTreeSample3D([NotNull] GrParametricCurveTreeLeaf3D leafNode, double parameterValue)
        {
            LeafNode = leafNode;
            
            ParameterValue = parameterValue;

            InterpolationValue = 
                (ParameterValue - leafNode.MinParameterValue) / 
                (leafNode.MaxParameterValue - leafNode.MinParameterValue);
        }


        public Tuple3D GetPoint()
        {
            if (ParameterValue == LeafNode.MinParameterValue)
                return LeafNode.Frame0.Point;

            if (ParameterValue == LeafNode.MaxParameterValue)
                return LeafNode.Frame1.Point;

            return InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);
        }

        public Tuple3D GetTangent()
        {
            if (ParameterValue == LeafNode.MinParameterValue)
                return LeafNode.Frame0.Tangent;

            if (ParameterValue == LeafNode.MaxParameterValue)
                return LeafNode.Frame1.Tangent;

            if (FrameInterpolationMethod == GrCurveFrameInterpolationMethod.TangentLinearInterpolation)
                return InterpolationValue.Lerp(
                    LeafNode.Frame0.Tangent, 
                    LeafNode.Frame1.Tangent
                ).ToUnitVector();

            var (axis, angle) = 
                VectorAlgebraUtils.GetRotationAxisAngle(
                    LeafNode.Frame0.Tangent, 
                    LeafNode.Frame1.Tangent
                );

            return SquareMatrix4
                .CreateRotationMatrix3D(axis, InterpolationValue * angle)
                .TimesVector(LeafNode.Frame0.Tangent);
        }

        public GrParametricCurveLocalFrame3D GetFrame()
        {
            if (ParameterValue == LeafNode.MinParameterValue)
                return LeafNode.Frame0;

            if (ParameterValue == LeafNode.MaxParameterValue)
                return LeafNode.Frame1;
            
            var point = 
                InterpolationValue.Lerp(LeafNode.Frame0.Point, LeafNode.Frame1.Point);

            Tuple3D normal1, normal2, tangent;

            if (FrameInterpolationMethod == GrCurveFrameInterpolationMethod.TangentLinearInterpolation)
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
                    VectorAlgebraUtils.GetRotationAxisAngle(
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

            return GrParametricCurveLocalFrame3D.CreateFrame(
                ParameterValue,
                point,
                normal1,
                normal2,
                tangent
            );
        }
    }
}