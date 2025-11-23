using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

public interface ILinFloat64Vector3D :
    ILinFloat64Vector,
    ITriplet<double>
{
    double X { get; }

    double Y { get; }

    double Z { get; }
}