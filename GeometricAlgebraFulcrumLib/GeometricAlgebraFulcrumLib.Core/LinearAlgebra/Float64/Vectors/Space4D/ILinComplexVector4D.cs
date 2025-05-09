using System.Numerics;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space4D;

public interface ILinComplexVector4D :
    IAlgebraicElement,
    IQuad<Complex>
{
    Complex X { get; }

    Complex Y { get; }

    Complex Z { get; }

    Complex W { get; }
}