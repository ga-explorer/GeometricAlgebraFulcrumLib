using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models;

public interface IRenderInstructions
{
    Size Size { get; }
    Rectangle Source { get; }
    Rectangle Target { get; }
    Point Translate { get; }
    double Scale { get; }
}