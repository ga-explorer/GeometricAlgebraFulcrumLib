using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;

namespace WebComposerLib.Svg.Elements.Gradient;

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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
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
            .StopColor.SetToRgba(color)
            .StopOpacity.SetToNumber(1);

        return this;
    }

    public SvgElementGradientStop SetAbsoluteStop(double offset, Color color, double opacity)
    {
        Offset.SetToNumber(offset);

        Style
            .StopColor.SetToRgba(color)
            .StopOpacity.SetToNumber(opacity);

        return this;
    }

    public SvgElementGradientStop SetRelativeStop(double offset, Color color)
    {
        Offset.SetToPercent(offset);

        Style
            .StopColor.SetToRgba(color)
            .StopOpacity.SetToNumber(1);

        return this;
    }

    public SvgElementGradientStop SetRelativeStop(double offset, Color color, double opacity)
    {
        Offset.SetToPercent(offset);

        Style
            .StopColor.SetToRgba(color)
            .StopOpacity.SetToNumber(opacity);

        return this;
    }
}