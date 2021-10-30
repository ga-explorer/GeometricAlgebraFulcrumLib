using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace EuclideanGeometryLib.BasicMath.Maps.Space2D
{
    public sealed class IdentityMap2D : IAffineMap2D
    {
        public AffineMapMatrix3X3 ToMatrix()
        {
            return AffineMapMatrix3X3.CreateIdentityMatrix();
        }

        public double[,] ToArray2D()
        {
            throw new System.NotImplementedException();
        }

        public ITuple2D MapPoint(ITuple2D point)
        {
            return point;
        }

        public ITuple2D MapVector(ITuple2D vector)
        {
            return vector;
        }

        public ITuple2D MapNormal(ITuple2D normal)
        {
            return normal;
        }

        public IAffineMap2D InverseMap()
        {
            return this;
        }
    }
}
