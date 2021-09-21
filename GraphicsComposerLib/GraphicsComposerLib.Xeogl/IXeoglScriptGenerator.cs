using System.Collections.Generic;

namespace GraphicsComposerLib.Xeogl
{
    public interface IXeoglScriptGenerator
    {
        string HtmlTemplateText { get; }

        string HtmlPageTitle { get; }

        IEnumerable<string> JavaScriptIncludes { get; }

        string GetJavaScriptCode();
    }
}