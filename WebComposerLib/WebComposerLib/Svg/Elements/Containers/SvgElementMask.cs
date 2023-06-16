using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;
using WebComposerLib.Svg.Transforms;
using WebComposerLib.Svg.Values;

namespace WebComposerLib.Svg.Elements.Containers
{
    /// <summary>
    /// In SVG, you can specify that any other graphics object or 'g' element can be used as an
    /// alpha mask for compositing the current object into the background. A mask is defined with
    /// the 'mask' element. A mask is used/referenced using the mask property.
    /// http://docs.w3cub.com/svg/element/mask/
    /// </summary>
    public sealed class SvgElementMask : SvgElement, ISvgContainerElement
    {
        public static SvgElementMask Create()
        {
            return new SvgElementMask();
        }

        public static SvgElementMask Create(string id)
        {
            return new SvgElementMask() { Id = id };
        }


        public override string ElementName => "mask";


        //public SvgEavString<SvgElementMask> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementMask>;

        //        var attrValue1 = new SvgEavString<SvgElementMask>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementMask> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementMask>;

                var attrValue1 = new SvgEavString<SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementMask> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementMask>;

                var attrValue1 = new SvgEavString<SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementMask> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementMask>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementMask> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementMask>;

                var attrValue1 = new SvgEavString<SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementMask> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementMask>;

        //        var attrValue1 = new SvgEavStyle<SvgElementMask>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementMask> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementMask>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementMask> X
        {
            get
            {
                var attrInfo = SvgAttributeUtils.X;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementMask>;

                var attrValue1 = new SvgEavLength<SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementMask> Y
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Y;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementMask>;

                var attrValue1 = new SvgEavLength<SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementMask> Width
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Width;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementMask>;

                var attrValue1 = new SvgEavLength<SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementMask> Height
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Height;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementMask>;

                var attrValue1 = new SvgEavLength<SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueMaskUnits, SvgElementMask> MaskUnits
        {
            get
            {
                var attrInfo = SvgAttributeUtils.MaskUnits;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueMaskUnits, SvgElementMask>;

                var attrValue1 = new SvgEav<SvgValueMaskUnits, SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueMaskUnits, SvgElementMask> MaskContentUnits
        {
            get
            {
                var attrInfo = SvgAttributeUtils.MaskContentUnits;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueMaskUnits, SvgElementMask>;

                var attrValue1 = new SvgEav<SvgValueMaskUnits, SvgElementMask>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementMask()
        {
        }
    }
}