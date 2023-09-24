using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.LinearMaps.SpaceND;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms
{
    public interface IXGaFloat64Outermorphism : 
        IXGaFloat64UnilinearMap
    {
        IXGaFloat64Outermorphism GetOmAdjoint();


        XGaFloat64Vector OmMapBasisVector(int index);
        
        XGaFloat64Bivector OmMapBasisBivector(int index1, int index2);

        XGaFloat64KVector OmMapBasisBlade(IIndexSet id);
        

        XGaFloat64Vector OmMap(XGaFloat64Vector vector);

        XGaFloat64Bivector OmMap(XGaFloat64Bivector bivector);
        
        XGaFloat64HigherKVector OmMap(XGaFloat64HigherKVector kVector);

        XGaFloat64KVector OmMap(XGaFloat64KVector kVector);

        XGaFloat64Multivector OmMap(XGaFloat64Multivector multivector);

        
        IEnumerable<KeyValuePair<IIndexSet, XGaFloat64Vector>> GetOmMappedBasisVectors(int vSpaceDimensions);

        //IEnumerable<IndexBivectorStorageRecord> GetOmMappedBasisBivectors();

        //IEnumerable<IndexKVectorStorageRecord> GetOmMappedBasisKVectors(uint grade);


        LinFloat64UnilinearMap GetVectorMapPart(int vSpaceDimensions);
    }
}