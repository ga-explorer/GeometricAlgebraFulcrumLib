using NumericalGeometryLib.BasicMath.Maps.Space2D;
using NumericalGeometryLib.BasicShapes;

namespace NumericalGeometryLib.Borders
{
    /// <summary>
    /// This interface represents a convex bounding surface (i.e. a closed convex curve) in 2D
    /// </summary>
    public interface IBorderCurve2D : IFiniteGeometricShape2D, IIntersectable
    {
        IBorderCurve2D MapUsing(IAffineMap2D affineMap);
    }
}