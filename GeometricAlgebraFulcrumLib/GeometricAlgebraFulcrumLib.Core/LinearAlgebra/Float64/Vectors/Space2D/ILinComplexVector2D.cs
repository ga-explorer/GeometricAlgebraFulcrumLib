using System.Numerics;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinComplexVector2D :
    IAlgebraicElement,
    IPair<Complex>
{
    Complex X { get; }

    Complex Y { get; }
}