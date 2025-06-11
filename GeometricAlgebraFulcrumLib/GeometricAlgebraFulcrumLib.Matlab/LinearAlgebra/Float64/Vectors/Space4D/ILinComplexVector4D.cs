using System.Numerics;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.Space4D;

public interface ILinComplexVector4D :
    IAlgebraicElement,
    IQuad<Complex>
{
    Complex X { get; }

    Complex Y { get; }

    Complex Z { get; }

    Complex W { get; }
}