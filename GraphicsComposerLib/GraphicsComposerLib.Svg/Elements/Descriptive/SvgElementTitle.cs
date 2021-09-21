using GraphicsComposerLib.Svg.Attributes;

namespace GraphicsComposerLib.Svg.Elements.Descriptive
{
    public sealed class SvgElementTitle : SvgElement
    {
        public static SvgElementTitle Create(string titleText)
        {
            var element = new SvgElementTitle();

            element.Contents.AppendText(titleText);

            return element;
        }

        public static SvgElementTitle Create(string id, string titleText)
        {
            var element = new SvgElementTitle() { Id = id };

            element.Contents.AppendText(titleText);

            return element;
        }


        public override string ElementName => "title";


        //public SvgEavString<SvgElementTitle> Id
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Id;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavString<SvgElementTitle>;

        //        var attrValue1 = new SvgEavString<SvgElementTitle>(this, attrInfo);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}

        public SvgEavString<SvgElementTitle> XmlBase
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlBase;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementTitle>;

                var attrValue1 = new SvgEavString<SvgElementTitle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementTitle> XmlLanguage
        {
            get
            {
                var attrInfo = SvgAttributeUtils.XmlLanguage;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementTitle>;

                var attrValue1 = new SvgEavString<SvgElementTitle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavStruct<bool, SvgElementTitle> ExternalResourcesRequired
        {
            get
            {
                var attrInfo = SvgAttributeUtils.ExternalResourcesRequired;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavStruct<bool, SvgElementTitle>;

                var attrValue1 = new SvgEavStruct<bool, SvgElementTitle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        public SvgEavString<SvgElementTitle> Class
        {
            get
            {
                var attrInfo = SvgAttributeUtils.Class;

                ISvgAttributeValue attrValue;
                if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
                    return attrValue as SvgEavString<SvgElementTitle>;

                var attrValue1 = new SvgEavString<SvgElementTitle>(this, attrInfo);
                AttributesTable.Add(attrInfo.Id, attrValue1);

                return attrValue1;
            }
        }

        //public SvgEavStyle<SvgElementTitle> Style
        //{
        //    get
        //    {
        //        var attrInfo = SvgAttributes.Style;

        //        ISvgAttributeValue attrValue;
        //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
        //            return attrValue as SvgEavStyle<SvgElementTitle>;

        //        var attrValue1 = new SvgEavStyle<SvgElementTitle>(this);
        //        AttributesTable.Add(attrInfo.Id, attrValue1);

        //        return attrValue1;
        //    }
        //}


        private SvgElementTitle()
        {
        }
    }
}