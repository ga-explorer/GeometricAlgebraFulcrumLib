using NumericalGeometryLib.BasicMath.Tuples.Mutable;
using TextComposerLib;
using TextComposerLib.Code.JavaScript;

namespace GraphicsComposerLib.Rendering.Xeogl.Geometry.Builtin
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


        public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
        {
            base.UpdateConstructorAttributes(composer);

            composer
                .SetNumbersArrayValue("origin", Origin)
                .SetValue("size", Size, 1)
                .SetTextValue("text", Text.ValueToLiteral(), "\"\"");
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