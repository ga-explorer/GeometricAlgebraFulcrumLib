using GraphicsComposerLib.Rendering.Svg.Attributes;
using GraphicsComposerLib.Rendering.Svg.Elements.Categories;
using GraphicsComposerLib.Rendering.Svg.Transforms;

namespace GraphicsComposerLib.Rendering.Svg.Elements.Containers
{
    /// <summary>
    /// The 'g' SVG element is a container used to group other SVG elements. Transformations applied
    /// to the 'g' element are performed on all of its child elements, and any of its attributes are
    /// inherited by its child elements. It can also group multiple elements to be referenced later
    /// with the 'use' element.
    /// </summary>
    public sealed class SvgElementGroup : SvgElement, ISvgContainerElement
    {
        public static SvgElementGroup Create()
        {
            return new SvgElementGroup();
        }

        public static SvgElementGroup Create(string id)
        {
            return new SvgElementGroup() {Id=id};
        }


        public override string ElementName => "g";


        //public SvgEavString<SvgElementGroup> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementGroup>;

        //        var attrValue1 = new SvgEavString<SvgElementGroup>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementGroup> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementGroup>;

                var attrValue1 = new SvgEavString<SvgElementGroup>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementGroup> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementGroup>;

                var attrValue1 = new SvgEavString<SvgElementGroup>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementGroup> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementGroup>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementGroup>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementGroup> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementGroup>;

                var attrValue1 = new SvgEavString<SvgElementGroup>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementGroup> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementGroup>;

        //        var attrValue1 = new SvgEavStyle<SvgElementGroup>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementGroup> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementGroup>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementGroup>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementGroup()
        {
        }
    }
}
