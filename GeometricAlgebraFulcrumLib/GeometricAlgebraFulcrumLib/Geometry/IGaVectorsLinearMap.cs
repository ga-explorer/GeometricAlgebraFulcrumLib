using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public interface IGaVectorsLinearMap<T> 
        : IGaGeometricElement<T>
    {
        IGaVectorsLinearMap<T> GetAdjoint();

        IGaVectorStorage<T> MapBasisVector(int index);

        IGaVectorStorage<T> MapBasisVector(ulong index);

        IGaBivectorStorage<T> MapBasisBivector(int index1, int index2);

        IGaBivectorStorage<T> MapBasisBivector(ulong index1, ulong index2);

        IGaKVectorStorage<T> MapBasisBlade(ulong id);

        IGaKVectorStorage<T> MapBasisBlade(int grade, ulong index);

        IGaScalarStorage<T> MapScalar(IGaScalarStorage<T> storage);

        IGaKVectorStorage<T> MapTerm(IGaKVectorTermStorage<T> storage);

        IGaVectorStorage<T> MapVector(IGaVectorStorage<T> storage);

        IGaBivectorStorage<T> MapBivector(IGaBivectorStorage<T> storage);

        IGaKVectorStorage<T> MapKVector(IGaKVectorStorage<T> storage);

        IGaMultivectorStorage<T> MapMultivector(IGaMultivectorGradedStorage<T> storage);

        IGaMultivectorStorage<T> MapMultivector(IGaMultivectorTermsStorage<T> storage);

        IGaMultivectorStorage<T> MapMultivector(IGaMultivectorStorage<T> storage);
    }
}