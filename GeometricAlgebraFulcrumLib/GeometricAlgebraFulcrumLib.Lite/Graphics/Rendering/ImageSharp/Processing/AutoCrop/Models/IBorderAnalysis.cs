namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models
{
    public interface IBorderAnalysis
    {
        int Colors { get; }
        Color Background { get; }
        float BucketRatio { get; }
        bool Success { get; }
    }
}
