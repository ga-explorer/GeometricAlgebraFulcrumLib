using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;

public sealed class ImageSharpArrowHeadDrawingStyle(Color strokeColor, Color fillColor, float strokeWidth = 1) 
    : ImageSharpSolidStrokeFillStyle(strokeColor, fillColor, strokeWidth)
{
    public ArrowHeadShape Shape { get; set; } = ArrowHeadShape.Triangle;

    public float SizeX { get; set; } = 5f;

    public float SizeY { get; set; } = 5f;
}