using GraphicsComposerLib.Rendering.Svg.Attributes;
using GraphicsComposerLib.Rendering.Svg.Values;

namespace GraphicsComposerLib.Rendering.Svg.Styles.Properties
{
    public sealed class SvgSpvFontSize : SvgStylePropertyValue
    {
        private SvgConstants.FontSize _fontSizeSpecs = SvgConstants.FontSize.AbsoluteMedium;
        public SvgConstants.FontSize FontSizeSpecs
        {
            get => _fontSizeSpecs;
            set
            {
                _fontSizeSpecs = value;
                IsValueComputed = true;
            }
        }

        private double _size;
        public double Size
        {
            get => _size;
            set
            {
                _size = value;
                _fontSizeSpecs = SvgConstants.FontSize.Length;
                IsValueComputed = true;
            }
        }

        private SvgValueLengthUnit _unit = SvgValueLengthUnit.None;
        public SvgValueLengthUnit Unit
        {
            get => _unit;
            set
            {
                _unit = value ?? SvgValueLengthUnit.None;
                _fontSizeSpecs = SvgConstants.FontSize.Length;
                IsValueComputed = true;
            }
        }

        protected override string ValueComputedText
        {
            get
            {
                switch (FontSizeSpecs)
                {
                    case SvgConstants.FontSize.Length:
                        return Size.ToSvgLengthText(Unit);
                    case SvgConstants.FontSize.AbsoluteSmallXx:
                        return "xx-small";
                    case SvgConstants.FontSize.AbsoluteSmallX:
                        return "x-small";
                    case SvgConstants.FontSize.AbsoluteSmall:
                        return "small";
                    case SvgConstants.FontSize.AbsoluteMedium:
                        return "medium";
                    case SvgConstants.FontSize.AbsoluteLarge:
                        return "large";
                    case SvgConstants.FontSize.AbsoluteLargeX:
                        return "large-x";
                    case SvgConstants.FontSize.AbsoluteLargeXx:
                        return "large-xx";
                    case SvgConstants.FontSize.RelativeLarger:
                        return "larger";
                    case SvgConstants.FontSize.RelativeSmaller:
                        return "smaller";
                    default:
                        return "medium";
                }
            }
        }


        public SvgSpvFontSize(SvgStyle parentStyle, SvgAttributeInfo attributeInfo) 
            : base(parentStyle, attributeInfo)
        {
        }


        public override SvgStylePropertyValue CreateCopy()
        {
            var result = new SvgSpvFontSize(ParentStyle, AttributeInfo);

            if (IsValueStored)
            {
                result._fontSizeSpecs = _fontSizeSpecs;
                result._size = _size;
                result._unit = _unit;
                result.ValueStoredText = ValueStoredText;

                return result;
            }

            result.FontSizeSpecs = FontSizeSpecs;
            result.Size = Size;
            result.Unit = Unit;

            return result;
        }

        public override SvgStylePropertyValue UpdateFrom(SvgStylePropertyValue sourcePropertyValue)
        {
            var source = sourcePropertyValue as SvgSpvFontSize;

            if (ReferenceEquals(source, null) || source.IsValueStored)
            {
                ValueStoredText = source?.ValueStoredText;

                return this;
            }

            FontSizeSpecs = source.FontSizeSpecs;
            Size = source.Size;
            Unit = source.Unit;

            return this;
        }

        public SvgStyle SetTo(SvgConstants.FontSize fontSizeSpecs)
        {
            FontSizeSpecs = fontSizeSpecs;

            return ParentStyle;
        }

        public SvgStyle SetTo(double size)
        {
            FontSizeSpecs = SvgConstants.FontSize.Length;
            Size = size;

            return ParentStyle;
        }

        public SvgStyle SetTo(double size, SvgValueLengthUnit unit)
        {
            FontSizeSpecs = SvgConstants.FontSize.Length;
            Size = size;
            Unit = unit;

            return ParentStyle;
        }
    }
}