using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Models;

public sealed class RenderInstructions
{
    public Size Size { get; set; }
    public Rectangle Source { get; set; }
    public Rectangle Target { get; set; }
    public Point Translate { get; set; }
    public double Scale { get; set; }
}