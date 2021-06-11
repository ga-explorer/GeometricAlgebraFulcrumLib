using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.BasicMath.Tuples.Immutable;

namespace EuclideanGeometryLib.Curves.Space3D
{
    public interface ICurve3D : IGeometricElement
    {
        Tuple3D GetPointAt(double t);
    }
}