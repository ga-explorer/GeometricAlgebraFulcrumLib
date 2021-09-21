using GraphicsComposerLib.Svg.Attributes;
using GraphicsComposerLib.Svg.Elements.Categories;
using GraphicsComposerLib.Svg.Transforms;
using GraphicsComposerLib.Svg.Values;

namespace GraphicsComposerLib.Svg.Elements.Containers
{
    /// <summary>
    /// The 'pattern' element defines a graphics object which can be redrawn at repeated x
    /// and y-coordinate intervals ("tiled") to cover an area. The 'pattern' is referenced by
    /// the fill and/or stroke attributes on other graphics elements to fill or stroke those
    /// elements with the referenced pattern.
    /// </summary>
    public sealed class SvgElementPattern : SvgElement, ISvgContainerElement
    {
        public static SvgElementPattern Create()
        {
            return new SvgElementPattern();
        }

        public static SvgElementPattern Create(string id)
        {
            return new SvgElementPattern() { Id = id };
        }


        public override string ElementName => "pattern";


        //public SvgEavString<SvgElementPattern> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementPattern>;

        //        var attrValue1 = new SvgEavString<SvgElementPattern>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementPattern> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementPattern>;

                var attrValue1 = new SvgEavString<SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementPattern> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementPattern>;

                var attrValue1 = new SvgEavString<SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementPattern> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementPattern>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementPattern> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementPattern>;

                var attrValue1 = new SvgEavString<SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementPattern> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementPattern>;

        //        var attrValue1 = new SvgEavStyle<SvgElementPattern>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavLength<SvgElementPattern> X
        {
            get
            {
                var attrInfo = SvgAttributeUtils.X;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementPattern>;

                var attrValue1 = new SvgEavLength<SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementPattern> Y
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Y;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementPattern>;

                var attrValue1 = new SvgEavLength<SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementPattern> Width
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Width;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementPattern>;

                var attrValue1 = new SvgEavLength<SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementPattern> Height
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Height;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavLength<SvgElementPattern>;

                var attrValue1 = new SvgEavLength<SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavViewBox<SvgElementPattern> ViewBox
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ViewBox;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavViewBox<SvgElementPattern>;

                var attrValue1 = new SvgEavViewBox<SvgElementPattern>(this);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavPreserveAspectRatio<SvgElementPattern> PreserveAspectRatio
        {
            get
            {
                var attrInfo = SvgAttributeUtils.PreserveAspectRatio;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavPreserveAspectRatio<SvgElementPattern>;

                var attrValue1 = new SvgEavPreserveAspectRatio<SvgElementPattern>(this);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementPattern> HRef
        {
            get
            {
                var attrInfo = SvgAttributeUtils.HRef;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementPattern>;

                var attrValue1 = new SvgEavString<SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValuePatternUnits, SvgElementPattern> PatternUnits
        {
            get
            {
                var attrInfo = SvgAttributeUtils.PatternUnits;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgValuePatternUnits, SvgElementPattern>;

                var attrValue1 = new SvgEav<SvgValuePatternUnits, SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValuePatternUnits, SvgElementPattern> PatternContentUnits
        {
            get
            {
                var attrInfo = SvgAttributeUtils.PatternContentUnits;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgValuePatternUnits, SvgElementPattern>;

                var attrValue1 = new SvgEav<SvgValuePatternUnits, SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgTransform, SvgElementPattern> PatternTransform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.PatternTransform;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementPattern>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementPattern>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementPattern()
        {
        }
    }
}