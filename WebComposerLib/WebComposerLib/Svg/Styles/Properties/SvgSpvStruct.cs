using WebComposerLib.Svg.Attributes;

namespace WebComposerLib.Svg.Styles.Properties
{
    public sealed class SvgSpvStruct<TValue> : SvgStylePropertyValue where TValue : struct
    {
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
            => _value.ToString();


        internal SvgSpvStruct(SvgStyle parentStyle, SvgAttributeInfo attributeInfo)
            : base(parentStyle, attributeInfo)
        {
        }


        public override SvgStylePropertyValue CreateCopy()
        {
            var result = new SvgSpvStruct<TValue>(ParentStyle, AttributeInfo);

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
            var source = sourcePropertyValue as SvgSpvStruct<TValue>;

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