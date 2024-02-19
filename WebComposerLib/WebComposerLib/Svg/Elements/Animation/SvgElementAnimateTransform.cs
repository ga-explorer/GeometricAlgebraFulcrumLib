using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Elements.Animation;

public sealed class SvgElementAnimateTransform : SvgElement, ISvgAnimationElement
{
    public static SvgElementAnimateTransform Create()
    {
        return new SvgElementAnimateTransform();
    }

    public static SvgElementAnimateTransform Create(string id)
    {
        return new SvgElementAnimateTransform() { Id = id };
    }


    public override string ElementName => "animateTransform";


    //public SvgEavString<SvgElementAnimateTransform> Id
    //{
    //    get
    //    {
    //        var attrInfo = SvgAttributes.Id;

    //        ISvgAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as SvgEavString<SvgElementAnimateTransform>;

    //        var attrValue1 = new SvgEavString<SvgElementAnimateTransform>(this, attrInfo);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}

    public SvgEavString<SvgElementAnimateTransform> XmlBase
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlBase;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementAnimateTransform>;

            var attrValue1 = new SvgEavString<SvgElementAnimateTransform>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementAnimateTransform> XmlLanguage
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlLanguage;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementAnimateTransform>;

            var attrValue1 = new SvgEavString<SvgElementAnimateTransform>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavStruct<bool, SvgElementAnimateTransform> ExternalResourcesRequired
    {
        get
        {
            var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavStruct<bool, SvgElementAnimateTransform>;

            var attrValue1 = new SvgEavStruct<bool, SvgElementAnimateTransform>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavAttribute<SvgElementAnimateTransform> Attribute
    {
        get
        {
            var attrInfo = SvgAttributeUtils.AttributeName;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavAttribute<SvgElementAnimateTransform>;

            var attrValue1 = new SvgEavAttribute<SvgElementAnimateTransform>(this);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEav<SvgValueAttributeType, SvgElementAnimateTransform> AttributeType
    {
        get
        {
            var attrInfo = SvgAttributeUtils.AttributeType;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEav<SvgValueAttributeType, SvgElementAnimateTransform>;

            var attrValue1 = new SvgEav<SvgValueAttributeType, SvgElementAnimateTransform>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementAnimateTransform> From
    {
        get
        {
            var attrInfo = SvgAttributeUtils.From;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementAnimateTransform>;

            var attrValue1 = new SvgEavString<SvgElementAnimateTransform>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEav<SvgValueAnimationCalcMode, SvgElementAnimateTransform> CalcMode
    {
        get
        {
            var attrInfo = SvgAttributeUtils.CalcMode;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEav<SvgValueAnimationCalcMode, SvgElementAnimateTransform>;

            var attrValue1 = new SvgEav<SvgValueAnimationCalcMode, SvgElementAnimateTransform>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEav<SvgValueLengthsList, SvgElementAnimateTransform> KeyTimes
    {
        get
        {
            var attrInfo = SvgAttributeUtils.KeyTimes;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEav<SvgValueLengthsList, SvgElementAnimateTransform>;

            var attrValue1 = new SvgEav<SvgValueLengthsList, SvgElementAnimateTransform>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }



    private SvgElementAnimateTransform()
    {
    }
}