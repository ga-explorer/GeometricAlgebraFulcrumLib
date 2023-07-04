using System.Diagnostics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Basic
{
    public sealed class GrVisualRightAngle3D :
        GrVisualCurveWithAnimation3D
    {
        public static GrVisualRightAngle3D CreateStatic(string name, GrVisualCurveStyle3D style, IFloat64Vector3D origin, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius)
        {
            return new GrVisualRightAngle3D(
                name,
                style,
                origin,
                direction1,
                direction2,
                radius,
                GrVisualAnimationSpecs.Static
            );
        }

        public static GrVisualRightAngle3D CreateStatic(string name, GrVisualCurveStyle3D style, GrVisualSurfaceThinStyle3D innerStyle, IFloat64Vector3D origin, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius)
        {
            return new GrVisualRightAngle3D(
                name,
                style,
                origin,
                direction1,
                direction2,
                radius,
                GrVisualAnimationSpecs.Static
            )
            {
                InnerStyle = innerStyle
            };
        }

        public static GrVisualRightAngle3D Create(string name, GrVisualCurveStyle3D style, IFloat64Vector3D origin, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualRightAngle3D(
                name,
                style,
                origin,
                direction1,
                direction2,
                radius,
                animationSpecs
            );
        }

        public static GrVisualRightAngle3D Create(string name, GrVisualCurveStyle3D style, GrVisualSurfaceThinStyle3D innerStyle, IFloat64Vector3D origin, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualRightAngle3D(
                name,
                style,
                origin,
                direction1,
                direction2,
                radius,
                animationSpecs
            )
            {
                InnerStyle = innerStyle
            };
        }


        public Float64Vector3D Origin { get; }

        public Float64Vector3D Direction1 { get; }

        public Float64Vector3D Direction2 { get; }

        public double Radius { get; }

        public double Width
            => Radius / 2d.Sqrt();

        public double Height
            => Radius / 2d.Sqrt();

        public override int PathPointCount { get; }

        public override double Length
            => Radius * 2d.Sqrt();

        public Float64Vector3D Normal
            => Direction1.VectorUnitCross(Direction2);

        public GrVisualSurfaceThinStyle3D? InnerStyle { get; set; }


        private GrVisualRightAngle3D(string name, GrVisualCurveStyle3D style, IFloat64Vector3D origin, IFloat64Vector3D direction1, IFloat64Vector3D direction2, double radius, GrVisualAnimationSpecs animationSpecs)
            : base(name, style, animationSpecs)
        {
            Origin = origin.ToVector3D();
            Direction1 = direction1.ToUnitVector();
            Direction2 = direction2.RejectOnUnitVector(Direction1).ToUnitVector();
            Radius = radius;

            Debug.Assert(IsValid());
        }


        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public Triplet<Float64Vector3D> GetArcPointsTriplet()
        {
            var s = Radius / Math.Sqrt(2d);

            return new Triplet<Float64Vector3D>(
                Origin + s * Direction1,
                Origin + s * (Direction1 + Direction2),
                Origin + s * Direction2
            );
        }

        public override IPointsPath3D GetPositionsPath()
        {
            return new ArrayPointsPath3D(
                GetArcPointsTriplet().GetItemArray().Cast<IFloat64Vector3D>()
            );
        }

        public override IPointsPath3D GetPositionsPath(double time)
        {
            throw new NotImplementedException();
        }

        public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
        {
            throw new NotImplementedException();
        }

        public GrVisualRightAngle3D SetAnimatedVisibility(GrVisualAnimatedVector1D visibility)
        {
            AnimatedVisibility = visibility;

            return this;
        }


    }
}