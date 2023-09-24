using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Curves
{
    public sealed class GrVisualLineSegment3D :
        GrVisualCurveWithAnimation3D
    {
        public sealed record KeyFrameRecord(
            int FrameIndex, 
            double Time,
            double Visibility,
            Float64Vector3D Position1, 
            Float64Vector3D Position2
        ) : GrVisualAnimatedGeometryKeyFrameRecord(
            FrameIndex,
            Time,
            Visibility
        );

        
        public static GrVisualLineSegment3D CreateStatic(string name, GrVisualCurveStyle3D style, IFloat64Vector3D position2)
        {
            return new GrVisualLineSegment3D(
                name,
                style, 
                Float64Vector3D.Zero, 
                position2,
                GrVisualAnimationSpecs.Static
            );
        }

        public static GrVisualLineSegment3D CreateStatic(string name, GrVisualCurveStyle3D style, IFloat64Vector3D position1, IFloat64Vector3D position2)
        {
            return new GrVisualLineSegment3D(
                name,
                style, 
                position1, 
                position2,
                GrVisualAnimationSpecs.Static
            );
        }
        
        public static GrVisualLineSegment3D Create(string name, GrVisualCurveStyle3D style, IFloat64Vector3D position2, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualLineSegment3D(
                name,
                style, 
                Float64Vector3D.Zero, 
                position2,
                animationSpecs
            );
        }

        public static GrVisualLineSegment3D Create(string name, GrVisualCurveStyle3D style, IFloat64Vector3D position1, IFloat64Vector3D position2, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualLineSegment3D(
                name,
                style, 
                position1, 
                position2,
                animationSpecs
            );
        }
        
        public static GrVisualLineSegment3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVector3D position1, GrVisualAnimatedVector3D position2)
        {
            return new GrVisualLineSegment3D(
                name,
                style, 
                Float64Vector3D.Zero, 
                Float64Vector3D.E1, 
                position1.AnimationSpecs
            ).SetAnimatedPositions(position1, position2);
        }


        public IFloat64Vector3D Position1 { get; }

        public IFloat64Vector3D Position2 { get; }
        
        public Float64Vector3D Direction 
            => Position1.GetDirectionTo(Position2);
        
        public Float64Vector3D UnitDirection 
            => Position1.GetUnitDirectionTo(Position2);

        public override int PathPointCount 
            => 2;

        public override double Length 
            => Position1.GetDistanceToPoint(Position2);

        public GrVisualAnimatedVector3D? AnimatedPosition1 { get; set; }

        public GrVisualAnimatedVector3D? AnimatedPosition2 { get; set; }


        private GrVisualLineSegment3D(string name, GrVisualCurveStyle3D style, IFloat64Vector3D position1, IFloat64Vector3D position2, GrVisualAnimationSpecs animationSpecs) 
            : base(name, style, animationSpecs)
        {
            //if (position1.Subtract(position2).IsZeroVector())
            //    throw new InvalidOperationException();

            Position1 = position1;
            Position2 = position2;

            Debug.Assert(IsValid());
        }

        
        public override bool IsValid()
        {
            return Position1.IsValid() &&
                   Position2.IsValid() &&
                   //!Position1.GetDistanceSquaredToPoint(Position2).IsZero() &&
                   GetAnimatedGeometries().All(g => g.IsValid());
        }

        public override IPointsPath3D GetPositionsPath()
        {
            return new LinearPointsPath3D(
                Position1, 
                Position2, 
                2
            );
        }

        public override IPointsPath3D GetPositionsPath(double time)
        {
            return new LinearPointsPath3D(
                GetPosition1(time), 
                GetPosition2(time), 
                2
            );
        }

        public GrVisualLineSegment3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
        {
            AnimatedVisibility = visibility;
            
            return this;
        }

        public GrVisualLineSegment3D SetAnimatedPosition1(GrVisualAnimatedVector3D position1)
        {
            AnimatedPosition1 = position1;

            return this;
        }
        
        public GrVisualLineSegment3D SetAnimatedPosition2(GrVisualAnimatedVector3D position2)
        {
            AnimatedPosition2 = position2;

            return this;
        }
        
        public GrVisualLineSegment3D SetAnimatedPositions(GrVisualAnimatedVector3D position1, GrVisualAnimatedVector3D position2)
        {
            AnimatedPosition1 = position1;
            AnimatedPosition2 = position2;

            return this;
        }

        public GrVisualLineSegment3D SetAnimatedDirection(GrVisualAnimatedVector3D direction)
        {
            AnimatedPosition2 = AnimatedPosition1 is null
                ? Position1 + direction 
                : AnimatedPosition1 + direction;

            return this;
        }

        public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
        {
            var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

            if (AnimatedVisibility is not null)
                animatedGeometries.Add(AnimatedVisibility);

            if (AnimatedPosition1 is not null)
                animatedGeometries.Add(AnimatedPosition1);
            
            if (AnimatedPosition2 is not null)
                animatedGeometries.Add(AnimatedPosition2);

            return animatedGeometries;
        }
        
        public Float64Vector3D GetPosition1(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedPosition1 is null
                ? Position1.ToVector3D()
                : AnimatedPosition1.GetPoint(time);
        }
        
        public Float64Vector3D GetPosition2(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedPosition2 is null
                ? Position2.ToVector3D()
                : AnimatedPosition2.GetPoint(time);
        }

        public Float64Vector3D GetDirection(double time)
        {
            return GetPosition2(time) - GetPosition1(time);
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
                    GetPosition1(time), 
                    GetPosition2(time)
                );
            }
        }
    }
}