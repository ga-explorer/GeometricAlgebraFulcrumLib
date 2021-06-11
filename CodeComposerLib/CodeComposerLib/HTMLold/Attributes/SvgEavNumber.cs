using CodeComposerLib.HTMLold.Elements;

namespace CodeComposerLib.HTMLold.Attributes
{
    public sealed class HtmlEavNumber<TParentElement>
        : HtmlElementAttributeValue<TParentElement> where TParentElement : HtmlElement
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
            => _number.ToHtmlNumberText(IsPercent);


        internal HtmlEavNumber(TParentElement parentElement, HtmlAttributeInfo attributeInfo)
            : base(parentElement, attributeInfo)
        {
        }


        public override IHtmlAttributeValue CreateCopy()
        {
            throw new System.NotImplementedException();
        }

        public override IHtmlAttributeValue UpdateFrom(IHtmlAttributeValue sourceAttributeValue)
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