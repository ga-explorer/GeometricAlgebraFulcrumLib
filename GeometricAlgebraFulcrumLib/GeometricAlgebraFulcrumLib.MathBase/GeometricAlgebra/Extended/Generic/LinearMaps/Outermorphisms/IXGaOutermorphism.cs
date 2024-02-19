using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;

public interface IXGaOutermorphism<T> : 
    IXGaUnilinearMap<T>
{
    IXGaOutermorphism<T> GetOmAdjoint();


    XGaVector<T> OmMapBasisVector(int index);
        
    XGaBivector<T> OmMapBasisBivector(int index1, int index2);

    XGaKVector<T> OmMapBasisBlade(IIndexSet id);
        

    XGaVector<T> OmMap(XGaVector<T> vector);

    XGaBivector<T> OmMap(XGaBivector<T> bivector);
        
    XGaHigherKVector<T> OmMap(XGaHigherKVector<T> kVector);

    XGaKVector<T> OmMap(XGaKVector<T> kVector);

    XGaMultivector<T> OmMap(XGaMultivector<T> multivector);

        
    IEnumerable<KeyValuePair<IIndexSet, XGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions);

    //IEnumerable<IndexBivectorStorageRecord<T>> GetOmMappedBasisBivectors();

    //IEnumerable<IndexKVectorStorageRecord<T>> GetOmMappedBasisKVectors(uint grade);


    LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions);
}