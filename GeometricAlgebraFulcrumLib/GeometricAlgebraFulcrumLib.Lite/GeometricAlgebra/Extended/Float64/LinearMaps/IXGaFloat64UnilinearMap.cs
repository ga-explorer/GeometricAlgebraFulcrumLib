using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps;

public interface IXGaFloat64UnilinearMap : 
    IXGaFloat64Element
{
    IXGaFloat64UnilinearMap GetAdjoint();
    
    XGaFloat64Multivector MapBasisBlade(IIndexSet id);
    
    XGaFloat64Multivector Map(XGaFloat64Multivector multivector);
    
    IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions);
}