using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D
{
    public interface IFloat64Vector3D : 
        ILinearElement, 
        ITriplet<double>
    {
        Float64Scalar X { get; }

        Float64Scalar Y { get; }

        Float64Scalar Z { get; }
    }
}