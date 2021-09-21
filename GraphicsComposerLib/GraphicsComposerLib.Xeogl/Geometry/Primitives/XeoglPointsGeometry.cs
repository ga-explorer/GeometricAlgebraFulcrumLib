using EuclideanGeometryLib.GraphicsGeometry;
using EuclideanGeometryLib.GraphicsGeometry.Points;

namespace GraphicsComposerLib.Xeogl.Geometry.Primitives
{
    public sealed class XeoglPointsGeometry : XeoglPrimitivesGeometry
    {
        public IGraphicsPointsGeometry3D GraphicsPointsGeometry { get; set; }

        public override IGraphicsGeometry3D GraphicsGeometry
            => GraphicsPointsGeometry;


        public XeoglPointsGeometry()
        {
        }

        public XeoglPointsGeometry(IGraphicsPointsGeometry3D geometryData)
        {
            GraphicsPointsGeometry = geometryData;
        }


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetAttributeValue("positions", VertexPositions.ToXeoglNumbersArrayText(" // Vertex Position "), "[]")
                .SetAttributeValue("indices", VertexIndices.ToXeoglNumbersArrayText(), "[]");
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