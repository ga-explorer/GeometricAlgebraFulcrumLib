using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps
{
    public interface IXGaUnilinearMap<T> : 
        IXGaElement<T>
    {
        IXGaUnilinearMap<T> GetAdjoint();
    
        XGaMultivector<T> MapBasisBlade(IIndexSet id);
    
        XGaMultivector<T> Map(XGaMultivector<T> multivector);
    
        IEnumerable<KeyValuePair<IIndexSet, XGaMultivector<T>>> GetMappedBasisBlades(int vSpaceDimensions);
    }
}