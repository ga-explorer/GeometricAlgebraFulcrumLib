using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Models;

public sealed class CropAnalysis : ICropAnalysis
{
    public Rectangle BoundingBox { get; set; }

    public Color Background { get; set; }

    public bool Success { get; set; }
}