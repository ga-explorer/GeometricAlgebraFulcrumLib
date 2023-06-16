using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space3D.Mutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes
{
    public interface IFiniteGeometricShape3D : 
        IGeometricElement, 
        IIntersectable
    {
        BoundingBox3D GetBoundingBox();

        MutableBoundingBox3D GetMutableBoundingBox();
    }
}