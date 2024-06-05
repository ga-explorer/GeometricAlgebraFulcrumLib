using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Size;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Tokens;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/MathML/Element/mspace
/// </summary>
public sealed class MathMlSpace : MathMlNonTextTokenElement
{
    public static MathMlSpace Create()
    {
        return new MathMlSpace();
    }


    public override string XmlTagName 
        => "mspace";

    public override string ContentsText 
        => string.Empty;

    public MathMlLengthValue Depth { get; set; }
        = MathMlLengthValue.Empty;

    public MathMlLengthValue Width { get; set; }
        = MathMlLengthValue.Empty;

    public MathMlLengthValue Height { get; set; }
        = MathMlLengthValue.Empty;


    internal MathMlSpace()
    {
    }


    internal override void UpdateAttributesComposer(MathMlAttributesComposer composer)
    {
        base.UpdateAttributesComposer(composer);

        composer
            .SetAttributeValue("depth", Depth)
            .SetAttributeValue("width", Width)
            .SetAttributeValue("height", Height);
    }
}