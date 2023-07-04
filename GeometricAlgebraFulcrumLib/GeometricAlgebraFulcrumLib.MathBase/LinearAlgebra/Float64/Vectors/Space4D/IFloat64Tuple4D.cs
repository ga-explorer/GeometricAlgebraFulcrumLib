using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space4D
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