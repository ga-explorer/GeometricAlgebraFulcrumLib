using CodeComposerLib.MathML.Values.Color;

namespace CodeComposerLib.MathML.Elements.Layout.Tabular
{
    public sealed class MathMlTableCell
        : MathMlLayoutListElement<IMathMlElement>
    {
        public override string XmlTagName 
            => "mtd";

        public MathMlColorValue BackgroundColor { get; set; }
            = MathMlColorValue.Empty;

        public MathMlColorValue TextColor { get; set; }
            = MathMlColorValue.Empty;


        internal MathMlTableCell()
        {
        }


        internal override void UpdateAttributesComposer(MathMlAttributesComposer composer)
        {
            base.UpdateAttributesComposer(composer);

            composer
                .SetAttributeValue("mathcolor", TextColor)
                .SetAttributeValue("mathbackground", BackgroundColor);
        }
    }
}
