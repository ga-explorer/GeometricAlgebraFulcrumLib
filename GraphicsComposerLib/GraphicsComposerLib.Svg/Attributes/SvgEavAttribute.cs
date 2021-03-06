using GraphicsComposerLib.Svg.Elements;

namespace GraphicsComposerLib.Svg.Attributes
{
    public sealed class SvgEavAttribute<TParentElement>
        : SvgElementAttributeValue<TParentElement> where TParentElement : SvgElement
    {
        private SvgAttributeInfo _attribute;
        public SvgAttributeInfo Attribute
        {
            get { return _attribute; }
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
            throw new System.NotImplementedException();
        }

        public override ISvgAttributeValue UpdateFrom(ISvgAttributeValue sourceAttributeValue)
        {
            throw new System.NotImplementedException();
        }

        public TParentElement SetTo(SvgAttributeInfo value)
        {
            Attribute = value;

            return ParentElement;
        }
    }
}