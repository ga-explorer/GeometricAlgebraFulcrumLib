using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.ImageSharp.DrawingStyles;

public sealed class ImageSharpLineDrawingStyle(Color strokeColor, float strokeWidth = 1) 
    : ImageSharpSolidStrokeStyle(strokeColor, strokeWidth)
{
    
}