using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace EuclideanGeometryLib.BasicMath.Maps.Space3D
{
    public sealed class IdentityMap3Dp : IAffineMap3D
    {
        public static IdentityMap3Dp Default { get; } 
            = new IdentityMap3Dp();


        public bool SwapsHandedness => false;


        public Matrix4X4 ToMatrix()
        {
            return Matrix4X4.CreateIdentityMatrix();
        }

        public ITuple3D MapPoint(ITuple3D point)
        {
            return point;
        }

        public ITuple3D MapVector(ITuple3D vector)
        {
            return vector;
        }

        public ITuple3D MapNormal(ITuple3D normal)
        {
            return normal;
        }

        public IAffineMap3D InverseMap()
        {
            return this;
        }


        private IdentityMap3Dp()
        {
        }
    }
}
