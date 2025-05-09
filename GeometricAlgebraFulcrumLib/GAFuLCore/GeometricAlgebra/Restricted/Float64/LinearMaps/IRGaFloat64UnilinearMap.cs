using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps;

public interface IRGaFloat64UnilinearMap : 
    IRGaFloat64Element
{
    IRGaFloat64UnilinearMap GetAdjoint();
        
    RGaFloat64Multivector MapBasisBlade(ulong id);
    
    RGaFloat64Multivector Map(RGaFloat64Multivector multivector);
    
    IEnumerable<KeyValuePair<ulong, RGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions);
}