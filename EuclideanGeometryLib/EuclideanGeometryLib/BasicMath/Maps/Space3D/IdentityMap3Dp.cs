using System.Numerics;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace EuclideanGeometryLib.BasicMath.Maps.Space3D
{
    public sealed class IdentityMap3Dp : IAffineMap3D
    {
        public static IdentityMap3Dp Default { get; } 
            = new IdentityMap3Dp();


        public bool SwapsHandedness => false;


        public AffineMapMatrix4X4 ToMatrix()
        {
            return AffineMapMatrix4X4.CreateIdentityMatrix();
        }

        public Matrix4x4 ToSystemNumericsMatrix()
        {
            return Matrix4x4.Identity;
        }

        public double[,] ToArray2D()
        {
            throw new System.NotImplementedException();
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
