using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;

public class ImageSharpSolidFillStyle(Color fillColor) :
    IImageSharpFillBrushComposer
{
    /// <summary>
    /// The color of the solid brush
    /// </summary>
    public Color FillColor { get; } = fillColor;


    public Brush GetFillBrush()
    {
        return new SolidBrush(FillColor);
    }
}