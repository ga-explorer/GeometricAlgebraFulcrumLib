using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Meshes.PointsPath.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Animations;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Styles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Visuals.Space3D.Curves
{
    public sealed class GrVisualPointPathCurve3D :
        GrVisualCurveWithAnimation3D
    {
        public sealed record KeyFrameRecord(
            int FrameIndex, 
            double Time,
            double Visibility,
            IPointsPath3D PositionPath
        ) : GrVisualAnimatedGeometryKeyFrameRecord(
            FrameIndex,
            Time,
            Visibility
        );


        public static GrVisualPointPathCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, params IFloat64Vector3D[] positionList)
        {
            return new GrVisualPointPathCurve3D(
                name,
                style,
                new ArrayPointsPath3D(positionList),
                GrVisualAnimationSpecs.Static
            );
        }

        public static GrVisualPointPathCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, IReadOnlyList<IFloat64Vector3D> positionList)
        {
            return new GrVisualPointPathCurve3D(
                name,
                style,
                new ArrayPointsPath3D(positionList),
                GrVisualAnimationSpecs.Static
            );
        }
        
        public static GrVisualPointPathCurve3D CreateStatic(string name, GrVisualCurveStyle3D style, IPointsPath3D positionList)
        {
            return new GrVisualPointPathCurve3D(
                name,
                style,
                positionList,
                GrVisualAnimationSpecs.Static
            );
        }

        public static GrVisualPointPathCurve3D Create(string name, GrVisualCurveStyle3D style, IEnumerable<IFloat64Vector3D> positionList, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualPointPathCurve3D(
                name,
                style,
                new ArrayPointsPath3D(positionList),
                animationSpecs
            );
        }
        
        public static GrVisualPointPathCurve3D Create(string name, GrVisualCurveStyle3D style, IPointsPath3D positionList, GrVisualAnimationSpecs animationSpecs)
        {
            return new GrVisualPointPathCurve3D(
                name,
                style,
                positionList,
                animationSpecs
            );
        }
        
        public static GrVisualPointPathCurve3D CreateAnimated(string name, GrVisualCurveStyle3D style, GrVisualAnimatedVectorPath3D positionPath)
        {
            return new GrVisualPointPathCurve3D(
                name,
                style,
                positionPath.GetPointsPath(positionPath.AnimationSpecs.MinFrameTime),
                positionPath.AnimationSpecs
            ).SetAnimatedPositionPath(positionPath);
        }
        
        
        public int PointCount 
            => PositionPath.Count;

        public IPointsPath3D PositionPath { get; }

        public override int PathPointCount 
            => PositionPath.Count;

        public override double Length
        {
            get
            {
                var length = 0d;
                var position1 = PositionPath[0];

                for (var i = 1; i < PositionPath.Count; i++)
                {
                    var position2 = PositionPath[i];

                    length += position1.GetDistanceToPoint(position2);

                    position1 = position2;
                }

                return length;
            }
        }

        public GrVisualAnimatedVectorPath3D? AnimatedPositionPath { get; set; }

        
        private GrVisualPointPathCurve3D(string name, GrVisualCurveStyle3D style, int pointCount, GrVisualAnimationSpecs animationSpecs)
            : base(name, style, animationSpecs)
        {
            PositionPath = new ArrayPointsPath3D(pointCount);

            Debug.Assert(IsValid());
        }

        private GrVisualPointPathCurve3D(string name, GrVisualCurveStyle3D style, IPointsPath3D positionPath, GrVisualAnimationSpecs animationSpecs)
            : base(name, style, animationSpecs)
        {
            PositionPath = positionPath;

            Debug.Assert(IsValid());
        }
        
        
        public override bool IsValid()
        {
            return PositionPath.All(position => position.IsValid()) &&
                   AnimatedPositionPath.IsNullOrValid(AnimationSpecs.FrameTimeRange);
        }


        public GrVisualPointPathCurve3D SetAnimatedVisibility(GrVisualAnimatedScalar visibility)
        {
            AnimatedVisibility = visibility;

            return this;
        }

        public GrVisualPointPathCurve3D SetAnimatedPositionPath(GrVisualAnimatedVectorPath3D positionPath)
        {
            AnimatedPositionPath = positionPath;

            return this;
        }
        
        public override IReadOnlyList<GrVisualAnimatedGeometry> GetAnimatedGeometries()
        {
            var animatedGeometries = new List<GrVisualAnimatedGeometry>(2);

            if (AnimatedVisibility is not null)
                animatedGeometries.Add(AnimatedVisibility);

            if (AnimatedPositionPath is not null)
                animatedGeometries.Add(AnimatedPositionPath);

            return animatedGeometries;
        }
        
        public override IPointsPath3D GetPositionsPath()
        {
            return PositionPath;
        }

        public override IPointsPath3D GetPositionsPath(double time)
        {
            return AnimationSpecs.IsStatic || AnimatedPositionPath is null
                ? PositionPath
                : AnimatedPositionPath.GetPointsPath(time);
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
                    GetPositionsPath(time)
                );
            }
        }
        
    }
}