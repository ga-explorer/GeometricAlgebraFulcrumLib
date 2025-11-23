using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinFloat64Vector2D :
    ILinFloat64Vector,
    IPair<double>
{
    double X { get; }

    double Y { get; }
}