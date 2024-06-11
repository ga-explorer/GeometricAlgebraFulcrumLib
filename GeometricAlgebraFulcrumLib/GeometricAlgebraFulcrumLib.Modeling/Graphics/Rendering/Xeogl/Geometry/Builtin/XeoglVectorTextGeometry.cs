using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Geometry.Builtin;

/// <summary>
/// A VectorTextGeometry extends Geometry to define vector text
/// geometry for attached Meshes.
/// http://xeogl.org/docs/classes/VectorTextGeometry.html
/// </summary>
public sealed class XeoglVectorTextGeometry : XeoglGeometry
{
    public LinFloat64Vector3DComposer Origin { get; }
        = LinFloat64Vector3DComposer.Create();

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