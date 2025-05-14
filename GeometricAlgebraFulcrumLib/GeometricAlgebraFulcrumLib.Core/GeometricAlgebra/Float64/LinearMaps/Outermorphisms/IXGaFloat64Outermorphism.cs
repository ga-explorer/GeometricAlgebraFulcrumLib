using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.LinearMaps.SpaceND;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public interface IXGaFloat64Outermorphism : 
    IXGaFloat64UnilinearMap
{
    IXGaFloat64Outermorphism GetOmAdjoint();


    XGaFloat64Vector OmMapBasisVector(int index);
        
    XGaFloat64Bivector OmMapBasisBivector(int index1, int index2);

    XGaFloat64KVector OmMapBasisBlade(IndexSet id);
        

    XGaFloat64Vector OmMap(XGaFloat64Vector vector);

    XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector);
        
    XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector);

    XGaFloat64KVector OmMap(XGaFloat64KVector kVector);

    XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector);

        
    IEnumerable<KeyValuePair<IndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions);

    //IEnumerable<IndexBivectorStorageRecord> GetOmMappedBasisBivectors();

    //IEnumerable<IndexKVectorStorageRecord> GetOmMappedBasisKVectors(uint grade);


    LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions);
}