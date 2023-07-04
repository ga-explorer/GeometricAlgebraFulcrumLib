using System.Diagnostics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves
{
    public sealed class GrVisualCircleCurve3D :
        GrVisualCurveWithAnimation3D
    {
        public sealed record KeyFrameRecord(
            int FrameIndex, 
            double Time,
            double Visibility,
            Float64Vector3D Center, 
            Float64Vector3D Normal,
            double Radius
        ) : GrVisualAnimatedGeometryKeyFrameRecord(
            FrameIndex,
            Time,
            Visibility
        );


        public static GrVisualCircleCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, IFloat64Vector3D normal, double radius)
        {
            return new GrVisualCircleCurve3D(
                name,
                style,
                Float64Vector3D.Zero, 
                normal,
                radius,
                GrVisualAnimationSpecs.Static
            );
        }

        public static GrVisualCircleCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, IFloat64Vector3D center, IFloat64Vector3D normal, double radius)
        {
            return new GrVisualCircleCurve3D(
                name,
                style,
                center,
                normal,
                radius,
                GrVisualAnimationSpecs.Static
            );
        }
        
        public static GrVisualCircleCurve3D Create(string name, GrVisualCurveStyle3D style, IFloat64Vector3D normal, double radius, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualCircleCurve3D(
                name,
                style,
                Float64Vector3D.Zero, 
                normal,
                radius,
                animationSpecs
            );
        }

        public static GrVisualCircleCurve3D Create(string name, GrVisualCurveStyle3D style, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualCircleCurve3D(
                name,
                style,
                center,
                normal,
                radius,
                animationSpecs
            );
        }
        
        public static GrVisualCircleCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVector3D normal, GrVisualAnimatedVector1D radius, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualCircleCurve3D(
                name,
                style,
                Float64Vector3D.Zero, 
                Float64Vector3D.E2, 
                1d,
                animationSpecs
            ).SetAnimatedNormal(normal)
                .SetAnimatedRadius(radius);
        }

        public static GrVisualCircleCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal, GrVisualAnimatedVector1D radius, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualCircleCurve3D(
                name,
                style,
                Float64Vector3D.Zero, 
                Float64Vector3D.E2, 
                1d,
                animationSpecs
            ).SetAnimatedCenter(center)
                .SetAnimatedNormal(normal)
                .SetAnimatedRadius(radius);
        }


        public IFloat64Vector3D Center { get; }

        public Float64Vector3D Normal { get; }

        public double Radius { get; }

        public override int PathPointCount 
            => 360;

        public override double Length 
            => 2d * Math.PI * Radius;

        public GrVisualAnimatedVector3D? AnimatedCenter { get; set; }

        public GrVisualAnimatedVector3D? AnimatedNormal { get; set; }

        public GrVisualAnimatedVector1D? AnimatedRadius { get; set; }
        
        
        private GrVisualCircleCurve3D(string name, GrVisualCurveStyle3D style, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, GrVisualAnimationSpecs animationSpecs) 
            : base(name, style, animationSpecs)
        {
            Center = center;
            Normal = normal.ToUnitVector();
            Radius = radius;

            Debug.Assert(IsValid());
        }

        
        public override bool IsValid()
        {
            return Center.IsValid() &&
                   Normal.IsValid() &&
                   Normal.IsNearUnitVector() &&
                   Radius.IsValid() &&
                   Radius > 0 &&
                   GetAnimatedGeometries().All(g => 
                       g.IsValid(AnimationSpecs.TimeRange)
                   );
        }

        public Triplet<Float64Vector3D> GetPointsTriplet()
        {
            var quaternion = LinUnitBasisVector3D.PositiveZ.CreateAxisToVectorRotationQuaternion(
                Normal.ToUnitVector()
            );

            const double angle = 2d * Math.PI / 3d;

            var a = Radius * Math.Cos(angle);
            var b = Radius * Math.Sin(angle);

            var point1 = Center + quaternion.RotateVector(Radius, 0, 0);
            var point2 = Center + quaternion.RotateVector(a, b, 0);
            var point3 = Center + quaternion.RotateVector(a, -b, 0);

            return new Triplet<Float64Vector3D>(point1, point2, point3);
        }
        
        public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
        {
            var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

            if (AnimatedVisibility is not null)
                animatedGeometries.Add(AnimatedVisibility);

            if (AnimatedCenter is not null)
                animatedGeometries.Add(AnimatedCenter);
            
            if (AnimatedNormal is not null)
                animatedGeometries.Add(AnimatedNormal);

            if (AnimatedRadius is not null)
                animatedGeometries.Add(AnimatedRadius);

            return animatedGeometries;
        }
        
        public override IPointsPath3D GetPositionsPath()
        {
            var (direction1, direction2) = 
                Normal.GetUnitNormalPair();

            var angles = 
                0d.GetLinearRange(2d * Math.PI, PathPointCount, false);

            var points =
                angles.Select(angle => 
                    Center + Radius * angle.Cos() * direction1 +
                    Center + Radius * angle.Sin() * direction2
                );

            return new ArrayPointsPath3D(points);
        }

        public override IPointsPath3D GetPositionsPath(double time)
        {
            var center = GetCenter(time);
            var normal = GetNormal(time);
            var radius = GetRadius(time);

            var (direction1, direction2) = 
                normal.GetUnitNormalPair();

            var angles = 
                0d.GetLinearRange(2d * Math.PI, PathPointCount, false);

            var points =
                angles.Select(angle => 
                    center + radius * angle.Cos() * direction1 +
                    center + radius * angle.Sin() * direction2
                );

            return new ArrayPointsPath3D(points);
        }

        public GrVisualCircleCurve3D SetAnimatedVisibility(GrVisualAnimatedVector1D visibility)
        {
            AnimatedVisibility = visibility;
            
            return this;
        }

        public GrVisualCircleCurve3D SetAnimatedCenter(GrVisualAnimatedVector3D center)
        {
            AnimatedCenter = center;
            
            return this;
        }
        
        public GrVisualCircleCurve3D SetAnimatedNormal(GrVisualAnimatedVector3D normal)
        {
            AnimatedNormal = normal;
            
            return this;
        }
        
        public GrVisualCircleCurve3D SetAnimatedRadius(GrVisualAnimatedVector1D radius)
        {
            AnimatedRadius = radius;
            
            return this;
        }

        public Float64Vector3D GetCenter(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedCenter is null
                ? Center.ToVector3D()
                : AnimatedCenter.GetPoint(time);
        }
        
        public Float64Vector3D GetNormal(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedNormal is null
                ? Normal
                : AnimatedNormal.GetPoint(time).ToUnitVector();
        }
        
        public double GetRadius(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedRadius is null
                ? Radius
                : AnimatedRadius.GetPoint(time);
        }
        
        public IEnumerable<KeyFrameRecord> GetKeyFrameRecords()
        {
            Debug.Assert(IsValid());

            foreach (var frameIndex in KeyFrameRange)
            {
                var time = (double)frameIndex / AnimationSpecs.FrameRate;
                
                yield return new KeyFrameRecord(
                    frameIndex, 
                    time, 
                    GetVisibility(time),
                    GetCenter(time), 
                    GetNormal(time),
                    GetRadius(time)
                );
            }
        }

    }
}