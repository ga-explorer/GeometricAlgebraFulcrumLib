using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Geometry.Curves;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// Creates extruded geometry from a path shape.
/// https://threejs.org/docs/#api/en/geometries/ExtrudeGeometry
/// </summary>
public class TjExtrudeGeometry :
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "ExtrudeGeometry";

    public TjExtrudeGeometrySettings Settings { get; }
        = new TjExtrudeGeometrySettings();

    public List<TjShape> ShapesList { get; }
        = new List<TjShape>();


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        var shapesText =
            ShapesList
                .Select(s => s.GetJavaScriptVariableNameOrCode())
                .Concatenate(", ", "[", "]");

        var settingsText =
            Settings.GetJavaScriptVariableNameOrCode();

        attributesDictionary
            .SetTextValue("shapes", shapesText, "[]")
            .SetTextValue("options", settingsText, "{}");
    }
}