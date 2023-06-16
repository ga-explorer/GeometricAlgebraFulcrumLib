namespace WebComposerLib.Html.Media
{
    public interface IWclHtmlImageDataUrlCache :
        IWclHtmlDataUrlCache
    {
        Color WhitespaceColor { get; }

        int MarginSize { get; set; }

        Color BackgroundColor { get; set; }
    }
}