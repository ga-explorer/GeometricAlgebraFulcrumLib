using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Primitives;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using TextComposerLib.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.Xeogl.Geometry.Builtin
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

        public static XeoglBoxGeometry Create(IFloat64Vector3D halfSize)
            => new XeoglBoxGeometry(halfSize);

        public static XeoglBoxGeometry Create(IFloat64Vector3D center, double halfSize)
            => new XeoglBoxGeometry(center, halfSize);

        public static XeoglBoxGeometry Create(IFloat64Vector3D center, IFloat64Vector3D halfSize)
            => new XeoglBoxGeometry(center, halfSize);

        public static XeoglBoxGeometry Create(IBoundingBox3D box)
            => new XeoglBoxGeometry(box);


        public Float64Vector3DComposer Center { get; }
            = Float64Vector3DComposer.Create();

        public Float64Vector3DComposer HalfSize { get; }
            = Float64Vector3DComposer.Create();

        public override string JavaScriptClassName => "BoxGeometry";


        public XeoglBoxGeometry()
        {
        }

        public XeoglBoxGeometry(double halfSize)
        {
            HalfSize.SetVector(halfSize, halfSize, halfSize);
        }

        public XeoglBoxGeometry(IFloat64Vector3D halfSize)
        {
            HalfSize.SetVector(halfSize);
        }

        public XeoglBoxGeometry(IFloat64Vector3D center, double halfSize)
        {
            Center.SetVector(center);
            HalfSize.SetVector(halfSize, halfSize, halfSize);
        }

        public XeoglBoxGeometry(IFloat64Vector3D center, IFloat64Vector3D halfSize)
        {
            Center.SetVector(center);
            HalfSize.SetVector(halfSize);
        }

        public XeoglBoxGeometry(IBoundingBox3D box)
        {
            Center.SetVector(box.GetMidPoint());
            HalfSize.SetVector(box.GetSideHalfLengths());
        }


        public XeoglBoxGeometry SetTo(IBoundingBox3D box)
        {
            Center.SetVector(box.GetMidPoint());

            HalfSize.SetVector(box.GetSideHalfLengths());

            return this;
        }


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetValue("primitive", PrimitiveType, GraphicsPrimitiveType3D.Triangles)
                .SetNumbersArrayValue("center", Center, Float64Vector3D.Zero)
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
