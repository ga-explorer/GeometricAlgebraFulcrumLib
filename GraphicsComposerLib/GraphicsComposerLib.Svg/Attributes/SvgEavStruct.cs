using GraphicsComposerLib.Svg.Elements;

namespace GraphicsComposerLib.Svg.Attributes
{
    public sealed class SvgEavStruct<TValue, TParentElement>
        : SvgElementAttributeValue<TParentElement> 
        where TParentElement : SvgElement 
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


        internal SvgEavStruct(TParentElement parentElement, SvgAttributeInfo attributeInfo)
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

        public TParentElement SetTo(TValue value)
        {
            Value = value;

            return ParentElement;
        }
    }
}