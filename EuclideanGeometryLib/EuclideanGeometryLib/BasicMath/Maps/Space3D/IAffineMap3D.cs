using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace EuclideanGeometryLib.BasicMath.Maps.Space3D
{
    public interface IAffineMap3D
    {
        bool SwapsHandedness { get; }

        Matrix4X4 ToMatrix();

        ITuple3D MapPoint(ITuple3D point);

        ITuple3D MapVector(ITuple3D vector);

        ITuple3D MapNormal(ITuple3D normal);

        IAffineMap3D InverseMap();
    }
}