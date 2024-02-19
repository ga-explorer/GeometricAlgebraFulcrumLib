using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ThreeJs.Obsolete.Geometry;

/// <summary>
/// BoxGeometry is a geometry class for a rectangular cuboid with a
/// given 'width', 'height', and 'depth'. On creation, the cuboid is
/// centered on the origin, with each edge parallel to one of the axes.
/// https://threejs.org/docs/#api/en/geometries/BoxGeometry
/// </summary>
public class TjBoxGeometry : 
    TjBufferGeometryBase
{
    public override string JavaScriptClassName 
        => "BoxGeometry";

    public double Width { get; set; } = 1d;

    public double Height { get; set; } = 1d;

    public double Depth { get; set; } = 1d;

    public int WidthSegments { get; set; } = 1;

    public int HeightSegments { get; set; } = 1;

    public int DepthSegments { get; set; } = 1;


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary attributesDictionary)
    {
        base.UpdateConstructorAttributes(attributesDictionary);

        attributesDictionary
            .SetValue("width", Width, 1d)
            .SetValue("height", Height, 1d)
            .SetValue("depth", Depth, 1d)
            .SetValue("widthSegments", WidthSegments, 1)
            .SetValue("heightSegments", HeightSegments, 1)
            .SetValue("depthSegments", DepthSegments, 1);
    }
}