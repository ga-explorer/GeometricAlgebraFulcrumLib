using GraphicsComposerLib.Geometry.Primitives;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Geometry.Builtin
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


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetNumbersArrayValue("center", Center, Tuple3D.Zero);

            composer
                .SetValue("radius", Radius, 1);

            composer
                .SetValue("widthSegments", WidthSegments, 18)
                .SetValue("heightSegments", HeightSegments, 24)
                .SetValue("lod", LevelOfDetail, 1);
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