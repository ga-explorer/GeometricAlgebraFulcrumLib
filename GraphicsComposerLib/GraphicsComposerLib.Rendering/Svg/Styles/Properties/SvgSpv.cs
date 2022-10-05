using GraphicsComposerLib.Rendering.Svg.Attributes;
using GraphicsComposerLib.Rendering.Svg.Values;

namespace GraphicsComposerLib.Rendering.Svg.Styles.Properties
{
    public sealed class SvgSpv<TValue> 
        : SvgStylePropertyValue 
        where TValue : SvgStoredValue //Stored values are immutable, so there is no problem in directly copying them
    {
        //TODO: Implement default value computation based on parent element type
        private TValue _value;
        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                IsValueComputed = true;
            }
        }

        protected override string ValueComputedText 
            => Value?.ValueText ?? string.Empty;


        internal SvgSpv(SvgStyle parentStyle, SvgAttributeInfo attributeInfo)
            : base(parentStyle, attributeInfo)
        {
        }


        public override SvgStylePropertyValue CreateCopy()
        {
            var result = new SvgSpv<TValue>(ParentStyle, AttributeInfo);

            if (IsValueStored)
            {
                result._value = _value;
                result.ValueStoredText = ValueStoredText;

                return result;
            }

            result.Value = Value;

            return result;
        }

        public override SvgStylePropertyValue UpdateFrom(SvgStylePropertyValue sourcePropertyValue)
        {
            var source = sourcePropertyValue as SvgSpv<TValue>;

            if (ReferenceEquals(source, null) || source.IsValueStored)
            {
                ValueStoredText = source?.ValueStoredText;

                return this;
            }

            Value = source.Value;

            return this;
        }

        public SvgStyle SetTo(TValue value)
        {
            Value = value;

            return ParentStyle;
        }
    }
}