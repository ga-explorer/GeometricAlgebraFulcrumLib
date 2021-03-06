using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Styles.Properties
{
    public sealed class SvgSpvLength : SvgStylePropertyValue
    {
        private double _length;
        public double Length
        {
            get { return _length; }
            set
            {
                _length = value;
                IsValueComputed = true;
            }
        }

        private SvgValueLengthUnit _unit = SvgValueLengthUnit.None;
        public SvgValueLengthUnit Unit
        {
            get { return _unit; }
            set
            {
                _unit = value ?? SvgValueLengthUnit.None;
                IsValueComputed = true;
            }
        }

        protected override string ValueComputedText
            => _length.ToSvgLengthText(_unit);


        internal SvgSpvLength(SvgStyle parentElement, SvgAttributeInfo attributeInfo)
            : base(parentElement, attributeInfo)
        {
        }


        public override SvgStylePropertyValue CreateCopy()
        {
            var result = new SvgSpvLength(ParentStyle, AttributeInfo);

            if (IsValueStored)
            {
                result._length = _length;
                result._unit = _unit;
                result.ValueStoredText = ValueStoredText;

                return result;
            }

            result.Length = Length;
            result.Unit = Unit;

            return result;
        }

        public override SvgStylePropertyValue UpdateFrom(SvgStylePropertyValue sourcePropertyValue)
        {
            var source = sourcePropertyValue as SvgSpvLength;

            if (ReferenceEquals(source, null) || source.IsValueStored)
            {
                ValueStoredText = source?.ValueStoredText;

                return this;
            }

            Length = source.Length;
            Unit = source.Unit;

            return this;
        }

        public SvgStyle SetTo(double length)
        {
            Length = length;
            Unit = SvgValueLengthUnit.None;

            return ParentStyle;
        }

        public SvgStyle SetTo(double length, SvgValueLengthUnit unit)
        {
            Length = length;
            Unit = unit;

            return ParentStyle;
        }
    }
}