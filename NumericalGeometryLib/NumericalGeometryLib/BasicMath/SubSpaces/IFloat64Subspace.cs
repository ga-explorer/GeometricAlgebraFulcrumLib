using System.Collections.Generic;
using NumericalGeometryLib.BasicMath.Tuples.Mutable;

namespace NumericalGeometryLib.BasicMath.SubSpaces;

public interface IFloat64Subspace
{
    int Dimensions { get; }

    int SubspaceDimensions { get; }

    IEnumerable<Float64Tuple> BasisVectors { get; }

    bool NearContains(Float64Tuple vector, double epsilon = 1e-12);

    bool NearContains(IFloat64Subspace subspace, double epsilon = 1e-12);
}