using System.Collections.Generic;
using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Styles.Properties;

namespace GraphicsComposerLib.Svg.Styles
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