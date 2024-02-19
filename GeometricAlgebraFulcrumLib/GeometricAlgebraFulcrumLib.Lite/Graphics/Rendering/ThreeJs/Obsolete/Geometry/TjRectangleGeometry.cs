using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// https://threejs.org/docs/#api/en/geometries/PlaneGeometry
/// </summary>
public class TjRectangleGeometry : 
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "PlaneGeometry";

    public double Width { get; set; } = 1d;

    public double Height { get; set; } = 1d;

    public int WidthSegments { get; set; } = 1;

    public int HeightSegments { get; set; } = 1;


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        attributesDictionary
            .SetValue("width", Width, 1d)
            .SetValue("height", Height, 1d)
            .SetValue("widthSegments", WidthSegments, 1)
            .SetValue("heightSegments", HeightSegments, 1);
    }
}