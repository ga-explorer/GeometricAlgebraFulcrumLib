using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// CircleGeometry is a simple shape of Euclidean geometry. It is constructed from
/// a number of triangular segments that are oriented around a central point and extend
/// as far out as a given radius. It is built counter-clockwise from a start angle and a
/// given central angle. It can also be used to create regular polygons, where the number
/// of segments determines the number of sides.
/// https://threejs.org/docs/#api/en/geometries/CircleGeometry
/// </summary>
public class TjCircleGeometry :
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "CircleGeometry";

    public double Radius { get; set; }
        = 1d;

    public int Segments { get; set; }
        = 8;

    public double ThetaStart { get; set; }

    public double ThetaLength { get; set; }
        = 2d * System.Math.PI;


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        attributesDictionary
            .SetValue("radius", Radius, 1d)
            .SetValue("segments", Segments, 8)
            .SetValue("thetaStart", ThetaStart, 0d)
            .SetValue("thetaLength", ThetaLength, 2d * System.Math.PI);
    }
}