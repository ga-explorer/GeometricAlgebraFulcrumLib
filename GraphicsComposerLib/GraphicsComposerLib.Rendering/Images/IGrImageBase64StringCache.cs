namespace GraphicsComposerLib.Rendering.Images
{
    public interface IGrImageBase64StringCache :
        IGrBase64StringCache
    {
        Color WhitespaceColor { get; }

        int MarginSize { get; set; }

        Color BackgroundColor { get; set; }
    }
}