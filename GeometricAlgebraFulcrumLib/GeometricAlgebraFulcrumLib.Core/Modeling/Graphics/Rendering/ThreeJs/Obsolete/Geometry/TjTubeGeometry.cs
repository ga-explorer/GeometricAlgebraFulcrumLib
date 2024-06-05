using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Geometry.Curves;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// Creates a tube that extrudes along a 3d curve.
/// https://threejs.org/docs/#api/en/geometries/TubeGeometry
/// </summary>
public class TjTubeGeometry :
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "TubeGeometry";

    public TjCurveBase Curve { get; set; }

    public double Radius { get; set; } = 1d;

    public int RadialSegments { get; set; } = 8;

    public int TubularSegments { get; set; } = 64;

    public bool Closed { get; set; } = false;


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        attributesDictionary
            .SetValue("radius", Radius, 1d)
            .SetValue("radialSegments", RadialSegments, 8)
            .SetValue("tubularSegments", TubularSegments, 64)
            .SetValue("closed", Closed, false)
            .SetTextValue("path", Curve.GetJavaScriptVariableNameOrCode());
    }
}