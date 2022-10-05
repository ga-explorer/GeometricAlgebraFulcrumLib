using GraphicsComposerLib.Rendering.Svg.Attributes;
using GraphicsComposerLib.Rendering.Svg.Styles.Properties;

namespace GraphicsComposerLib.Rendering.Svg.Styles
{
    public interface ISvgStyle
    {
        IEnumerable<SvgAttributeInfo> PropertyInfos { get; }

        IEnumerable<SvgAttributeInfo> ActivePropertyInfos { get; }

        IEnumerable<SvgStylePropertyValue> ActivePropertyValues { get; }

        string ActivePropertyValuesText { get; }

        SvgStyle BaseStyle { get; }

        bool IsSubStyle { get; }
    }
}