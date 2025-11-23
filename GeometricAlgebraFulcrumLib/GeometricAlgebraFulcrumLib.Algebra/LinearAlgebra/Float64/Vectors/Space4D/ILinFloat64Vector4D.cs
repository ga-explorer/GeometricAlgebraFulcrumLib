using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space4D;

public interface ILinFloat64Vector4D :
    ILinFloat64Vector,
    IQuad<double>
{
    double X { get; }

    double Y { get; }

    double Z { get; }

    double W { get; }
}