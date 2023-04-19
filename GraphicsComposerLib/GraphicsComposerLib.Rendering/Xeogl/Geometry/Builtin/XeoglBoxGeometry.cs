using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space3D;
using GraphicsComposerLib.Geometry.Primitives;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Geometry.Builtin
{
    /// <summary>
    /// A BoxGeometry is a parameterized Geometry that defines a
    /// box-shaped mesh for attached Meshes.
    /// http://xeogl.org/docs/classes/BoxGeometry.html
    /// </summary>
    public sealed class XeoglBoxGeometry : XeoglBuiltinSolidGeometry
    {
        public static XeoglBoxGeometry Create(double halfSize)
            => new XeoglBoxGeometry(halfSize);

        public static XeoglBoxGeometry Create(IFloat64Tuple3D halfSize)
            => new XeoglBoxGeometry(halfSize);

        public static XeoglBoxGeometry Create(IFloat64Tuple3D center, double halfSize)
            => new XeoglBoxGeometry(center, halfSize);

        public static XeoglBoxGeometry Create(IFloat64Tuple3D center, IFloat64Tuple3D halfSize)
            => new XeoglBoxGeometry(center, halfSize);

        public static XeoglBoxGeometry Create(IBoundingBox3D box)
            => new XeoglBoxGeometry(box);


        public MutableFloat64Tuple3D Center { get; }
            = new MutableFloat64Tuple3D();

        public MutableFloat64Tuple3D HalfSize { get; }
            = new MutableFloat64Tuple3D();

        public override string JavaScriptClassName => "BoxGeometry";


        public XeoglBoxGeometry()
        {
        }

        public XeoglBoxGeometry(double halfSize)
        {
            HalfSize.SetTuple(halfSize, halfSize, halfSize);
        }

        public XeoglBoxGeometry(IFloat64Tuple3D halfSize)
        {
            HalfSize.SetTuple(halfSize);
        }

        public XeoglBoxGeometry(IFloat64Tuple3D center, double halfSize)
        {
            Center.SetTuple(center);
            HalfSize.SetTuple(halfSize, halfSize, halfSize);
        }

        public XeoglBoxGeometry(IFloat64Tuple3D center, IFloat64Tuple3D halfSize)
        {
            Center.SetTuple(center);
            HalfSize.SetTuple(halfSize);
        }

        public XeoglBoxGeometry(IBoundingBox3D box)
        {
            Center.SetTuple(box.GetMidPoint());
            HalfSize.SetTuple(box.GetSideHalfLengths());
        }


        public XeoglBoxGeometry SetTo(IBoundingBox3D box)
        {
            Center.SetTuple(box.GetMidPoint());

            HalfSize.SetTuple(box.GetSideHalfLengths());

            return this;
        }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetNumbersArrayValue("center", Center, Float64Tuple3D.Zero)
                .SetValue("xSize", HalfSize.X, 1)
                .SetValue("ySize", HalfSize.Y, 1)
                .SetValue("zSize", HalfSize.Z, 1);
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
