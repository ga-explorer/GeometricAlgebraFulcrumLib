using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Color;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Tokens;

public abstract class MathMlTokenElement 
    : MathMlElement, IMathMlTokenElement
{
    public override bool IsToken 
        => true;

    public MathMlColorValue BackgroundColor { get; set; }
        = MathMlColorValue.Empty;


    internal override void UpdateAttributesComposer(MathMlAttributesComposer composer)
    {
        base.UpdateAttributesComposer(composer);

        composer.SetAttributeValue("mathbackground", BackgroundColor);
    }
}