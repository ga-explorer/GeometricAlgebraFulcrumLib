using System.Numerics;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Vectors.Space3D;

public interface ILinComplexVector3D :
    IAlgebraicElement,
    ITriplet<Complex>
{
    Complex X { get; }

    Complex Y { get; }

    Complex Z { get; }
}