using CodeComposerLib.HTMLold.Attributes;

namespace CodeComposerLib.HTMLold.Elements.Descriptive;

public sealed class HtmlElementTitle : HtmlElement
{
    public static HtmlElementTitle Create(string titleText)
    {
        var element = new HtmlElementTitle();

        element.Contents.AppendText(titleText);

        return element;
    }

    public static HtmlElementTitle Create(string id, string titleText)
    {
        var element = new HtmlElementTitle() { Id = id };

        element.Contents.AppendText(titleText);

        return element;
    }


    public override string ElementName => "title";


    //public HtmlEavString<HtmlElementTitle> Id
    //{
    //    get
    //    {
    //        var attrInfo = HtmlAttributes.Id;

    //        IHtmlAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as HtmlEavString<HtmlElementTitle>;

    //        var attrValue1 = new HtmlEavString<HtmlElementTitle>(this, attrInfo);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}

    public HtmlEavString<HtmlElementTitle> XmlBase
    {
        get
        {
            var attrInfo = HtmlAttributeUtils.XmlBase;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as HtmlEavString<HtmlElementTitle>;

            var attrValue1 = new HtmlEavString<HtmlElementTitle>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public HtmlEavString<HtmlElementTitle> XmlLanguage
    {
        get
        {
            var attrInfo = HtmlAttributeUtils.XmlLanguage;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as HtmlEavString<HtmlElementTitle>;

            var attrValue1 = new HtmlEavString<HtmlElementTitle>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public HtmlEavStruct<bool, HtmlElementTitle> ExternalResourcesRequired
    {
        get
        {
            var attrInfo = HtmlAttributeUtils.ExternalResourcesRequired;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as HtmlEavStruct<bool, HtmlElementTitle>;

            var attrValue1 = new HtmlEavStruct<bool, HtmlElementTitle>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public HtmlEavString<HtmlElementTitle> Class
    {
        get
        {
            var attrInfo = HtmlAttributeUtils.Class;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as HtmlEavString<HtmlElementTitle>;

            var attrValue1 = new HtmlEavString<HtmlElementTitle>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    //public HtmlEavStyle<HtmlElementTitle> Style
    //{
    //    get
    //    {
    //        var attrInfo = HtmlAttributes.Style;

    //        IHtmlAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as HtmlEavStyle<HtmlElementTitle>;

    //        var attrValue1 = new HtmlEavStyle<HtmlElementTitle>(this);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}


    private HtmlElementTitle()
    {
    }
}