namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.ImageSharp.Processing.AutoCrop.Models
{
    public sealed class AutoCropSettings : IAutoCropSettings
    {
        public int PadX { get; set; }
        public int PadY { get; set; }
        public int? ColorThreshold { get; set; }
        public float? BucketThreshold { get; set; }
        public PadMode PadMode { get; set; } = PadMode.Expand;
        public bool AnalyzeWeights { get; set; }
    }
}
