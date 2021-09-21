using System.Collections.Generic;
using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicShapes.Lines;
using EuclideanGeometryLib.GraphicsGeometry;
using EuclideanGeometryLib.GraphicsGeometry.Lines;

namespace GraphicsComposerLib.Xeogl.Geometry.Primitives
{
    public sealed class XeoglLinesGeometry : XeoglPrimitivesGeometry
    {
        public static XeoglLinesGeometry CreateLineSegment(ITuple3D point1, ITuple3D point2)
        {
            var geometryData = GraphicsLinesGeometry3D.Create(point1, point2);

            return new XeoglLinesGeometry(geometryData);
        }

        public static XeoglLinesGeometry CreateLineSegment(ILineSegment3D lineSegment)
        {
            var geometryData = GraphicsLinesGeometry3D.Create(lineSegment);

            return new XeoglLinesGeometry(geometryData);
        }

        public static XeoglLinesGeometry CreateLineSegments(params ILineSegment3D[] lineSegmentsList)
        {
            var geometryData = GraphicsLinesGeometry3D.Create(lineSegmentsList);

            return new XeoglLinesGeometry(geometryData);
        }

        public static XeoglLinesGeometry CreateLineSegments(IEnumerable<ILineSegment3D> lineSegmentsList)
        {
            var geometryData = GraphicsLinesGeometry3D.Create(lineSegmentsList);

            return new XeoglLinesGeometry(geometryData);
        }


        public IGraphicsLinesGeometry3D GraphicsLinesGeometry { get; set; }

        public override IGraphicsGeometry3D GraphicsGeometry
            => GraphicsLinesGeometry;


        public XeoglLinesGeometry()
        {
        }

        public XeoglLinesGeometry(IGraphicsLinesGeometry3D geometryData)
        {
            GraphicsLinesGeometry = geometryData;
        }


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetAttributeValue("positions", GraphicsLinesGeometry.VertexPoints.ToXeoglNumbersArrayText(" // Vertex Position "), "[]")
                .SetAttributeValue("indices", GraphicsLinesGeometry.VertexIndices.ToXeoglNumbersArrayText(), "[]");
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