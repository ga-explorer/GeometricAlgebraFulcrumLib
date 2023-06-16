using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Surfaces
{
    public sealed class GrVisualSphereSurface3D :
        GrVisualSurfaceWithAnimation3D
    {
        public sealed record KeyFrameRecord(
            int FrameIndex, 
            double Time,
            double Visibility,
            Float64Vector3D Center, 
            double Radius
        ) : GrVisualAnimatedGeometryKeyFrameRecord(
            FrameIndex,
            Time,
            Visibility
        );


        public static GrVisualSphereSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, double radius) 
        {
            return new GrVisualSphereSurface3D(
                name,
                style,
                Float64Vector3D.Zero, 
                radius,
                GrVisualAnimationSpecs.Static
            );
        }
        
        public static GrVisualSphereSurface3D CreateStatic(string name, GrVisualSurfaceStyle3D style, IFloat64Tuple3D center, double radius) 
        {
            return new GrVisualSphereSurface3D(
                name,
                style,
                center,
                radius,
                GrVisualAnimationSpecs.Static
            );
        }
        
        public static GrVisualSphereSurface3D Create(string name, GrVisualSurfaceStyle3D style, double radius, GrVisualAnimationSpecs animationSpecs) {
            return new GrVisualSphereSurface3D(
                name,
                style,
                Float64Vector3D.Zero, 
                radius,
                animationSpecs
            );
        }

        public static GrVisualSphereSurface3D Create(string name, GrVisualSurfaceStyle3D style, IFloat64Tuple3D center, double radius, GrVisualAnimationSpecs animationSpecs) {
            return new GrVisualSphereSurface3D(
                name,
                style,
                center,
                radius,
                animationSpecs
            );
        }
        
        public static GrVisualSphereSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector1D radius, GrVisualAnimationSpecs animationSpecs) {
            return new GrVisualSphereSurface3D(
                name,
                style,
                Float64Vector3D.Zero, 
                1d,
                animationSpecs
            ).SetAnimatedRadius(radius);
        }

        public static GrVisualSphereSurface3D CreateAnimated(string name, GrVisualSurfaceStyle3D style, GrVisualAnimatedVector3D center, GrVisualAnimatedVector1D radius, GrVisualAnimationSpecs animationSpecs) {
            return new GrVisualSphereSurface3D(
                name,
                style,
                Float64Vector3D.Zero, 
                1d,
                animationSpecs
            ).SetAnimatedCenter(center)
                .SetAnimatedRadius(radius);
        }


        public IFloat64Tuple3D Center { get; }
        
        public double Radius { get; }

        public GrVisualAnimatedVector3D? AnimatedCenter { get; set; }
        
        public GrVisualAnimatedVector1D? AnimatedRadius { get; set; }


        private GrVisualSphereSurface3D(string name, GrVisualSurfaceStyle3D style, IFloat64Tuple3D center, double radius, GrVisualAnimationSpecs animationSpecs) 
            : base(name, style, animationSpecs)
        {
            Center = center;
            Radius = radius;

            Debug.Assert(IsValid());
        }


        public override bool IsValid()
        {
            return Center.IsValid() &&
                   Radius.IsValid() &&
                   Radius > 0 &&
                   GetAnimatedGeometries().All(g => 
                       g.IsValid(AnimationSpecs.TimeRange)
                   );
        }
        
        public GrVisualSphereSurface3D SetAnimatedVisibility(GrVisualAnimatedVector1D visibility)
        {
            AnimatedVisibility = visibility;
            
            return this;
        }

        public GrVisualSphereSurface3D SetAnimatedCenter(GrVisualAnimatedVector3D center)
        {
            AnimatedCenter = center;
            
            return this;
        }
        
        public GrVisualSphereSurface3D SetAnimatedRadius(GrVisualAnimatedVector1D radius)
        {
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
                    GetRadius(time)
                );
            }
        }
    }
}