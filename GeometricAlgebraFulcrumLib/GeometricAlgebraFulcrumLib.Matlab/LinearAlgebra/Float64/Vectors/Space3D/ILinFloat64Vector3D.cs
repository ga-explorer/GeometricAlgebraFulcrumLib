using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space3D;

public interface ILinFloat64Vector3D :
    IFloat64LinearAlgebraElement,
    ITriplet<double>
{
    double X { get; }

    double Y { get; }

    double Z { get; }
}