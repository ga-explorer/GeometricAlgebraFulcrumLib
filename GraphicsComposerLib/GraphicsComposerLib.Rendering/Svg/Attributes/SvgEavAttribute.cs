using GraphicsComposerLib.Rendering.Svg.Elements;

namespace GraphicsComposerLib.Rendering.Svg.Attributes
{
    public sealed class SvgEavAttribute<TParentElement>
        : SvgElementAttributeValue<TParentElement> where TParentElement : SvgElement
    {
        private SvgAttributeInfo _attribute;
        public SvgAttributeInfo Attribute
        {
            get => _attribute;
            set
            {
                _attribute = value;
                IsValueComputed = true;
            }
        }

        protected override string ValueComputedText => _attribute.Name;


        internal SvgEavAttribute(TParentElement parentElement)
            : base(parentElement, SvgAttributeUtils.AttributeName)
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

        public TParentElement SetTo(SvgAttributeInfo value)
        {
            Attribute = value;

            return ParentElement;
        }
    }
}