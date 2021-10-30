using System.Collections.Generic;

namespace TextComposerLib.Code.JavaScript
{
    public interface IJsCodeHtmlPageGenerator
    {
        string HtmlTemplateText { get; }

        string HtmlPageTitle { get; }

        IEnumerable<string> JavaScriptIncludes { get; }

        string GetJavaScriptCode();
    }
}