namespace GraphicsComposerLib.Rendering.ImageSharp.Processing.AutoCrop.Extensions
{
    internal static class PointExtensions
    {
        public static Point Invert(this Point point)
        {
            return new Point(-point.X, -point.Y);
        }
    }
}
