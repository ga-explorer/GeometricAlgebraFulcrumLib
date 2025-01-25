using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Models;

public interface IBorderAnalysis
{
    int Colors { get; }
    Color Background { get; }
    float BucketRatio { get; }
    bool Success { get; }
}