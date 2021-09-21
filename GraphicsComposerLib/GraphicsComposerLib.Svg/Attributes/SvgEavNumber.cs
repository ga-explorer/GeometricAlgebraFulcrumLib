using GraphicsComposerLib.Svg.Elements;

namespace GraphicsComposerLib.Svg.Attributes
{
    public sealed class SvgEavNumber<TParentElement>
        : SvgElementAttributeValue<TParentElement> where TParentElement : SvgElement
    {
        private double _number;
        public double Number
        {
            get { return _number; }
            set
            {
                _number = value;
                IsValueComputed = true;
            }
        }

        private bool _isPercent;
        public bool IsPercent
        {
            get { return _isPercent; }
            set
            {
                _isPercent = value;
                IsValueComputed = true;
            }
        }

        protected override string ValueComputedText
            => _number.ToSvgNumberText(IsPercent);


        internal SvgEavNumber(TParentElement parentElement, SvgAttributeInfo attributeInfo)
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

        public TParentElement SetToNumber(double number)
        {
            Number = number;

            return ParentElement;
        }

        public TParentElement SetToPercent(double number)
        {
            Number = number;
            IsPercent = true;

            return ParentElement;
        }

        public TParentElement SetToIndefinite()
        {
            ValueStoredText = "indefinite";

            return ParentElement;
        }
    }
}