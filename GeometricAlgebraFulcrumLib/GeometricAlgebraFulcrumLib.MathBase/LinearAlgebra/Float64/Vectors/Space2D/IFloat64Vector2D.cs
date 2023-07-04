using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D
{
    public interface IFloat64Vector2D : 
        ILinearElement, 
        IPair<double>
    {
        Float64Scalar X { get; }

        Float64Scalar Y { get; }
    }
}