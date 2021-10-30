using System;
using EuclideanGeometryLib.BasicMath.Matrices;
using EuclideanGeometryLib.BasicMath.Tuples;

namespace EuclideanGeometryLib.BasicMath.Maps.Space2D
{
    public sealed class ReflectionOriginMap2D : IAffineMap2D
    {
        public AffineMapMatrix3X3 ToMatrix()
        {
            throw new NotImplementedException();
        }

        public double[,] ToArray2D()
        {
            throw new NotImplementedException();
        }

        public ITuple2D MapPoint(ITuple2D point)
        {
            throw new NotImplementedException();
        }

        public ITuple2D MapVector(ITuple2D vector)
        {
            throw new NotImplementedException();
        }

        public ITuple2D MapNormal(ITuple2D normal)
        {
            throw new NotImplementedException();
        }

        public IAffineMap2D InverseMap()
        {
            throw new NotImplementedException();
        }
    }
}
