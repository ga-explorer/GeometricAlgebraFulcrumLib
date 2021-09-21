using System.Drawing;
using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Elements.Categories;

namespace GraphicsComposerLib.Svg.Elements.Gradient
{
    public sealed class SvgElementGradientStop : SvgElement, ISvgGradientElement
    {
        public static SvgElementGradientStop Create()
        {
            return new SvgElementGradientStop();
        }

        public static SvgElementGradientStop Create(string id)
        {
            return new SvgElementGradientStop() { Id = id };
        }


        public override string ElementName => "stop";


        public SvgEavString<SvgElementGradientStop> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementGradientStop>;

                var attrValue1 = new SvgEavString<SvgElementGradientStop>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementGradientStop> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementGradientStop>;

                var attrValue1 = new SvgEavString<SvgElementGradientStop>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementGradientStop> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementGradientStop>;

                var attrValue1 = new SvgEavString<SvgElementGradientStop>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementGradientStop> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementGradientStop>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementGradientStop>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavNumber<SvgElementGradientStop> Offset
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Offset;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavNumber<SvgElementGradientStop>;

                var attrValue1 = new SvgEavNumber<SvgElementGradientStop>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementGradientStop()
        {
        }


        public SvgElementGradientStop SetAbsoluteStop(double offset, Color color)
        {
            Offset.SetToNumber(offset);

            Style
                .StopColor.SetToRgb(color)
                .StopOpacity.SetToNumber(1);

            return this;
        }

        public SvgElementGradientStop SetAbsoluteStop(double offset, Color color, double opacity)
        {
            Offset.SetToNumber(offset);

            Style
                .StopColor.SetToRgb(color)
                .StopOpacity.SetToNumber(opacity);

            return this;
        }

        public SvgElementGradientStop SetRelativeStop(double offset, Color color)
        {
            Offset.SetToPercent(offset);

            Style
                .StopColor.SetToRgb(color)
                .StopOpacity.SetToNumber(1);

            return this;
        }

        public SvgElementGradientStop SetRelativeStop(double offset, Color color, double opacity)
        {
            Offset.SetToPercent(offset);

            Style
                .StopColor.SetToRgb(color)
                .StopOpacity.SetToNumber(opacity);

            return this;
        }
    }
}