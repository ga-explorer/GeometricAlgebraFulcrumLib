using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace GeometricAlgebraFulcrumLib.Utilities.Code.HTML;

public sealed class HtmlComposer
{
    public IHtmlDocument Document { get; private set; }

    public IHtmlHeadElement HeadElement 
        => Document.Head;

    public IHtmlBodyElement BodyElement
        => Document.Body as IHtmlBodyElement;


    public HtmlComposer()
    {
        Document = new HtmlParser().ParseDocument(
            @"<!DOCTYPE html><html lang=""en""><head></head><body></body></html>"
        );

    }


    public HtmlComposer Reset()
    {
        Document = new HtmlParser().ParseDocument(
            @"<!DOCTYPE html><html lang=""en""><head></head><body></body></html>"
        );

        return this;
    }


    public override string ToString()
    {
        return Document.DocumentElement.InnerHtml;
    }
}