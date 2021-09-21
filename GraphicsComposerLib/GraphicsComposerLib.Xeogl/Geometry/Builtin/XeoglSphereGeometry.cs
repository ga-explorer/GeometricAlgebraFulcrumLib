using EuclideanGeometryLib.BasicMath.Tuples;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;
using EuclideanGeometryLib.BasicMath.Tuples.Mutable;
using EuclideanGeometryLib.GraphicsGeometry;

namespace GraphicsComposerLib.Xeogl.Geometry.Builtin
{
    /// <summary>
    /// A SphereGeometry is a parameterized Geometry that defines a
    /// sphere-shaped mesh for attached Meshes.
    /// http://xeogl.org/docs/classes/SphereGeometry.html
    /// </summary>
    public sealed class XeoglSphereGeometry : XeoglBuiltinSolidGeometry
    {
        public static XeoglSphereGeometry Create(double radius)
            => new XeoglSphereGeometry()
            {
                Radius = radius
            };

        public static XeoglSphereGeometry Create(double radius, int widthSegments, int heightSegments)
            => new XeoglSphereGeometry()
            {
                Radius = radius,
                WidthSegments = widthSegments,
                HeightSegments = heightSegments
            };

        public static XeoglSphereGeometry Create(ITuple3D center, double radius)
            => new XeoglSphereGeometry(center)
            {
                Radius = radius
            };

        public static XeoglSphereGeometry Create(ITuple3D center, double radius, int widthSegments, int heightSegments)
            => new XeoglSphereGeometry(center)
            {
                Radius = radius,
                WidthSegments = widthSegments,
                HeightSegments = heightSegments
            };


        public MutableTuple3D Center { get; }
            = new MutableTuple3D();

        public double Radius { get; set; } = 1;

        public int WidthSegments { get; set; } = 18;

        public int HeightSegments { get; set; } = 24;

        public double LevelOfDetail { get; set; } = 1;

        public override string JavaScriptClassName => "SphereGeometry";


        public XeoglSphereGeometry()
        {
        }

        public XeoglSphereGeometry(ITuple3D center)
        {
            Center.SetTuple(center);
        }


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetAttributeValue("center", Center, Tuple3D.Zero);

            composer
                .SetAttributeValue("radius", Radius, 1);

            composer
                .SetAttributeValue("widthSegments", WidthSegments, 18)
                .SetAttributeValue("heightSegments", HeightSegments, 24)
                .SetAttributeValue("lod", LevelOfDetail, 1);
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