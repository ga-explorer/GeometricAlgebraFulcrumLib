using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Geometry.Curves;
using TextComposerLib.Code.JavaScript;
using TextComposerLib.Text;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// Creates an one-sided polygonal geometry from one or more path shapes.
/// https://threejs.org/docs/#api/en/geometries/ShapeGeometry
/// </summary>
public class TjShapeGeometry :
    TjBufferGeometryBase
{
    public override string JavaScriptClassName
        => "ShapeGeometry";

    public int CurveSegments { get; set; } = 12;

    public List<TjShape> ShapesList { get; }
        = new List<TjShape>();

        
    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        var shapesText =
            ShapesList
                .Select(s => s.GetJavaScriptVariableNameOrCode())
                .Concatenate(", ", "[", "]");
            
        attributesDictionary
            .SetValue("curveSegments", CurveSegments, 12)
            .SetTextValue("shapes", shapesText, "[]");
    }
}