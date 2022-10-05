using GraphicsComposerLib.Rendering.Svg.Styles.Properties;
using GraphicsComposerLib.Rendering.Svg.Styles.SubStyles;

namespace GraphicsComposerLib.Rendering.Svg.Styles
{
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
}
