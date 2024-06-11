using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Primitives.Triangles;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Geometry.Primitives;

public sealed class XeoglTrianglesGeometry 
    : XeoglPrimitivesGeometry
{
    public IGraphicsTriangleGeometry3D GraphicsTrianglesGeometry { get; set; }

    public override IGraphicsPrimitiveGeometry3D GraphicsGeometry
        => GraphicsTrianglesGeometry;

    public bool AutoVertexNormals { get; set; }


    public XeoglTrianglesGeometry()
    {
    }

    public XeoglTrianglesGeometry(IGraphicsTriangleGeometry3D geometryData)
    {
        GraphicsTrianglesGeometry = geometryData;
    }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetValue("primitive", GraphicsTrianglesGeometry.PrimitiveType, GraphicsPrimitiveType3D.Triangles)
            .SetNumbersArrayValue("positions", GraphicsTrianglesGeometry.GeometryPoints, " // Vertex Position ", "[]")
            .SetNumbersArrayValue("normals", GraphicsTrianglesGeometry.VertexNormals, " // Vertex Normal ", "[]")
            .SetNumbersArrayValue("uv", GraphicsTrianglesGeometry.VertexTextureUVs, " // Vertex UV ", "[]")
            .SetTextValue("colors", GraphicsTrianglesGeometry.VertexColors.ToSystemDrawingColors().ToJavaScriptRgbaNumbersArrayText(" // Vertex Color "), "[]")
            .SetTextValue("indices", VertexIndices.ToJavaScriptNumbersArrayText(), "[]")
            .SetTextValue("autoVertexNormals", AutoVertexNormals ? "true" : "false", "false");
    }

    //public override string ToString()
    //{
    //    var composer = new XeoglAttributesTextComposer();

    //    UpdateAttributesComposer(composer);

    //    return composer
    //        .AppendXeoglConstructorCall(this)
    //        .ToString();
    //}
}