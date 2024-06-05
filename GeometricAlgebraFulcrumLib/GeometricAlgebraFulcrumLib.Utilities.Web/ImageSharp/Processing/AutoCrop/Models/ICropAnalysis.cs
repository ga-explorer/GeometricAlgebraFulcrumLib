using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Models;

public interface ICropAnalysis
{
    Rectangle BoundingBox { get; }
    Color Background { get; }
    bool Success { get; }
}