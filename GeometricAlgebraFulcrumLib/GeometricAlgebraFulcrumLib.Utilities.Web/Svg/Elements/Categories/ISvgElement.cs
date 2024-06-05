using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Content;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Elements.Categories;

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