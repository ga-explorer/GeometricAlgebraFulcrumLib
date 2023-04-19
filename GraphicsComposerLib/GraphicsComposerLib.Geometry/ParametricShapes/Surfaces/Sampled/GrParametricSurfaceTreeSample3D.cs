using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GraphicsComposerLib.Geometry.ParametricShapes.Surfaces.Sampled
{
    public sealed record GrParametricSurfaceTreeSample3D
    {
        public GrParametricSurfaceTreeLeaf3D LeafNode { get; }

        public double ParameterValue1 { get; }

        public double ParameterValue2 { get; }
        
        public double InterpolationValue1 { get; }

        public double InterpolationValue2 { get; }


        internal GrParametricSurfaceTreeSample3D([NotNull] GrParametricSurfaceTreeLeaf3D leafNode, double parameterValue1, double parameterValue2)
        {
            LeafNode = leafNode;
            
            ParameterValue1 = parameterValue1;
            ParameterValue2 = parameterValue2;

            InterpolationValue1 = 
                (ParameterValue1 - leafNode.MinParameterValue1) / 
                (leafNode.MaxParameterValue1 - leafNode.MinParameterValue1);

            InterpolationValue2 = 
                (ParameterValue2 - leafNode.MinParameterValue2) / 
                (leafNode.MaxParameterValue2 - leafNode.MinParameterValue2);

            Debug.Assert(InterpolationValue1 is >= 0d and <= 1d);
            Debug.Assert(InterpolationValue2 is >= 0d and <= 1d);
        }


        public Float64Tuple3D GetPoint()
        {
            var t10 = 1d - InterpolationValue1;
            var t11 = InterpolationValue1;

            var t20 = 1 - InterpolationValue2;
            var t21 = InterpolationValue2;

            var s00 = t10 * t20;
            var s01 = t10 * t21;
            var s10 = t11 * t20;
            var s11 = t11 * t21;
            
            return s00 * LeafNode.Frame00.Point +
                   s01 * LeafNode.Frame01.Point +
                   s10 * LeafNode.Frame10.Point +
                   s11 * LeafNode.Frame11.Point;
        }

        public Float64Tuple3D GetNormal()
        {
            var t10 = 1d - InterpolationValue1;
            var t11 = InterpolationValue1;

            var t20 = 1 - InterpolationValue2;
            var t21 = InterpolationValue2;

            var s00 = t10 * t20;
            var s01 = t10 * t21;
            var s10 = t11 * t20;
            var s11 = t11 * t21;
            
            return s00 * LeafNode.Frame00.Normal.ToTuple3D() +
                   s01 * LeafNode.Frame01.Normal.ToTuple3D() +
                   s10 * LeafNode.Frame10.Normal.ToTuple3D() +
                   s11 * LeafNode.Frame11.Normal.ToTuple3D();
        }

        public GrParametricSurfaceLocalFrame3D GetFrame()
        {
            var t10 = 1d - InterpolationValue1;
            var t11 = InterpolationValue1;

            var t20 = 1 - InterpolationValue2;
            var t21 = InterpolationValue2;

            var s00 = t10 * t20;
            var s01 = t10 * t21;
            var s10 = t11 * t20;
            var s11 = t11 * t21;
            
            var point = 
                s00 * LeafNode.Frame00.Point +
                s01 * LeafNode.Frame01.Point +
                s10 * LeafNode.Frame10.Point +
                s11 * LeafNode.Frame11.Point;
            
            var normal = 
                s00 * LeafNode.Frame00.Normal.ToTuple3D() +
                s01 * LeafNode.Frame01.Normal.ToTuple3D() +
                s10 * LeafNode.Frame10.Normal.ToTuple3D() +
                s11 * LeafNode.Frame11.Normal.ToTuple3D();

            return new GrParametricSurfaceLocalFrame3D(
                ParameterValue1,
                ParameterValue2,
                point,
                normal
            );
        }
    }
}