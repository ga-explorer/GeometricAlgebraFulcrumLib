using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.Curves.Space2D
{
    public interface ICurve2D : IGeometricElement
    {
        Tuple2D GetPointAt(double t);
    }
}