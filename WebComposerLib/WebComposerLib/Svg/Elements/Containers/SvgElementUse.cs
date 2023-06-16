using WebComposerLib.Svg.Attributes;
using WebComposerLib.Svg.Elements.Categories;
using WebComposerLib.Svg.Transforms;

namespace WebComposerLib.Svg.Elements.Containers
{
    /// <summary>
    /// The 'use' element takes nodes from within the SVG document, and duplicates them somewhere else.
    /// The effect is the same as if the nodes were deeply cloned into a non-exposed DOM, and then pasted
    /// where the use element is, much like cloned template elements in HTML5. Since the cloned nodes are
    /// not exposed, care must be taken when using CSS to style a use element and its hidden descendants.
    /// CSS attributes are not guaranteed to be inherited by the hidden, cloned DOM unless you explicitly
    /// request it using CSS inheritance.
    /// http://docs.w3cub.com/svg/element/use/
    /// </summary>
    public sealed class SvgElementUse : SvgElement, ISvgContainerElement
    {
        public static SvgElementUse Create()
        {
            return new SvgElementUse();
        }

        public static SvgElementUse Create(string id)
        {
            return new SvgElementUse() { Id = id };
        }


        public override string ElementName => "use";


        //public SvgEavString<SvgElementUse> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementUse>;

        //        var attrValue1 = new SvgEavString<SvgElementUse>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementUse> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementUse>;

                var attrValue1 = new SvgEavString<SvgElementUse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementUse> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementUse>;

                var attrValue1 = new SvgEavString<SvgElementUse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementUse> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementUse>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementUse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementUse> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementUse>;

                var attrValue1 = new SvgEavString<SvgElementUse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementUse> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementUse>;

        //        var attrValue1 = new SvgEavStyle<SvgElementUse>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEav<SvgTransform, SvgElementUse> Transform
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Transform;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEav<SvgTransform, SvgElementUse>;

                var attrValue1 = new SvgEav<SvgTransform, SvgElementUse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementUse> X
        {
            get
            {
                var attrInfo = SvgAttributeUtils.X;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementUse>;

                var attrValue1 = new SvgEavLength<SvgElementUse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementUse> Y
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Y;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementUse>;

                var attrValue1 = new SvgEavLength<SvgElementUse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementUse> Width
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Width;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementUse>;

                var attrValue1 = new SvgEavLength<SvgElementUse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavLength<SvgElementUse> Height
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Height;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavLength<SvgElementUse>;

                var attrValue1 = new SvgEavLength<SvgElementUse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementUse> HRef
        {
            get
            {
                var attrInfo = SvgAttributeUtils.HRef;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementUse>;

                var attrValue1 = new SvgEavString<SvgElementUse>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }


        private SvgElementUse()
        {
        }
    }
}