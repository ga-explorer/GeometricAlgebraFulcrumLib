using WebComposerLib.Svg.Attributes;

namespace WebComposerLib.Svg.Elements.Descriptive
{
    /// <summary>
    /// Each container element or graphics element in an SVG drawing can supply a description string
    /// using the 'desc' element where the description is text-only. When the current SVG document
    /// fragment is rendered as SVG on visual media, 'desc' elements are not rendered as part of the
    /// graphics. Alternate presentations are possible, both visual and aural, which display the
    /// 'desc' element but do not display 'path' elements or other graphics elements. The 'desc'
    /// element generally improves accessibility of SVG documents.
    /// http://docs.w3cub.com/svg/element/desc/
    /// </summary>
    public sealed class SvgElementDescription : SvgElement
    {
        public static SvgElementDescription Create(string descriptionText)
        {
            var element = new SvgElementDescription();

            element.Contents.AppendText(descriptionText);

            return element;
        }

        public static SvgElementDescription Create(string id, string descriptionText)
        {
            var element = new SvgElementDescription() { Id = id };

            element.Contents.AppendText(descriptionText);

            return element;
        }


        public override string ElementName => "desc";


        //public SvgEavString<SvgElementDescription> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementDescription>;

        //        var attrValue1 = new SvgEavString<SvgElementDescription>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementDescription> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementDescription>;

                var attrValue1 = new SvgEavString<SvgElementDescription>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementDescription> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementDescription>;

                var attrValue1 = new SvgEavString<SvgElementDescription>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementDescription> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementDescription>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementDescription>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementDescription> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                    return attrValue as SvgEavString<SvgElementDescription>;

                var attrValue1 = new SvgEavString<SvgElementDescription>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementDescription> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementDescription>;

        //        var attrValue1 = new SvgEavStyle<SvgElementDescription>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}


        private SvgElementDescription()
        {
        }
    }
}