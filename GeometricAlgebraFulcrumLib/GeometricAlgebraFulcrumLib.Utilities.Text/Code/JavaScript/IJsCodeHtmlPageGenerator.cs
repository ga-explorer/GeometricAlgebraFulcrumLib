namespace GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

public interface IJsCodeHtmlPageGenerator
{
    string HtmlTemplateText { get; }

    string HtmlPageTitle { get; }

    IEnumerable<string> JavaScriptIncludes { get; }

    string GetJavaScriptCode();
}