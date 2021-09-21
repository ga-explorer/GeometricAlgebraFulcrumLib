using System.Collections.Generic;
using GraphicsComposerLib.Svg.Attributes;
using TextComposerLib.Text;

namespace GraphicsComposerLib.Svg.Styles.Properties
{
    public sealed class SvgSpvString : SvgStylePropertyValue
    {
        protected override string ValueComputedText 
            => string.Empty;


        internal SvgSpvString(SvgStyle parentElement, SvgAttributeInfo attributeInfo)
            : base(parentElement, attributeInfo)
        {
        }


        public override SvgStylePropertyValue CreateCopy()
        {
            return new SvgSpvString(ParentStyle, AttributeInfo)
            {
                ValueStoredText = ValueStoredText
            };
        }

        public override SvgStylePropertyValue UpdateFrom(SvgStylePropertyValue sourcePropertyValue)
        {
            ValueStoredText = sourcePropertyValue?.ValueText;

            return this;
        }

        public SvgStyle SetTo(params string[] values)
        {
            ValueStoredText = values?.Concatenate(" ") ?? string.Empty;

            return ParentStyle;
        }

        public SvgStyle SetTo(IEnumerable<string> values)
        {
            ValueStoredText = values?.Concatenate(" ") ?? string.Empty;

            return ParentStyle;
        }
    }
}