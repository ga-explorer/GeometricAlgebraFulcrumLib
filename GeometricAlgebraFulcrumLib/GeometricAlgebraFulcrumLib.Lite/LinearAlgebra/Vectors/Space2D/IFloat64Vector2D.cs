using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D
{
    public interface IFloat64Vector2D : 
        ILinearElement, 
        IPair<double>
    {
        Float64Scalar X { get; }

        Float64Scalar Y { get; }
    }
}