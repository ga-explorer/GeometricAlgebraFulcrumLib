using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Color;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Layout.Elementary;

public sealed class MathMlRow
    : MathMlLayoutListElement<IMathMlElement>
{
    public static MathMlRow Create()
    {
        return new MathMlRow();
    }

    public static MathMlRow Create(IEnumerable<IMathMlElement> contents)
    {
        var row = new MathMlRow();

        row.AppendElements(contents);

        return row;
    }


    public override string XmlTagName 
        => "mrow";

    public MathMlTextDirection TextDirection { get; set; }
        = MathMlTextDirection.Empty;

    public MathMlColorValue BackgroundColor { get; set; }
        = MathMlColorValue.Empty;

    public MathMlColorValue TextColor { get; set; }
        = MathMlColorValue.Empty;


    internal MathMlRow()
    {
    }


    internal override void UpdateAttributesComposer(MathMlAttributesComposer composer)
    {
        base.UpdateAttributesComposer(composer);

        composer
            .SetAttributeValue("dir", TextDirection)
            .SetAttributeValue("mathcolor", TextColor)
            .SetAttributeValue("mathbackground", BackgroundColor);
    }
}