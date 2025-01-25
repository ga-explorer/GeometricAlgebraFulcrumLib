using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;

/// <summary>
/// This interface represents a convex bounding surface in 3D
/// </summary>
public interface IFloat64BorderSurface3D :
    IFloat64FiniteGeometricShape3D
{
    IFloat64BorderSurface3D MapUsing(IFloat64AffineMap3D affineMap);
}