using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space2D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders;

/// <summary>
/// This interface represents a convex bounding surface (i.e. a closed convex curve) in 2D
/// </summary>
public interface IBorderCurve2D : IFiniteGeometricShape2D, IIntersectable
{
    IBorderCurve2D MapUsing(IAffineMap2D affineMap);
}