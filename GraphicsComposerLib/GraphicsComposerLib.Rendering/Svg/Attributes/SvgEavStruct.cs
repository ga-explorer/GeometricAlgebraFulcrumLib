using GraphicsComposerLib.Rendering.Svg.Elements;

namespace GraphicsComposerLib.Rendering.Svg.Attributes
{
    public sealed class SvgEavStruct<TValue, TParentElement>
        : SvgElementAttributeValue<TParentElement> 
        where TParentElement : SvgElement 
        where TValue : struct 
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

        protected override string ValueComputedText => _value.ToString();


        internal SvgEavStruct(TParentElement parentElement, SvgAttributeInfo attributeInfo)
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

        public TParentElement SetTo(TValue value)
        {
            Value = value;

            return ParentElement;
        }
    }
}