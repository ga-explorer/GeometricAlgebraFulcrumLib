using NumericalGeometryLib.BasicMath;

namespace NumericalGeometryLib.BasicShapes
{
    public interface IFiniteGeometricShape4D 
        : IGeometricElement, IIntersectable
    {
        //BoundingBox4D GetBoundingBox();

        //MutableBoundingBox4D GetMutableBoundingBox();
    }
}