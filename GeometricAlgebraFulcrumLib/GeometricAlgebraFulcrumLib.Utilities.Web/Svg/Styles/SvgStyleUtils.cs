using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles.Properties;
using GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles.SubStyles;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Svg.Styles;

public static class SvgStyleUtils
{
    public static SvgSubStyleStroke CreateStrokeSubStyle(this SvgStyle baseStyle)
    {
        return SvgSubStyleStroke.Create(baseStyle);
    }

    public static SvgSubStyleFill CreateFillSubStyle(this SvgStyle baseStyle)
    {
        return SvgSubStyleFill.Create(baseStyle);
    }

    public static bool IsNullOrEmpty(this SvgStylePropertyValue propertyValue)
    {
        return ReferenceEquals(propertyValue, null) || 
               propertyValue.IsValueEmpty;
    }
}