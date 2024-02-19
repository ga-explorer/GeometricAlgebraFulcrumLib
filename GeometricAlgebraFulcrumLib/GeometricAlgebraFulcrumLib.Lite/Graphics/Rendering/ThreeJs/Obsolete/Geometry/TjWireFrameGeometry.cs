using TextComposerLib.Code.JavaScript;
using TextComposerLib.Code.JavaScript.Obsolete;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// This can be used as a helper object to view a geometry as a wire frame.
/// https://threejs.org/docs/#api/en/geometries/WireframeGeometry
/// </summary>
public class TjWireFrameGeometry :
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "WireframeGeometry";

    public TjBufferGeometryBase Geometry { get; }
        

    public TjWireFrameGeometry(TjBufferGeometryBase geometry)
    {
        Geometry = geometry;
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        attributesDictionary
            .SetValue("geometry", Geometry);
    }
}