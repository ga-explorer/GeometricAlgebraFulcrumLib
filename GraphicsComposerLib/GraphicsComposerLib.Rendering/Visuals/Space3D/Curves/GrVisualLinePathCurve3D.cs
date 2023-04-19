using System.Collections.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves
{
    public sealed class GrVisualLinePathCurve3D :
        GrVisualCurve3D
    {
        public IReadOnlyList<IFloat64Tuple3D> PointList { get; }


        public GrVisualLinePathCurve3D(string name, IReadOnlyList<IFloat64Tuple3D> pointList)
            : base(name)
        {
            PointList = pointList;
        }
    
        public GrVisualLinePathCurve3D(string name, params IFloat64Tuple3D[] pointList)
            : base(name)
        {
            PointList = pointList;
        }
    
        public GrVisualLinePathCurve3D(string name, IEnumerable<IFloat64Tuple3D> pointList)
            : base(name)
        {
            PointList = pointList.ToImmutableArray();
        }


        public double GetLength()
        {
            var length = 0d;
            var point1 = PointList[0];

            for (var i = 1; i < PointList.Count; i++)
            {
                var point2 = PointList[i];

                length += point1.GetDistanceToPoint(point2);

                point1 = point2;
            }

            return length;
        }
    }
}