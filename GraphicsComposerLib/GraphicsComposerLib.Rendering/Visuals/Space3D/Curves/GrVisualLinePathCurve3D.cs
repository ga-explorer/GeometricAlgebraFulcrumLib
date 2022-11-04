using System.Collections.Immutable;
using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;

public sealed class GrVisualLinePathCurve3D :
    GrVisualCurve3D
{
    public IReadOnlyList<ITuple3D> PointList { get; }


    public GrVisualLinePathCurve3D(string name, IReadOnlyList<ITuple3D> pointList)
        : base(name)
    {
        PointList = pointList;
    }
    
    public GrVisualLinePathCurve3D(string name, params ITuple3D[] pointList)
        : base(name)
    {
        PointList = pointList;
    }
    
    public GrVisualLinePathCurve3D(string name, IEnumerable<ITuple3D> pointList)
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