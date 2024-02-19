using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;

namespace WebComposerLib.Svg.Elements.Animation;

public sealed class SvgElementDiscard : SvgElement, ISvgAnimationElement
{
    public static SvgElementDiscard Create()
    {
        return new SvgElementDiscard();
    }

    public static SvgElementDiscard Create(string id)
    {
        return new SvgElementDiscard() { Id = id };
    }


    public override string ElementName => "discard";


    //public SvgEavString<SvgElementDiscard> Id
    //{
    //    get
    //    {
    //        var attrInfo = SvgAttributes.Id;

    //        ISvgAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as SvgEavString<SvgElementDiscard>;

    //        var attrValue1 = new SvgEavString<SvgElementDiscard>(this, attrInfo);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}

    public SvgEavString<SvgElementDiscard> XmlBase
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlBase;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementDiscard>;

            var attrValue1 = new SvgEavString<SvgElementDiscard>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavString<SvgElementDiscard> XmlLanguage
    {
        get
        {
            var attrInfo = SvgAttributeUtils.XmlLanguage;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavString<SvgElementDiscard>;

            var attrValue1 = new SvgEavString<SvgElementDiscard>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public SvgEavStruct<bool, SvgElementDiscard> ExternalResourcesRequired
    {
        get
        {
            var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as SvgEavStruct<bool, SvgElementDiscard>;

            var attrValue1 = new SvgEavStruct<bool, SvgElementDiscard>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }


    private SvgElementDiscard()
    {
    }

    //TODO: Complete this
}