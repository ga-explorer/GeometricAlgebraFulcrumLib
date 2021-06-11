namespace CodeComposerLib.HTMLold.Content
{
    public interface IHtmlContent
    {
        bool IsContentText { get; }

        bool IsContentComment { get; }

        bool IsContentElement { get; }
    }
}