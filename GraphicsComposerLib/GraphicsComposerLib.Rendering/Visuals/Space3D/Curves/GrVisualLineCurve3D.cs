using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Rendering.Visuals.Space3D.Curves;

public sealed class GrVisualLineCurve3D :
    GrVisualCurve3D
{
    public IReadOnlyList<ITuple3D> PositionList { get; set; }


    public GrVisualLineCurve3D(string name) 
        : base(name)
    {
    }


    public double GetLength()
    {
        var length = 0d;
        var point1 = PositionList[0];

        for (var i = 1; i < PositionList.Count; i++)
        {
            var point2 = PositionList[i];

            length += point1.GetDistanceToPoint(point2);

            point1 = point2;
        }

        return length;
    }
}