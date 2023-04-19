using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
using GraphicsComposerLib.Geometry.Primitives;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Geometry.Builtin
{
    /// <summary>
    /// A PlaneGeometry is a parameterized Geometry that defines a
    /// plane-shaped mesh for attached Meshes.
    /// http://xeogl.org/docs/classes/PlaneGeometry.html
    /// </summary>
    public sealed class XeoglPlaneGeometry : XeoglBuiltinSolidGeometry
    {
        public static XeoglPlaneGeometry Create(double size, int segments)
        {
            return new XeoglPlaneGeometry()
            {
                XSize = size,
                ZSize = size,
                XSegments = segments,
                ZSegments = segments
            };
        }

        public static XeoglPlaneGeometry Create(double size, int xSegments, int zSegments)
        {
            return new XeoglPlaneGeometry()
            {
                XSize = size,
                ZSize = size,
                XSegments = xSegments,
                ZSegments = zSegments
            };
        }

        public static XeoglPlaneGeometry Create(IFloat64Tuple2D size, int segments)
        {
            return new XeoglPlaneGeometry()
            {
                XSize = size.X,
                ZSize = size.Y,
                XSegments = segments,
                ZSegments = segments
            };
        }

        public static XeoglPlaneGeometry Create(IFloat64Tuple2D size, int xSegments, int zSegments)
        {
            return new XeoglPlaneGeometry()
            {
                XSize = size.X,
                ZSize = size.Y,
                XSegments = xSegments,
                ZSegments = zSegments
            };
        }

        public static XeoglPlaneGeometry Create(IFloat64Tuple3D center, double size, int segments)
        {
            return new XeoglPlaneGeometry(center)
            {
                XSize = size,
                ZSize = size,
                XSegments = segments,
                ZSegments = segments
            };
        }

        public static XeoglPlaneGeometry Create(IFloat64Tuple3D center, double size, int xSegments, int zSegments)
        {
            return new XeoglPlaneGeometry(center)
            {
                XSize = size,
                ZSize = size,
                XSegments = xSegments,
                ZSegments = zSegments
            };
        }

        public static XeoglPlaneGeometry Create(IFloat64Tuple3D center, IFloat64Tuple2D size, int segments)
        {
            return new XeoglPlaneGeometry(center)
            {
                XSize = size.X,
                ZSize = size.Y,
                XSegments = segments,
                ZSegments = segments
            };
        }

        public static XeoglPlaneGeometry Create(IFloat64Tuple3D center, IFloat64Tuple2D size, int xSegments, int zSegments)
        {
            return new XeoglPlaneGeometry(center)
            {
                XSize = size.X,
                ZSize = size.Y,
                XSegments = xSegments,
                ZSegments = zSegments
            };
        }


        public MutableFloat64Tuple3D Center { get; }
            = new MutableFloat64Tuple3D();

        public double XSize { get; set; } = 1;

        public double ZSize { get; set; } = 1;

        public int XSegments { get; set; } = 1;

        public int ZSegments { get; set; } = 1;

        public override string JavaScriptClassName => "PlaneGeometry";


        public XeoglPlaneGeometry()
        {
        }

        public XeoglPlaneGeometry(IFloat64Tuple3D center)
        {
            Center.SetTuple(center);
        }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetNumbersArrayValue("center", Center, Float64Tuple3D.Zero)
                .SetValue("xSize", XSize, 1)
                .SetValue("ZSize", ZSize, 1)
                .SetValue("xSegments", XSegments, 1)
                .SetValue("zSegments", ZSegments, 1);
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