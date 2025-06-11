using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Matlab.LinearAlgebra.Float64.SubSpaces.SpaceND;

public interface ILinFloat64Subspace :
    IFloat64LinearAlgebraElement
{
    int SubspaceDimensions { get; }

    IEnumerable<LinFloat64Vector> BasisVectors { get; }

    LinFloat64Vector GetVectorProjection(LinFloat64Vector vector);

    LinFloat64PolarAngle GetVectorProjectionPolarAngle(LinFloat64Vector vector);

    LinFloat64Vector GetVectorRejection(LinFloat64Vector vector);

    bool NearContains(LinFloat64Vector vector, double zeroEpsilon = Float64Utils.ZeroEpsilon);

    bool NearContains(ILinFloat64Subspace subspace, double zeroEpsilon = Float64Utils.ZeroEpsilon);
}