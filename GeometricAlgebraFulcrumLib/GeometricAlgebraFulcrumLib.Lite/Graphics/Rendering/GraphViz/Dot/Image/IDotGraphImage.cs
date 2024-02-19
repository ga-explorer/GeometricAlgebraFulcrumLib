namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Image;

public interface IDotGraphImage
{
    string ImageId { get; }

    string ImageFileName { get; }

    bool IsGenerated { get; }

    SixLabors.ImageSharp.Image GetImage();
}