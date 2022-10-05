using GraphicsComposerLib.Rendering.Svg.Attributes;
using GraphicsComposerLib.Rendering.Svg.Elements;
using GraphicsComposerLib.Rendering.Svg.Styles.Properties;
using TextComposerLib.Text;

namespace GraphicsComposerLib.Rendering.Svg.Styles.SubStyles
{
    public abstract class SvgSubStyle : ISvgStyle
    {
        public abstract IEnumerable<SvgAttributeInfo> PropertyInfos { get; }

        public IEnumerable<SvgAttributeInfo> ActivePropertyInfos
            => PropertyInfos
                .Where(
                    propertyInfo => BaseStyle.ContainsProperty(propertyInfo)
                );

        public IEnumerable<SvgStylePropertyValue> ActivePropertyValues
        {
            get
            {
                foreach (var propertyInfo in PropertyInfos)
                {
                    if (BaseStyle.TryGetPropertyValue(propertyInfo, out var propertyValue))
                        yield return propertyValue;
                }
            }
        }

        public string ActivePropertyValuesText 
            => ActivePropertyValues.Concatenate(" ");

        public bool IsSubStyle => true;

        private SvgStyle _baseStyle;
        public SvgStyle BaseStyle
        {
            get => _baseStyle;
            set => _baseStyle = value ?? SvgStyle.Create();
        }


        protected SvgSubStyle(SvgStyle baseStyle)
        {
            BaseStyle = baseStyle ?? SvgStyle.Create();
        }


        public abstract void UpdateTargetStyle(SvgStyle targetStyle);

        public void UpdateElementStyle(SvgElement targetElement)
        {
            if (ReferenceEquals(targetElement, null))
                return;

            UpdateTargetStyle(targetElement.Style);
        }

        public void ClearProperties()
        {
            _baseStyle.ClearProperties(PropertyInfos);
        }
    }
}
