using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Attributes;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles.Properties;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles;

public interface ISvgStyle
{
    IEnumerable<SvgAttributeInfo> PropertyInfos { get; }

    IEnumerable<SvgAttributeInfo> ActivePropertyInfos { get; }

    IEnumerable<SvgStylePropertyValue> ActivePropertyValues { get; }

    string ActivePropertyValuesText { get; }

    SvgStyle BaseStyle { get; }

    bool IsSubStyle { get; }
}