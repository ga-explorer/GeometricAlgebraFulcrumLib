using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Content;

namespace WebComposerLib.Svg.Elements.Categories
{
    /// <summary>
    /// http://docs.w3cub.com/svg/element/
    /// </summary>
    public interface ISvgElement : ISvgContent
    {
        SvgContentsList Contents { get; }

        string ElementName { get; }

        string Id { get; }

        string ContentsText { get; }

        string AttributesText { get; }

        string BeginEndTagText { get; }

        string BeginTagText { get; }

        string EndTagText { get; }

        string TagText { get; }

        ISvgElement ClearAttributes();

        ISvgElement ClearAttribute(SvgAttributeInfo attributeInfo);

        ISvgElement ClearAttributes(params SvgAttributeInfo[] attributeInfoList);

        ISvgElement ClearDefaultAttributes(bool clearInChildren);
    }
}