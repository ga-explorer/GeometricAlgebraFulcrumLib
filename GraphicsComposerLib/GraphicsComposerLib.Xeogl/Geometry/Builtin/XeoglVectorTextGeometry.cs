using EuclideanGeometryLib.BasicMath.Tuples.Mutable;
using TextComposerLib;

namespace GraphicsComposerLib.Xeogl.Geometry.Builtin
{
    /// <summary>
    /// A VectorTextGeometry extends Geometry to define vector text
    /// geometry for attached Meshes.
    /// http://xeogl.org/docs/classes/VectorTextGeometry.html
    /// </summary>
    public sealed class XeoglVectorTextGeometry : XeoglGeometry
    {
        public MutableTuple3D Origin { get; }
            = new MutableTuple3D();

        public double Size { get; set; } = 1;

        public string Text { get; set; } = string.Empty;

        public override string JavaScriptClassName => "VectorTextGeometry";


        internal override void UpdateAttributesComposer(XeoglCodeComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("origin", Origin)
                .SetAttributeValue("size", Size, 1)
                .SetAttributeValue("text", Text.ValueToLiteral(), "\"\"");
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