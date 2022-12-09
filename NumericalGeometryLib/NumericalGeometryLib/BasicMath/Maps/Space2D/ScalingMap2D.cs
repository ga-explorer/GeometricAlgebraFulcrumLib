using System;
using NumericalGeometryLib.BasicMath.Matrices;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace NumericalGeometryLib.BasicMath.Maps.Space2D
{
    public sealed class ScalingMap2D : IAffineMap2D
    {
        public SquareMatrix3 ToSquareMatrix3()
        {
            throw new NotImplementedException();
        }

        public double[,] ToArray2D()
        {
            throw new NotImplementedException();
        }

        public Float64Tuple2D MapPoint(IFloat64Tuple2D point)
        {
            throw new NotImplementedException();
        }

        public Float64Tuple2D MapVector(IFloat64Tuple2D vector)
        {
            throw new NotImplementedException();
        }

        public Float64Tuple2D MapNormal(IFloat64Tuple2D normal)
        {
            throw new NotImplementedException();
        }

        public IAffineMap2D InverseMap()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
