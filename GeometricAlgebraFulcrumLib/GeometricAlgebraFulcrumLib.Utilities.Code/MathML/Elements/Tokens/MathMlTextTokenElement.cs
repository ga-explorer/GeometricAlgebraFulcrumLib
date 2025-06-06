﻿using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Constants;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Color;
using GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Values.Size;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Tokens;

public abstract class MathMlTextTokenElement 
    : MathMlTokenElement
{
    public string Text { get; set; }
        = string.Empty;

    public override string ContentsText 
        => Text;

    public MathMlTextDirection TextDirection { get; set; }
        = MathMlTextDirection.Empty;

    public MathMlColorValue TextColor { get; set; }
        = MathMlColorValue.Empty;

    public MathMlLengthValue TextSize { get; set; }
        = MathMlLengthValue.Empty;

    public MathMlTextVariant TextVariant { get; set; }
        = MathMlTextVariant.Empty;


    internal override void UpdateAttributesComposer(MathMlAttributesComposer composer)
    {
        base.UpdateAttributesComposer(composer);

        composer
            .SetAttributeValue("dir", TextDirection)
            .SetAttributeValue("mathcolor", TextColor)
            .SetAttributeValue("mathvariant", TextVariant)
            .SetAttributeValue("mathsize", TextSize);
    }

}