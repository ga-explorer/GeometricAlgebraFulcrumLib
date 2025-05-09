using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Generic.LinearMaps;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;

public interface IRGaOutermorphism<T> : 
    IRGaUnilinearMap<T>
{
    IRGaOutermorphism<T> GetOmAdjoint();


    RGaVector<T> OmMapBasisVector(int index);
        
    RGaBivector<T> OmMapBasisBivector(int index1, int index2);

    RGaKVector<T> OmMapBasisBlade(ulong id);
        

    RGaVector<T> OmMap(RGaVector<T> vector);

    RGaBivector<T> OmMap(RGaBivector<T> bivector);
        
    RGaHigherKVector<T> OmMap(RGaHigherKVector<T> kVector);

    RGaKVector<T> OmMap(RGaKVector<T> kVector);

    RGaMultivector<T> OmMap(RGaMultivector<T> multivector);

    
    IEnumerable<KeyValuePair<ulong, RGaVector<T>>> GetOmMappedBasisVectors(int vSpaceDimensions);

    //IEnumerable<IndexBivectorStorageRecord<T>> GetOmMappedBasisBivectors();

    //IEnumerable<IndexKVectorStorageRecord<T>> GetOmMappedBasisKVectors(uint grade);


    LinUnilinearMap<T> GetVectorMapPart(int vSpaceDimensions);
}