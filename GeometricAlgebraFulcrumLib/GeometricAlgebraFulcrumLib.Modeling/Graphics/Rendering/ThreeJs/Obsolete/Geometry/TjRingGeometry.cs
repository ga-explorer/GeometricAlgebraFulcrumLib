using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// https://threejs.org/docs/#api/en/geometries/RingGeometry
/// </summary>
public class TjRingGeometry :
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "RingGeometry";

    public double InnerRadius { get; set; } = 0.5d;

    public double OuterRadius { get; set; } = 1d;

    public int PhiSegments { get; set; } = 1;

    public int ThetaSegments { get; set; } = 8;

    public double ThetaStart { get; set; } = 0d;

    public double ThetaLength { get; set; } = 2d * System.Math.PI;

        
    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        attributesDictionary
            .SetValue("innerRadius", InnerRadius, 0.5d)
            .SetValue("outerRadius", OuterRadius, 1d)
            .SetValue("phiSegments", PhiSegments, 1)
            .SetValue("thetaSegments", ThetaSegments, 8)
            .SetValue("thetaStart", ThetaStart, 0d)
            .SetValue("thetaLength", ThetaLength, 2d * System.Math.PI);
    }
}