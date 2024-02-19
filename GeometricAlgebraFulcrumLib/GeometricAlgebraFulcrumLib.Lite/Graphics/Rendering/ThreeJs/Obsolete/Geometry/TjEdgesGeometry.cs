using TextComposerLib.Code.JavaScript;
using JsCodeComponentUtils = TextComposerLib.Code.JavaScript.Obsolete.JsCodeComponentUtils;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// This can be used as a helper object to view the edges of a geometry.
/// https://threejs.org/docs/#api/en/geometries/EdgesGeometry
/// </summary>
public class TjEdgesGeometry :
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "EdgesGeometry";

    public TjBufferGeometryBase Geometry { get; }

    public int ThresholdAngleInDegrees { get; set; } = 1;


    public TjEdgesGeometry(TjBufferGeometryBase geometry)
    {
        Geometry = geometry;
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        JsCodeComponentUtils.SetValue(attributesDictionary, "geometry", Geometry)
            .SetValue("thresholdAngle", ThresholdAngleInDegrees, 1);
    }
}