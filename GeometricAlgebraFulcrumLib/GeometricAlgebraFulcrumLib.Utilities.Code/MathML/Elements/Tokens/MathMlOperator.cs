using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Size;
using GeometricAlgebraFulcrumLib.Utilities.Text.Text.Literals;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Tokens;

/// <summary>
/// https://developer.mozilla.org/en-US/docs/Web/MathML/Element/mo
/// </summary>
public sealed class MathMlOperator : MathMlTextTokenElement
{
    public static MathMlOperator PlusSign { get; }
        = new MathMlOperator() { Text = "+" };

    public static MathMlOperator MinusSign { get; }
        = new MathMlOperator() { Text = HtmlLiterals.MathMinusSign };

    public static MathMlOperator Divide { get; }
        = new MathMlOperator() { Text = "/" };


    public static MathMlOperator Create()
    {
        return new MathMlOperator();
    }

    public static MathMlOperator Create(string text)
    {
        return new MathMlOperator()
        {
            Text = text
        };
    }


    public override string XmlTagName => "mo";

    public MathMlBoolean IsAccent { get; set; }
        = MathMlBoolean.Empty;

    public MathMlBoolean IsFence { get; set; }
        = MathMlBoolean.Empty;

    public MathMlBoolean IsSeparator { get; set; }
        = MathMlBoolean.Empty;

    public MathMlBoolean IsStretchy { get; set; }
        = MathMlBoolean.Empty;

    public MathMlBoolean IsSymmetric { get; set; }
        = MathMlBoolean.Empty;

    public MathMlBoolean IsLargeOperator { get; set; }
        = MathMlBoolean.Empty;

    public MathMlBoolean HasMovableLimits { get; set; }
        = MathMlBoolean.Empty;

    public MathMlOperatorForm Form { get; set; }
        = MathMlOperatorForm.Empty;

    public MathMlLengthValue LeftSpace { get; set; }
        = MathMlLengthValue.Empty;

    public MathMlLengthValue RightSpace { get; set; }
        = MathMlLengthValue.Empty;


    internal MathMlOperator()
    {
    }


    internal override void UpdateAttributesComposer(MathMlAttributesComposer composer)
    {
        base.UpdateAttributesComposer(composer);

        composer
            .SetAttributeValue("accent", IsAccent)
            .SetAttributeValue("fence", IsFence)
            .SetAttributeValue("separator", IsSeparator)
            .SetAttributeValue("stretchy", IsStretchy)
            .SetAttributeValue("symmetric", IsSymmetric)
            .SetAttributeValue("movablelimits", HasMovableLimits)
            .SetAttributeValue("form", Form)
            .SetAttributeValue("largeop", IsLargeOperator)
            .SetAttributeValue("lspace", LeftSpace)
            .SetAttributeValue("rspace", RightSpace);
    }
}