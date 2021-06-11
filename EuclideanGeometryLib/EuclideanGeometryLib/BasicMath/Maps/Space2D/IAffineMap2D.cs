using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace EuclideanGeometryLib.BasicMath.Maps.Space2D
{
    public interface IAffineMap2D
    {
        Matrix3X3 ToMatrix();

        ITuple2D MapPoint(ITuple2D point);

        ITuple2D MapVector(ITuple2D vector);

        ITuple2D MapNormal(ITuple2D normal);

        IAffineMap2D InverseMap();
    }
}