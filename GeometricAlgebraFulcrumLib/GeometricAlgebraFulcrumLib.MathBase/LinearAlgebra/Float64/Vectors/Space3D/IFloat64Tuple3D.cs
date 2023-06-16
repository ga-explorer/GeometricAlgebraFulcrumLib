using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
{
    public interface IFloat64Tuple3D : 
        ILinearElement, 
        ITriplet<double>
    {
        Float64Scalar X { get; }

        Float64Scalar Y { get; }

        Float64Scalar Z { get; }
    }
}