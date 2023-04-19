using GraphicsComposerLib.Rendering.Svg.Elements;
using GraphicsComposerLib.Rendering.Svg.Values;

namespace GraphicsComposerLib.Rendering.Svg.Attributes
{
    public sealed class SvgEav<TValueType, TParentElement> : 
        SvgElementAttributeValue<TParentElement>
            where TParentElement : SvgElement
            where TValueType : ISvgValue
    {
        //TODO: Implement default value computation based on parent element type
        private TValueType _value;
        public TValueType Value
        {
            get => _value;
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
            throw new NotImplementedException();
        }

        public override ISvgAttributeValue UpdateFrom(ISvgAttributeValue sourceAttributeValue)
        {
            throw new NotImplementedException();
        }

        public TParentElement SetTo(TValueType value)
        {
            Value = value;

            return ParentElement;
        }
    }
}
