using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Color;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Layout.General;

public sealed class MathMlFenced 
    : MathMlLayoutListElement<IMathMlElement>
{
    public override string XmlTagName 
        => "mfenced";

    public string Open { get; set; }

    public string Close { get; set; }

    public string Separators { get; set; }

    public MathMlColorValue BackgroundColor { get; set; }
        = MathMlColorValue.Empty;

    public MathMlColorValue TextColor { get; set; }
        = MathMlColorValue.Empty;


    internal override void UpdateAttributesComposer(MathMlAttributesComposer composer)
    {
        base.UpdateAttributesComposer(composer);

        composer
            .SetAttributeValue("mathcolor", TextColor)
            .SetAttributeValue("mathbackground", BackgroundColor)
            .SetAttributeValue("open", Open.DoubleQuote(), "\"\"")
            .SetAttributeValue("close", Close.DoubleQuote(), "\"\"")
            .SetAttributeValue("separators", Separators.DoubleQuote(), "\"\"");
    }
}