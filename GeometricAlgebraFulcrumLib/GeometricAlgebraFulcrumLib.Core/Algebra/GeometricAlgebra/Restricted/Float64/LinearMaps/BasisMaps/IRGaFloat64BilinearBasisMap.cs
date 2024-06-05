using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Basis;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.BasisMaps;

/// <summary>
/// Represents a scaled permutation bilinear map on multivectors
/// All bilinear GA products can be represented using this interface
/// </summary>
public interface IRGaFloat64BilinearBasisMap :
    IRGaFloat64Element
{
    RGaFloat64ScaledBasisBlade MapBasisBlades(ulong basisBladeId1, ulong basisBladeId2);

    IEnumerable<KeyValuePair<Pair<ulong>, RGaFloat64ScaledBasisBlade>> GetMappedBasisBlades(int vSpaceDimensions);
}