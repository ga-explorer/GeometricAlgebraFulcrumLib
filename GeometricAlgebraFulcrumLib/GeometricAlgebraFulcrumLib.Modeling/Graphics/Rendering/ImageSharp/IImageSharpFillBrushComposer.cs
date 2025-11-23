using SixLabors.ImageSharp.Drawing.Processing;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp;

public interface IImageSharpFillBrushComposer
{
    /// <summary>
    /// Gets the brush used to color the inside of a shape.
    /// </summary>
    Brush GetFillBrush();
}