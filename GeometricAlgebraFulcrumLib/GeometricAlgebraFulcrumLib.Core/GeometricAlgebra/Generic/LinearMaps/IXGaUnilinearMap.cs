using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps;

public interface IXGaUnilinearMap<T> : 
    IXGaElement<T>
{
    IXGaUnilinearMap<T> GetAdjoint();
    
    XGaMultivector<T> MapBasisBlade(IndexSet id);
    
    XGaMultivector<T> Map(XGaMultivector<T> multivector);
    
    IEnumerable<KeyValuePair<IndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions);
}