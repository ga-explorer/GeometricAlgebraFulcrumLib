using System.Numerics;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

public interface ILinComplexVector2D :
    IAlgebraicElement,
    IPair<Complex>
{
    Complex X { get; }

    Complex Y { get; }
}