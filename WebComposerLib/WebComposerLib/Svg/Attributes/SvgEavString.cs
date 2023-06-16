using TextComposerLib.Text;
using WebComposerLib.Svg.Elements;

namespace WebComposerLib.Svg.Attributes
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
            throw new NotImplementedException();
        }

        public override ISvgAttributeValue UpdateFrom(ISvgAttributeValue sourceAttributeValue)
        {
            throw new NotImplementedException();
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