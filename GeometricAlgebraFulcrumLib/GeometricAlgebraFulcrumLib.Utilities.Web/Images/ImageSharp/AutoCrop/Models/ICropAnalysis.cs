using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Models;

public interface ICropAnalysis
{
    Rectangle BoundingBox { get; }
    Color Background { get; }
    bool Success { get; }
}