using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.LinearMaps;

public interface IXGaFloat64UnilinearMap : 
    IXGaFloat64Element
{
    IXGaFloat64UnilinearMap GetAdjoint();
    
    XGaFloat64Multivector MapBasisBlade(IndexSet id);
    
    XGaFloat64Multivector Map(XGaFloat64Multivector multivector);
    
    IEnumerable<KeyValuePair<IndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions);
}