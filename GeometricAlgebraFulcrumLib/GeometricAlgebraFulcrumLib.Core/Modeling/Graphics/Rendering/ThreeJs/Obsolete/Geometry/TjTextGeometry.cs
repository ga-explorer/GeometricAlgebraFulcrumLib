using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// A class for generating text as a single geometry. It is constructed
/// by providing a string of text, and a hash of parameters consisting of
/// a loaded Font and settings for the geometry's parent ExtrudeGeometry.
/// See the Font, FontLoader and Creating_Text pages for additional details.
/// https://threejs.org/docs/#api/en/geometries/TextGeometry
/// </summary>
public class TjTextGeometry :
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "TextGeometry";

    public TjTextGeometrySettings Settings { get; }
        = new TjTextGeometrySettings();

    public string Text { get; } = string.Empty;


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        var settingsText =
            Settings.GetJavaScriptVariableNameOrCode();

        attributesDictionary
            .SetTextValue("text", Text, "[]")
            .SetTextValue("parameters", settingsText, "{}");
    }
}