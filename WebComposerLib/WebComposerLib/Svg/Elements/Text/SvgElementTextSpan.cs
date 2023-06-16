using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;

namespace WebComposerLib.Svg.Elements.Text
{
    public sealed class SvgElementTextPath : SvgElement, ISvgContainerElement
    {
        public static SvgElementTextPath Create()
        {
            return new SvgElementTextPath();
        }

        public static SvgElementTextPath Create(string id)
        {
            return new SvgElementTextPath() {Id = id};
        }


        public override string ElementName => "textPath";


        //public SvgEavString<SvgElementTextPath> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementTextPath>;

        //        var attrValue1 = new SvgEavString<SvgElementTextPath>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementTextPath> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementTextPath>;

                var attrValue1 = new SvgEavString<SvgElementTextPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementTextPath> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementTextPath>;

                var attrValue1 = new SvgEavString<SvgElementTextPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementTextPath> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementTextPath>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementTextPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementTextPath()
        {
        }

        //TODO: Complete this
    }
}