using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space4D
{
    public interface IFloat64Vector4D : 
        ILinearElement, 
        IQuad<double>
    {
        Float64Scalar X { get; }

        Float64Scalar Y { get; }

        Float64Scalar Z { get; }

        Float64Scalar W { get; }
    }
}