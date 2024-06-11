using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;
using GeometricAlgebraFulcrumLib.Utilities.Web.Colors;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.Xeogl.Materials;

/// <summary>
/// http://xeogl.org/docs/classes/Fresnel.html
/// </summary>
public sealed class XeoglFresnelEffect : XeoglComponent
{
    public override string JavaScriptClassName => "Fresnel";

    public Color EdgeColor { get; set; } = Color.Black;

    public Color CenterColor { get; set; } = Color.White;

    public double EdgeBias { get; set; } = 0;

    public double CenterBias { get; set; } = 1;

    public double Power { get; set; }


    public override void UpdateConstructorAttributes(JavaScriptAttributesDictionary composer)
    {
        base.UpdateConstructorAttributes(composer);

        composer
            .SetRgbNumbersArrayValue("cfg.edgeColor", EdgeColor.ToSystemDrawingColor(), Color.Black.ToSystemDrawingColor())
            .SetRgbNumbersArrayValue("cfg.centerColor", CenterColor.ToSystemDrawingColor(), Color.White.ToSystemDrawingColor())
            .SetValue("edgeBias", EdgeBias, 0)
            .SetValue("centerBias", CenterBias, 1)
            .SetValue("power", Power, 0);
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