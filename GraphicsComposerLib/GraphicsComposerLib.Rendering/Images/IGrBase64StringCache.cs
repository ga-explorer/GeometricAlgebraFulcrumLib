namespace GraphicsComposerLib.Rendering.Images;

public interface IGrBase64StringCache
{
    IEnumerable<string> Keys { get; }

    IEnumerable<string> Base64Strings { get; }

    IEnumerable<string> Base64HtmlStrings { get; }

    string GetBase64String(string key);

    string GetBase64HtmlString(string key);
}