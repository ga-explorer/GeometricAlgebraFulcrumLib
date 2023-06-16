using WebComposerLib.Html.Media;
using WebComposerLib.Svg.Elements;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Attributes
{
    public sealed class SvgEavLength<TParentElement>
        : SvgElementAttributeValue<TParentElement> where TParentElement : SvgElement
    {
        private double _length;
        public double Length
        {
            get => _length;
            set
            {
                _length = value;
                IsValueComputed = true;
            }
        }

        private SvgValueLengthUnit _unit = SvgValueLengthUnit.None;
        public SvgValueLengthUnit Unit
        {
            get => _unit;
            set
            {
                _unit = value ?? SvgValueLengthUnit.None;
                IsValueComputed = true;
            }
        }

        protected override string ValueComputedText
            => _length.ToSvgLengthText(_unit);


        internal SvgEavLength(TParentElement parentElement, SvgAttributeInfo attributeInfo)
            : base(parentElement, attributeInfo)
        {
        }


        public override ISvgAttributeValue CreateCopy()
        {
            throw new NotImplementedException();
        }

        public override ISvgAttributeValue UpdateFrom(ISvgAttributeValue sourceAttributeValue)
        {
            throw new NotImplementedException();
        }

        public TParentElement SetTo(double length)
        {
            Length = length;
            Unit = SvgValueLengthUnit.None;

            return ParentElement;
        }

        public TParentElement SetTo(double length, SvgValueLengthUnit unit)
        {
            Length = length;
            Unit = unit;

            return ParentElement;
        }
    }
}