using EuclideanGeometryLib.GraphicsGeometry;
using EuclideanGeometryLib.GraphicsGeometry.Triangles;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.WebGl.Xeogl.Geometry.Primitives
{
    public sealed class XeoglTrianglesGeometry 
        : XeoglPrimitivesGeometry
    {
        public IGraphicsTrianglesGeometry3D GraphicsTrianglesGeometry { get; set; }

        public override IGraphicsGeometry3D GraphicsGeometry
            => GraphicsTrianglesGeometry;

        public bool AutoVertexNormals { get; set; }


        public XeoglTrianglesGeometry()
        {
        }

        public XeoglTrianglesGeometry(IGraphicsTrianglesGeometry3D geometryData)
        {
            GraphicsTrianglesGeometry = geometryData;
        }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("primitive", GraphicsTrianglesGeometry.PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetNumbersArrayValue("positions", GraphicsTrianglesGeometry.VertexPoints, " // Vertex Position ", "[]")
                .SetNumbersArrayValue("normals", GraphicsTrianglesGeometry.VertexNormals, " // Vertex Normal ", "[]")
                .SetNumbersArrayValue("uv", GraphicsTrianglesGeometry.VertexUVs, " // Vertex UV ", "[]")
                .SetTextValue("colors", GraphicsTrianglesGeometry.VertexColors.ToJavaScriptRgbaNumbersArrayText(" // Vertex Color "), "[]")
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
}