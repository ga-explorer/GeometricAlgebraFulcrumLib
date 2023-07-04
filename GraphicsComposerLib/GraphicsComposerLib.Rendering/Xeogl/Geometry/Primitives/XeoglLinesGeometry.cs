using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.MathBase.Graphics.Primitives.Lines;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Geometry.Primitives
{
    public sealed class XeoglLinesGeometry : XeoglPrimitivesGeometry
    {
        public static XeoglLinesGeometry CreateLineSegment(IFloat64Vector3D point1, IFloat64Vector3D point2)
        {
            var geometryData = GrLineGeometry3D.Create(point1, point2);

            return new XeoglLinesGeometry(geometryData);
        }

        public static XeoglLinesGeometry CreateLineSegment(ILineSegment3D lineSegment)
        {
            var geometryData = GrLineGeometry3D.Create(lineSegment);

            return new XeoglLinesGeometry(geometryData);
        }

        public static XeoglLinesGeometry CreateLineSegments(params ILineSegment3D[] lineSegmentsList)
        {
            var geometryData = GrLineGeometry3D.Create(lineSegmentsList);

            return new XeoglLinesGeometry(geometryData);
        }

        public static XeoglLinesGeometry CreateLineSegments(IEnumerable<ILineSegment3D> lineSegmentsList)
        {
            var geometryData = GrLineGeometry3D.Create(lineSegmentsList);

            return new XeoglLinesGeometry(geometryData);
        }


        public IGraphicsLineGeometry3D GraphicsLinesGeometry { get; set; }

        public override IGraphicsPrimitiveGeometry3D GraphicsGeometry
            => GraphicsLinesGeometry;


        public XeoglLinesGeometry()
        {
        }

        public XeoglLinesGeometry(IGraphicsLineGeometry3D geometryData)
        {
            GraphicsLinesGeometry = geometryData;
        }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetTextValue("positions", GraphicsLinesGeometry.GeometryPoints.ToJavaScriptNumbersArrayText(" // Vertex Position "), "[]")
                .SetTextValue("indices", GraphicsLinesGeometry.GeometryIndices.ToJavaScriptNumbersArrayText(), "[]");
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