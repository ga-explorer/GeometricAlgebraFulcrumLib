using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms
{
    public interface IRGaFloat64Outermorphism : 
        IRGaFloat64UnilinearMap
    {
        IRGaFloat64Outermorphism GetOmAdjoint();


        RGaFloat64Vector OmMapBasisVector(int index);
        
        RGaFloat64Bivector OmMapBasisBivector(int index1, int index2);

        RGaFloat64KVector OmMapBasisBlade(ulong id);
        

        RGaFloat64Vector OmMap(RGaFloat64Vector vector);

        RGaFloat64Bivector OmMap(RGaFloat64Bivector bivector);
        
        RGaFloat64HigherKVector OmMap(RGaFloat64HigherKVector kVector);

        RGaFloat64KVector OmMap(RGaFloat64KVector kVector);

        RGaFloat64Multivector OmMap(RGaFloat64Multivector multivector);

        
        IEnumerable<KeyValuePair<ulong, RGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions);

        //IEnumerable<IndexBivectorStorageRecord> GetOmMappedBasisBivectors();

        //IEnumerable<IndexKVectorStorageRecord> GetOmMappedBasisKVectors(uint grade);


        LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions);
    }
}