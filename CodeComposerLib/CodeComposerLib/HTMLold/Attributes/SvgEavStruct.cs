using CodeComposerLib.HTMLold.Elements;

namespace CodeComposerLib.HTMLold.Attributes
{
    public sealed class HtmlEavStruct<TValue, TParentElement>
        : HtmlElementAttributeValue<TParentElement> 
        where TParentElement : HtmlElement 
        where TValue : struct 
    {
        private TValue _value;
        public TValue Value
        {
            get { return _value; }
            set
            {
                _value = value;
                IsValueComputed = true;
            }
        }

        protected override string ValueComputedText => _value.ToString();


        internal HtmlEavStruct(TParentElement parentElement, HtmlAttributeInfo attributeInfo)
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

        public TParentElement SetTo(TValue value)
        {
            Value = value;

            return ParentElement;
        }
    }
}