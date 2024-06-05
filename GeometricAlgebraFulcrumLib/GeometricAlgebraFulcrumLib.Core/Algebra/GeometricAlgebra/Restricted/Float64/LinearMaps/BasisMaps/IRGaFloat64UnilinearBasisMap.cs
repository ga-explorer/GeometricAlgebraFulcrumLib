using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Basis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.BasisMaps;

/// <summary>
/// Represents a scaled permutation linear map on multivectors
/// </summary>
public interface IRGaFloat64UnilinearBasisMap :
    IRGaFloat64Element
{
    RGaFloat64ScaledBasisBlade MapBasisBlade(ulong basisBladeId);

    IEnumerable<KeyValuePair<ulong, RGaFloat64ScaledBasisBlade>> GetMappedBasisBlades(int vSpaceDimensions);
}