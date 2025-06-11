using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

public interface ILinFloat64Vector4D :
    IFloat64LinearAlgebraElement,
    IQuad<double>
{
    double X { get; }

    double Y { get; }

    double Z { get; }

    double W { get; }
}