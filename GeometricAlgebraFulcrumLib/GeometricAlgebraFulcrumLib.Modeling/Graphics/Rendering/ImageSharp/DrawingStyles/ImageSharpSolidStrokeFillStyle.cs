using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;

public class ImageSharpSolidStrokeFillStyle(Color strokeColor, Color fillColor, float strokeWidth = 1) :
    ImageSharpSolidStrokeStyle(strokeColor, strokeWidth),
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