using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;
using WebComposerLib.Svg.Transforms;

namespace WebComposerLib.Svg.Elements.Containers
{
    /// <summary>
    /// The 'a' SVG element defines a hyperlink.
    /// </summary>
    public sealed class SvgElementAnchor : SvgElement, ISvgContainerElement
    {
        public static SvgElementAnchor Create()
        {
            return new SvgElementAnchor();
        }

        public static SvgElementAnchor Create(string id)
        {
            return new SvgElementAnchor() {Id = id};
        }


        public override string ElementName => "a";


        //public SvgEavString<SvgElementAnchor> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementAnchor>;

        //        var attrValue1 = new SvgEavString<SvgElementAnchor>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementAnchor> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnchor>;

                var attrValue1 = new SvgEavString<SvgElementAnchor>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementAnchor> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnchor>;

                var attrValue1 = new SvgEavString<SvgElementAnchor>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementAnchor> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementAnchor>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementAnchor>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementAnchor> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnchor>;

                var attrValue1 = new SvgEavString<SvgElementAnchor>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementAnchor> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementAnchor>;

        //        var attrValue1 = new SvgEavStyle<SvgElementAnchor>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementAnchor> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementAnchor>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementAnchor>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementAnchor> HRef
        {
            get
            {
                var attrInfo = SvgAttributeUtils.HRef;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnchor>;

                var attrValue1 = new SvgEavString<SvgElementAnchor>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementAnchor> Target
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Target;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementAnchor>;

                var attrValue1 = new SvgEavString<SvgElementAnchor>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }



        private SvgElementAnchor()
        {
        }
    }
}