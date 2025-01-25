using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Utilities.Web.Images.ImageSharp.AutoCrop.Extensions;

internal static class PointExtensions
{
    public static Point Invert(this Point point)
    {
        return new Point(-point.X, -point.Y);
    }
}