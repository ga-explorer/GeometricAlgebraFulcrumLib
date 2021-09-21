using EuclideanGeometryLib.GraphicsGeometry;
using EuclideanGeometryLib.GraphicsGeometry.Triangles;

namespace GraphicsComposerLib.Xeogl.Geometry.Primitives
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


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("primitive", GraphicsTrianglesGeometry.PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetAttributeValue("positions", GraphicsTrianglesGeometry.VertexPoints, " // Vertex Position ", "[]")
                .SetAttributeValue("normals", GraphicsTrianglesGeometry.VertexNormals, " // Vertex Normal ", "[]")
                .SetAttributeValue("uv", GraphicsTrianglesGeometry.VertexUVs, " // Vertex UV ", "[]")
                .SetAttributeValue("colors", GraphicsTrianglesGeometry.VertexColors.ToXeoglRgbaNumbersArrayText(" // Vertex Color "), "[]")
                .SetAttributeValue("indices", VertexIndices.ToXeoglNumbersArrayText(), "[]")
                .SetAttributeValue("autoVertexNormals", AutoVertexNormals ? "true" : "false", "false");
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