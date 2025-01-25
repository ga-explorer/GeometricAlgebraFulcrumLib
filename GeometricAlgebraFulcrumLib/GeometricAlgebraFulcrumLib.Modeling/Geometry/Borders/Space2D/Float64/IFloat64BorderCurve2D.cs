using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

/// <summary>
/// This interface represents a convex bounding surface (i.e. a closed convex curve) in 2D
/// </summary>
public interface IFloat64BorderCurve2D :
    IFloat64FiniteGeometricShape2D,
    IIntersectable
{
    IFloat64BorderCurve2D MapUsing(IFloat64AffineMap2D affineMap);
}