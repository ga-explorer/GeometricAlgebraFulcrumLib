using GraphicsComposerLib.Rendering.Svg.Attributes;
using GraphicsComposerLib.Rendering.Svg.Elements.Categories;
using GraphicsComposerLib.Rendering.Svg.Transforms;
using GraphicsComposerLib.Rendering.Svg.Values;

namespace GraphicsComposerLib.Rendering.Svg.Elements.Text
{
    /// <summary>
    /// The SVG 'text' element defines a graphics element consisting of text. It's possible to apply
    /// a gradient, pattern, clipping path, mask, or filter to 'text', just like any other SVG graphics
    /// element. If text is included within SVG not inside of a 'text' element, it is not rendered.
    /// This is different from being hidden by default, as setting the display property will not show
    /// the text.
    /// </summary>
    public sealed class SvgElementText : SvgElement, ISvgContainerElement
    {
        public static SvgElementText Create()
        {
            return new SvgElementText();
        }

        public static SvgElementText Create(string id)
        {
            return new SvgElementText() {Id = id};
        }


        public override string ElementName => "text";


        //public SvgEavString<SvgElementText> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementText>;

        //        var attrValue1 = new SvgEavString<SvgElementText>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementText> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementText>;

                var attrValue1 = new SvgEavString<SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementText> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementText>;

                var attrValue1 = new SvgEavString<SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementText> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementText>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementText> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementText>;

                var attrValue1 = new SvgEavString<SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementText> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementText>;

        //        var attrValue1 = new SvgEavStyle<SvgElementText>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementText> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementText>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementText> X
        {
            get
            {
                var attrInfo = SvgAttributeUtils.X;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementText>;

                var attrValue1 = new SvgEavLength<SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementText> Y
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Y;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementText>;

                var attrValue1 = new SvgEavLength<SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueLengthsList, SvgElementText> DeltaX
        {
            get
            {
                var attrInfo = SvgAttributeUtils.DeltaX;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueLengthsList, SvgElementText>;

                var attrValue1 = new SvgEav<SvgValueLengthsList, SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueLengthsList, SvgElementText> DeltaY
        {
            get
            {
                var attrInfo = SvgAttributeUtils.DeltaY;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueLengthsList, SvgElementText>;

                var attrValue1 = new SvgEav<SvgValueLengthsList, SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEav<SvgValueTextAnchor, SvgElementText> TextAnchor
        {
            get
            {
                var attrInfo = SvgAttributeUtils.TextAnchor;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgValueTextAnchor, SvgElementText>;

                var attrValue1 = new SvgEav<SvgValueTextAnchor, SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementText> TextLength
        {
            get
            {
                var attrInfo = SvgAttributeUtils.TextLength;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementText>;

                var attrValue1 = new SvgEavLength<SvgElementText>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementText()
        {
        }
    }
}