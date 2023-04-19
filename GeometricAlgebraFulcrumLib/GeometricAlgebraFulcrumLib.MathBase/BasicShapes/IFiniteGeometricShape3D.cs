using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space3D.Mutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicShapes
{
    public interface IFiniteGeometricShape3D : 
        IGeometricElement, 
        IIntersectable
    {
        BoundingBox3D GetBoundingBox();

        MutableBoundingBox3D GetMutableBoundingBox();
    }
}