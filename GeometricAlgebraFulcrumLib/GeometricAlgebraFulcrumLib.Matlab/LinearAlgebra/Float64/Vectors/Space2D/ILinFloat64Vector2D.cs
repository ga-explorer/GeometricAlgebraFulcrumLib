using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinFloat64Vector2D :
    IFloat64LinearAlgebraElement,
    IPair<double>
{
    double X { get; }

    double Y { get; }
}