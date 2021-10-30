namespace GraphicsComposerLib.GraphViz.Dot.Image
{
    public interface IDotGraphImage
    {
        string ImageId { get; }

        string ImageFileName { get; }

        bool IsGenerated { get; }

        System.Drawing.Image GetImage();
    }
}