using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinComplexVector2D :
    IAlgebraicElement,
    IPair<Complex>
{
    Complex X { get; }

    Complex Y { get; }
}