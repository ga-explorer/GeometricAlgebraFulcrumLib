using GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Attributes;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTMLold.Elements.Descriptive;

/// <summary>
/// Each container element or graphics element in an HTML drawing can supply a description string
/// using the 'desc' element where the description is text-only. When the current HTML document
/// fragment is rendered as HTML on visual media, 'desc' elements are not rendered as part of the
/// graphics. Alternate presentations are possible, both visual and aural, which display the
/// 'desc' element but do not display 'path' elements or other graphics elements. The 'desc'
/// element generally improves accessibility of HTML documents.
/// http://docs.w3cub.com/svg/element/desc/
/// </summary>
public sealed class HtmlElementDescription : HtmlElement
{
    public static HtmlElementDescription Create(string descriptionText)
    {
        var element = new HtmlElementDescription();

        element.Contents.AppendText(descriptionText);

        return element;
    }

    public static HtmlElementDescription Create(string id, string descriptionText)
    {
        var element = new HtmlElementDescription() { Id = id };

        element.Contents.AppendText(descriptionText);

        return element;
    }


    public override string ElementName => "desc";


    //public HtmlEavString<HtmlElementDescription> Id
    //{
    //    get
    //    {
    //        var attrInfo = HtmlAttributes.Id;

    //        IHtmlAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as HtmlEavString<HtmlElementDescription>;

    //        var attrValue1 = new HtmlEavString<HtmlElementDescription>(this, attrInfo);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}

    public HtmlEavString<HtmlElementDescription> XmlBase
    {
        get
        {
            var attrInfo = HtmlAttributeUtils.XmlBase;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as HtmlEavString<HtmlElementDescription>;

            var attrValue1 = new HtmlEavString<HtmlElementDescription>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public HtmlEavString<HtmlElementDescription> XmlLanguage
    {
        get
        {
            var attrInfo = HtmlAttributeUtils.XmlLanguage;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as HtmlEavString<HtmlElementDescription>;

            var attrValue1 = new HtmlEavString<HtmlElementDescription>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public HtmlEavStruct<bool, HtmlElementDescription> ExternalResourcesRequired
    {
        get
        {
            var attrInfo = HtmlAttributeUtils.ExternalResourcesRequired;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as HtmlEavStruct<bool, HtmlElementDescription>;

            var attrValue1 = new HtmlEavStruct<bool, HtmlElementDescription>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    public HtmlEavString<HtmlElementDescription> Class
    {
        get
        {
            var attrInfo = HtmlAttributeUtils.Class;

            if (AttributesTable.TryGetValue(attrInfo.Id, out var attrValue))
                return attrValue as HtmlEavString<HtmlElementDescription>;

            var attrValue1 = new HtmlEavString<HtmlElementDescription>(this, attrInfo);
            AttributesTable.Add(attrInfo.Id, attrValue1);

            return attrValue1;
        }
    }

    //public HtmlEavStyle<HtmlElementDescription> Style
    //{
    //    get
    //    {
    //        var attrInfo = HtmlAttributes.Style;

    //        IHtmlAttributeValue attrValue;
    //        if (AttributesTable.TryGetValue(attrInfo.Id, out attrValue))
    //            return attrValue as HtmlEavStyle<HtmlElementDescription>;

    //        var attrValue1 = new HtmlEavStyle<HtmlElementDescription>(this);
    //        AttributesTable.Add(attrInfo.Id, attrValue1);

    //        return attrValue1;
    //    }
    //}


    private HtmlElementDescription()
    {
    }
}