using GraphicsComposerLib.Rendering.Svg.Elements;
using GraphicsComposerLib.Rendering.Svg.Values;

namespace GraphicsComposerLib.Rendering.Svg.Attributes
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
            throw new System.NotImplementedException();
        }

        public override ISvgAttributeValue UpdateFrom(ISvgAttributeValue sourceAttributeValue)
        {
            throw new System.NotImplementedException();
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