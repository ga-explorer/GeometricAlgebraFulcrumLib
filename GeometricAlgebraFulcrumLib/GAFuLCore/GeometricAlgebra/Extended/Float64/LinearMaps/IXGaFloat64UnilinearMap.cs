using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps;

public interface IXGaFloat64UnilinearMap : 
    IXGaFloat64Element
{
    IXGaFloat64UnilinearMap GetAdjoint();
    
    XGaFloat64Multivector MapBasisBlade(IIndexSet id);
    
    XGaFloat64Multivector Map(XGaFloat64Multivector multivector);
    
    IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Multivector>> GetMappedBasisBlades(int vSpaceDimensions);
}