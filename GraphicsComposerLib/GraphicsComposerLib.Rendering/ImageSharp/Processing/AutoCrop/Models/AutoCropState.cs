namespace GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop.Models
{
    public sealed class AutoCropState
    {
        public readonly Rectangle Bounds;
        public readonly Color BorderColor;
        public readonly Rectangle OriginalDimensions;

        public Rectangle TargetDimensions;
        public Size Padding;
        public int BytesPerPixel;

        public bool ShouldPreRender;
        public RenderInstructions Instructions;

        public AutoCropState(ICropAnalysis analysis, Image image)
        {
            Bounds = analysis.BoundingBox;
            BorderColor = analysis.Background;
            BytesPerPixel = image.PixelType.BitsPerPixel / 8;
            OriginalDimensions = new Rectangle(0, 0, image.Width, image.Height);
        }
    }
}
