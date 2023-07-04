using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Basic
{
    public sealed class GrVisualPoint3D :
        GrVisualElementWithAnimation3D,
        IFloat64Vector3D
    {
        public sealed record KeyFrameRecord(
            int FrameIndex,
            double Time,
            double Visibility,
            Float64Vector3D Position
        ) : GrVisualAnimatedGeometryKeyFrameRecord(
            FrameIndex, 
            Time, 
            Visibility
        );
        

        public static GrVisualPoint3D CreateStatic(string name, GrVisualSurfaceThickStyle3D style, IFloat64Vector3D position)
        {
            return new GrVisualPoint3D(
                name, 
                style, 
                position, 
                GrVisualAnimationSpecs.Static
            );
        }
        
        public static GrVisualPoint3D Create(string name, GrVisualSurfaceThickStyle3D style, IFloat64Vector3D position, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualPoint3D(
                name, 
                style, 
                position,
                animationSpecs
            );
        }
        
        public static GrVisualPoint3D CreateAnimated(string name, GrVisualSurfaceThickStyle3D style, GrVisualAnimatedVector3D position, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualPoint3D(
                name, 
                style, 
                Float64Vector3D.Zero,
                animationSpecs
            ).SetAnimatedPosition(position);
        }


        public GrVisualSurfaceThickStyle3D Style { get; }

        public IFloat64Vector3D Position { get; } 
        
        public GrVisualAnimatedVector3D? AnimatedPosition { get; set; }
        
        public int VSpaceDimensions 
            => 3;

        public double Item1 
            => Position.X;
        
        public double Item2 
            => Position.Y;
        
        public double Item3 
            => Position.Z;
        
        public Float64Scalar X 
            => Position.X;
        
        public Float64Scalar Y 
            => Position.Y;
        
        public Float64Scalar Z 
            => Position.Z;


        private GrVisualPoint3D(string name, GrVisualSurfaceThickStyle3D style, IFloat64Vector3D position, GrVisualAnimationSpecs animationSpecs) 
            : base(name, animationSpecs)
        {
            Position = position;
            Style = style;

            Debug.Assert(IsValid());
        }
        

        public override bool IsValid()
        {
            return Position.IsValid() &&
                   AnimatedPosition.IsNullOrValid(AnimationSpecs.TimeRange);
        }

        public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
        {
            var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

            if (AnimatedVisibility is not null)
                animatedGeometries.Add(AnimatedVisibility);

            if (AnimatedPosition is not null)
                animatedGeometries.Add(AnimatedPosition);

            return animatedGeometries;
        }
        
        public GrVisualPoint3D SetAnimatedVisibility(GrVisualAnimatedVector1D visibility)
        {
            AnimatedVisibility = visibility;
            
            return this;
        }

        public GrVisualPoint3D SetAnimatedPosition(GrVisualAnimatedVector3D position)
        {
            AnimatedPosition = position;

            return this;
        }
        
        public Float64Vector3D GetPosition(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedPosition is null
                ? Position.ToVector3D()
                : AnimatedPosition.GetPoint(time);
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
                    GetPosition(time)
                );
            }
        }
    }
}
