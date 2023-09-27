using System.Diagnostics;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Surfaces
{
    public sealed class GrVisualCircleSurface3D :
        GrVisualSurfaceWithAnimation3D
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


        public static GrVisualCircleSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, bool drawEdge)
        {
            return new GrVisualCircleSurface3D(
                name,
                style, 
                center, 
                normal, 
                radius,
                drawEdge,
                GrVisualAnimationSpecs.Static
            );
        }
        
        public static GrVisualCircleSurface3D Create(string name, GrVisualSurfaceStyle3D style, IFloat64Vector3D normal, double radius, bool drawEdge, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualCircleSurface3D(
                name,
                style, 
                Float64Vector3D.Zero, 
                normal, 
                radius,
                drawEdge,
                animationSpecs
            );
        }
        
        public static GrVisualCircleSurface3D Create(string name, GrVisualSurfaceStyle3D style, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, bool drawEdge, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualCircleSurface3D(
                name,
                style, 
                center, 
                normal, 
                radius,
                drawEdge,
                animationSpecs
            );
        }
        
        public static GrVisualCircleSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar radius, bool drawEdge)
        {
            return new GrVisualCircleSurface3D(
                name,
                style, 
                Float64Vector3D.Zero, 
                Float64Vector3D.E1, 
                1d,
                drawEdge,
                normal.AnimationSpecs
            ).SetAnimatedNormal(normal)
                .SetAnimatedRadius(radius);
        }
        
        public static GrVisualCircleSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal, double radius, bool drawEdge)
        {
            return new GrVisualCircleSurface3D(
                name,
                style, 
                Float64Vector3D.Zero, 
                Float64Vector3D.E1, 
                radius,
                drawEdge,
                center.AnimationSpecs
            ).SetAnimatedCenterNormal(
                center,
                normal
            );
        }
        
        public static GrVisualCircleSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, Float64Vector3D center, Float64Vector3D normal, GrVisualAnimatedScalar radius, bool drawEdge)
        {
            return new GrVisualCircleSurface3D(
                    name,
                    style, 
                    center, 
                    normal, 
                    1d,
                    drawEdge,
                    radius.AnimationSpecs
                ).SetAnimatedRadius(radius);
        }
        
        public static GrVisualCircleSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, Float64Vector3D center, GrVisualAnimatedVector3D normal, double radius, bool drawEdge)
        {
            return new GrVisualCircleSurface3D(
                    name,
                    style, 
                    center, 
                    Float64Vector3D.E1, 
                    radius,
                    drawEdge,
                    normal.AnimationSpecs
                ).SetAnimatedNormal(normal);
        }

        public static GrVisualCircleSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, Float64Vector3D center, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar radius, bool drawEdge)
        {
            return new GrVisualCircleSurface3D(
                name,
                style, 
                center, 
                Float64Vector3D.E1, 
                1d,
                drawEdge,
                normal.AnimationSpecs
            ).SetAnimatedNormal(normal)
            .SetAnimatedRadius(radius);
        }

        public static GrVisualCircleSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar radius, bool drawEdge)
        {
            return new GrVisualCircleSurface3D(
                name,
                style, 
                Float64Vector3D.Zero, 
                Float64Vector3D.E1, 
                1d,
                drawEdge,
                center.AnimationSpecs
            ).SetAnimatedCenterNormalRadius(
                center,
                normal,
                radius
            );
        }


        public IFloat64Vector3D Center { get; }

        public Float64Vector3D Normal { get; }

        public double Radius { get; }

        public GrVisualAnimatedVector3D? AnimatedCenter { get; set; }

        public GrVisualAnimatedVector3D? AnimatedNormal { get; set; }

        public GrVisualAnimatedScalar? AnimatedRadius { get; set; }

        public bool DrawEdge { get; } 


        private GrVisualCircleSurface3D(string name, GrVisualSurfaceStyle3D style, IFloat64Vector3D center, IFloat64Vector3D normal, double radius, bool drawEdge, GrVisualAnimationSpecs animationSpecs) 
            : base(name, style, animationSpecs)
        {
            Center = center;
            Normal = normal.ToUnitVector();
            Radius = radius;
            DrawEdge = drawEdge;

            Debug.Assert(IsValid());
        }


        public override bool IsValid()
        {
            return Center.IsValid() &&
                   Normal.IsValid() &&
                   Normal.IsNearUnitVector() &&
                   Radius.IsValid() &&
                   Radius > 0 &&
                   GetAnimatedGeometries().All(g => g.IsValid());
        }

        public double GetEdgeLength()
        {
            return 2d * Math.PI * Radius;
        }

        public Triplet<Float64Vector3D> GetEdgePointsTriplet()
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
        
        public GrVisualCircleSurface3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
        {
            AnimatedVisibility = visibility;
            
            return this;
        }

        public GrVisualCircleSurface3D SetAnimatedCenter(GrVisualAnimatedVector3D center)
        {
            AnimatedCenter = center;
            
            return this;
        }
        
        public GrVisualCircleSurface3D SetAnimatedNormal(GrVisualAnimatedVector3D normal)
        {
            AnimatedNormal = normal;
            
            return this;
        }
        
        public GrVisualCircleSurface3D SetAnimatedRadius(GrVisualAnimatedScalar radius)
        {
            AnimatedRadius = radius;
            
            return this;
        }
        
        public GrVisualCircleSurface3D SetAnimatedCenterNormal(GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal)
        {
            AnimatedCenter = center;
            AnimatedNormal = normal;
            
            return this;
        }

        public GrVisualCircleSurface3D SetAnimatedCenterNormalRadius(GrVisualAnimatedVector3D center, GrVisualAnimatedVector3D normal, GrVisualAnimatedScalar radius)
        {
            AnimatedCenter = center;
            AnimatedNormal = normal;
            AnimatedRadius = radius;
            
            return this;
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
                : AnimatedRadius.GetValue(time);
        }

        public IEnumerable<KeyFrameRecord> GetKeyFrameRecords()
        {
            Debug.Assert(IsValid());

            foreach (var frameIndex in GetValidFrameIndexSet())
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