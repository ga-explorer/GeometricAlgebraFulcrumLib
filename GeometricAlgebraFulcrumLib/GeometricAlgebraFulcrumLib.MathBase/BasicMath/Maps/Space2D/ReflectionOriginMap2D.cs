using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Maps.Space2D
{
    public sealed class ReflectionOriginMap2D : 
        IAffineMap2D
    {
        public SquareMatrix3 GetSquareMatrix3()
        {
            throw new NotImplementedException();
        }

        public double[,] GetArray2D()
        {
            throw new NotImplementedException();
        }

        public bool SwapsHandedness 
            => false;

        public Float64Vector2D MapPoint(IFloat64Vector2D point)
        {
            throw new NotImplementedException();
        }

        public Float64Vector2D MapVector(IFloat64Vector2D vector)
        {
            throw new NotImplementedException();
        }

        public Float64Vector2D MapNormal(IFloat64Vector2D normal)
        {
            throw new NotImplementedException();
        }

        public IAffineMap2D GetInverseAffineMap()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
