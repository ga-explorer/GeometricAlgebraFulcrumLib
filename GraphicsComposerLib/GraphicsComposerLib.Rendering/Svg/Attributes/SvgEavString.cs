using GraphicsComposerLib.Rendering.Svg.Elements;
using TextComposerLib.Text;

namespace GraphicsComposerLib.Rendering.Svg.Attributes
{
    public sealed class SvgEavString<TParentElement>
        : SvgElementAttributeValue<TParentElement> where TParentElement : SvgElement
    {
        protected override string ValueComputedText => string.Empty;


        internal SvgEavString(TParentElement parentElement, SvgAttributeInfo attributeInfo)
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

        //public TParentElement SetTo(params string[] values)
        //{
        //    ValueStoredText = values?.Concatenate(" ") ?? string.Empty;

        //    return ParentElement;
        //}

        public TParentElement SetTo(IEnumerable<string> values)
        {
            ValueStoredText = values?.Concatenate(" ") ?? string.Empty;

            return ParentElement;
        }
    }
}