using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.ImageSharp.Processing.AutoCrop.Extensions;

internal static class PointExtensions
{
    public static Point Invert(this Point point)
    {
        return new Point(-point.X, -point.Y);
    }
}