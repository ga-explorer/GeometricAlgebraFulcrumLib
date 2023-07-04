using GraphicsComposerLib.Rendering.Visuals.Space3D.Animations;
using GraphicsComposerLib.Rendering.Visuals.Space3D.Styles;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Basic
{
    public sealed class GrVisualArrowHead3D :
        GrVisualElementWithAnimation3D,
        IFloat64Vector3D
    {
        public sealed record KeyFrameRecord(
            int FrameIndex, 
            double Time,
            double Visibility,
            Float64Vector3D Origin, 
            Float64Vector3D Direction,
            double MaxHeight
        ) : GrVisualAnimatedGeometryKeyFrameRecord(
            FrameIndex,
            Time,
            Visibility
        );


        public static GrVisualArrowHead3D CreateStatic(string name, GrVisualCurveTubeStyle3D style, IFloat64Vector3D direction) 
        {
            return new GrVisualArrowHead3D(
                name, 
                style, 
                Float64Vector3D.Zero, 
                direction,
                GrVisualAnimationSpecs.Static
            );
        }

        public static GrVisualArrowHead3D CreateStatic(string name, GrVisualCurveTubeStyle3D style, IFloat64Vector3D position, IFloat64Vector3D direction) 
        {
            return new GrVisualArrowHead3D(
                name, 
                style, 
                position, 
                direction,
                GrVisualAnimationSpecs.Static
            );
        }
        
        public static GrVisualArrowHead3D Create(string name, GrVisualCurveTubeStyle3D style, IFloat64Vector3D position, IFloat64Vector3D direction, GrVisualAnimationSpecs animationSpecs) 
        {
            return new GrVisualArrowHead3D(
                name, 
                style, 
                position, 
                direction,
                animationSpecs
            );
        }
        
        public static GrVisualArrowHead3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D direction, GrVisualAnimationSpecs animationSpecs) 
        {
            return new GrVisualArrowHead3D(
                name, 
                style, 
                Float64Vector3D.Zero, 
                Float64Vector3D.E1,
                animationSpecs
            ).SetAnimatedDirection(direction);
        }

        public static GrVisualArrowHead3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, IFloat64Vector3D position, GrVisualAnimatedVector3D direction, GrVisualAnimationSpecs animationSpecs) 
        {
            return new GrVisualArrowHead3D(
                name, 
                style, 
                position, 
                Float64Vector3D.E1,
                animationSpecs
            ).SetAnimatedDirection(direction);
        }

        public static GrVisualArrowHead3D CreateAnimated(string name, GrVisualCurveTubeStyle3D style, GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D direction, GrVisualAnimationSpecs animationSpecs) 
        {
            return new GrVisualArrowHead3D(
                name, 
                style, 
                Float64Vector3D.Zero, 
                Float64Vector3D.E1,
                animationSpecs
            ).SetAnimatedOriginDirection(position, direction);
        }


        public GrVisualCurveTubeStyle3D Style { get; } 
        
        public int VSpaceDimensions 
            => 3;

        public IFloat64Vector3D Position { get; } 

        public Float64Vector3D Direction { get; }
        
        public double MaxHeight { get; }

        public GrVisualAnimatedVector3D? AnimatedPosition { get; set; }

        public GrVisualAnimatedVector3D? AnimatedDirection { get; set; }
        
        public double Item1 
            => Direction.X;
        
        public double Item2 
            => Direction.Y;
        
        public double Item3 
            => Direction.Z;
        
        public Float64Scalar X 
            => Direction.X;
        
        public Float64Scalar Y 
            => Direction.Y;
        
        public Float64Scalar Z 
            => Direction.Z;
    
        
        private GrVisualArrowHead3D(string name, GrVisualCurveTubeStyle3D style, IFloat64Vector3D position, IFloat64Vector3D direction, GrVisualAnimationSpecs animationSpecs) 
            : base(name, animationSpecs)
        {
            Position = position;
            MaxHeight = direction.ENorm();
            Direction = direction.ToUnitVector();
            Style = style;

            Debug.Assert(IsValid());
        }

        
        public override bool IsValid()
        {
            return Position.IsValid() &&
                   Direction.IsValid() &&
                   Direction.IsNearUnitVector() &&
                   MaxHeight.IsValid() &&
                   MaxHeight >= 0 &&
                   GetAnimatedGeometries().All(g => 
                       g.IsValid(AnimationSpecs.TimeRange)
                   );
        }
        
        public GrVisualArrowHead3D SetAnimatedVisibility(GrVisualAnimatedVector1D visibility)
        {
            AnimatedVisibility = visibility;

            return this;
        }

        public GrVisualArrowHead3D SetAnimatedOrigin(GrVisualAnimatedVector3D position)
        {
            AnimatedPosition = position;

            return this;
        }

        public GrVisualArrowHead3D SetAnimatedDirection(GrVisualAnimatedVector3D direction)
        {
            AnimatedDirection = direction;

            return this;
        }
        
        public GrVisualArrowHead3D SetAnimatedOriginDirection(GrVisualAnimatedVector3D position, GrVisualAnimatedVector3D direction)
        {
            AnimatedPosition = position;
            AnimatedDirection = direction;

            return this;
        }

        public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
        {
            var animatedGeometries = new List<GrVisualAnimatedGeometry>();

            if (AnimatedVisibility is not null)
                animatedGeometries.Add(AnimatedVisibility);

            if (AnimatedPosition is not null)
                animatedGeometries.Add(AnimatedPosition);
            
            if (AnimatedDirection is not null)
                animatedGeometries.Add(AnimatedDirection);

            return animatedGeometries;
        }
        
        public Float64Vector3D GetOrigin(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedPosition is null
                ? Position.ToVector3D()
                : AnimatedPosition.GetPoint(time);
        }
        
        public Float64Vector3D GetDirection(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedDirection is null
                ? Direction
                : AnimatedDirection.GetPoint(time).ToUnitVector();
        }
        
        public double GetMaxHeight(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedDirection is null
                ? MaxHeight
                : AnimatedDirection.GetPoint(time).ENorm();
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
                    GetOrigin(time), 
                    GetDirection(time),
                    GetMaxHeight(time)
                );
            }
        }
    }
}