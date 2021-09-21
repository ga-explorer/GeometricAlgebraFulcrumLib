using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Elements.Categories;

namespace GraphicsComposerLib.Svg.Elements.Animation
{
    public sealed class SvgElementMPath : SvgElement, ISvgAnimationElement
    {
        public static SvgElementMPath Create()
        {
            return new SvgElementMPath();
        }

        public static SvgElementMPath Create(string id)
        {
            return new SvgElementMPath() { Id = id };
        }


        public override string ElementName => "mpath";


        //public SvgEavString<SvgElementMPath> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementMPath>;

        //        var attrValue1 = new SvgEavString<SvgElementMPath>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementMPath> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementMPath>;

                var attrValue1 = new SvgEavString<SvgElementMPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementMPath> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementMPath>;

                var attrValue1 = new SvgEavString<SvgElementMPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementMPath> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementMPath>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementMPath>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementMPath()
        {
        }

        //TODO: Complete this
    }
}