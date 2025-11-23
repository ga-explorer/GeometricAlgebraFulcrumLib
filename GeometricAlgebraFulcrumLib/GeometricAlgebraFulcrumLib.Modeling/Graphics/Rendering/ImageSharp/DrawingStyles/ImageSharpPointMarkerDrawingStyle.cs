using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;

public sealed class ImageSharpPointMarkerDrawingStyle(Color strokeColor, Color fillColor, float strokeWidth = 1) 
    : ImageSharpSolidStrokeFillStyle(strokeColor, fillColor, strokeWidth)
{
    public PointMarkerShape Shape { get; set; } = PointMarkerShape.Circle;

    public float Size { get; set; } = 5f;
}