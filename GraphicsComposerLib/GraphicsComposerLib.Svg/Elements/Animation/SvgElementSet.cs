using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Elements.Categories;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Elements.Animation
{
    public sealed class SvgElementSet : SvgElement, ISvgAnimationElement
    {
        public static SvgElementSet Create()
        {
            return new SvgElementSet();
        }

        public static SvgElementSet Create(string id)
        {
            return new SvgElementSet() { Id = id };
        }


        public override string ElementName => "set";


        //public SvgEavString<SvgElementSet> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementSet>;

        //        var attrValue1 = new SvgEavString<SvgElementSet>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementSet> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementSet>;

                var attrValue1 = new SvgEavString<SvgElementSet>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementSet> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementSet>;

                var attrValue1 = new SvgEavString<SvgElementSet>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementSet> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementSet>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementSet>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavAttribute<SvgElementSet> Attribute
        {
            get
            {
                var attrInfo = SvgAttributeUtils.AttributeName;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavAttribute<SvgElementSet>;

                var attrValue1 = new SvgEavAttribute<SvgElementSet>(this);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueAttributeType, SvgElementSet> AttributeType
        {
            get
            {
                var attrInfo = SvgAttributeUtils.AttributeType;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgValueAttributeType, SvgElementSet>;

                var attrValue1 = new SvgEav<SvgValueAttributeType, SvgElementSet>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementSet()
        {
        }

        //TODO: Complete this
    }
}