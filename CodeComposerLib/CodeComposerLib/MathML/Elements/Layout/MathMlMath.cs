using System.Collections.Generic;
using CodeComposerLib.MathML.Constants;
using CodeComposerLib.MathML.Values.Color;

namespace CodeComposerLib.MathML.Elements.Layout
{
    /// <summary>
    /// https://developer.mozilla.org/en-US/docs/Web/MathML/Element/math
    /// </summary>
    public sealed class MathMlMath
        : MathMlLayoutListElement<IMathMlElement>
    {
        public static MathMlMath Create()
        {
            return new MathMlMath();
        }

        public static MathMlMath Create(IEnumerable<IMathMlElement> contents)
        {
            var mathElement = new MathMlMath();

            mathElement.AppendElements(contents);

            return mathElement;
        }


        public override string XmlTagName 
            => "math";

        public MathMlTextDirection TextDirection { get; set; }
            = MathMlTextDirection.Empty;

        public MathMlColorValue BackgroundColor { get; set; }
            = MathMlColorValue.Empty;

        public MathMlColorValue TextColor { get; set; }
            = MathMlColorValue.Empty;

        public MathMlDisplay Display { get; set; }
            = MathMlDisplay.Empty;

        public MathMlOverflow Overflow { get; set; }
            = MathMlOverflow.Empty;


        internal MathMlMath()
        {
        }


        internal override void UpdateAttributesComposer(MathMlAttributesComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("dir", TextDirection)
                .SetAttributeValue("mathcolor", TextColor)
                .SetAttributeValue("mathbackground", BackgroundColor)
                .SetAttributeValue("display", Display)
                .SetAttributeValue("overflow", Overflow);
        }
    }
}
