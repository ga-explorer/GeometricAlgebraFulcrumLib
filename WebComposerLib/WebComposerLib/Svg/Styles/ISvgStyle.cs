using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Styles.Properties;

namespace WebComposerLib.Svg.Styles;

public interface ISvgStyle
{
    IEnumerable<SvgAttributeInfo> PropertyInfos { get; }

    IEnumerable<SvgAttributeInfo> ActivePropertyInfos { get; }

    IEnumerable<SvgStylePropertyValue> ActivePropertyValues { get; }

    string ActivePropertyValuesText { get; }

    SvgStyle BaseStyle { get; }

    bool IsSubStyle { get; }
}