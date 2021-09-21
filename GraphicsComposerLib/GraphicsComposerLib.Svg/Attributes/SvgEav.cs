using GraphicsComposerLib.Svg.Elements;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Attributes
{
    public sealed class SvgEav<TValueType, TParentElement>
        : SvgElementAttributeValue<TParentElement>
        where TParentElement : SvgElement
        where TValueType : ISvgValue
    {
        //TODO: Implement default value computation based on parent element type
        private TValueType _value;
        public TValueType Value
        {
            get { return _value; }
            set
            {
                _value = value;
                IsValueComputed = true;
            }
        }

        protected override string ValueComputedText => Value?.ValueText ?? string.Empty;


        internal SvgEav(TParentElement parentElement, SvgAttributeInfo attributeInfo)
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

        public TParentElement SetTo(TValueType value)
        {
            Value = value;

            return ParentElement;
        }
    }
}
